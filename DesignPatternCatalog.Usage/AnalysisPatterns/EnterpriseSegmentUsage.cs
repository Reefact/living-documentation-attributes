#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.EnterpriseSegmentSample {

    // A dairy co-operative's management accounts. The board wants margin by region, and then by product, and
    // then by region AND product for the two categories that lost money, and then the same split by channel
    // because the food-service business behaves nothing like retail.
    //
    // Written as classes, that is a class per combination — RegionMargin, ProductMargin, RegionProductMargin —
    // and each new question is a deployment. Written as columns on one report class, it is a migration.
    //
    // ENTERPRISE SEGMENT makes the combination a value. A segment names at most one element of each axis, the
    // axes are configured, and the elements nest, so a figure asked for New Zealand is the figures of its
    // cities and nobody wrote the roll-up as a special case.

    /// <summary>
    ///     An axis the business reports along.
    /// </summary>
    /// <remarks>
    ///     The knowledge level. Adding "customer tier" to the way the board slices the business is adding one of
    ///     these, not editing whatever identifies a slice.
    /// </remarks>
    [EnterpriseSegment.Dimension]
    public sealed class ReportingDimension {

        public ReportingDimension(string name, IReadOnlyList<Granularity> levels) {
            Name   = name;
            Levels = levels;
        }

        /// <summary>Geography, product, channel.</summary>
        public string Name { get; }

        /// <summary>Coarsest first. The order is what makes two segments comparable.</summary>
        public IReadOnlyList<Granularity> Levels { get; }

    }

    /// <summary>
    ///     A named rank of one axis's hierarchy.
    /// </summary>
    /// <remarks>
    ///     Held in order on the dimension, so an element's depth is derived rather than counted by whoever draws
    ///     the report.
    /// </remarks>
    [EnterpriseSegment.DimensionLevel(Dimension = typeof(ReportingDimension))]
    public sealed class Granularity {

        public Granularity(string name) {
            Name = name;
        }

        /// <summary>Region, country, territory — or category, product, stock unit.</summary>
        public string Name { get; }

    }

    /// <summary>
    ///     One value on one axis, and the parent of the values beneath it.
    /// </summary>
    /// <remarks>
    ///     The parent belongs to the same dimension, which is the constraint that keeps Waikato from being nested
    ///     under Cheese.
    /// </remarks>
    [EnterpriseSegment.DimensionElement(Dimension = typeof(ReportingDimension))]
    public sealed class DimensionValue {

        public DimensionValue(ReportingDimension dimension, Granularity level, string name, DimensionValue? parent) {
            if (parent is not null && parent.Dimension != dimension) {
                throw new ArgumentException("a parent belongs to the same dimension", nameof(parent));
            }
            Dimension = dimension;
            Level     = level;
            Name      = name;
            Parent    = parent;
        }

        public ReportingDimension Dimension { get; }

        public Granularity Level { get; }

        /// <summary>Waikato, New Zealand, Australasia.</summary>
        public string Name { get; }

        /// <summary>Null at the top. A segment naming a parent covers everything below it.</summary>
        public DimensionValue? Parent { get; }

    }

    /// <summary>
    ///     The slice itself: one element per axis, and the subject a figure is reported for.
    /// </summary>
    /// <remarks>
    ///     A value. Two segments naming the same elements are the same segment, which is what lets figures be
    ///     added up across a report without a key class.
    /// </remarks>
    [EnterpriseSegment.EnterpriseSegment]
    public sealed class Segment : IEquatable<Segment> {

        private readonly Dictionary<ReportingDimension, DimensionValue> _elements;

        public Segment(IEnumerable<DimensionValue> elements) {
            _elements = new Dictionary<ReportingDimension, DimensionValue>();
            foreach (DimensionValue element in elements) {
                if (_elements.ContainsKey(element.Dimension)) {
                    throw new ArgumentException($"two elements of {element.Dimension.Name}", nameof(elements));
                }
                _elements.Add(element.Dimension, element);
            }
        }

        /// <summary>
        ///     The elements named, keyed by dimension.
        /// </summary>
        /// <remarks>
        ///     The key is the assertion: a segment is a coordinate, so it cannot name two regions.
        /// </remarks>
        [EnterpriseSegment.Elements]
        public IReadOnlyDictionary<ReportingDimension, DimensionValue> Elements => _elements;

        public bool Equals(Segment? other) {
            if (other is null || other._elements.Count != _elements.Count) { return false; }
            foreach (KeyValuePair<ReportingDimension, DimensionValue> pair in _elements) {
                if (!other._elements.TryGetValue(pair.Key, out DimensionValue? mine) || mine != pair.Value) { return false; }
            }
            return true;
        }

        public override bool Equals(object? obj) => Equals(obj as Segment);

        public override int GetHashCode() => _elements.Count;

    }

}
