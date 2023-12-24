#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Iterator design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Iterator pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the IteratorPatternAttribute:
    ///     <code>
    /// [IteratorPattern(IteratorParticipant.Iterator)]
    /// public class ConcreteIterator : IIterator
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class IteratorPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="IteratorPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Iterator design pattern.</param>
        public IteratorPatternAttribute(IteratorParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Iterator design pattern.
        /// </summary>
        public IteratorParticipant Participant { get; }

    }

}