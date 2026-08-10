#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessagingBridgeSample {

    // The terminal is moving from MSMQ to a cloud bus over eighteen months. During those months both exist,
    // and a crane move published to one must be readable on the other.
    //
    // MESSAGING BRIDGE is the seam that lets the old system be retired gradually rather than at a weekend.

    /// <summary>
    ///     Consumes from one messaging system and publishes to another.
    /// </summary>
    /// <remarks>
    ///     It exists because two messaging systems are rarely replaced at once, and it is what makes a gradual retirement possible.
    /// </remarks>
    [MessagingBridge]
    public sealed class LegacyQueueBridge {

        public void Forward() {
            // ... take from MSMQ, publish to the bus, unchanged
        }

    }
}
