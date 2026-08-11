#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.XUnitTestPatterns {

    /// <summary>
    ///     TestMethod (xUnit Test Patterns) — Holds one test, so that a single expectation about the system has a name,
    ///     a body and a result of its own.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gerard Meszaros, <i>xUnit Test Patterns</i>, 2007.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class TestMethodAttribute : LivingDocumentationAttribute { }

}
