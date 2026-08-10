#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.CircuitBreakerSample {

    // The quote desk calls the grid for spare capacity on every quote. The evening the grid's database went
    // to eight-second queries, the quote desk did not fail — it filled its thread pool waiting, and stopped
    // answering calls about anything at all. One slow service took out a service that was working.
    //
    // CIRCUIT BREAKER stops the waiting. Past a threshold of consecutive failures it opens and returns at
    // once, and after a timeout it lets a few calls through to see. What it introduces is a failure the
    // remote service never sent, and a caller written as though every error came from the grid is now wrong.

    /// <summary>
    ///     What the grid exposes, unchanged.
    /// </summary>
    public interface IGridCapacityApi {

        decimal SpareCapacityAt(string substation);

    }

    /// <summary>
    ///     Raised when the breaker is open, by the breaker and not by the grid.
    /// </summary>
    public sealed class CircuitOpenException : Exception {

        public CircuitOpenException() : base("the grid capacity circuit is open") { }

    }

    /// <summary>
    ///     The proxy that trips.
    /// </summary>
    /// <remarks>
    ///     Annotated here rather than on <see cref="IGridCapacityApi" />: the interface is the grid's
    ///     contract, and the tripping is this class's behaviour. A caller holding the interface cannot tell
    ///     which of the two it is talking to, which is the point and also the hazard.
    /// </remarks>
    [CircuitBreaker]
    public sealed class GridCapacityBreaker : IGridCapacityApi {

        private readonly IGridCapacityApi _grid;
        private readonly int              _threshold;

        private int _consecutiveFailures;

        public GridCapacityBreaker(IGridCapacityApi grid, int threshold) {
            _grid      = grid;
            _threshold = threshold;
        }

        public bool IsOpen => _consecutiveFailures >= _threshold;

        public decimal SpareCapacityAt(string substation) {
            if (IsOpen) { throw new CircuitOpenException(); }

            try {
                decimal capacity = _grid.SpareCapacityAt(substation);
                _consecutiveFailures = 0;

                return capacity;
            } catch (TimeoutException) {
                _consecutiveFailures++;

                throw;
            }
        }

    }
}
