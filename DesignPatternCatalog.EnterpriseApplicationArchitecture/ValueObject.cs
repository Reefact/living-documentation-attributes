#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     ValueObject (Patterns of Enterprise Application Architecture) — A small object, such as an amount of money
    ///     or a date range, whose equality is not based on identity but on the values it holds, and which is therefore
    ///     safe to copy and to share.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class ValueObjectAttribute : LivingDocumentationAttribute { }

}
