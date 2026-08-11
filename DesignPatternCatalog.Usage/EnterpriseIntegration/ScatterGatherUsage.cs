#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.ScatterGatherSample {

    // A container needs a berth window. Three terminals in the port could take it, and the answer wanted is
    // whichever can take it soonest.
    //
    // SCATTER-GATHER asks all three and assembles the replies. What is distributed here is the whole message to
    // several parties — not, as in a composed message processor, the parts of one message.

    /// <summary>
    ///     Broadcasts a request and aggregates the replies.
    /// </summary>
    /// <remarks>
    ///     The reply set is what makes it useful and what makes it hard: how long to wait for parties that may
    ///     never answer is a decision this participant owns.
    /// </remarks>
    [ScatterGather]
    public sealed class BerthWindowEnquiry {

        public string? Best(IReadOnlyList<(string Terminal, DateOnly? Window)> replies) {
            string? best = null;
            DateOnly? soonest = null;
            foreach ((string terminal, DateOnly? window) in replies) {
                if (window is null) { continue; }
                if (soonest is null || window < soonest) { soonest = window; best = terminal; }
            }

            return best;
        }

    }
}
