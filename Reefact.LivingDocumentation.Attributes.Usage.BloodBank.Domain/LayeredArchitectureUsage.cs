#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

// A blood establishment — collecting from donors, screening what is collected, issuing units to hospitals.
// It is told across four assemblies, because a LAYERED ARCHITECTURE is a partition and an assembly makes one
// set of claims. The other three are BloodBank.Ui, BloodBank.Application and BloodBank.Infrastructure.
//
// This one is the DOMAIN LAYER: the concepts, their state, and the rules that hold whatever calls them.
//
// The rule below is the whole argument for the pattern. A unit of red cells expires thirty-five days after
// collection, and issuing an expired unit is the kind of mistake that reaches a patient. That rule has to
// hold when the unit is issued from the counter, when it is issued by the overnight batch that supplies the
// air ambulance, and when it is issued by the import that reconciles a transfer from another centre. Three
// callers, one rule — and there is exactly one place it can live such that none of them can skip it.
//
// Put it in the screen and the batch does not have it. Put it in a stored procedure and the reviewer reading
// this class cannot see it. Put it here and the only way to issue a unit is to ask a unit.
//
// What the annotation adds is the CONVERSE, which is the part nobody enforces by hand: this assembly must
// reference nothing above it. Not the application layer, not the user interface, and — the one that actually
// happens — not the infrastructure. The day someone needs a donor's history and reaches for the data access
// library from in here, the model starts being shaped by what is cheap to query. Nothing about that day looks
// like a mistake; it looks like one sensible reference. An architecture rule ranging over these four
// annotations is what turns it back into one.
//
// Note also what is NOT here. There is no transaction, no unit of work, no notification to the hospital: all
// of that is coordination, it belongs to the application layer, and the model is smaller and more readable
// for not carrying it.

[assembly: LayeredArchitecture.Domain]

namespace Reefact.LivingDocumentation.Attributes.Usage.BloodBank.Domain.LayeredArchitectureSample {

    /// <summary>
    ///     One bag of red cells, from collection to issue or discard.
    /// </summary>
    public sealed class BloodUnit {

        private static readonly TimeSpan ShelfLife = TimeSpan.FromDays(35);

        public BloodUnit(string reference, string group, DateTime collectedOn) {
            Reference   = reference;
            Group       = group;
            CollectedOn = collectedOn;
        }

        public string   Reference   { get; }
        public string   Group       { get; }
        public DateTime CollectedOn { get; }
        public string?  IssuedTo    { get; private set; }

        public DateTime ExpiresOn => CollectedOn + ShelfLife;

        /// <summary>
        ///     Issues the unit to a hospital, or refuses to.
        /// </summary>
        /// <remarks>
        ///     The counter, the overnight batch and the transfer import all come through here, which is the only
        ///     arrangement in which none of them can be the one that forgets.
        /// </remarks>
        public void IssueTo(string hospital, DateTime on) {
            if (IssuedTo is not null) { throw new InvalidOperationException($"Unit {Reference} was already issued to {IssuedTo}."); }
            if (on > ExpiresOn) { throw new InvalidOperationException($"Unit {Reference} expired on {ExpiresOn:d}."); }

            IssuedTo = hospital;
        }

    }

    /// <summary>
    ///     What the domain layer needs from storage, declared here and implemented above.
    /// </summary>
    /// <remarks>
    ///     The interface belongs to the model because the model is what states its needs. Its implementation
    ///     belongs to infrastructure — which is how this assembly can reference nothing and still be persisted.
    /// </remarks>
    public interface IBloodUnitStore {

        BloodUnit? Find(string reference);

        void Save(BloodUnit unit);

    }

}
