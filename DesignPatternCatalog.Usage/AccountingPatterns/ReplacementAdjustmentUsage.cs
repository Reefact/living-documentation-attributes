#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AccountingPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AccountingPatterns.ReplacementAdjustmentSample {

    // The same misreading, in a system whose statements are drawn from live entries and whose auditors do not
    // care what the figure was yesterday. Every reversal on the account is then noise a customer has to be
    // talked through.
    //
    // REPLACEMENT ADJUSTMENT drops the old entries and reprocesses the corrected event, so the account shows
    // only what is now believed. It trades the audit trail away deliberately.

    /// <summary>
    ///     The entry that stands where the old one stood.
    /// </summary>
    /// <remarks>
    ///     Made by the ordinary rules, so nothing about the correction is special-cased.
    /// </remarks>
    [ReplacementAdjustment.ReplacingEntry]
    public sealed class Replacing {

        public Replacing(decimal amount, DateTime occurred) {
            Amount   = amount;
            Occurred = occurred;
        }

        public decimal Amount { get; }

        public DateTime Occurred { get; }

    }

    /// <summary>
    ///     The correction as a whole.
    /// </summary>
    [ReplacementAdjustment.ReplacementAdjustment(AdjustedEvent = typeof(Replacing))]
    public sealed class ReadingReplaced {

        private readonly List<Replacing> _entries = new List<Replacing>();

        public ReadingReplaced(string adjustedEvent) { AdjustedEvent = adjustedEvent; }

        /// <summary>
        ///     The event being corrected, and the way back to the entries to remove.
        /// </summary>
        /// <remarks>
        ///     Without it the old entries cannot be found, which is what makes this strategy depend on the link
        ///     rather than merely record it.
        /// </remarks>
        [ReplacementAdjustment.AdjustedEvent]
        public string AdjustedEvent { get; }

        public IReadOnlyList<Replacing> Entries => _entries;

        /// <summary>Drops what the old event produced, then posts what the new one produces.</summary>
        public void Replace(IEnumerable<Replacing> fresh) {
            _entries.Clear();
            _entries.AddRange(fresh);
        }

    }

}
