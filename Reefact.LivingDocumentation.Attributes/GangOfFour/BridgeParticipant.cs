namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Bridge design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Bridge pattern, there are generally two roles: Abstraction and Implementor.
    ///     Use this value as a parameter for the <see cref="BridgePatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the BridgeParticipant enum:
    ///     <code>
    /// [BridgePattern(BridgeParticipant.Abstraction)]
    /// public class MyAbstraction
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum BridgeParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Abstraction.
        /// </summary>
        Abstraction,

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Implementor.
        /// </summary>
        Implementor

    }

}