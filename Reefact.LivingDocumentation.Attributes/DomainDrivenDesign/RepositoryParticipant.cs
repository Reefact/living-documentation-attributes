namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Enumeration to define the role played by a class participating as a Repository.
    /// </summary>
    /// <remarks>
    ///     In the context of Domain-Driven Design, a Repository acts as a sort of in-memory collection for domain objects.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the RepositoryParticipant enum:
    ///     <code>
    /// [Repository(RepositoryParticipant.Repository)]
    /// public class OrderRepository
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum RepositoryParticipant {

        /// <summary>
        ///     Indicates that the class plays the role of a Repository.
        /// </summary>
        Repository,

        /// <summary>
        ///     Indicates that the class plays the role of a Concrete Repository.
        /// </summary>
        ConcreteRepository

    }

}