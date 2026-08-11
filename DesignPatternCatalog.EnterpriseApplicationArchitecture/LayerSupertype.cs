#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     LayerSupertype (Patterns of Enterprise Application Architecture) — A type that is the supertype of every
    ///     type in one layer, holding what they all share.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class LayerSupertypeAttribute : LivingDocumentationAttribute { }

}
