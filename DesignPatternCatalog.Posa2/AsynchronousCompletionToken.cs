#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Posa2 {

    /// <summary>
    ///     AsynchronousCompletionToken (Pattern-Oriented Software Architecture, Volume 2) — Lets an application
    ///     demultiplex the responses of the asynchronous operations it invoked, by handing the service a value it
    ///     returns untouched with the completion.
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
    public static class AsynchronousCompletionToken {

        /// <summary>
        ///     Role played by a type or a member in the AsynchronousCompletionToken design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Identifies the actions and the state the client needs in order to process one completion. Opaque to the
        ///     service and meaningful to the client, which is the whole trick: it travels through code that cannot
        ///     interpret it and must not try.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class AsynchronousCompletionTokenAttribute : Role { }

        /// <summary>
        ///     Invokes the operation, supplies the token with it, and is the only participant that can make sense of
        ///     the token when it comes back. Everything it will need at completion time has to be reachable from that
        ///     token, and what it forgot is not recoverable later.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ClientAttribute : Role {

            /// <summary>
            ///     The <see cref="AsynchronousCompletionTokenAttribute" /> this role is bound to. Optional: it is only
            ///     needed when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AsynchronousCompletionToken { get; init; }

        }

        /// <summary>
        ///     Performs the operation and returns the token unchanged with the completion. It may hold many at once and
        ///     must interpret none of them — reading one is the coupling this pattern exists to avoid, and it is
        ///     invisible from the client's side.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ServiceAttribute : Role {

            /// <summary>
            ///     The <see cref="AsynchronousCompletionTokenAttribute" /> this role is bound to. Optional: it is only
            ///     needed when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AsynchronousCompletionToken { get; init; }

        }

    }

}
