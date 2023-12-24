#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Bridge design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Bridge pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the BridgePatternAttribute:
    ///     <code>
    /// [BridgePattern(BridgeParticipant.Abstraction)]
    /// public class MyAbstraction
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class BridgePatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="BridgePatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Bridge design pattern.</param>
        public BridgePatternAttribute(BridgeParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Bridge design pattern.
        /// </summary>
        public BridgeParticipant Participant { get; }

    }

}