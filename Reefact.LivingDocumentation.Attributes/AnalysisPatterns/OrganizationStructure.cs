#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     OrganizationStructure (Analysis Patterns) — Reifies the relationship between two organizations as an object
    ///     carrying its own type, so that a business with several overlapping structures keeps one model instead of one
    ///     hierarchy per structure.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         A narrower case of Accountability, in Analysis Patterns: every participant annotated here is one of
    ///         those too, and a consumer asking for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Analysis Patterns</i>, 1997.
    ///     </para>
    /// </remarks>
    public static class OrganizationStructure {

        /// <summary>
        ///     Role played by a type or a member in the OrganizationStructure design pattern.
        /// </summary>
        public abstract class Role : AnalysisPatterns.Accountability.Role { }

        /// <summary>
        ///     What kind of structure this is — a sales hierarchy, a legal ownership chain, a reporting line — and
        ///     which organizations may stand at each end of it. A new structure is configured here rather than written,
        ///     which is what stops the model growing a class per hierarchy. It is also where the admissibility rule
        ///     lives, so the question "may this organization sit under that one" is asked in one place.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class OrganizationStructureTypeAttribute : Role { }

        /// <summary>
        ///     One relationship between two organizations, of one kind, for a period. The dates are why it is an
        ///     object: a reorganisation does not erase the structure that preceded it, and a report about last quarter
        ///     is asked against the structure that was in force then.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class OrganizationStructureAttribute : Role {

            /// <summary>
            ///     The <see cref="OrganizationStructureTypeAttribute" /> this role is bound to. Optional: it is only
            ///     needed when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? OrganizationStructureType { get; init; }

        }

        /// <summary>
        ///     The organization above. Naming which end is which is the assertion, exactly as for an accountability:
        ///     both ends are organizations, nothing in the type system tells them apart, and a model with them the
        ///     wrong way round inverts every chart drawn from it while compiling and passing its tests.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ParentAttribute : Role { }

        /// <summary>
        ///     The organization below. It is a whole organization rather than a division type, because the pattern's
        ///     reach depends on the same organization being able to sit under different parents in different
        ///     structures.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class SubsidiaryAttribute : Role { }

    }

}
