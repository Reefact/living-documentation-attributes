#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.PublishSubscribeChannelSample {

    // A vessel's departure interests the billing system, the customs interface, the customer portal and the
    // performance dashboard. Next quarter it will interest something that does not exist yet.
    //
    // PUBLISH-SUBSCRIBE CHANNEL means the publisher adds no code for any of them.

    /// <summary>
    ///     A channel that copies its message to every subscriber.
    /// </summary>
    /// <remarks>
    ///     A sender writes nothing when a subscriber is added, which is what makes this the channel for events rather than for commands.
    /// </remarks>
    [PublishSubscribeChannel]
    public interface IVesselDepartedTopic {

        void Publish(string vesselCall);

    }
}
