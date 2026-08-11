#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.ScriptedTestSample {

    // Beside the four hundred recorded ones, the team writes its own. They cost time and they say what they
    // are about.
    //
    // SCRIPTED TEST is the counterpart, and annotating both is what makes the ratio visible — which is the
    // number a team actually wants when it argues about its test suite.

    public sealed class GateTests {

        /// <summary>
        ///     Written by hand, and it states its subject in its name.
        /// </summary>
        /// <remarks>
        ///     It survives a refactoring and can be read by somebody who did not write it — neither of which
        ///     is true of the recording next door.
        /// </remarks>
        [ScriptedTest]
        public void A_truck_without_a_booking_is_turned_away() { }

    }
}
