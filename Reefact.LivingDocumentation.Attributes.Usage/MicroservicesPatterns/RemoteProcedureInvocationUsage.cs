#region Usings declarations

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.RemoteProcedureInvocationSample {

    // The connections desk quotes a supply point while a customer is on the telephone, and the quote needs
    // the substation's spare capacity as it stands now. A message and an eventual reply are no use to
    // somebody holding a handset, so the grid is asked and answers.
    //
    // REMOTE PROCEDURE INVOCATION is the simple, familiar answer, and it is the one that trades availability
    // for it: the connections desk is up exactly as often as the grid is, for the length of every call, and
    // no signature here says so.

    /// <summary>
    ///     What the grid exposes to the rest of the company.
    /// </summary>
    /// <remarks>
    ///     Annotating the interface rather than its implementation is the rule of ADR-0010, and it is also
    ///     the honest place: the availability this role talks about is a property of the contract, not of
    ///     whichever class happens to be behind it today.
    /// </remarks>
    [RemoteProcedureInvocation.Service]
    public interface IGridCapacityApi {

        decimal SpareCapacityAt(string substation);

    }

    /// <summary>
    ///     The connections desk, calling and waiting.
    /// </summary>
    /// <remarks>
    ///     This is where a circuit breaker and a discovery mechanism belong, and where somebody will ask —
    ///     rightly — whether a quote could have been made from a replica instead.
    /// </remarks>
    [RemoteProcedureInvocation.Client(Service = typeof(IGridCapacityApi))]
    public sealed class QuoteDesk {

        private readonly IGridCapacityApi _grid;

        public QuoteDesk(IGridCapacityApi grid) {
            _grid = grid;
        }

        public bool CanConnect(string substation, decimal kilowatts) => _grid.SpareCapacityAt(substation) >= kilowatts;

    }
}
