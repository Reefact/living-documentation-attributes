#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     AssociatedObservation (Analysis Patterns) — Derives an observation from others by a stated function while
    ///     keeping what it was derived from, so that a conclusion can be re-explained rather than merely trusted.
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
    public static class AssociatedObservation {

        /// <summary>
        ///     Role played by a type or a member in the AssociatedObservation design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The rule that produces one observation concept from others, held at the knowledge level. Figure 3.14
        ///     gives it arguments and a product, both observation concepts, which is what makes it configurable: a body
        ///     mass index is a function of a height and a weight, and stating that as data rather than as a method
        ///     means a new derived measure is added without a release.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AssociativeFunctionAttribute : Role { }

        /// <summary>
        ///     An observation produced by such a function rather than taken directly. Naming it is what keeps a derived
        ///     value from being mistaken for a measured one — the difference matters the moment an argument is
        ///     corrected, because the derivation has to be redone and a directly measured value must not be.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AssociatedObservationAttribute : Role {

            /// <summary>
            ///     The <see cref="AssociativeFunctionAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AssociativeFunction { get; init; }

        }

        /// <summary>
        ///     The observations it was derived from, retained rather than consumed. Keeping them is the pattern's
        ///     second claim and the harder one to hold: it is always tempting to store only the result, and a stored
        ///     result whose inputs are gone cannot be rechecked when one of them turns out to have been rejected.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class EvidenceAttribute : Role { }

    }

}
