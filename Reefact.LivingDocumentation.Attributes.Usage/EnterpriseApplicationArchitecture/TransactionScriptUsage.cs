#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.TransactionScriptSample {

    // Municipal parking permits — the domain shared by the four patterns that answer one question: where
    // does business logic live? Transaction script, table module, domain model and service layer are
    // alternatives, and putting them on one domain is the only way to see what separates them.
    //
    // A TRANSACTION SCRIPT is one procedure per request, start to finish. The whole of "issue a resident
    // permit" is below: check the address is in the zone, check the vehicle is not already permitted, take
    // the fee, write the permit, return the number.
    //
    // It is the right choice here and worth saying why. Permit issuance has perhaps four rules, they have
    // not changed since the by-law was written, and the council's team is two people. A domain model for
    // this would be more moving parts than the problem has.
    //
    // The trade is real and shows the moment a second script appears. "Issue a visitor permit" shares the
    // zone check — and shares it by COPYING, because there is no object for a zone to hang the rule on.
    // Two copies is tolerable; the pattern is being outgrown by the time there are six, and that is the
    // signal to move to a table module or a domain model rather than a sign that this was a mistake.
    //
    // Annotated on the method, not the class: the pattern is the procedure. The class around it is a
    // container, and calling the class a transaction script would say that every method on it is one.

    /// <summary>
    ///     The council's permit desk, as scripts.
    /// </summary>
    public sealed class PermitDesk {

        /// <summary>
        ///     Issues a resident permit, from validation to receipt.
        /// </summary>
        [TransactionScript]
        public string IssueResidentPermit(string postcode, string registration, decimal feePaid) {
            if (!postcode.StartsWith("CB", StringComparison.Ordinal)) { throw new InvalidOperationException("Address is outside the permit zone."); }
            if (feePaid < 60m) { throw new InvalidOperationException("The annual resident fee is £60."); }

            return $"RES-{registration}-{DateOnly.FromDateTime(DateTime.UtcNow):yyyy}";
        }

        /// <summary>
        ///     Issues a visitor permit — and repeats the zone check, which is the pattern's cost showing.
        /// </summary>
        [TransactionScript]
        public string IssueVisitorPermit(string postcode, int days) {
            if (!postcode.StartsWith("CB", StringComparison.Ordinal)) { throw new InvalidOperationException("Address is outside the permit zone."); }
            if (days > 14) { throw new InvalidOperationException("A visitor permit runs for at most 14 days."); }

            return $"VIS-{days}D-{DateOnly.FromDateTime(DateTime.UtcNow):yyyyMMdd}";
        }

    }

}
