#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark classes or interfaces participating in the {patternName} design pattern.
    /// </summary>
    /// < remarks>
    ///     Use this attribute to label the role played by the class or interface in the pattern -
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the VisitorPatternAttribute:
    ///     <code>
    /// [VisitorPattern(VisitorParticipant.ConcreteVisitor)]
    /// public class MyConcreteVisitor : IVisitor
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="VisitorParticipant" />
    public class VisitorPatternAttribute : LivingDocumentationAttribute { }

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public sealed class VisitorParticipantAttribute : VisitorPatternAttribute {

        #region Constructors declarations

        public VisitorParticipantAttribute(VisitorParticipant participant) {
            Participant = participant;
        }

        #endregion

        public VisitorParticipant Participant { get; }

    }

}