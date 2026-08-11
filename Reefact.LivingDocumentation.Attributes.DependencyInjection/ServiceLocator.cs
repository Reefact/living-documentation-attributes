#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DependencyInjection {

    /// <summary>
    ///     ServiceLocator (Dependency Injection Principles, Practices, and Patterns) — Supplies a dependency by having
    ///     the consumer ask a registry for it at the moment it is needed, rather than being given it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Steven van Deursen, Mark Seemann, <i>Dependency Injection Principles, Practices, and Patterns</i>, 2019.
    ///     </para>
    /// </remarks>
    public static class ServiceLocator {

        /// <summary>
        ///     Role played by a type or a member in the ServiceLocator design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The registry a consumer asks. It is not the participant that carries the cost, and a codebase has one of
        ///     these against many consumers — annotating it says where the boundary of the pattern is, so a rule can
        ///     range over everything that touches it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ServiceLocatorAttribute : Role { }

        /// <summary>
        ///     A class that resolves what it needs instead of receiving it, and therefore does not state its
        ///     preconditions in its contract. Nothing about its constructor says what must be registered for it to
        ///     work, so a missing registration is a failure at run time and adding a dependency to it is a breaking
        ///     change that breaks no build.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class ConsumerAttribute : Role {

            /// <summary>
            ///     The <see cref="ServiceLocatorAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ServiceLocator { get; init; }

        }

    }

}
