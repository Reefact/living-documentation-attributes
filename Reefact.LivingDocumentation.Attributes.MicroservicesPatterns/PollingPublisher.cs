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
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class PollingPublisherAttribute : LivingDocumentationAttribute { }

}
