#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.RejectedObservationSample {

    // A hospital blood sciences laboratory. A potassium of 7.1 is reported at 02:40, the on-call registrar acts
    // on it, and at 04:15 the sample is found to have been drawn from the arm holding the infusion. The result
    // is wrong.
    //
    // The obvious move is to delete it. It is the wrong move, and not for tidiness: a doctor gave treatment on
    // the strength of that number. A record that no longer contains it cannot explain the treatment, cannot
    // support the incident review, and quietly makes a defensible decision look inexplicable.
    //
    // REJECTED OBSERVATION marks it instead. Figure 3.12 makes rejection a «dynamic» subtype — a state an
    // observation enters, not a kind it was created as — which is exactly right: nobody records a result
    // intending it to be wrong.
    //
    // What the annotation licenses is the query rule. Every clinical view must exclude rejected observations and
    // every audit view must include them, and the default has to be exclusion, because a rejected result that
    // reappears on a trend chart is the original error a second time.

    /// <summary>
    ///     One laboratory result.
    /// </summary>
    /// <remarks>
    ///     Rejection is a state it enters. It is never removed, because a decision was taken on it.
    /// </remarks>
    [Observation.Observation]
    public sealed class BloodResult {

        public BloodResult(string analyte, string sampleReference, decimal amount, string unit, DateTime reportedAt) {
            Analyte         = analyte;
            SampleReference = sampleReference;
            Amount          = amount;
            Unit            = unit;
            ReportedAt      = reportedAt;
        }

        /// <summary>What was measured.</summary>
        public string Analyte { get; }

        /// <summary>Which sample.</summary>
        public string SampleReference { get; }

        /// <summary>The value reported.</summary>
        public decimal Amount { get; }

        /// <summary>Its unit.</summary>
        public string Unit { get; }

        /// <summary>When it was reported to the ward.</summary>
        public DateTime ReportedAt { get; }

        /// <summary>The rejection, when there is one.</summary>
        public Rejection? Rejection { get; private set; }

        /// <summary>Whether the result has been withdrawn.</summary>
        public bool IsRejected => Rejection is not null;

        /// <summary>
        ///     Withdraws the result, recording who withdrew it and why. Both are required: a result that is
        ///     merely flagged cannot be defended at a review.
        /// </summary>
        public void Reject(string by, string reason, DateTime at) {
            Rejection = new Rejection(by, reason, at);
        }

    }

    /// <summary>
    ///     Why a result was withdrawn, and by whom.
    /// </summary>
    [RejectedObservation]
    public sealed class Rejection {

        public Rejection(string by, string reason, DateTime at) {
            By     = by;
            Reason = reason;
            At     = at;
        }

        /// <summary>Who withdrew it.</summary>
        public string By { get; }

        /// <summary>Why — "sample drawn proximal to infusion".</summary>
        public string Reason { get; }

        /// <summary>When.</summary>
        public DateTime At { get; }

    }

    /// <summary>
    ///     The results on record, with the two views the pattern makes distinguishable.
    /// </summary>
    public sealed class ResultRegister {

        private readonly List<BloodResult> _results = new();

        /// <summary>Records a result.</summary>
        public void Add(BloodResult result) {
            _results.Add(result);
        }

        /// <summary>
        ///     What a clinician sees. Excludes rejected results, and the exclusion is the default because the
        ///     opposite default is how a withdrawn result reaches a trend chart.
        /// </summary>
        public IReadOnlyList<BloodResult> Clinical(string analyte) {
            List<BloodResult> view = new();
            foreach (BloodResult result in _results) {
                if (result.Analyte == analyte && !result.IsRejected) {
                    view.Add(result);
                }
            }

            return view;
        }

        /// <summary>
        ///     What an incident review sees: everything, rejected included, because the question is what was
        ///     acted upon.
        /// </summary>
        public IReadOnlyList<BloodResult> ForReview(string analyte) {
            List<BloodResult> view = new();
            foreach (BloodResult result in _results) {
                if (result.Analyte == analyte) {
                    view.Add(result);
                }
            }

            return view;
        }

    }

}
