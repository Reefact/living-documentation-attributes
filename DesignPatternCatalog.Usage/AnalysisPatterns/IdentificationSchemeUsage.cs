#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.IdentificationSchemeSample {

    // A hospital's patient index. One patient is known by an NHS number, a hospital number issued locally, a
    // second hospital number from the trust they were transferred from, a radiology accession, and a trial
    // subject code that deliberately does not identify them to the sponsor.
    //
    // Figure 5.3 is the model everyone writes first: one id, unique. It asserts a single issuer and global
    // uniqueness, and the day a second issuer exists that assertion is false with nothing failing. Figure 5.4
    // adds the scheme, and with it the two things the single field cannot say: that a patient has several
    // identifiers with no privileged one, and that each is unique only where it was issued.
    //
    // The second is the assertion worth checking, and it is the one that produces real harm. Two schemes may
    // perfectly well issue the same string — a hospital number "1234567" exists in both trusts — so a lookup
    // that ignores the scheme does not fail to find a patient. It finds the wrong one.

    /// <summary>
    ///     Who issues identifiers, and within what they are unique.
    /// </summary>
    [IdentificationScheme.IdentificationScheme]
    public sealed class Scheme {

        public Scheme(string name, string issuer) {
            Name   = name;
            Issuer = issuer;
        }

        /// <summary>NHS number, hospital number, trial subject code.</summary>
        public string Name { get; }

        /// <summary>Who issues them, which is the scope uniqueness is measured in.</summary>
        public string Issuer { get; }

    }

    /// <summary>
    ///     One identifier of one patient, within one scheme.
    /// </summary>
    /// <remarks>
    ///     Equality includes the scheme. Comparing the value alone is the lookup that finds the wrong patient.
    /// </remarks>
    [IdentificationScheme.Identifier(IdentificationScheme = typeof(Scheme))]
    public sealed class PatientIdentifier {

        public PatientIdentifier(Scheme scheme, string value) {
            Scheme = scheme;
            Value  = value;
        }

        /// <summary>The scheme it belongs to.</summary>
        public Scheme Scheme { get; }

        /// <summary>The identifier as issued.</summary>
        public string Value { get; }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return obj is PatientIdentifier other
                && ReferenceEquals(other.Scheme, Scheme)
                && other.Value == Value;
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return System.HashCode.Combine(Scheme, Value);
        }

    }

    /// <summary>
    ///     A patient, known by several identifiers and privileging none.
    /// </summary>
    [Party]
    public sealed class Patient {

        private readonly List<PatientIdentifier> _identifiers = new();

        /// <summary>Every identifier the patient is known by.</summary>
        public IReadOnlyList<PatientIdentifier> Identifiers => _identifiers;

        /// <summary>Records an identifier.</summary>
        public void KnownAs(PatientIdentifier identifier) {
            _identifiers.Add(identifier);
        }

        /// <summary>
        ///     The patient's identifier within one scheme, absent when they have none there — which is ordinary
        ///     rather than exceptional: a patient who has never been in a trial has no subject code.
        /// </summary>
        public PatientIdentifier? In(Scheme scheme) {
            foreach (PatientIdentifier identifier in _identifiers) {
                if (ReferenceEquals(identifier.Scheme, scheme)) {
                    return identifier;
                }
            }

            return null;
        }

    }

    /// <summary>
    ///     The index, keyed the only way that is safe.
    /// </summary>
    public sealed class PatientIndex {

        private readonly Dictionary<PatientIdentifier, Patient> _byIdentifier = new();

        /// <summary>Registers a patient under one of their identifiers.</summary>
        public void Register(PatientIdentifier identifier, Patient patient) {
            _byIdentifier[identifier] = patient;
        }

        /// <summary>
        ///     Finds a patient. The scheme is a required argument, not a default — that signature is what the
        ///     annotation is for, and a lookup taking a bare string is the one that finds the wrong patient.
        /// </summary>
        public Patient? Find(Scheme scheme, string value) {
            return _byIdentifier.TryGetValue(new PatientIdentifier(scheme, value), out Patient? found)
                ? found
                : null;
        }

    }

}
