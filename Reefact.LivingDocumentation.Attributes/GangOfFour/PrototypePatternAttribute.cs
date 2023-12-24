#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Prototype design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Prototype pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the PrototypePatternAttribute:
    ///     <code>
    /// [PrototypePattern(PrototypeParticipant.Prototype)]
    /// public class MyPrototype
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class PrototypePatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="PrototypePatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Prototype design pattern.</param>
        public PrototypePatternAttribute(PrototypeParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Prototype design pattern.
        /// </summary>
        public PrototypeParticipant Participant { get; }

    }

}