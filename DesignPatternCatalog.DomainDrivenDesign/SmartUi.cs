#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DomainDrivenDesign {

    /// <summary>
    ///     SmartUi (Domain-Driven Design) — Puts the business rules into the user interface itself, one screen at a
    ///     time, and keeps no model at all — named by Evans as the anti-pattern, and presented with the circumstances
    ///     under which it is nonetheless the right choice.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class SmartUiAttribute : LivingDocumentationAttribute { }

}
