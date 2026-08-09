#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AccountingPatterns {

    /// <summary>
    ///     ReversalAdjustment (Accounting Patterns) — Corrects a booked entry by reversing it and posting what it
    ///     should have been, so that both the mistake and the correction stay on the record.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Accounting Patterns</i>, 2000.
    ///     </para>
    /// </remarks>
    public static class ReversalAdjustment {

        /// <summary>
        ///     Role played by a type or a member in the ReversalAdjustment design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The correction as a whole: for every entry to be adjusted, two new entries. Nothing is edited and
        ///     nothing is removed, which is what keeps the account auditable at the price of more entries than either
        ///     other strategy.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ReversalAdjustmentAttribute : Role {

            /// <summary>
            ///     The <see cref="AdjustedEventAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AdjustedEvent { get; init; }

        }

        /// <summary>
        ///     An entry cancelling the original: the same occurred date, the same amount, the opposite sign. Using the
        ///     original's date rather than today's is what keeps a past period's total unchanged.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ReversingEntryAttribute : Role { }

        /// <summary>
        ///     The entry the original should have been, calculated the way any entry of its kind is calculated. It is
        ///     not a special case, which is the point of reversing first.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ReplacementEntryAttribute : Role { }

        /// <summary>
        ///     The event being corrected, named by the event that corrects it. The link is what lets the correction be
        ///     explained rather than merely observed.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class AdjustedEventAttribute : Role { }

    }

}
