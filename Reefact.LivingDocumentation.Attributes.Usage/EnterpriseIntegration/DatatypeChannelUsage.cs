#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.DatatypeChannelSample {

    // Crane moves and customs responses travelled one channel for a year, and every consumer began with a
    // switch on a type discriminator. Two of them got it wrong.
    //
    // DATATYPE CHANNEL trades channels for certainty: a reader knows what it is reading.

    /// <summary>
    ///     A channel restricted to one kind of message.
    /// </summary>
    /// <remarks>
    ///     More channels to manage, and no receiver that has to ask what it just got.
    /// </remarks>
    [DatatypeChannel]
    public interface ICraneMovesOnly {

        void Send(string craneMove);

    }
}
