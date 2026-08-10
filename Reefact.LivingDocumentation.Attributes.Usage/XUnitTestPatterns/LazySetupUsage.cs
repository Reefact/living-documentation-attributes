#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.LazySetupSample {

    // Loading the dangerous-goods reference takes eleven seconds and only a third of the suite touches it.
    // Building it in the suite setup costs eleven seconds every run, including the runs that never look at
    // it.
    //
    // LAZY SETUP builds it the first time somebody asks.

    public static class DangerousGoods {

        private static object? _reference;

        /// <summary>
        ///     Builds what is missing, returns what exists.
        /// </summary>
        /// <remarks>
        ///     It buys the cost of an unused fixture and pays in a lifetime nobody stated: what it builds
        ///     outlives the test that triggered it, so the first test in a run and the same test alone are
        ///     not running against the same thing.
        /// </remarks>
        [LazySetup]
        public static object Reference() {
            return _reference ??= new object();
        }

    }
}
