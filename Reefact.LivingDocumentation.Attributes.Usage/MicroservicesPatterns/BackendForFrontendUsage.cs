#region Usings declarations

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.BackendForFrontendSample {

    // One gateway served the customer web site, the meter reader's tablet and the price-comparison sites
    // the regulator obliges the operator to feed. Three teams, three release cadences, one class — and a
    // field the comparison sites needed held up a release the tablet team had nothing to do with.
    //
    // BACKEND FOR FRONTEND gives each client its own gateway. Every one of them is still an API gateway, so
    // a rule written for the broader pattern reaches all three without naming them — the annotation derives
    // from it. What it costs is that the fan-out the three have in common is now written three times.

    /// <summary>
    ///     For the customer web site.
    /// </summary>
    /// <remarks>
    ///     Answers with what a browser renders in one screen, in one round trip, and changes when the site
    ///     changes rather than when the regulator does.
    /// </remarks>
    [BackendForFrontend]
    public interface ICustomerSiteApi {

        string AccountPage(string supplyPoint);

    }

    /// <summary>
    ///     For the meter reader's tablet.
    /// </summary>
    /// <remarks>
    ///     A different shape entirely: a round's worth of supply points, small enough to survive a rural
    ///     connection, and no billing at all.
    /// </remarks>
    [BackendForFrontend]
    public interface IFieldTabletApi {

        string Round(string readerId);

    }

    /// <summary>
    ///     For the price-comparison sites.
    /// </summary>
    /// <remarks>
    ///     The one whose shape is not the operator's to choose — the regulator specifies it — which is the
    ///     clearest argument in the group for not serving all three from one class.
    /// </remarks>
    [BackendForFrontend]
    public interface IComparisonSiteApi {

        string PublishedTariffs();

    }
}
