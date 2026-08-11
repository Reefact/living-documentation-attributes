#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     EnterpriseSegment (Analysis Patterns) — Identifies a slice of the enterprise by naming one element of each
    ///     of several dimensions, so that a figure can be reported for a combination nobody declared a class for.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Analysis Patterns</i>, 1997.
    ///     </para>
    /// </remarks>
    public static class EnterpriseSegment {

        /// <summary>
        ///     Role played by a type or a member in the EnterpriseSegment design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The slice itself, holding at most one element per dimension. It is a value — two segments naming the
        ///     same elements are the same segment — and it is what an observation is made about, so that 'retail, in
        ///     Auckland, of dairy products' is a subject rather than a report heading.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class EnterpriseSegmentAttribute : Role { }

        /// <summary>
        ///     An axis a segment may name an element of: geography, industry, product. It is an object rather than a
        ///     field of the segment, which is what lets an axis be added to the business without changing the type that
        ///     identifies a slice of it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class DimensionAttribute : Role { }

        /// <summary>
        ///     A value on one axis, and the parent of others on the same axis — Auckland within New Zealand within
        ///     Australasia. A parent is of the same subtype as its children, so an element never leaves its own
        ///     dimension, and a segment that names a parent covers everything beneath it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class DimensionElementAttribute : Role {

            /// <summary>
            ///     The <see cref="DimensionAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Dimension { get; init; }

        }

        /// <summary>
        ///     A named rank of one dimension's hierarchy, such as city or country, held in order on the dimension so
        ///     that the depth an element sits at is derived rather than counted. It is what lets two segments be
        ///     compared at the same granularity instead of by accident.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class DimensionLevelAttribute : Role {

            /// <summary>
            ///     The <see cref="DimensionAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Dimension { get; init; }

        }

        /// <summary>
        ///     The dimension elements the segment names, keyed by dimension. The key is the assertion: a segment may
        ///     not name two elements of one axis, which is what makes it a coordinate rather than a bag of labels.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ElementsAttribute : Role { }

    }

}
