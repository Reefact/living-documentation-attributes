#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessagingGatewaySample {

    // Yard planning is asked for a restow from four places in the terminal application. Written directly,
    // each of the four builds a message, sets a correlation identifier, picks a channel and knows the broker
    // is there. Changing broker means finding all four.
    //
    // MESSAGING GATEWAY leaves the messaging in one class and gives the rest of the application a method that
    // speaks about containers.

    /// <summary>
    ///     The whole of what the application sees of the messaging system.
    /// </summary>
    /// <remarks>
    ///     Its methods take domain arguments rather than message properties, which is what lets the rest of
    ///     the terminal be read, tested and changed without a broker in sight.
    /// </remarks>
    [MessagingGateway]
    public interface IYardPlanningGateway {

        void RequestRestow(string containerNumber, string toPosition);

        string AskCurrentPosition(string containerNumber);

    }
}
