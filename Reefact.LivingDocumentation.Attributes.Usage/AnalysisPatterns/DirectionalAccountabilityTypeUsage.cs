#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.DirectionalAccountabilityTypeSample {

    // A hospital trust's internal audit function. The head of internal audit is answerable to the audit
    // committee for the opinion they give, and to nobody else for it — in particular not to the finance director,
    // whose controls they are auditing. That independence is the whole reason the function exists, and the
    // arrangement is what protects it.
    //
    // This is not a ladder. Nobody is being skipped: the audit committee is not two grades above the head of
    // audit, it is simply the body that may commission that particular responsibility. A LEVELED ladder would be
    // the wrong shape and would force an ordering the governance does not have.
    //
    // DIRECTIONAL ACCOUNTABILITY TYPE is the looser constraint that fits: two sets of party types, one
    // admissible at each end. It forbids reversal — an audit committee cannot be made answerable to the head of
    // audit — and it forbids the wrong commissioner, which is the one that matters here. It says nothing about
    // order, because there is none to say.
    //
    // What this buys is that the sentence "only the audit committee may commission an audit opinion" exists
    // somewhere a rule can read. Otherwise it lives in a terms-of-reference document, and the day someone adds a
    // reporting line from audit to the executive team, nothing objects: it compiles, it looks like an ordinary
    // management relationship, and the independence is gone without a single failing test.
    //
    // Fowler draws it in figure 2.13 beside LEVELED, both under the party-type-rules branch of accountability
    // type, with the hierarchic constraint on an independent axis. The role sits on the type object for the same
    // reason as its siblings: the admissible sets describe the kind, not any one instance.

    /// <summary>
    ///     A kind of accountability carrying the party types admissible at each end.
    /// </summary>
    /// <remarks>
    ///     Constrains direction without ordering: it says who may stand where, and nothing about levels.
    /// </remarks>
    [DirectionalAccountabilityType]
    public sealed class GovernanceRelationship {

        private readonly HashSet<string> _commissioners;
        private readonly HashSet<string> _responsibles;

        public GovernanceRelationship(string name, IEnumerable<string> commissioners, IEnumerable<string> responsibles) {
            Name           = name;
            _commissioners = new HashSet<string>(commissioners);
            _responsibles  = new HashSet<string>(responsibles);
        }

        /// <summary>What the terms of reference call it.</summary>
        public string Name { get; }

        /// <summary>The party types that may commission this responsibility.</summary>
        public IReadOnlyCollection<string> AdmissibleCommissioners => _commissioners;

        /// <summary>The party types that may hold it.</summary>
        public IReadOnlyCollection<string> AdmissibleResponsibles => _responsibles;

        /// <summary>
        ///     Establishes the responsibility, refusing an end the kind does not admit.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     If either party is of a type not admissible at the end it is standing at.
        /// </exception>
        public GovernanceAccountability Establish(GovernanceBody commissioner, GovernanceBody responsible) {
            if (!_commissioners.Contains(commissioner.BodyType)) {
                throw new InvalidOperationException($"a {commissioner.BodyType} may not commission {Name}");
            }

            if (!_responsibles.Contains(responsible.BodyType)) {
                throw new InvalidOperationException($"a {responsible.BodyType} may not hold {Name}");
            }

            return new GovernanceAccountability(this, commissioner, responsible);
        }

    }

    /// <summary>
    ///     One governance responsibility, of one kind.
    /// </summary>
    public sealed class GovernanceAccountability {

        internal GovernanceAccountability(GovernanceRelationship kind, GovernanceBody commissioner,
                                          GovernanceBody responsible) {
            Kind         = kind;
            Commissioner = commissioner;
            Responsible  = responsible;
        }

        /// <summary>What kind of relationship this is.</summary>
        public GovernanceRelationship Kind { get; }

        /// <summary>The body the responsibility is owed to.</summary>
        public GovernanceBody Commissioner { get; }

        /// <summary>The body that holds it.</summary>
        public GovernanceBody Responsible { get; }

    }

    /// <summary>
    ///     A committee, a director, or a function — anything the governance framework can place at an end.
    /// </summary>
    [Party]
    public sealed class GovernanceBody {

        public GovernanceBody(string name, string bodyType) {
            Name     = name;
            BodyType = bodyType;
        }

        /// <summary>Its name.</summary>
        public string Name { get; }

        /// <summary>Its party type, which is what the relationship kind is stated in terms of.</summary>
        public string BodyType { get; }

    }

}
