#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     UnitOfWork (Patterns of Enterprise Application Architecture) — Keeps track of everything done during a
    ///     business transaction that affects the store, and coordinates the writing out of the changes.
    /// </summary>
    /// <remarks>
    ///     This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class UnitOfWorkAttribute : LivingDocumentationAttribute { }

}
