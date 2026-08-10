#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     IdempotentConsumer (Microservices Patterns) — Lets a consumer be invoked repeatedly with one message and
    ///     produce the outcome it would have produced once, by recording the messages it has already handled.
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
    public static class IdempotentConsumer {

        /// <summary>
        ///     Role played by a type or a member in the IdempotentConsumer design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The handler for which processing a message once and processing it three times have the same outcome. At-
        ///     least-once delivery makes that obligatory rather than desirable, and nothing but this says which
        ///     handlers have honoured it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class IdempotentConsumerAttribute : Role {

            /// <summary>
            ///     The <see cref="ProcessedMessagesAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ProcessedMessages { get; init; }

        }

        /// <summary>
        ///     Where the identifiers of handled messages are recorded, so that a duplicate is detected and discarded —
        ///     a table of its own, or the identifier carried on the business entity the consumer updates. The primary
        ///     key is what does the work, and no C# type expresses it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProcessedMessagesAttribute : Role { }

    }

}
