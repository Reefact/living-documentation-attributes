#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessageEndpointSample {

    // The yard planner should not hold a broker's connection factory, its retry policy or its serialiser. The
    // day the terminal moves from MSMQ to a cloud bus, the planner should not know.
    //
    // MESSAGE ENDPOINT is that seam. The application sends and receives; the messaging library lives behind
    // this type.

    /// <summary>
    ///     Connects application code to a channel.
    /// </summary>
    /// <remarks>
    ///     The seam the messaging library lives behind, which is what lets the application be tested without a
    ///     broker and the broker be replaced without the application.
    /// </remarks>
    [MessageEndpoint]
    public interface IYardPlannerEndpoint {

        void Send(string message);

        string? Receive(TimeSpan within);

    }
}
