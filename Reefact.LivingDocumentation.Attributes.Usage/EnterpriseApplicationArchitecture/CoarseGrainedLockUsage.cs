#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.CoarseGrainedLockSample {

    // Hospital roster: why locking each shift separately is not merely slower — it is wrong.
    //
    // A roster is a month of shifts, and the rules that matter run ACROSS them: no nurse works more than
    // three nights in a row, every night has a senior on it, nobody exceeds their contracted hours. Those
    // are invariants of the whole roster, not of any shift.
    //
    // Lock shift by shift and two editors can each hold a valid lock, each make a change that is fine on
    // its own, and produce a roster that breaks the three-nights rule between them. Both locks were
    // honoured. The invariant was not.
    //
    // A COARSE GRAINED LOCK covers the group that changes together — here the roster, which is the root of
    // it — so the set is taken as one.
    //
    // That is the argument worth keeping: this pattern is usually presented as an optimisation, and it is
    // one, but the reason it is not optional is correctness. A set of objects with an invariant between
    // them must be locked as a unit, or the invariant can be broken between two perfectly legal locks.
    //
    // The lock lives on the root and the members carry none, which is what the annotation records: a
    // reader finding no lock on Shift should conclude that it is covered, not that it was forgotten.

    /// <summary>
    ///     The root that carries the lock for everything under it.
    /// </summary>
    [CoarseGrainedLock]
    public sealed class MonthlyRoster {

        [IdentityField]
        public long Id { get; set; }

        [OptimisticOfflineLock.VersionField]
        public int Version { get; set; }

        public IList<Shift> Shifts { get; } = new List<Shift>();

    }

    /// <summary>
    ///     A member of the group. Deliberately holds no lock and no version of its own.
    /// </summary>
    public sealed class Shift {

        public DateOnly Date  { get; set; }
        public string   Nurse { get; set; } = "";
        public bool     IsNight { get; set; }

    }

}
