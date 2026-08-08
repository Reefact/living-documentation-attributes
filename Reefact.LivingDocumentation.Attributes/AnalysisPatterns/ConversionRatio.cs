#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     ConversionRatio (Analysis Patterns) — Reifies the factor between two units as an object, so that converting
    ///     a quantity is looking something up rather than knowing it.
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
    public static class ConversionRatio {

        /// <summary>
        ///     Role played by a type or a member in the ConversionRatio design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The factor from one unit to another, held as data. Figure 3.3 gives it two distinct ends and a number,
        ///     which is the whole of it — and the reason it is worth having is that conversions written as code are
        ///     written once per place that needs one, while a ratio held as an object is stated once and composed.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class ConversionRatioAttribute : Role { }

        /// <summary>
        ///     The unit converted out of. Naming which end is which is the assertion: both ends are units, nothing
        ///     distinguishes them, and an inverted ratio is a factor applied upside down — which produces a plausible
        ///     number rather than an error.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class FromAttribute : Role { }

        /// <summary>
        ///     The unit converted into, and never the same as the one converted out of.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ToAttribute : Role { }

    }

}
