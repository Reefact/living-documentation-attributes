#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.IdempotentReceiverSample {

    // The transactional receiver next door returns a message to the channel when it crashes mid-processing,
    // so releases arrive twice. Releasing twice bills the haulier twice and opens a gate that was already
    // open.
    //
    // IDEMPOTENT RECEIVER is what makes that safe. It is the pattern that makes a resend a free action, and
    // it is an assertion about behaviour that no signature carries.

    /// <summary>
    ///     Releasing the same container twice has the effect of releasing it once.
    /// </summary>
    /// <remarks>
    ///     Reached here by de-duping on the release identifier. The other route the book gives is to define
    ///     the message so that repetition is harmless — "set the position to D4" rather than "move it one
    ///     bay" — which needs no bookkeeping at all.
    /// </remarks>
    [IdempotentReceiver]
    public sealed class ContainerReleaseReceiver {

        private readonly HashSet<string> _seen = new HashSet<string>();

        public void Handle(ContainerRelease release) {
            if (!_seen.Add(release.ReleaseIdentifier)) { return; }

            // Open the gate and bill the haulier, exactly once.
        }

    }

    public sealed record ContainerRelease(string ReleaseIdentifier, string ContainerNumber);
}
