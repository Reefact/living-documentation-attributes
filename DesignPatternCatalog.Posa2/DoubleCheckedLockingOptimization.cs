#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Posa2 {

    /// <summary>
    ///     DoubleCheckedLockingOptimization (Pattern-Oriented Software Architecture, Volume 2) — Reduces contention
    ///     where a critical section must run exactly once, by testing a flag before taking the lock and testing it
    ///     again after taking it.
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
    public static class DoubleCheckedLockingOptimization {

        /// <summary>
        ///     Role played by a type or a member in the DoubleCheckedLockingOptimization design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The code that must execute exactly once, an initialisation typically. It is reached rarely compared with
        ///     the accesses that find the work already done, which is the entire reason for paying for two tests
        ///     instead of one lock.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class JustOnceCriticalSectionAttribute : Role { }

        /// <summary>
        ///     The lock that serializes the threads which find the flag unset. It is contended on the first pass and
        ///     untouched afterwards, so a measurement taken once the flag is set will show this lock costing nothing
        ///     and prove nothing.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
        public sealed class MutexAttribute : Role { }

        /// <summary>
        ///     Says whether the critical section has already run, and is read twice: once before the lock as the
        ///     optimisation, and once inside it as the correctness. Unless it is written and read atomically, the
        ///     thread that skips the lock can reach a value that is published but not yet built — which is why this
        ///     pattern is famous for being wrong in a way testing does not find.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public sealed class FlagAttribute : Role { }

    }

}
