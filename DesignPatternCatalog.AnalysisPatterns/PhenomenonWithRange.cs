#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     PhenomenonWithRange (Analysis Patterns) — Defines a category by the range of quantities it covers, so that a
    ///     measured amount is classified by the model rather than by a chain of comparisons written at each call site.
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
    public static class PhenomenonWithRange {

        /// <summary>
        ///     Role played by a type or a member in the PhenomenonWithRange design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     A phenomenon — a category such as an age band or a fever — carrying the range of the quantitative
        ///     phenomenon type it stands for. The ranges of the phenomena of one type should neither overlap nor leave
        ///     a gap, which is the pair of constraints that makes the classification total and unambiguous.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class RangedPhenomenonAttribute : Role { }

        /// <summary>
        ///     The function mapping a quantity to the phenomenon whose range contains it. It is an object rather than a
        ///     lookup repeated at each call site, so the classification has one definition; and because it derives one
        ///     observation from another, what it yields keeps the measurement it was derived from.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class RangeFunctionAttribute : Role { }

        /// <summary>
        ///     The range a ranged phenomenon covers, on the phenomenon rather than in the function, so that adding a
        ///     band is configuration and the function is unchanged.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class RangeAttribute : Role { }

    }

}
