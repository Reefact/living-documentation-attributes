#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     SeparatedInterface (Patterns of Enterprise Application Architecture) — An interface declared apart from its
    ///     implementation, so that a client depends on the first without the second.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public sealed class SeparatedInterfaceAttribute : LivingDocumentationAttribute { }

}
