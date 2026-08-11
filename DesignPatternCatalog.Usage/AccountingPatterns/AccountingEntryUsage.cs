#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AccountingPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AccountingPatterns.AccountingEntrySample {

    // The same retailer. A month's usage produces a charge to the customer, the tax on it, and the cost of the
    // electricity bought in from the generator — three facts about money that are not three payments. Two of
    // them will never be paid by anybody; they are classifications.
    //
    // ACCOUNTING ENTRY is one amount and what it is for. Nothing about it says a payment happened.

    /// <summary>
    ///     One record of one amount.
    /// </summary>
    /// <remarks>
    ///     The amount is not necessarily money: the kilowatt hours bought in are accounted for by the same
    ///     patterns, which is why the quantity carries its unit.
    /// </remarks>
    [AccountingEntry.AccountingEntry]
    public sealed class Entry {

        public Entry(decimal amount, string unit, string descriptor, DateTime whenBooked) {
            Amount     = amount;
            Unit       = unit;
            Descriptor = descriptor;
            WhenBooked = whenBooked;
        }

        /// <summary>
        ///     What is being accounted for.
        /// </summary>
        /// <remarks>
        ///     Signed. A negative amount is money out, and the sign is what a transaction's balance ranges over.
        /// </remarks>
        [AccountingEntry.Amount]
        public decimal Amount { get; }

        public string Unit { get; }

        /// <summary>
        ///     What kind of entry this is.
        /// </summary>
        /// <remarks>
        ///     "energy charge", "GST", "network cost". It is what an account gathers entries by.
        /// </remarks>
        [AccountingEntry.Descriptor]
        public string Descriptor { get; }

        /// <summary>
        ///     When the entry was made.
        /// </summary>
        /// <remarks>
        ///     Distinct from when the usage occurred, and it is this date a closed period is closed against.
        /// </remarks>
        [AccountingEntry.WhenBooked]
        public DateTime WhenBooked { get; }

    }

}
