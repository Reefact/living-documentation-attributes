#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.DeltaAssertionSample {

    // The billing tests run against a database an overnight job seeds, so nobody can say how many invoice
    // lines it holds. Asserting "seven lines" is asserting about the seeding job.
    //
    // DELTA ASSERTION asserts the difference the exercise made.

    public static class BillingAssertions {

        /// <summary>
        ///     Asserts that the exercise added exactly one line.
        /// </summary>
        /// <remarks>
        ///     What makes a test safe against a shared or prebuilt fixture whose exact contents nobody
        ///     guarantees — and weaker on purpose, since a delta of one says nothing about what the one was.
        /// </remarks>
        [DeltaAssertion]
        public static void AssertOneMoreInvoiceLine(int before, int after) { }

    }
}
