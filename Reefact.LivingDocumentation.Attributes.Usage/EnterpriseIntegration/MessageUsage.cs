#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessageSample {

    // A crane move crossing a boundary is not an argument list: it is a thing, with an identity, a moment and
    // a return address, that may be logged, replayed and versioned.
    //
    // MESSAGE names it, and separates what the messaging system reads from what the application sent. The
    // infrastructure routes on the header and never opens the body.

    /// <summary>
    ///     One packet sent over a channel.
    /// </summary>
    /// <remarks>
    ///     A type, so that what crosses a boundary is named and versionable — which a call's parameters are
    ///     not.
    /// </remarks>
    [Message.Message]
    public sealed class CraneMoveMessage {

        public CraneMoveMessage(MessageHeader header, CraneMove body) {
            Header = header;
            Body   = body;
        }

        /// <summary>
        ///     What the messaging system reads.
        /// </summary>
        /// <remarks>
        ///     Held apart from the body because the infrastructure may read this and has no business reading
        ///     the rest.
        /// </remarks>
        [Message.Header]
        public MessageHeader Header { get; }

        /// <summary>
        ///     What the application sent.
        /// </summary>
        /// <remarks>
        ///     Carried without being looked at, which is what lets one channel serve payloads it knows nothing
        ///     about.
        /// </remarks>
        [Message.Body]
        public CraneMove Body { get; }

    }

    /// <summary>The identifiers and instructions the infrastructure acts on.</summary>
    public sealed record MessageHeader(Guid MessageId, DateTimeOffset SentAt, string? ReplyTo);

    /// <summary>The move itself.</summary>
    public sealed record CraneMove(string ContainerNumber, string FromSlot, string ToSlot);
}
