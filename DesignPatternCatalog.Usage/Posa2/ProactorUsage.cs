#region Usings declarations

using System.Collections.Generic;
using System.Threading.Tasks;

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.ProactorSample {

    // Before a vessel is cleared to enter, the service fetches its port-state-control history from four
    // national registries. Each takes between two hundred milliseconds and nine seconds, and the
    // reactor's single thread cannot wait on any of them.
    //
    // Moving the fetches to threads worked until a bulk-carrier morning: forty vessels in the queue, one
    // hundred and sixty threads waiting on sockets, and the machine spent more time switching than
    // fetching.
    //
    // PROACTOR starts the fetches and waits for their completions instead of for their sockets. The
    // waiting is the operating system's, which is what it is good at, and the service holds no thread per
    // vessel.

    /// <summary>
    ///     What the service is told when a fetch finishes.
    /// </summary>
    /// <remarks>
    ///     A handler is given the completion, not the context that led to it. Which registry, for which
    ///     vessel, and what the operator was going to do with the answer are not here — which is why this
    ///     pattern arrives with the asynchronous completion token.
    /// </remarks>
    [Proactor.CompletionHandler(Proactor = typeof(ClearanceProactor))]
    public interface IHistoryCompletionHandler {

        void OnFetched(string registry, string history);

        void OnFailed(string registry, string reason);

    }

    /// <summary>
    ///     Carries out the fetches and queues their completions.
    /// </summary>
    /// <remarks>
    ///     Normally the operating system, and here the runtime's socket stack behind it. Annotating the
    ///     boundary is how a reader tells which asynchrony the platform provides from which the service
    ///     built — a distinction that decides who is responsible when completions stop arriving.
    /// </remarks>
    [Proactor.AsynchronousOperationProcessor(Proactor = typeof(ClearanceProactor))]
    public interface IRegistryTransport {

        /// <remarks>
        ///     Started on the service's behalf and performed without borrowing its thread. Nothing written
        ///     after this call has anything to do with the outcome, which is the shape most easily misread as
        ///     a sequence by whoever maintains it next.
        /// </remarks>
        [Proactor.AsynchronousOperation(Proactor = typeof(ClearanceProactor))]
        Task<string> BeginFetch(string registry, string vesselId);

    }

    /// <summary>
    ///     Asks for a vessel's history and says who is to be told the answer.
    /// </summary>
    /// <remarks>
    ///     The participant a stack trace will not show: by the time a handler runs, this one has returned.
    ///     Anything the completion needs has to have been arranged here, and this is the last moment at
    ///     which it can be.
    /// </remarks>
    [Proactor.ProactiveInitiator(Proactor = typeof(ClearanceProactor))]
    public sealed class ClearanceRequest {

        private readonly IRegistryTransport _transport;
        private readonly ClearanceProactor  _proactor;

        public ClearanceRequest(IRegistryTransport transport, ClearanceProactor proactor) {
            _transport = transport;
            _proactor  = proactor;
        }

        public void Start(string vesselId, IReadOnlyList<string> registries, IHistoryCompletionHandler handler) {
            foreach (string registry in registries) {
                _proactor.Register(registry, handler);
                _ = _transport.BeginFetch(registry, vesselId)
                              .ContinueWith(t => _proactor.OnCompleted(registry, t));
            }
        }

    }

    /// <summary>
    ///     Dispatches each completion to the handler registered with its operation.
    /// </summary>
    /// <remarks>
    ///     The reactor's counterpart moved to the other end: it waits for work to have finished rather than
    ///     for work to be startable. A completion arriving for an operation nobody registered is a fetch
    ///     whose answer is discarded, and it looks exactly like a registry that never replied.
    /// </remarks>
    [Proactor.Proactor]
    public sealed class ClearanceProactor {

        private readonly Dictionary<string, IHistoryCompletionHandler> _expected =
            new Dictionary<string, IHistoryCompletionHandler>();

        public void Register(string registry, IHistoryCompletionHandler handler) {
            _expected[registry] = handler;
        }

        public void OnCompleted(string registry, Task<string> completed) {
            if (!_expected.Remove(registry, out IHistoryCompletionHandler? handler)) { return; }

            if (completed.IsFaulted) {
                handler.OnFailed(registry, completed.Exception!.GetBaseException().Message);
            } else {
                handler.OnFetched(registry, completed.Result);
            }
        }

    }

}
