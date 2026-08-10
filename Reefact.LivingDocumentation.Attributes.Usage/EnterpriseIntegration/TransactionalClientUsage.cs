#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.TransactionalClientSample {

    // A gate transaction writes a row and publishes a message. If the row rolls back and the message has gone,
    // the yard is planning for a container that never entered the terminal.
    //
    // TRANSACTIONAL CLIENT lets the client draw the boundary. Two roles, because the two guarantees are not
    // the same one seen from both ends.

    /// <summary>
    ///     A sender whose message is not really on the channel until it commits.
    /// </summary>
    /// <remarks>
    ///     Work done before the commit can be abandoned without anyone downstream ever having seen it.
    /// </remarks>
    [TransactionalClient.Sender]
    public interface IGateTransactionPublisher {

        void Publish(string containerNumber);

        void Commit();

        void Rollback();

    }

    /// <summary>
    ///     A receiver whose message is not really off the channel until it commits.
    /// </summary>
    /// <remarks>
    ///     The mirror guarantee, and a different one: a crash mid-processing returns the message rather than
    ///     losing it — at the price of having to tolerate seeing it twice, which is why an idempotent receiver
    ///     is usually nearby.
    /// </remarks>
    [TransactionalClient.Receiver]
    public interface IBillingEventConsumer {

        string Receive();

        void Commit();

    }
}
