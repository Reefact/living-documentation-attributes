#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     IdentityField (Patterns of Enterprise Application Architecture) — The database key, carried on the object,
    ///     so that an object in memory and a row in a table can be matched.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class IdentityFieldAttribute : LivingDocumentationAttribute { }

}
