#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     TransactionScript (Patterns of Enterprise Application Architecture) — One procedure that handles one request
    ///     from the presentation, from start to finish.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class TransactionScriptAttribute : LivingDocumentationAttribute { }

}
