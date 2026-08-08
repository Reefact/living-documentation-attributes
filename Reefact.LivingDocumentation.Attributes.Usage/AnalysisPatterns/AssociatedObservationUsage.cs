#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.AssociatedObservationSample {

    // A district heating operator's plant monitoring. Boiler efficiency is not measured — nothing on the plant
    // reads it. It is derived: fuel flow, flow temperature, return temperature and mass flow go in, a number
    // comes out, and that number is what the operator is judged on.
    //
    // The tempting model stores the number. It fails on the day one of the inputs is found to have been wrong:
    // the flow meter was reading three per cent high for a fortnight, the readings are corrected, and nothing
    // knows which efficiency figures need recomputing — or which of them were derived at all.
    //
    // ASSOCIATED OBSERVATION derives the observation and keeps what it was derived from. Figure 3.14 puts the
    // function at the knowledge level with its argument concepts and its product concept, so a new derived
    // measure is configured rather than written; and it keeps the evidence at the operational level, so a
    // derivation can be re-explained instead of merely trusted.
    //
    // Both halves earn their keep, and the evidence is the one that gets dropped. It is always tempting to store
    // only the result — and a stored result whose inputs are gone cannot be rechecked when one of them turns out
    // to have been rejected.

    /// <summary>
    ///     The rule that produces one observation concept from others.
    /// </summary>
    /// <remarks>
    ///     Knowledge level. Stating the arguments and the product as concepts rather than as a method signature
    ///     is what lets a new derived measure be added without a release.
    /// </remarks>
    [AssociatedObservation.AssociativeFunction]
    public sealed class DerivationRule {

        private readonly Func<IReadOnlyList<PlantReading>, decimal> _compute;
        private readonly List<string>                               _arguments;

        public DerivationRule(string product, IEnumerable<string> arguments,
                              Func<IReadOnlyList<PlantReading>, decimal> compute) {
            Product    = product;
            _arguments = new List<string>(arguments);
            _compute   = compute;
        }

        /// <summary>What it produces: "boiler efficiency".</summary>
        public string Product { get; }

        /// <summary>What it needs, by concept.</summary>
        public IReadOnlyList<string> Arguments => _arguments;

        /// <summary>
        ///     Applies the rule, refusing readings that do not match its arguments. The check is the reason the
        ///     arguments are data: a derivation fed the wrong concepts produces a plausible number.
        /// </summary>
        public DerivedReading Apply(IReadOnlyList<PlantReading> evidence, DateTime at) {
            HashSet<string> given = new();
            foreach (PlantReading reading in evidence) {
                given.Add(reading.Concept);
            }

            foreach (string argument in _arguments) {
                if (!given.Contains(argument)) {
                    throw new InvalidOperationException($"{Product} needs {argument}, which was not supplied");
                }
            }

            return new DerivedReading(this, _compute(evidence), evidence, at);
        }

    }

    /// <summary>
    ///     A reading taken directly from the plant.
    /// </summary>
    public sealed class PlantReading {

        public PlantReading(string concept, decimal amount, string unit, DateTime at) {
            Concept = concept;
            Amount  = amount;
            Unit    = unit;
            At      = at;
        }

        /// <summary>What was read.</summary>
        public string Concept { get; }

        /// <summary>The amount.</summary>
        public decimal Amount { get; }

        /// <summary>Its unit.</summary>
        public string Unit { get; }

        /// <summary>When.</summary>
        public DateTime At { get; }

        /// <summary>Whether the reading has since been withdrawn.</summary>
        public bool Rejected { get; private set; }

        /// <summary>Withdraws it — the event that makes the evidence link matter.</summary>
        public void Reject() {
            Rejected = true;
        }

    }

    /// <summary>
    ///     An observation produced by a rule rather than taken from the plant.
    /// </summary>
    /// <remarks>
    ///     Keeps its evidence, so it can be re-explained and, more to the point, re-checked.
    /// </remarks>
    [AssociatedObservation.AssociatedObservation(AssociativeFunction = typeof(DerivationRule))]
    public sealed class DerivedReading {

        private readonly List<PlantReading> _evidence;

        internal DerivedReading(DerivationRule rule, decimal amount, IEnumerable<PlantReading> evidence, DateTime at) {
            Rule      = rule;
            Amount    = amount;
            At        = at;
            _evidence = new List<PlantReading>(evidence);
        }

        /// <summary>The rule that produced it.</summary>
        public DerivationRule Rule { get; }

        /// <summary>The derived value.</summary>
        public decimal Amount { get; }

        /// <summary>When it was derived.</summary>
        public DateTime At { get; }

        /// <summary>
        ///     What it was derived from, retained rather than consumed.
        /// </summary>
        [AssociatedObservation.Evidence]
        public IReadOnlyList<PlantReading> Evidence => _evidence;

        /// <summary>
        ///     Whether this figure now rests on a withdrawn reading. Answerable only because the evidence was
        ///     kept, and the whole reason to keep it.
        /// </summary>
        public bool RestsOnWithdrawnEvidence {
            get {
                foreach (PlantReading reading in _evidence) {
                    if (reading.Rejected) {
                        return true;
                    }
                }

                return false;
            }
        }

    }

}
