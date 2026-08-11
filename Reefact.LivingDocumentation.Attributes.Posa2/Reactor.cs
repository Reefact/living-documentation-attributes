#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Posa2 {

    /// <summary>
    ///     Reactor (Pattern-Oriented Software Architecture, Volume 2) — Lets an event-driven application demultiplex
    ///     and dispatch service requests that arrive from one or more clients on a single thread.
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
    public static class Reactor {

        /// <summary>
        ///     Role played by a type or a member in the Reactor design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Registers and removes event handlers and dispatches to them when their sources have events. It runs the
        ///     loop that everything else waits inside, so anything slow reached from here delays every other source it
        ///     is watching.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ReactorAttribute : Role { }

        /// <summary>
        ///     Blocks until an operation can be started on one of the handles without blocking, and returns which. It
        ///     is the only place the application is allowed to wait, and every other wait in a reactive design is a
        ///     defect.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class SynchronousEventDemultiplexerAttribute : Role {

            /// <summary>
            ///     The <see cref="ReactorAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Reactor { get; init; }

        }

        /// <summary>
        ///     Identifies one source of events the reactor watches — a connection, a file, a timer. It is what the
        ///     demultiplexer waits on, and a handle left registered after its source is finished is an event loop that
        ///     wakes for nothing.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public sealed class HandleAttribute : Role {

            /// <summary>
            ///     The <see cref="ReactorAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Reactor { get; init; }

        }

        /// <summary>
        ///     Declares the hook the reactor calls when a source has an event. It says nothing about a thread, because
        ///     there is only one, which is the assumption everything implementing it inherits.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class EventHandlerAttribute : Role {

            /// <summary>
            ///     The <see cref="ReactorAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Reactor { get; init; }

        }

        /// <summary>
        ///     Implements the hook for one kind of service. It runs on the reactor's thread: the time it takes is time
        ///     the reactor is not dispatching, so a blocking call here stops the whole application rather than one
        ///     request.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ConcreteEventHandlerAttribute : Role {

            /// <summary>
            ///     The <see cref="EventHandlerAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? EventHandler { get; init; }

        }

    }

}
