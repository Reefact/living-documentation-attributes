#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DomainDrivenDesign.ClosureOfOperationSample {

    // Cartography: the extent covered by a set of surveyed plots.
    //
    // A survey arrives as a few thousand plots, and the map server needs the rectangle that contains
    // them all. Written the obvious way, that is a loop with four running variables — minimum
    // latitude, maximum latitude, minimum longitude, maximum longitude — and the abstraction the
    // domain actually has, an extent, exists only in the reader's head between the loop and the
    // constructor at the end.
    //
    // A closure of operations is the alternative: an operation on `Extent` that takes an `Extent` and
    // gives back an `Extent`. Nothing else appears in the signature — no primitive, no service, no
    // type from another module — so the operation stays entirely inside the abstraction it belongs
    // to.
    //
    // Two things follow, and they are why Evans singles it out rather than filing it under "nice
    // signature":
    //
    //   * It composes without ceremony. `a.Union(b).Union(c)` is well-formed for the same reason
    //     `1 + 2 + 3` is, and the whole survey folds into one line with no running state at all.
    //   * It introduces no dependency. An operation returning a different type couples `Extent` to
    //     that type; this one couples it to nothing, so the class stays readable on its own.
    //
    // This is also the most mechanically checkable pattern in the catalog: the annotation claims the
    // parameter and the return type are the declaring type, and a rule can verify exactly that from
    // the signature — no interpretation required.

    [ValueObject]
    public readonly record struct Extent {

        public Extent(double southLatitude, double westLongitude, double northLatitude, double eastLongitude) {
            SouthLatitude = southLatitude;
            WestLongitude = westLongitude;
            NorthLatitude = northLatitude;
            EastLongitude = eastLongitude;
        }

        public double SouthLatitude { get; }
        public double WestLongitude { get; }
        public double NorthLatitude { get; }
        public double EastLongitude { get; }

        /// <summary>
        ///     The smallest extent containing this one and <paramref name="other" />.
        /// </summary>
        [ClosureOfOperation]
        [SideEffectFreeFunction]
        public Extent Union(Extent other) {
            return new Extent(
                Math.Min(SouthLatitude, other.SouthLatitude),
                Math.Min(WestLongitude, other.WestLongitude),
                Math.Max(NorthLatitude, other.NorthLatitude),
                Math.Max(EastLongitude, other.EastLongitude));
        }

        /// <summary>
        ///     The part covered by both extents, or an empty extent where they do not meet.
        /// </summary>
        [ClosureOfOperation]
        [SideEffectFreeFunction]
        public Extent Intersect(Extent other) {
            double south = Math.Max(SouthLatitude, other.SouthLatitude);
            double west  = Math.Max(WestLongitude, other.WestLongitude);
            double north = Math.Min(NorthLatitude, other.NorthLatitude);
            double east  = Math.Min(EastLongitude, other.EastLongitude);

            return north <= south || east <= west ? new Extent(0, 0, 0, 0) : new Extent(south, west, north, east);
        }

    }

    [Service]
    public sealed class SurveyExtent {

        // The whole survey folds into one expression, because every step of the fold stays an Extent.
        [SideEffectFreeFunction]
        public Extent Covering(IEnumerable<Extent> plots) => plots.Aggregate((left, right) => left.Union(right));

    }

}
