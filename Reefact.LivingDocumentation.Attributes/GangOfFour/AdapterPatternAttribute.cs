#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Adapter design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Adapter pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the AdapterPatternAttribute:
    ///     <code>
    /// [AdapterPattern(AdapterParticipant.Adapter)]
    /// public class MyAdapter
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class AdapterPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdapterPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Adapter design pattern.</param>
        public AdapterPatternAttribute(AdapterParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Adapter design pattern.
        /// </summary>
        public AdapterParticipant Participant { get; }

    }

}