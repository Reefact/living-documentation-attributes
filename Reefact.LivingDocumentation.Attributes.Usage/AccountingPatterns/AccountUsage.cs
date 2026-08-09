#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AccountingPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AccountingPatterns.AccountSample {

    // "What does this customer owe?" is easy. "What did they owe on the thirtieth of June?" is what the auditor
    // asks, and a balance held as a field cannot answer it. Neither can it say why the figure moved.
    //
    // ACCOUNT keeps the entries and derives the balance, so the history is the record and the total is a
    // question asked of it.

    /// <summary>
    ///     The entries of one classification, and what they add up to.
    /// </summary>
    /// <remarks>
    ///     More than a container: it answers a balance as at any past moment, because it holds every discrete
    ///     change rather than a running figure.
    /// </remarks>
    [Account.Account]
    public sealed class ReceivableAccount {

        private readonly List<(decimal Amount, DateTime WhenBooked)> _entries = new List<(decimal, DateTime)>();

        public ReceivableAccount(string customer) { Customer = customer; }

        public string Customer { get; }

        /// <summary>
        ///     What has been booked here.
        /// </summary>
        /// <remarks>
        ///     An entry is put into an account when it is created, and the account decides which entries belong
        ///     together.
        /// </remarks>
        [Account.Entries]
        public IReadOnlyList<(decimal Amount, DateTime WhenBooked)> Entries => _entries;

        public void Book(decimal amount, DateTime whenBooked) => _entries.Add((amount, whenBooked));

        /// <summary>
        ///     What the entries come to, as at a date.
        /// </summary>
        /// <remarks>
        ///     Derived rather than stored beside the entries, so it cannot disagree with them.
        /// </remarks>
        [Account.Balance]
        public decimal BalanceAt(DateTime asAt) {
            decimal total = 0m;
            foreach ((decimal amount, DateTime booked) in _entries) {
                if (booked <= asAt) { total += amount; }
            }
            return total;
        }

    }

}
