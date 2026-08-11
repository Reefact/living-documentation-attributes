#region Usings declarations

using System.Collections.Generic;
using System.Threading;

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.ActiveObjectSample {

    // An emergency call taker types an address and presses dispatch. Choosing which ambulance goes
    // takes between forty milliseconds and four seconds, depending on how many vehicles are in the
    // air-ambulance handshake, and it must not be four seconds of a frozen screen while somebody is
    // describing a road accident.
    //
    // The first version ran the choice on the operator's thread. The second ran it on a background
    // thread and passed a callback, and the callback ran on whichever thread finished, so two
    // dispatches could touch the fleet register at once — which they did, twice, sending two vehicles
    // to one incident and none to another.
    //
    // ACTIVE OBJECT gives the fleet register a thread of its own. The call returns at once with
    // something to wait on, the work is queued, and the servant is only ever touched by one thread.

    /// <summary>
    ///     What a call taker calls to dispatch a vehicle.
    /// </summary>
    /// <remarks>
    ///     Every method here returns immediately, having queued the work. A caller who reasons about
    ///     what has happened by the time the call returns is reasoning about nothing: at that point the
    ///     request has not run.
    /// </remarks>
    [ActiveObject.Proxy]
    public interface IDispatchDesk {

        Pending<string> Dispatch(string incidentId, string category);

    }

    /// <summary>
    ///     Stands for a call sign that has not been chosen yet.
    /// </summary>
    /// <remarks>
    ///     Returned the moment the dispatch is asked for. A caller that neither waits on it nor polls it
    ///     has asked for an ambulance and will never hear whether one was found.
    /// </remarks>
    [ActiveObject.Future(Proxy = typeof(IDispatchDesk))]
    public sealed class Pending<T> {

        private readonly ManualResetEventSlim _arrived = new ManualResetEventSlim(false);

        private T? _value;

        public void Complete(T value) {
            _value = value;
            _arrived.Set();
        }

        public T Wait() {
            _arrived.Wait();

            return _value!;
        }

    }

    /// <summary>
    ///     One queued dispatch, with everything needed to carry it out later.
    /// </summary>
    /// <remarks>
    ///     Carries the arguments, the servant to apply them to, the future to put the answer in, and the
    ///     guard that says whether it may run yet. The guard is what lets the scheduler hold a request
    ///     back rather than fail it.
    /// </remarks>
    [ActiveObject.MethodRequest(Proxy = typeof(IDispatchDesk))]
    public abstract class DispatchRequest {

        public abstract bool Guard();

        public abstract void Call();

    }

    /// <summary>
    ///     The dispatches that are pending.
    /// </summary>
    /// <remarks>
    ///     Bounded, and that bound is the interesting number: it is what decouples the operator's thread
    ///     from the register's, and what an operator waits on once the register is further behind than
    ///     the backlog is long.
    /// </remarks>
    [ActiveObject.ActivationList(Proxy = typeof(IDispatchDesk))]
    public sealed class DispatchBacklog {

        private readonly object                _gate     = new object();
        private readonly Queue<DispatchRequest> _pending = new Queue<DispatchRequest>();
        private readonly int                   _bound;

        public DispatchBacklog(int bound) {
            _bound = bound;
        }

        public void Enqueue(DispatchRequest request) {
            lock (_gate) {
                while (_pending.Count == _bound) { Monitor.Wait(_gate); }

                _pending.Enqueue(request);
                Monitor.PulseAll(_gate);
            }
        }

        public DispatchRequest Dequeue() {
            lock (_gate) {
                while (_pending.Count == 0) { Monitor.Wait(_gate); }

                DispatchRequest request = _pending.Dequeue();
                Monitor.PulseAll(_gate);

                return request;
            }
        }

    }

    /// <summary>
    ///     The register's own thread, and the order it works in.
    /// </summary>
    /// <remarks>
    ///     The order it chooses is the dispatch policy — a cardiac arrest ahead of a broken wrist that
    ///     was queued before it — and that is a decision somebody made, not a property of the queue.
    /// </remarks>
    [ActiveObject.Scheduler(Proxy = typeof(IDispatchDesk))]
    public sealed class DispatchScheduler {

        private readonly DispatchBacklog _backlog;

        public DispatchScheduler(DispatchBacklog backlog) {
            _backlog = backlog;
        }

        public void Run(CancellationToken stopping) {
            while (!stopping.IsCancellationRequested) {
                DispatchRequest request = _backlog.Dequeue();
                if (request.Guard()) { request.Call(); }
            }
        }

    }

    /// <summary>
    ///     Which vehicles exist, where they are, and which are free.
    /// </summary>
    /// <remarks>
    ///     Holds no synchronization at all, and that is deliberate: it is only ever touched by the
    ///     scheduler's thread. The same register can be put under a different dispatch policy without a
    ///     line of it changing, which is what the absence of locks here buys.
    /// </remarks>
    [ActiveObject.Servant(Proxy = typeof(IDispatchDesk))]
    public sealed class FleetRegister {

        private readonly Dictionary<string, string> _assigned = new Dictionary<string, string>();

        public bool HasFreeVehicle(string category) {
            return _assigned.Count < 40 && category.Length > 0;
        }

        public string Assign(string incidentId, string category) {
            string callSign = $"{category[0]}{_assigned.Count + 101}";
            _assigned[incidentId] = callSign;

            return callSign;
        }

    }

}
