#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.ProcessOfObservationSample {

    // A highways authority's bridge inspections. An inspector records findings — spalling on the north abutment,
    // chloride ingress at 0.4 per cent by mass of cement, a widening crack at pier three — and then records a
    // condition rating, which is what decides whether the structure stays open.
    //
    // The rating is an observation too, and it is the one that gets read. What gets lost is why. Six months
    // later, when a load assessment questions the rating, the findings are in the same table with no link to it,
    // and reconstructing which of them the inspector actually weighed is guesswork.
    //
    // PROCESS OF OBSERVATION is figure 3.8, and it is economical in a way worth noticing: it is an association
    // FROM observation TO observation, with roles named evidence and assessment. No new class. Because an
    // assessment is an observation like any other, it can itself be evidence for a further one — a condition
    // rating feeding a network-level risk assessment — and a chain of reasoning of any depth needs nothing more.
    //
    // The evidence member is what makes "why does the record say this" answerable from the model. It also
    // decides what happens when a finding is withdrawn: an assessment whose evidence has been retracted is not
    // automatically wrong, but it can no longer be left unexamined, and something has to be able to find it.

    /// <summary>
    ///     Anything an inspector records about a structure.
    /// </summary>
    [Observation.Observation]
    public abstract class InspectionObservation {

        protected InspectionObservation(string structure, string note, DateOnly on) {
            Structure = structure;
            Note      = note;
            On        = on;
        }

        /// <summary>Which structure.</summary>
        public string Structure { get; }

        /// <summary>What was recorded.</summary>
        public string Note { get; }

        /// <summary>When.</summary>
        public DateOnly On { get; }

        /// <summary>Whether it has been withdrawn.</summary>
        public bool Retracted { get; private set; }

        /// <summary>Withdraws it.</summary>
        public void Retract() {
            Retracted = true;
        }

    }

    /// <summary>
    ///     A direct finding: what the inspector saw or measured.
    /// </summary>
    public sealed class Finding : InspectionObservation {

        public Finding(string structure, string note, DateOnly on, string element)
            : base(structure, note, on) {
            Element = element;
        }

        /// <summary>Which element of the structure it concerns.</summary>
        public string Element { get; }

    }

    /// <summary>
    ///     An observation concluded from others.
    /// </summary>
    /// <remarks>
    ///     An observation like any other, so it can itself be evidence for a further assessment. That is what
    ///     makes a chain of reasoning need no new class.
    /// </remarks>
    [ProcessOfObservation.Assessment]
    public sealed class ConditionRating : InspectionObservation {

        private readonly List<InspectionObservation> _evidence;

        public ConditionRating(string structure, string note, DateOnly on, int rating,
                               IEnumerable<InspectionObservation> evidence)
            : base(structure, note, on) {
            _evidence = new List<InspectionObservation>(evidence);
            if (_evidence.Count == 0) {
                throw new ArgumentException("a condition rating must rest on something", nameof(evidence));
            }

            Rating = rating;
        }

        /// <summary>The rating, one to five.</summary>
        public int Rating { get; }

        /// <summary>
        ///     The observations it rests on — findings, or earlier assessments.
        /// </summary>
        [ProcessOfObservation.Evidence]
        public IReadOnlyList<InspectionObservation> Evidence => _evidence;

        /// <summary>
        ///     Whether any observation beneath this one, at any depth, has been retracted. The recursion is free
        ///     because an assessment is an observation.
        /// </summary>
        public bool NeedsReexamination {
            get {
                foreach (InspectionObservation observation in _evidence) {
                    if (observation.Retracted) {
                        return true;
                    }

                    if (observation is ConditionRating nested && nested.NeedsReexamination) {
                        return true;
                    }
                }

                return false;
            }
        }

    }

}
