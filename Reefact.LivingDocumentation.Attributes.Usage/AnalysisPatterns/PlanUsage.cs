#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.PlanSample {

    // The refit plan: strip the hull before blasting, blast before coating, and do not float the ship until the
    // shaft is aligned. Three weeks in, the owner defers the coating to the next docking, and the plan is
    // reissued — with the original kept, because the yard is paid against what was agreed.
    //
    // PLAN makes the plan an action built from references to other actions, so it nests, it can be replaced,
    // and the same task can sit in two plans without being copied into either.

    /// <summary>One task's place in one plan.</summary>
    /// <remarks>
    ///     The reference exists because the same task can appear in two plans and must not be confused between
    ///     them; the pair of plan and task is unique.
    /// </remarks>
    [Plan.ActionReference(Plan = typeof(RefitPlan))]
    public sealed class PlannedTask {

        public PlannedTask(RefitPlan plan, string task) {
            Plan = plan;
            Task = task;
        }

        public RefitPlan Plan { get; }

        public string Task { get; }

    }

    /// <summary>That one step must follow another.</summary>
    /// <remarks>
    ///     Both ends belong to the same plan, and the graph is acyclic — steps depending on each other in a
    ///     circle cannot be scheduled, and that is a fact about the model rather than about a scheduler.
    /// </remarks>
    [Plan.PlanDependency(ActionReference = typeof(PlannedTask))]
    public sealed class MustFollow {

        public MustFollow(PlannedTask dependent, PlannedTask consequent) {
            if (dependent.Plan != consequent.Plan) {
                throw new ArgumentException("a dependency stays within one plan", nameof(consequent));
            }
            Dependent  = dependent;
            Consequent = consequent;
        }

        public PlannedTask Dependent { get; }

        public PlannedTask Consequent { get; }

    }

    /// <summary>A composite action: the refit as a whole.</summary>
    /// <remarks>
    ///     Itself an action, so a plan is a step of a larger plan without anything special being said — a
    ///     docking is a step of the vessel's five-year survey cycle.
    /// </remarks>
    [Plan.Plan]
    public sealed class RefitPlan {

        private readonly List<PlannedTask> _steps = new List<PlannedTask>();

        public RefitPlan(string vessel, RefitPlan? replaces) {
            Vessel   = vessel;
            Replaces = replaces;
        }

        public string Vessel { get; }

        /// <summary>
        ///     The plan this one supersedes.
        /// </summary>
        /// <remarks>
        ///     Kept rather than edited, which is what lets a change of plan be seen as such and a variance be
        ///     measured against what was agreed.
        /// </remarks>
        [Plan.Replaces]
        public RefitPlan? Replaces { get; }

        /// <summary>
        ///     What the plan is made of.
        /// </summary>
        /// <remarks>
        ///     Reached through references, which is what keeps one task shared between plans instead of copied
        ///     into each.
        /// </remarks>
        [Plan.Components]
        public IReadOnlyList<PlannedTask> Components => _steps;

        public PlannedTask Add(string task) {
            PlannedTask step = new(this, task);
            _steps.Add(step);

            return step;
        }

    }

}
