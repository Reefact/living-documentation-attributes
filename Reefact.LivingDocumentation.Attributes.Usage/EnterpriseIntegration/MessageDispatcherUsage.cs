#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessageDispatcherSample {

    // Crane moves must be concurrent across cranes and ordered within one: crane 7 cannot lift a box it has
    // not yet put down. Competing consumers give concurrency and lose the ordering, because any consumer can
    // take any move.
    //
    // MESSAGE DISPATCHER keeps one consumer on the channel and hands each move to the performer for its
    // crane. Concurrency across cranes, order within each, and the rule lives in the application.

    /// <summary>
    ///     The single consumer on the crane move channel.
    /// </summary>
    /// <remarks>
    ///     Being the only consumer is the difference from competing consumers: the application decides who
    ///     gets what, so a move can be routed to the performer that already holds the crane's order.
    /// </remarks>
    [MessageDispatcher.Dispatcher(Performer = typeof(ICraneMovePerformer))]
    public sealed class CraneMoveDispatcher {

        private readonly IReadOnlyDictionary<string, ICraneMovePerformer> _performers;

        public CraneMoveDispatcher(IReadOnlyDictionary<string, ICraneMovePerformer> performers) {
            _performers = performers;
        }

        public void Dispatch(CraneMove move) {
            _performers[move.CraneIdentifier].Perform(move);
        }

    }

    /// <summary>
    ///     One per crane, each on a thread of its own.
    /// </summary>
    /// <remarks>
    ///     A performer may be created per message or drawn from a pool; here it is matched to the message,
    ///     which is what buys the ordering.
    /// </remarks>
    [MessageDispatcher.Performer]
    public interface ICraneMovePerformer {

        void Perform(CraneMove move);

    }

    public sealed record CraneMove(string CraneIdentifier, string ContainerNumber, string ToPosition);
}
