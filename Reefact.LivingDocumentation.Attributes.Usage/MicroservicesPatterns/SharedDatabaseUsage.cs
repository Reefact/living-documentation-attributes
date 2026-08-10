#region Usings declarations

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.SharedDatabaseSample {

    // The same grid operator did not get to split everything. The connections desk and the outage map both
    // read the twenty-year-old customer information system, and a supply point's address has to be the same
    // in both at the moment an engineer is dispatched to it.
    //
    // SHARED DATABASE is what the two are stuck with, and this work names it an anti-pattern where DATABASE
    // PER SERVICE applies. Annotating it is worth more than annotating the clean half: the constraint is
    // that neither team can alter the CUSTOMER_SUPPLY table alone, and nothing in either codebase says so.

    /// <summary>
    ///     The connections desk, reading the legacy schema directly.
    /// </summary>
    /// <remarks>
    ///     Straightforward ACID against tables the outage map also writes. What it costs is a schema change
    ///     that is now two deployments and a meeting.
    /// </remarks>
    [SharedDatabase]
    public sealed class ConnectionsDesk {

        public string? AddressOf(string supplyPoint) {
            // ... SELECT ADDRESS FROM CUSTOMER_SUPPLY WHERE SUPPLY_POINT = ?
            return null;
        }

    }

    /// <summary>
    ///     The outage map, reading the same tables.
    /// </summary>
    /// <remarks>
    ///     Annotated separately because the pattern is about participants rather than about the schema: two
    ///     annotations is the count of what a migration has to move.
    /// </remarks>
    [SharedDatabase]
    public sealed class OutageMap {

        public int SupplyPointsAffectedBy(string substation) {
            // ... SELECT COUNT(*) FROM CUSTOMER_SUPPLY WHERE SUBSTATION = ?
            return 0;
        }

    }
}
