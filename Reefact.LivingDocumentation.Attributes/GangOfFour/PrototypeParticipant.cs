namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Prototype design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Prototype pattern, there are generally two roles: Prototype and ConcretePrototype.
    ///     Use this value as a parameter for the <see cref="PrototypePatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the PrototypeParticipant enum:
    ///     <code>
    /// [PrototypePattern(PrototypeParticipant.Prototype)]
    /// public class MyPrototype
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum PrototypeParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Prototype.
        /// </summary>
        Prototype,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a ConcretePrototype.
        /// </summary>
        ConcretePrototype

    }

}