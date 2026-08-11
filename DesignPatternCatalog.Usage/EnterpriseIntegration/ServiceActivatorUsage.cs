#region Usings declarations

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.ServiceActivatorSample {

    // The weighbridge service is called directly by the gate screen an operator uses, and over a channel by
    // the haulier portal. It is the same weighing, and it must stay one implementation.
    //
    // SERVICE ACTIVATOR is the piece that lets it: the activator consumes the message and calls the service
    // like any other client. The service never learns which of the two happened.

    /// <summary>
    ///     What is being made available, in the terminal's own terms.
    /// </summary>
    /// <remarks>
    ///     Naming it is what makes the pattern's claim checkable: a <c>Weigh</c> that grew a reply channel in
    ///     its parameters would no longer be callable from the gate screen, and nothing but this annotation
    ///     would have said that was the point.
    /// </remarks>
    [ServiceActivator.Service]
    public interface IWeighbridgeService {

        decimal Weigh(string containerNumber);

    }

    /// <summary>
    ///     Consumes weighing requests from the channel and calls the service.
    /// </summary>
    /// <remarks>
    ///     It handles every messaging detail — here half of a request-reply, since the weight goes back.
    /// </remarks>
    [ServiceActivator.ServiceActivator(Service = typeof(IWeighbridgeService))]
    public sealed class WeighbridgeServiceActivator {

        private readonly IWeighbridgeService _service;

        public WeighbridgeServiceActivator(IWeighbridgeService service) {
            _service = service;
        }

        public decimal OnRequest(string containerNumber) {
            return _service.Weigh(containerNumber);
        }

    }
}
