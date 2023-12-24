namespace Reefact.LivingDocumentation.Attributes.EntrepriseApplicationArchitecture {

    /// <summary>
    ///     Enumeration to define the role played by a class participating in the UnitOfWork design pattern.
    /// </summary>
    public enum UnitOfWorkParticipant
    {
        /// <summary>
        ///     Indicates that the class plays the role of a UnitOfWork.
        /// </summary>
        UnitOfWork,

        /// <summary>
        ///     Indicates that the class plays the role of a ConcreteUnitOfWork.
        /// </summary>
        ConcreteUnitOfWork
    }

}