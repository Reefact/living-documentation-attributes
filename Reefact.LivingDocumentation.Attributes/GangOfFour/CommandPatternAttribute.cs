#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Command design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Command pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the CommandPatternAttribute:
    ///     <code>
    /// [CommandPattern(CommandParticipant.Command)]
    /// public class MyCommand
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class CommandPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Command design pattern.</param>
        public CommandPatternAttribute(CommandParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Command design pattern.
        /// </summary>
        public CommandParticipant Participant { get; }

    }

}