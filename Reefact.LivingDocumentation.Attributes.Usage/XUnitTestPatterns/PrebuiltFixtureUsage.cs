#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.PrebuiltFixtureSample {

    // The integration suite runs against a database an overnight job seeds with a week of vessel calls.
    // Nothing in the test tree creates it, and the suite takes four minutes instead of forty.
    //
    // PREBUILT FIXTURE is that arrangement — and the reason a new joiner's first run fails.

    /// <summary>
    ///     The seeded database the integration tests assume.
    /// </summary>
    /// <remarks>
    ///     Fast, and dependent on something no version of the code describes. The ordinary failure is the
    ///     one that costs a morning: a test that fails on a fresh machine and passes on the machine of
    ///     whoever wrote it.
    /// </remarks>
    [PrebuiltFixture]
    public static class SeededTerminalDatabase {

        public static string ConnectionString => "Server=integration;Database=terminal";

    }
}
