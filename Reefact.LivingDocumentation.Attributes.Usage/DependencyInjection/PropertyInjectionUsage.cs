#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DependencyInjection;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DependencyInjection.PropertyInjectionSample {

    // The playout engine can announce what it is doing — track changes, fades, the moment it falls back to
    // the sustaining service. The station's own installation sends that to the studio wallboard. The two
    // relay stations that run the same engine have no wallboard and want none.
    //
    // Requiring an announcer in the constructor meant the relays passed a do-nothing one, which meant the
    // do-nothing one was public API, which meant somebody eventually shipped it to the main station by
    // configuring the wrong profile. Nobody noticed for a fortnight: the wallboard was blank, and a blank
    // wallboard looks like a quiet night.
    //
    // PROPERTY INJECTION says what is true: the engine works without an announcer, and announcing is
    // something an installation may add.

    public interface IPlayoutAnnouncer {

        void Announce(string what);

    }

    /// <summary>
    ///     An announcer that says nothing, which is the default and is not a placeholder.
    /// </summary>
    /// <remarks>
    ///     This is what makes the property injection honest. Without a default that genuinely works, the
    ///     dependency is required and the property is a constructor parameter that has forgotten to fail.
    /// </remarks>
    public sealed class SilentAnnouncer : IPlayoutAnnouncer {

        public void Announce(string what) { }

    }

    /// <summary>
    ///     Plays what the schedule asks for.
    /// </summary>
    public sealed class PlayoutEngine {

        private IPlayoutAnnouncer _announcer = new SilentAnnouncer();

        /// <remarks>
        ///     Optional, and the annotation is the claim that it is: this engine runs on the two relay
        ///     stations with nothing set here, and does so correctly rather than quietly.
        ///     <para>
        ///         The failure this shape prevents is the one that has no exception. A required dependency
        ///         left null throws somewhere far from here, days later, at three in the morning; a genuinely
        ///         optional one with a working default never throws at all, because there was nothing to
        ///         announce to.
        ///     </para>
        /// </remarks>
        [PropertyInjection]
        public IPlayoutAnnouncer Announcer {
            get => _announcer;
            set => _announcer = value ?? new SilentAnnouncer();
        }

        public void Play(string trackId) {
            _announcer.Announce($"now playing {trackId}");
        }

    }

}
