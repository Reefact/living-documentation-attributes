#region Usings declarations

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.PollingConsumerSample {

    // Customs holds are worked by officers, and there are four of them. Handing the desk a hold the moment
    // one arrives achieves nothing: the work is limited by people, not by the channel.
    //
    // POLLING CONSUMER asks for the next hold when an officer is free. The queue depth is then a real
    // measure of the backlog rather than an artefact of how fast messages were pushed.

    /// <summary>
    ///     Asks for the next customs hold, and blocks until there is one.
    /// </summary>
    /// <remarks>
    ///     The synchronous receiver: its thread is its own, so how many holds are being worked at once is
    ///     something the desk can count.
    /// </remarks>
    [PollingConsumer]
    public interface ICustomsHoldPoller {

        CustomsHold Receive();

    }

    public sealed record CustomsHold(string ContainerNumber, string Reason);
}
