#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.WireTapSample {

    // Gate transactions travel a point-to-point channel, so exactly one consumer sees each one. That is what
    // was wanted, and it means nobody can watch the flow: reading the channel to audit it would consume the
    // transactions the yard is waiting for.
    //
    // WIRE TAP publishes each transaction to a second channel as well. Nothing on the first changes.

    /// <summary>
    ///     Forwards every gate transaction unmodified to both channels.
    /// </summary>
    /// <remarks>
    ///     A fixed recipient list with two outputs. The analysis lives in whatever consumes the second
    ///     channel, which is what lets this be inserted into a running terminal without risk to the first.
    /// </remarks>
    [WireTap]
    public sealed class GateTransactionWireTap {

        public IEnumerable<string> Route(GateTransaction transaction) {
            yield return "terminal.gate.transactions";
            yield return "terminal.audit.gate";
        }

    }

    public sealed record GateTransaction(string ContainerNumber, string HaulierCode, bool Inbound);
}
