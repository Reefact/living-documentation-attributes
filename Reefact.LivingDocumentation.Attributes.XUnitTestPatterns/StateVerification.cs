#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.XUnitTestPatterns {

    /// <summary>
    ///     StateVerification (xUnit Test Patterns) — Verifies by looking at what the system under test holds
    ///     afterwards, so that a test asserts about the result rather than about the journey.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gerard Meszaros, <i>xUnit Test Patterns</i>, 2007.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class StateVerificationAttribute : LivingDocumentationAttribute { }

}
