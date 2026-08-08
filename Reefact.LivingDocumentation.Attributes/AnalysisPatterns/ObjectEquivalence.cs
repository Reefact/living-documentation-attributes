#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     ObjectEquivalence (Analysis Patterns) — Records that two things are believed to be the same, and who
    ///     believes it, so that a disagreement between sources is expressible rather than resolved by whoever loaded
    ///     last.
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
    public static class ObjectEquivalence {

        /// <summary>
        ///     Role played by a type or a member in the ObjectEquivalence design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The claim that two or more things are one. It is an object rather than a merge because the claim may be
        ///     wrong, may be withdrawn, and may be made by one source and denied by another — none of which a merged
        ///     record can express. Figure 5.7 gives it two or more objects, so it is a claim about a set rather than a
        ///     pair.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class EquivalenceAttribute : Role { }

        /// <summary>
        ///     The parties that hold the claim, one at least. This is the part of figure 5.7 that surprises, and the
        ///     part worth annotating: an equivalence with no author is indistinguishable from a fact, so a system that
        ///     drops the asserter cannot tell a curated identification from an automated guess, and cannot report that
        ///     two catalogues disagree.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class AsserterAttribute : Role { }

    }

}
