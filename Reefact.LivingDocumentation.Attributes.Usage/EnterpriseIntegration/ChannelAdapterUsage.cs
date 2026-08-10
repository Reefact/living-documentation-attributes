#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.ChannelAdapterSample {

    // The weighbridge is a twenty-year-old system with a serial port and no notion of a message. It will not
    // be modified: the vendor is gone and the certification is not worth reopening.
    //
    // CHANNEL ADAPTER reaches into it from outside and puts its readings on a channel.

    /// <summary>
    ///     Reads or writes an application's own interface on one side and a channel on the other.
    /// </summary>
    /// <remarks>
    ///     It is what lets a system take part in an integration without being modified, which is often the only option there is.
    /// </remarks>
    [ChannelAdapter]
    public sealed class WeighbridgeAdapter {

        public void Poll() {
            // ... reads the serial port, publishes a weight message
        }

    }
}
