#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     IdentificationScheme (Analysis Patterns) — Makes an identifier belong to a scheme, so that a thing known by
    ///     several numbers has no privileged one and each is unique only where it was issued.
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
    public static class IdentificationScheme {

        /// <summary>
        ///     Role played by a type or a member in the IdentificationScheme design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     Who issues identifiers, and within what they are unique. It is the object figure 5.3 lacks and figure
        ///     5.4 adds, and the difference between them is the whole pattern: a single id field asserts one issuer and
        ///     global uniqueness, and the moment a second issuer exists that assertion is false without anything
        ///     failing.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class IdentificationSchemeAttribute : Role { }

        /// <summary>
        ///     One identifier of one thing, within one scheme. Uniqueness is scoped to the scheme rather than to the
        ///     type, which is the assertion a rule can check and the one everybody gets wrong first: two schemes may
        ///     perfectly well issue the same string, so a lookup that ignores the scheme finds the wrong thing rather
        ///     than nothing.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class IdentifierAttribute : Role {

            /// <summary>
            ///     The <see cref="IdentificationSchemeAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? IdentificationScheme { get; init; }

        }

    }

}
