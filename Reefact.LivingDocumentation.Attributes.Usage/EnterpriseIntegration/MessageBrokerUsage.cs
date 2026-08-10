#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessageBrokerSample {

    // Eleven applications around the terminal. Point to point, that is fifty-five integrations to build and to
    // keep working, and the twelfth application makes it sixty-six.
    //
    // MESSAGE BROKER replaces that arithmetic with one dependency — and becomes the thing whose failure stops
    // everything, which is a trade to state rather than to discover on a Sunday.

    /// <summary>
    ///     The hub every application sends to and receives from.
    /// </summary>
    /// <remarks>
    ///     The only participant that knows the topology. That is what removes the arithmetic, and what makes it
    ///     the single point of failure worth designing for.
    /// </remarks>
    [MessageBroker]
    public interface ITerminalBroker {

        void Publish(string channel, string message);

        void Route(string fromChannel, string toChannel, Func<string, bool> when);

    }
}
