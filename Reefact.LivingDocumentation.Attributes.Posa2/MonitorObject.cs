#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Posa2 {

    /// <summary>
    ///     MonitorObject (Pattern-Oriented Software Architecture, Volume 2) — Serializes concurrent method calls on an
    ///     object so that only one runs at a time, and lets its methods cooperatively schedule their turns.
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
    public static class MonitorObject {

        /// <summary>
        ///     Role played by a type or a member in the MonitorObject design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The object whose methods are serialized. It has no thread of its own: every method runs in the thread of
        ///     the client that called it, which is the whole difference from an active object and the reason a long
        ///     method here blocks a caller rather than a worker.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class MonitorObjectAttribute : Role { }

        /// <summary>
        ///     One of the thread-safe services the monitor exports. Exactly one of them runs inside the monitor at a
        ///     time, whatever the number of threads calling and whatever the number of such methods — so the object's
        ///     throughput is one method, not one per caller.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public sealed class SynchronizedMethodAttribute : Role { }

        /// <summary>
        ///     The lock this monitor object holds of its own, taken as a synchronized method enters and released as it
        ///     leaves. One monitor object, one lock: sharing a lock between two of them serializes both and neither of
        ///     them says so.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public sealed class MonitorLockAttribute : Role { }

        /// <summary>
        ///     What a synchronized method waits on when it cannot make progress, which releases the monitor lock and
        ///     suspends the caller until another method notifies it. A condition nothing ever notifies is a caller that
        ///     never returns, and no compiler pairs the two.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
        public sealed class MonitorConditionAttribute : Role { }

    }

}
