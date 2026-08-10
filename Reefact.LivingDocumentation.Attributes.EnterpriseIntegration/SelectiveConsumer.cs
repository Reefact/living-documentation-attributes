#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseIntegration {

    /// <summary>
    ///     SelectiveConsumer (Enterprise Integration Patterns) — Lets a consumer take only the messages matching its
    ///     criteria, so that one channel serves several consumers that each want a different part of it.
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
    public static class SelectiveConsumer {

        /// <summary>
        ///     Role played by a type or a member in the SelectiveConsumer design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The sender that sets the selection value before sending. It is a named participant because the selection
        ///     is a contract between two parties who never meet: a producer that stops setting the value breaks
        ///     consumers it has never heard of.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class SpecifyingProducerAttribute : Role { }

        /// <summary>
        ///     What a consumer reads to decide whether the message is for it. Its range is the thing to watch: a value
        ///     no consumer's criteria accept is a message that stays on the channel forever, or until it expires.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class SelectionValueAttribute : Role { }

        /// <summary>
        ///     The consumer that receives only what matches its criteria. Note where it sits: it chooses for itself and
        ///     leaves the rest, whereas a message filter sits in the channel and drops for everyone. On a point-to-
        ///     point channel several of these are competing consumers that are also selective.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class SelectiveConsumerAttribute : Role { }

    }

}
