#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Posa2 {

    /// <summary>
    ///     Proactor (Pattern-Oriented Software Architecture, Volume 2) — Lets an event-driven application demultiplex
    ///     and dispatch the completions of asynchronous operations, so concurrency is had without a thread per request.
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
    public static class Proactor {

        /// <summary>
        ///     Role played by a type or a member in the Proactor design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     Demultiplexes completion events and dispatches each to the handler that was registered with the
        ///     operation. It is the reactor's counterpart moved to the other end: it waits for work to have finished
        ///     rather than for work to be startable.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProactorAttribute : Role { }

        /// <summary>
        ///     Started on the application's behalf and performed without borrowing the application's thread. Nothing
        ///     after the call in that method has anything to do with the operation's outcome, which is the shape most
        ///     easily misread as a sequence.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class AsynchronousOperationAttribute : Role {

            /// <summary>
            ///     The <see cref="ProactorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Proactor { get; init; }

        }

        /// <summary>
        ///     Carries out the operations and queues their completions. It is normally the operating system, and
        ///     annotating the boundary is how a reader tells which asynchrony is the platform's and which the
        ///     application built.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class AsynchronousOperationProcessorAttribute : Role {

            /// <summary>
            ///     The <see cref="ProactorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Proactor { get; init; }

        }

        /// <summary>
        ///     Declares the hooks the proactor calls when an operation finishes. What a handler is given is the
        ///     completion, not the context that led to it, which is why this pattern arrives with the next one.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class CompletionHandlerAttribute : Role {

            /// <summary>
            ///     The <see cref="ProactorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Proactor { get; init; }

        }

        /// <summary>
        ///     Whatever starts an asynchronous operation and registers the handler for its completion. It is the
        ///     participant a stack trace will not show, because by the time the handler runs this one has returned.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProactiveInitiatorAttribute : Role {

            /// <summary>
            ///     The <see cref="ProactorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Proactor { get; init; }

        }

    }

}
