#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.LayerTestSample {

    // The terminal claims a hexagonal architecture: the domain knows nothing of the database or the message
    // bus. The claim is in a diagram, and the tests are the place it is either true or not.
    //
    // LAYER TEST scopes a test to one layer, so a failure names the layer it is in.

    /// <summary>
    ///     The stowage rules, with nothing underneath them.
    /// </summary>
    /// <remarks>
    ///     What makes a claimed architecture checkable from the test side. A layer test that has quietly
    ///     started reaching two layers is a test whose failure no longer localises anything — and it still
    ///     passes, which is why nobody notices.
    /// </remarks>
    [LayerTest]
    public sealed class StowageDomainTests {

        public void A_tank_cannot_be_stacked_above_tier_three() { }

    }
}
