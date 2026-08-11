#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.XUnitTestPatterns {

    /// <summary>
    ///     DependencyInjection (xUnit Test Patterns) — Has its collaborators handed to it from outside, so that a test
    ///     can give it something other than what production gives it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gerard Meszaros, <i>xUnit Test Patterns</i>, 2007.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public sealed class DependencyInjectionAttribute : LivingDocumentationAttribute { }

}
