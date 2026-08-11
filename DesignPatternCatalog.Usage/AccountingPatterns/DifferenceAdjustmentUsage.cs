#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AccountingPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AccountingPatterns.DifferenceAdjustmentSample {

    // A retailer correcting a few thousand readings a month by reversal doubles its entry volume. Somebody will
    // ask whether that is necessary.
    //
    // DIFFERENCE ADJUSTMENT posts one entry holding the gap. Cheaper in entries, and harder to read: no line on
    // the account says what the figure should have been, only what it was out by.

    /// <summary>
    ///     The single entry posted.
    /// </summary>
    /// <remarks>
    ///     Its sign carries the direction of the error, which is the only place that direction is recorded.
    /// </remarks>
    [DifferenceAdjustment.AdjustingEntry]
    public sealed class Difference {

        public Difference(decimal booked, decimal shouldHaveBeen, DateTime occurred) {
            Amount   = shouldHaveBeen - booked;
            Occurred = occurred;
        }

        public decimal Amount { get; }

        public DateTime Occurred { get; }

    }

    /// <summary>
    ///     The correction as a whole.
    /// </summary>
    [DifferenceAdjustment.DifferenceAdjustment(AdjustedEvent = typeof(Difference))]
    public sealed class ReadingCorrection {

        public ReadingCorrection(string adjustedEvent, Difference difference) {
            AdjustedEvent = adjustedEvent;
            Difference    = difference;
        }

        /// <summary>
        ///     The event being corrected.
        /// </summary>
        /// <remarks>
        ///     It matters more here than under a reversal: the difference alone does not say what it was a
        ///     difference from.
        /// </remarks>
        [DifferenceAdjustment.AdjustedEvent]
        public string AdjustedEvent { get; }

        public Difference Difference { get; }

    }

}
