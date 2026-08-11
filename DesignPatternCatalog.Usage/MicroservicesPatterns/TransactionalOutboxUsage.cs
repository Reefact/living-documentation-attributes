#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.TransactionalOutboxSample {

    // Metering validates a reading and four other services need to hear about it. Writing the reading and
    // then publishing to the broker leaves a window: the service can commit and crash before the publish,
    // and the invoice is never raised. Publishing first leaves the opposite window, and a transaction
    // across the database and the broker is what nobody wants to operate.
    //
    // TRANSACTIONAL OUTBOX removes the window by making the send part of the write: the message goes into a
    // table in the same database, in the same transaction, and something else forwards it later. The cost is
    // moved rather than removed — the forwarding can happen twice, so every consumer has to be idempotent.

    /// <summary>
    ///     Metering's database, holding both the readings and the outbox.
    /// </summary>
    /// <remarks>
    ///     That these are one database is the whole mechanism. Splitting the outbox onto its own store would
    ///     compile, and would silently reintroduce the distributed transaction this pattern exists to avoid.
    /// </remarks>
    [TransactionalOutbox.Database]
    public interface IMeteringDatabase {

        void InTransaction(Action work);

    }

    /// <summary>
    ///     The table the messages wait in.
    /// </summary>
    /// <remarks>
    ///     Empty in a healthy system, which is exactly why nobody notices the day it stops being drained.
    ///     Naming it is what lets a check ask how old its oldest row is.
    /// </remarks>
    [TransactionalOutbox.MessageOutbox(Database = typeof(IMeteringDatabase))]
    public sealed class MeteringOutbox {

        private readonly Queue<string> _pending = new Queue<string>();

        public void Enqueue(string message) => _pending.Enqueue(message);

        public bool TryDequeue(out string? message) {
            if (_pending.Count == 0) {
                message = null;

                return false;
            }

            message = _pending.Dequeue();

            return true;
        }

    }

    /// <summary>
    ///     Metering, writing the reading and the message together.
    /// </summary>
    /// <remarks>
    ///     It holds no reference to a broker, and that absence is the pattern. What nothing catches is the
    ///     opposite mistake: a command that updates the readings and forgets the outbox row compiles, passes,
    ///     and drops an event on the floor.
    /// </remarks>
    [TransactionalOutbox.Sender(MessageOutbox = typeof(MeteringOutbox))]
    public sealed class MeteringService {

        private readonly IMeteringDatabase _database;
        private readonly MeteringOutbox    _outbox;

        public MeteringService(IMeteringDatabase database, MeteringOutbox outbox) {
            _database = database;
            _outbox   = outbox;
        }

        public void Validate(string supplyPoint, decimal kilowattHours) =>
            _database.InTransaction(() => {
                // ... the reading is marked valid here, in the same transaction
                _outbox.Enqueue($"MeterReadingValidated:{supplyPoint}:{kilowattHours}");
            });

    }

    /// <summary>
    ///     Moves what the outbox holds to the broker.
    /// </summary>
    /// <remarks>
    ///     It can publish the same message twice — crash between the publish and the record of it, and the
    ///     message goes again on restart. Every consumer downstream inherits that, whether or not its author
    ///     ever read this class.
    /// </remarks>
    [TransactionalOutbox.MessageRelay(MessageOutbox = typeof(MeteringOutbox))]
    public sealed class MeteringRelay {

        private readonly MeteringOutbox _outbox;

        public MeteringRelay(MeteringOutbox outbox) {
            _outbox = outbox;
        }

        public int Drain(Action<string> publish) {
            int sent = 0;
            while (_outbox.TryDequeue(out string? message)) {
                publish(message!);
                sent++;
            }

            return sent;
        }

    }
}
