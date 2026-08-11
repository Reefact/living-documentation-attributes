#region Usings declarations

using DesignPatternCatalog.Idioms;

#endregion

namespace DesignPatternCatalog.Usage.Idioms.NullObjectSample {

    // Water treatment: the siren that is not there.
    //
    // Every pumping station raises alarms — a chlorine reading out of range, a pump drawing too much
    // current. Most stations have a siren and a beacon. The three oldest have neither: they are unmanned,
    // fenced, and the alarm goes to the control room over the telemetry link and nowhere else.
    //
    // The code that decides an alarm has to run identically at all of them, because the decision is about
    // water quality and has nothing to do with what hardware happens to be bolted to the wall. What it must
    // not become is this:
    //
    //     if (station.Siren is not null) { station.Siren.Sound(level); }
    //
    // — repeated at every point that raises an alarm, and forgotten at the one added last winter.
    //
    // A NULL OBJECT is the do-nothing member of the protocol: the station with no siren gets one that
    // accepts the call and ignores it. The check disappears because there is nothing to check.
    //
    // What makes it a NULL object rather than merely a special case is that its answers are NEUTRAL by
    // design. Sound() does nothing. IsSounding answers false — not "false because the siren is broken",
    // which would be information, but false because there is nothing to report. A special case that
    // answered something meaningful — a lapsed insurance policy refusing settlement, next door in
    // EnterpriseApplicationArchitecture/SpecialCaseUsage.cs — is the broader pattern this narrows.
    //
    // That relation is in the catalog, not just in this comment: NullObject is declared a specialisation of
    // SpecialCase, so a rule written for special cases reaches this too, and a consumer counting patterns
    // still counts two.

    /// <summary>
    ///     Whatever makes a noise at a station, if anything does.
    /// </summary>
    public interface IAudibleAlarm {

        void Sound(int level);

        void Silence();

        bool IsSounding { get; }

    }

    /// <summary>
    ///     A real siren, at a station that has one.
    /// </summary>
    public sealed class Siren : IAudibleAlarm {

        public void Sound(int level) {
            IsSounding = level > 0;
        }

        public void Silence() {
            IsSounding = false;
        }

        public bool IsSounding { get; private set; }

    }

    /// <summary>
    ///     The alarm of a station that has none.
    /// </summary>
    /// <remarks>
    ///     Every answer is the neutral one, and that is the whole design: a caller that had to know this is
    ///     the silent kind would be back to the null check this removes.
    /// </remarks>
    [NullObject]
    public sealed class NoAudibleAlarm : IAudibleAlarm {

        public void Sound(int level) { }

        public void Silence() { }

        public bool IsSounding => false;

    }

}
