#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     CommandMessage (Enterprise Integration Patterns) — Carries an instruction to do something, so that invoking
    ///     a procedure in another application is a message rather than a call.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of Message: every participant annotated here is one of those too, and a consumer asking
    ///         for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class CommandMessageAttribute : Message.Role { }

}
