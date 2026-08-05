#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Entity (Domain-Driven Design) — An object of the domain defined by a thread of continuity and identity,
    ///     rather than by its attributes: two entities with equal attributes remain distinct.
    /// </summary>
    /// <remarks>
    ///     This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class EntityAttribute : LivingDocumentationAttribute { }

}
