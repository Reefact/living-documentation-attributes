#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     ProposedAndImplementedAction (Analysis Patterns) — Keeps what was planned and what happened as two linked
    ///     objects rather than one object with a flag, so that a plan can be compared with its outcome instead of being
    ///     overwritten by it.
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
    public static class ProposedAndImplementedAction {

        /// <summary>
        ///     Role played by a type or a member in the ProposedAndImplementedAction design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Something done or to be done, carrying when it is referred to, where it happens and who performs it. It
        ///     is one type for both sides of the story, which is what lets a report range over intentions and outcomes
        ///     together.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ActionAttribute : Role { }

        /// <summary>
        ///     The action as intended. It may point at the implemented action that answered it, and may point at none —
        ///     an intention that came to nothing is still on the record, which is the whole reason for the split.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProposedActionAttribute : Role {

            /// <summary>
            ///     The <see cref="ImplementedActionAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ImplementedAction { get; init; }

        }

        /// <summary>
        ///     The action as it happened. It may point at the proposal it answers, and may point at none — work done
        ///     that nobody planned is as real as work planned and done.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ImplementedActionAttribute : Role {

            /// <summary>
            ///     The <see cref="ProposedActionAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ProposedAction { get; init; }

        }

        /// <summary>
        ///     Where the action stands: proposed, started, completed, suspended, abandoned. A type object rather than
        ///     an enumeration, and derived rather than set, so the status cannot disagree with the facts it is drawn
        ///     from.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ActionStatusAttribute : Role { }

        /// <summary>
        ///     The parties who carry the action out. Several, because work is done by people and organisations
        ///     together, and naming them on the action is what lets a workload be asked of a party.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class PerformersAttribute : Role { }

    }

}
