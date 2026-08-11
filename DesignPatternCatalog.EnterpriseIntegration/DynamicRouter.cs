#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     DynamicRouter (Enterprise Integration Patterns) — Lets the destinations tell the router how to reach them,
    ///     so that adding one is a message rather than a change to the router.
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
    public static class DynamicRouter {

        /// <summary>
        ///     Role played by a type or a member in the DynamicRouter design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     A router whose rule is data it maintains rather than code it contains. It keeps the efficiency of a
        ///     single hop while losing the dependency on knowing every destination in advance.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class DynamicRouterAttribute : Role {

            /// <summary>
            ///     The <see cref="RoutingTableAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? RoutingTable { get; init; }

        }

        /// <summary>
        ///     The channel a destination announces itself on. It is what makes the router dynamic: the knowledge
        ///     arrives as a message, so a new destination costs the router no edit.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ControlChannelAttribute : Role { }

        /// <summary>
        ///     What the router learned from the control channel. Held as state rather than configuration, which is what
        ///     makes it answerable at run time and what makes it something to rebuild after a restart.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class RoutingTableAttribute : Role { }

    }

}
