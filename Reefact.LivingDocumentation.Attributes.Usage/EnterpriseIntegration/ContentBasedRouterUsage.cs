#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.ContentBasedRouterSample {

    // The same container terminal. A gate transaction goes to yard planning if the box is coming in, to billing
    // if it is going out, and to the reefer desk if it needs power. Written as a condition inside the gate
    // service, every new destination is a change to the gate.
    //
    // CONTENT-BASED ROUTER moves that knowledge into one participant. The gate sends one message and knows
    // nothing about who wants it.

    /// <summary>
    ///     Inspects a message and forwards it unchanged to exactly one destination.
    /// </summary>
    /// <remarks>
    ///     It centralises knowledge of the destinations, which is the trade: one participant knows them all so
    ///     that no sender knows any.
    /// </remarks>
    [ContentBasedRouter]
    public sealed class GateTransactionRouter {

        public string Route(GateTransaction transaction) {
            if (transaction.NeedsPower) { return "terminal.reefer.desk"; }

            return transaction.Inbound ? "terminal.yard.planning" : "terminal.billing";
        }

    }

    public sealed record GateTransaction(string ContainerNumber, bool Inbound, bool NeedsPower);
}
