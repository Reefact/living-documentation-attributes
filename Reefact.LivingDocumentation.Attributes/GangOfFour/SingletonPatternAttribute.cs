#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Singleton design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class is intended to be a Singleton.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the SingletonPatternAttribute:
    ///     <code>
    /// [SingletonPattern]
    /// public class MySingleton
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SingletonPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="SingletonPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Singleton design pattern.</param>
        public SingletonPatternAttribute(SingletonParticipant participant = SingletonParticipant.Singleton) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Singleton design pattern.
        /// </summary>
        public SingletonParticipant Participant { get; }

    }

}