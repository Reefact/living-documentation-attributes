#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     ValueObject (Domain-Driven Design) — An object of the domain described only by its values. It carries no
    ///     identity, it is immutable, and two instances holding the same values are interchangeable.
    /// </summary>
    /// <remarks>
    ///     This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class ValueObjectAttribute : LivingDocumentationAttribute {

        /// <inheritdoc />
        public override string Catalog => "DomainDrivenDesign";

        /// <inheritdoc />
        public override string PatternName => "ValueObject";

        /// <inheritdoc />
        public override string RoleName => "ValueObject";

    }

}
