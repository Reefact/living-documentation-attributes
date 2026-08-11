#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

namespace DesignPatternCatalog.Usage.DomainDrivenDesign.RepositorySample {

    // Farm management: the parcels a holding declares to the paying agency.
    //
    // The parcels live in a cadastral database with a few hundred thousand rows, and the agronomist
    // writing a crop rotation does not want to think about that. What they want to write is "the
    // parcels of this holding", and to get back parcels.
    //
    // That is the whole of the pattern: the repository gives the illusion of an in-memory collection
    // of aggregates. Which is why the interface below says nothing about SQL, rows, connections or
    // transactions — those belong to the implementation, and letting one of them appear in the
    // signature would leak the storage into the model that the pattern exists to keep out of it.
    //
    // Two consequences worth noticing in the shape:
    //
    //   * The queries are named in the language of the domain — `InProductionFor`, not `Select`. A
    //     repository that exposes a generic query language has given up and become a database
    //     handle.
    //   * There is one repository per aggregate root, not one per table. `Parcel` is the root here;
    //     a `SoilSample` is reached through its parcel and gets no repository of its own, because
    //     nothing outside the aggregate is allowed to hold one.

    [ValueObject]
    public readonly record struct ParcelId(string CadastralReference);

    [Entity]
    public sealed class Parcel {

        public Parcel(ParcelId id, decimal hectares, string crop) {
            Id       = id;
            Hectares = hectares;
            Crop     = crop;
        }

        public ParcelId Id       { get; }
        public decimal  Hectares { get; }
        public string   Crop     { get; private set; }

        public void Sow(string crop) => Crop = crop;

    }

    [Repository]
    public interface IParcelRepository {

        Parcel?              ById(ParcelId id);
        IReadOnlyList<Parcel> InProductionFor(string holding);

        void Add(Parcel parcel);

        // No `Save`. Within the collection illusion, a parcel obtained from the repository is already
        // in it; persisting what changed is the unit of work's business, not the caller's.

    }

}
