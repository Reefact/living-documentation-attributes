#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     RequestReply (Enterprise Integration Patterns) — Pairs a request with a reply over two channels, so that a
    ///     message can get an answer without either side blocking on the other's availability.
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
    public static class RequestReply {

        /// <summary>
        ///     Role played by a type or a member in the RequestReply design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The message that asks. It travels one channel and names, or carries, the channel the answer should come
        ///     back on — which is what makes the exchange two one-way messages rather than a call.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class RequestAttribute : Role {

            /// <summary>
            ///     The <see cref="ReplyAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Reply { get; init; }

        }

        /// <summary>
        ///     The message that answers, sent on a channel of its own. Being a separate message is what lets the
        ///     requestor be down when it arrives and still receive it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class ReplyAttribute : Role {

            /// <summary>
            ///     The <see cref="RequestAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Request { get; init; }

        }

        /// <summary>
        ///     The participant that sends the request and consumes the reply. It must be able to match one to the
        ///     other, which is what a correlation identifier is for and why the two patterns are always seen together.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class RequestorAttribute : Role { }

        /// <summary>
        ///     The participant that consumes the request and sends the reply. It learns where to answer from the
        ///     message rather than from configuration, which is what lets one replier serve requestors it was never
        ///     told about.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ReplierAttribute : Role { }

    }

}
