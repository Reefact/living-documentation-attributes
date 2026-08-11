#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.PollingPublisherSample {

    // Field work keeps its outbox in the same SQL Server instance as everything else the depot runs, and
    // nobody is going to be given a replication slot on it. What is available is a SELECT.
    //
    // POLLING PUBLISHER asks the table on a timer. It works against any database, which is the argument for
    // it, and the two things it costs are worth stating out loud: the poll interval is latency every
    // consumer downstream inherits, and the order is only as good as the ORDER BY somebody remembered.

    /// <summary>
    ///     Reads the outbox on a timer and publishes what it finds.
    /// </summary>
    /// <remarks>
    ///     The alternative to tailing the log, and the one that survives a change of database. Two instances
    ///     polling the same table is where the ordering guarantee quietly goes, and nothing in this class
    ///     says whether there are two.
    /// </remarks>
    [PollingPublisher]
    public sealed class FieldWorkOutboxPoller {

        private readonly TimeSpan _interval;

        public FieldWorkOutboxPoller(TimeSpan interval) {
            _interval = interval;
        }

        /// <summary>
        ///     How long a message can sit in the outbox before anyone hears about it.
        /// </summary>
        /// <remarks>
        ///     Latency the whole chain inherits: a saga waiting on this event waits this long at least.
        /// </remarks>
        public TimeSpan Interval => _interval;

        public int PublishBatch(IReadOnlyList<string> unpublished, Action<string> publish) {
            // ... SELECT id, payload FROM field_work_outbox WHERE published_at IS NULL ORDER BY id
            foreach (string message in unpublished) { publish(message); }

            return unpublished.Count;
        }

    }
}
