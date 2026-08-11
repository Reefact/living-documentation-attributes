#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.RemoteFacadeSample {

    // A supermarket till talking to the back office over a shop's ADSL line.
    //
    // Closing a till at the end of a shift means: read the drawer count, reconcile against the day's
    // takings, register the variance, flag anything over £20 for the duty manager, and print the report.
    // Against the fine-grained model that is nine calls, each a round trip, on a link with 90ms latency and
    // an evening habit of dropping.
    //
    // A REMOTE FACADE exists because of the WIRE, and for no other reason. Its methods are shaped by round
    // trips: `CloseShift` below does in one call what a caller would otherwise have done in nine, and it
    // hands back everything the till needs in one DataTransferObject rather than making it ask again.
    //
    // The rule that keeps it honest is that it holds NO LOGIC. It decides nothing about variance thresholds
    // or manager approval; it calls the objects that do. The moment a rule appears here, it has moved out
    // of the model and onto the boundary — where it is invisible to every caller that does not come over
    // the network, and where it will be duplicated the day a second boundary appears.
    //
    // It pairs with a DTO by nature: coarse-grained calls need coarse-grained answers.

    /// <summary>
    ///     Everything a till needs from the back office, in as few round trips as the operations allow.
    /// </summary>
    [RemoteFacade]
    public interface ITillFacade {

        ShiftClosure CloseShift(string tillId, decimal countedCash);

        PriceList PricesFor(string storeCode, DateOnly day);

    }

    /// <summary>
    ///     The result of nine operations, returned once.
    /// </summary>
    [DataTransferObject]
    public sealed record ShiftClosure(decimal Expected, decimal Counted, decimal Variance, bool NeedsManager, string ReportText);

    /// <summary>
    ///     A day's prices, fetched once at open rather than per scan.
    /// </summary>
    [DataTransferObject]
    public sealed record PriceList(DateOnly Day, IReadOnlyDictionary<string, decimal> ByBarcode);

}
