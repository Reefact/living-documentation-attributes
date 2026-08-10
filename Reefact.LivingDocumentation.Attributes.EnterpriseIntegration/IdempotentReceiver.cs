#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseIntegration {

    /// <summary>
    ///     IdempotentReceiver (Enterprise Integration Patterns) — Can receive the same message twice without doing it
    ///     twice, so that a sender is free to resend whenever it is unsure the first one arrived.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class IdempotentReceiverAttribute : LivingDocumentationAttribute { }

}
