#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DependencyInjection {

    /// <summary>
    ///     TransientLifestyle (Dependency Injection Principles, Practices, and Patterns) — A new instance is created
    ///     for every consumer that asks for one, and none is ever reused.
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
    public sealed class TransientLifestyleAttribute : DesignPatternAttribute { }

}
