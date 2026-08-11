#region Usings declarations

using DesignPatternCatalog.DependencyInjection;

#endregion

namespace DesignPatternCatalog.Usage.DependencyInjection.CompositionRootSample {

    // A community radio station's playout system: schedules, jingles, royalty returns to the collecting
    // society, and a transmitter that must never be handed silence.
    //
    // It grew a container, and then the container grew into the code. A resolve call appeared in the
    // schedule editor because a dependency was awkward to reach; another in the royalty report because
    // the first one had made it look normal. By the time anybody counted there were nineteen, in eleven
    // classes, and nothing could be constructed in a test without standing up the whole container.
    //
    // COMPOSITION ROOT puts the composing in one place, as close to the entry point as it goes. The
    // nineteen calls became zero, and the rule that keeps it that way is checkable by a build: no
    // assembly but this one references the container package.

    /// <summary>
    ///     Where the station's object graph is assembled.
    /// </summary>
    /// <remarks>
    ///     The only place that may reference a DI container. Every other module here is composed rather
    ///     than composing, which is what makes each of them constructible in a test with three lines and
    ///     no container at all.
    ///     <para>
    ///         There is one of these for the station, and there would be one however large the station
    ///         grew — the rule is one per application, not one per feature. The playout library that ships
    ///         to the two relay stations has none, on purpose: composing is the application's decision, and
    ///         a library that composes has taken it away from its host.
    ///     </para>
    /// </remarks>
    public static class StationStartup {

        [CompositionRoot]
        public static PlayoutScheduler Compose(string scheduleConnectionString) {
            // Pure DI here — no container — because the graph is small enough to read. The annotation is
            // about where composition happens, not about what does it.
            IScheduleRepository schedules = new SqlScheduleRepository(scheduleConnectionString);
            IClock              clock     = new SystemClock();

            return new PlayoutScheduler(schedules, clock);
        }

    }

    public interface IScheduleRepository {

        string? WhatIsOnAt(DateTimeOffset moment);

    }

    public interface IClock {

        DateTimeOffset Now();

    }

    public sealed class SqlScheduleRepository : IScheduleRepository {

        private readonly string _connectionString;

        public SqlScheduleRepository(string connectionString) {
            _connectionString = connectionString;
        }

        public string? WhatIsOnAt(DateTimeOffset moment) {
            return _connectionString.Length == 0 ? null : "Morning Show";
        }

    }

    public sealed class SystemClock : IClock {

        public DateTimeOffset Now() {
            return DateTimeOffset.UtcNow;
        }

    }

    public sealed class PlayoutScheduler {

        private readonly IScheduleRepository _schedules;
        private readonly IClock              _clock;

        public PlayoutScheduler(IScheduleRepository schedules, IClock clock) {
            _schedules = schedules;
            _clock     = clock;
        }

        public string NowPlaying() {
            return _schedules.WhatIsOnAt(_clock.Now()) ?? "Sustaining Service";
        }

    }

}
