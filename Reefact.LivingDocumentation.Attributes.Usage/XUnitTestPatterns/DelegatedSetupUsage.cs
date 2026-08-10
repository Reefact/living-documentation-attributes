#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.DelegatedSetupSample {

    // The gate tests each need a truck, a booking and a container. Written out, that is nine lines before
    // anything happens; hidden in a setUp, it is nine lines the reader cannot see at all.
    //
    // DELEGATED SETUP is the middle answer: the test says what it needs, in one line, and the reader can
    // follow the call or not.

    public sealed class GateTests {

        /// <summary>
        ///     States the situation and delegates the building of it.
        /// </summary>
        /// <remarks>
        ///     The reader can follow <c>ABookedTruck</c> if they want to and does not have to — which is what
        ///     separates this from an implicit setup, where there is nothing in the test to follow.
        /// </remarks>
        [DelegatedSetup]
        public void A_booked_truck_is_admitted() {
            object truck = GateFixtures.ABookedTruck();
        }

    }

    public static class GateFixtures {

        [CreationMethod]
        public static object ABookedTruck() {
            return new object();
        }

    }
}
