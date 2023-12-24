#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Builder design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Builder pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the BuilderPatternAttribute:
    ///     <code>
    /// [BuilderPattern(BuilderParticipant.Builder)]
    /// public interface IOrderBuilder
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class BuilderPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="BuilderPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role played by the annotated class or interface in the Builder pattern.</param>
        public BuilderPatternAttribute(BuilderParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     Gets the role played by the class or interface in the Builder design pattern.
        /// </summary>
        public BuilderParticipant Participant { get; }

    }

}