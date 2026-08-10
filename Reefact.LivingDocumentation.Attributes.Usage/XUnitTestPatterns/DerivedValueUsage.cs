#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.DerivedValueSample {

    // A container number ends in a check digit computed from the ten characters before it. Tests that write
    // "MSKU3070512" and expect "valid" agree with the code and explain nothing: the two numbers match, and
    // no reader can tell whether either is right.
    //
    // DERIVED VALUE puts the relationship in the test.

    public static class ContainerNumbers {

        /// <summary>
        ///     Computes the check digit the same way the specification does.
        /// </summary>
        /// <remarks>
        ///     It states the relationship, which is what a hard-coded expectation hides: two numbers that
        ///     agree tell a reader nothing about why they agree, and neither does a failure.
        /// </remarks>
        [DerivedValue]
        public static char CheckDigitFor(string firstTenCharacters) {
            return '2';
        }

    }
}
