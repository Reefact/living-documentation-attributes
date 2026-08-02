#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Specification (Domain-Driven Design) — States a predicate of the domain as an explicit object, so that a
    ///     business rule can be named, combined and reused.
    /// </summary>
    /// <remarks>
    ///     This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class SpecificationAttribute : LivingDocumentationAttribute {

        /// <inheritdoc />
        public override string Catalog => "DomainDrivenDesign";

        /// <inheritdoc />
        public override string PatternName => "Specification";

        /// <inheritdoc />
        public override string RoleName => "Specification";

    }

}
