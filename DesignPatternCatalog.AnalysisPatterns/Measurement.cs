#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     Measurement (Analysis Patterns) — Records that a phenomenon of a stated kind had a quantity, so that what
    ///     may be measured is configured rather than written as a field per measurement.
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
    public static class Measurement {

        /// <summary>
        ///     Role played by a type or a member in the Measurement design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     What kind of thing was measured — height, weight, blood glucose level. It is the knowledge level of
        ///     figure 3.6, and the return on it is exactly the difference between figures 3.1 and 3.2: a class with one
        ///     property per measurable thing must be edited to measure a new one, and a clinician who wants to record a
        ///     new observation type should not need a release.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PhenomenonTypeAttribute : Role { }

        /// <summary>
        ///     One recorded quantity of one phenomenon type. Holding the type as a reference rather than as a name is
        ///     what lets a rule range over measurements of a kind, which is what every clinical query does.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class MeasurementAttribute : Role {

            /// <summary>
            ///     The <see cref="PhenomenonTypeAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? PhenomenonType { get; init; }

        }

        /// <summary>
        ///     What or who was measured. It is a party rather than a person in the general case, because an
        ///     organization has measurable properties too and the pattern loses nothing by admitting them.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class SubjectAttribute : Role { }

    }

}
