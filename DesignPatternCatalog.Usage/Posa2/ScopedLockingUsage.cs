#region Usings declarations

using System.Threading;

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.ScopedLockingSample {

    // Eighteen turnstiles admit spectators into one stadium process, and they share a count that decides
    // when a stand must close. The licensed capacity of a stand is not a service-level objective; going
    // over it is a criminal matter, so the count is taken under a lock and nobody argues about the cost.
    //
    // The version that acquired and released explicitly survived four years. It died on the fifth
    // failure branch somebody added — an early return above the release, in a case that fires perhaps
    // twice a season, which is exactly as often as it takes to never see it in testing. The next full
    // house queued outside a gate whose lock was never given back.
    //
    // SCOPED LOCKING moves the release out of the author's hands: the guard is constructed, and the
    // release is what leaving the scope means.

    /// <summary>
    ///     How many spectators are inside a stand, and whether one more may enter.
    /// </summary>
    public sealed class StandOccupancy {

        /// <remarks>
        ///     Taken through <see cref="AdmissionScope" /> and never directly. That is the claim the
        ///     annotation makes: a bare <c>Monitor.Enter</c> on this field elsewhere in the type is a breach
        ///     of the pattern rather than a difference of style, and it is the shape the guard exists to
        ///     make unnecessary.
        /// </remarks>
        [ScopedLocking.Lock]
        private readonly object _turnstiles = new object();

        private readonly int _licensedCapacity;

        private int _inside;

        public StandOccupancy(int licensedCapacity) {
            _licensedCapacity = licensedCapacity;
        }

        /// <summary>
        ///     Admits one spectator if the stand has room, and says whether it did.
        /// </summary>
        public bool TryAdmit() {
            using (new AdmissionScope(_turnstiles)) {
                if (_inside >= _licensedCapacity) {
                    // The early return that used to skip the release. It cannot now.
                    return false;
                }

                _inside++;

                return true;
            }
        }

        /// <summary>
        ///     Records a spectator leaving through an exit gate.
        /// </summary>
        public void Release() {
            using (new AdmissionScope(_turnstiles)) {
                if (_inside > 0) { _inside--; }
            }
        }

    }

    /// <summary>
    ///     Holds the turnstile lock for the length of one admission.
    /// </summary>
    /// <remarks>
    ///     The constructor acquires and <see cref="Dispose" /> releases, so every way out of the scope
    ///     releases: a return, a thrown exception, a branch written by somebody who never read this class.
    ///     What the idiom cannot cover is a path that leaves without unwinding at all — killing the thread
    ///     inside the scope keeps the lock, which is why nothing in the stadium process does that.
    /// </remarks>
    [ScopedLocking.Guard]
    public sealed class AdmissionScope : IDisposable {

        private readonly object _lock;

        public AdmissionScope(object turnstiles) {
            _lock = turnstiles;
            Monitor.Enter(_lock);
        }

        public void Dispose() {
            Monitor.Exit(_lock);
        }

    }

}
