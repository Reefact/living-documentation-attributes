#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.XUnitTestPatterns {

    /// <summary>
    ///     TestcaseClassPerFeature (xUnit Test Patterns) — Puts the tests of one feature into one testcase class,
    ///     whatever classes the feature runs through, so that the test tree mirrors what the system does.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of TestcaseClass: every participant annotated here is one of those too, and a consumer
    ///         asking for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Gerard Meszaros, <i>xUnit Test Patterns</i>, 2007.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TestcaseClassPerFeatureAttribute : TestcaseClassAttribute { }

}
