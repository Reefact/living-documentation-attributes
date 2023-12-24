namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Strategy design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Strategy pattern, roles commonly include Context, Strategy, and ConcreteStrategy.
    ///     Use this value as a parameter for the <see cref="StrategyPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the StrategyParticipant enum:
    ///     <code>
    /// [StrategyPattern(StrategyParticipant.Context)]
    /// public class ConcreteContext
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum StrategyParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Context.
        /// </summary>
        Context,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Strategy.
        /// </summary>
        Strategy,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a ConcreteStrategy.
        /// </summary>
        ConcreteStrategy

    }

}