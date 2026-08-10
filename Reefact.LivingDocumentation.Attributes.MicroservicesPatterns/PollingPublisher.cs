#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     PollingPublisher (Microservices Patterns) — Publishes the messages an outbox holds by polling the table for
    ///     them, which works on any database and buys that with a poll interval and an ordering problem.
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
    public sealed class PollingPublisherAttribute : TransactionalOutbox.MessageRelayAttribute { }

}
