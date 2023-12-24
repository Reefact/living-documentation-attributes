﻿#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Proxy design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Proxy pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the ProxyPatternAttribute:
    ///     <code>
    /// [ProxyPattern(ProxyParticipant.Proxy)]
    /// public class MyProxy
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class ProxyPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProxyPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Proxy design pattern.</param>
        public ProxyPatternAttribute(ProxyParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Proxy design pattern.
        /// </summary>
        public ProxyParticipant Participant { get; }

    }

}