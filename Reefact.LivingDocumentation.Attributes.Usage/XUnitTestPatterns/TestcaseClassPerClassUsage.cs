#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.TestcaseClassPerClassSample {

    // The terminal's test tree grew without anyone choosing how it is organised: some classes follow the
    // production tree, some follow a feature, some follow a fixture. All three are legitimate and they answer
    // different questions, and nothing in the code says which was decided.
    //
    // TESTCASE CLASS PER CLASS is the first of the three: the test tree mirrors the production tree.

    /// <summary>
    ///     Everything <c>ContainerRegistry</c> does, in one place.
    /// </summary>
    /// <remarks>
    ///     The easiest organisation to find and the least likely to say anything: a class with six
    ///     responsibilities gets one testcase class with six unrelated fixtures, and that pressure is exactly
    ///     what this choice hides.
    /// </remarks>
    [TestcaseClassPerClass]
    public sealed class ContainerRegistryTests {

        public void Describe_returns_the_iso_type() { }

        public void Describe_throws_when_the_number_is_unknown() { }

    }
}
