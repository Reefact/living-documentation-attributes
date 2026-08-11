#region Usings declarations


using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestRunnerSample {

    // The lashing checks run on a handheld the crane drivers carry: no test framework, no console, a screen
    // four lines high. The team wrote the runner.
    //
    // TEST RUNNER is that piece — and this is the case where the role earns its place, because most
    // codebases use the one their framework ships and annotate nothing.

    /// <summary>
    ///     Runs the suite on the handheld and reports on four lines.
    /// </summary>
    /// <remarks>
    ///     Annotated because it is home-made: the piece nobody else on the team understands, and the one a
    ///     newcomer would otherwise take for application code.
    /// </remarks>
    [TestRunner]
    public sealed class HandheldTestRunner {

        public int Run(object suite) {
            return 0;
        }

    }
}
