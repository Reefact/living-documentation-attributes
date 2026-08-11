#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

namespace DesignPatternCatalog.Usage.DomainDrivenDesign.ServiceSample {

    // Hospital pharmacy: checking a prescription against what the patient is already taking.
    //
    // The question — may these be dispensed together — belongs to no object in the model, and the
    // attempts to give it one are all worse than the service.
    //
    // Put it on the drug, and `warfarin.InteractsWith(aspirin)` makes one of the two the subject when
    // the interaction is symmetric, and a drug now needs to know the whole formulary. Put it on the
    // prescription, and the prescription grows a dependency on an interaction database in order to
    // answer a question that is not about it. Put it on the patient, and everything ends up there
    // eventually.
    //
    // A domain service is what is left when an operation is genuinely an operation and not a thing:
    // it is named in the ubiquitous language — the pharmacists say "run the interaction check" — it
    // takes domain objects and returns domain objects, and it holds no state between calls, because
    // there is nothing it would be the state *of*.
    //
    // The line to watch is the one with the application service on the other side. This one is
    // domain: the rule it applies is clinical, and a pharmacist would recognise it. Loading the
    // patient's file, writing the audit trail and sending the alert are not clinical, and they do
    // not belong here.

    [ValueObject]
    public readonly record struct Substance(string InternationalName);

    public sealed record InteractionFinding(Substance Left, Substance Right, string Severity);

    [Service]
    public interface IInteractionCheck {

        // Neither substance is the subject: the operation is about the pair, which is precisely why
        // it could not sit on either of them.
        IReadOnlyList<InteractionFinding> Between(IReadOnlyList<Substance> prescribed, IReadOnlyList<Substance> current);

    }

    [Service]
    public sealed class InteractionCheck : IInteractionCheck {

        private static readonly (string Left, string Right, string Severity)[] Known = {
            ("warfarin", "acetylsalicylic acid", "major"),
            ("simvastatin", "clarithromycin", "major"),
            ("metformin", "iodinated contrast", "moderate")
        };

        public IReadOnlyList<InteractionFinding> Between(IReadOnlyList<Substance> prescribed, IReadOnlyList<Substance> current) {
            List<InteractionFinding> findings = new();

            foreach (Substance candidate in prescribed) {
                foreach (Substance taken in current) {
                    foreach ((string left, string right, string severity) in Known) {
                        bool matches = (candidate.InternationalName == left && taken.InternationalName == right)
                                    || (candidate.InternationalName == right && taken.InternationalName == left);

                        if (matches) { findings.Add(new InteractionFinding(candidate, taken, severity)); }
                    }
                }
            }

            return findings;
        }

    }

}
