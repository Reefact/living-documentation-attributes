#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.NamedTestSuiteSample {

    // The terminal's suite takes forty minutes because a third of it talks to a real database. Before a
    // release somebody wants the ten tests that would catch a broken deployment, and nothing in the tree
    // groups them: they are spread across the gate, the yard and billing.
    //
    // NAMED TEST SUITE gives that subset a name.

    /// <summary>
    ///     The ten tests worth running before a deployment.
    /// </summary>
    /// <remarks>
    ///     The useful groupings are rarely the ones the directory layout produces. A subset nobody can name
    ///     is a subset nobody runs deliberately — it is run by everybody typing a filter they each invented.
    /// </remarks>
    [NamedTestSuite]
    public sealed class SmokeTests {

        public IEnumerable<string> Members() {
            yield return "GateTests.A_truck_without_a_booking_is_turned_away";
            yield return "BillingTests.A_move_produces_one_line";
            yield return "CustomsTests.A_held_container_is_not_released";
        }

    }
}
