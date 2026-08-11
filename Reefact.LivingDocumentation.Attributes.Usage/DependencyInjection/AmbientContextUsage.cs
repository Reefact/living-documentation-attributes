#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DependencyInjection;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DependencyInjection.AmbientContextSample {

    // Everything at the station needs the time, and for nine years everything got it from one static
    // property. It is reached from sixty-one places, and the transmitter guard's clock — injected through
    // its constructor, on purpose, after the outside-broadcast incident — is the exception rather than the
    // rule.
    //
    // What it cost showed up in the tests. Freezing the clock for one test froze it for whatever ran
    // beside it, so the schedule tests had to run in sequence, and the suite went from forty seconds to
    // four minutes. Nobody connected the two for a year.
    //
    // Worth knowing about this one: the same author called it a PATTERN in the 2011 edition and files it
    // under anti-patterns in the 2019 one. The catalogue follows the 2019 edition, and that is exactly why
    // ADR-0037 names the edition rather than the work.

    /// <summary>
    ///     The station's clock, reachable from anywhere.
    /// </summary>
    /// <remarks>
    ///     Whatever depends on this says so nowhere. Two classes that use the time and two that need
    ///     nothing look identical from outside, so there is no list of what breaks when it changes — and
    ///     the only way to find the sixty-one call sites is to search for the name.
    ///     <para>
    ///         The annotation is on the access point rather than on the class, because the access point is
    ///         what makes it ambient: an injected <c>StationClock</c> would be an ordinary dependency, and
    ///         it is <c>Current</c> that lets anything reach it.
    ///     </para>
    /// </remarks>
    public static class StationClock {

        [AmbientContext]
        public static IClock Current { get; set; } = new SystemClock();

    }

    public interface IClock {

        DateTimeOffset Now();

    }

    public sealed class SystemClock : IClock {

        public DateTimeOffset Now() {
            return DateTimeOffset.UtcNow;
        }

    }

    /// <summary>
    ///     One of the sixty-one, and a fair example of why they were written this way.
    /// </summary>
    /// <remarks>
    ///     Reaching for the clock here is one line; taking it as a parameter would mean threading it
    ///     through the four callers above, none of which needs it. That is the trade the ambient context
    ///     offers, and it is a real one — which is why the entry records the shape rather than scolding
    ///     about it.
    /// </remarks>
    public sealed class PlayoutLogLine {

        public string Format(string trackId) {
            return $"{StationClock.Current.Now():HH:mm:ss} {trackId}";
        }

    }

}
