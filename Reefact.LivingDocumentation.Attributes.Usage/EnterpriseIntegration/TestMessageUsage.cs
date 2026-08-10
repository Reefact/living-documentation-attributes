#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.TestMessageSample {

    // The stowage calculator kept sending its heartbeat, kept its CPU low, and for eleven hours returned
    // positions off by one tier because a table had been reloaded half-empty. Everything the control bus
    // could see said it was healthy.
    //
    // TEST MESSAGE catches that: known input through the live component, known output checked. Four
    // participants, because the four fail differently.

    /// <summary>
    ///     Produces the moves used to exercise the calculator.
    /// </summary>
    /// <remarks>
    ///     A generator that only ever emits the easy case — one box, an empty bay — is a green light that
    ///     means nothing, which is why it is worth naming apart from the verifier.
    /// </remarks>
    [TestMessage.TestDataGenerator]
    public sealed class StowageProbeGenerator {

        public StowageRequest Next() {
            return new StowageRequest("PROBE0000001", "BAY07", IsProbe: true);
        }

    }

    /// <summary>
    ///     Puts probes into the real stream and marks them.
    /// </summary>
    /// <remarks>
    ///     The marking is the delicate part. Here it is a field of its own; the book's last resort is a magic
    ///     value in a business field, which makes one field mean two things.
    /// </remarks>
    [TestMessage.TestMessageInjector]
    public sealed class StowageProbeInjector {

        public void Inject(StowageRequest probe) { }

    }

    /// <summary>
    ///     Takes probe results back out of the output stream.
    /// </summary>
    /// <remarks>
    ///     It is what keeps the experiment from reaching the cranes: a separator that misses one sends a
    ///     fabricated position to a machine that will act on it.
    /// </remarks>
    [TestMessage.TestMessageSeparator]
    public sealed class StowageProbeSeparator {

        public bool IsProbe(StowageResult result) {
            return result.ContainerNumber.StartsWith("PROBE");
        }

    }

    /// <summary>
    ///     Compares what came back with what was expected.
    /// </summary>
    /// <remarks>
    ///     It needs the original probe to do so — the one coupling inside this pattern, and the reason the
    ///     generator is pointed at rather than merely present.
    /// </remarks>
    [TestMessage.TestDataVerifier(TestDataGenerator = typeof(StowageProbeGenerator))]
    public sealed class StowageProbeVerifier {

        public bool Verify(StowageRequest sent, StowageResult received) {
            return received.Position == "BAY07.R04.T02";
        }

    }

    public sealed record StowageRequest(string ContainerNumber, string Bay, bool IsProbe);

    public sealed record StowageResult(string ContainerNumber, string Position);
}
