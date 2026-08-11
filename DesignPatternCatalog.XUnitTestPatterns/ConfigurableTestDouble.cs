#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.XUnitTestPatterns {

    /// <summary>
    ///     ConfigurableTestDouble (xUnit Test Patterns) — Holds the values to return or expect in fields the test sets
    ///     during fixture setup, so that one double serves many tests.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of TestDouble: every participant annotated here is one of those too, and a consumer
    ///         asking for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Gerard Meszaros, <i>xUnit Test Patterns</i>, 2007.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class ConfigurableTestDoubleAttribute : TestDoubleAttribute { }

}
