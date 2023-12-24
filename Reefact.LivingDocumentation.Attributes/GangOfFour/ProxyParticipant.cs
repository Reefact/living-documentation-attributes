namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Enumeration to define the role played by a class or interface participating in the Proxy design pattern.
    /// </summary>
    /// <remarks>
    ///     In the Proxy pattern, roles commonly include Proxy, RealSubject, and Subject.
    ///     Use this value as a parameter for the <see cref="ProxyPatternAttribute" /> to indicate
    ///     the role a class or interface plays.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the ProxyParticipant enum:
    ///     <code>
    /// [ProxyPattern(ProxyParticipant.Proxy)]
    /// public class MyProxy
    /// {
    ///     // Implementation details
    /// }
    /// </code>
    /// </example>
    public enum ProxyParticipant {

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Proxy.
        /// </summary>
        Proxy,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a RealSubject.
        /// </summary>
        RealSubject,

        /// <summary>
        ///     Indicates that the class or interface plays the role of a Subject.
        /// </summary>
        Subject

    }

}