#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

using DesignPatternCatalog.Usage.RailNetwork.SharedKernelSample;

#endregion

namespace DesignPatternCatalog.Usage.TrainOperations.OpenHostServiceSample {

    // Regional rail: one protocol for everyone who asks what the network can still take.
    //
    // Six parties want to know whether a section is free at a given minute: the freight sales desk, two
    // ticket resellers, the engineering works planner, the national path-request portal and our own
    // Invoicing assembly reconciling what was booked against what ran.
    //
    // The way that usually goes is six integrations, negotiated one at a time, each shaped by whoever asked
    // most recently — and then a change to the model has to be agreed six times.
    //
    // An OPEN HOST SERVICE is the other way round: design the protocol ONCE, for all comers, and publish
    // it. The difference is not technical — it is who the interface is shaped for. An integration built for
    // one consumer answers that consumer's question; a host service answers the question the subsystem is
    // able to answer, and lets consumers take what they need.
    //
    // Two consequences show in the shape below. The protocol speaks the shared kernel's vocabulary
    // (SectionId, KilometrePoint), never the internal one — TrainPath does not appear, because it would tie
    // every consumer to a model that changes when the railway changes. And a consumer wanting more than
    // this gets an EXTENSION rather than a change: the freight desk's reservation service sits beside this
    // one instead of adding a parameter that the other five would have to absorb.

    /// <summary>
    ///     What the network can still take. Designed for every consumer, not for the one who asked first.
    /// </summary>
    [OpenHostService]
    public interface INetworkCapacityService {

        bool IsSectionAvailable(SectionId section, DateOnly day, TimeOnly from, TimeOnly to);

        IReadOnlyCollection<SectionId> SectionsAvailableAt(DateOnly day, TimeOnly at);

    }

}
