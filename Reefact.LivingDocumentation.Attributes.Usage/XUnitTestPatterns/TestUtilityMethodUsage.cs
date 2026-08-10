#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.TestUtilityMethodSample {

    // Eleven gate tests begin with the same nine lines: a booking, a haulier, a truck at the lane, a
    // container on it. A reader looking for what a test is about has to skip them every time.
    //
    // TEST UTILITY METHOD gives the nine lines a name, so the test states what it is about.

    public sealed class GateTests {

        public void A_truck_without_a_booking_is_turned_away() {
            GivenATruckAtTheLane("MSKU3070512");
            // ... exercise and verify
        }

        /// <summary>
        ///     Puts a truck at the lane with a booking and a container on it.
        /// </summary>
        /// <remarks>
        ///     Called by tests, and it asserts nothing on its own behalf. A utility method that has grown its
        ///     own assertions has quietly become a test that never runs — which is the drift the annotation
        ///     makes reviewable.
        /// </remarks>
        [TestUtilityMethod]
        private void GivenATruckAtTheLane(string containerNumber) { }

    }
}
