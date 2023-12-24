#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Attribute to mark classes or interface participating in the Domain Events design pattern in the context of
    ///     Domain-Driven Design.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays the role of a Domain Event.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the DomainEventAttribute:
    ///     <code>
    /// [DomainEvent]
    /// public class OrderCreated
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public sealed class DomainEventPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        public DomainEventPatternAttribute(DomainEventParticipant participant = DomainEventParticipant.DomainEvent) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     Gets the role of the class in the context of Domain Events.
        /// </summary>
        public DomainEventParticipant Participant { get; }

    }

}