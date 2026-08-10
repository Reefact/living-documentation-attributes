#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.XUnitTestPatterns {

    /// <summary>
    ///     BehaviorVerification (xUnit Test Patterns) — Verifies by checking the calls the system under test made to
    ///     something else, so that an effect leaving no state behind can still be asserted.
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
    public sealed class BehaviorVerificationAttribute : LivingDocumentationAttribute { }

}
