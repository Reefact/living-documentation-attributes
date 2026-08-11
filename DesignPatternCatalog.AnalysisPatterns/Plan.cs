#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     Plan (Analysis Patterns) — Makes a plan an action built from references to other actions, so that a plan can
    ///     be nested, replaced and depended upon without a structure of its own.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Analysis Patterns</i>, 1997.
    ///     </para>
    /// </remarks>
    public static class Plan {

        /// <summary>
        ///     Role played by a type or a member in the Plan design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     A composite action: it holds references to the proposed actions it is made of, and it is itself an
        ///     action, so a plan is a step of a larger plan without anything special being said.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PlanAttribute : Role { }

        /// <summary>
        ///     One proposed action's place in one plan. It exists because the same action can appear in two plans and
        ///     must not be confused between them; the pair of plan and action is unique.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ActionReferenceAttribute : Role {

            /// <summary>
            ///     The <see cref="PlanAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Plan { get; init; }

        }

        /// <summary>
        ///     That one reference must follow another. Both ends belong to the same plan, and the graph they form is
        ///     acyclic — a plan whose steps depend on each other in a circle cannot be scheduled, and that is a fact
        ///     about the model rather than about a scheduler.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PlanDependencyAttribute : Role {

            /// <summary>
            ///     The <see cref="ActionReferenceAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ActionReference { get; init; }

        }

        /// <summary>
        ///     The plan this one supersedes. Keeping the replaced plan rather than editing it is what lets a change of
        ///     plan be seen as such, and what a variance against the original is measured from.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ReplacesAttribute : Role { }

        /// <summary>
        ///     The references this plan is made of. Reached through references rather than directly, which is what
        ///     keeps one proposed action shared between plans instead of copied into each.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ComponentsAttribute : Role { }

    }

}
