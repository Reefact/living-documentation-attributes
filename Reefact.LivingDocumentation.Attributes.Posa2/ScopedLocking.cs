#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Posa2 {

    /// <summary>
    ///     ScopedLocking (Pattern-Oriented Software Architecture, Volume 2) — Ensures that a lock is acquired when
    ///     control enters a scope and released automatically when control leaves it, by whatever path.
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
    public static class ScopedLocking {

        /// <summary>
        ///     Role played by a type or a member in the ScopedLocking design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The class whose construction acquires the lock and whose disposal releases it. Every way out of the
        ///     scope releases it — a return, a thrown exception, a branch added next year by somebody who never read
        ///     this class — which is the one thing an explicit release on each path cannot promise.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class GuardAttribute : Role { }

        /// <summary>
        ///     The lock the guard manages. Annotating it claims that this lock is taken through the guard and never
        ///     directly, so a bare acquire anywhere else in the type is the breach rather than a matter of style.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public sealed class LockAttribute : Role { }

    }

}
