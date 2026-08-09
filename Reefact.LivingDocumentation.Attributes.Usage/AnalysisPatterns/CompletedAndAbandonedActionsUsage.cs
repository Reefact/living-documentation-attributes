#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.CompletedAndAbandonedActionsSample {

    // The same refit. Two tasks are dropped: one before anybody touched it, because the owner declined the
    // quote, and one halfway through, because the plating underneath turned out to be sound. Both are
    // abandoned, and only the second has hours against it.
    //
    // Modelled as a status, "abandoned" competes with "started" and the second task cannot be both. COMPLETED
    // AND ABANDONED ACTIONS makes abandonment a dimension of its own, crossing the proposed and implemented
    // split rather than sitting under one side of it.

    /// <summary>
    ///     A task given up on.
    /// </summary>
    /// <remarks>
    ///     Whether or not it was started. The reason is worth keeping: a task declined by the owner and a task
    ///     found unnecessary are the same shape and different facts.
    /// </remarks>
    [CompletedAndAbandonedActions.AbandonedAction]
    public sealed class AbandonedTask {

        public AbandonedTask(string description, string reason, decimal hoursSpentBeforeStopping) {
            Description = description;
            Reason      = reason;
            HoursSpent  = hoursSpentBeforeStopping;
        }

        public string Description { get; }

        /// <summary>"owner declined", "found unnecessary once opened".</summary>
        public string Reason { get; }

        /// <summary>Zero when it was abandoned before starting, which is a fact and not an absence.</summary>
        public decimal HoursSpent { get; }

    }

    /// <summary>
    ///     A task that ran to its end.
    /// </summary>
    /// <remarks>
    ///     Only work that was done can be complete, so this narrows the implemented side rather than adding a
    ///     second dimension — which is why nothing has to say that a quote cannot be complete.
    /// </remarks>
    [CompletedAndAbandonedActions.CompletedAction]
    public sealed class CompletedWork {

        public CompletedWork(string description, DateOnly finishedOn, string signedOffBy) {
            Description = description;
            FinishedOn  = finishedOn;
            SignedOffBy = signedOffBy;
        }

        public string Description { get; }

        public DateOnly FinishedOn { get; }

        /// <summary>A surveyor's signature. Completion is somebody's assertion, not a timer expiring.</summary>
        public string SignedOffBy { get; }

    }

}
