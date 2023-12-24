#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Attribute to mark classes or interfaces as Repositories in the context of Domain-Driven Design.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays the role of a Repository.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the RepositoryAttribute:
    ///     <code>
    /// [Repository]
    /// public class OrderRepository
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public sealed class RepositoryPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="RepositoryPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role that the class or interface plays in the Repository pattern.</param>
        public RepositoryPatternAttribute(RepositoryParticipant participant = RepositoryParticipant.Repository) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     Gets the role that the target plays in the Repository pattern.
        /// </summary>
        public RepositoryParticipant Participant { get; }

    }

}