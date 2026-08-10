#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.ImplicitTeardownSample {

    // The customs tests open a connection to the stub customs service. Closing it at the end of the test body
    // works right up to the first failure — and a failure is exactly when somebody is about to look.
    //
    // IMPLICIT TEARDOWN runs whether the test succeeded, failed or threw.

    public sealed class CustomsTests {

        /// <summary>
        ///     Run by the framework after every test in this class.
        /// </summary>
        /// <remarks>
        ///     Running after a failure is the property that matters: a test cleaning up at the end of its own
        ///     body leaves the fixture behind precisely on the runs where it would have been useful.
        /// </remarks>
        [ImplicitTeardown]
        public void TearDown() { }

    }
}
