namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Mediator design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Mediator pattern, roles commonly include Mediator and Colleague.
    ///     Use this value as a parameter for the <see cref="MediatorPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the MediatorParticipant enum:
    ///     <code>
    /// [MediatorPattern(MediatorParticipant.Mediator)]
    /// public class ConcreteMediator : IMediator
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum MediatorParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Mediator.
        /// </summary>
        Mediator,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Colleague.
        /// </summary>
        Colleague

    }

}