#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.UnfinishedTestAssertionSample {

    // Reefer alarms during a power cut are handled by code nobody has tested. Everybody knows; it is in a
    // ticket somewhere.
    //
    // UNFINISHED TEST ASSERTION turns that into a red bar, which is the only form in which a gap gets fixed.

    public sealed class ReeferAlarmTests {

        /// <summary>
        ///     A test that exists to be missing.
        /// </summary>
        /// <remarks>
        ///     The one annotation in this catalogue whose value is that somebody comes back and removes it.
        ///     The role attaches to the test rather than to the assertion inside it: what is being stated is
        ///     that this test is a placeholder.
        /// </remarks>
        [UnfinishedTestAssertion]
        public void An_alarm_during_a_power_cut_is_queued() {
            throw new System.NotImplementedException("not written yet");
        }

    }
}
