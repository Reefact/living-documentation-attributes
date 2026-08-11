#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Posa2 {

    /// <summary>
    ///     ThreadSpecificStorage (Pattern-Oriented Software Architecture, Volume 2) — Lets several threads use one
    ///     logically global access point to fetch an object that is physically their own, without locking and without
    ///     passing it through every call.
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
    public static class ThreadSpecificStorage {

        /// <summary>
        ///     Role played by a type or a member in the ThreadSpecificStorage design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The one access point every thread uses, which hides the key and the collection behind it and hands each
        ///     caller its own object. It reads as global state and is not, which is the pattern's whole value and the
        ///     reason a reader needs telling.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ThreadSpecificObjectProxyAttribute : Role { }

        /// <summary>
        ///     One thread's own instance of the thread-specific object, reached only through the proxy. Nothing
        ///     serializes access to it because nothing else can see it — so publishing a reference to it anywhere
        ///     another thread can reach removes the only guarantee the pattern makes.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public sealed class ThreadSpecificObjectAttribute : Role {

            /// <summary>
            ///     The <see cref="ThreadSpecificObjectProxyAttribute" /> this role is bound to. Optional: it is only
            ///     needed when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ThreadSpecificObjectProxy { get; init; }

        }

        /// <summary>
        ///     The per-thread set of that thread's objects, which the proxy uses and the caller never sees. On a
        ///     platform whose runtime supplies thread-local storage this is the runtime's, and the role is for the
        ///     codebases that keep their own.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ThreadSpecificObjectSetAttribute : Role {

            /// <summary>
            ///     The <see cref="ThreadSpecificObjectProxyAttribute" /> this role is bound to. Optional: it is only
            ///     needed when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ThreadSpecificObjectProxy { get; init; }

        }

        /// <summary>
        ///     Mints the keys under which objects are filed, one key per kind of thread-specific object rather than one
        ///     per thread. A key handed out twice for two different kinds of object is two callers reading each other's
        ///     storage, and it is the one failure here that no thread boundary protects against.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class KeyFactoryAttribute : Role {

            /// <summary>
            ///     The <see cref="ThreadSpecificObjectProxyAttribute" /> this role is bound to. Optional: it is only
            ///     needed when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ThreadSpecificObjectProxy { get; init; }

        }

    }

}
