#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     TransactionalClient (Enterprise Integration Patterns) — Makes a client's session with the messaging system
    ///     transactional, so that the client says where a transaction begins and ends rather than the infrastructure
    ///     deciding for it.
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
    public static class TransactionalClient {

        /// <summary>
        ///     Role played by a type or a member in the TransactionalClient design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     A sender whose message is not really on the channel until it commits. That is the guarantee worth
        ///     annotating: work done before the commit can be abandoned without anyone downstream ever having seen it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class SenderAttribute : Role { }

        /// <summary>
        ///     A receiver whose message is not really off the channel until it commits. The mirror guarantee, and a
        ///     different one: a crash mid-processing returns the message rather than losing it, at the price of the
        ///     receiver having to tolerate seeing it twice.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ReceiverAttribute : Role { }

    }

}
