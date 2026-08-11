#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.DataTransferObjectSample {

    // A mountain weather network: one call from a hut, over a link that is down more often than not.
    //
    // Forty automatic stations report to a refuge whose only connection is a satellite modem billed by the
    // kilobyte and unavailable in bad weather — which is exactly when the readings matter. A chatty API
    // that fetched a station, then its sensors, then each sensor's last reading, would need a hundred and
    // twenty round trips and would fail halfway through.
    //
    // A DATA TRANSFER OBJECT is shaped by that call rather than by the model: everything the refuge needs
    // about every station, flattened, in one response.
    //
    // What makes it the pattern is what it refuses to be. No behaviour, no invariants, no cleverness — and
    // above all, NOT the domain object with a serializer on it. Publishing the model would tie forty
    // stations' firmware to the internal shape of this system, and the model could then never be
    // refactored without breaking devices on a mountainside. The duplication is the price of being able to
    // change one side without the other.
    //
    // Note the flattening: the summary carries `SensorCount` rather than the sensors, because the refuge
    // display shows a number. A DTO answers one screen's question, not the model's.

    /// <summary>
    ///     Everything the refuge needs about one station, in the response it already asked for.
    /// </summary>
    [DataTransferObject]
    public sealed record StationSummary(
        string   StationCode,
        decimal  AltitudeMetres,
        decimal? TemperatureCelsius,
        decimal? WindSpeedKnots,
        int      SensorCount,
        DateTimeOffset ReportedAt);

    /// <summary>
    ///     The whole network in one call, because the second call may not get through.
    /// </summary>
    [DataTransferObject]
    public sealed record NetworkSnapshot(DateTimeOffset TakenAt, IReadOnlyList<StationSummary> Stations);

}
