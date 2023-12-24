namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the roles played by classes or interfaces participating in the Visitor design pattern.
    /// </summary>
    /// <remarks>
    ///     Use these values as parameters for the<see cref="VisitorPatternAttribute" /> to indicate
    ///     the specific role a class or interface plays within the Visitor pattern.
    /// </remarks>
    public enum VisitorParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Visitor.
        /// </summary>
        Visitor,
        /// <summary>
        ///     Indicates that the class or interface plays the role of a Concrete Visitor.
        /// </summary>
        ConcreteVisitor,
        /// <summary>
        ///     Indicates that the class or interface plays the role of an Element.
        /// </summary>
        Element,
        /// <summary>
        ///     Indicates that the class or interface plays the role of a Concrete Element.
        /// </summary>
        ConcreteElement

    }

}