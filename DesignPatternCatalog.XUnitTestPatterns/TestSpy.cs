#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.XUnitTestPatterns {

    /// <summary>
    ///     TestSpy (xUnit Test Patterns) — Captures the indirect outputs the system under test sends to another
    ///     component, so that the test can verify them after the fact.
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
    public sealed class TestSpyAttribute : TestDoubleAttribute { }

}
