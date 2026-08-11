#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.SerializedLobSample {

    // Museum collection: the conservation dossier, which no two conservators structure the same way.
    //
    // A dossier holds treatments, materials, environmental readings, photographs at each stage, and the
    // conservator's own notes. Its shape changes with the discipline — a textile dossier and a panel
    // painting dossier share almost nothing — and it changes again whenever the studio adopts a new
    // standard.
    //
    // Mapped properly that is nine tables that will be eleven next year. And nobody queries into it: the
    // dossier is read whole, by a conservator, on the screen for that object.
    //
    // A SERIALIZED LOB stores the graph in one column, whole.
    //
    // The cost is deferred rather than avoided, and both halves are worth saying. Nothing can query inside
    // it — "which objects were treated with beeswax" becomes unanswerable in SQL, and the day the registrar
    // asks, the answer is a migration. And every stored value must stay readable as the format evolves,
    // which means the serialised form is a published format with versions, whether or not anyone treats it
    // as one.
    //
    // Chosen here because both costs are acceptable and the alternative is nine tables nobody queries.

    /// <summary>
    ///     Everything the conservation studio recorded about one object, stored as one value.
    /// </summary>
    /// <remarks>
    ///     Written to a single column. The shape below will change; what is already stored will not, so a
    ///     reader must handle every version it has ever written.
    /// </remarks>
    [SerializedLob]
    public sealed record ConservationDossier(
        int                          FormatVersion,
        IReadOnlyList<Treatment>     Treatments,
        IReadOnlyList<string>        Materials,
        string                       ConservatorNotes);

    public sealed record Treatment(DateOnly Performed, string By, string Description);

    /// <summary>
    ///     An object in the collection.
    /// </summary>
    public sealed class CataloguedItem {

        [IdentityField]
        public long Id { get; set; }

        public ConservationDossier? Dossier { get; set; }

    }

}
