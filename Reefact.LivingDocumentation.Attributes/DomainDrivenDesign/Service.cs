#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     Service (Domain-Driven Design) — An operation of the domain that does not naturally belong to an entity or
    ///     to a value object, exposed as a standalone, stateless interface.
    /// </summary>
    /// <remarks>
    ///     This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ServiceAttribute : LivingDocumentationAttribute { }

}
