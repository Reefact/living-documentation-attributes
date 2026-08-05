#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

using Reefact.LivingDocumentation.Attributes.Usage.RailNetwork.SharedKernelSample;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.TrainOperations.AnticorruptionLayerSample {

    // Regional rail: talking to the national timetable mainframe without catching what it has.
    //
    // Paths are ultimately confirmed by a system written in 1987. It answers with fixed-width records, it
    // calls a section a "TRACK-SEG", it encodes a time as an integer number of minutes since midnight that
    // goes past 1440 for trains running after midnight, and it reports a cancelled path as one with an
    // entry time of 9999. None of that is negotiable: the mainframe is upstream, it has other consumers,
    // and it will outlive this project.
    //
    // Left alone, that model creeps. One method takes an int because "the mainframe gives us minutes", then
    // a field keeps a TRACK-SEG string because converting felt wasteful, and within a year the operations
    // model is reasoning about 9999 — a concept it has no name for and no rule about.
    //
    // An ANTICORRUPTION LAYER is a wall with three distinct jobs in it, and the value is in keeping them
    // distinct rather than in having a wall:
    //
    //   * the FACADE simplifies the mainframe, still speaking the mainframe's language;
    //   * the TRANSLATOR converts between the two models, and is the only thing that knows both;
    //   * the ADAPTER is what our model calls, and speaks only our language.
    //
    // The test that the layer works is mechanical: no type from the upstream model appears in any signature
    // outside this file. A rule engine can check exactly that, which is what these annotations are for.

    #region The upstream model — what the mainframe actually says

    /// <summary>
    ///     A record as the 1987 system returns it. Deliberately ugly: this is not ours to fix.
    /// </summary>
    public sealed record MainframePathRecord(string TrackSeg, int EntryMinutes, int ExitMinutes);

    #endregion

    /// <summary>
    ///     A face over the mainframe that is easier to call — and still entirely in its terms.
    /// </summary>
    /// <remarks>
    ///     It translates nothing: <c>TrackSeg</c> and minutes-since-midnight are still here, because a facade
    ///     that started converting would be doing the translator's job and there would be two places that
    ///     know both models.
    /// </remarks>
    [AnticorruptionLayer.Facade]
    public interface IMainframeTimetableFacade {

        IReadOnlyCollection<MainframePathRecord> PathsForDay(string operatorCode, DateOnly day);

    }

    /// <summary>
    ///     The only thing in the codebase that knows both models.
    /// </summary>
    /// <remarks>
    ///     Everything the upstream system gets wrong by our lights is dealt with here and nowhere else: the
    ///     9999 sentinel becomes an absent path, and minutes past 1440 become a time on the following day.
    /// </remarks>
    [AnticorruptionLayer.Translator]
    public interface IMainframePathTranslator {

        ConfirmedPath? ToConfirmedPath(MainframePathRecord record);

    }

    /// <summary>
    ///     What Train Operations calls. Nothing upstream appears in its signature — that is the whole test.
    /// </summary>
    [AnticorruptionLayer.Adapter(Facade = typeof(IMainframeTimetableFacade), Translator = typeof(IMainframePathTranslator))]
    public interface IConfirmedPathRepository {

        IReadOnlyCollection<ConfirmedPath> ConfirmedFor(Operator holder, DateOnly day);

    }

    #region The downstream model — ours

    /// <summary>
    ///     A path the national system has confirmed, in our terms.
    /// </summary>
    public sealed record ConfirmedPath(SectionId Section, DateTimeOffset Entry, DateTimeOffset Exit);

    /// <summary>
    ///     Declared here rather than reused, so the sample shows the boundary rather than crossing it.
    /// </summary>
    public sealed record Operator(string LicenceNumber);

    #endregion

}
