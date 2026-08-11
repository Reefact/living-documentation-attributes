#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.AsynchronousCompletionTokenSample {

    // The registry fetches come back out of order and look alike: a registry name and a block of text.
    // The service needs to know which vessel it was for, which operator asked, whether the answer is
    // still wanted because the vessel may have been diverted, and what to do next.
    //
    // The first version kept a dictionary keyed by registry name. Two vessels in the queue for the same
    // registry collided, and a bulk carrier was cleared on a chemical tanker's history.
    //
    // ASYNCHRONOUS COMPLETION TOKEN sends the service a value the service returns untouched. The value
    // means everything to the operator's side and nothing at all to the registry's.

    /// <summary>
    ///     Everything the service needs in order to make sense of one completion.
    /// </summary>
    /// <remarks>
    ///     Opaque to the registry and meaningful here, which is the whole trick: it travels through code
    ///     that cannot interpret it and must not try. Anything the completion will need has to be reachable
    ///     from this value, and what was left out is not recoverable when the answer arrives.
    /// </remarks>
    [AsynchronousCompletionToken.AsynchronousCompletionToken]
    public sealed class ClearanceToken {

        public ClearanceToken(string vesselId, string operatorId, int sequence) {
            VesselId   = vesselId;
            OperatorId = operatorId;
            Sequence   = sequence;
        }

        public string VesselId { get; }

        public string OperatorId { get; }

        public int Sequence { get; }

    }

    /// <summary>
    ///     A registry, which performs the lookup and gives the token back.
    /// </summary>
    /// <remarks>
    ///     May hold many tokens at once and must interpret none of them. Reading one is the coupling the
    ///     pattern exists to avoid — and it is invisible from the caller's side, because a service that
    ///     reads a token behaves identically until the caller changes what a token means.
    /// </remarks>
    [AsynchronousCompletionToken.Service(AsynchronousCompletionToken = typeof(ClearanceToken))]
    public interface IPortStateRegistry {

        void BeginLookup(string registry, ClearanceToken token);

        void OnLookupDone(string history, ClearanceToken token);

    }

    /// <summary>
    ///     The clearance desk: it asks, and it is the only participant that can read the answer.
    /// </summary>
    /// <remarks>
    ///     The token is what lets two vessels be in the queue for one registry at the same time. The
    ///     version this replaced keyed on the registry name, which is the collision that cleared a bulk
    ///     carrier on a chemical tanker's history.
    /// </remarks>
    [AsynchronousCompletionToken.Client(AsynchronousCompletionToken = typeof(ClearanceToken))]
    public sealed class ClearanceDesk {

        private readonly IDictionary<int, string> _clearedBy;

        private int _sequence;

        public ClearanceDesk(IDictionary<int, string> clearedBy) {
            _clearedBy = clearedBy;
        }

        public ClearanceToken Ask(IPortStateRegistry registry, string registryName, string vesselId, string operatorId) {
            ClearanceToken token = new ClearanceToken(vesselId, operatorId, ++_sequence);
            registry.BeginLookup(registryName, token);

            return token;
        }

        public void Completed(string history, ClearanceToken token) {
            _clearedBy[token.Sequence] = $"{token.VesselId} by {token.OperatorId}";
        }

    }

}
