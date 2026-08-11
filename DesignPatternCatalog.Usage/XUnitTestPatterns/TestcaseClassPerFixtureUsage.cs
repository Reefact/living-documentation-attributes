#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestcaseClassPerFixtureSample {

    // Six tests need a vessel berthed, discharged, and its boxes in the yard. Setting that up per test is six
    // copies; setting it up in a class that also holds tests needing an empty yard means a setup nobody can
    // read as true.
    //
    // TESTCASE CLASS PER FIXTURE groups by the starting state instead.

    /// <summary>
    ///     Every test here starts with the same vessel discharged into the same yard.
    /// </summary>
    /// <remarks>
    ///     The setup is read once and is true for every test in the class. A test that needs something
    ///     slightly different is then a signal to split the class rather than to widen the setup — which is
    ///     the discipline this organisation buys and the one it loses first.
    /// </remarks>
    [TestcaseClassPerFixture]
    public sealed class AfterTheMaerskSelandiaIsDischargedTests {

        public void The_yard_holds_four_hundred_boxes() { }

        public void Billing_has_one_line_per_move() { }

    }
}
