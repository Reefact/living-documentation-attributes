#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.DependencyInjection;

#endregion

namespace DesignPatternCatalog.Usage.DependencyInjection.MethodInjectionSample {

    // Every quarter the station reports what it played to a collecting society, and which society depends
    // on the track: the domestic one for most of it, a different one for the two hours of imported jazz,
    // and a third for anything from the community archive, which charges nothing but wants the returns
    // anyway.
    //
    // The report class first took the society in its constructor, which meant three report classes, then
    // one report class built three times per quarter with a loop outside it that nobody could follow.
    //
    // METHOD INJECTION puts the society where it actually varies: on the call. One report, three calls,
    // and the thing that changes is visible at the point it changes.

    public interface IRightsRegistry {

        decimal RoyaltyFor(string trackId, int seconds);

    }

    /// <summary>
    ///     What the station owes for a quarter's play-out.
    /// </summary>
    public sealed class RoyaltyReturn {

        private readonly IReadOnlyList<(string TrackId, int Seconds)> _played;

        public RoyaltyReturn(IReadOnlyList<(string TrackId, int Seconds)> played) {
            _played = played;
        }

        /// <remarks>
        ///     The registry belongs to the invocation, not to this instance, which is what the annotation
        ///     asserts. The same quarter is reported to three societies, and none of them is *the* registry
        ///     for this report.
        ///     <para>
        ///         The way to break it is to hold on to what arrives here — assign it to a field, cache it
        ///         "to avoid passing it around" — and the code will compile, pass its tests, and report the
        ///         archive's tracks to the domestic society for the rest of the year.
        ///     </para>
        /// </remarks>
        [MethodInjection]
        public decimal TotalFor(IRightsRegistry registry) {
            decimal total = 0m;
            foreach ((string trackId, int seconds) in _played) {
                total += registry.RoyaltyFor(trackId, seconds);
            }

            return total;
        }

    }

}
