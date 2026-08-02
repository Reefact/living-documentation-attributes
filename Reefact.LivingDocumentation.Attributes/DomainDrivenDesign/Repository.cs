#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Repository (Domain-Driven Design) — Gives access to aggregates as though they were an in-memory collection,
    ///     and hides the storage mechanism from the domain.
    /// </summary>
    /// <remarks>
    ///     This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class RepositoryAttribute : LivingDocumentationAttribute {

        /// <inheritdoc />
        public override string Catalog => "DomainDrivenDesign";

        /// <inheritdoc />
        public override string PatternName => "Repository";

        /// <inheritdoc />
        public override string RoleName => "Repository";

    }

}
