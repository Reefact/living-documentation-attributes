#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.OrganizationHierarchiesSample {

    // A diocesan office. Province, diocese, deanery, parish — four levels, one tree, and a nesting that has not
    // changed since the nineteenth century: a parish sits in a deanery, a deanery in a diocese, a diocese in a
    // province, and a province in nothing.
    //
    // ORGANIZATION HIERARCHIES is the honest model for that, and it is catalogued because being the simple case
    // is not the same as being a naive one. What it asserts is exactly two things: one parent, and a nesting
    // fixed in the class hierarchy. Both are true here, both are cheap, and the alternative — a structure object
    // with its own type, configured at runtime — would be machinery bought for flexibility this domain does not
    // want. A deanery inside a parish is not a case to configure away; it is an error, and stating it as an
    // invariant on the class is the clearest place to say so.
    //
    // What is worth annotating is where it ends. The pattern has no type object, so every constraint is written
    // into a subtype, and the day the office needs a second structure — a schools trust that cuts across
    // deaneries, a safeguarding region that groups three dioceses — every one of those invariants has to know
    // about both structures, and the model that was cheapest becomes the one that resists. That is Fowler's own
    // sequence: the hierarchy, then a second hierarchy, then the structure object.
    //
    // The single parent is the assertion a rule can range over. Every traversal here is written against it, and
    // the day it quietly becomes a collection nothing stops compiling.

    /// <summary>
    ///     A node of the single tree.
    /// </summary>
    /// <remarks>
    ///     No structure type and no structure object: which kinds may nest inside which is stated by each
    ///     subtype below. That is the pattern, and also its ceiling.
    /// </remarks>
    [OrganizationHierarchies.Organization]
    public abstract class EcclesiasticalBody {

        protected EcclesiasticalBody(string name, EcclesiasticalBody? contains) {
            Name = name;
            if (contains is not null && !MayNestWithin(contains)) {
                throw new System.ArgumentException($"a {GetType().Name} may not sit within a {contains.GetType().Name}", nameof(contains));
            }

            Within = contains;
        }

        /// <summary>Its name.</summary>
        public string Name { get; }

        /// <summary>
        ///     The body directly above, absent at the root. Single-valued, which is the whole claim.
        /// </summary>
        [OrganizationHierarchies.Parent]
        public EcclesiasticalBody? Within { get; }

        /// <summary>
        ///     Whether this kind of body may sit within that one. Stated per subtype because there is nowhere
        ///     else to state it.
        /// </summary>
        protected abstract bool MayNestWithin(EcclesiasticalBody candidate);

        /// <summary>This body and every body above it, root last.</summary>
        public IReadOnlyList<EcclesiasticalBody> UpwardChain {
            get {
                List<EcclesiasticalBody> chain = new();
                EcclesiasticalBody?      body  = this;
                while (body is not null) {
                    chain.Add(body);
                    body = body.Within;
                }

                return chain;
            }
        }

    }

    /// <summary>The root. Sits within nothing.</summary>
    public sealed class Province : EcclesiasticalBody {

        public Province(string name) : base(name, null) { }

        /// <inheritdoc />
        protected override bool MayNestWithin(EcclesiasticalBody candidate) {
            return false;
        }

    }

    /// <summary>Sits within a province.</summary>
    public sealed class Diocese : EcclesiasticalBody {

        public Diocese(string name, Province province) : base(name, province) { }

        /// <inheritdoc />
        protected override bool MayNestWithin(EcclesiasticalBody candidate) {
            return candidate is Province;
        }

    }

    /// <summary>Sits within a diocese.</summary>
    public sealed class Deanery : EcclesiasticalBody {

        public Deanery(string name, Diocese diocese) : base(name, diocese) { }

        /// <inheritdoc />
        protected override bool MayNestWithin(EcclesiasticalBody candidate) {
            return candidate is Diocese;
        }

    }

    /// <summary>Sits within a deanery, and contains nothing.</summary>
    public sealed class Parish : EcclesiasticalBody {

        public Parish(string name, Deanery deanery) : base(name, deanery) { }

        /// <inheritdoc />
        protected override bool MayNestWithin(EcclesiasticalBody candidate) {
            return candidate is Deanery;
        }

    }

}
