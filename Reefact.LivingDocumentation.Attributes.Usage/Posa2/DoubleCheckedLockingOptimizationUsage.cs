#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.Posa2;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.Posa2.DoubleCheckedLockingOptimizationSample {

    // The seating plan is forty thousand seats with their stand, row, accessibility and restricted view.
    // Reading it out of the ticketing system takes a second and a half; every turnstile swipe needs it,
    // and there are two hundred a second when the gates open.
    //
    // Taking a lock on every swipe to read something that never changes after the first read is two
    // hundred acquisitions a second for one initialisation. Checking without a lock initialises it twice
    // when two gates open together.
    //
    // DOUBLE-CHECKED LOCKING OPTIMIZATION tests once outside the lock for speed and once inside it for
    // correctness. It is also the pattern most famous for being subtly wrong: without the volatile read,
    // a gate thread can be handed a plan reference that has been published before the object behind it
    // is finished. That failure appears under load, on one processor architecture, and never in a test.

    /// <summary>
    ///     The seating plan, read once from the ticketing system and shared by every gate thread.
    /// </summary>
    public sealed class SeatingPlanCache {

        /// <remarks>
        ///     Serializes the threads that find the flag unset. It is contended for a second and a half when
        ///     the gates open and untouched for the rest of the match — so a measurement taken at half time
        ///     will show this lock costing nothing, and will have measured nothing.
        /// </remarks>
        [DoubleCheckedLockingOptimization.Mutex]
        private readonly object _planGate = new object();

        /// <remarks>
        ///     Says whether the plan has been read yet, and is the plan: the reference is the flag, which is
        ///     the shape the pattern's authors describe. <c>volatile</c> is not decoration here. It is what
        ///     stops the thread that skips the lock from seeing a reference that is published before the
        ///     object it points at is built.
        /// </remarks>
        [DoubleCheckedLockingOptimization.Flag]
        private volatile IReadOnlyDictionary<string, string>? _plan;

        private readonly Func<IReadOnlyDictionary<string, string>> _ticketing;

        public SeatingPlanCache(Func<IReadOnlyDictionary<string, string>> ticketing) {
            _ticketing = ticketing;
        }

        /// <summary>
        ///     Which stand a seat is in.
        /// </summary>
        public string StandOf(string seat) {
            IReadOnlyDictionary<string, string>? plan = _plan;
            if (plan is null) {
                lock (_planGate) {
                    plan = _plan;
                    if (plan is null) {
                        plan  = ReadPlan();
                        _plan = plan;
                    }
                }
            }

            return plan[seat];
        }

        /// <remarks>
        ///     Runs exactly once in the life of the process, and is reached by perhaps one swipe in a hundred
        ///     thousand. That ratio is the entire reason for paying for two tests rather than one lock.
        /// </remarks>
        [DoubleCheckedLockingOptimization.JustOnceCriticalSection]
        private IReadOnlyDictionary<string, string> ReadPlan() {
            return _ticketing();
        }

    }

}
