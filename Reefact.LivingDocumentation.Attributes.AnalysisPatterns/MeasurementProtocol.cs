#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     MeasurementProtocol (Analysis Patterns) — Makes the method behind a measurement an object stating what it
    ///     takes and what it yields, so that a figure can be recomputed and explained rather than only stored.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         A narrower case of Protocol: every participant annotated here is one of those too, and a consumer asking
    ///         for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Analysis Patterns</i>, 1997.
    ///     </para>
    /// </remarks>
    public static class MeasurementProtocol {

        /// <summary>
        ///     Role played by a type or a member in the MeasurementProtocol design pattern.
        /// </summary>
        public abstract class Role : ProtocolAttribute { }

        /// <summary>
        ///     The protocol as a type object: it names the phenomenon type it results in, and in order the phenomenon
        ///     types it takes as inputs. What it adds to a protocol is that it says enough for a measurement to be
        ///     produced, and not only enough for two figures obtained differently to be kept apart.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class MeasurementProtocolAttribute : Role { }

        /// <summary>
        ///     A protocol whose result is observed rather than computed — someone read an instrument. It takes no
        ///     inputs, and it is where the numbers every calculation stands on enter the model.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class SourceMeasurementProtocolAttribute : Role { }

        /// <summary>
        ///     A protocol whose result is computed from measurements of its input types by a formula it holds. Every
        ///     measurement made under it is a calculated measurement and every calculated measurement is made under
        ///     one, which is the constraint stated in both directions.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class CalculatedMeasurementProtocolAttribute : Role { }

        /// <summary>
        ///     The formula a calculated protocol holds, as an object rather than as code inside the protocol, so that
        ///     one piece of arithmetic can serve several protocols and a new protocol can be configured instead of
        ///     written.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class MethodAttribute : Role { }

        /// <summary>
        ///     A method deriving its result from inputs of different phenomenon types — a cost per unit from a cost and
        ///     a count. Its inputs are heterogeneous, which is exactly what distinguishes it from a comparison.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class CausalCalculationAttribute : Role { }

        /// <summary>
        ///     A method taking exactly two inputs of the same phenomenon type, so that its result is a comparison: this
        ///     month against last, actual against planned. The cardinality and the sameness of the two types are the
        ///     assertion.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ComparativeCalculationAttribute : Role { }

        /// <summary>
        ///     A method summing a measurement over the children of one dimension element, so that a figure for a
        ///     country is derived from the figures for its cities. Its single input type is its result type, which is
        ///     what lets it compose up a hierarchy without a case per level.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class DimensionCombinationAttribute : Role { }

        /// <summary>
        ///     The measurement produced under a calculated protocol. It is an ordinary measurement to anything that
        ///     reads it, which is what keeps a derived figure comparable with an observed one.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class CalculatedMeasurementAttribute : Role {

            /// <summary>
            ///     The <see cref="CalculatedMeasurementProtocolAttribute" /> this role is bound to. Optional: it is
            ///     only needed when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? CalculatedMeasurementProtocol { get; init; }

        }

        /// <summary>
        ///     The measurements a calculated measurement was computed from, kept rather than discarded once the
        ///     arithmetic is done. Keeping them is what makes a figure re-explainable instead of a number whose
        ///     provenance is lost.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class SourcesAttribute : Role { }

    }

}
