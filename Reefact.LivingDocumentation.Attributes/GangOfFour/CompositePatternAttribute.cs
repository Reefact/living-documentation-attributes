#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Composite design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Composite pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the CompositePatternAttribute:
    ///     <code>
    /// [CompositePattern(CompositeParticipant.Composite)]
    /// public class MyComposite
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class CompositePatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="CompositePatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Composite design pattern.</param>
        public CompositePatternAttribute(CompositeParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Composite design pattern.
        /// </summary>
        public CompositeParticipant Participant { get; }

    }

}