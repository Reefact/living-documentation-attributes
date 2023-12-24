namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Decorator design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Decorator pattern, there are generally two roles: Component and Decorator.
    ///     Use this value as a parameter for the <see cref="DecoratorPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the DecoratorParticipant enum:
    ///     <code>
    /// [DecoratorPattern(DecoratorParticipant.Decorator)]
    /// public class MyDecorator
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum DecoratorParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Component.
        /// </summary>
        Component,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Decorator.
        /// </summary>
        Decorator

    }

}