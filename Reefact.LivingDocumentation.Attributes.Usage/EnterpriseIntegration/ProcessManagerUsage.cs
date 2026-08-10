#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.ProcessManagerSample {

    // A vessel call is a process with branches: if the draft survey disagrees with the manifest by more than a
    // tolerance, a reweigh is inserted and the load plan is recomputed. A routing slip cannot decide that,
    // because the itinerary depends on what the replies said.
    //
    // PROCESS MANAGER holds the state and decides as it goes. The trade is stated rather than discovered: it
    // can branch, and it is a participant that holds state and can become a bottleneck.

    /// <summary>
    ///     The definition the running processes follow.
    /// </summary>
    /// <remarks>
    ///     A template, so that changing how a vessel call runs is configuration rather than a class — the same
    ///     knowledge-level move a posting rule makes for money.
    /// </remarks>
    [ProcessManager.ProcessTemplate]
    public sealed class VesselCallDefinition {

        public VesselCallDefinition(IReadOnlyList<string> steps, decimal draftTolerance) {
            Steps           = steps;
            DraftTolerance  = draftTolerance;
        }

        public IReadOnlyList<string> Steps { get; }

        public decimal DraftTolerance { get; }

    }

    /// <summary>
    ///     One running occurrence, holding where it has got to.
    /// </summary>
    /// <remarks>
    ///     Separate from the manager because a manager serves many at once, and conflating them is how a
    ///     process manager becomes a single-threaded one.
    /// </remarks>
    [ProcessManager.ProcessInstance]
    public sealed class VesselCallInstance {

        public VesselCallInstance(string vesselCall, VesselCallDefinition definition) {
            VesselCall = vesselCall;
            Definition = definition;
        }

        public string VesselCall { get; }

        public VesselCallDefinition Definition { get; }

        public int Step { get; internal set; }

    }

    /// <summary>
    ///     Receives each reply and decides the next step.
    /// </summary>
    /// <remarks>
    ///     The alternative to a routing slip, and worth its cost only when the next step depends on what the
    ///     replies said.
    /// </remarks>
    [ProcessManager.ProcessManager(ProcessInstance = typeof(VesselCallInstance))]
    public sealed class VesselCallManager {

        private readonly Dictionary<string, VesselCallInstance> _running = new Dictionary<string, VesselCallInstance>();

        public string? OnReply(string vesselCall, decimal draftDifference) {
            VesselCallInstance instance = _running[vesselCall];
            if (draftDifference > instance.Definition.DraftTolerance) { return "terminal.reweigh"; }

            instance.Step++;

            return instance.Step < instance.Definition.Steps.Count ? instance.Definition.Steps[instance.Step] : null;
        }

        public void Start(string vesselCall, VesselCallDefinition definition) =>
            _running[vesselCall] = new VesselCallInstance(vesselCall, definition);

    }
}
