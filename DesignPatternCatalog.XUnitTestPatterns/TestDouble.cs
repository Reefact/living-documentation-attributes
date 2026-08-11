#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.XUnitTestPatterns {

    /// <summary>
    ///     TestDouble (xUnit Test Patterns) — Replaces a component the system under test depends on with a test-
    ///     specific equivalent, so that logic can be verified when what it depends on is unusable or slow.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gerard Meszaros, <i>xUnit Test Patterns</i>, 2007.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class TestDoubleAttribute : LivingDocumentationAttribute { }

}
