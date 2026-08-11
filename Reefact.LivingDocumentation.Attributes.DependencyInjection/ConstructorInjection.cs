#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DependencyInjection {

    /// <summary>
    ///     ConstructorInjection (Dependency Injection Principles, Practices, and Patterns) — Declares the dependencies
    ///     a class requires by taking them as constructor parameters, so that an instance cannot exist without them.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Steven van Deursen, Mark Seemann, <i>Dependency Injection Principles, Practices, and Patterns</i>, 2019.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public sealed class ConstructorInjectionAttribute : LivingDocumentationAttribute { }

}
