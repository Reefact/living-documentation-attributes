#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EntrepriseApplicationArchitecture {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the UnitOfWork design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the UnitOfWork pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class UnitOfWorkPatternAttribute : Attribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitOfWorkPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role that the class or interface plays in the UnitOfWork pattern.</param>
        public UnitOfWorkPatternAttribute(UnitOfWorkParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     Gets the role that the target plays in the UnitOfWork pattern.
        /// </summary>
        public UnitOfWorkParticipant Participant { get; }

    }

}