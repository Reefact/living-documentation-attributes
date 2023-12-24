namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Attribute to mark classes or interfaces as participating in the Value Object DDD tactical pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a class or interface plays a specific role in the Value Object pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    public class ValueObjectPatternAttribute : LivingDocumentationAttribute {

        #region Constructors declarations

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValueObjectPatternAttribute" /> class.
        /// </summary>
        /// <param name="participant">The role that the class or interface plays in the Value Object pattern.</param>
        public ValueObjectPatternAttribute(ValueObjectParticipant participant = ValueObjectParticipant.ValueObject) {
            Participant = participant;
        }

        #endregion

        /// <summary>
        ///     Gets the role that the target plays in the Value Object pattern.
        /// </summary>
        public ValueObjectParticipant Participant { get; }

    }

}