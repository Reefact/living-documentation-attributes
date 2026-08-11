#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AccountingPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AccountingPatterns.EventSample {

    // An electricity retailer. A meter reading arrives, and three weeks later the reading is found to have been
    // misread. The tempting fix is to edit the reading, and it is the one fix that must not happen: the bill
    // already went out against the old figure, and a reading that changes silently makes every total
    // unexplainable.
    //
    // EVENT makes what happened immutable, and dates it twice — when it happened, and when we heard. A
    // correction is then a new event, never an edit.

    /// <summary>
    ///     What kind of thing happened.
    /// </summary>
    /// <remarks>
    ///     Configured rather than declared, so a posting rule attaches to a kind and not to a class.
    /// </remarks>
    [Event.EventType]
    public sealed class UsageEventType {

        public UsageEventType(string name) { Name = name; }

        /// <summary>Meter read, service connected, tariff changed.</summary>
        public string Name { get; }

    }

    /// <summary>
    ///     Something that happened which the business reacts to.
    /// </summary>
    /// <remarks>
    ///     No setters, on purpose. A reading found to be wrong is corrected by a further event and an
    ///     adjustment, because editing this one would break the log every bill was drawn from.
    /// </remarks>
    [Event.Event]
    public sealed class UsageEvent {

        public UsageEvent(UsageEventType type, string meter, decimal kilowattHours, DateTime occurred, DateTime noticed) {
            if (noticed < occurred) {
                throw new ArgumentException("an event cannot be noticed before it occurred", nameof(noticed));
            }
            Type           = type;
            Meter          = meter;
            KilowattHours  = kilowattHours;
            WhenOccurred   = occurred;
            WhenNoticed    = noticed;
        }

        public UsageEventType Type { get; }

        public string Meter { get; }

        public decimal KilowattHours { get; }

        /// <summary>
        ///     When the usage happened.
        /// </summary>
        /// <remarks>
        ///     With the noticed date this is Analysis Patterns' dual time record, which the 1997 book names; what
        ///     an event asserts is only that it carries both.
        /// </remarks>
        [Event.WhenOccurred]
        public DateTime WhenOccurred { get; }

        /// <summary>
        ///     When we learned of it.
        /// </summary>
        /// <remarks>
        ///     A reading taken on the first and keyed on the ninth is one event with two dates, which is what lets
        ///     a restatement avoid pretending it was known earlier.
        /// </remarks>
        [Event.WhenNoticed]
        public DateTime WhenNoticed { get; }

    }

}
