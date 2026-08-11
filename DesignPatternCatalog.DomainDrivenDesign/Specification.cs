#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DomainDrivenDesign {

    /// <summary>
    ///     Specification (Domain-Driven Design) — States a predicate of the domain as an explicit object, so that a
    ///     business rule can be named, combined and reused.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class SpecificationAttribute : LivingDocumentationAttribute { }

}
