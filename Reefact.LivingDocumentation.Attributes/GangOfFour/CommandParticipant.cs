namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Command design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Command pattern, roles commonly include Command, Receiver, and Invoker.
    ///     Use this value as a parameter for the <see cref="CommandPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the CommandParticipant enum:
    ///     <code>
    /// [CommandPattern(CommandParticipant.Command)]
    /// public class MyCommand
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum CommandParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Command.
        /// </summary>
        Command,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Receiver.
        /// </summary>
        Receiver,

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Invoker.
        /// </summary>
        Invoker

    }

}