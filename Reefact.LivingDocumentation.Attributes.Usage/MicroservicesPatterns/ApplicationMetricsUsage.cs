#region Usings declarations


using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.ApplicationMetricsSample {

    // On the night the reading validator wedged, every dashboard was green. Nothing was failing; nothing was
    // happening either, and no chart could tell the difference.
    //
    // APPLICATION METRICS instruments the operations themselves. What annotating them adds is the inventory:
    // which operations are measured, and therefore which silence is an outage and which silence is a gap in
    // the instrumentation.

    /// <summary>
    ///     Validation, instrumented.
    /// </summary>
    /// <remarks>
    ///     Annotating the method answers what a dashboard cannot: which operations are measured. That
    ///     matters because it decides what silence means — no readings validated in an hour is an outage
    ///     if this is instrumented and nothing at all if it is not.
    /// </remarks>
    public sealed class MeteringMetrics {

        [ApplicationMetrics]
        public void RecordValidation(string outcome, double milliseconds) {
            // ... increments metering_validations_total{outcome} and observes the duration
        }

        public void RecordEstimate(string reason) {
            // Deliberately not annotated: this one is not instrumented, and that is the point of the
            // annotation above being present here and absent here.
        }

    }
}
