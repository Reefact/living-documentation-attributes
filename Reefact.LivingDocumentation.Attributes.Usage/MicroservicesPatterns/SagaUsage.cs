#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.SagaSample {

    // Connecting a new supply point crosses three services: the grid reserves capacity on a substation,
    // billing opens an account, and field work schedules a meter fit. No transaction spans them, and each
    // of the three commits before the next one is tried.
    //
    // SAGA runs them in order and undoes what it has to when a later step fails. What it does not have is a
    // rollback: the capacity reservation is released by code somebody wrote for that purpose, and a step
    // whose author forgot is a step the saga will fail to back out of at three in the morning.

    /// <summary>
    ///     Connecting a supply point, as a sequence of local transactions.
    /// </summary>
    /// <remarks>
    ///     Named as a whole so that the three participants can be found from one place. What the type holds
    ///     is the running state; what it does not hold is any guarantee about it.
    /// </remarks>
    [Saga.Saga]
    public sealed class ConnectSupplyPoint {

        public ConnectSupplyPoint(string supplyPoint, string substation, decimal kilowatts) {
            SupplyPoint = supplyPoint;
            Substation  = substation;
            Kilowatts   = kilowatts;
        }

        public string SupplyPoint { get; }

        public string Substation { get; }

        public decimal Kilowatts { get; }

        public string? ReservationId { get; internal set; }

        public string? AccountId { get; internal set; }

    }

    /// <summary>
    ///     The grid, which reserves the capacity.
    /// </summary>
    [Saga.Participant(Saga = typeof(ConnectSupplyPoint))]
    public sealed class GridService {

        private readonly HashSet<string> _reservations = new HashSet<string>();

        /// <summary>
        ///     Reserves capacity on the substation and commits.
        /// </summary>
        /// <remarks>
        ///     Committed and visible before anyone knows whether billing will accept the customer, which is
        ///     the isolation a saga gives up in exchange for not needing a distributed transaction.
        /// </remarks>
        [Saga.LocalTransaction(Saga = typeof(ConnectSupplyPoint))]
        public string Reserve(string substation, decimal kilowatts) {
            string id = $"{substation}:{kilowatts}";
            _reservations.Add(id);

            return id;
        }

        /// <summary>
        ///     Releases a reservation the saga no longer needs.
        /// </summary>
        /// <remarks>
        ///     The undo, and semantic rather than transactional: the capacity was really reserved and is now
        ///     really released, and anything that read the substation in between saw it taken.
        /// </remarks>
        [Saga.CompensatingTransaction(Saga = typeof(ConnectSupplyPoint))]
        public void Release(string reservationId) => _reservations.Remove(reservationId);

    }

    /// <summary>
    ///     Billing, which opens the account.
    /// </summary>
    [Saga.Participant(Saga = typeof(ConnectSupplyPoint))]
    public sealed class BillingService {

        /// <summary>
        ///     Opens an account, or refuses the customer.
        /// </summary>
        [Saga.LocalTransaction(Saga = typeof(ConnectSupplyPoint))]
        public string Open(string supplyPoint) {
            if (supplyPoint.StartsWith("BLOCKED", StringComparison.Ordinal)) { throw new InvalidOperationException("customer refused"); }

            return $"ACC-{supplyPoint}";
        }

        /// <summary>
        ///     Closes an account opened earlier in the saga.
        /// </summary>
        [Saga.CompensatingTransaction(Saga = typeof(ConnectSupplyPoint))]
        public void Close(string accountId) {
            // ... marks the account closed; the account number is not reused
        }

    }

    /// <summary>
    ///     Decides the order and the undoing.
    /// </summary>
    /// <remarks>
    ///     The alternative is choreography, where this class does not exist and the same sequence is spread
    ///     over three event handlers in three repositories — readable only by someone who has all three open.
    /// </remarks>
    [Saga.Orchestrator(Saga = typeof(ConnectSupplyPoint))]
    public sealed class ConnectSupplyPointOrchestrator {

        private readonly GridService    _grid;
        private readonly BillingService _billing;

        public ConnectSupplyPointOrchestrator(GridService grid, BillingService billing) {
            _grid    = grid;
            _billing = billing;
        }

        public bool Run(ConnectSupplyPoint saga) {
            saga.ReservationId = _grid.Reserve(saga.Substation, saga.Kilowatts);
            try {
                saga.AccountId = _billing.Open(saga.SupplyPoint);

                return true;
            } catch (InvalidOperationException) {
                _grid.Release(saga.ReservationId);

                return false;
            }
        }

    }
}
