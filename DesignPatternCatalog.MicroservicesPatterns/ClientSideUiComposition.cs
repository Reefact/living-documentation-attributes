#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     ClientSideUiComposition (Microservices Patterns) — Builds one screen from UI components rendered by several
    ///     service-specific front ends, composed on the client.
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
    public static class ClientSideUiComposition {

        /// <summary>
        ///     Role played by a type or a member in the ClientSideUiComposition design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The participant rendering one region, owned by the team behind its service. Composed in the browser
        ///     rather than on the server, so a failure degrades one region instead of the response.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class UiComponentAttribute : Role {

            /// <summary>
            ///     The <see cref="PageSkeletonAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? PageSkeleton { get; init; }

        }

        /// <summary>
        ///     The skeleton the components are placed into. It decides layout and nothing else, and it is the
        ///     participant that must keep working when a component does not arrive.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class PageSkeletonAttribute : Role { }

    }

}
