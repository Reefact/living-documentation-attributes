#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.DatabaseSessionStateSample {

    // Enrolment portal: the third answer, and the one clearing week actually needed.
    //
    // DATABASE SESSION STATE puts the session where every node can read it. The affinity that server
    // session state imposes disappears — any of the six nodes serves any request, a node can be taken out
    // mid-application, and nobody starts again.
    //
    // The price is a read and a write per request. That is not nothing at clearing volumes, and it is the
    // reason this is a decision rather than a default.
    //
    // The part that is usually discovered late is the second half: session data now needs the same care as
    // the rest of the schema. It has a shape that must be migrated when it changes. It shows up in backups
    // and therefore in the retention policy — an abandoned application is personal data. And it must be
    // CLEANED UP, which nothing else in this family requires: a cookie expires by itself and a process
    // forgets when it stops, but a table grows until something deletes from it.
    //
    // That is why the interface below has an expiry and a sweep on it. A database session store without
    // them is a table that is fine for a year and then is the largest object in the database.

    /// <summary>
    ///     Sessions in the database, readable by every node.
    /// </summary>
    [DatabaseSessionState]
    public interface ISessionTable {

        string? Read(string sessionToken);

        void Write(string sessionToken, string payload, DateTimeOffset expiresAt);

        /// <summary>
        ///     Not optional: nothing else removes what this pattern accumulates.
        /// </summary>
        int DeleteExpired(DateTimeOffset asOf);

    }

}
