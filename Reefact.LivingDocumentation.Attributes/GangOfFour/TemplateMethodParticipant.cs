namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Template Method design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Template Method pattern, roles commonly include AbstractClass and ConcreteClass.
    ///     Use this value as a parameter for the <see cref="TemplateMethodPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the TemplateMethodParticipant enum:
    ///     <code>
    /// [TemplateMethodPattern(TemplateMethodParticipant.AbstractClass)]
    /// public abstract class AbstractAlgorithm
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum TemplateMethodParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of an AbstractClass.
        /// </summary>
        AbstractClass,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a ConcreteClass.
        /// </summary>
        ConcreteClass

    }

}