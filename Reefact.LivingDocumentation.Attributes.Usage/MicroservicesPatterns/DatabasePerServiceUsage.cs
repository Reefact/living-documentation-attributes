#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.DatabasePerServiceSample {

    // A regional grid operator split metering out of the old customer system. Metering owns every reading
    // ever taken from a supply point, and billing owns the invoices computed from them. Before the split,
    // the billing run read the readings table directly, and the day metering wanted to add a validation
    // status column it discovered it could not.
    //
    // DATABASE PER SERVICE says the readings are metering's and nobody else's. The rule is not enforceable
    // in C# — a connection string reaches any schema it has a grant for — so the annotation is where the
    // boundary is written down, and the grant is where it is defended.

    /// <summary>
    ///     The store of readings, private to metering.
    /// </summary>
    /// <remarks>
    ///     Named as the boundary rather than as a table so that the annotation has somewhere to sit. A second
    ///     service reaching this schema does not fail to compile; it fails review, and this is what it fails
    ///     against.
    /// </remarks>
    [DatabasePerService.PrivateDatabase(Service = typeof(IMeteringService))]
    public interface IReadingStore {

        void Append(string supplyPoint, DateTime takenOn, decimal kilowattHours);

        IReadOnlyList<decimal> Since(string supplyPoint, DateTime from);

    }

    /// <summary>
    ///     Everything the rest of the company may ask metering.
    /// </summary>
    /// <remarks>
    ///     The whole point of the pattern is that this interface is the only way in. Adding a column to the
    ///     readings is now metering's decision alone; adding a method here is a conversation.
    /// </remarks>
    [DatabasePerService.Service]
    public interface IMeteringService {

        decimal ConsumptionSince(string supplyPoint, DateTime from);

    }

    /// <summary>
    ///     Metering, holding its own store.
    /// </summary>
    public sealed class MeteringService : IMeteringService {

        private readonly IReadingStore _readings;

        public MeteringService(IReadingStore readings) {
            _readings = readings;
        }

        public decimal ConsumptionSince(string supplyPoint, DateTime from) {
            decimal total = 0m;
            foreach (decimal reading in _readings.Since(supplyPoint, from)) { total += reading; }

            return total;
        }

    }

    /// <summary>
    ///     Billing, which asks rather than reads.
    /// </summary>
    /// <remarks>
    ///     It holds no reference to <see cref="IReadingStore" />, and that absence is the pattern. It is also
    ///     invisible: nothing in this class says it used to hold one.
    /// </remarks>
    public sealed class BillingRun {

        private readonly IMeteringService _metering;

        public BillingRun(IMeteringService metering) {
            _metering = metering;
        }

        public decimal AmountDue(string supplyPoint, DateTime from, decimal unitPrice) =>
            _metering.ConsumptionSince(supplyPoint, from) * unitPrice;

    }
}
