#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DependencyInjection {

    /// <summary>
    ///     PropertyInjection (Dependency Injection Principles, Practices, and Patterns) — Exposes a settable property
    ///     through which an optional dependency may be supplied, the class remaining usable when it is not.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Steven van Deursen, Mark Seemann, <i>Dependency Injection Principles, Practices, and Patterns</i>, 2019.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class PropertyInjectionAttribute : LivingDocumentationAttribute { }

}
