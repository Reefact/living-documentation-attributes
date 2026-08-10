#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.CustomAssertionSample {

    // A yard position compares as a string, so a failure reports "expected BAY07.R04.T02, was BAY07.R04.T03"
    // and leaves the reader to spot which of the three parts moved.
    //
    // CUSTOM ASSERTION says it in the terminal's own words, and says it back on failure.

    public static class PositionAssertions {

        /// <summary>
        ///     Asserts a position, and reports which part is wrong.
        /// </summary>
        /// <remarks>
        ///     It shortens tests and, more importantly, makes the failure message say something: "tier 3,
        ///     expected 2" rather than two strings a reader has to diff by eye.
        /// </remarks>
        [CustomAssertion]
        public static void AssertPositionIs(string expected, string actual) { }

    }
}
