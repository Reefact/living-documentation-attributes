#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.ReturnAddressSample {

    // One release authority answers the terminal, two other terminals in the port and a customs broker. Told by
    // configuration where to reply, it needs a deployment for the fourth.
    //
    // RETURN ADDRESS puts the channel on the request. One replier, four requestors, each answered on its own
    // channel.

    /// <summary>
    ///     Where the reply belongs.
    /// </summary>
    /// <remarks>
    ///     Carrying it on the message is what lets one replier serve many requestors — and what makes a reply
    ///     that goes nowhere a defect in the message rather than in the replier.
    /// </remarks>
    public sealed class ReleaseEnquiry {

        public ReleaseEnquiry(string containerNumber, string replyTo) {
            ContainerNumber = containerNumber;
            ReplyTo         = replyTo;
        }

        public string ContainerNumber { get; }

        /// <summary>The channel the answer should be sent on.</summary>
        [ReturnAddress]
        public string ReplyTo { get; }

    }
}
