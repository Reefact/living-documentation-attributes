#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.OptimisticOfflineLockSample {

    // A hospital's shift roster — the domain for the four offline concurrency patterns, because it is a
    // real case of two people editing one thing across several requests, with a database transaction
    // spanning none of it.
    //
    // The ward manager opens next month's roster at 09:00 and works on it until 09:40. The deputy opens the
    // same roster at 09:20. Both press save. Whatever the database does inside each save, the conflict
    // happened between the reads, and no transaction was open long enough to see it.
    //
    // An OPTIMISTIC OFFLINE LOCK detects it at commit: the update carries the version that was read, and
    // updates nothing if the row no longer has it.
    //
    // It suits this because conflicts are rare — two managers editing one ward's roster at once happens
    // perhaps monthly — and because nobody waits: the deputy is not blocked from opening it.
    //
    // The cost falls entirely on the loser, and it is not the failed save. It is forty minutes of work
    // already done, discovered at the end. Which is why the message matters more than the mechanism, and
    // why a system that only says "concurrency error" has implemented the pattern and missed the point.
    //
    // The version field is annotated because everything rests on it. An UPDATE that forgets to include it
    // in the WHERE clause turns the whole pattern off — and nothing fails, no test goes red, and the loss
    // is silent. Naming the member is what lets a reviewer or a rule check every statement that writes it.

    /// <summary>
    ///     A month of shifts for one ward, edited across many requests by one person at a time — usually.
    /// </summary>
    [OptimisticOfflineLock.OptimisticOfflineLock]
    public sealed class WardRoster {

        [IdentityField]
        public long Id { get; set; }

        /// <summary>
        ///     What the check is made against.
        /// </summary>
        /// <remarks>
        ///     Every UPDATE must carry this in its WHERE clause. One that does not disables the pattern
        ///     silently — there is no error to see, only a lost roster.
        /// </remarks>
        [OptimisticOfflineLock.VersionField]
        public int Version { get; set; }

        public string Ward { get; set; } = "";

        public IList<string> Shifts { get; } = new List<string>();

    }

}
