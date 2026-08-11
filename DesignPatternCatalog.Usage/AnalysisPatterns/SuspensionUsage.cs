#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.SuspensionSample {

    // Shaft alignment stops for nine days waiting on a bearing from Rotterdam, restarts, then stops again for
    // four days over a holiday. The yard bills elapsed berth time and pays worked hours, so the two numbers
    // must both come out of the same record.
    //
    // A boolean "suspended" gives neither, and loses the first stoppage the moment the second begins.
    // SUSPENSION makes each pause an interval, and the status derivable from them.

    /// <summary>
    ///     One period during which a task was not proceeding.
    /// </summary>
    /// <remarks>
    ///     A record rather than a change to the task, and there may be several — which is what lets elapsed time
    ///     be told from worked time.
    /// </remarks>
    [Suspension.Suspension]
    public sealed class Stoppage {

        public Stoppage(DateOnly from, DateOnly? until, string cause) {
            if (until.HasValue && until < from) {
                throw new ArgumentException("a stoppage does not end before it begins", nameof(until));
            }
            From  = from;
            Until = until;
            Cause = cause;
        }

        /// <summary>
        ///     The interval covered.
        /// </summary>
        /// <remarks>
        ///     Open at the end while it lasts, which is what makes the current status answerable from the same
        ///     data as the history.
        /// </remarks>
        [Suspension.Period]
        public (DateOnly From, DateOnly? Until) Period => (From, Until);

        public DateOnly From { get; }

        public DateOnly? Until { get; }

        /// <summary>"awaiting bearing", "yard holiday".</summary>
        public string Cause { get; }

    }

    /// <summary>A task and its stoppages.</summary>
    public sealed class Alignment {

        private readonly List<Stoppage> _stoppages = new List<Stoppage>();

        public IReadOnlyList<Stoppage> Stoppages => _stoppages;

        public void Stop(DateOnly from, string cause) => _stoppages.Add(new Stoppage(from, null, cause));

        /// <summary>
        ///     Whether the task is stopped right now.
        /// </summary>
        /// <remarks>
        ///     Derived from the stoppages, never stored beside them: a stored flag is what lets a task be marked
        ///     suspended with no stoppage to show for it.
        /// </remarks>
        [Suspension.Suspended]
        public bool IsSuspended {
            get {
                foreach (Stoppage stoppage in _stoppages) {
                    if (!stoppage.Until.HasValue) { return true; }
                }

                return false;
            }
        }

    }

}
