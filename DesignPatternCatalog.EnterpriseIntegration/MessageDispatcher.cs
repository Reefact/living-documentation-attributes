#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     MessageDispatcher (Enterprise Integration Patterns) — Puts one consumer on the channel and hands each
    ///     message to a performer, so that several workers process concurrently under a coordination the application
    ///     controls.
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
    public static class MessageDispatcher {

        /// <summary>
        ///     Role played by a type or a member in the MessageDispatcher design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The single consumer on the channel, which obtains a performer and gives it the message. Being the only
        ///     consumer is the difference from competing consumers: the application, not the messaging system, decides
        ///     who gets what and how many run at once.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class DispatcherAttribute : Role {

            /// <summary>
            ///     The <see cref="PerformerAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Performer { get; init; }

        }

        /// <summary>
        ///     The worker the dispatcher hands a message to, often on a thread of its own. It may be created per
        ///     message or drawn from a pool, and the dispatcher may pick a specialised one by looking at the message —
        ///     none of which the channel knows about.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PerformerAttribute : Role { }

    }

}
