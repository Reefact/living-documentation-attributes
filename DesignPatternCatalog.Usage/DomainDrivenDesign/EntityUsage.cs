#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

namespace DesignPatternCatalog.Usage.DomainDrivenDesign.EntitySample {

    // Rail freight: a wagon, followed through a fleet over decades.
    //
    // Everything you could describe a wagon by will change. It is repainted, re-tared after a repair,
    // it moves from yard to yard, its bogies are replaced, it is leased to another operator. Two
    // wagons can leave the workshop with the same tare, the same capacity and the same livery and
    // still be two wagons — and the same wagon, twenty years apart, matches none of what was recorded
    // about it on delivery.
    //
    // That is what makes it an entity rather than a value object: the model needs to say "this one",
    // and no description does that. The registration number is not one attribute among others, it is
    // the thread that makes the twenty years one wagon. Equality follows it and nothing else.
    //
    // The practical consequence is visible below: the identity is settled at construction and has no
    // setter, while everything that can legitimately change is mutable through a method that says
    // what happened. An entity is mutable *on purpose* — forbidding that would just be a value object
    // wearing an identifier.

    [Entity]
    public sealed class Wagon {

        private readonly List<string> _movements = new();

        public Wagon(string registration, decimal tareTonnes) {
            Registration = registration;
            TareTonnes   = tareTonnes;
            Location     = "workshop";
        }

        // The identity: given at construction, never reassigned. It is what the equality below reads,
        // and what a repository will key on.
        public string Registration { get; }

        public decimal TareTonnes { get; private set; }
        public string  Location   { get; private set; }

        public IReadOnlyList<string> Movements => _movements;

        // A repair changes what the wagon weighs empty. It is still the same wagon — which is exactly
        // the sentence a value object could not have expressed.
        public void ReTareAfterRepair(decimal tareTonnes) => TareTonnes = tareTonnes;

        public void MoveTo(string yard) {
            _movements.Add($"{Location} → {yard}");
            Location = yard;
        }

        // Equality on identity, not on state. Two wagons that happen to weigh the same are not one
        // wagon, and a wagon whose tare changed this morning is not a new one.
        public override bool Equals(object? obj) => obj is Wagon other && other.Registration == Registration;

        public override int GetHashCode() => Registration.GetHashCode();

    }

}
