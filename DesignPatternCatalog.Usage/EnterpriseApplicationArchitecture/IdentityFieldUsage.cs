#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.IdentityFieldSample {

    // A museum collection system — the domain shared by the structural mapping family below, because these
    // patterns are all about how one model meets one schema, and that is only legible against a real model
    // and a real schema.
    //
    // An IDENTITY FIELD is the row's key, carried on the object so that the two can be matched again.
    //
    // It is the one piece of the database a domain object legitimately holds, and the annotation earns its
    // place by saying which member it is — because the mistake it guards against is treating it as part of
    // the model's MEANING.
    //
    // Two accessions are not equal because their ids match; they are equal because their accession numbers
    // do — that is what the registrar means by the same object, and it is written on the museum's labels.
    // And two unsaved accessions are not equal at all, though both carry an id of zero, which is the bug
    // every codebase writes once. Note that Equals below ignores the identity field entirely: the key
    // belongs to the mapper, not to the museum.
    //
    // Fowler is explicit that the choice of key — table-unique or database-unique, meaningful or not —
    // belongs to the mapping rather than to the model. This one is a meaningless surrogate on purpose:
    // accession numbers have been renumbered twice since 1974.

    /// <summary>
    ///     Anything the museum has accessioned.
    /// </summary>
    public class Accession {

        public Accession(string accessionNumber, string title) {
            AccessionNumber = accessionNumber;
            Title           = title;
        }

        /// <summary>
        ///     The database's key, and nothing the museum would recognise.
        /// </summary>
        /// <remarks>
        ///     Assigned by the mapper on first save, zero until then, and deliberately absent from equality.
        /// </remarks>
        [IdentityField]
        public long Id { get; set; }

        /// <summary>The museum's own identifier — what a registrar means by "the same object".</summary>
        public string AccessionNumber { get; }

        public string Title { get; }

        public override bool Equals(object? other) {
            return other is Accession accession && accession.AccessionNumber == AccessionNumber;
        }

        public override int GetHashCode() {
            return AccessionNumber.GetHashCode();
        }

    }

}
