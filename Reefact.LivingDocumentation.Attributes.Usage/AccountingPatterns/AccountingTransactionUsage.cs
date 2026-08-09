#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AccountingPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AccountingPatterns.AccountingTransactionSample {

    // A customer is charged for energy: they owe us, and we recognise revenue. Booked as one entry, the books
    // do not balance and nobody notices until a reconciliation months later.
    //
    // ACCOUNTING TRANSACTION makes conservation an invariant. Nothing is created, only moved, and the model
    // refuses a movement that does not add to zero.

    /// <summary>
    ///     One balanced movement.
    /// </summary>
    /// <remarks>
    ///     Double entry stated as something the model can be held to, rather than a discipline the accountant
    ///     remembers.
    /// </remarks>
    [AccountingTransaction.AccountingTransaction]
    public abstract class Movement {

        protected Movement(IReadOnlyList<decimal> entries) {
            decimal sum = 0m;
            foreach (decimal e in entries) { sum += e; }
            if (sum != 0m) { throw new ArgumentException("the entries of a movement sum to zero", nameof(entries)); }
            Entries = entries;
        }

        /// <summary>
        ///     The entries the invariant ranges over.
        /// </summary>
        /// <remarks>
        ///     No entry of a movement stands alone.
        /// </remarks>
        [AccountingTransaction.Entries]
        public IReadOnlyList<decimal> Entries { get; }

    }

    /// <summary>
    ///     Exactly two entries, of opposite sign.
    /// </summary>
    /// <remarks>
    ///     The literal single movement from one account to another, and a stronger constraint than the general
    ///     one.
    /// </remarks>
    [AccountingTransaction.TwoLegged]
    public sealed class Charge : Movement {

        public Charge(decimal amount) : base(new[] { amount, -amount }) { }

    }

    /// <summary>
    ///     Any number of entries that still sum to zero.
    /// </summary>
    /// <remarks>
    ///     A charge, the tax on it and what is owed, in one balanced whole.
    /// </remarks>
    [AccountingTransaction.MultiLegged]
    public sealed class ChargeWithTax : Movement {

        public ChargeWithTax(decimal net, decimal tax)
            : base(new[] { net, tax, -(net + tax) }) { }

    }

}
