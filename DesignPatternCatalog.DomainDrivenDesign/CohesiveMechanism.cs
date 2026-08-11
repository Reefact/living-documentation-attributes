#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DomainDrivenDesign {

    /// <summary>
    ///     CohesiveMechanism (Domain-Driven Design) — Separates a self-contained piece of machinery — an algorithm, a
    ///     formalism, a solver — from the model that needs it, so that the model states what it wants and not how the
    ///     answer is computed.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class CohesiveMechanismAttribute : LivingDocumentationAttribute { }

}
