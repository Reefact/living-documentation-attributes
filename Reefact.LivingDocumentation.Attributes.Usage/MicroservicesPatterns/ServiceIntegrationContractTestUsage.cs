#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.ServiceIntegrationContractTestSample {

    // Metering renamed a field on its consumption API and found out from billing, in production, on the
    // night of the monthly run. Launching both services in one end-to-end suite would have caught it and
    // would have taken eleven minutes a commit, so nobody was going to keep it.
    //
    // SERVICE INTEGRATION CONTRACT TEST puts billing's expectations in metering's build. What is unusual
    // about it is who owns it: the suite lives here and belongs to the team next door. That is the whole
    // pattern, and it is the thing a test class cannot show.

    /// <summary>
    ///     What metering publishes.
    /// </summary>
    [ServiceIntegrationContractTest.Provider]
    public interface IMeteringApi {

        decimal ConsumptionSince(string supplyPoint, DateTime from);

    }

    /// <summary>
    ///     The team whose expectations are written down.
    /// </summary>
    /// <remarks>
    ///     The work's open question sits on this role: nothing checks that what billing wrote down is what
    ///     billing actually needs. A suite can be green and still describe a January that has moved on.
    /// </remarks>
    [ServiceIntegrationContractTest.Consumer]
    public interface IBillingService {

        decimal AmountDue(string supplyPoint, DateTime from);

    }

    /// <summary>
    ///     Billing's expectations of metering, run by metering.
    /// </summary>
    /// <remarks>
    ///     It fails for a team that did not write it, which is the point and the difficulty. Annotating it
    ///     is what lets that team find out whose expectation they just broke without reading the git log.
    /// </remarks>
    [ServiceIntegrationContractTest.ServiceIntegrationContractTest(Provider = typeof(IMeteringApi), Consumer = typeof(IBillingService))]
    public sealed class BillingExpectationsOfMetering {

        public bool ConsumptionIsReturnedInKilowattHours(IMeteringApi metering) =>
            metering.ConsumptionSince("GB0001", new DateTime(2026, 1, 1)) >= 0m;

    }
}
