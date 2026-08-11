#region Usings declarations

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.DurableSubscriberSample {

    // Customs holds are published to everyone who cares. The customs interface is restarted every night for a
    // batch window, and a hold published during it must not be lost: releasing a held container is the kind
    // of mistake that closes a terminal.
    //
    // DURABLE SUBSCRIBER makes the messaging system keep them. Nothing in the code differs from a subscriber
    // that would lose them — which is exactly why the intent has to be declared.

    /// <summary>
    ///     A subscriber whose subscription outlives its connection.
    /// </summary>
    /// <remarks>
    ///     Its behaviour while connected is identical to a non-durable one. The whole pattern is a behaviour
    ///     visible only during an absence, so nothing in the code can be read for it.
    /// </remarks>
    [DurableSubscriber]
    public interface ICustomsHoldSubscriber {

        void OnHold(CustomsHold hold);

    }

    public sealed record CustomsHold(string ContainerNumber, string Reason);
}
