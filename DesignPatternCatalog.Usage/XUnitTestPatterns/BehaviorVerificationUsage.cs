#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.BehaviorVerificationSample {

    // Releasing a held container must tell customs. Nothing in the terminal's own state records that the
    // telling happened, so there is nothing to look at afterwards.
    //
    // BEHAVIOR VERIFICATION checks the call instead — the counterpart of the test next door, and the reason
    // spies and mocks exist.

    public sealed class ReleaseTests {

        /// <summary>
        ///     Asserts that customs was told, because nothing else would show it.
        /// </summary>
        /// <remarks>
        ///     The only way to verify an indirect output, bought with coupling to the collaboration: a
        ///     refactoring that changes how the work is delegated breaks this even when the outcome is
        ///     unchanged.
        /// </remarks>
        [BehaviorVerification]
        public void Releasing_a_held_container_tells_customs() { }

    }
}
