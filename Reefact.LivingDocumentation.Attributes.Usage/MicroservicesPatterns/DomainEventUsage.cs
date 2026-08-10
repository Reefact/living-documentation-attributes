#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.DomainEventSample {

    // Metering validates a reading, and four services care: billing raises the invoice, the tariff engine
    // re-evaluates the band, analytics counts it, and the customer app pushes a notification. Metering
    // knows about none of them, and must not: the day the fifth appears, metering is not deployed.
    //
    // DOMAIN EVENT is Evans's, and this work asks it a question Evans did not — how does a service publish
    // an event when it updates its data? The name is the same, the shape is the same, and what is added is
    // that somebody outside the process is listening. That is why it is held here as well.

    /// <summary>
    ///     A reading has been accepted as good.
    /// </summary>
    /// <remarks>
    ///     Named for what happened to the meter, not for what billing intends to do about it. The moment it
    ///     is called <c>RaiseInvoiceRequested</c>, metering has acquired a consumer it cannot see.
    /// </remarks>
    [DomainEvent]
    public sealed class MeterReadingValidated {

        public MeterReadingValidated(string supplyPoint, DateTime takenOn, decimal kilowattHours) {
            SupplyPoint   = supplyPoint;
            TakenOn       = takenOn;
            KilowattHours = kilowattHours;
        }

        public string SupplyPoint { get; }

        public DateTime TakenOn { get; }

        public decimal KilowattHours { get; }

    }

    /// <summary>
    ///     A reading has been rejected, with the check it failed.
    /// </summary>
    /// <remarks>
    ///     Past tense and immutable, like the other. It carries the reason because a consumer that has to
    ///     ask metering why is a consumer coupled to metering at request time.
    /// </remarks>
    [DomainEvent]
    public sealed class MeterReadingRejected {

        public MeterReadingRejected(string supplyPoint, DateTime takenOn, string failedCheck) {
            SupplyPoint = supplyPoint;
            TakenOn     = takenOn;
            FailedCheck = failedCheck;
        }

        public string SupplyPoint { get; }

        public DateTime TakenOn { get; }

        public string FailedCheck { get; }

    }
}
