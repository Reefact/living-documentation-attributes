#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     DatabaseSessionState (Patterns of Enterprise Application Architecture) — Session state kept in the database,
    ///     so that any process can serve any request.
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
    public sealed class DatabaseSessionStateAttribute : LivingDocumentationAttribute { }

}
