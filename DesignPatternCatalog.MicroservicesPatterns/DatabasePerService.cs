#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     DatabasePerService (Microservices Patterns) — Keeps each service's persistent data private to it and
    ///     reachable only through its API, so that a change to one service's schema is a change to nothing else.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    public static class DatabasePerService {

        /// <summary>
        ///     Role played by a type or a member in the DatabasePerService design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The service whose data is its own: its transactions involve its database and no other, and anything
        ///     outside it that wants the data asks for it rather than reading it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class ServiceAttribute : Role { }

        /// <summary>
        ///     The tables, schema or server one service owns. Nothing in the code enforces the privacy — a connection
        ///     string is a connection string — so the barrier is a grant, a reviewer, or this annotation.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PrivateDatabaseAttribute : Role {

            /// <summary>
            ///     The <see cref="ServiceAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Service { get; init; }

        }

    }

}
