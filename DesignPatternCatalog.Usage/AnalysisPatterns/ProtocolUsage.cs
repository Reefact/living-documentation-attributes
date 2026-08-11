#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.ProtocolSample {

    // A water authority's laboratory. Biochemical oxygen demand is reported for every discharge consent in the
    // region, and it is determined two ways: the five-day incubation, and a rapid respirometric method the lab
    // moved to for routine samples.
    //
    // The two do not agree. They correlate, they are both correct, and they differ by a margin that matters at a
    // consent limit. Two results of "BOD in mg/L" obtained differently are not interchangeable, and the model
    // that records only the phenomenon type cannot say so.
    //
    // PROTOCOL is the method, recorded alongside. Figure 3.10 hangs it off the phenomenon type at the knowledge
    // level, and what it buys is comparability: a trend line drawn through results from both methods is a
    // reported breach that never happened, or a real one hidden, and no arithmetic detects either.
    //
    // The annotation says the comparison must ask. That is the whole of what a rule can check here, and it is
    // enough: a query that groups by phenomenon type and ignores protocol is findable.

    /// <summary>
    ///     How a determination was made.
    /// </summary>
    /// <remarks>
    ///     Configured at the knowledge level beside the thing determined, so a new method is data rather than a
    ///     column.
    /// </remarks>
    [Protocol]
    public sealed class Determination {

        public Determination(string name, string reference, decimal typicalBias) {
            Name        = name;
            Reference   = reference;
            TypicalBias = typicalBias;
        }

        /// <summary>Five-day incubation, rapid respirometric.</summary>
        public string Name { get; }

        /// <summary>The published method it follows.</summary>
        public string Reference { get; }

        /// <summary>
        ///     Its known bias against the reference method, which is why results under two protocols cannot
        ///     simply be pooled.
        /// </summary>
        public decimal TypicalBias { get; }

    }

    /// <summary>
    ///     One laboratory result: what was determined, on what, how, and when.
    /// </summary>
    public sealed class LabResult {

        public LabResult(string analyte, string sampleReference, Determination by, decimal amount, string unit,
                         DateOnly on) {
            Analyte         = analyte;
            SampleReference = sampleReference;
            By              = by;
            Amount          = amount;
            Unit            = unit;
            On              = on;
        }

        /// <summary>What was determined.</summary>
        public string Analyte { get; }

        /// <summary>Which sample.</summary>
        public string SampleReference { get; }

        /// <summary>The protocol it was determined by — never absent.</summary>
        public Determination By { get; }

        /// <summary>The amount.</summary>
        public decimal Amount { get; }

        /// <summary>Its unit.</summary>
        public string Unit { get; }

        /// <summary>When.</summary>
        public DateOnly On { get; }

    }

    /// <summary>
    ///     The results on record, and the question the protocol makes answerable.
    /// </summary>
    public sealed class ConsentMonitoring {

        private readonly List<LabResult> _results = new();

        /// <summary>Records a result.</summary>
        public void Add(LabResult result) {
            _results.Add(result);
        }

        /// <summary>
        ///     A series for one analyte determined one way. Taking the protocol as an argument rather than
        ///     defaulting it is the point: there is no such thing as "the BOD series" for a discharge.
        /// </summary>
        public IReadOnlyList<LabResult> Series(string analyte, Determination by) {
            List<LabResult> series = new();
            foreach (LabResult result in _results) {
                if (result.Analyte == analyte && ReferenceEquals(result.By, by)) {
                    series.Add(result);
                }
            }

            return series;
        }

        /// <summary>
        ///     The protocols an analyte has been determined by. A caller about to draw one line through all of
        ///     them can find out here that it should not.
        /// </summary>
        public IReadOnlySet<Determination> ProtocolsUsedFor(string analyte) {
            HashSet<Determination> used = new();
            foreach (LabResult result in _results) {
                if (result.Analyte == analyte) {
                    used.Add(result.By);
                }
            }

            return used;
        }

    }

}
