#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseIntegration {

    /// <summary>
    ///     RecipientList (Enterprise Integration Patterns) — Sends a message to a set of destinations the sender
    ///     computes, so that who receives it is decided per message rather than by a subscription.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class RecipientList {

        /// <summary>
        ///     Role played by a type or a member in the RecipientList design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The participant that computes the recipients of one message and sends a copy to each. Unlike a publish-
        ///     subscribe channel, the decision is the sender's and per message, which is what lets it depend on the
        ///     message's content.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class RecipientListAttribute : Role {

            /// <summary>
            ///     The <see cref="RecipientsAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Recipients { get; init; }

        }

        /// <summary>
        ///     The destinations computed for this message. Exposing them is what makes the routing decision auditable
        ///     rather than a side effect nobody can inspect after the fact.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class RecipientsAttribute : Role { }

    }

}
