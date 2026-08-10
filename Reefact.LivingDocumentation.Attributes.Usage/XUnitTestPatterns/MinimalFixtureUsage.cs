#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.MinimalFixtureSample {

    // A test about turning a truck away needs a truck and no booking. The fixture it inherited also builds a
    // vessel, a discharge list and four hundred containers, because some other test needed those.
    //
    // MINIMAL FIXTURE is the discipline of the first version.

    public sealed class GateRefusalTests {

        /// <summary>
        ///     A truck, and nothing else.
        /// </summary>
        /// <remarks>
        ///     Everything present is there for a reason, which is what makes the test readable. Its failure
        ///     mode is quiet: a fixture that has grown one field per new test stops telling a reader anything
        ///     about any of them.
        /// </remarks>
        [MinimalFixture]
        private void GivenATruckWithNoBooking() { }

    }
}
