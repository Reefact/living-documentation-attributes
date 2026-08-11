#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestDiscoverySample {

    // On the handheld there is no attribute scanner, so the runner finds its tests by convention: every
    // public method whose name starts with "Check".
    //
    // TEST DISCOVERY is that rule — and the rule has a gap in it the day somebody writes "Verify".

    /// <summary>
    ///     Builds the suite by looking rather than by being told.
    /// </summary>
    /// <remarks>
    ///     Its assertion is the one that matters most in practice: a test that exists is a test that runs. A
    ///     discovery rule with a gap produces a green build for a test nobody has executed in months, and
    ///     nothing in the report says so.
    /// </remarks>
    [TestDiscovery]
    public interface IHandheldTestDiscovery {

        IEnumerable<string> Find(object testcaseClass);

    }
}
