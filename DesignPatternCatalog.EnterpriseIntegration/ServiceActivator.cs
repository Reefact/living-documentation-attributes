#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     ServiceActivator (Enterprise Integration Patterns) — Connects a channel to a service the application already
    ///     has, so that the same service can be reached by a message and by an ordinary call without knowing which
    ///     happened.
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
    public static class ServiceActivator {

        /// <summary>
        ///     Role played by a type or a member in the ServiceActivator design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The participant that consumes the message and invokes the service like any other client. It handles
        ///     every messaging detail, and may be one-way or half of a request-reply, so that whether the caller
        ///     arrived over a channel is a fact the service never has to learn.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ServiceActivatorAttribute : Role {

            /// <summary>
            ///     The <see cref="ServiceAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Service { get; init; }

        }

        /// <summary>
        ///     What is being made available. Naming it is what makes the pattern's claim checkable: a service that has
        ///     acquired a message type in its signature, or a reply channel in its parameters, is no longer invocable
        ///     by anything but the activator.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ServiceAttribute : Role { }

    }

}
