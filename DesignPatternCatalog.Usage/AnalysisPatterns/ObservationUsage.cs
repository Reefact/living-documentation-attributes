#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.ObservationSample {

    // A national soil survey. At every sampling point the field team records a mixture of two kinds of fact: pH
    // 6.2, organic carbon 3.1 per cent — and texture "sandy clay loam", drainage class "imperfectly drained",
    // parent material "glacial till".
    //
    // The first kind is a MEASUREMENT. The second is not a measurement with a missing number: there is no
    // arithmetic on textures at all. A model that forces it into a quantity ends up with a texture code that can
    // be averaged, and the mean of "sandy clay loam" and "silt" is a number somebody will eventually put on a
    // map.
    //
    // OBSERVATION is the supertype both belong to, and naming it is what stops everything true of observations
    // in general from being said twice. Who recorded it, when, whether it was rejected, what evidence it rests
    // on — all of that attaches here, once.
    //
    // The phenomenon is the third role and the one that makes a category observation checkable. Its admissible
    // values hang off the phenomenon type at the knowledge level, so "sandy clay loam" is refusable when the
    // type is drainage class, rather than merely surprising.

    /// <summary>
    ///     Anything recorded about a sampling point at a time.
    /// </summary>
    /// <remarks>
    ///     Everything true of observations in general lives here — including rejection, which is why a model
    ///     that generalises only quantities has to say it twice.
    /// </remarks>
    [Observation.Observation]
    public abstract class SoilObservation {

        protected SoilObservation(string samplingPoint, PropertyType type, DateOnly on) {
            SamplingPoint = samplingPoint;
            Type          = type;
            On            = on;
        }

        /// <summary>Where it was recorded.</summary>
        public string SamplingPoint { get; }

        /// <summary>What kind of property was observed.</summary>
        public PropertyType Type { get; }

        /// <summary>When.</summary>
        public DateOnly On { get; }

        /// <summary>Whether the observation has been withdrawn.</summary>
        public bool Rejected { get; private set; }

        /// <summary>Withdraws the observation without deleting it.</summary>
        public void Reject() {
            Rejected = true;
        }

        /// <summary>How it reads on a report.</summary>
        public abstract string Reading { get; }

    }

    /// <summary>
    ///     An observation whose value is a quantity.
    /// </summary>
    public sealed class SoilMeasurement : SoilObservation {

        public SoilMeasurement(string samplingPoint, PropertyType type, decimal amount, string unit, DateOnly on)
            : base(samplingPoint, type, on) {
            Amount = amount;
            Unit   = unit;
        }

        /// <summary>The amount.</summary>
        public decimal Amount { get; }

        /// <summary>Its unit.</summary>
        public string Unit { get; }

        /// <inheritdoc />
        public override string Reading => $"{Amount} {Unit}";

    }

    /// <summary>
    ///     An observation whose value is one of a fixed set of categories.
    /// </summary>
    /// <remarks>
    ///     Nothing about it can be added, scaled or averaged. Refusing the value the type does not admit is the
    ///     assertion the pattern licenses.
    /// </remarks>
    [Observation.CategoryObservation]
    public sealed class SoilCategoryObservation : SoilObservation {

        public SoilCategoryObservation(string samplingPoint, PropertyType type, Category category, DateOnly on)
            : base(samplingPoint, type, on) {
            if (!type.Admits(category)) {
                throw new ArgumentException($"{category.Name} is not a {type.Name}", nameof(category));
            }

            Category = category;
        }

        /// <summary>The category observed.</summary>
        public Category Category { get; }

        /// <inheritdoc />
        public override string Reading => Category.Name;

    }

    /// <summary>
    ///     One admissible value of a property type.
    /// </summary>
    [Observation.Phenomenon]
    public sealed class Category {

        public Category(string name) {
            Name = name;
        }

        /// <summary>What it is called: "sandy clay loam", "imperfectly drained".</summary>
        public string Name { get; }

    }

    /// <summary>
    ///     What kind of property may be observed, and — where it is categorical — which values it admits.
    /// </summary>
    public sealed class PropertyType {

        private readonly List<Category> _categories = new();

        public PropertyType(string name, params Category[] categories) {
            Name = name;
            _categories.AddRange(categories);
        }

        /// <summary>pH, organic carbon, texture, drainage class.</summary>
        public string Name { get; }

        /// <summary>The categories it admits, empty when the type is measured rather than categorised.</summary>
        public IReadOnlyList<Category> Categories => _categories;

        /// <summary>Whether this type admits that category.</summary>
        public bool Admits(Category category) {
            return _categories.Contains(category);
        }

    }

}
