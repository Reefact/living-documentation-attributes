#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.FreshFixtureSample {

    // The yard tests each move boxes around. Run alone they pass; run after one another they passed for
    // years and then did not, on a Tuesday, in a different order.
    //
    // FRESH FIXTURE is the only arrangement in which that cannot happen.

    public sealed class YardTests {

        /// <summary>
        ///     Rebuilt before every test in this class.
        /// </summary>
        /// <remarks>
        ///     A lifetime of one test, so no test can be affected by what another did. It is bought with the
        ///     time it takes to build, which is the whole of the argument against it.
        /// </remarks>
        [FreshFixture]
        public void SetUp() { }

    }
}
