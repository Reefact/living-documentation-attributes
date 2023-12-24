#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Strategy design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Strategy pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the StrategyPatternAttribute:
    ///     <code>
    /// [StrategyPattern(StrategyParticipant.Context)]
    /// public class ConcreteContext
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class StrategyPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="StrategyPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Strategy design pattern.</param>
        public StrategyPatternAttribute(StrategyParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Strategy design pattern.
        /// </summary>
        public StrategyParticipant Participant { get; }

    }

}