#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.ProposedAndImplementedActionSample {

    // A ship repair yard. A refit is quoted as forty tasks, and what actually happens is fifty-three: eleven of
    // the forty were never done, and twenty-four were not on the list. The yard is paid on what was planned and
    // costed on what was done, so the difference is the business.
    //
    // The tempting model is one Task with a "done" flag, which loses the eleven that were dropped and cannot
    // hold the twenty-four nobody planned. PROPOSED AND IMPLEMENTED ACTION keeps both sides, each able to exist
    // without the other.

    /// <summary>Where an action stands.</summary>
    /// <remarks>
    ///     Derived, not set: a status that can be assigned is a status that can disagree with the facts.
    /// </remarks>
    [ProposedAndImplementedAction.ActionStatus]
    public sealed class TaskStatus {

        public static readonly TaskStatus Proposed  = new("proposed");
        public static readonly TaskStatus Started   = new("started");
        public static readonly TaskStatus Completed = new("completed");

        private TaskStatus(string name) { Name = name; }

        public string Name { get; }

    }

    /// <summary>Something done or to be done in the dock.</summary>
    /// <remarks>
    ///     One type for both sides, which is what lets a report range over intentions and outcomes together.
    /// </remarks>
    [ProposedAndImplementedAction.Action]
    public abstract class YardTask {

        protected YardTask(string description, string berth, IReadOnlyList<string> performers) {
            Description = description;
            Berth       = berth;
            Performers  = performers;
        }

        public string Description { get; }

        /// <summary>Where it happens.</summary>
        public string Berth { get; }

        /// <summary>
        ///     Who carries it out.
        /// </summary>
        /// <remarks>
        ///     Several, and on the action rather than beside it, which is what lets a week's work be asked of a
        ///     welding crew.
        /// </remarks>
        [ProposedAndImplementedAction.Performers]
        public IReadOnlyList<string> Performers { get; }

    }

    /// <summary>The task as quoted.</summary>
    /// <remarks>
    ///     May answer to nothing: a task quoted and then dropped stays on the record, which is the reason for
    ///     the split.
    /// </remarks>
    [ProposedAndImplementedAction.ProposedAction(ImplementedAction = typeof(WorkDone))]
    public sealed class QuotedTask : YardTask {

        public QuotedTask(string description, string berth, IReadOnlyList<string> performers, decimal quotedHours)
            : base(description, berth, performers) {
            QuotedHours = quotedHours;
        }

        public decimal QuotedHours { get; }

        /// <summary>Null while nothing has been done, and after the task is dropped.</summary>
        public WorkDone? Answered { get; internal set; }

    }

    /// <summary>The task as worked.</summary>
    /// <remarks>
    ///     May answer to nothing either: work nobody quoted is as real as work quoted and done.
    /// </remarks>
    [ProposedAndImplementedAction.ImplementedAction(ProposedAction = typeof(QuotedTask))]
    public sealed class WorkDone : YardTask {

        public WorkDone(string description, string berth, IReadOnlyList<string> performers, decimal hours, QuotedTask? answers)
            : base(description, berth, performers) {
            Hours   = hours;
            Answers = answers;
            if (answers is not null) { answers.Answered = this; }
        }

        public decimal Hours { get; }

        /// <summary>Null for work found once the hull was open.</summary>
        public QuotedTask? Answers { get; }

    }

}
