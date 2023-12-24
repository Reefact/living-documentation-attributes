namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Composite design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Composite pattern, there are generally two roles: Leaf and Composite.
    ///     Use this value as a parameter for the <see cref="CompositePatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the CompositeParticipant enum:
    ///     <code>
    /// [CompositePattern(CompositeParticipant.Composite)]
    /// public class MyComposite
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum CompositeParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Leaf.
        /// </summary>
        Leaf,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Composite.
        /// </summary>
        Composite

    }

}