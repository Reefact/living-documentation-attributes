namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Enumeration to define the role played by a class participating as a Service.
    /// </summary>
    /// <remarks>
    ///     In Domain-Driven Design, a Service is an operation offered as an interface that stands alone in the model.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the ServiceParticipant enum:
    ///     <code>
    /// [Service(ServiceParticipant.Service)]
    /// public class OrderService
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum ServiceParticipant {

        /// <summary>
        ///     Indicates that the class plays the role of a Service.
        /// </summary>
        Service

    }

}