#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;
using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

// Municipal parking permits, five years on — the same domain as the transaction scripts, after the by-law
// stopped being simple.
//
// The council now has resident permits, visitor permits, carer permits, trade permits and a blue-badge
// exemption; zones overlap at three boundaries; a household's entitlement depends on how many permits it
// already holds and on whether the street is in a controlled zone at that hour. The scripts that were the
// right answer at four rules are now nine procedures sharing eleven copies of the zone check.
//
// A DOMAIN MODEL is the answer at that scale: objects carrying both the data and the rules, so a rule is
// written once, on the thing it is about.
//
// The annotation is on the ASSEMBLY because that is what the pattern qualifies. It is not a claim about any
// one class — it is a claim about how this code is organised, and about what it is NOT: not transaction
// scripts, not a table module. That makes it a statement something can be held to. A class in here that
// turns out to be a bag of properties acted on from a service is a defect against a declared intent rather
// than a matter of taste, and an architecture rule can say so.
//
// Note what it costs, because the pattern is a trade and not an upgrade: the permit rules now need a
// mapping layer to reach the tables, and the two-person team needs to understand one. That is why
// TransactionScriptUsage.cs is not written as the naive version of this — it was correct until it was not.

[assembly: DomainModel]

namespace Reefact.LivingDocumentation.Attributes.Usage.ParkingPermits.DomainModelSample {

    /// <summary>
    ///     A controlled parking zone, which now owns the rule that eleven scripts used to copy.
    /// </summary>
    [Entity]
    public sealed class ControlledZone {

        private readonly IReadOnlyCollection<string> _postcodePrefixes;

        public ControlledZone(string code, IReadOnlyCollection<string> postcodePrefixes) {
            Code              = code;
            _postcodePrefixes = postcodePrefixes;
        }

        public string Code { get; }

        public bool Covers(string postcode) {
            return _postcodePrefixes.Any(prefix => postcode.StartsWith(prefix, StringComparison.Ordinal));
        }

    }

    /// <summary>
    ///     A household's entitlement — a rule that had nowhere to live when the logic was procedural.
    /// </summary>
    [Entity]
    public sealed class Household {

        private readonly List<Permit> _permits = new();

        public Household(string postcode) {
            Postcode = postcode;
        }

        public string Postcode { get; }

        /// <summary>
        ///     Two resident permits per household, and none at all outside a controlled zone.
        /// </summary>
        public bool MayHoldAnother(ControlledZone zone) {
            return zone.Covers(Postcode) && _permits.Count(permit => permit.IsResident) < 2;
        }

        public void Grant(Permit permit) {
            _permits.Add(permit);
        }

    }

    /// <summary>
    ///     The permit itself.
    /// </summary>
    [Entity]
    public sealed class Permit {

        public Permit(string number, bool isResident, DateOnly expiresOn) {
            Number     = number;
            IsResident = isResident;
            ExpiresOn  = expiresOn;
        }

        public string   Number     { get; }
        public bool     IsResident { get; }
        public DateOnly ExpiresOn  { get; }

        public bool IsValidOn(DateOnly date) {
            return date <= ExpiresOn;
        }

    }

}
