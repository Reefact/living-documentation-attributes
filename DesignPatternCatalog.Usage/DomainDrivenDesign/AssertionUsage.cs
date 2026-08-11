#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

namespace DesignPatternCatalog.Usage.DomainDrivenDesign.AssertionSample {

    // Aircraft maintenance: the logbook of an engine, and the hours it may fly before overhaul.
    //
    // "Hours since overhaul never exceed the certified interval" is not a validation of an input. It
    // is a statement that is true of the engine at every instant, and an engine for which it is false
    // is not a badly filled form — it is an engine that must not fly.
    //
    // Left implicit, that sentence lives in whoever last read the maintenance manual. The operations
    // below look reasonable without it: recording a flight adds hours, an overhaul resets them.
    // Nothing in either signature says which combinations of them are allowed, so the rule has to be
    // rediscovered by reading both and holding them side by side — and rediscovered again by the next
    // person who adds a third operation.
    //
    // Evans' answer is to state the contract rather than infer it: the post-condition of each
    // operation, and the invariant of the type, written down and checked. `CheckInvariant` below is
    // that sentence, in one place, called from every operation that could break it.
    //
    // The annotation makes it something a tool can range over. Once the invariant method is named,
    // a rule can require that every public mutating operation of an annotated type ends by calling
    // it — which is exactly the kind of check that catches the third operation somebody adds in a
    // hurry two years from now.

    [ValueObject]
    public readonly record struct FlightHours(decimal Value) {

        public FlightHours Plus(FlightHours other) => new(Value + other.Value);

    }

    [Entity]
    [Assertion]
    public sealed class EngineLogbook {

        private readonly FlightHours _certifiedInterval;

        public EngineLogbook(string serialNumber, FlightHours certifiedInterval) {
            SerialNumber       = serialNumber;
            _certifiedInterval = certifiedInterval;
            SinceOverhaul      = new FlightHours(0m);

            CheckInvariant();
        }

        public string      SerialNumber  { get; }
        public FlightHours SinceOverhaul { get; private set; }

        /// <summary>
        ///     Post-condition: the hours since overhaul have increased by <paramref name="flown" />, and the engine is
        ///     still within its certified interval. An engine that would exceed it is grounded instead.
        /// </summary>
        [Assertion]
        public void RecordFlight(FlightHours flown) {
            FlightHours candidate = SinceOverhaul.Plus(flown);

            if (candidate.Value > _certifiedInterval.Value) {
                throw new InvalidOperationException($"Engine {SerialNumber} would exceed its {_certifiedInterval.Value} h interval.");
            }

            SinceOverhaul = candidate;

            CheckInvariant();
        }

        /// <summary>
        ///     Post-condition: the hours since overhaul are zero.
        /// </summary>
        [Assertion]
        public void Overhaul() {
            SinceOverhaul = new FlightHours(0m);

            CheckInvariant();
        }

        // The invariant of the type, stated once and checked rather than assumed. Every operation
        // above ends here, which is the property a rule over this annotation can require.
        [Assertion]
        private void CheckInvariant() {
            if (SinceOverhaul.Value < 0m || SinceOverhaul.Value > _certifiedInterval.Value) {
                throw new InvalidOperationException($"Engine {SerialNumber} is outside its certified interval.");
            }
        }

    }

}
