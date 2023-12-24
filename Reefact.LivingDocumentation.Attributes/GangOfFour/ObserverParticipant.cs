namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Observer design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Observer pattern, roles commonly include Subject and Observer.
    ///     Use this value as a parameter for the <see cref="ObserverPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the ObserverParticipant enum:
    ///     <code>
    /// [ObserverPattern(ObserverParticipant.Subject)]
    /// public class ConcreteSubject
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum ObserverParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Subject.
        /// </summary>
        Subject,

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Observer.
        /// </summary>
        Observer

    }

}