#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.ActiveObservationSample {

    // A maternity record. Three things sit side by side in it and look identical in a database: the gestational
    // age measured at the twelve-week scan, the working diagnosis of gestational diabetes the midwife is
    // treating for while the test is pending, and the estimated date of delivery.
    //
    // The first is believed. The second is proposed. The third is expected of a future that has not happened.
    // They share a shape and share none of their standing.
    //
    // Figure 3.13 makes each a subtype of observation, and the reason to bother is what happens when they are
    // not distinguished. A working diagnosis counted in a prevalence figure inflates it, and the arithmetic
    // never objects. A projected delivery date read as a fact is a fact about next month. Both are the kind of
    // error that survives review because every individual record is defensible.
    //
    // A note on the name. Fowler's section is titled "Active Observation, Hypothesis, and Projection", whose
    // faithful PascalCase is unreadable, so the entry is catalogued as ActiveObservation with the other two as
    // roles. That departs from the convention chapter 2 set — the section title where the book has one — and it
    // is a judgement worth reversing if a reviewer disagrees.

    /// <summary>
    ///     Anything recorded about a pregnancy, with its standing.
    /// </summary>
    public abstract class MaternityObservation {

        protected MaternityObservation(string concept, string reading, DateOnly recordedOn) {
            Concept    = concept;
            Reading    = reading;
            RecordedOn = recordedOn;
        }

        /// <summary>What was observed.</summary>
        public string Concept { get; }

        /// <summary>How it reads.</summary>
        public string Reading { get; }

        /// <summary>When it was recorded.</summary>
        public DateOnly RecordedOn { get; }

        /// <summary>Whether this may be counted as fact — the question the three subtypes exist to answer.</summary>
        public abstract bool CountsAsEstablished { get; }

    }

    /// <summary>
    ///     An observation held to be true of the world.
    /// </summary>
    /// <remarks>
    ///     The only one a report should include without saying otherwise.
    /// </remarks>
    [ActiveObservation.Active]
    public sealed class Established : MaternityObservation {

        public Established(string concept, string reading, DateOnly recordedOn)
            : base(concept, reading, recordedOn) { }

        /// <inheritdoc />
        public override bool CountsAsEstablished => true;

    }

    /// <summary>
    ///     An observation proposed but not established — a working diagnosis.
    /// </summary>
    /// <remarks>
    ///     Recordable because clinicians act on hypotheses, and separate because a hypothesis counted in a
    ///     prevalence figure inflates it silently.
    /// </remarks>
    [ActiveObservation.Hypothesis]
    public sealed class WorkingDiagnosis : MaternityObservation {

        public WorkingDiagnosis(string concept, string reading, DateOnly recordedOn, string pendingTest)
            : base(concept, reading, recordedOn) {
            PendingTest = pendingTest;
        }

        /// <summary>What would settle it.</summary>
        public string PendingTest { get; }

        /// <inheritdoc />
        public override bool CountsAsEstablished => false;

    }

    /// <summary>
    ///     An observation expected of a future time.
    /// </summary>
    /// <remarks>
    ///     Shares the shape of the others and none of their standing: a projection that has become the basis of
    ///     a payment is a forecast being invoiced.
    /// </remarks>
    [ActiveObservation.Projection]
    public sealed class Expected : MaternityObservation {

        public Expected(string concept, string reading, DateOnly recordedOn, DateOnly expectedOn)
            : base(concept, reading, recordedOn) {
            ExpectedOn = expectedOn;
        }

        /// <summary>The future date it is expected of.</summary>
        public DateOnly ExpectedOn { get; }

        /// <inheritdoc />
        public override bool CountsAsEstablished => false;

    }

    /// <summary>
    ///     One pregnancy's record, and the count the distinction protects.
    /// </summary>
    public sealed class MaternityRecord {

        private readonly List<MaternityObservation> _observations = new();

        /// <summary>Records an observation of any standing.</summary>
        public void Add(MaternityObservation observation) {
            _observations.Add(observation);
        }

        /// <summary>Everything recorded.</summary>
        public IReadOnlyList<MaternityObservation> All => _observations;

        /// <summary>
        ///     What may be counted as fact. A statistics run that skips this is the inflated prevalence figure.
        /// </summary>
        public IReadOnlyList<MaternityObservation> Established() {
            List<MaternityObservation> established = new();
            foreach (MaternityObservation observation in _observations) {
                if (observation.CountsAsEstablished) {
                    established.Add(observation);
                }
            }

            return established;
        }

    }

}
