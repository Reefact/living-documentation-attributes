#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessageChannelSample {

    // Crane moves go one way, customs responses another, and the terminal's own audit trail a third. Written
    // as queue-name strings scattered through the code, a typo is a message that vanishes and a rename is a
    // search across the solution.
    //
    // MESSAGE CHANNEL gives the path a type. The sender addresses a channel, never a receiver.

    /// <summary>
    ///     A named path a message travels.
    /// </summary>
    /// <remarks>
    ///     What it asserts is that the sender chooses a channel and not a recipient — which is exactly what
    ///     makes a receiver replaceable without the sender being touched.
    /// </remarks>
    [MessageChannel]
    public interface ITerminalChannel {

        string Name { get; }

    }

    /// <summary>Where completed crane moves are announced.</summary>
    public sealed class CraneMovesChannel : ITerminalChannel {

        public string Name => "terminal.crane.moves";

    }
}
