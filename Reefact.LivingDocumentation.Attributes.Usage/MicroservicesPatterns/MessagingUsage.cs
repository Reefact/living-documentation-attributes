#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.MessagingSample {

    // Nothing about a validated meter reading needs an answer. Billing wants to know, the tariff engine
    // wants to know, and analytics wants to know — and metering wants to be deployable on a Tuesday
    // afternoon whether or not any of the three is running.
    //
    // MESSAGING is that: metering publishes and stops caring. What it buys is that the other end may be
    // down. What it costs is a broker that had better not be, and a reply — when one is wanted — that is a
    // second message rather than a return value.

    /// <summary>
    ///     Metering's outbound side.
    /// </summary>
    /// <remarks>
    ///     No consumer appears in this interface, and that absence is the pattern: adding a return value
    ///     here would quietly turn the style back into a call.
    /// </remarks>
    [Messaging]
    public interface IMeteringPublisher {

        void Publish(string channel, string message);

    }

    /// <summary>
    ///     Billing's inbound side.
    /// </summary>
    /// <remarks>
    ///     Annotated separately, because the pattern is about participants: two annotations is the count of
    ///     what a broker outage actually stops.
    /// </remarks>
    [Messaging]
    public sealed class BillingSubscriber {

        public void On(string channel, Action<string> handle) {
            // ... subscribes and hands each message to the handler
        }

    }
}
