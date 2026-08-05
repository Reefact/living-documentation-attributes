#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DomainDrivenDesign.ValueObjectSample {

    // Cattle traceability: an ear tag identifies an animal, and a weighing records what it weighed.
    //
    // Both are value objects in Evans' sense, and the reason is the same for each: they are not
    // things the herd owns, they are things the herd *says*. Two ear tags carrying the same country,
    // herd and animal number are not two tags — they are the same tag, written twice. Nothing about
    // one of them is more real than the other, so identity would be a fiction.
    //
    // The immutability is not a coding preference here, it is the model refusing a sentence that
    // makes no sense. "Correcting" the number on a tag does not correct anything: it silently makes
    // an animal into a different animal. A weighing behaves the same way — an animal weighed 412 kg
    // on that morning, and no later event changes what the scale read. What one does instead is
    // record a new tag, or a new weighing.
    //
    // This is where Evans parts company with Fowler. The Enterprise Application Architecture value
    // object asks only that equality not be based on identity, and tolerates a mutable one; the
    // attribute below derives from it, and adds the immutability that makes it a modelling decision.
    // Compare with EnterpriseApplicationArchitecture/ValueObjectUsage.cs, which is deliberately
    // mutable and would fail this reading.

    [ValueObject]
    public readonly record struct EarTag {

        public EarTag(string country, int herd, int animal) {
            if (country.Length != 2) { throw new ArgumentException("An ISO country code is two letters.", nameof(country)); }
            if (herd    <= 0) { throw new ArgumentOutOfRangeException(nameof(herd)); }
            if (animal  <= 0) { throw new ArgumentOutOfRangeException(nameof(animal)); }

            Country = country;
            Herd    = herd;
            Animal  = animal;
        }

        public string Country { get; }
        public int    Herd    { get; }
        public int    Animal  { get; }

        // Validated once, in the constructor, because a value object is never half valid: there is no
        // later moment at which it could be repaired.
        public override string ToString() => $"{Country} {Herd:D8} {Animal:D5}";

    }

    [ValueObject]
    public readonly record struct LiveWeight {

        public LiveWeight(decimal kilograms) {
            if (kilograms <= 0) { throw new ArgumentOutOfRangeException(nameof(kilograms)); }

            Kilograms = kilograms;
        }

        public decimal Kilograms { get; }

        // An operation on a value object answers with another value object rather than mutating this
        // one — the daily gain between two weighings is itself a value, not a change to either.
        public LiveWeight Plus(LiveWeight gain) => new(Kilograms + gain.Kilograms);

    }

}
