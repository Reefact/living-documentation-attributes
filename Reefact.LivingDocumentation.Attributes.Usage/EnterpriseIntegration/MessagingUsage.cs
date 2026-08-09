#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessagingSample {

    // Every crane move, gate transaction and yard shuffle is announced as a message. Nobody waits: the
    // billing system, the ship planner and the customer portal each read what interests them, and a portal
    // down for maintenance misses nothing once it comes back.
    //
    // MESSAGING is the style the rest of this catalogue elaborates. Sender and receiver are decoupled in
    // technology and, which matters more here, in time.

    /// <summary>
    ///     Announces a completed crane move to whoever cares.
    /// </summary>
    /// <remarks>
    ///     The publisher names a channel and not a recipient, so a new consumer costs the publisher nothing.
    /// </remarks>
    [Messaging]
    public sealed class CraneMoveAnnouncer {

        public void Announce(string containerNumber, string fromSlot, string toSlot) {
            // ... hands the message to an endpoint; who reads it is not this class's business
        }

    }
}
