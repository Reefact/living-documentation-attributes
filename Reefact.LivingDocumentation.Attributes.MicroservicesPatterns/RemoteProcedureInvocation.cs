#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     RemoteProcedureInvocation (Microservices Patterns) — Has services collaborate by request and reply over a
    ///     network protocol, so that a caller gets its answer at once and is unavailable for as long as the service it
    ///     called is.
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
    public static class RemoteProcedureInvocation {

        /// <summary>
        ///     Role played by a type or a member in the RemoteProcedureInvocation design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The participant that exposes a request/reply API to other services. Its availability becomes theirs for
        ///     the duration of every call, and its signature says nothing about that.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class ServiceAttribute : Role { }

        /// <summary>
        ///     The participant that calls it. It cannot answer while the service cannot, and it has to find the service
        ///     before it can call it at all — which is why this is the role a circuit breaker and a discovery mechanism
        ///     attach to.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class ClientAttribute : Role {

            /// <summary>
            ///     The <see cref="ServiceAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Service { get; init; }

        }

    }

}
