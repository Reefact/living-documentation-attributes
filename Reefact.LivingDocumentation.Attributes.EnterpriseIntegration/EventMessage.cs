#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseIntegration {

    /// <summary>
    ///     EventMessage (Enterprise Integration Patterns) — Announces that something has happened, so that the sender
    ///     is relieved of knowing who cares.
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
    public sealed class EventMessageAttribute : Message.Role { }

}
