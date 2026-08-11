#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.IdempotentConsumerSample {

    // The broker guarantees at-least-once delivery, and the outbox relay in front of it can republish after
    // a crash. So billing will one day be handed the same validated reading twice, and the second time it
    // must not raise a second invoice.
    //
    // IDEMPOTENT CONSUMER makes that a property of the handler rather than a hope. The mechanism is the
    // primary key: inserting the message identifier fails when it is already there, and the transaction is
    // rolled back with nothing done. What no C# type states is that this insert is the safety, which is why
    // the annotation is worth more here than in most places.

    /// <summary>
    ///     The identifiers billing has already dealt with.
    /// </summary>
    /// <remarks>
    ///     The work is done by the uniqueness of (subscriber, message), which lives in the schema and in no
    ///     signature here. Drop that constraint and every method below still compiles and still passes its
    ///     tests, on messages that never arrive twice.
    /// </remarks>
    [IdempotentConsumer.ProcessedMessages]
    public sealed class ProcessedMessageTable {

        private readonly HashSet<(string Subscriber, string MessageId)> _seen = new HashSet<(string, string)>();

        /// <summary>Records the message, or reports that it was already there.</summary>
        public bool TryRecord(string subscriber, string messageId) => _seen.Add((subscriber, messageId));

    }

    /// <summary>
    ///     Raises the invoice, once, however often it is called.
    /// </summary>
    /// <remarks>
    ///     The role is on the handler rather than on the class, because that is the declaration the claim is
    ///     about: a second handler on this same class would have to earn it separately.
    /// </remarks>
    public sealed class InvoiceOnReadingValidated {

        private readonly ProcessedMessageTable _processed;

        public InvoiceOnReadingValidated(ProcessedMessageTable processed) {
            _processed = processed;
        }

        [IdempotentConsumer.IdempotentConsumer(ProcessedMessages = typeof(ProcessedMessageTable))]
        public void Handle(string messageId, string supplyPoint, decimal kilowattHours) {
            if (!_processed.TryRecord("billing", messageId)) { return; }

            // ... raises the invoice, in the same transaction as the insert above
        }

    }
}
