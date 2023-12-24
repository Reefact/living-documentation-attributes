#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Memento design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Memento pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the MementoPatternAttribute:
    ///     <code>
    /// [MementoPattern(MementoParticipant.Originator)]
    /// public class ConcreteOriginator
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class MementoPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="MementoPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Memento design pattern.</param>
        public MementoPatternAttribute(MementoParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Memento design pattern.
        /// </summary>
        public MementoParticipant Participant { get; }

    }

}