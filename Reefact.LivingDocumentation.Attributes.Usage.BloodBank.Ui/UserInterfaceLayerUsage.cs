#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;
using Reefact.LivingDocumentation.Attributes.Usage.BloodBank.Application.LayeredArchitectureSample;

#endregion

// The blood establishment, fourth of four assemblies. The story is in
// BloodBank.Domain/LayeredArchitectureUsage.cs.
//
// This is the USER INTERFACE LAYER: it shows what happened and interprets what the operator did. Everything
// it shows was decided below it.
//
// The screen below is the shape the pattern asks for and it looks almost empty, which is the point. It reads
// a reference and a hospital, calls the application layer, and prints the sentence it gets back. It does not
// know that a unit expires after thirty-five days, and it does not know that an already-issued unit cannot be
// issued twice.
//
// The counter clerk is not the only caller. The same establishment issues units from an overnight batch and
// from a transfer import, and neither goes through this assembly. A rule that lived here would hold for one
// of the three — which is the failure mode this layer exists to make impossible rather than unlikely.
//
// The annotation is also what an architecture rule needs in order to state the interesting prohibition: this
// assembly must not reach past the application layer into the domain. Reaching straight for a BloodUnit to
// render a field is the reference that starts the erosion, and it is invisible in review because it is one
// line and it works.

[assembly: LayeredArchitecture.UserInterface]

namespace Reefact.LivingDocumentation.Attributes.Usage.BloodBank.Ui.LayeredArchitectureSample {

    /// <summary>
    ///     The counter screen: two fields, one button, and no idea what makes an issue legitimate.
    /// </summary>
    public sealed class IssueUnitScreen {

        private readonly IssueUnitService _service;

        public IssueUnitScreen(IssueUnitService service) {
            _service = service;
        }

        public string Reference { get; set; } = "";
        public string Hospital  { get; set; } = "";

        /// <summary>
        ///     What the button does — and the whole of what this layer decides.
        /// </summary>
        public string Submit(DateTime on) {
            if (Reference.Length == 0) { return "Enter a unit reference."; }

            return _service.Issue(Reference, Hospital, on);
        }

    }

}
