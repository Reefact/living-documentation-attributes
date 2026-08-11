#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.GarbageCollectedTeardownSample {

    // The stowage rules tests build a yard, move boxes around it and throw it away. Nothing touches a file,
    // a socket or a database, so there is nothing to clean up — and every one of them has an empty tearDown
    // that somebody wrote out of habit.
    //
    // GARBAGE-COLLECTED TEARDOWN says the absence is deliberate.

    /// <summary>
    ///     Nothing to clean up, on purpose.
    /// </summary>
    /// <remarks>
    ///     A claim rather than a shrug: everything this creates is reclaimed by the runtime, so no file, no
    ///     socket, no row and no temporary directory is left behind. The day one appears, this is the
    ///     statement that was broken.
    /// </remarks>
    [GarbageCollectedTeardown]
    public sealed class StowageRulesTests {

        public void A_tank_cannot_be_stacked_above_tier_three() { }

    }
}
