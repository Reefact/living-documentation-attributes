namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Entity DDD tactical pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Entity pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    public class EntityPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role that the class or interface plays in the Entity pattern.</param>
        public EntityPatternAttribute(EntityParticipant participant = EntityParticipant.Entity) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     Gets the role that the target plays in the Entity pattern.
        /// </summary>
        public EntityParticipant Participant { get; }

    }

}