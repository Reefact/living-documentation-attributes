#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.GuaranteedDeliverySample {

    // A crane move announced the instant before the broker's host is rebooted must still reach billing.
    // Losing it means an invoice that is short by one lift, found weeks later by a customer.
    //
    // GUARANTEED DELIVERY persists the message rather than holding it in memory.

    /// <summary>
    ///     A channel that persists what it carries until it is delivered.
    /// </summary>
    /// <remarks>
    ///     A property of the channel rather than of a message, and it costs throughput for durability — which is why it is declared rather than assumed.
    /// </remarks>
    [GuaranteedDelivery]
    public interface IDurableCraneMoves {

        void Send(string craneMove);

    }
}
