#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Service design pattern within the Domain-Driven
    ///     Design context.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays the role of a Service.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the ServiceAttribute:
    ///     <code>
    /// [Service]
    /// public class OrderService
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public sealed class ServicePatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServicePatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role that the class or interface plays in the Service pattern.</param>
        public ServicePatternAttribute(ServiceParticipant participant = ServiceParticipant.Service) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     Gets the role that the target plays in the Service pattern.
        /// </summary>
        public ServiceParticipant Participant { get; }

    }

}