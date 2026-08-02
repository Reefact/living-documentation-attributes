#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     DomainEvent (Domain-Driven Design) — States that something meaningful to the domain has happened. It is
    ///     named in the past tense, and it is immutable once raised.
    /// </summary>
    /// <remarks>
    ///     This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class DomainEventAttribute : LivingDocumentationAttribute {

        /// <inheritdoc />
        public override string Catalog => "DomainDrivenDesign";

        /// <inheritdoc />
        public override string PatternName => "DomainEvent";

        /// <inheritdoc />
        public override string RoleName => "DomainEvent";

    }

}
