#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Posa2 {

    /// <summary>
    ///     ThreadSafeInterface (Pattern-Oriented Software Architecture, Volume 2) — Ensures that intra-component method
    ///     calls avoid self-deadlock and incur no unnecessary locking, by taking the lock only at the border of the
    ///     component.
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
    public static class ThreadSafeInterface {

        /// <summary>
        ///     Role played by a type or a member in the ThreadSafeInterface design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The component whose lock is taken at its border and nowhere within. Its methods divide in two, and that
        ///     division is the whole pattern: the annotation on the type says the division is meant, so a method that
        ///     belongs to neither side is visible as an omission.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class ComponentAttribute : Role { }

        /// <summary>
        ///     A method callers reach from outside: it acquires the lock, forwards to an implementation method, and is
        ///     responsible for releasing the lock when control returns to the caller. It performs the check, not the
        ///     work.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public sealed class InterfaceMethodAttribute : Role { }

        /// <summary>
        ///     A method that performs the work and trusts that it was called with the lock already held: it never
        ///     acquires or releases the lock, and it never calls an interface method. One call back to an interface
        ///     method from here self-deadlocks on a non-recursive lock, and nothing in the type system says so.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public sealed class ImplementationMethodAttribute : Role { }

    }

}
