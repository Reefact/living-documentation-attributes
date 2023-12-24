#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Factory Method design pattern.
    /// </summary>
    /// <remarks>
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the FactoryMethodPatternAttribute:
    ///     <code>
    /// [FactoryMethodPattern(FactoryMethodParticipant.Creator)]
    /// public abstract class Dialog
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class FactoryMethodPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="FactoryMethodPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role played by the annotated class or interface in the Factory Method pattern.</param>
        public FactoryMethodPatternAttribute(FactoryMethodParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     Gets the role played by the class or interface in the Factory Method design pattern.
        /// </summary>
        public FactoryMethodParticipant Participant { get; }

    }

}