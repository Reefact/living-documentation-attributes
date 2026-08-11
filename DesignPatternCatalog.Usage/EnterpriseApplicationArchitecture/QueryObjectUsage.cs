#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.QueryObjectSample {

    // Regional library: the acquisitions report nobody can write in one line.
    //
    // The acquisitions librarian wants titles that are overdue somewhere in the county, held at fewer than
    // three branches, published in the last five years, excluding reference stock — and she wants to add a
    // clause to that next month without a developer.
    //
    // Written as a method per combination, the repository grows a method per question ever asked. Written
    // as a SQL string passed in, the repository has handed its callers the storage model and stopped being
    // a repository at all.
    //
    // A QUERY OBJECT is the query as a VALUE: built up in steps, passed across a boundary, combined with
    // another, and translated to SQL in one place. Because it is an object, the caller states criteria in
    // the domain's language and never learns the schema — which is exactly what lets the repository next
    // door keep its promise.

    /// <summary>
    ///     A question about the stock, assembled rather than written.
    /// </summary>
    [QueryObject]
    public sealed record StockQuery {

        public string?  Branch          { get; init; }
        public bool     OverdueOnly     { get; init; }
        public int?     PublishedAfter  { get; init; }
        public bool     ExcludeReference { get; init; }

        public StockQuery At(string branch) {
            return this with { Branch = branch };
        }

        public StockQuery Overdue() {
            return this with { OverdueOnly = true };
        }

        public StockQuery PublishedSince(int year) {
            return this with { PublishedAfter = year };
        }

        /// <summary>
        ///     Two questions become one, which is the thing a method-per-question can never do.
        /// </summary>
        public StockQuery And(StockQuery other) {
            return new StockQuery {
                Branch           = Branch ?? other.Branch,
                OverdueOnly      = OverdueOnly || other.OverdueOnly,
                PublishedAfter   = PublishedAfter ?? other.PublishedAfter,
                ExcludeReference = ExcludeReference || other.ExcludeReference
            };
        }

    }

}
