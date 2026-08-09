#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     Money (Patterns of Enterprise Application Architecture) — Makes an amount and its currency one value with
    ///     arithmetic of its own, so that a number whose meaning depends on a currency cannot be added to one that
    ///     means something else.
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
    public sealed class MoneyAttribute : LivingDocumentationAttribute { }

}
