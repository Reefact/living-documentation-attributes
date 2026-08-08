#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.NameSample {

    // A botanic garden's living collection. Every plant in the ground has an accession number, and it has names:
    // Sorbus aucuparia, rowan, mountain ash, quickbeam, cuirn, sorbier des oiseleurs.
    //
    // Figures 5.1 and 5.2 are the pattern and there is nothing more to it: a name may be absent, and there may be
    // several. Neither is true of an identifier. A seedling collected on an expedition arrives with no name at
    // all; a species gets reclassified and the accepted name changes while the plant does not.
    //
    // NAME is worth annotating precisely because the mistake it prevents is invisible. Keying on a name works
    // perfectly — until two plants share one, or one is renamed, and by then everything that joined on it is
    // quietly wrong. Nothing in the type system distinguishes the string that identifies from the string that
    // merely labels, so the annotation is the only place the difference exists.
    //
    // The role is repeatable, which is figure 5.2 said in the vocabulary: a type may carry several members that
    // are names, and none of them is the name.

    /// <summary>
    ///     One plant in the ground.
    /// </summary>
    /// <remarks>
    ///     The accession number identifies it. Everything else it is called is a name, and no two of the members
    ///     below may be relied on to be unique or stable.
    /// </remarks>
    public sealed class Accession {

        private readonly List<string> _vernacularNames = new();

        public Accession(string accessionNumber, string? acceptedName = null) {
            AccessionNumber = accessionNumber;
            AcceptedName    = acceptedName;
        }

        /// <summary>
        ///     What identifies the plant. Not a name: issued once, never reused, and never changed by a
        ///     reclassification.
        /// </summary>
        public string AccessionNumber { get; }

        /// <summary>
        ///     The currently accepted botanical name, absent for material not yet determined.
        /// </summary>
        /// <remarks>
        ///     A name despite being scientific and despite looking canonical — it changes when the taxonomy
        ///     changes, which is exactly what an identifier must not do.
        /// </remarks>
        [Name]
        public string? AcceptedName { get; private set; }

        /// <summary>
        ///     What people call it, in any language. Several, ranked by nothing.
        /// </summary>
        [Name]
        public IReadOnlyList<string> VernacularNames => _vernacularNames;

        /// <summary>Adds a vernacular name.</summary>
        public void AlsoCalled(string name) {
            _vernacularNames.Add(name);
        }

        /// <summary>
        ///     Records a reclassification. The plant is the same plant, which is the point: the accession number
        ///     does not move.
        /// </summary>
        public void Redetermined(string acceptedName) {
            if (AcceptedName is not null) {
                _vernacularNames.Add(AcceptedName);
            }

            AcceptedName = acceptedName;
        }

    }

    /// <summary>
    ///     The collection, and the difference the annotation is about.
    /// </summary>
    public sealed class LivingCollection {

        private readonly Dictionary<string, Accession> _byAccessionNumber = new();

        /// <summary>Adds a plant, keyed on the thing that identifies it.</summary>
        public void Add(Accession accession) {
            _byAccessionNumber.Add(accession.AccessionNumber, accession);
        }

        /// <summary>One plant, found by the identifier. Exactly one, always.</summary>
        public Accession? ByAccessionNumber(string accessionNumber) {
            return _byAccessionNumber.TryGetValue(accessionNumber, out Accession? found) ? found : null;
        }

        /// <summary>
        ///     Plants matching a name — plural on purpose. A search by name returns a list because a name
        ///     identifies nothing, and a signature returning one would be the bug the annotation warns about.
        /// </summary>
        public IReadOnlyList<Accession> ByName(string name) {
            List<Accession> found = new();
            foreach (Accession accession in _byAccessionNumber.Values) {
                if (accession.AcceptedName == name || accession.VernacularNames.Contains(name)) {
                    found.Add(accession);
                }
            }

            return found;
        }

    }

}
