#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.HardCodedTestDoubleSample {

    // One test asks what the gate does when customs has a hold on the box. It needs a gateway that says yes,
    // and it needs it once.
    //
    // HARD-CODED TEST DOUBLE answers with what was written into it. The right choice while there is one job.

    public interface ICustomsGateway {

        bool IsHeld(string containerNumber);

    }

    /// <summary>
    ///     Always held, and there is no way to tell it otherwise.
    /// </summary>
    /// <remarks>
    ///     The counterpart of the configurable double, and the annotation makes the pair legible: this one
    ///     costs nothing to write and nothing to read, right up to the fifth near-copy — which is the point
    ///     at which the book sends a reader to the configurable kind, and the point a review can now catch.
    /// </remarks>
    [HardCodedTestDouble]
    public sealed class AlwaysHeldCustomsGateway : ICustomsGateway {

        public bool IsHeld(string containerNumber) {
            return true;
        }

    }
}
