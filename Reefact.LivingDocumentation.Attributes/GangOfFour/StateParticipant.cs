namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the State design pattern.
    /// </summary>
    /// <remarks>
    ///     In the State pattern, roles commonly include Context and ConcreteState.
    ///     Use this value as a parameter for the <see cref="StatePatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the StateParticipant enum:
    ///     <code>
    /// [StatePattern(StateParticipant.Context)]
    /// public class ConcreteContext
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum StateParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Context.
        /// </summary>
        Context,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a ConcreteState.
        /// </summary>
        ConcreteState

    }

}