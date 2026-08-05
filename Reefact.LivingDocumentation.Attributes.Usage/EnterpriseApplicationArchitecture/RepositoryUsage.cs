#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.RepositorySample {

    // Regional library: asking for books without asking a database.
    //
    // A REPOSITORY is a collection-like interface onto domain objects. The illusion it maintains is that
    // the objects are simply THERE, in memory, and that the code asking for them is written in the
    // library's language rather than in storage's.
    //
    // Two things below are the pattern rather than decoration:
    //
    //   * the queries are named for the domain — OnLoanTo, Overdue — not Select or Where;
    //   * criteria arrive as an OBJECT (see QueryObjectUsage.cs), not as a query language. A repository
    //     that took a SQL string, or an IQueryable, would have handed its callers the storage model back
    //     and stopped being this pattern.
    //
    // Note what it sits on. A repository is not an alternative to a data mapper; it is usually in front of
    // one, and the data mapper next door is exactly what this would delegate to.
    //
    // Evans catalogued this pattern too, a year later, and added a constraint Fowler's does not carry: a
    // repository serves an AGGREGATE ROOT, and only an aggregate root. That is why the catalog relates the
    // two by specialisation rather than declension — DomainDrivenDesign/RepositoryUsage.cs is the narrower
    // one, and a rule written for this reaches it.

    /// <summary>
    ///     Copies of books, as a collection that happens to be very large and very far away.
    /// </summary>
    [Repository]
    public interface ICopyRepository {

        Copy? WithBarcode(string barcode);

        IReadOnlyCollection<Copy> OnLoanTo(long memberId);

        IReadOnlyCollection<Copy> Matching(CopyCriteria criteria);

        void Add(Copy copy);

        void Remove(Copy copy);

    }

    /// <summary>
    ///     One physical book on one shelf.
    /// </summary>
    public sealed record Copy(string Barcode, string Isbn, string Branch);

    /// <summary>
    ///     Criteria as a value, so that callers never meet a query language.
    /// </summary>
    public sealed record CopyCriteria(string? Branch = null, bool? AvailableOnly = null);

}
