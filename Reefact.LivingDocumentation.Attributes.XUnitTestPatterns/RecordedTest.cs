#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.XUnitTestPatterns {

    /// <summary>
    ///     RecordedTest (xUnit Test Patterns) — Is produced by recording what somebody did to the system, so that a
    ///     test exists without anyone writing one.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gerard Meszaros, <i>xUnit Test Patterns</i>, 2007.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class RecordedTestAttribute : LivingDocumentationAttribute { }

}
