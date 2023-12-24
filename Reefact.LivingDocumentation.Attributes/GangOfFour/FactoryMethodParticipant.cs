namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the roles played by a class or interface participating in the Factory Method design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Factory Method pattern, there are generally four roles: Creator, ConcreteCreator, Product, and
    ///     ConcreteProduct.
    ///     Use this value as a parameter for the <see cref="FactoryMethodPatternAttribute" /> to indicate
    ///     the role played by the class or interface.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the FactoryMethodParticipant enum:
    ///     <code>
    /// [FactoryMethodPattern(FactoryMethodParticipant.Creator)]
    /// public abstract class Dialog
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum FactoryMethodParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Creator, defining a factory method for creating objects.
        /// </summary>
        Creator,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a ConcreteCreator, implementing the factory method to
        ///     return an instance of a ConcreteProduct.
        /// </summary>
        ConcreteCreator,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Product, defining the interface for objects the factory
        ///     method creates.
        /// </summary>
        Product,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a ConcreteProduct, implementing the Product interface.
        /// </summary>
        ConcreteProduct

    }

}