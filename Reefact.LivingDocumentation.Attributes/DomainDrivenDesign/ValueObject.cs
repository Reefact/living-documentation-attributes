#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     ValueObject (Domain-Driven Design) — An object of the domain described only by its values. It carries no
    ///     identity, it is immutable, and two instances holding the same values are interchangeable.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         The same pattern as ValueObject, in Patterns of Enterprise Application Architecture, which published it
    ///         first and holds its definition. Written from either catalog, an annotation resolves to that one identity
    ///         — so a reader of this catalog finds the pattern where it looks for it, without the two spellings
    ///         drifting apart.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    // AttributeUsage is inherited from the pattern this one derives from, on purpose: the two spellings cannot end up accepting different targets.
    public sealed class ValueObjectAttribute : EnterpriseApplicationArchitecture.ValueObjectAttribute { }

}
