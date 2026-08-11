#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DependencyInjection {

    /// <summary>
    ///     ConstrainedConstruction (Dependency Injection Principles, Practices, and Patterns) — Requires every
    ///     implementation of an abstraction to offer a particular constructor signature, so that something outside can
    ///     create them all the same way.
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
    public sealed class ConstrainedConstructionAttribute : LivingDocumentationAttribute { }

}
