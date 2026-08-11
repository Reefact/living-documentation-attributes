#region Usings declarations

using DesignPatternCatalog.DependencyInjection;

#endregion

namespace DesignPatternCatalog.Usage.DependencyInjection.ConstructorInjectionSample {

    // The station's transmitter guard decides, every ten seconds, whether what is going out is what
    // should be going out. It cannot work without the schedule and it cannot work without a clock: with
    // either missing there is no question for it to answer.
    //
    // The version before this one took them as properties, set after construction by whoever remembered.
    // A new outside-broadcast path forgot the clock, and the guard compared the current programme against
    // a schedule read at midnight — for six days, without failing, because a null clock read as "no
    // change to report".
    //
    // CONSTRUCTOR INJECTION makes that impossible rather than unlikely: the guard cannot be built at all
    // without both.

    public interface IScheduleRepository {

        string? WhatIsOnAt(DateTimeOffset moment);

    }

    public interface IClock {

        DateTimeOffset Now();

    }

    /// <summary>
    ///     Checks that what is on air is what the schedule says.
    /// </summary>
    public sealed class TransmitterGuard {

        private readonly IScheduleRepository _schedules;
        private readonly IClock              _clock;

        /// <remarks>
        ///     Both of these are required, and the constructor is where that word is enforceable: an
        ///     instance cannot exist without them, so no code path anywhere can reach a half-built guard.
        ///     <para>
        ///         The annotation is a claim about *requirement*, not about mechanism. A dependency that may
        ///         legitimately be absent does not belong here — it belongs on a property, with a default
        ///         that works. And a parameter added here is a new demand on every composition root that
        ///         builds this type, which is the cost worth seeing before it is paid.
        ///     </para>
        /// </remarks>
        [ConstructorInjection]
        public TransmitterGuard(IScheduleRepository schedules, IClock clock) {
            _schedules = schedules ?? throw new ArgumentNullException(nameof(schedules));
            _clock     = clock     ?? throw new ArgumentNullException(nameof(clock));
        }

        public bool IsOnSchedule(string whatIsActuallyPlaying) {
            string? expected = _schedules.WhatIsOnAt(_clock.Now());

            return expected is not null && expected == whatIsActuallyPlaying;
        }

    }

}
