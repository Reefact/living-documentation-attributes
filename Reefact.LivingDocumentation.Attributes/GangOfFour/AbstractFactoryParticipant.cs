namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the roles played by classes or interfaces participating in the Abstract Factory design
    ///     pattern.
    /// </summary>
    /// <remarks>
    ///     Use these values as parameters for the <see cref="AbstractFactoryPatternAttribute" /> to indicate
    ///     the specific role a class or interface plays within the Abstract Factory pattern.
    /// </remarks>
    /// <example>
    ///     <code>
    /// [AbstractFactoryPattern(AbstractFactoryParticipant.AbstractFactory)]
    /// public interface IGuiFactory
    /// {
    ///     // Factory methods
    /// }
    /// </code>
    /// </example>
    public enum AbstractFactoryParticipant {

        #region Statics members declarations

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Abstract Factory.
        /// </summary>
        AbstractFactory,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Concrete Factory.
        /// </summary>
        ConcreteFactory,

        /// <summary>
        ///     Indicates that the class or interface plays the role of an Abstract Product.
        /// </summary>
        AbstractProduct,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Concrete Product.
        /// </summary>
        ConcreteProduct

        #endregion

    }

}