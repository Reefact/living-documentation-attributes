#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     CommandSideReplica (Microservices Patterns) — Lets a service implementing a command read another service's
    ///     data from a local read-only replica it keeps current from that service's events, rather than querying it.
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
    public static class CommandSideReplica {

        /// <summary>
        ///     Role played by a type or a member in the CommandSideReplica design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The service implementing the command. It reads the replica instead of calling the provider, which is
        ///     what removes the runtime coupling and what makes the data it decides on potentially stale.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class CommandServiceAttribute : Role {

            /// <summary>
            ///     The <see cref="ReplicaDatabaseAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ReplicaDatabase { get; init; }

        }

        /// <summary>
        ///     The service that owns the data, and that must publish an event whenever it changes — a duty it acquires
        ///     for the benefit of a service it does not know is there.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class ProviderServiceAttribute : Role { }

        /// <summary>
        ///     The read-only copy the command service keeps up to date by subscribing to the provider's events. It has
        ///     exactly one writer, and nothing reading it may assume it is current.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ReplicaDatabaseAttribute : Role {

            /// <summary>
            ///     The <see cref="ProviderServiceAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ProviderService { get; init; }

        }

    }

}
