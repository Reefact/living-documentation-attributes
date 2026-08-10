#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.TestcaseClassPerFeatureSample {

    // Getting a container out of the terminal runs through the gate, billing, customs and the yard. No single
    // class owns it, so no testcase class per class holds its tests — they end up scattered across four, and
    // the feature has no tests of its own.
    //
    // TESTCASE CLASS PER FEATURE follows what the system does rather than how it is built.

    /// <summary>
    ///     Releasing a container, whatever classes that runs through.
    /// </summary>
    /// <remarks>
    ///     It survives a refactoring that moves the feature between classes, and it costs the direct
    ///     correspondence a reader uses to find the tests of a class — which is why the two organisations are
    ///     worth telling apart in the code rather than by reading the class names.
    /// </remarks>
    [TestcaseClassPerFeature]
    public sealed class ReleasingAContainerTests {

        public void A_held_container_is_not_released() { }

        public void Releasing_bills_the_haulier_once() { }

        public void Releasing_tells_customs() { }

    }
}
