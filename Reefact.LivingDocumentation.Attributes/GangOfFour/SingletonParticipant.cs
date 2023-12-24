namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class participating in the Singleton design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Singleton pattern, there is generally only one role: that of the Singleton itself.
    ///     Use this value as a parameter for the <see cref="SingletonPatternAttribute" /> to indicate
    ///     that a class is a Singleton.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the SingletonParticipant enum:
    ///     <code>
    /// [SingletonPattern(SingletonParticipant.Singleton)]
    /// public class MySingleton
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum SingletonParticipant {

        /// <summary>
        ///     Indicates that the class plays the role of a Singleton.
        /// </summary>
        Singleton

    }

}