#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DependencyInjection {

    /// <summary>
    ///     AmbientContext (Dependency Injection Principles, Practices, and Patterns) — Exposes a dependency through a
    ///     static access point that any code may reach, so that it is passed to nobody and available to everybody.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Steven van Deursen, Mark Seemann, <i>Dependency Injection Principles, Practices, and Patterns</i>, 2019.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class AmbientContextAttribute : LivingDocumentationAttribute { }

}
