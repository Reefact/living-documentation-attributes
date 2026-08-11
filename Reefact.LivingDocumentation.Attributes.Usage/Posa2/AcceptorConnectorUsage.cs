#region Usings declarations

using System.Net;
using System.Net.Sockets;

using Reefact.LivingDocumentation.Attributes.Posa2;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.Posa2.AcceptorConnectorSample {

    // Two kinds of peer talk to the traffic service over the same protocol. Vessel transponders dial in,
    // so the service listens. The neighbouring VTS centre up the coast expects to be dialled, so the
    // service connects. Once a link is up, both are the same conversation: position reports in,
    // instructions out.
    //
    // The first version had two classes with the same protocol logic in both, one written around
    // Accept and one around Connect. A fix to the sequence-number wraparound went into the transponder
    // copy; six weeks later the coastal handover dropped a vessel at exactly that boundary.
    //
    // ACCEPTOR-CONNECTOR separates how a link is made from what is said over it. One handler, reached two
    // ways, and it does not know which way it was reached.

    /// <summary>
    ///     The conversation itself, once a link exists.
    /// </summary>
    /// <remarks>
    ///     Does not talk to the acceptor or the connector again after it is activated, which is exactly what
    ///     lets the same handler serve a transponder that dialled in and a centre the service dialled. A
    ///     handler that reaches back for its acceptor has quietly become two handlers.
    /// </remarks>
    [AcceptorConnector.ServiceHandler]
    public sealed class TrafficLinkHandler {

        /// <remarks>
        ///     The data-mode endpoint this handler exchanges reports through. Whether an endpoint is this or
        ///     the acceptor's passive one decides everything about its lifetime — one closes with the
        ///     conversation, the other outlives every conversation — and nothing in the type says which.
        /// </remarks>
        [AcceptorConnector.TransportEndpoint(ServiceHandler = typeof(TrafficLinkHandler))]
        private Socket? _endpoint;

        public void Activate(Socket endpoint) {
            _endpoint = endpoint;
        }

        public void OnReport(string report) {
            if (_endpoint is null) { throw new InvalidOperationException("the link is not activated"); }
        }

    }

    /// <summary>
    ///     Listens for transponders dialling in, and gives each one a handler.
    /// </summary>
    /// <remarks>
    ///     A factory, so what it decides is not how a link is used but which handler exists at all. Its own
    ///     endpoint is the passive one: it is bound once at start-up and outlives every conversation it
    ///     produces.
    /// </remarks>
    [AcceptorConnector.Acceptor(ServiceHandler = typeof(TrafficLinkHandler))]
    public sealed class TransponderAcceptor {

        private readonly Socket _listening;

        public TransponderAcceptor(IPEndPoint address) {
            _listening = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listening.Bind(address);
            _listening.Listen(64);
        }

        public TrafficLinkHandler Accept() {
            Socket               accepted = _listening.Accept();
            TrafficLinkHandler handler  = new TrafficLinkHandler();
            handler.Activate(accepted);

            return handler;
        }

    }

    /// <summary>
    ///     Dials the neighbouring centre, and gives the link the same handler.
    /// </summary>
    /// <remarks>
    ///     May finish while the caller waits or long afterwards, and the handler is written not to know
    ///     which — which is the property that makes the coastal handover and the transponder link one piece
    ///     of protocol code instead of two that drift.
    /// </remarks>
    [AcceptorConnector.Connector(ServiceHandler = typeof(TrafficLinkHandler))]
    public sealed class CoastalCentreConnector {

        public TrafficLinkHandler Connect(IPEndPoint peer) {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(peer);

            TrafficLinkHandler handler = new TrafficLinkHandler();
            handler.Activate(socket);

            return handler;
        }

    }

}
