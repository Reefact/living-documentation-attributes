#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     ControlBus (Enterprise Integration Patterns) — Carries the management of a messaging system over the
    ///     messaging system itself, on channels kept apart from the ones the application's data travels on.
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
    public static class ControlBus {

        /// <summary>
        ///     Role played by a type or a member in the ControlBus design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The second messaging subsystem, the one that administers the first. It uses the same mechanism and
        ///     separate channels, which is what lets a system spread over several continents be configured and watched
        ///     without a second technology — and what makes management traffic that has crept onto an application
        ///     channel a defect somebody can name.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ControlBusAttribute : Role { }

        /// <summary>
        ///     A component connected to both flows: the application's and the control bus's. Annotating it is what says
        ///     which parts of a system can be told to reconfigure, asked how they are, or heard from at all — an
        ///     absence here is a component nobody can administer, which is the state most of them start in.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ManagedComponentAttribute : Role {

            /// <summary>
            ///     The <see cref="ControlBusAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ControlBus { get; init; }

        }

    }

}
