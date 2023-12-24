#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Flyweight design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Flyweight pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the FlyweightPatternAttribute:
    ///     <code>
    /// [FlyweightPattern(FlyweightParticipant.Flyweight)]
    /// public class MyFlyweight
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class FlyweightPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="FlyweightPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Flyweight design pattern.</param>
        public FlyweightPatternAttribute(FlyweightParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Flyweight design pattern.
        /// </summary>
        public FlyweightParticipant Participant { get; }

    }

}