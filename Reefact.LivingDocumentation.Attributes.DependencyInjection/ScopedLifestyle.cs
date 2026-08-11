#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DependencyInjection {

    /// <summary>
    ///     ScopedLifestyle (Dependency Injection Principles, Practices, and Patterns) — One instance serves a well-
    ///     defined scope — a web request, a unit of work — and a different one serves the next.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Steven van Deursen, Mark Seemann, <i>Dependency Injection Principles, Practices, and Patterns</i>, 2019.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class ScopedLifestyleAttribute : LivingDocumentationAttribute { }

}
