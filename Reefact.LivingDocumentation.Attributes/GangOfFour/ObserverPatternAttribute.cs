#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Observer design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Observer pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the ObserverPatternAttribute:
    ///     <code>
    /// [ObserverPattern(ObserverParticipant.Subject)]
    /// public class ConcreteSubject
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class ObserverPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObserverPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Observer design pattern.</param>
        public ObserverPatternAttribute(ObserverParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Observer design pattern.
        /// </summary>
        public ObserverParticipant Participant { get; }

    }

}