#region Usings declarations

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.ApiGatewaySample {

    // A customer opening the grid operator's account page wants their balance, their last reading and
    // whether their street is out. Three services own those, none of them is on the public internet, and
    // one of them still speaks a protocol nobody outside the company should meet.
    //
    // API GATEWAY is the one door. Clients stop knowing how the company is partitioned, which is what makes
    // the partitioning free to change — and the gateway acquires a reason to know about every service there
    // is, which is how it turns back into the monolith it replaced.

    /// <summary>
    ///     The single entry point for everything outside.
    /// </summary>
    /// <remarks>
    ///     Annotated on the interface, which is what a client meets. The list of services below is the thing
    ///     to watch: every one added here is a service whose existence the gateway now depends on.
    /// </remarks>
    [ApiGateway]
    public interface ICustomerApiGateway {

        string AccountSummary(string supplyPoint);

    }

    /// <summary>
    ///     Routes some requests and fans out for others.
    /// </summary>
    public sealed class CustomerApiGateway : ICustomerApiGateway {

        private readonly IBillingApi  _billing;
        private readonly IMeteringApi _metering;

        public CustomerApiGateway(IBillingApi billing, IMeteringApi metering) {
            _billing  = billing;
            _metering = metering;
        }

        public string AccountSummary(string supplyPoint) =>
            $"{_billing.BalanceOf(supplyPoint)} / {_metering.LastReading(supplyPoint)}";

    }

    /// <summary>What billing exposes inside the company.</summary>
    public interface IBillingApi {

        decimal BalanceOf(string supplyPoint);

    }

    /// <summary>What metering exposes inside the company.</summary>
    public interface IMeteringApi {

        decimal LastReading(string supplyPoint);

    }
}
