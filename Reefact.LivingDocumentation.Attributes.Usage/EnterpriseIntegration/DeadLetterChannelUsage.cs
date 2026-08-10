#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.DeadLetterChannelSample {

    // A channel is renamed during a deployment and eleven crane moves have nowhere to go. If the broker drops
    // them the yard is wrong and nobody knows why.
    //
    // DEAD LETTER CHANNEL makes a failure to deliver visible instead of silent.

    /// <summary>
    ///     Where the messaging system puts a message it cannot deliver.
    /// </summary>
    /// <remarks>
    ///     The assertion worth checking is that nothing is lost quietly: a channel with no dead letter channel behind it drops messages and says nothing.
    /// </remarks>
    [DeadLetterChannel]
    public interface IDeadLetters {

        void Park(string message, string reason);

    }
}
