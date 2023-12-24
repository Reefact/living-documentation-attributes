#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Template Method design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Template Method pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the TemplateMethodPatternAttribute:
    ///     <code>
    /// [TemplateMethodPattern(TemplateMethodParticipant.AbstractClass)]
    /// public abstract class AbstractAlgorithm
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class TemplateMethodPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="TemplateMethodPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Template Method design pattern.</param>
        public TemplateMethodPatternAttribute(TemplateMethodParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Template Method design pattern.
        /// </summary>
        public TemplateMethodParticipant Participant { get; }

    }

}