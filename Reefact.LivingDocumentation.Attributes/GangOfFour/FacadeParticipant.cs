namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Façade design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Façade pattern, there is generally only one role: that of the Façade itself.
    ///     Use this value as a parameter for the <see cref="FacadePatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the FacadeParticipant enum:
    ///     <code>
    /// [FacadePattern(FacadeParticipant.Facade)]
    /// public class MyFacade
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum FacadeParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Façade.
        /// </summary>
        Facade

    }

}