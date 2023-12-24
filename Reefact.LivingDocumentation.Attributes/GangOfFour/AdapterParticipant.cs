namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Adapter design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Adapter pattern, there are generally three roles: Target, Adapter, and Adaptee.
    ///     Use this value as a parameter for the <see cref="AdapterPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the AdapterParticipant enum:
    ///     <code>
    /// [AdapterPattern(AdapterParticipant.Adapter)]
    /// public class MyAdapter
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum AdapterParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Target.
        /// </summary>
        Target,

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Adapter.
        /// </summary>
        Adapter,

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Adaptee.
        /// </summary>
        Adaptee

    }

}