#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.SubtypingObservationConceptsSample {

    // A plant health inspectorate's diagnosis register. Inspectors record findings against a vocabulary of some
    // thousands of terms, and the epidemiologists query it upward: how many wilts this season, how many virus
    // diseases, how many notifiable findings.
    //
    // Those questions only work if the vocabulary generalises. And it does not generalise as a tree: tomato
    // spotted wilt is BOTH a virus disease and a wilt, and it has to be counted under either without being
    // counted twice under both. Figure 3.10 marks the generalization {dag} for exactly this.
    //
    // That is what separates this pattern from PARTY TYPE GENERALIZATIONS, where figure 2.10 allows one
    // supertype. Both put the hierarchy at the knowledge level so a vocabulary is loaded rather than compiled;
    // only this one admits several parents, and a traversal written for the party-type case terminates here by
    // luck and reports one of the two.
    //
    // The two assertions the annotation licenses are the ones nothing else states: several supertypes are
    // permitted, and no cycle is. A cycle is one ordinary assignment, and what it breaks is the upward walk —
    // which stops terminating rather than returning a wrong count.

    /// <summary>
    ///     A term in the diagnostic vocabulary — a phenomenon type or a phenomenon indifferently.
    /// </summary>
    /// <remarks>
    ///     Generalises as a directed acyclic graph, which is the whole difference from the party-type case.
    /// </remarks>
    [SubtypingObservationConcepts.ObservationConcept]
    public sealed class DiagnosticConcept {

        private readonly List<DiagnosticConcept> _fallsUnder = new();

        public DiagnosticConcept(string name) {
            Name = name;
        }

        /// <summary>The term as the vocabulary spells it.</summary>
        public string Name { get; }

        /// <summary>
        ///     The broader terms this one falls under. Plural, and acyclic.
        /// </summary>
        [SubtypingObservationConcepts.Supertypes]
        public IReadOnlyList<DiagnosticConcept> FallsUnder => _fallsUnder;

        /// <summary>
        ///     Places this term under a broader one, refusing a cycle.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">If the link would close a cycle.</exception>
        public void PlaceUnder(DiagnosticConcept broader) {
            if (ReferenceEquals(broader, this) || broader.Reaches(this)) {
                throw new System.InvalidOperationException(
                    $"{Name} cannot fall under {broader.Name}: it would close a cycle");
            }

            _fallsUnder.Add(broader);
        }

        /// <summary>Whether this term reaches that one by walking upward.</summary>
        public bool Reaches(DiagnosticConcept concept) {
            foreach (DiagnosticConcept parent in _fallsUnder) {
                if (ReferenceEquals(parent, concept) || parent.Reaches(concept)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     This term and every term above it, deduplicated — a set rather than a chain, because two paths
        ///     upward may meet. Counting a finding once under a broad term depends on that deduplication.
        /// </summary>
        public IReadOnlySet<DiagnosticConcept> AllConcepts {
            get {
                HashSet<DiagnosticConcept> all = new() { this };
                foreach (DiagnosticConcept parent in _fallsUnder) {
                    all.UnionWith(parent.AllConcepts);
                }

                return all;
            }
        }

        /// <summary>Whether this term is, or falls under, that one.</summary>
        public bool Is(DiagnosticConcept concept) {
            return AllConcepts.Contains(concept);
        }

    }

}
