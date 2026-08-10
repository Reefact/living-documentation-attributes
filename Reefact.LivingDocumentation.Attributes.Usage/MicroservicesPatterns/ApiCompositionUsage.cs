#region Usings declarations

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.ApiCompositionSample {

    // A field engineer opening a supply point on a tablet wants one screen: the account, the meter, and
    // whether the street is currently out. Three services own those three answers, and no database can join
    // them because no database holds two of them.
    //
    // API COMPOSITION does the join here, in memory, on the read path. It is the cheap answer and it stays
    // cheap only while the result sets stay small — which is a property of the data rather than of this
    // code, and therefore not something the code will warn about.

    /// <summary>
    ///     Billing, for the account.
    /// </summary>
    [ApiComposition.Provider]
    public interface IAccountLookup {

        string? AccountOf(string supplyPoint);

    }

    /// <summary>
    ///     Metering, for the meter.
    /// </summary>
    [ApiComposition.Provider]
    public interface IMeterLookup {

        string? SerialOf(string supplyPoint);

    }

    /// <summary>
    ///     Outages, for the street.
    /// </summary>
    [ApiComposition.Provider]
    public interface IOutageLookup {

        bool IsAffected(string supplyPoint);

    }

    /// <summary>
    ///     What the tablet is given.
    /// </summary>
    public sealed class SupplyPointSummary {

        public SupplyPointSummary(string? account, string? meterSerial, bool affectedByOutage) {
            Account          = account;
            MeterSerial      = meterSerial;
            AffectedByOutage = affectedByOutage;
        }

        public string? Account { get; }

        public string? MeterSerial { get; }

        public bool AffectedByOutage { get; }

    }

    /// <summary>
    ///     Calls the three and assembles the answer.
    /// </summary>
    /// <remarks>
    ///     Its latency is the slowest of the three, and its availability is all three at once. Naming it is
    ///     what lets somebody ask, later, which screens are built this way and which are built on a view.
    /// </remarks>
    [ApiComposition.Composer]
    public sealed class SupplyPointSummaryComposer {

        private readonly IAccountLookup _accounts;
        private readonly IMeterLookup   _meters;
        private readonly IOutageLookup  _outages;

        public SupplyPointSummaryComposer(IAccountLookup accounts, IMeterLookup meters, IOutageLookup outages) {
            _accounts = accounts;
            _meters   = meters;
            _outages  = outages;
        }

        public SupplyPointSummary Compose(string supplyPoint) =>
            new SupplyPointSummary(_accounts.AccountOf(supplyPoint),
                                   _meters.SerialOf(supplyPoint),
                                   _outages.IsAffected(supplyPoint));

    }
}
