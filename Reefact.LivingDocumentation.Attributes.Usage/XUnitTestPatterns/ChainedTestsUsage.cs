#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.ChainedTestsSample {

    // Discharging a vessel end to end takes ninety seconds of setup. The team wrote it once and let each
    // test carry on where the last one stopped: berth, discharge, gate out, invoice.
    //
    // CHAINED TESTS is what that is. The book offers it as a last resort, and annotating it is worth more
    // than annotating most things here.

    /// <summary>
    ///     Each test leaves the state the next one needs.
    /// </summary>
    /// <remarks>
    ///     It breaks when a runner changes order, parallelises, or runs one test alone — three things nobody
    ///     announces. Nothing else in the code admits this was chosen rather than drifted into, which is
    ///     exactly why it is worth an annotation.
    /// </remarks>
    [ChainedTests]
    public sealed class VesselCallWalkthroughTests {

        public void Step1_The_vessel_berths() { }

        public void Step2_The_vessel_is_discharged() { }

        public void Step3_The_first_box_leaves_by_road() { }

    }
}
