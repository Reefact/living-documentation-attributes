#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Decorator design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Decorator pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the DecoratorPatternAttribute:
    ///     <code>
    /// [DecoratorPattern(DecoratorParticipant.Decorator)]
    /// public class MyDecorator
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class DecoratorPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="DecoratorPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Decorator design pattern.</param>
        public DecoratorPatternAttribute(DecoratorParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Decorator design pattern.
        /// </summary>
        public DecoratorParticipant Participant { get; }

    }

}