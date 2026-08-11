#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     CorrelationIdentifier (Enterprise Integration Patterns) — Makes a reply name the request it answers, so that
    ///     a requestor sending many can tell which answer is which.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class CorrelationIdentifier {

        /// <summary>
        ///     Role played by a type or a member in the CorrelationIdentifier design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The property identifying a request uniquely. It is what a reply will quote, so it must be unique for as
        ///     long as an answer might arrive — which is longer than the request takes.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class IdentifierAttribute : Role { }

        /// <summary>
        ///     The property on the reply that quotes the request's identifier. This is the assertion the pattern exists
        ///     for: an answer that does not carry it cannot be matched to anything, and a requestor holding several
        ///     open requests has no way to guess.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class CorrelationAttribute : Role {

            /// <summary>
            ///     The <see cref="IdentifierAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Identifier { get; init; }

        }

    }

}
