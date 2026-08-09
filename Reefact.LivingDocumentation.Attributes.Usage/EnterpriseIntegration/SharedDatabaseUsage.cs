#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.SharedDatabaseSample {

    // The terminal operating system and the gate kiosks read one schema. A truck arriving at the gate must see
    // the same booking the yard planner saw thirty seconds ago, and no copy of that booking would be current
    // enough.
    //
    // SHARED DATABASE removes the transfer entirely — there is nothing to fall out of step because there is
    // one copy. What it costs is that the schema becomes a contract, and altering a column is altering both
    // applications at once.

    /// <summary>
    ///     Reads bookings straight from the schema the yard planner writes.
    /// </summary>
    /// <remarks>
    ///     Consistency comes free. The price is that this table can no longer be changed by one team alone.
    /// </remarks>
    [SharedDatabase]
    public sealed class GateBookingLookup {

        public string? FindBooking(string truckPlate) {
            // ... SELECT against the shared bookings table
            return null;
        }

    }
}
