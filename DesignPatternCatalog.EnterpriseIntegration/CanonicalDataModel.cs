#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     CanonicalDataModel (Enterprise Integration Patterns) — A message format belonging to no application, which
    ///     every application translates to and from, so that adding one application costs one translation rather than
    ///     one per correspondent.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class CanonicalDataModelAttribute : LivingDocumentationAttribute { }

}
