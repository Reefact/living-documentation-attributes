#region Usings declarations


using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestSelectionSample {

    // The full suite takes forty minutes and the handheld has a battery. On the quay it runs the tests
    // tagged "quay", nothing else.
    //
    // TEST SELECTION is the filter applied to whatever discovery or enumeration produced.

    /// <summary>
    ///     Chooses which of the available tests run.
    /// </summary>
    /// <remarks>
    ///     Worth naming because of how it fails: a selection excluding more than anybody realises is
    ///     indistinguishable, from the report, from a suite that passed.
    /// </remarks>
    [TestSelection]
    public sealed class QuayTestSelection {

        public bool Includes(string testName) {
            return testName.Contains("Lashing") || testName.Contains("Stability");
        }

    }
}
