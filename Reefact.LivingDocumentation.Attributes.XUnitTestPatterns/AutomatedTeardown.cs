#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.XUnitTestPatterns {

    /// <summary>
    ///     AutomatedTeardown (xUnit Test Patterns) — Records what the test created and removes it afterwards without
    ///     the test saying so, so that cleaning up cannot be forgotten.
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
    public sealed class AutomatedTeardownAttribute : LivingDocumentationAttribute { }

}
