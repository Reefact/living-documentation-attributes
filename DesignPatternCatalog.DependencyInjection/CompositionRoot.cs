#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DependencyInjection {

    /// <summary>
    ///     CompositionRoot (Dependency Injection Principles, Practices, and Patterns) — Assembles the application's
    ///     object graph in one location as close as possible to its entry point, so that everything else is composed
    ///     rather than composing.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Steven van Deursen, Mark Seemann, <i>Dependency Injection Principles, Practices, and Patterns</i>, 2019.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class CompositionRootAttribute : DesignPatternAttribute { }

}
