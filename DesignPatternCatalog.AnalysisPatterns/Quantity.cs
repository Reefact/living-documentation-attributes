#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     Quantity (Analysis Patterns) — Makes an amount and its unit one value with arithmetic of its own, so that a
    ///     number whose meaning depends on a unit cannot be used as though it were a bare number.
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
    public static class Quantity {

        /// <summary>
        ///     Role played by a type or a member in the Quantity design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     An amount together with the unit that gives it meaning, carrying the arithmetic Fowler draws on it in
        ///     figure 3.2 — addition, subtraction, scaling, division and comparison. The pattern is the refusal as much
        ///     as the pairing: adding millilitres to milligrams and comparing a weight to a length must fail, and a
        ///     model that keeps the unit in a neighbouring field cannot make either fail.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
        public sealed class QuantityAttribute : Role { }

        /// <summary>
        ///     The number. Marked because it is the member a rule must never find alone: every reported unit error
        ///     begins with something reading this and doing arithmetic on it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
        public sealed class AmountAttribute : Role { }

        /// <summary>
        ///     What the amount is measured in. Its presence is what makes the quantity self-describing, and what lets a
        ///     conversion be asked for rather than assumed.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
        public sealed class UnitAttribute : Role { }

    }

}
