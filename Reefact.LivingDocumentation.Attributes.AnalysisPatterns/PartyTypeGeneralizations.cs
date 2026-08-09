#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     PartyTypeGeneralizations (Analysis Patterns) — Lets the kinds of party generalise one another at the
    ///     knowledge level, so that a rule stated for a broad kind reaches every narrower kind without being restated
    ///     for each.
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
    public static class PartyTypeGeneralizations {

        /// <summary>
        ///     Role played by a type or a member in the PartyTypeGeneralizations design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The kind of party, and itself an object that generalises. Putting the generalization here rather than in
        ///     the class hierarchy is the whole point: a business that invents a narrower kind of supplier on a Tuesday
        ///     configures one, where a subclass would have been a release.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PartyTypeAttribute : Role { }

        /// <summary>
        ///     The broader kind this one falls under, at most one, and never a kind that already falls under this one.
        ///     The acyclicity is the assertion worth checking: nothing in the type system prevents it, a cycle is
        ///     written as one ordinary assignment, and what it breaks is the closure below — which stops terminating
        ///     rather than returning a wrong answer.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class SupertypeAttribute : Role { }

        /// <summary>
        ///     The derived closure: this kind and every kind above it. It is the member a rule about parties must range
        ///     over, and mistaking it for the immediate kind is the failure the pattern exists to prevent — the check
        ///     still passes for parties registered under the exact kind, so it looks correct until the day a narrower
        ///     kind is configured and a party that should qualify silently does not.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class AllTypesAttribute : Role { }

    }

}
