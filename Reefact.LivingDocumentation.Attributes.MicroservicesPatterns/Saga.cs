#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     Saga (Microservices Patterns) — Maintains consistency across services without a distributed transaction, by
    ///     running a sequence of local transactions and undoing the earlier ones with compensating transactions when a
    ///     later one fails.
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
    public static class Saga {

        /// <summary>
        ///     Role played by a type or a member in the Saga design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The whole sequence, standing in for a transaction that cannot span services. It has no rollback of its
        ///     own: everything it undoes is undone by code somebody wrote for the purpose.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class SagaAttribute : Role { }

        /// <summary>
        ///     Tells the participants which local transaction to run next and decides the outcome. Its alternative is
        ///     choreography, where no such class exists and the order is spread across the event handlers of every
        ///     service involved.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class OrchestratorAttribute : Role {

            /// <summary>
            ///     The <see cref="SagaAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Saga { get; init; }

        }

        /// <summary>
        ///     A service whose local transaction is one step. It commits before the saga is over, which is the
        ///     isolation the pattern gives up and the reason a half-finished saga is visible to everybody.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class ParticipantAttribute : Role {

            /// <summary>
            ///     The <see cref="SagaAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Saga { get; init; }

        }

        /// <summary>
        ///     One step: it updates its own service's database and triggers the next. Its effects are visible the
        ///     moment it commits, whether or not the steps after it will succeed.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class LocalTransactionAttribute : Role {

            /// <summary>
            ///     The <see cref="SagaAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Saga { get; init; }

        }

        /// <summary>
        ///     Undoes what a local transaction did, semantically rather than by rollback — a refund rather than an
        ///     unpayment. A step without one is a step the saga cannot back out of, and counting the two annotations on
        ///     a participant is the only way to notice.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class CompensatingTransactionAttribute : Role {

            /// <summary>
            ///     The <see cref="SagaAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Saga { get; init; }

        }

    }

}
