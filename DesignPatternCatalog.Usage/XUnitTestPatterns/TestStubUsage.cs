#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestStubSample {

    // Yard planning refuses a tank container above tier three. Testing that rule needs the registry to say
    // "this box is a tank", and the real registry answers from a database nobody wants in a unit test.
    //
    // TEST STUB feeds that indirect input. Its direction is inward and only inward.

    public interface IContainerRegistry {

        (string IsoType, int TareKilos, bool IsTank) Describe(string containerNumber);

    }

    /// <summary>
    ///     Answers with what the test decided it should answer.
    /// </summary>
    /// <remarks>
    ///     Note what is absent: no assertion, no recording, nothing the test consults afterwards. A stub that
    ///     grows a <c>VerifyDescribeWasCalled</c> is a mock wearing the wrong name, and the annotation is what
    ///     makes that drift reviewable.
    /// </remarks>
    [TestStub]
    public sealed class StubContainerRegistry : IContainerRegistry {

        public (string IsoType, int TareKilos, bool IsTank) Describe(string containerNumber) {
            return ("22T1", 2400, true);
        }

    }
}
