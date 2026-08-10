#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.SelfContainedServiceSample {

    // Connecting a supply point used to call the grid for capacity, billing for credit and field work for
    // an appointment, and answer the customer only when all three had. The desk was therefore available
    // exactly as often as the least available of the four, which on a bad afternoon was not often.
    //
    // SELF-CONTAINED SERVICE removes the waiting. Capacity comes from a replica the connections desk keeps
    // current; the rest is a saga finished afterwards. The reply is available and, in exchange, it cannot
    // say whether the connection was approved — the customer is given a reference and has to come back.

    /// <summary>
    ///     The connections desk, answering on its own.
    /// </summary>
    /// <remarks>
    ///     No synchronous call while handling a request, so its availability is its own rather than the
    ///     product of everything it would have called. That is the claim, and the one thing that breaks it
    ///     is a single innocuous-looking call added inside <c>Request</c> — which compiles, passes and
    ///     silently restores the coupling.
    /// </remarks>
    [SelfContainedService]
    public sealed class ConnectionsDesk {

        private readonly ICapacityReplica _capacity;
        private readonly IConnectionSaga  _saga;

        public ConnectionsDesk(ICapacityReplica capacity, IConnectionSaga saga) {
            _capacity = capacity;
            _saga     = saga;
        }

        /// <summary>
        ///     Answers at once, with a reference rather than an outcome.
        /// </summary>
        public string Request(string supplyPoint, string substation, decimal kilowatts) {
            if (_capacity.SpareAt(substation) < kilowatts) { return "REFUSED"; }

            string reference = $"REQ-{Guid.NewGuid():N}";
            _saga.Begin(reference, supplyPoint, substation, kilowatts);

            return reference;
        }

    }

    /// <summary>The replica read instead of calling the grid.</summary>
    public interface ICapacityReplica {

        decimal SpareAt(string substation);

    }

    /// <summary>What finishes the work after the answer has gone out.</summary>
    public interface IConnectionSaga {

        void Begin(string reference, string supplyPoint, string substation, decimal kilowatts);

    }
}
