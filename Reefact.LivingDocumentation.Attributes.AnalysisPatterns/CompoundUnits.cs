#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     CompoundUnits (Analysis Patterns) — Builds a unit out of other units raised to powers, so that a rate, an
    ///     area or a dose per kilogram is a unit the model can reason about rather than a string.
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
    public static class CompoundUnits {

        /// <summary>
        ///     Role played by a type or a member in the CompoundUnits design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     What a quantity is measured in, atomic or compound indifferently. The role sits on the supertype because
        ///     that is the point: a quantity holds a unit without caring which kind it is, and a conversion between
        ///     millilitres per hour and litres per day is asked of the same interface as one between grams and
        ///     kilograms.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class UnitAttribute : Role { }

        /// <summary>
        ///     A unit that decomposes no further — a metre, a gram, a second.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class AtomicUnitAttribute : Role { }

        /// <summary>
        ///     A unit made of unit references. Figure 3.4 states the constraint that keeps it from being a redundant
        ///     wrapper: it must hold more than one reference, or one whose power is negative or above one. A compound
        ///     unit of a single reference to the power of one is a metre spelled twice.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class CompoundUnitAttribute : Role { }

        /// <summary>
        ///     One unit and the power it is raised to. The power is what makes the model reason rather than
        ///     concatenate: metres to the power of two is an area and metres to the power of minus one is a reciprocal,
        ///     and a name like "m2" says neither to anything.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
        public sealed class UnitReferenceAttribute : Role { }

    }

}
