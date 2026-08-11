#region Usings declarations

using System;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.EnvelopeWrapperSample {

    // The terminal's tally system was written in 1998 and emits a flat record: container number, move type,
    // timestamp. The message bus wants a correlation identifier, a return address and an expiry on every
    // message, and rejects anything without them. Neither side can be changed.
    //
    // ENVELOPE WRAPPER puts the tally record inside something the bus accepts, and takes it back out at the
    // far end. The tally system never learns what a header is.

    /// <summary>
    ///     What the tally system produces, and all it knows how to produce.
    /// </summary>
    public sealed record TallyRecord(string ContainerNumber, string MoveType, DateTimeOffset At);

    /// <summary>
    ///     The tally record plus everything the bus requires around it.
    /// </summary>
    /// <remarks>
    ///     Naming the envelope keeps the two apart: <c>Payload</c> belongs to the terminal, the rest belongs to
    ///     the transport, and a field that drifts from one side to the other is visible.
    /// </remarks>
    [EnvelopeWrapper.Envelope]
    public sealed class TallyEnvelope {

        public TallyEnvelope(TallyRecord payload, Guid messageId, string replyTo, DateTimeOffset expiresAt) {
            Payload   = payload;
            MessageId = messageId;
            ReplyTo   = replyTo;
            ExpiresAt = expiresAt;
        }

        public TallyRecord     Payload   { get; }
        public Guid           MessageId { get; }
        public string         ReplyTo   { get; }
        public DateTimeOffset ExpiresAt { get; }

    }

    /// <summary>
    ///     Puts a tally record into an envelope the bus will accept.
    /// </summary>
    /// <remarks>
    ///     It exists so that the tally system never learns the header fields — which is what lets a system
    ///     written in 1998 take part in a messaging exchange designed long after it.
    /// </remarks>
    [EnvelopeWrapper.Wrapper(Envelope = typeof(TallyEnvelope))]
    public sealed class TallyEnvelopeWrapper {

        public TallyEnvelope Wrap(TallyRecord record) {
            return new TallyEnvelope(record, Guid.NewGuid(), "terminal.tally.replies", record.At.AddMinutes(30));
        }

    }

    /// <summary>
    ///     Takes the tally record back out at the destination.
    /// </summary>
    /// <remarks>
    ///     Named apart from the wrapper because the two live in different applications: an envelope nobody
    ///     opens is a message the receiver reads as malformed.
    /// </remarks>
    [EnvelopeWrapper.Unwrapper(Envelope = typeof(TallyEnvelope))]
    public sealed class TallyEnvelopeUnwrapper {

        public TallyRecord Unwrap(TallyEnvelope envelope) {
            return envelope.Payload;
        }

    }
}
