#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Mediator design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Mediator pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the MediatorPatternAttribute:
    ///     <code>
    /// [MediatorPattern(MediatorParticipant.Mediator)]
    /// public class ConcreteMediator : IMediator
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class MediatorPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediatorPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Mediator design pattern.</param>
        public MediatorPatternAttribute(MediatorParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Mediator design pattern.
        /// </summary>
        public MediatorParticipant Participant { get; }

    }

}