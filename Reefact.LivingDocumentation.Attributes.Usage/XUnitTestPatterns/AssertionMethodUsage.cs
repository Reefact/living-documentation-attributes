#region Usings declarations


using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.AssertionMethodSample {

    // A yard position compares as a string, so a failed comparison reports "expected BAY07.R04.T02, was
    // BAY07.R04.T03" and leaves the reader to spot which of the three parts moved.
    //
    // ASSERTION METHOD is the call a test makes to state its expectation. This one knows what a position is.
    //
    // Note: a domain-specific assertion like this one is also a CUSTOM ASSERTION, a chapter 21 pattern not
    // yet catalogued. What is annotated here is the generic role: a method that fails the test.

    public static class PositionAssertions {

        /// <summary>
        ///     States what the position should be, and fails the test when it is not.
        /// </summary>
        /// <remarks>
        ///     It fails rather than returning a verdict. An assertion whose result the caller has to check is
        ///     one a caller can forget to check, which is how a test starts passing for the wrong reason.
        /// </remarks>
        [AssertionMethod]
        public static void AssertPositionIs(string expected, string actual) { }

    }
}
