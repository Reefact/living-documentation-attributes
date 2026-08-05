#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.MapperSample {

    // Regional library: keeping the catalogue and the discovery index from knowing about each other.
    //
    // A MAPPER sets up communication between two things while both remain ignorant of it — and of each
    // other. That direction of ignorance is the whole distinction from a GATEWAY: a gateway's caller knows
    // it and calls it deliberately; a mapper is called by neither of the things it joins.
    //
    // Here the two are the library catalogue and the public discovery index that powers the website's
    // search. They evolve separately, they are owned by different teams, and neither should carry a
    // reference to the other — a catalogue that imported the index's model would start acquiring fields
    // that exist only because a search box wants them.
    //
    // So something outside both moves data across, and it is the only thing that knows both shapes. When
    // the index adds a facet, exactly one file changes, and it is not in the catalogue.
    //
    // The specialisation next door, DATA MAPPER, is this same idea with the second side fixed: a database.

    /// <summary>
    ///     Moves catalogue records into index documents. Neither side knows it exists.
    /// </summary>
    [Mapper]
    public interface ICatalogueToIndexMapper {

        IndexDocument ToDocument(CatalogueRecord record);

    }

    /// <summary>
    ///     The catalogue's shape: what a librarian curates.
    /// </summary>
    public sealed record CatalogueRecord(string Isbn, string Title, string Author, string DeweyCode, int Copies);

    /// <summary>
    ///     The index's shape: what a search box needs.
    /// </summary>
    public sealed record IndexDocument(string Id, string Heading, IReadOnlyList<string> Keywords, bool Borrowable);

}
