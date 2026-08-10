#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.AggregatorSample {

    // Four hundred containers were split apart, moved, and each announced its own completion. The shipping line
    // wants one message: the discharge is finished.
    //
    // AGGREGATOR is the counterpart of the splitter, and the only router here that must hold state. Its three
    // hard questions are named separately on purpose — what belongs together, when it is finished, and what to
    // emit — because conflating them is how an aggregator becomes unreadable.

    /// <summary>
    ///     Holds messages until they belong together, then emits one.
    /// </summary>
    /// <remarks>
    ///     Being stateful is what distinguishes it from every other router: it must survive a restart or lose a
    ///     half-finished set.
    /// </remarks>
    [Aggregator.Aggregator]
    public sealed class DischargeCompletion {

        private readonly Dictionary<string, List<string>> _pending = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, int>          _expected = new Dictionary<string, int>();

        public void Expect(string vesselCall, int containers) => _expected[vesselCall] = containers;

        /// <summary>
        ///     What decides that two messages belong to the same set.
        /// </summary>
        /// <remarks>
        ///     Named explicitly because getting it wrong merges two unrelated discharges, and nothing else in
        ///     the pattern would notice.
        /// </remarks>
        [Aggregator.Correlation]
        public string CorrelationOf(string vesselCall, string containerNumber) => vesselCall;

        /// <summary>
        ///     What decides that a set is finished.
        /// </summary>
        /// <remarks>
        ///     The hard part: a condition that never holds is a set that never emits and a leak nobody sees.
        ///     A count here, and in a real terminal a timeout beside it.
        /// </remarks>
        [Aggregator.CompletenessCondition]
        public bool IsComplete(string vesselCall) =>
            _expected.TryGetValue(vesselCall, out int expected)
         && _pending.TryGetValue(vesselCall, out List<string>? seen)
         && seen.Count >= expected;

        /// <summary>
        ///     How the collected messages become one.
        /// </summary>
        /// <remarks>
        ///     Held apart from the completeness condition because when to emit and what to emit are different
        ///     questions.
        /// </remarks>
        [Aggregator.AggregationStrategy]
        public string Aggregate(string vesselCall) =>
            $"{vesselCall}: {_pending[vesselCall].Count} containers discharged";

        public void Collect(string vesselCall, string containerNumber) {
            if (!_pending.TryGetValue(vesselCall, out List<string>? seen)) {
                seen = new List<string>();
                _pending.Add(vesselCall, seen);
            }
            seen.Add(containerNumber);
        }

    }
}
