#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.PessimisticOfflineLockSample {

    // Hospital roster: the published rota, where discovering a conflict at the end is not acceptable.
    //
    // Once a month's roster is published, changes go through a swap process: two named nurses, an approval,
    // and a notification to payroll. Redoing that at the end because someone else was editing is not forty
    // minutes lost — it is a swap that was communicated to two people and must now be uncommunicated.
    //
    // A PESSIMISTIC OFFLINE LOCK prevents the conflict instead of detecting it: only one editor holds the
    // roster, and the second is told so before doing any work.
    //
    // It is chosen where a late conflict costs more than waiting, which is exactly the opposite of the
    // trade in OptimisticOfflineLockUsage.cs — and it brings back the problems a database transaction was
    // quietly solving.
    //
    // A lock across requests is a lock a user can walk away from. So it needs a timeout, and therefore a
    // decision about how long is too long; an owner, so that whoever holds it can be named to whoever
    // wants it; and a way to break it, because the alternative is a ward whose roster nobody can edit
    // because a nurse closed her laptop at 17:00 on a Friday.
    //
    // All three are on the interface below. A pessimistic lock without them is not a simpler version of
    // this pattern — it is the same pattern with its failure modes left to the operations team.

    /// <summary>
    ///     Exclusive rights to edit something, held across requests rather than inside a transaction.
    /// </summary>
    [PessimisticOfflineLock]
    public interface IRosterLock {

        bool TryAcquire(long rosterId, string owner, TimeSpan expiresAfter);

        /// <summary>Who holds it, so the second editor is told something useful rather than "locked".</summary>
        string? HeldBy(long rosterId);

        void Release(long rosterId, string owner);

        /// <summary>The way out of a lock whose owner went home. Audited, because it discards their work.</summary>
        void Break(long rosterId, string brokenBy, string reason);

    }

}
