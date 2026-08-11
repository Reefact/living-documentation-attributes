#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     MessagingBridge (Enterprise Integration Patterns) — Connects two messaging systems so that a message
    ///     available on one is available on the other, without either being aware of the other.
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
    public sealed class MessagingBridgeAttribute : LivingDocumentationAttribute { }

}
