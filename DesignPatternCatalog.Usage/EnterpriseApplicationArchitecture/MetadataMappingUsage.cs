#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.MetadataMappingSample {

    // Museum collection: sixty-one entity types, and nobody writing sixty-one mappers by hand.
    //
    // The collection system covers objects, artists, exhibitions, loans, conservation, storage locations,
    // rights, donors. Written out, each mapper is the same forty lines with different column names — and
    // each is forty lines where a typo compiles.
    //
    // METADATA MAPPING holds the mapping as DATA a mapper reads, rather than as code written per class.
    // Here it is a table the studio can edit; elsewhere it is attributes on the classes, or an XML file.
    //
    // What it removes is real: sixty-one hand-written mappers become one engine and sixty-one rows.
    //
    // What it moves is the part worth annotating. A mapping error stops being a compilation failure and
    // becomes a run-time one — `AccessionNumber` misspelled in a row is found by a test or by a user, not
    // by the compiler. And what was explicit becomes something a reader must go and look up: the mapping
    // for an object is no longer next to the object, and "which column does this property go to" needs a
    // query rather than a click.
    //
    // That is a good trade at sixty-one types and a poor one at three, which is precisely the kind of
    // decision that is invisible six years later — hence the annotation.

    /// <summary>
    ///     The mapping itself, as data: one row per property, read by a single generic mapper.
    /// </summary>
    [MetadataMapping]
    public interface IMappingMetadata {

        string TableFor(Type entity);

        string ColumnFor(Type entity, string propertyName);

        IReadOnlyCollection<string> MappedProperties(Type entity);

    }

}
