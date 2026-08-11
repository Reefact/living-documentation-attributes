#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     TransactionLogTailing (Microservices Patterns) — Publishes the messages an outbox holds by reading the
    ///     database's own transaction log, so that every committed row reaches the broker and nothing else has to be
    ///     asked.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of TransactionalOutbox's MessageRelay role: every participant annotated here is one of
    ///         those too, and a consumer asking for that role gets these as well.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TransactionLogTailingAttribute : TransactionalOutbox.MessageRelayAttribute { }

}
