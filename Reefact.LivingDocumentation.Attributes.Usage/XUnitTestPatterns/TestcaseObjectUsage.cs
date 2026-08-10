#region Usings declarations


using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.TestcaseObjectSample {

    // The handheld's runner needs to hold its tests, count them, and re-run the ones that failed. A method
    // is none of those things.
    //
    // TESTCASE OBJECT is one test, reified.

    /// <summary>
    ///     One test, as an object the runner can keep.
    /// </summary>
    /// <remarks>
    ///     It is what lets a suite be built at run time rather than written out, and why a test can be
    ///     counted, filtered and re-run without anything parsing source.
    /// </remarks>
    [TestcaseObject]
    public sealed class LashingTestcase {

        public LashingTestcase(string name) {
            Name = name;
        }

        public string Name { get; }

        public void Run() { }

    }
}
