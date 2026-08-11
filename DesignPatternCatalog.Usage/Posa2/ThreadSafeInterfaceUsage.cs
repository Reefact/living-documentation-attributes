#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.ThreadSafeInterfaceSample {

    // The register of who is inside the ground answers two questions from the gate threads: admit this
    // card, and move this holder from one stand to another. Moving is admitting somewhere else and
    // releasing here, so the obvious implementation of Transfer called Admit and Release.
    //
    // On a recursive lock that works and costs three acquisitions where one would do. On the plain lock
    // the register actually holds, the first call froze the thread and, within a minute, every gate
    // thread queued behind it. The stadium opened its gates by hand.
    //
    // THREAD-SAFE INTERFACE splits the methods in two and puts the lock on the border. Interface methods
    // check; implementation methods trust.

    /// <summary>
    ///     Who is inside the ground, by stand.
    /// </summary>
    /// <remarks>
    ///     The lock is taken at the border of this class and nowhere within it. Every public method here
    ///     acquires and forwards; every private one assumes the lock is already held. A method belonging
    ///     to neither side is an omission, and the annotations are what make that visible.
    /// </remarks>
    [ThreadSafeInterface.Component]
    public sealed class AdmissionRegister {

        private readonly object                   _lock   = new object();
        private readonly Dictionary<string, string> _stands = new Dictionary<string, string>();

        /// <summary>
        ///     Admits a card holder to a stand.
        /// </summary>
        [ThreadSafeInterface.InterfaceMethod]
        public bool Admit(string cardNumber, string stand) {
            lock (_lock) { return AdmitCore(cardNumber, stand); }
        }

        /// <summary>
        ///     Records a card holder leaving the ground.
        /// </summary>
        [ThreadSafeInterface.InterfaceMethod]
        public void Release(string cardNumber) {
            lock (_lock) { ReleaseCore(cardNumber); }
        }

        /// <summary>
        ///     Moves a card holder from whichever stand they are in to another one.
        /// </summary>
        /// <remarks>
        ///     This is the method that used to call <see cref="Admit" /> and <see cref="Release" />, and
        ///     deadlocked on the second acquisition of a lock it already held. It takes the lock once and
        ///     forwards to implementation methods, which is the same work with one acquisition instead of
        ///     three.
        /// </remarks>
        [ThreadSafeInterface.InterfaceMethod]
        public bool Transfer(string cardNumber, string stand) {
            lock (_lock) {
                ReleaseCore(cardNumber);

                return AdmitCore(cardNumber, stand);
            }
        }

        /// <summary>
        ///     How many holders a stand currently has.
        /// </summary>
        [ThreadSafeInterface.InterfaceMethod]
        public int CountIn(string stand) {
            lock (_lock) { return CountInCore(stand); }
        }

        /// <remarks>
        ///     Trusts that the lock is held. It never acquires it, and it never calls an interface method —
        ///     one call back across the border is the self-deadlock this class was rewritten to remove, and
        ///     nothing in the type system says so.
        /// </remarks>
        [ThreadSafeInterface.ImplementationMethod]
        private bool AdmitCore(string cardNumber, string stand) {
            if (_stands.ContainsKey(cardNumber)) { return false; }

            _stands[cardNumber] = stand;

            return true;
        }

        /// <remarks>
        ///     Trusts that the lock is held.
        /// </remarks>
        [ThreadSafeInterface.ImplementationMethod]
        private void ReleaseCore(string cardNumber) {
            _stands.Remove(cardNumber);
        }

        /// <remarks>
        ///     Trusts that the lock is held.
        /// </remarks>
        [ThreadSafeInterface.ImplementationMethod]
        private int CountInCore(string stand) {
            int count = 0;
            foreach (KeyValuePair<string, string> entry in _stands) {
                if (entry.Value == stand) { count++; }
            }

            return count;
        }

    }

}
