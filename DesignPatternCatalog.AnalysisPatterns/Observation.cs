#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     Observation (Analysis Patterns) — Generalises a measurement to anything that can be observed, so that a fact
    ///     recorded as a category and a fact recorded as a quantity are one kind of thing.
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
    public static class Observation {

        /// <summary>
        ///     Role played by a type or a member in the Observation design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Anything recorded about a subject at a time. Figures 3.7 and 3.9 put a measurement and a category
        ///     observation under it, and the reason to name the supertype is that everything true of observations in
        ///     general attaches here: who recorded it, when, whether it was rejected, and what evidence it rests on. A
        ///     model that generalises only quantities has to say all of that twice.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class ObservationAttribute : Role { }

        /// <summary>
        ///     An observation whose value is a category rather than a quantity — blood group A, non-smoker, female. It
        ///     is not a measurement with a missing number: nothing about it can be added, scaled or averaged, and
        ///     treating it as a quantity is how a model ends up ranking blood groups.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class CategoryObservationAttribute : Role { }

        /// <summary>
        ///     One admissible value of a phenomenon type, at the knowledge level. Figure 3.9 hangs it under the type,
        ///     which is what makes a category observation checkable at all: a value outside the phenomena its type
        ///     admits is refusable rather than merely unusual.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class PhenomenonAttribute : Role { }

    }

}
