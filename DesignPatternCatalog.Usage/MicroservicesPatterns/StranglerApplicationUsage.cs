#region Usings declarations

using System;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.StranglerApplicationSample {

    // The grid operator's customer information system is twenty-two years old, runs the whole company, and
    // cannot be rewritten in one go — nobody would fund the two years in which nothing shipped. So it is
    // being strangled: metering came out first, then billing, and the outage map was built new because the
    // regulator wanted it and the old system had nothing to move.
    //
    // STRANGLER APPLICATION is the shape of that. Its value is that it ships before the migration is over.
    // Its risk is that the migration is never over, and there is nothing in a codebase that reports which
    // of the two is happening.

    /// <summary>
    ///     The twenty-two-year-old customer information system.
    /// </summary>
    /// <remarks>
    ///     Reads identically to an application nobody intends to replace. This annotation is the whole of
    ///     the difference, and the reason a migration can be counted at all.
    /// </remarks>
    [StranglerApplication.Monolith]
    public interface ILegacyCustomerSystem {

        decimal BalanceOf(string supplyPoint);

        decimal LastReading(string supplyPoint);

    }

    /// <summary>
    ///     What is growing around it.
    /// </summary>
    [StranglerApplication.StranglerApplication]
    public interface IGridPlatform {

        object ServiceFor(string capability);

    }

    /// <summary>
    ///     Metering, taken out of the monolith.
    /// </summary>
    /// <remarks>
    ///     The obligation this role carries is the one that rots: <c>LastReading</c> still exists on the
    ///     legacy interface above, and until somebody deletes it the company has two answers to one
    ///     question. Counting extracted services is how that debt stops being invisible.
    /// </remarks>
    [StranglerApplication.ExtractedService(Monolith = typeof(ILegacyCustomerSystem))]
    public interface IMeteringService {

        decimal ConsumptionSince(string supplyPoint, DateTime from);

    }

    /// <summary>
    ///     The outage map, which the monolith never had.
    /// </summary>
    /// <remarks>
    ///     Singled out by the work because it shows a return before any extraction finishes — and, unlike
    ///     metering, it leaves nothing behind to remove.
    /// </remarks>
    [StranglerApplication.NewService(StranglerApplication = typeof(IGridPlatform))]
    public interface IOutageMapService {

        int SupplyPointsAffectedBy(string substation);

    }
}
