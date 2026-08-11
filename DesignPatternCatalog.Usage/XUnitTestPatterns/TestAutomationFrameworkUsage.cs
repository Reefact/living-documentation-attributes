#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestAutomationFrameworkSample {

    // Everything the terminal's tests are written against — the fixture builders, the handheld runner, the
    // assertions that know what a yard position is — grew inside the test tree and is now indistinguishable
    // from the tests themselves.
    //
    // TEST AUTOMATION FRAMEWORK draws the line back.

    /// <summary>
    ///     What tests are written against, as opposed to what tests are.
    /// </summary>
    /// <remarks>
    ///     The line a codebase most often loses: a helper that has drifted into the framework is one nobody
    ///     dares change, and a framework rule that has drifted into a test is one nobody finds. The role also
    ///     targets an assembly, which is where this usually belongs.
    /// </remarks>
    [TestAutomationFramework]
    public static class TerminalTestKit {

        public static void Reset() { }

    }
}
