#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.TestHookSample {

    // Whether a reefer alarm escalates depends on how long the box has been warm, which is measured against
    // a clock the legacy alarm service creates itself. There is no seam, and the escalation is untested.
    //
    // TEST HOOK is the last resort: a member the production path never uses.

    public sealed class LegacyAlarmService {

        private System.DateTimeOffset? _nowForTests;

        /// <summary>
        ///     Overrides the clock. Nothing in production ever sets this.
        /// </summary>
        /// <remarks>
        ///     The reason to annotate it is the reason to hesitate over it: this is code shipped to
        ///     production for the benefit of a test, and a codebase that cannot list its test hooks has no
        ///     way to know how much of that it carries.
        /// </remarks>
        [TestHook]
        internal System.DateTimeOffset? NowForTests {
            get => _nowForTests;
            set => _nowForTests = value;
        }

    }
}
