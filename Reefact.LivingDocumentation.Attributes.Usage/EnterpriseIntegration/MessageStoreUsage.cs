#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessageStoreSample {

    // The terminal is asked, every month, how long a container waits between discharge and gate-out. Nothing
    // holds the answer: the messages that would say went through the channels and are gone, which is what
    // being transient means.
    //
    // MESSAGE STORE keeps enough to answer without making the flow any less transient. Fed and forgotten, so
    // the crane is not waiting on a report.

    /// <summary>
    ///     Where a copy of each message, or a few fields of it, is kept for later analysis.
    /// </summary>
    /// <remarks>
    ///     What it keeps is a decision and not an oversight: everything costs traffic and storage, too little
    ///     answers no question. Here it is the identifier, the channel and the moment.
    /// </remarks>
    [MessageStore]
    public interface ITerminalMessageStore {

        void Record(Guid messageId, string channel, DateTimeOffset at);

    }
}
