#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     MessageExpiration (Enterprise Integration Patterns) — Says when a message stops being worth acting on, so
    ///     that a stale instruction is discarded rather than obeyed late.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class MessageExpirationAttribute : LivingDocumentationAttribute { }

}
