#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.InvalidMessageChannelSample {

    // A manifest arrives with a container number that is not a container number. The yard planner cannot
    // process it, and must not stop for it — a single malformed message blocking the channel is how a
    // terminal stops working.
    //
    // INVALID MESSAGE CHANNEL is where the receiver puts what it has read and rejected.

    /// <summary>
    ///     Where a receiver puts a message it cannot process.
    /// </summary>
    /// <remarks>
    ///     The distinction from a dead letter channel is WHO decides: here the receiver read the message and rejected it.
    /// </remarks>
    [InvalidMessageChannel]
    public interface IInvalidManifests {

        void Reject(string message, string why);

    }
}
