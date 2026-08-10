#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.ContentEnricherSample {

    // A gate transaction carries a container number and nothing else — the gate reads it off the box and has
    // no reason to know more. Yard planning needs the ISO type, the tare weight and whether it is a tank,
    // because those decide which stack it can go on.
    //
    // CONTENT ENRICHER fetches the rest. The gate stays ignorant, and the dependency on the registry is
    // stated rather than buried in the planner.

    /// <summary>
    ///     What the gate can say.
    /// </summary>
    public sealed record GateArrival(string ContainerNumber);

    /// <summary>
    ///     What yard planning needs.
    /// </summary>
    public sealed record PlannableArrival(string ContainerNumber, string IsoType, int TareKilos, bool IsTank);

    /// <summary>
    ///     The registry the missing data comes from.
    /// </summary>
    /// <remarks>
    ///     Named because it is the difference from a plain message translator: the enricher has a dependency
    ///     outside the message, so it can be slow, be down, or answer differently tomorrow.
    /// </remarks>
    [ContentEnricher.Resource]
    public interface IContainerRegistry {

        (string IsoType, int TareKilos, bool IsTank) Describe(string containerNumber);

    }

    /// <summary>
    ///     Adds to a gate arrival what the gate could not supply.
    /// </summary>
    /// <remarks>
    ///     It uses what the message already carries — the container number — to fetch the rest. The
    ///     destination does not change, only the content, which is what makes it a transformer.
    /// </remarks>
    [ContentEnricher.ContentEnricher(Resource = typeof(IContainerRegistry))]
    public sealed class GateArrivalEnricher {

        private readonly IContainerRegistry _registry;

        public GateArrivalEnricher(IContainerRegistry registry) {
            _registry = registry;
        }

        public PlannableArrival Enrich(GateArrival arrival) {
            (string isoType, int tareKilos, bool isTank) = _registry.Describe(arrival.ContainerNumber);

            return new PlannableArrival(arrival.ContainerNumber, isoType, tareKilos, isTank);
        }

    }
}
