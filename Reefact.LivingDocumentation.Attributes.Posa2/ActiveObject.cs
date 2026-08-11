#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Posa2 {

    /// <summary>
    ///     ActiveObject (Pattern-Oriented Software Architecture, Volume 2) — Decouples method invocation from method
    ///     execution, so that a client's call returns at once and the work runs later in the object's own thread.
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
    public static class ActiveObject {

        /// <summary>
        ///     Role played by a type or a member in the ActiveObject design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The interface clients call, in the client's own thread. Every call it takes returns immediately, having
        ///     built a method request and queued it — so a caller who reasons about what has happened by the time the
        ///     call returns is reasoning about nothing.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProxyAttribute : Role { }

        /// <summary>
        ///     Carries the context of one invocation from the proxy to the scheduler: the arguments, the servant to
        ///     apply them to, somewhere to put the result, and the code to run. It also carries the guard that says
        ///     whether it may run yet.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class MethodRequestAttribute : Role {

            /// <summary>
            ///     The <see cref="ProxyAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Proxy { get; init; }

        }

        /// <summary>
        ///     Holds the method requests that are pending. It is bounded, which is the part that matters: it is what
        ///     decouples the calling thread from the executing one, and what a client blocks on once the object is
        ///     further behind than the list is long.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ActivationListAttribute : Role {

            /// <summary>
            ///     The <see cref="ProxyAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Proxy { get; init; }

        }

        /// <summary>
        ///     Runs in a thread of its own, decides which pending request may run next by evaluating its guard, and
        ///     dispatches it on the servant. The order it chooses is the object's concurrency policy, and it is a
        ///     decision rather than a queue discipline.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class SchedulerAttribute : Role {

            /// <summary>
            ///     The <see cref="ProxyAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Proxy { get; init; }

        }

        /// <summary>
        ///     Defines the behaviour and state being modelled as an active object, and holds no synchronization at all
        ///     — that is deliberate, and it is what lets the same servant be scheduled under a different policy without
        ///     being rewritten.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ServantAttribute : Role {

            /// <summary>
            ///     The <see cref="ProxyAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Proxy { get; init; }

        }

        /// <summary>
        ///     Stands for a result that does not exist yet, and is returned the moment the call is made. A client that
        ///     neither waits on it nor polls it has invoked a method whose failure it will never hear about.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class FutureAttribute : Role {

            /// <summary>
            ///     The <see cref="ProxyAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Proxy { get; init; }

        }

    }

}
