﻿#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the State design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the State pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the StatePatternAttribute:
    ///     <code>
    /// [StatePattern(StateParticipant.Context)]
    /// public class ConcreteContext
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class StatePatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatePatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the State design pattern.</param>
        public StatePatternAttribute(StateParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the State design pattern.
        /// </summary>
        public StateParticipant Participant { get; }

    }

}