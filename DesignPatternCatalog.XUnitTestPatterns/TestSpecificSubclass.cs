#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.XUnitTestPatterns {

    /// <summary>
    ///     TestSpecificSubclass (xUnit Test Patterns) — Adds to a subclass of the system under test the methods a test
    ///     needs to reach its state or override its behaviour, so that code not designed for testing can be tested.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gerard Meszaros, <i>xUnit Test Patterns</i>, 2007.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class TestSpecificSubclassAttribute : LivingDocumentationAttribute { }

}
