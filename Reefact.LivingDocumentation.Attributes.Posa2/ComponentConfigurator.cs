#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Posa2 {

    /// <summary>
    ///     ComponentConfigurator (Pattern-Oriented Software Architecture, Volume 2) — Lets an application link and
    ///     unlink its component implementations at run time, without modifying, recompiling or relinking the
    ///     application.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Douglas Schmidt, Michael Stal, Hans Rohnert, Frank Buschmann, <i>Pattern-Oriented Software Architecture,
    ///         Volume 2</i>, 2000.
    ///     </para>
    /// </remarks>
    public static class ComponentConfigurator {

        /// <summary>
        ///     Role played by a type or a member in the ComponentConfigurator design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the hooks by which a component is initialized, suspended, resumed and terminated. Everything
        ///     the configurator can do to a component is on this interface, so a component that needs anything else is
        ///     one the configurator cannot manage.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ComponentAttribute : Role { }

        /// <summary>
        ///     Implements the hooks and the service itself. Its initialization runs at a moment the application chooses
        ///     rather than at start-up, so anything it assumes about what else exists yet is an assumption about
        ///     configuration order.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ConcreteComponentAttribute : Role {

            /// <summary>
            ///     The <see cref="ComponentAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Component { get; init; }

        }

        /// <summary>
        ///     Holds the components currently configured into the application, and is what makes them addressable at
        ///     run time. A component missing from here is not merely unreachable — it cannot be suspended, resumed or
        ///     terminated either.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ComponentRepositoryAttribute : Role {

            /// <summary>
            ///     The <see cref="ComponentAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Component { get; init; }

        }

        /// <summary>
        ///     Links and unlinks implementations while the application runs, and records them in the repository. It is
        ///     the one participant with the power to change what the application is made of after it has started.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ComponentConfiguratorAttribute : Role {

            /// <summary>
            ///     The <see cref="ComponentAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Component { get; init; }

        }

    }

}
