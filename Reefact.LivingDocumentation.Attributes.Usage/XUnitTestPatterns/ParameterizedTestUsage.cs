#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.ParameterizedTestSample {

    // The check digit of a container number is verified the same way for every prefix. Written out, that is
    // eleven test methods differing by two literals — and when the rule changes, eleven edits.
    //
    // PARAMETERIZED TEST takes the values that differ.

    public sealed class ContainerNumberTests {

        /// <summary>
        ///     Verifies the check digit, once, for every case.
        /// </summary>
        /// <remarks>
        ///     One body to maintain, at the price that a failure names a row rather than a case. The
        ///     arguments have to identify themselves — <c>containerNumber</c> here does, a bare
        ///     <c>index: 7</c> would not — or the report says nothing a reader can act on.
        /// </remarks>
        [ParameterizedTest]
        public void The_check_digit_is_verified(string containerNumber, bool expectedValid) { }

    }
}
