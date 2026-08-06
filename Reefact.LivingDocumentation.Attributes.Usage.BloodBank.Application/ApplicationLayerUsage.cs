#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;
using Reefact.LivingDocumentation.Attributes.Usage.BloodBank.Domain.LayeredArchitectureSample;

#endregion

// The blood establishment, second of four assemblies. The story is in
// BloodBank.Domain/LayeredArchitectureUsage.cs.
//
// This is the APPLICATION LAYER, and the pattern's instruction about it is a restraint rather than a
// capability: keep it thin. It opens the transaction, finds the unit, tells the unit to be issued, saves,
// and says what happened. Every one of those is coordination.
//
// What makes it worth naming is how easily it stops being thin. Read the method below and notice how natural
// it would be to write `if (on > unit.ExpiresOn)` right here — it is one line, it gives a nicer error
// message, and the screen would show it sooner. It is also the first line of a second model, one that the
// overnight batch and the transfer import do not share, and once two rules live here nobody can say which
// layer decides anything.
//
// So the layer states WHAT THE SYSTEM DOES — issue a unit to a hospital — and never what the business is.
// That sentence is the test to apply to anything proposed for this assembly.
//
// The dependencies say the same thing in a form a build can check: this references the domain and the domain
// references nothing. It does not reference infrastructure either; it asks for a store and is handed one.

[assembly: LayeredArchitecture.Application]

namespace Reefact.LivingDocumentation.Attributes.Usage.BloodBank.Application.LayeredArchitectureSample {

    /// <summary>
    ///     Issuing a unit, as an operation the system offers — not as a rule of the domain.
    /// </summary>
    public sealed class IssueUnitService {

        private readonly IBloodUnitStore _store;

        public IssueUnitService(IBloodUnitStore store) {
            _store = store;
        }

        /// <summary>
        ///     Finds the unit, asks it to be issued, and records the outcome.
        /// </summary>
        /// <remarks>
        ///     No expiry check here, deliberately. The one in <see cref="BloodUnit.IssueTo" /> is the only one,
        ///     and a second would be the beginning of a second model.
        /// </remarks>
        public string Issue(string reference, string hospital, DateTime on) {
            BloodUnit? unit = _store.Find(reference);
            if (unit is null) { return $"No unit {reference}."; }

            try {
                unit.IssueTo(hospital, on);
                _store.Save(unit);

                return $"Unit {reference} issued to {hospital}.";
            } catch (InvalidOperationException refused) {
                return refused.Message;
            }
        }

    }

}
