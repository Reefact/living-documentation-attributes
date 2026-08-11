#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Posa2 {

    /// <summary>
    ///     LeaderFollowers (Pattern-Oriented Software Architecture, Volume 2) — Lets a pool of threads take turns
    ///     waiting on a shared set of event sources, so that the thread which receives an event is the thread that
    ///     processes it, with no handoff and no dedicated dispatcher.
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
    public static class LeaderFollowers {

        /// <summary>
        ///     Role played by a type or a member in the LeaderFollowers design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The group of threads that take turns, and the synchronizer they queue on. Its protocol is the pattern:
        ///     the leader promotes a follower before it starts processing, and a leader that processes first leaves the
        ///     event sources unwatched for exactly as long as the work takes.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ThreadPoolAttribute : Role { }

        /// <summary>
        ///     The collection of event sources the leader waits on, which returns when an operation on one of them can
        ///     be started without blocking. What is in the set is what the pool can serve, and a source added to it
        ///     without a handler is an event that wakes a thread and goes nowhere.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class HandleSetAttribute : Role {

            /// <summary>
            ///     The <see cref="ThreadPoolAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ThreadPool { get; init; }

        }

        /// <summary>
        ///     Declares the hook methods that process the events arriving on a source. It says nothing about which
        ///     thread will call them, and that is the point: any thread in the pool may, so the implementation may hold
        ///     no state belonging to one of them.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class EventHandlerAttribute : Role {

            /// <summary>
            ///     The <see cref="ThreadPoolAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ThreadPool { get; init; }

        }

        /// <summary>
        ///     One application service, processing the events of its source in its own way, and running in whichever
        ///     thread has just been demoted from leader. Its hook method is the work that keeps that thread out of the
        ///     pool while it runs.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class ConcreteEventHandlerAttribute : Role {

            /// <summary>
            ///     The <see cref="EventHandlerAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? EventHandler { get; init; }

        }

    }

}
