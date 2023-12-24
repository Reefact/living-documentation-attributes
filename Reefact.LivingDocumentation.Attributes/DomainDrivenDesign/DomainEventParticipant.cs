namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Enumeration to define the role played by a class participating as a Domain Event.
    /// </summary>
    /// <remarks>
    ///     In the context of Domain-Driven Design, Domain Events are generally standalone elements
    ///     that capture a state change in the domain.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the DomainEventParticipant enum:
    ///     <code>
    /// [DomainEvent(DomainEventParticipant.DomainEvent)]
    /// public class OrderCreated
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum DomainEventParticipant {

        /// <summary>
        ///     Indicates that the class plays the role of a Domain Event.
        /// </summary>
        DomainEvent

    }

}