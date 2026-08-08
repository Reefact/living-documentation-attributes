#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     Accountability (Analysis Patterns) — Reifies a responsibility of one party towards another as an object
    ///     carrying its own type, so that the kinds of responsibility a system recognises become data rather than
    ///     structure.
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
    public static class Accountability {

        /// <summary>
        ///     Role played by a type or a member in the Accountability design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     What kind of responsibility this is, and which parties may stand at each end of it. It is the reason the
        ///     pattern is worth its indirection: a new kind of responsibility is a row, not a class, and the constraint
        ///     on who may be made responsible is stated once here instead of in every screen that creates one.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AccountabilityTypeAttribute : Role { }

        /// <summary>
        ///     One responsibility, held by one party towards another, for a period. Reifying it is what lets it be
        ///     found, dated and ended: a responsibility modelled as a reference from one party to another cannot record
        ///     when it started, and cannot be two things at once.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AccountabilityAttribute : Role {

            /// <summary>
            ///     The <see cref="AccountabilityTypeAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AccountabilityType { get; init; }

        }

        /// <summary>
        ///     The party the responsibility is owed to. Naming which end is which is the assertion worth making: the
        ///     two ends are both parties, so nothing in the type system tells them apart, and a model that swaps them
        ///     is wrong in a way that compiles and passes its tests.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class CommissionerAttribute : Role { }

        /// <summary>
        ///     The party that holds the responsibility. It is a party and not a person on purpose — the point of the
        ///     pattern is that a department can be answerable for something, and that whoever currently staffs it is a
        ///     separate question.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ResponsibleAttribute : Role { }

    }

}
