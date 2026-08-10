#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.RecipientListSample {

    // A customs hold on a container must reach the yard, the gate, the shipping line and — only if the box is
    // refrigerated — the reefer desk. Which of the four depends on the message, so a subscription cannot decide
    // it.
    //
    // RECIPIENT LIST computes the recipients per message, and says who they were.

    /// <summary>
    ///     Computes the recipients of one message and sends a copy to each.
    /// </summary>
    /// <remarks>
    ///     Unlike a publish-subscribe channel the decision is the sender's and per message, which is what lets
    ///     it depend on the message's content.
    /// </remarks>
    [RecipientList.RecipientList(Recipients = typeof(CustomsHoldDistribution))]
    public sealed class CustomsHoldDistribution {

        /// <summary>
        ///     The destinations computed for this message.
        /// </summary>
        /// <remarks>
        ///     Exposing them is what makes the routing decision auditable rather than a side effect nobody can
        ///     inspect afterwards.
        /// </remarks>
        [RecipientList.Recipients]
        public IReadOnlyList<string> RecipientsFor(bool refrigerated) {
            List<string> to = new() { "terminal.yard", "terminal.gate", "line.interface" };
            if (refrigerated) { to.Add("terminal.reefer.desk"); }

            return to;
        }

    }
}
