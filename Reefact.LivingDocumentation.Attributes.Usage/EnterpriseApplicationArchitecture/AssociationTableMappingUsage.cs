#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.AssociationTableMappingSample {

    // Museum collection: who made what, when several people made one thing.
    //
    // A triptych has a painter, a gilder and a workshop. A single artist worked on four hundred items. The
    // association runs both ways and neither side can hold it — there is no column on `accession` that can
    // carry three artists, and none on `artist` that can carry four hundred accessions.
    //
    // An ASSOCIATION TABLE MAPPING gives the association a table of its own: `accession_artist`, two keys,
    // one row per pairing.
    //
    // What makes this pattern worth its own name — rather than being "the many-to-many one" — is the
    // consequence in the last sentence of the catalog entry: it is the only mapping where the association
    // has an existence separate from both ends. So the moment the link itself carries something, there is
    // already somewhere to put it.
    //
    // And it does here. The museum needs to record what each artist DID: painted, gilded, restored in 1978.
    // That attribution is a fact about the pairing, not about the artist and not about the object, and the
    // link table is its natural home — which is why the collection below is of Attribution rather than of
    // Artist.

    /// <summary>
    ///     One person's part in one object — a row of the association table, given a name.
    /// </summary>
    public sealed record Attribution(Artist Artist, string Role, int? Year);

    /// <summary>
    ///     A maker.
    /// </summary>
    public sealed class Artist {

        [IdentityField]
        public long Id { get; set; }

        public string Name { get; set; } = "";

    }

    /// <summary>
    ///     An object in the collection, and everyone who had a hand in it.
    /// </summary>
    public sealed class CataloguedItem {

        [IdentityField]
        public long Id { get; set; }

        /// <summary>
        ///     Stored in `accession_artist`, which carries the role and the year as well as the two keys.
        /// </summary>
        [AssociationTableMapping]
        public IList<Attribution> Attributions { get; } = new List<Attribution>();

    }

}
