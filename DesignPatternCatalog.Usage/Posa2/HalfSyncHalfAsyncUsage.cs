#region Usings declarations

using System.Collections.Generic;
using System.Threading;

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.HalfSyncHalfAsyncSample {

    // Two thousand emergency calls an hour arrive at a switchboard, and each one is answered by a person
    // who then spends four minutes on it: taking an address, asking whether the patient is breathing,
    // typing while listening.
    //
    // A thread per call is four minutes of stack for something whose first millisecond is the only part
    // that is urgent. Handling everything on the switchboard's own thread is worse: the moment a call
    // taker's screen is slow to save, the switchboard stops accepting calls, and a caller hears nothing.
    //
    // HALF-SYNC/HALF-ASYNC splits the system in two and puts a queue between. The switchboard never
    // blocks, because it has nothing to block on. The desks block freely, because blocking is what
    // reads like a conversation. Neither knows the other exists.

    /// <summary>
    ///     The buffering and notification point between the switchboard and the desks.
    /// </summary>
    /// <remarks>
    ///     The only place the two layers meet, which is what makes the split real rather than a drawing:
    ///     a direct call from a desk into the switchboard would put four minutes of blocking into the
    ///     thread that is supposed to be accepting calls.
    /// </remarks>
    [HalfSyncHalfAsync.QueueingLayer]
    public sealed class CallQueue {

        private readonly object         _gate    = new object();
        private readonly Queue<string> _waiting = new Queue<string>();

        public void Offer(string callReference) {
            lock (_gate) {
                _waiting.Enqueue(callReference);
                Monitor.PulseAll(_gate);
            }
        }

        public string Take() {
            lock (_gate) {
                while (_waiting.Count == 0) { Monitor.Wait(_gate); }

                return _waiting.Dequeue();
            }
        }

    }

    /// <summary>
    ///     Receives calls as they land and puts them in the queue.
    /// </summary>
    /// <remarks>
    ///     Never blocks, because it has no thread of its own to block on: it is driven by the telephony
    ///     stack's callbacks. A blocking call added in here does not make this layer slower — it stops the
    ///     switchboard accepting calls at all, which is the failure the whole split exists to prevent.
    /// </remarks>
    [HalfSyncHalfAsync.AsynchronousTaskLayer(QueueingLayer = typeof(CallQueue))]
    public sealed class Switchboard {

        private readonly CallQueue _queue;

        public Switchboard(CallQueue queue) {
            _queue = queue;
        }

        public void OnCallLanded(string callReference) {
            _queue.Offer(callReference);
        }

    }

    /// <summary>
    ///     One call taker's desk: takes a call from the queue and works it through to the end.
    /// </summary>
    /// <remarks>
    ///     Runs in a thread of its own and may block as much as it likes — it has a stack, so the four
    ///     minutes of a call read as a sequence of steps rather than as a state machine. Everything in
    ///     this system that is easy to follow lives on this side of the queue.
    /// </remarks>
    [HalfSyncHalfAsync.SynchronousTaskLayer(QueueingLayer = typeof(CallQueue))]
    public sealed class OperatorDesk {

        private readonly CallQueue                _queue;
        private readonly Func<string, string> _work;

        public OperatorDesk(CallQueue queue, Func<string, string> work) {
            _queue = queue;
            _work  = work;
        }

        public void Serve(CancellationToken stopping) {
            while (!stopping.IsCancellationRequested) {
                string callReference = _queue.Take();
                _work(callReference);
            }
        }

    }

}
