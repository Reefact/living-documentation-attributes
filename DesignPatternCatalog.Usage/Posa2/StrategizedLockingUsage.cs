#region Usings declarations

using System.Collections.Generic;
using System.Threading;

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.StrategizedLockingSample {

    // The season-ticket index is read by the eighteen gate threads on a match day and rebuilt overnight
    // by a single-threaded job that has the stadium to itself. Two use-cases, one body of code: look up
    // a card number, decide whether it is valid for this fixture, count the swipe.
    //
    // It began as two classes, one locked and one not, and they drifted. A fix to the rebate rule went
    // into the gate copy in March and into the overnight copy in June, and for three months the report
    // and the turnstiles disagreed about how many people were in the ground.
    //
    // STRATEGIZED LOCKING leaves one class and makes the lock the thing that varies. The overnight job
    // configures the null lock, whose acquire and release do nothing, and pays nothing for it.

    /// <summary>
    ///     One member of the family of locking strategies the index can be configured with.
    /// </summary>
    /// <remarks>
    ///     Every member answers the same acquire, which is the whole of what makes them interchangeable.
    ///     The role is annotated here, on the declaration that introduces it, rather than on each
    ///     implementation — the type graph already says which classes implement it.
    /// </remarks>
    [StrategizedLocking.LockingStrategy]
    public interface IAdmissionLock {

        IDisposable Acquire();

    }

    /// <summary>
    ///     The index of season tickets, with its synchronization configured into it.
    /// </summary>
    /// <remarks>
    ///     There is one implementation of this class, and that is the point: a fix applied here reaches
    ///     every configuration. The two hand-copied classes it replaced are why the rebate rule was wrong
    ///     in one of them for three months.
    /// </remarks>
    [StrategizedLocking.Component]
    public sealed class SeasonTicketIndex {

        private readonly IAdmissionLock                _lock;
        private readonly Dictionary<string, int> _swipes = new Dictionary<string, int>();

        public SeasonTicketIndex(IAdmissionLock admissionLock) {
            _lock = admissionLock;
        }

        public int RecordSwipe(string cardNumber) {
            using (_lock.Acquire()) {
                _swipes.TryGetValue(cardNumber, out int swipes);
                swipes++;
                _swipes[cardNumber] = swipes;

                return swipes;
            }
        }

    }

    /// <summary>
    ///     The strategy the gate threads are configured with on a match day.
    /// </summary>
    public sealed class MutexAdmissionLock : IAdmissionLock {

        private readonly object _gate = new object();

        public IDisposable Acquire() {
            Monitor.Enter(_gate);

            return new Release(_gate);
        }

        private sealed class Release : IDisposable {

            private readonly object _gate;

            public Release(object gate) {
                _gate = gate;
            }

            public void Dispose() {
                Monitor.Exit(_gate);
            }

        }

    }

    /// <summary>
    ///     The strategy the overnight rebuild is configured with: it does nothing, on purpose.
    /// </summary>
    /// <remarks>
    ///     A single-threaded job pays no synchronization at all while running the same code the gates run.
    ///     Its acquire returns a disposable that does nothing, which the runtime is free to make free.
    /// </remarks>
    public sealed class NullAdmissionLock : IAdmissionLock {

        private static readonly IDisposable Nothing = new NoRelease();

        public IDisposable Acquire() {
            return Nothing;
        }

        private sealed class NoRelease : IDisposable {

            public void Dispose() { }

        }

    }

}
