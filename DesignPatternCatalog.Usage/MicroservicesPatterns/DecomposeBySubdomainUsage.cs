#region Usings declarations

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.DecomposeBySubdomainSample {

    // The same operator, cut the other way. Network planning and settlement both belong to the capability
    // "run the grid", and they hold irreconcilable models of the same word: a *connection* to planning is a
    // physical joint with a rating, and to settlement it is a metered point with a contract start date.
    //
    // DECOMPOSE BY SUBDOMAIN puts the line where the models part. It usually lands where a capability line
    // would; where it does not, it is because two parts of one capability mean different things by the same
    // noun — and that is a reason to split that no organisation chart shows.

    /// <summary>
    ///     Network planning: the core subdomain.
    /// </summary>
    /// <remarks>
    ///     Core in the work's classification, borrowed from Evans: what the operator does better than the
    ///     operator next door, and the last thing anybody should buy off the shelf.
    /// </remarks>
    [DecomposeBySubdomain]
    public interface INetworkPlanning {

        decimal RatingOf(string connection);

    }

    /// <summary>
    ///     Settlement: a supporting subdomain, with its own meaning of the same words.
    /// </summary>
    /// <remarks>
    ///     Its <c>connection</c> is not planning's <c>connection</c>, and the annotation is what says the
    ///     collision is deliberate rather than an unfinished refactoring.
    /// </remarks>
    [DecomposeBySubdomain]
    public interface ISettlement {

        decimal ChargeFor(string connection);

    }
}
