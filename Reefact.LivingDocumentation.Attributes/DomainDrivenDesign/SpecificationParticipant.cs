namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Enumeration to define the role played by a class participating as a Specification.
    /// </summary>
    /// <remarks>
    ///     In Domain-Driven Design, a Specification is a predicate-like function that determines if an object is satisfactory.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the SpecificationParticipant enum:
    ///     <code>
    /// [Specification(SpecificationParticipant.Specification)]
    /// public class OrderSpecification
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum SpecificationParticipant {

        /// <summary>
        ///     Indicates that the class plays the role of a Specification.
        /// </summary>
        Specification

    }

}