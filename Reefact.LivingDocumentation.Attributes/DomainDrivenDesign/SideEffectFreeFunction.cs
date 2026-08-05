#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     SideEffectFreeFunction (Domain-Driven Design) — An operation that computes and returns a result while
    ///     leaving the state of the system untouched, so that it can be called freely, repeated, and combined without
    ///     reasoning about order.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class SideEffectFreeFunctionAttribute : LivingDocumentationAttribute { }

}
