#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     PessimisticOfflineLock (Patterns of Enterprise Application Architecture) — A conflict between two long
    ///     transactions is prevented, by letting only one of them hold the data.
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
    public sealed class PessimisticOfflineLockAttribute : LivingDocumentationAttribute { }

}
