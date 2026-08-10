#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.SmartProxySample {

    // The weighbridge service answers wherever the request's return address says — that is what lets one
    // service serve the gate screen and the haulier portal. It also means there is no fixed output channel,
    // so a pair of wire taps has nothing to tap, and nobody can say how long a weighing takes.
    //
    // SMART PROXY keeps the requestor's address, puts its own in the request, and forwards the reply on.

    /// <summary>
    ///     Stands between the requestors and the weighbridge, remembering where each answer really belongs.
    /// </summary>
    /// <remarks>
    ///     It holds state per outstanding request, so an answer that never comes is a leak rather than a
    ///     silence — the cost of being able to measure a service that answers wherever it is told.
    /// </remarks>
    [SmartProxy]
    public sealed class WeighbridgeSmartProxy {

        private readonly Dictionary<Guid, string> _realReturnAddresses = new Dictionary<Guid, string>();

        public WeighingRequest Intercept(WeighingRequest request) {
            _realReturnAddresses[request.RequestId] = request.ReplyTo;

            return request with { ReplyTo = "terminal.weighbridge.proxy.replies" };
        }

        public string ForwardTo(Guid requestId) {
            string replyTo = _realReturnAddresses[requestId];
            _realReturnAddresses.Remove(requestId);

            return replyTo;
        }

    }

    public sealed record WeighingRequest(Guid RequestId, string ContainerNumber, string ReplyTo);
}
