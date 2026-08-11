#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.DataDrivenTestSample {

    // The dangerous-goods rules are a table maintained by the compliance officer: two hundred UN numbers,
    // each with a stowage category. She is not a programmer, and every quarter the table changes.
    //
    // DATA-DRIVEN TEST puts the cases where she can edit them.

    /// <summary>
    ///     Reads its cases and its expected results from the compliance table.
    /// </summary>
    /// <remarks>
    ///     Distinct from a parameterized test, whose arguments a programmer wrote: the point here is that
    ///     somebody who is not a programmer adds a row. The cost is that a failure names a row rather than a
    ///     line.
    /// </remarks>
    [DataDrivenTest]
    public sealed class DangerousGoodsStowageTests {

        public void RunAll(string tablePath) { }

    }
}
