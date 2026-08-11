#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DependencyInjection {

    /// <summary>
    ///     ControlFreak (Dependency Injection Principles, Practices, and Patterns) — A class that creates the
    ///     dependencies it uses, so that nothing outside it can choose them.
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
    public sealed class ControlFreakAttribute : DesignPatternAttribute { }

}
