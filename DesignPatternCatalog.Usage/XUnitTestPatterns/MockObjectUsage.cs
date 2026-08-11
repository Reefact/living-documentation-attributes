#region Usings declarations

using System;

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.MockObjectSample {

    // Releasing a held container must tell customs, exactly once, with the hold reference. Forgetting the
    // call is the failure that matters, and a test that only checks the return value would pass.
    //
    // MOCK OBJECT is told beforehand what it should be asked, and it judges. That is what separates it from
    // the spy next door: the expectation lives in the double, not in the test's assertions.

    public interface ICustomsGateway {

        void DeclareRelease(string containerNumber, string holdReference);

    }

    /// <summary>
    ///     Carries the call it expects, and fails when it does not get it.
    /// </summary>
    /// <remarks>
    ///     Two failure modes belong to the double rather than to the test: an unexpected call throws where it
    ///     happens, and a missing one surfaces at final verification. A test using this can pass every
    ///     assertion it wrote and still fail on a call nobody wrote down.
    /// </remarks>
    [MockObject]
    public sealed class MockCustomsGateway : ICustomsGateway {

        private readonly string _expectedContainer;
        private readonly string _expectedHold;
        private          bool   _received;

        public MockCustomsGateway(string expectedContainer, string expectedHold) {
            _expectedContainer = expectedContainer;
            _expectedHold      = expectedHold;
        }

        public void DeclareRelease(string containerNumber, string holdReference) {
            if (containerNumber != _expectedContainer || holdReference != _expectedHold) {
                throw new InvalidOperationException($"unexpected declaration for {containerNumber}");
            }

            _received = true;
        }

        public void Verify() {
            if (!_received) { throw new InvalidOperationException("customs was never told about the release"); }
        }

    }
}
