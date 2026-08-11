#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AccountingPatterns {

    /// <summary>
    ///     ReplacementAdjustment (Accounting Patterns) — Corrects a booked entry by removing it and processing the
    ///     corrected event afresh, so that the account shows only what is now believed.
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
    public static class ReplacementAdjustment {

        /// <summary>
        ///     Role played by a type or a member in the ReplacementAdjustment design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The correction as a whole: find the entries of the old event, get rid of them, and process the new event
        ///     the ordinary way. Getting rid of them may be a deletion or an unlinking that keeps them for logging, and
        ///     either way this is the strategy to choose when the audit trail of the old entries is not wanted.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ReplacementAdjustmentAttribute : Role {

            /// <summary>
            ///     The <see cref="AdjustedEventAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AdjustedEvent { get; init; }

        }

        /// <summary>
        ///     The entry created by processing the corrected event, which stands where the old one stood. Because it is
        ///     made by the ordinary rules, nothing about the correction is special-cased.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ReplacingEntryAttribute : Role { }

        /// <summary>
        ///     The event being corrected, and the way back to the entries to remove. Without it the old entries cannot
        ///     be found, which is what makes this strategy depend on the link rather than merely record it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class AdjustedEventAttribute : Role { }

    }

}
