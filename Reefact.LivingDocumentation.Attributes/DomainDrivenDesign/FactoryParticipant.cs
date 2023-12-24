namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Enumeration to define the role played by a class participating as a Factory.
    /// </summary>
    /// <remarks>
    ///     In Domain-Driven Design, a Factory is responsible for creating complex objects or aggregates.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the FactoryParticipant enum:
    ///     <code>
    /// [Factory(FactoryParticipant.Factory)]
    /// public class OrderFactory
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum FactoryParticipant {

        /// <summary>
        ///     Indicates that the class plays the role of a Factory.
        /// </summary>
        Factory

    }

}