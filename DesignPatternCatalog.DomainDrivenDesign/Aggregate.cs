#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DomainDrivenDesign {

    /// <summary>
    ///     Aggregate (Domain-Driven Design) — A cluster of entities and value objects treated as one unit for the
    ///     purpose of data changes, with a single root that everything outside the boundary must go through.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class Aggregate {

        /// <summary>
        ///     Role played by a type or a member in the Aggregate design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The single entity by which the aggregate is referenced from outside, and the only participant allowed to
        ///     enforce the invariants that span the boundary.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
        public sealed class RootAttribute : Role { }

        /// <summary>
        ///     A participant living inside the boundary, reachable only through the root and never referenced by
        ///     anything outside it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
        public sealed class MemberAttribute : Role {

            /// <summary>
            ///     The <see cref="RootAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Root { get; init; }

        }

    }

}
