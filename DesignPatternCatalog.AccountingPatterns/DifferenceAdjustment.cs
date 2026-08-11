#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AccountingPatterns {

    /// <summary>
    ///     DifferenceAdjustment (Accounting Patterns) — Corrects a booked entry with a single entry carrying the
    ///     difference, so that a correction costs one entry rather than two.
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
    public static class DifferenceAdjustment {

        /// <summary>
        ///     Role played by a type or a member in the DifferenceAdjustment design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The correction as a whole: one entry per original, holding what the original was short or over by. It is
        ///     the cheaper strategy in entries and the harder one to read, because no entry on the account states what
        ///     the figure should have been — only the gap.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class DifferenceAdjustmentAttribute : Role {

            /// <summary>
            ///     The <see cref="AdjustedEventAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AdjustedEvent { get; init; }

        }

        /// <summary>
        ///     The single entry posted, whose amount is the difference between what was booked and what should have
        ///     been. Its sign carries the direction of the error.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AdjustingEntryAttribute : Role { }

        /// <summary>
        ///     The event being corrected. It matters more here than under a reversal, because the difference alone does
        ///     not say what it was a difference from.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class AdjustedEventAttribute : Role { }

    }

}
