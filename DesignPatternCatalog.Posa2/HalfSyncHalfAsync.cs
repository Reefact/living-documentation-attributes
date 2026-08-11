#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Posa2 {

    /// <summary>
    ///     HalfSyncHalfAsync (Pattern-Oriented Software Architecture, Volume 2) — Decomposes concurrent processing into
    ///     a synchronous layer and an asynchronous one, with a queueing layer between them, so simplicity and
    ///     responsiveness are each paid for where they are worth it.
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
    public static class HalfSyncHalfAsync {

        /// <summary>
        ///     Role played by a type or a member in the HalfSyncHalfAsync design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     Where the high-level work happens, in threads of its own that may block: it has a stack and registers,
        ///     so it can be written as a sequence of steps and read as one. Everything easy to follow in the system
        ///     lives here, and it is here that the thread count is paid for.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class SynchronousTaskLayerAttribute : Role {

            /// <summary>
            ///     The <see cref="QueueingLayerAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? QueueingLayer { get; init; }

        }

        /// <summary>
        ///     Where the low-level work happens, driven by events from outside and never blocking, because it has no
        ///     stack of its own to block on. A blocking call added here does not slow the layer down — it stops the
        ///     system taking events at all.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class AsynchronousTaskLayerAttribute : Role {

            /// <summary>
            ///     The <see cref="QueueingLayerAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? QueueingLayer { get; init; }

        }

        /// <summary>
        ///     The buffering and notification point between the two layers, in both directions. It is the only place
        ///     they meet, which is what makes the decomposition real rather than a diagram: a direct call across it
        ///     puts one layer's blocking into the other's thread.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class QueueingLayerAttribute : Role { }

    }

}
