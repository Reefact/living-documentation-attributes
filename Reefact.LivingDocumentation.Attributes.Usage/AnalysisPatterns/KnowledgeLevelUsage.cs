#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.KnowledgeLevelSample {

    // A crop insurance scheme. An actuary decides, every spring, what may be covered and on what terms: hail
    // on cereals with a ten per cent excess, frost on vines only above two hundred metres, drought not at all
    // this year. A hundred and forty thousand growers then buy cover under those terms.
    //
    // The two things in that paragraph are not the same kind of thing, and the whole pattern is noticing it.
    // What the actuary decides is *what may happen*. What a grower buys is *what did happen*. A model that
    // has one class for both has to be redeployed every time the actuary changes their mind — which is to say
    // every spring, and twice more when the reinsurer pushes back.
    //
    // KNOWLEDGE LEVEL splits them. The terms become objects the actuary configures; a policy points at the
    // terms it was written under. The scheme changes by loading data, and a season already sold keeps the
    // terms it was sold under, which is the second thing this buys and the one nobody expects: overwriting a
    // rule in place would silently restate history.
    //
    // The direction of the reference is the assertion, and it is the one worth checking mechanically. Every
    // operational object names its knowledge-level counterpart; nothing at the knowledge level names an
    // operational object. It is easy to break and it breaks quietly — a `CoverTerms.Sold` count added for a
    // report is one line, it compiles, and the level that was supposed to be pure configuration now depends
    // on what has been sold. From there the two collapse back into one, slowly, and nobody notices the day it
    // happens.
    //
    // Fowler names this in the accountability chapter as the *accountability knowledge level*; Evans reaches
    // it in Domain-Driven Design as KNOWLEDGE LEVEL and points back here. It is catalogued where the work
    // that named it put it.

    /// <summary>
    ///     What the scheme allows: a peril, a crop, and the terms on which the pair may be covered.
    /// </summary>
    /// <remarks>
    ///     Populated by the actuary rather than by a release. It refers to no policy, and must not: a rule
    ///     that mentions one of the things it permits has stopped being a rule.
    /// </remarks>
    [KnowledgeLevel.Knowledge]
    public sealed class CoverTerms {

        public CoverTerms(string peril, string crop, decimal excessRate, int? minimumAltitudeInMetres) {
            Peril                   = peril;
            Crop                    = crop;
            ExcessRate              = excessRate;
            MinimumAltitudeInMetres = minimumAltitudeInMetres;
        }

        /// <summary>Hail, frost, drought.</summary>
        public string Peril { get; }

        /// <summary>The crop these terms are for.</summary>
        public string Crop { get; }

        /// <summary>The share of a loss the grower carries.</summary>
        public decimal ExcessRate { get; }

        /// <summary>Where the terms only apply above a height, that height.</summary>
        public int? MinimumAltitudeInMetres { get; }

        /// <summary>Whether a parcel at this altitude may be covered under these terms at all.</summary>
        public bool Admits(int altitudeInMetres) {
            return MinimumAltitudeInMetres is null || altitudeInMetres >= MinimumAltitudeInMetres;
        }

    }

    /// <summary>
    ///     What a grower actually bought, for one parcel, for one season.
    /// </summary>
    /// <remarks>
    ///     Holds the terms it was written under rather than a copy of them, so that a season already sold is
    ///     not restated when the actuary changes next spring's rules.
    /// </remarks>
    [KnowledgeLevel.Operational(Knowledge = typeof(CoverTerms))]
    public sealed class Cover {

        public Cover(string parcelReference, int altitudeInMetres, CoverTerms terms, decimal insuredValue,
                     DateOnly writtenOn) {
            if (!terms.Admits(altitudeInMetres)) {
                throw new ArgumentException($"the terms for {terms.Peril} on {terms.Crop} do not reach this parcel", nameof(terms));
            }

            ParcelReference  = parcelReference;
            AltitudeInMetres = altitudeInMetres;
            Terms            = terms;
            InsuredValue     = insuredValue;
            WrittenOn        = writtenOn;
        }

        /// <summary>The parcel covered.</summary>
        public string ParcelReference { get; }

        /// <summary>Its altitude, which some terms turn on.</summary>
        public int AltitudeInMetres { get; }

        /// <summary>
        ///     The terms this cover was written under. The reference runs this way and only this way.
        /// </summary>
        public CoverTerms Terms { get; }

        /// <summary>What it is insured for.</summary>
        public decimal InsuredValue { get; }

        /// <summary>When it was written.</summary>
        public DateOnly WrittenOn { get; }

        /// <summary>What the scheme pays on a loss of the given size, after the excess in the terms.</summary>
        public decimal Settlement(decimal loss) {
            decimal capped = Math.Min(loss, InsuredValue);

            return capped - capped * Terms.ExcessRate;
        }

    }

    /// <summary>
    ///     The season's terms, as the actuary leaves them.
    /// </summary>
    /// <remarks>
    ///     Deliberately a collection of knowledge-level objects and nothing else. A count of policies sold
    ///     would be the one line that turns this into an operational object.
    /// </remarks>
    public sealed class Scheme {

        private readonly List<CoverTerms> _terms = new();

        /// <summary>Everything the scheme allows this season.</summary>
        public IReadOnlyList<CoverTerms> Terms => _terms;

        /// <summary>Adds terms. This is what "changing the rules" means here — no deployment.</summary>
        public void Allow(CoverTerms terms) {
            _terms.Add(terms);
        }

    }

}
