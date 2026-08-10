#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     TransactionalOutbox (Microservices Patterns) — Makes updating the data and sending the message one local
    ///     transaction, by writing the message to a table in the same database and letting a separate process forward
    ///     it to the broker.
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
    public static class TransactionalOutbox {

        /// <summary>
        ///     Role played by a type or a member in the TransactionalOutbox design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The service that changes its data and sends the message. It writes both in one local transaction and
        ///     never touches the broker, which is what makes the send reliable — and what makes forgetting to write the
        ///     row a failure with nothing to see.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class SenderAttribute : Role {

            /// <summary>
            ///     The <see cref="MessageOutboxAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? MessageOutbox { get; init; }

        }

        /// <summary>
        ///     The one database holding both the business entities and the outbox. That they are the same database is
        ///     the entire mechanism: two would need the distributed transaction this pattern exists to avoid.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class DatabaseAttribute : Role { }

        /// <summary>
        ///     The table — or, in a document store, the property on each record — where a message waits. Written by the
        ///     business transaction, read by nothing but the relay, and empty in a healthy system, which is why nobody
        ///     notices when it stops being drained.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class MessageOutboxAttribute : Role {

            /// <summary>
            ///     The <see cref="DatabaseAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Database { get; init; }

        }

        /// <summary>
        ///     Moves what the outbox holds to the broker once the transaction has committed. It can publish the same
        ///     message twice — it may crash between publishing and recording that it did — so every consumer downstream
        ///     of it has to be idempotent whether its author knew that or not.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class MessageRelayAttribute : Role {

            /// <summary>
            ///     The <see cref="MessageOutboxAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? MessageOutbox { get; init; }

        }

    }

}
