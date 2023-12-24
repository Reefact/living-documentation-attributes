#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces participating in the Abstract Factory design pattern in the context of Gang
    ///     of Four.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate the role that a class or interface plays in the Abstract Factory pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     <code>
    /// [AbstractFactoryPattern(AbstractFactoryParticipant.AbstractFactory)]
    /// public interface IGuiFactory
    /// {
    ///     // Factory methods
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="AbstractFactoryParticipant" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class AbstractFactoryPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbstractFactoryPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">Role played by the class or interface in the Abstract Factory pattern.</param>
        public AbstractFactoryPatternAttribute(AbstractFactoryParticipant participant) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     Gets the role played by the class or interface in the Abstract Factory pattern.
        /// </summary>
        public AbstractFactoryParticipant Participant { get; }

    }

}