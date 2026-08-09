#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     StatusType (Analysis Patterns) — Makes planned and actual a type object an observation names, so that a
    ///     plan's figures and the world's figures are the same kind of thing and can be compared without a special
    ///     case.
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
    public static class StatusType {

        /// <summary>
        ///     Role played by a type or a member in the StatusType design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The status as a type object, so that which statuses exist is configured rather than written as classes.
        ///     The alternative — a subclass per status, or a boolean — makes comparing a plan against an outcome a
        ///     special case instead of an ordinary observation.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class StatusTypeAttribute : Role { }

        /// <summary>
        ///     A status asserting the observation is of the world as it was. It may carry a time offset, so that a
        ///     figure stated a month after the period it covers is distinguished from the same figure restated later.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ActualAttribute : Role { }

        /// <summary>
        ///     A status asserting the observation belongs to a plan, and naming which. Two plans for one period are
        ///     therefore two sets of observations rather than two columns on one.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PlannedAttribute : Role {

            /// <summary>
            ///     The <see cref="PlanAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Plan { get; init; }

        }

        /// <summary>
        ///     The plan a planned status refers to. Making it an object rather than a label is what lets one plan's
        ///     observations be gathered, and what lets a plan be superseded without rewriting the figures it held.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PlanAttribute : Role { }

        /// <summary>
        ///     A status type whose observations compare a datum against a comparator — a variance. It is a status
        ///     rather than a calculation because what it yields is an observation like any other, and so can itself be
        ///     planned or actual.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ComparativeStatusTypeAttribute : Role { }

        /// <summary>
        ///     The status type an observation names. One reference, on the observation rather than on its subtype, is
        ///     what makes planned and actual interchangeable to everything that reads observations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class StatusAttribute : Role { }

    }

}
