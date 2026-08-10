#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessageRouterSample {

    // A gate transaction goes to the yard planner if the container is inbound, to the billing system if it is
    // outbound, and to both if it is a re-handle. Written as a condition inside the gate service, every new
    // destination is a change to the gate.
    //
    // MESSAGE ROUTER puts the decision in one participant, and that participant forwards without altering.

    /// <summary>
    ///     Consumes a message and sends it on unchanged.
    /// </summary>
    /// <remarks>
    ///     The assertion is the "unchanged": a router that alters what it forwards is a translator wearing the
    ///     wrong name, and an architecture rule can be written against exactly that.
    /// </remarks>
    [MessageRouter]
    public sealed class GateTransactionRouter {

        public string Route(string direction) =>
            direction switch {
                "in"  => "terminal.yard.planning",
                "out" => "terminal.billing",
                _     => "terminal.invalid"
            };

    }
}
