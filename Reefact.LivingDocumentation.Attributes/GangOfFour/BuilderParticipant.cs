namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the roles played by a class or interface participating in the Builder design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Builder pattern, there are generally four roles: Builder, ConcreteBuilder, Product, and Director.
    ///     Use this value as a parameter for the <see cref="BuilderPatternAttribute" /> to indicate
    ///     the role played by the class or interface.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the BuilderParticipant enum:
    ///     <code>
    /// [BuilderPattern(BuilderParticipant.Builder)]
    /// public interface IOrderBuilder
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum BuilderParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Builder, defining the interface for creating parts of a
        ///     product.
        /// </summary>
        Builder,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a ConcreteBuilder, implementing the Builder interface to
        ///     construct and assemble parts of the product.
        /// </summary>
        ConcreteBuilder,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Product, representing the complex object being assembled.
        /// </summary>
        Product,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Director, responsible for constructing a product using
        ///     the Builder interface.
        /// </summary>
        Director

    }

}