#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

// Regional rail: the timetable feed the operator publishes to everyone.
//
// Journey planners, station displays, the national open-data portal and three ticket resellers all consume
// the timetable. None of them is going to negotiate a format with us, and we are not going to write four
// integrations.
//
// So this assembly is a PUBLISHED LANGUAGE: one documented vocabulary, used as the medium of exchange with
// the outside. The distinction that matters is what it is NOT — it is not the operations model with a
// serializer bolted on. Inside Train Operations a service is a rich thing with paths, rolling stock
// diagrams and crew links; here it is a departure, an arrival and a list of calls, because that is what a
// journey planner needs and all it needs.
//
// Keeping the two apart is the whole point. The operations model changes when the railway changes; this
// vocabulary changes when its CONSUMERS can absorb a change, which is a different schedule and usually a
// much slower one. A published language that tracked the internal model would make every refactoring a
// breaking change for four external parties.
//
// Note what is absent: no behaviour, no invariants, no domain rules. This is a contract with the outside,
// so it is deliberately anaemic — the shape of anything richer would leak a model that consumers must not
// depend on.

[assembly: PublishedLanguage]

namespace Reefact.LivingDocumentation.Attributes.Usage.TrainOperations.Contracts.PublishedLanguageSample {

    /// <summary>
    ///     One train, on one day, as the outside world sees it.
    /// </summary>
    public sealed record PublishedService(string ServiceCode, DateOnly OperatingDay, IReadOnlyList<PublishedCall> Calls);

    /// <summary>
    ///     A stop, with the times a passenger can act on.
    /// </summary>
    public sealed record PublishedCall(string StationCode, TimeOnly? Arrival, TimeOnly? Departure);

}
