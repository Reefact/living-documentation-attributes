#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.ObjectEquivalenceSample {

    // A cross-matched catalogue of astronomical sources. A radio survey lists a source at one position, an
    // infrared survey lists one nearby, an optical catalogue lists two close together — and the question of
    // whether these are one object or several is not a fact anybody holds. It is a claim, made by a survey team
    // or by an automated cross-match, and the claims disagree.
    //
    // This is where OBJECT MERGE stops being the right pattern. A merge asserts sameness on the system's own
    // authority and cannot express doubt: it has no room for "the radio team says these are one source and the
    // optical team says they are two", which is the ordinary state of the field.
    //
    // OBJECT EQUIVALENCE makes the claim an object. It may be wrong, it may be withdrawn, and it may be
    // contradicted — none of which a merged record can say. Figure 5.7 also gives it two or MORE objects, so it
    // is a claim about a set rather than a pair, which matters: a three-way identification is one claim, not
    // three, and recording it as three pairs loses the fact that a team asserted all of it together.
    //
    // The asserters are the part of figure 5.7 that surprises, and the part worth annotating. An equivalence with
    // no author is indistinguishable from a fact — so a system that drops it cannot tell a careful identification
    // from an automated positional guess, and cannot report that two catalogues disagree at all.

    /// <summary>
    ///     One catalogued source, as one survey lists it.
    /// </summary>
    public sealed class CatalogueEntry {

        public CatalogueEntry(string catalogue, string designation, double rightAscension, double declination) {
            Catalogue      = catalogue;
            Designation    = designation;
            RightAscension = rightAscension;
            Declination    = declination;
        }

        /// <summary>Which survey lists it.</summary>
        public string Catalogue { get; }

        /// <summary>Its designation in that survey.</summary>
        public string Designation { get; }

        /// <summary>Right ascension, in degrees.</summary>
        public double RightAscension { get; }

        /// <summary>Declination, in degrees.</summary>
        public double Declination { get; }

    }

    /// <summary>
    ///     Whoever may hold an identification: a survey team, or an automated cross-match.
    /// </summary>
    [Party]
    public sealed class Authority {

        public Authority(string name, bool automated) {
            Name      = name;
            Automated = automated;
        }

        /// <summary>What it is called.</summary>
        public string Name { get; }

        /// <summary>Whether it is a pipeline rather than a person — which is why the asserter is kept.</summary>
        public bool Automated { get; }

    }

    /// <summary>
    ///     The claim that a set of catalogued sources are one object.
    /// </summary>
    /// <remarks>
    ///     A claim rather than a merge, because it may be wrong, withdrawn or contradicted. Over a set rather
    ///     than a pair, because a three-way identification is one claim.
    /// </remarks>
    [ObjectEquivalence.Equivalence]
    public sealed class Identification {

        private readonly List<CatalogueEntry> _entries;
        private readonly List<Authority>      _assertedBy;

        public Identification(IEnumerable<CatalogueEntry> entries, IEnumerable<Authority> assertedBy) {
            _entries    = new List<CatalogueEntry>(entries);
            _assertedBy = new List<Authority>(assertedBy);

            if (_entries.Count < 2) {
                throw new System.ArgumentException("an equivalence needs at least two entries", nameof(entries));
            }

            if (_assertedBy.Count == 0) {
                throw new System.ArgumentException(
                    "an equivalence with no asserter is indistinguishable from a fact", nameof(assertedBy));
            }
        }

        /// <summary>The entries claimed to be one object.</summary>
        public IReadOnlyList<CatalogueEntry> Entries => _entries;

        /// <summary>
        ///     Who holds the claim, one at least. Required at construction, which is the assertion made
        ///     structural.
        /// </summary>
        [ObjectEquivalence.Asserter]
        public IReadOnlyList<Authority> AssertedBy => _assertedBy;

        /// <summary>Whether the claim has been withdrawn.</summary>
        public bool Withdrawn { get; private set; }

        /// <summary>Withdraws it, which a merge could not do.</summary>
        public void Withdraw() {
            Withdrawn = true;
        }

        /// <summary>Whether this claim covers an entry.</summary>
        public bool Covers(CatalogueEntry entry) {
            return _entries.Contains(entry);
        }

    }

    /// <summary>
    ///     The identifications on record, and the question they make askable.
    /// </summary>
    public sealed class CrossMatch {

        private readonly List<Identification> _identifications = new();

        /// <summary>Records an identification.</summary>
        public void Add(Identification identification) {
            _identifications.Add(identification);
        }

        /// <summary>Live claims touching an entry.</summary>
        public IReadOnlyList<Identification> Covering(CatalogueEntry entry) {
            List<Identification> covering = new();
            foreach (Identification identification in _identifications) {
                if (!identification.Withdrawn && identification.Covers(entry)) {
                    covering.Add(identification);
                }
            }

            return covering;
        }

        /// <summary>
        ///     Whether an entry is claimed to be different things by different authorities — the question the
        ///     whole pattern exists to make askable, and which a merged record answers by silently picking one.
        /// </summary>
        public bool IsDisputed(CatalogueEntry entry) {
            IReadOnlyList<Identification> covering = Covering(entry);
            if (covering.Count < 2) {
                return false;
            }

            for (int i = 1; i < covering.Count; i++) {
                if (covering[i].Entries.Count != covering[0].Entries.Count) {
                    return true;
                }

                foreach (CatalogueEntry other in covering[i].Entries) {
                    if (!covering[0].Covers(other)) {
                        return true;
                    }
                }
            }

            return false;
        }

    }

}
