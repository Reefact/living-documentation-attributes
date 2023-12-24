namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Memento design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Memento pattern, roles commonly include Originator, Memento, and Caretaker.
    ///     Use this value as a parameter for the <see cref="MementoPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the MementoParticipant enum:
    ///     <code>
    /// [MementoPattern(MementoParticipant.Originator)]
    /// public class ConcreteOriginator
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum MementoParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Originator.
        /// </summary>
        Originator,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Memento.
        /// </summary>
        Memento,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Caretaker.
        /// </summary>
        Caretaker

    }

}