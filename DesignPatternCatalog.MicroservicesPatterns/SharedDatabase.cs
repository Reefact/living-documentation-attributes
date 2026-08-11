#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     SharedDatabase (Microservices Patterns) — Lets several services read and write one database, so that a
    ///     transaction spanning them is an ordinary local one — and so that none of them can change a table alone.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class SharedDatabaseAttribute : LivingDocumentationAttribute { }

}
