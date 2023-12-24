namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Flyweight design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Flyweight pattern, roles commonly include Flyweight and ConcreteFlyweight.
    ///     Use this value as a parameter for the <see cref="FlyweightPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the FlyweightParticipant enum:
    ///     <code>
    /// [FlyweightPattern(FlyweightParticipant.Flyweight)]
    /// public class MyFlyweight
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum FlyweightParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Flyweight.
        /// </summary>
        Flyweight,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a ConcreteFlyweight.
        /// </summary>
        ConcreteFlyweight

    }

}