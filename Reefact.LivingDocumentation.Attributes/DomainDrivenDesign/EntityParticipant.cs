namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Enumeration to define the role played by a class participating in the Entity DDD tactical pattern.
    /// </summary>
    /// <remarks>
    ///     In the Entity pattern, there is generally only one role: that of the Entity itself.
    ///     Use this value as a parameter for the <see cref="EntityPatternAttribute" /> to indicate
    ///     that a class is an Entity.
    /// </remarks>
    public enum EntityParticipant {

        /// <summary>
        ///     Indicates that the class plays the role of an Entity.
        /// </summary>
        Entity

    }

}