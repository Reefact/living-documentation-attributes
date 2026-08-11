#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestcaseSuperclassSample {

    // Every testcase class touching the yard needs the same three boxes stacked before it starts. Written per
    // class, that is nine copies of the same six lines.
    //
    // TESTCASE SUPERCLASS puts them in one place, by inheritance.

    /// <summary>
    ///     What every yard testcase class starts from.
    /// </summary>
    /// <remarks>
    ///     The more expensive of the two ways to share: it spends the single base class a testcase class has,
    ///     and it hides in a parent the setup a reader is looking for in the test. Worth it when the classes
    ///     sharing it genuinely are the same kind of test.
    /// </remarks>
    [TestcaseSuperclass]
    public abstract class YardTestBase {

        protected void GivenThreeBoxesInBay(string bay) { }

    }

    // The subclass is a testcase class, not a superclass — which is why this role is not inherited: what it
    // states is a decision about one declaration, not a nature its subtypes carry.

    public sealed class RestowTests : YardTestBase {

        public void A_restow_moves_the_top_box_first() { }

    }
}
