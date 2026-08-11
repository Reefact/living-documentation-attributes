#region Usings declarations

using System.Collections.Generic;
using System.Threading;

using Reefact.LivingDocumentation.Attributes.Posa2;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.Posa2.MonitorObjectSample {

    // Sixteen call takers reserve ambulances from one register. When none is free the honest answer is
    // "wait" rather than "no": a vehicle clears in a minute or two, and a caller told no rings the
    // neighbouring service and the incident is dispatched twice.
    //
    // Waiting is where this gets subtle. Polling every second held the register's lock sixteen times a
    // second to be told nothing had changed. Blocking while holding the lock froze the register
    // completely, because the vehicle that would have freed it could not be returned.
    //
    // MONITOR OBJECT is the shape that gets both right: the methods are serialized by the object's own
    // lock, and a method that cannot proceed waits on a condition, which gives the lock back while it
    // waits. Unlike an active object, this has no thread of its own — every method runs in the thread
    // of the call taker who called it, so a slow method blocks a person rather than a worker.

    /// <summary>
    ///     Which ambulances are free, and who has reserved which.
    /// </summary>
    [MonitorObject.MonitorObject]
    public sealed class AmbulanceRegister {

        /// <remarks>
        ///     This register's own lock, taken as a synchronized method enters and released as it leaves.
        ///     One register, one lock: sharing it with the incident log would serialize both, and neither
        ///     class would say so anywhere a reader would look.
        ///     <para>
        ///         It carries the condition role as well, because on this platform the two coincide:
        ///         <c>Monitor.Wait</c> and <c>Monitor.PulseAll</c> operate on the lock itself, so a monitor
        ///         object has exactly one condition and it is the lock. The pattern's own example needs two
        ///         — not-empty and not-full — and the way to have two here is two predicates re-tested
        ///         after every wake, not two conditions. That is worth knowing before writing the third.
        ///     </para>
        /// </remarks>
        [MonitorObject.MonitorLock]
        [MonitorObject.MonitorCondition]
        private readonly object _monitor = new object();

        private readonly Queue<string> _free = new Queue<string>();

        public AmbulanceRegister(IEnumerable<string> callSigns) {
            foreach (string callSign in callSigns) { _free.Enqueue(callSign); }
        }

        /// <summary>
        ///     Takes the next free ambulance, waiting until there is one.
        /// </summary>
        /// <remarks>
        ///     Exactly one synchronized method runs inside this register at a time, whatever the number of
        ///     call takers and whatever the number of such methods — so the register's throughput is one
        ///     method, not one per operator.
        /// </remarks>
        [MonitorObject.SynchronizedMethod]
        public string Reserve() {
            lock (_monitor) {
                while (_free.Count == 0) { Monitor.Wait(_monitor); }

                return _free.Dequeue();
            }
        }

        /// <summary>
        ///     Puts an ambulance back, and wakes whoever is waiting for one.
        /// </summary>
        [MonitorObject.SynchronizedMethod]
        public void Return(string callSign) {
            lock (_monitor) {
                _free.Enqueue(callSign);
                Monitor.PulseAll(_monitor);
            }
        }

        /// <summary>
        ///     How many are free right now.
        /// </summary>
        [MonitorObject.SynchronizedMethod]
        public int FreeCount() {
            lock (_monitor) { return _free.Count; }
        }

    }

}
