#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.CompetingConsumersSample {

    // Discharging a 14,000-TEU vessel puts work orders on the queue faster than one process can plan them.
    // Twelve instances of the same consumer run on the same channel and each takes the next order.
    //
    // COMPETING CONSUMERS is that arrangement. It reads as ordinary scaling until it is put on a
    // publish-subscribe channel by mistake, where twelve consumers plan the same order twelve times.

    /// <summary>
    ///     One of twelve identical consumers on the discharge work order channel.
    /// </summary>
    /// <remarks>
    ///     Which of them receives a given order is the messaging system's business. It works only on a
    ///     point-to-point channel — on a publish-subscribe channel each consumer gets its own copy, which is
    ///     the opposite of what this is for.
    /// </remarks>
    [CompetingConsumers]
    public sealed class DischargeWorkOrderConsumer {

        public void Handle(DischargeWorkOrder order) {
            // Plan the move, then acknowledge. Nothing here may assume it saw the previous order.
        }

    }

    public sealed record DischargeWorkOrder(string ContainerNumber, string Bay, string FromPosition);
}
