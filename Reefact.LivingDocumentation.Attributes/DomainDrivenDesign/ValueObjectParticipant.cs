namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Enumeration to define the role played by a class participating in the Value Object DDD tactical pattern.
    /// </summary>
    /// <remarks>
    ///     In the Value Object pattern, there is generally only one role: that of the Value Object itself.
    ///     Use this value as a parameter for the <see cref="ValueObjectPatternAttribute" /> to indicate
    ///     that a class is a Value Object.
    /// </remarks>
    public enum ValueObjectParticipant {

        /// <summary>
        ///     Indicates that the class plays the role of a Value Object.
        /// </summary>
        ValueObject

    }

}