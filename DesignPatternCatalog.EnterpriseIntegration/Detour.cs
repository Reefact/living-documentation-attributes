#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     Detour (Enterprise Integration Patterns) — Sends messages through extra steps or straight to their
    ///     destination depending on a switch the control bus holds, so that validation or debugging can be turned on
    ///     without a deployment.
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
    public sealed class DetourAttribute : LivingDocumentationAttribute { }

}
