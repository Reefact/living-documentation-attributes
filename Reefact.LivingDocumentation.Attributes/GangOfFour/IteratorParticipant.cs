namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Iterator design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Iterator pattern, roles commonly include Aggregate and Iterator.
    ///     Use this value as a parameter for the <see cref="IteratorPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the IteratorParticipant enum:
    ///     <code>
    /// [IteratorPattern(IteratorParticipant.Iterator)]
    /// public class ConcreteIterator : IIterator
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum IteratorParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Aggregate.
        /// </summary>
        Aggregate,

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Iterator.
        /// </summary>
        Iterator

    }

}