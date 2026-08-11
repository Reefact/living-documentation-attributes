#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.EventSourcingSample {

    // A supply point's connection history is what the regulator audits: when it was energised, every
    // change of occupier, every disconnection for non-payment and every reconnection. Storing the current
    // state and a separate audit log gave two answers to the same question, and they disagreed.
    //
    // EVENT SOURCING stores only the history and computes the state. Saving the change and publishing it
    // become one append, which is the problem it is here to solve — and in exchange the events are now the
    // schema: an event written today will be replayed by code nobody has written yet.

    /// <summary>
    ///     One thing that happened to a supply point.
    /// </summary>
    /// <remarks>
    ///     Annotated on the base, which is where the role is introduced. It is kept for the life of the
    ///     supply point, so it is the hardest type here to change and the easiest to change by accident.
    /// </remarks>
    [EventSourcing.Event]
    public abstract class SupplyPointEvent {

        protected SupplyPointEvent(DateTime occurredOn) {
            OccurredOn = occurredOn;
        }

        public DateTime OccurredOn { get; }

    }

    /// <summary>The supply point was energised.</summary>
    public sealed class Energised : SupplyPointEvent {

        public Energised(DateTime occurredOn) : base(occurredOn) { }

    }

    /// <summary>The supply was cut for non-payment.</summary>
    public sealed class Disconnected : SupplyPointEvent {

        public Disconnected(DateTime occurredOn, string reason) : base(occurredOn) {
            Reason = reason;
        }

        public string Reason { get; }

    }

    /// <summary>
    ///     The supply point, whose state is never stored.
    /// </summary>
    /// <remarks>
    ///     <c>Apply</c> is the whole of the model: changing what an old event means to it rewrites what
    ///     happened, silently and retroactively, which is the cost this pattern does not advertise.
    /// </remarks>
    [EventSourcing.Aggregate]
    public sealed class SupplyPoint {

        private readonly List<SupplyPointEvent> _history = new List<SupplyPointEvent>();

        public bool IsLive { get; private set; }

        public void Apply(SupplyPointEvent @event) {
            _history.Add(@event);
            IsLive = @event is Energised;
        }

        public IReadOnlyList<SupplyPointEvent> History => _history;

    }

    /// <summary>
    ///     Where the events are kept, and how they leave.
    /// </summary>
    /// <remarks>
    ///     Append and publish are one operation here, which is the point: there is no window in which the
    ///     database has the change and the broker does not.
    /// </remarks>
    [EventSourcing.EventStore]
    public interface ISupplyPointEvents {

        void Append(string supplyPoint, SupplyPointEvent @event);

        IReadOnlyList<SupplyPointEvent> Since(string supplyPoint, int fromVersion);

    }

    /// <summary>
    ///     A saved state, so that replay does not start in 1998.
    /// </summary>
    /// <remarks>
    ///     It says the same thing the events say, which is what makes it an optimisation and what makes it
    ///     wrong the day one of them is applied differently.
    /// </remarks>
    [EventSourcing.Snapshot(Aggregate = typeof(SupplyPoint))]
    public sealed class SupplyPointSnapshot {

        public SupplyPointSnapshot(int version, bool isLive) {
            Version = version;
            IsLive  = isLive;
        }

        public int Version { get; }

        public bool IsLive { get; }

    }
}
