#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.StandardFixtureSample {

    // Forty tests across the yard, billing and customs start from the same terminal: one vessel alongside,
    // four hundred boxes discharged, two hauliers registered. Learning it once is cheaper than reading forty
    // bespoke setups.
    //
    // STANDARD FIXTURE is that trade, and it is the opposite of the one next door.

    /// <summary>
    ///     The terminal everybody's tests start from.
    /// </summary>
    /// <remarks>
    ///     One design learned once, at the price that most tests use a fraction of it. Worth stating rather
    ///     than drifting into: a standard and a minimal fixture are indistinguishable in code and answer
    ///     opposite questions about what a reader should assume is relevant.
    /// </remarks>
    [StandardFixture]
    public static class ATerminalMidShift {

        public static void Build() { }

    }
}
