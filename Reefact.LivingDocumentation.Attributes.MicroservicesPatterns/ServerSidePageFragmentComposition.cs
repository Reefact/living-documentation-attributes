#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     ServerSidePageFragmentComposition (Microservices Patterns) — Builds one page from HTML fragments generated
    ///     by several service-specific web applications, aggregated on the server.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    public static class ServerSidePageFragmentComposition {

        /// <summary>
        ///     Role played by a type or a member in the ServerSidePageFragmentComposition design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The participant generating one region of the page, owned by the team that owns the service behind it.
        ///     Its release cadence is its own, which is the point — and the page it lands in is somebody else's.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class FragmentAttribute : Role {

            /// <summary>
            ///     The <see cref="PageTemplateAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? PageTemplate { get; init; }

        }

        /// <summary>
        ///     The template that assembles the fragments into a page. It is where a broken or slow fragment becomes a
        ///     broken or slow page, and it is owned by a team that owns none of them.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class PageTemplateAttribute : Role { }

    }

}
