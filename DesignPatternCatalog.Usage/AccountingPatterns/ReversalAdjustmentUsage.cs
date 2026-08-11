#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AccountingPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AccountingPatterns.ReversalAdjustmentSample {

    // The meter was misread: fifty kilowatt hours booked, twenty actually used. The bill went out, the period
    // closed, and an auditor will ask what happened.
    //
    // REVERSAL ADJUSTMENT posts two entries against one: a reversal carrying the original's date and the
    // opposite sign, then the entry that should have been made. Nothing is edited, nothing disappears, and the
    // account tells the whole story at the price of more lines.

    /// <summary>
    ///     Cancels the original.
    /// </summary>
    /// <remarks>
    ///     The original's occurred date, not today's, which is what leaves a closed period's total where it was.
    /// </remarks>
    [ReversalAdjustment.ReversingEntry]
    public sealed class Reversal {

        public Reversal(decimal originalAmount, DateTime originalDate) {
            Amount   = -originalAmount;
            Occurred = originalDate;
        }

        public decimal Amount { get; }

        public DateTime Occurred { get; }

    }

    /// <summary>
    ///     What should have been booked.
    /// </summary>
    /// <remarks>
    ///     Calculated the way any entry of its kind is, which is the point of reversing first: the correction is
    ///     not a special case.
    /// </remarks>
    [ReversalAdjustment.ReplacementEntry]
    public sealed class Restated {

        public Restated(decimal amount, DateTime occurred) {
            Amount   = amount;
            Occurred = occurred;
        }

        public decimal Amount { get; }

        public DateTime Occurred { get; }

    }

    /// <summary>
    ///     The correction as a whole.
    /// </summary>
    [ReversalAdjustment.ReversalAdjustment(AdjustedEvent = typeof(Restated))]
    public sealed class MeterRereadAdjustment {

        public MeterRereadAdjustment(string adjustedEvent, Reversal reversal, Restated restated) {
            AdjustedEvent = adjustedEvent;
            Reversal      = reversal;
            Restated      = restated;
        }

        /// <summary>
        ///     What is being corrected.
        /// </summary>
        /// <remarks>
        ///     The link is what lets the correction be explained rather than merely observed.
        /// </remarks>
        [ReversalAdjustment.AdjustedEvent]
        public string AdjustedEvent { get; }

        public Reversal Reversal { get; }

        public Restated Restated { get; }

    }

}
