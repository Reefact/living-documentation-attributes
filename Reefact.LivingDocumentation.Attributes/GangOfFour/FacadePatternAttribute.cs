#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Façade design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Façade pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the FacadePatternAttribute:
    ///     <code>
    /// [FacadePattern(FacadeParticipant.Facade)]
    /// public class MyFacade
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class FacadePatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="FacadePatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role the class or interface plays within the Façade design pattern.</param>
        public FacadePatternAttribute(FacadeParticipant participant = FacadeParticipant.Facade) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     The role the class or interface plays within the Façade design pattern.
        /// </summary>
        public FacadeParticipant Participant { get; }

    }

}