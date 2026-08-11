#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestEnumerationSample {

    // The stability tests must run in a fixed order and only the seven that the classification society
    // requires. Discovery would run whatever exists, which is exactly what an audit does not want.
    //
    // TEST ENUMERATION names them, by hand, in one place.

    public static class ClassificationSuite {

        /// <summary>
        ///     The seven tests, listed.
        /// </summary>
        /// <remarks>
        ///     Costs an edit per test and buys certainty about what runs. The failure it exists against is
        ///     the opposite of discovery's: a test written and never added — which is why annotating the list
        ///     is what lets a rule compare it to what exists.
        /// </remarks>
        [TestEnumeration]
        public static IEnumerable<string> Members() {
            yield return "StabilityTests.Metacentric_height_is_positive";
            yield return "StabilityTests.Draught_is_within_the_marks";
        }

    }
}
