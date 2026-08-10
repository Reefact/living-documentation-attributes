#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.XUnitTestPatterns {

    /// <summary>
    ///     ParameterizedTest (xUnit Test Patterns) — Takes the values that differ as arguments, so that several test
    ///     conditions share one body instead of one copy each.
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
    public sealed class ParameterizedTestAttribute : LivingDocumentationAttribute { }

}
