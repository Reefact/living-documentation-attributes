#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Posa2 {

    /// <summary>
    ///     AcceptorConnector (Pattern-Oriented Software Architecture, Volume 2) — Decouples connecting and initializing
    ///     cooperating peer services in a networked system from the processing those services perform once connected.
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
    public static class AcceptorConnector {

        /// <summary>
        ///     Role played by a type or a member in the AcceptorConnector design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     Performs the application's work over an established connection, and offers the hook an acceptor or
        ///     connector calls to activate it. Once running it does not talk to either of them again, which is what
        ///     lets the same handler be reached passively or actively.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ServiceHandlerAttribute : Role { }

        /// <summary>
        ///     Passively waits at an address for connection requests, then creates and activates a service handler for
        ///     each. It is a factory, so what it decides is not how a connection is used but which handler exists at
        ///     all.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AcceptorAttribute : Role {

            /// <summary>
            ///     The <see cref="ServiceHandlerAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ServiceHandler { get; init; }

        }

        /// <summary>
        ///     Actively initiates a connection to a remote acceptor and activates the service handler when it is
        ///     established. It may finish synchronously or asynchronously, and the handler is written not to know
        ///     which.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ConnectorAttribute : Role {

            /// <summary>
            ///     The <see cref="ServiceHandlerAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ServiceHandler { get; init; }

        }

        /// <summary>
        ///     The handle a service handler exchanges data through once it is connected, or the passive one an acceptor
        ///     listens on. Which of the two it is decides everything about its lifetime, and nothing in its type says
        ///     which.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public sealed class TransportEndpointAttribute : Role {

            /// <summary>
            ///     The <see cref="ServiceHandlerAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ServiceHandler { get; init; }

        }

    }

}
