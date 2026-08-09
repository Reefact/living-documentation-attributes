#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AccountingPatterns {

    /// <summary>
    ///     AccountingTransaction (Accounting Patterns) — Binds the entries of one movement so that they sum to zero, so
    ///     that money is conserved by the model rather than by the care of whoever wrote the last posting.
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
    public static class AccountingTransaction {

        /// <summary>
        ///     Role played by a type or a member in the AccountingTransaction design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The movement as a whole. Its entries must sum to zero — nothing is created, only moved — which is double
        ///     entry stated as an invariant the model can be held to instead of a discipline the accountant remembers.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AccountingTransactionAttribute : Role { }

        /// <summary>
        ///     The entries the invariant ranges over. They belong to the transaction, and no entry of a transaction
        ///     stands alone.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class EntriesAttribute : Role { }

        /// <summary>
        ///     A transaction of exactly two entries of opposite sign: one account down, one account up. It is the
        ///     literal single movement, and its constraint is stronger than the general one.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class TwoLeggedAttribute : Role { }

        /// <summary>
        ///     A transaction of any number of entries that still sum to zero, which is what a movement split across
        ///     several accounts needs — a charge, the tax on it, and what is owed, in one balanced whole.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class MultiLeggedAttribute : Role { }

    }

}
