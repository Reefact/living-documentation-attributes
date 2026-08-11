#region Usings declarations


using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestMethodSample {

    // The terminal's gate tests were written as three long methods, each verifying five things. When one
    // goes red it names the method, and the method is about "the gate" — which is not a fact anybody can act
    // on at three in the morning.
    //
    // TEST METHOD is one expectation, named.

    public sealed class GateTests {

        /// <summary>
        ///     One test condition, and its name is the report.
        /// </summary>
        /// <remarks>
        ///     A method verifying four things reports one failure and hides three, and no framework can tell
        ///     the difference — which is why the count is a decision rather than a style.
        /// </remarks>
        [TestMethod]
        public void A_truck_without_a_booking_is_turned_away() { }

        [TestMethod]
        public void A_truck_with_a_booking_for_another_terminal_is_turned_away() { }

    }
}
