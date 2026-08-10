#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.DecomposeByBusinessCapabilitySample {

    // The grid operator's first cut at services followed its database: a customer service, a reading
    // service, an invoice service. Every new tariff rule then touched all three, because pricing a supply
    // is not a table — it is something the business does.
    //
    // DECOMPOSE BY BUSINESS CAPABILITY draws the line around what the business does to generate value.
    // Metering is a capability; billing is a capability; connecting a new supply point is a capability. The
    // test is not aesthetic: a requirement that touches two of them should not touch one service.

    /// <summary>
    ///     Metering: everything the business does about knowing how much was used.
    /// </summary>
    /// <remarks>
    ///     Reading collection, validation and estimation live here because they change together, and they
    ///     change when the metering rules change and at no other time.
    /// </remarks>
    [DecomposeByBusinessCapability]
    public interface IMeteringCapability {

        void SubmitReading(string supplyPoint, decimal kilowattHours);

        decimal ConsumptionSince(string supplyPoint, DateTime from);

    }

    /// <summary>
    ///     Billing: everything the business does about being paid.
    /// </summary>
    /// <remarks>
    ///     It asks metering how much was used and never reads a meter. The day somebody adds an estimation
    ///     rule here, this annotation is what they are contradicting.
    /// </remarks>
    [DecomposeByBusinessCapability]
    public interface IBillingCapability {

        decimal AmountDue(string supplyPoint, DateTime from);

    }
}
