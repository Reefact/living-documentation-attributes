#region Usings declarations


using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestcaseClassSample {

    // The framework has to find the tests somewhere, the fixture has to be set up for something, and the
    // three organisations of chapter 24 are three answers about the same thing.
    //
    // TESTCASE CLASS is that thing.

    /// <summary>
    ///     Where the yard's test methods live.
    /// </summary>
    /// <remarks>
    ///     The role is inherited: a subclass holding tests is a testcase class too. That is a nature rather
    ///     than an organisation — which is why this flag differs from the three chapter 24 entries that
    ///     narrow it.
    /// </remarks>
    [TestcaseClass]
    public class YardTests {

        [TestMethod]
        public void A_box_cannot_be_stacked_on_a_tank() { }

    }
}
