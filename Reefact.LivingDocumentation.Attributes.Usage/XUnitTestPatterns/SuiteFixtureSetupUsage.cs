#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.SuiteFixtureSetupSample {

    // The integration suite needs a message broker. Starting one per test is four seconds each; starting one
    // per suite is four seconds once.
    //
    // SUITE FIXTURE SETUP is the framework's one-time hook, and the point at which every test in the suite
    // becomes able to affect every other.

    public sealed class MessagingIntegrationTests {

        /// <summary>
        ///     Runs once before the tests of this suite.
        /// </summary>
        /// <remarks>
        ///     Where a shared fixture is legitimately built. What it builds should be the part no test
        ///     modifies — which is a rule nothing enforces, and one this annotation at least makes
        ///     addressable.
        /// </remarks>
        [SuiteFixtureSetup]
        public static void BeforeAll() { }

    }
}
