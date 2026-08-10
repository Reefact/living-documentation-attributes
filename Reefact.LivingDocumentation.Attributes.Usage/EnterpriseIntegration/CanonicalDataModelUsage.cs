#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.CanonicalDataModelSample {

    // Six systems around the terminal: gate, yard, crane, billing, customs and the vessel interface. Letting
    // each translate to each of the others is thirty translators, and a seventh system makes it forty-two.
    //
    // CANONICAL DATA MODEL is the format that belongs to none of them. Each system translates to it and from
    // it, so the seventh costs two translators instead of twelve.

    /// <summary>
    ///     A container movement, in the terminal's own words rather than any system's.
    /// </summary>
    /// <remarks>
    ///     Annotating it is what makes the indirection countable — and a type that has quietly acquired the
    ///     gate's vocabulary is how the saving is lost, one field at a time.
    /// </remarks>
    [CanonicalDataModel]
    public sealed record ContainerMove(string ContainerNumber,
                                       string FromPosition,
                                       string ToPosition,
                                       DateTimeOffset At);

    /// <summary>
    ///     The vessel call, likewise.
    /// </summary>
    [CanonicalDataModel]
    public sealed record VesselCall(string CallSign, DateTimeOffset Arrival, DateTimeOffset Departure);

    // The role also targets an assembly, which is the usual shape once the model is more than a handful of
    // types: the canonical model is its own assembly, and [assembly: CanonicalDataModel] says so once rather
    // than on every record in it.
}
