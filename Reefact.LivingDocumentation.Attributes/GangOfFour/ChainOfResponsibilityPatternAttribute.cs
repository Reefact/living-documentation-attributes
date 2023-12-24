#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Chain of Responsibility design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Chain of Responsibility
    ///     pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the ChainOfResponsibilityPatternAttribute:
    ///     <code>
    /// [ChainOfResponsibilityPattern(ChainOfResponsibilityParticipant.Handler)]
    /// public class MyHandler
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class ChainOfResponsibilityPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChainOfResponsibilityPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Chain of Responsibility design pattern.</param>
        public ChainOfResponsibilityPatternAttribute(ChainOfResponsibilityParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Chain of Responsibility design pattern.
        /// </summary>
        public ChainOfResponsibilityParticipant Participant { get; }

    }

}