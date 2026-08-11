#region Usings declarations

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.SelectiveConsumerSample {

    // The terminal has one work order channel and three quays. North quay has no reefer plugs and no reason
    // to see reefer work; south quay has no rail siding. Three channels would be tidier and the broker allows
    // sixty-four in total, which the terminal passed years ago.
    //
    // SELECTIVE CONSUMER is the three-part arrangement that makes one channel behave like three: a producer
    // that states the area, the value it states, and consumers that take only their own.

    /// <summary>
    ///     Sets the area on every order it sends.
    /// </summary>
    /// <remarks>
    ///     A named participant because the selection is a contract between parties that never meet: a planner
    ///     that stops setting the area breaks consumers it has never heard of.
    /// </remarks>
    [SelectiveConsumer.SpecifyingProducer]
    public sealed class BerthPlanner {

        public WorkOrder Plan(string containerNumber, string area) {
            return new WorkOrder(containerNumber, area);
        }

    }

    public sealed class WorkOrder {

        public WorkOrder(string containerNumber, string terminalArea) {
            ContainerNumber = containerNumber;
            TerminalArea    = terminalArea;
        }

        public string ContainerNumber { get; }

        /// <summary>
        ///     What a consumer reads to decide whether the order is for it.
        /// </summary>
        /// <remarks>
        ///     Its range is the thing to watch: an area no consumer accepts — a fourth quay opened on a
        ///     Monday — is an order that sits on the channel until it expires.
        /// </remarks>
        [SelectiveConsumer.SelectionValue]
        public string TerminalArea { get; }

    }

    /// <summary>
    ///     Takes north quay orders and leaves the rest.
    /// </summary>
    /// <remarks>
    ///     It chooses for itself and leaves the others where they are, which is what separates it from a
    ///     message filter: a filter sits in the channel and drops for everyone.
    /// </remarks>
    [SelectiveConsumer.SelectiveConsumer]
    public sealed class NorthQuayConsumer {

        public bool Accepts(WorkOrder order) {
            return order.TerminalArea == "NORTH";
        }

    }
}
