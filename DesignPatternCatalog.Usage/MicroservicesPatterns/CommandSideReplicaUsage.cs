#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.CommandSideReplicaSample {

    // Pricing owns the tariffs. Billing needs them to raise an invoice, and used to call pricing once per
    // supply point, several hundred thousand times a night — which made the billing run as available as
    // pricing was, and no faster.
    //
    // COMMAND-SIDE REPLICA moves a copy of the tariffs into billing and keeps it current from pricing's
    // events. The trade is stated rather than discovered: the runtime dependency is gone, and the tariff
    // billing decides on is as old as the last event that arrived.

    /// <summary>
    ///     Pricing, which owns the tariffs.
    /// </summary>
    /// <remarks>
    ///     It publishes on every change, for the benefit of a service it does not reference. Removing the
    ///     publication compiles perfectly and silently freezes billing's replica.
    /// </remarks>
    [CommandSideReplica.ProviderService]
    public interface IPricingService {

        decimal UnitPrice(string tariffCode);

    }

    /// <summary>
    ///     Billing's copy of the tariffs.
    /// </summary>
    /// <remarks>
    ///     One writer, which is the subscription. A repository method that writes here for any other reason
    ///     is the defect, and it is the annotation rather than the type that says so.
    /// </remarks>
    [CommandSideReplica.ReplicaDatabase(ProviderService = typeof(IPricingService))]
    public sealed class TariffReplica {

        private readonly Dictionary<string, decimal> _prices = new Dictionary<string, decimal>();

        public void Apply(string tariffCode, decimal unitPrice) => _prices[tariffCode] = unitPrice;

        public decimal UnitPrice(string tariffCode) => _prices[tariffCode];

    }

    /// <summary>
    ///     The billing run, reading the replica rather than calling pricing.
    /// </summary>
    /// <remarks>
    ///     It prices an invoice from data it did not compute and cannot refresh on demand. Somebody will one
    ///     day want the price "as of now" here; this is where they will find out that it is not available.
    /// </remarks>
    [CommandSideReplica.CommandService(ReplicaDatabase = typeof(TariffReplica))]
    public sealed class InvoiceRun {

        private readonly TariffReplica _tariffs;

        public InvoiceRun(TariffReplica tariffs) {
            _tariffs = tariffs;
        }

        public decimal Raise(string tariffCode, decimal kilowattHours) => _tariffs.UnitPrice(tariffCode) * kilowattHours;

    }
}
