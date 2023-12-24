namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Chain of Responsibility design
    ///     pattern.
    /// </summary>
    /// <remarks>
    ///     In the Chain of Responsibility pattern, roles commonly include Handler and ConcreteHandler.
    ///     Use this value as a parameter for the <see cref="ChainOfResponsibilityPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the ChainOfResponsibilityParticipant enum:
    ///     <code>
    /// [ChainOfResponsibilityPattern(ChainOfResponsibilityParticipant.ConcreteHandler)]
    /// public class MyConcreteHandler : MyHandler
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum ChainOfResponsibilityParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Handler.
        /// </summary>
        Handler,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a ConcreteHandler.
        /// </summary>
        ConcreteHandler

    }

}