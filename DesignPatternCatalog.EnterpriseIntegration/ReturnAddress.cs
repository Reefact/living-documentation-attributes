#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     ReturnAddress (Enterprise Integration Patterns) — Puts the reply channel on the request, so that a replier
    ///     answers where the requestor asked rather than where it was configured to.
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
    public sealed class ReturnAddressAttribute : LivingDocumentationAttribute { }

}
