#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.DistributedTracingSample {

    // A customer's account page was taking nine seconds. Four services were involved, all four reported
    // healthy, and each said it had answered in under a hundred milliseconds.
    //
    // DISTRIBUTED TRACING gives the request one identifier and follows it. The instrumentation is the whole
    // pattern, and its failure mode is the quiet one: one participant that does not pass the identifier on
    // ends the trace, and the missing span reads as a call that never happened.

    /// <summary>
    ///     Carries the request identifier onward.
    /// </summary>
    /// <remarks>
    ///     Load-bearing and invisible. A participant that forgets to pass the identifier on does not fail
    ///     — it silently ends the trace, and the gap looks exactly like a service that was never called.
    ///     Which participants propagate is therefore worth being able to list.
    /// </remarks>
    [DistributedTracing]
    public sealed class TracingHandler {

        public string Handle(string? incomingTraceId, Func<string, string> next) {
            string traceId = incomingTraceId ?? Guid.NewGuid().ToString("N");

            return next(traceId);
        }

    }
}
