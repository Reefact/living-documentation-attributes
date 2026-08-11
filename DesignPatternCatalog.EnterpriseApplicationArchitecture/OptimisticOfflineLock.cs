#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     OptimisticOfflineLock (Patterns of Enterprise Application Architecture) — A conflict between two long
    ///     transactions is detected at commit, by checking that nothing changed underneath.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    public static class OptimisticOfflineLock {

        /// <summary>
        ///     Role played by a type or a member in the OptimisticOfflineLock design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     A record whose update is refused if it changed since it was read. It suits a system where conflicts are
        ///     rare: nobody waits, and the cost is paid only by the loser — who must be told something useful, because
        ///     a conflict discovered at save is work already done.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class OptimisticOfflineLockAttribute : Role { }

        /// <summary>
        ///     The member the check is made against — a version, a timestamp, a hash. Naming it matters because
        ///     everything rests on it: an update that forgets to include it in its WHERE clause silently turns the
        ///     whole pattern off, and nothing fails.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class VersionFieldAttribute : Role { }

    }

}
