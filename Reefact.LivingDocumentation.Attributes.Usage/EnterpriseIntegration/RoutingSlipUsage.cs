#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.RoutingSlipSample {

    // A hazardous container's paperwork goes through customs, then the port authority, then the line — unless
    // it is a tank container, in which case an inspection comes second. Six variations, and none of them worth
    // a process manager holding state for.
    //
    // ROUTING SLIP attaches the itinerary to the message. No step knows the next one, and nothing central
    // remembers where anything is.

    /// <summary>
    ///     Computes the itinerary and attaches it.
    /// </summary>
    /// <remarks>
    ///     The route travels with the message, so no step needs to know the next and no participant holds the
    ///     state.
    /// </remarks>
    [RoutingSlip.RoutingSlip(Itinerary = typeof(HazardousClearance))]
    public sealed class HazardousClearance {

        /// <summary>
        ///     The ordered steps carried on the message, and how far it has got.
        /// </summary>
        /// <remarks>
        ///     On the message rather than in a store, which is what makes a failure mid-route diagnosable from
        ///     the message alone.
        /// </remarks>
        [RoutingSlip.Itinerary]
        public IReadOnlyList<string> Steps { get; }

        public int Position { get; private set; }

        public HazardousClearance(bool isTank) {
            Steps = isTank
                ? new[] { "customs", "inspection", "port.authority", "line" }
                : new[] { "customs", "port.authority", "line" };
        }

        public string? Next() => Position < Steps.Count ? Steps[Position++] : null;

    }
}
