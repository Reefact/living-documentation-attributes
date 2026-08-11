#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.PartyTypeGeneralizationsSample {

    // A national research funder. Eligibility is written by policy people, in their language: "research
    // organisations may lead a consortium", "small businesses may claim the higher intervention rate",
    // "charities are exempt from the matched-funding requirement".
    //
    // None of those sentences names an applicant. They name a KIND of applicant — and the kinds are not flat.
    // A university is a research organisation. So is an institute, and so is a hospital trust running a
    // clinical research facility. A spin-out is a small business and also, for two of the schemes, a research
    // organisation. The kinds arrive faster than releases do: the department invents "public sector research
    // establishment" in a consultation and expects it to work by the next call.
    //
    // PARTY TYPE GENERALIZATIONS puts that structure where the kinds already are, at the knowledge level, so a
    // new narrower kind is configured rather than compiled. A subclass per kind would put the same information
    // in the class hierarchy, where inventing one is a release and where the policy people cannot see it.
    //
    // The derived closure is the part that carries the risk. A rule stated for research organisations has to be
    // asked against a university's *whole* line of kinds, not its immediate one — and the version that asks the
    // immediate one is not obviously wrong. It passes for every applicant registered as exactly a research
    // organisation, which on the day the model is written is most of them. It starts excluding people the moment
    // a narrower kind is configured, and what it produces is a rejection letter that reads correct.

    /// <summary>
    ///     A kind of applicant, and itself a thing that generalises.
    /// </summary>
    /// <remarks>
    ///     The generalization lives here rather than in the class hierarchy so that a new kind is data. That is
    ///     the trade: the compiler stops helping, and the policy team stops waiting for a release.
    /// </remarks>
    [PartyTypeGeneralizations.PartyType]
    public sealed class ApplicantKind {

        public ApplicantKind(string name, ApplicantKind? fallsUnder = null) {
            Name       = name;
            FallsUnder = fallsUnder;
        }

        /// <summary>What the scheme documents call it.</summary>
        public string Name { get; }

        /// <summary>
        ///     The broader kind this one falls under, at most one, and never one already below it.
        /// </summary>
        [PartyTypeGeneralizations.Supertype]
        public ApplicantKind? FallsUnder { get; }

        /// <summary>
        ///     This kind and every kind above it. The member an eligibility rule must be asked against.
        /// </summary>
        [PartyTypeGeneralizations.AllTypes]
        public IReadOnlyList<ApplicantKind> AllKinds {
            get {
                List<ApplicantKind> all  = new();
                ApplicantKind?      kind = this;
                while (kind is not null) {
                    all.Add(kind);
                    kind = kind.FallsUnder;
                }

                return all;
            }
        }

        /// <summary>Whether this kind is, or falls under, the given one.</summary>
        public bool Is(ApplicantKind kind) {
            foreach (ApplicantKind candidate in AllKinds) {
                if (ReferenceEquals(candidate, kind)) {
                    return true;
                }
            }

            return false;
        }

    }

    /// <summary>
    ///     An organisation or a person applying for funding.
    /// </summary>
    [Party]
    public sealed class Applicant {

        public Applicant(string name, ApplicantKind kind) {
            Name = name;
            Kind = kind;
        }

        /// <summary>Its name.</summary>
        public string Name { get; }

        /// <summary>The kind it is registered as — the narrowest one, not every one it satisfies.</summary>
        public ApplicantKind Kind { get; }

    }

    /// <summary>
    ///     One sentence of scheme policy, stated about a kind.
    /// </summary>
    /// <remarks>
    ///     Asks <see cref="ApplicantKind.AllKinds" /> through <c>Is</c>, never the registered kind directly.
    ///     That is the whole difference between this working and this appearing to work.
    /// </remarks>
    public sealed class EligibilityRule {

        public EligibilityRule(string description, ApplicantKind appliesTo) {
            Description = description;
            AppliesTo   = appliesTo;
        }

        /// <summary>The sentence as policy wrote it.</summary>
        public string Description { get; }

        /// <summary>The kind it is stated about.</summary>
        public ApplicantKind AppliesTo { get; }

        /// <summary>Whether the rule reaches this applicant.</summary>
        public bool Reaches(Applicant applicant) {
            return applicant.Kind.Is(AppliesTo);
        }

    }

}
