#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     StandaloneClass (Domain-Driven Design) — A type that can be understood, tested and reasoned about entirely
    ///     on its own, because it depends on nothing beyond primitives and the values it is given.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class StandaloneClassAttribute : LivingDocumentationAttribute { }

}
