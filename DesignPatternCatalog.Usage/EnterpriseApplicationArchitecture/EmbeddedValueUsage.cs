#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.EmbeddedValueSample {

    // Museum collection: the dimensions of an object, which were three loose columns for eleven years.
    //
    // `height_mm`, `width_mm`, `depth_mm` on the accession table, and every piece of code that needed to
    // compare two objects' sizes, or work out whether one fits a case, did it three numbers at a time. Two
    // of those places had the width and depth the wrong way round.
    //
    // An EMBEDDED VALUE lets the model have a Dimensions value while the schema keeps its three columns:
    // the value's fields map to the owner's row, so there is no table and no join.
    //
    // That is the whole trade, and it is a good one — it is what makes a rich value AFFORDABLE. Without it,
    // the choice is a value object that costs a join, or three loose numbers that cost correctness, and
    // most codebases take the numbers.
    //
    // It is available exactly while nothing needs the value on its own. The day the registrar wants to
    // query every object taller than two metres regardless of what it belongs to, or to share one set of
    // dimensions between an object and its frame, this stops being the right mapping — because an embedded
    // value has no identity and no row to find.

    /// <summary>
    ///     How big something is — one value in the model, three columns in the row.
    /// </summary>
    [EmbeddedValue]
    public readonly record struct Dimensions(int HeightMm, int WidthMm, int DepthMm) {

        public bool FitsInside(Dimensions space) {
            return HeightMm <= space.HeightMm && WidthMm <= space.WidthMm && DepthMm <= space.DepthMm;
        }

    }

    /// <summary>
    ///     An object in the collection.
    /// </summary>
    public sealed class CataloguedItem {

        [IdentityField]
        public long Id { get; set; }

        /// <summary>
        ///     Mapped into `height_mm`, `width_mm` and `depth_mm` on this object's own row.
        /// </summary>
        public Dimensions Dimensions { get; set; }

    }

}
