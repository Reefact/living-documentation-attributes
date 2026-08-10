#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.SharedFixtureSample {

    // Loading the dangerous-goods reference data takes eleven seconds. Two hundred tests need it, and none
    // of them changes it.
    //
    // SHARED FIXTURE pays for it once — and makes every one of those tests depend on the others' good
    // behaviour.

    /// <summary>
    ///     Built once, used by two hundred tests.
    /// </summary>
    /// <remarks>
    ///     It outlives a test, so it makes tests depend on each other whether anybody intended it or not.
    ///     The assertion worth checking is that no test mutates it — and nothing in the code says so unless
    ///     this is annotated.
    /// </remarks>
    [SharedFixture]
    public static class DangerousGoodsReference {

        public static void LoadOnce() { }

    }
}
