#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.PointToPointChannelSample {

    // Four instances of the gate service read the same channel so that a busy morning is absorbed by adding a
    // fifth. What must never happen is one truck being admitted twice because two instances read the same
    // transaction.
    //
    // POINT-TO-POINT CHANNEL is the assertion that exactly one of them gets it.

    /// <summary>
    ///     A channel whose message is consumed once, however many receivers listen.
    /// </summary>
    /// <remarks>
    ///     That is the assertion, and it is the one a consumer relies on in order to scale by adding an instance.
    /// </remarks>
    [PointToPointChannel]
    public interface IGateTransactionQueue {

        string? Take();

    }
}
