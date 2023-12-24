#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Interpreter design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Interpreter pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the InterpreterPatternAttribute:
    ///     <code>
    /// [InterpreterPattern(InterpreterParticipant.AbstractExpression)]
    /// public abstract class AbstractExpression
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class InterpreterPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="InterpreterPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Interpreter design pattern.</param>
        public InterpreterPatternAttribute(InterpreterParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Interpreter design pattern.
        /// </summary>
        public InterpreterParticipant Participant { get; }

    }

}