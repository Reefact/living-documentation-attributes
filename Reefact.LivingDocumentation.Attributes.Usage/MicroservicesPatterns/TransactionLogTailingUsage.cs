#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.TransactionLogTailingSample {

    // Metering's outbox has to be drained, and asking the table every second is a query per second per
    // instance against the busiest database the grid operator runs. The database is already writing every
    // committed row to its own log, and reading that costs it nothing extra.
    //
    // TRANSACTION LOG TAILING follows the log rather than the table. Nothing is missed, because the log is
    // what commit means. What it costs is written into the class: this is Postgres, and it will stay
    // Postgres until somebody rewrites it.

    /// <summary>
    ///     Follows the write-ahead log and publishes what the outbox received.
    /// </summary>
    /// <remarks>
    ///     Accurate by construction and tied to one engine by the same construction. A migration to another
    ///     database is a rewrite of this class, and this annotation is where a planner finds that out before
    ///     the migration rather than during it.
    /// </remarks>
    [TransactionLogTailing]
    public sealed class PostgresOutboxTail {

        private readonly List<string> _published = new List<string>();

        private long _lastSequence;

        /// <summary>
        ///     One batch of log records, as the replication slot hands them over.
        /// </summary>
        public void OnWalRecords(IEnumerable<(long Sequence, string Table, string Payload)> records, Action<string> publish) {
            foreach ((long sequence, string table, string payload) in records) {
                if (table != "metering_outbox") { continue; }
                if (sequence <= _lastSequence) { continue; }

                publish(payload);
                _published.Add(payload);
                _lastSequence = sequence;
            }
        }

        /// <summary>
        ///     Where the tail has got to, which is the only state it has and the only thing to lose.
        /// </summary>
        public long LastSequence => _lastSequence;

    }
}
