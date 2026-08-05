#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DomainDrivenDesign.AggregateSample {

    // Winemaking: a vintage and the wines blended into it.
    //
    // A blend is not a list of components. It is a list of components whose proportions add up to
    // exactly one hundred percent, and that sentence is the whole reason the aggregate exists. There
    // is no moment at which a 97% blend is acceptable and will be fixed later — the appellation rules
    // are checked on the declaration, and a blend that does not sum is not a draft, it is wrong.
    //
    // The invariant spans several objects, so no single component can enforce it: each one only knows
    // its own share. Something has to hold the boundary and be responsible for the whole, and that is
    // the root.
    //
    // What the pattern then buys is a rule that can actually be enforced: everything outside goes
    // through the root. `Vintage` has no public setter on its components and hands out a read-only
    // view, so there is no path by which a caller can add a component without passing through the
    // check. A `List<BlendComponent>` exposed as a property would have made the invariant a comment.
    //
    // Note what is *not* here: no component is reachable by identity from outside. A caller cannot
    // hold a `BlendComponent` and ask the system about it — it asks the vintage. That is what makes
    // the boundary real rather than decorative, and it is what a rule over these annotations can
    // check: no repository for a member, no member in a public signature outside its root.

    [Aggregate.Root]
    [Entity]
    public sealed class Vintage {

        private readonly List<BlendComponent> _components = new();

        public Vintage(string appellation, int year) {
            Appellation = appellation;
            Year        = year;
        }

        public string Appellation { get; }
        public int    Year        { get; }

        // Read-only on the way out: the only way to change the blend is the method below, which is
        // the only place the invariant is known.
        public IReadOnlyList<BlendComponent> Components => _components;

        public void Declare(params BlendComponent[] components) {
            decimal total = components.Sum(component => component.Share);

            // The invariant of the whole, checked by the only participant that can see the whole.
            if (total != 100m) { throw new InvalidOperationException($"A blend must total 100%, not {total}%."); }

            _components.Clear();
            _components.AddRange(components);
        }

    }

    [Aggregate.Member(Root = typeof(Vintage))]
    [ValueObject]
    public readonly record struct BlendComponent(string Grape, decimal Share) {

        // A member of the boundary, and a value object besides: two blend components carrying the
        // same grape and the same share are the same statement about the wine, not two of them.

    }

}
