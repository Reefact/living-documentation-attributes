#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     ApiComposition (Microservices Patterns) — Answers a query that spans services by invoking each service
    ///     owning part of the answer and joining the results in memory.
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
    public static class ApiComposition {

        /// <summary>
        ///     Role played by a type or a member in the ApiComposition design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Invokes the providers and joins what they return. The join that a database would have done on an index
        ///     is done here on a list, so the cost of the query is this participant's cost and its inefficiency is this
        ///     participant's problem.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
        public sealed class ComposerAttribute : Role { }

        /// <summary>
        ///     A service owning part of the answer, invoked for its share of it. It is queried on the read path of an
        ///     operation it knows nothing about, so its latency is the composer's latency.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class ProviderAttribute : Role {

            /// <summary>
            ///     The <see cref="ComposerAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Composer { get; init; }

        }

    }

}
