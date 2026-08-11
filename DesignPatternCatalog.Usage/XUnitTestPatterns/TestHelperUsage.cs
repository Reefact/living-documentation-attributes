#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestHelperSample {

    // Building a plausible container number is needed by the gate tests, the customs tests and the billing
    // tests. Those three have nothing else in common, so a shared superclass would relate them for no reason
    // and spend the one base class each of them has.
    //
    // TEST HELPER shares by delegation instead.

    /// <summary>
    ///     Test utility methods, in a class of their own.
    /// </summary>
    /// <remarks>
    ///     The counterpart of a testcase superclass, and the annotation is what makes the choice between the
    ///     two legible: nothing else in the code says which was decided and which was inherited from whoever
    ///     wrote the first test.
    /// </remarks>
    [TestHelper]
    public static class ContainerNumbers {

        /// <summary>
        ///     A valid container number, check digit included.
        /// </summary>
        [TestUtilityMethod]
        public static string Any() {
            return "MSKU3070512";
        }

        /// <summary>
        ///     A container number that fails the check digit, for the tests that need one.
        /// </summary>
        [TestUtilityMethod]
        public static string Invalid() {
            return "MSKU3070510";
        }

    }
}
