#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DomainDrivenDesign.StandaloneClassSample {

    // Brewing: the alcoholic strength of a batch, from the gravity readings taken before and after
    // fermentation.
    //
    // The arithmetic is fixed — it comes from the trade, not from this system — and it is quoted in
    // the duty return, on the label, and in the brewer's own quality log. It is exactly the kind of
    // thing that ends up as a private method on whichever class needed it first, and is then
    // reimplemented slightly differently by the second caller.
    //
    // What Evans is asking for here is not "extract a helper". It is a judgement about the cost of
    // reading. Every dependency a class declares is something the reader has to hold in mind before
    // they can be sure they understand it; a class that depends on the batch, the recipe, the vessel
    // and the duty schedule can only be understood by someone who already knows all four. A class
    // that depends on nothing can be read in one sitting, tested with two numbers, and trusted
    // afterwards.
    //
    // That is why the type below takes gravities and returns a strength, and knows nothing about
    // batches, recipes, vessels or duty. Note what is absent: no injected service, no repository, no
    // clock, no configuration. It could be moved to another codebase unchanged, which is the
    // practical test of the pattern — and the check a rule over this annotation can make, by
    // examining what the type's fields and signatures refer to.

    [ValueObject]
    public readonly record struct SpecificGravity {

        public SpecificGravity(decimal value) {
            if (value is < 0.980m or > 1.200m) { throw new ArgumentOutOfRangeException(nameof(value)); }

            Value = value;
        }

        public decimal Value { get; }

    }

    [ValueObject]
    public readonly record struct AlcoholByVolume(decimal Percent);

    [StandaloneClass]
    public sealed class AlcoholicStrength {

        // The trade formula, in one place. Nothing above this line refers to anything outside the
        // file, which is what the annotation claims.
        [SideEffectFreeFunction]
        public AlcoholByVolume Of(SpecificGravity original, SpecificGravity final) {
            if (final.Value > original.Value) { throw new ArgumentException("Fermentation lowers gravity.", nameof(final)); }

            decimal percent = (original.Value - final.Value) * 131.25m;

            return new AlcoholByVolume(Math.Round(percent, 2));
        }

    }

}
