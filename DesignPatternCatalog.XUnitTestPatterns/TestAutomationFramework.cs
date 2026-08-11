#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.XUnitTestPatterns {

    /// <summary>
    ///     TestAutomationFramework (xUnit Test Patterns) — Provides the mechanisms tests are written against, so that a
    ///     test states its intent rather than its plumbing.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gerard Meszaros, <i>xUnit Test Patterns</i>, 2007.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TestAutomationFrameworkAttribute : LivingDocumentationAttribute { }

}
