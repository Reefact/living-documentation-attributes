#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.ChannelPurgerSample {

    // The release enquiry test passed with the replier switched off. A reply left on the queue by the
    // previous run was consumed as though it answered the request just sent — and because each run leaves one
    // behind, it passed again the next day.
    //
    // CHANNEL PURGER is the answer: empty the channel before the run. A bug that reproduces perfectly and
    // explains nothing is the one worth having a named participant for.

    /// <summary>
    ///     Empties a channel, or removes the messages matching a criterion.
    /// </summary>
    /// <remarks>
    ///     Something that deletes messages on purpose is worth being able to find before somebody points it
    ///     at production — which is half of why it is annotated rather than left as a helper.
    /// </remarks>
    [ChannelPurger]
    public sealed class ReplyQueuePurger {

        public int PurgeAll(string channel) {
            return 0;
        }

        public int Purge(string channel, string containerNumber) {
            return 0;
        }

    }
}
