#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DomainDrivenDesign {

    /// <summary>
    ///     ValueObject (Domain-Driven Design) — An object of the domain described only by its values. It carries no
    ///     identity, it is immutable, and it exists because it says something about the domain — not merely because
    ///     comparing it by value is convenient.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class ValueObjectAttribute : LivingDocumentationAttribute { }

}
