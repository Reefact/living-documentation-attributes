#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     EventSourcing (Microservices Patterns) — Persists an entity as the sequence of events that changed it rather
    ///     than as its current state, so that recording a change and publishing it are one atomic append.
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
    public static class EventSourcing {

        /// <summary>
        ///     Role played by a type or a member in the EventSourcing design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The entity whose state is never stored, only replayed. Its current state is a function of its events, so
        ///     changing how an old event is applied silently changes what happened.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class AggregateAttribute : Role {

            /// <summary>
            ///     The <see cref="EventAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Event { get; init; }

        }

        /// <summary>
        ///     One state change, appended to the entity's history. It is kept for as long as the entity exists and
        ///     replayed by code years newer than itself, which makes it the hardest thing here to change and the
        ///     easiest to change by accident.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
        public sealed class EventAttribute : Role {

            /// <summary>
            ///     The <see cref="AggregateAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Aggregate { get; init; }

        }

        /// <summary>
        ///     The database of events, which also behaves like a broker: the append is one operation and it delivers to
        ///     every subscriber, which is how this pattern avoids the two-phase commit the problem starts from.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class EventStoreAttribute : Role { }

        /// <summary>
        ///     A saved copy of an entity's state, so that replay starts from it rather than from the beginning. It
        ///     restates what the events already say, and it is wrong from the moment one of them is applied
        ///     differently.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
        public sealed class SnapshotAttribute : Role {

            /// <summary>
            ///     The <see cref="AggregateAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Aggregate { get; init; }

        }

    }

}
