#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.ServiceStubSample {

    // Laboratory information system: testing the results workflow without a bench analyser.
    //
    // The real analyser costs ninety thousand pounds, lives in one room, takes eleven minutes per run and
    // is in clinical use. It cannot be part of a test suite, and the tests that matter most — what happens
    // when a result comes back outside the reportable range — need it to misbehave on demand.
    //
    // A SERVICE STUB is a working stand-in: it satisfies the same separated interface, and it is written to
    // be PREDICTABLE rather than faithful.
    //
    // The distinction from a mock is worth being precise about, because the words are used loosely. A mock
    // exists to assert HOW it was called — it fails the test if the expected call did not happen. This
    // exists so the thing under test can run at all, and it asserts nothing. A test using it still checks
    // the workflow's own behaviour, not the stub's.
    //
    // Note that it implements the separated interface from SeparatedInterfaceUsage.cs. That is not
    // incidental: the interface being with the client rather than with the drivers is exactly what makes a
    // stub possible without dragging a vendor SDK into the test project.
    //
    // A pattern of test design, which the catalog admits on the same terms as any other (ADR-0022).

    /// <summary>
    ///     An analyser that answers instantly, and answers whatever a test needs.
    /// </summary>
    [ServiceStub]
    public sealed class StubAnalyser {

        private readonly Dictionary<string, decimal> _byBarcode = new();

        public string Model => "stub";

        /// <summary>
        ///     Arranges a result — including the out-of-range ones the real machine will not produce to order.
        /// </summary>
        public StubAnalyser Returning(string sampleBarcode, decimal potassium) {
            _byBarcode[sampleBarcode] = potassium;

            return this;
        }

        public decimal Read(string sampleBarcode) {
            return _byBarcode.TryGetValue(sampleBarcode, out decimal value) ? value : 4.2m;
        }

    }

}
