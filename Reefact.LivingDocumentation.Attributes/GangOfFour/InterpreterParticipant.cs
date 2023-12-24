namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Interpreter design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Interpreter pattern, roles commonly include AbstractExpression, TerminalExpression, and Context.
    ///     Use this value as a parameter for the <see cref="InterpreterPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the InterpreterParticipant enum:
    ///     <code>
    /// [InterpreterPattern(InterpreterParticipant.AbstractExpression)]
    /// public abstract class AbstractExpression
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum InterpreterParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of an AbstractExpression.
        /// </summary>
        AbstractExpression,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a TerminalExpression.
        /// </summary>
        TerminalExpression,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Context.
        /// </summary>
        Context

    }

}