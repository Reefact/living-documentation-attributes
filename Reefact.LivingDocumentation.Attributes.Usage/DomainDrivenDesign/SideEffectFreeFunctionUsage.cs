#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DomainDrivenDesign.SideEffectFreeFunctionSample {

    // Maritime routing: how far apart two positions are, and how much fuel a leg will burn.
    //
    // A voyage planner tries hundreds of candidate routes before committing to one. It reorders legs,
    // drops a port, puts it back, and compares totals. Every one of those attempts calls the two
    // operations below, and the planner is only able to do that because calling them changes nothing.
    //
    // That is the property the annotation records, and it is worth recording because it cannot be
    // seen from the call site. `voyage.DistanceTo(port)` and `voyage.AddCall(port)` look alike in the
    // planner's code; one can be tried and discarded, the other cannot. Splitting a model into the
    // operations that answer and the operations that change is what makes the first kind safe to use
    // freely — cached, retried, run in parallel, evaluated speculatively.
    //
    // The discipline has an edge worth being explicit about: side-effect-free does not mean small.
    // `FuelForLeg` does real work. What it does not do is leave a trace: no field is assigned, no
    // argument is mutated, nothing is written anywhere. Run it twice and the second run is
    // indistinguishable from the first.
    //
    // Note also that these return values, and richer ones than a number where it helps — a distance
    // is a distance, not a bare double. A function that returns a value object rather than a
    // primitive stays composable, which is what makes it worth keeping free of effects in the first
    // place.

    [ValueObject]
    public readonly record struct Position(double LatitudeDegrees, double LongitudeDegrees);

    [ValueObject]
    public readonly record struct NauticalMiles(double Value);

    [ValueObject]
    public readonly record struct MetricTonnes(double Value);

    [Service]
    public sealed class VoyageCalculator {

        private const double EarthRadiusNauticalMiles = 3440.065;

        [SideEffectFreeFunction]
        public NauticalMiles GreatCircleDistance(Position from, Position to) {
            double φ1 = ToRadians(from.LatitudeDegrees);
            double φ2 = ToRadians(to.LatitudeDegrees);
            double Δφ = ToRadians(to.LatitudeDegrees  - from.LatitudeDegrees);
            double Δλ = ToRadians(to.LongitudeDegrees - from.LongitudeDegrees);

            double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2)
                     + Math.Cos(φ1)     * Math.Cos(φ2) * Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);

            return new NauticalMiles(2 * EarthRadiusNauticalMiles * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)));
        }

        // Not trivial, and still free of effects: it reads its arguments, computes, and returns.
        [SideEffectFreeFunction]
        public MetricTonnes FuelForLeg(NauticalMiles distance, double serviceSpeedKnots, double displacementTonnes) {
            double hours       = distance.Value / serviceSpeedKnots;
            double cubicFactor = Math.Pow(serviceSpeedKnots, 3) / Math.Pow(14.0, 3);

            return new MetricTonnes(hours * cubicFactor * displacementTonnes * 0.00012);
        }

        private static double ToRadians(double degrees) => degrees * Math.PI / 180.0;

    }

}
