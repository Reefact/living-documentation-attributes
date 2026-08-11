#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.RecordedTestSample {

    // The terminal operating system came with four hundred regression tests captured by clicking through the
    // old client. They pass, nobody has read one, and when one goes red the only move anyone knows is to
    // re-record it.
    //
    // RECORDED TEST is what they are, and saying so is the point: it is the kind whose provenance a reader
    // most needs and can least infer.

    /// <summary>
    ///     Captured from a session at the gate desk in 2019.
    /// </summary>
    /// <remarks>
    ///     Nothing in it says what it is about, so a failure cannot be diagnosed — only reproduced. Knowing
    ///     that before spending an afternoon on it is what the annotation buys.
    /// </remarks>
    [RecordedTest]
    public sealed class GateDeskRegression0142 {

        public void Replay() { }

    }
}
