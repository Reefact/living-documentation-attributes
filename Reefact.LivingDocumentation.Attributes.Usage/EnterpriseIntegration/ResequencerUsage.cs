#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.ResequencerSample {

    // Crane moves are published by six cranes over two brokers, and they arrive out of order often enough that
    // a yard rebuilt from them puts a container in a slot it left ten minutes ago.
    //
    // RESEQUENCER buffers what arrives early and releases in order. It touches neither the messages nor their
    // destination — which is what keeps it a router.

    /// <summary>
    ///     Buffers what arrives early and releases in order.
    /// </summary>
    /// <remarks>
    ///     It needs a sequence to work from, and it is stateful for the same reason an aggregator is: a gap it
    ///     is waiting on outlives the message that revealed it.
    /// </remarks>
    [Resequencer]
    public sealed class CraneMoveResequencer {

        private readonly SortedDictionary<long, string> _held = new SortedDictionary<long, string>();
        private long _next = 1;

        public IReadOnlyList<string> Offer(long sequence, string move) {
            _held[sequence] = move;
            List<string> released = new();
            while (_held.TryGetValue(_next, out string? ready)) {
                released.Add(ready);
                _held.Remove(_next);
                _next++;
            }

            return released;
        }

    }
}
