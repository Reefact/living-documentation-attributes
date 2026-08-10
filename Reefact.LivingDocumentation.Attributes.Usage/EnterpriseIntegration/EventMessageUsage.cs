#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.EventMessageSample {

    // A crane finishes a lift. Four systems care today and a fifth will care next quarter, and the crane must
    // learn about none of them.
    //
    // EVENT MESSAGE names a fact in the past tense, carries no instruction and expects no reply — which is
    // what makes a new subscriber cost the publisher nothing.

    /// <summary>
    ///     A message naming something that has happened.
    /// </summary>
    /// <remarks>
    ///     Past tense on purpose. It is the message of a publish-subscribe channel rather than of a queue,
    ///     because nothing about it says who should act.
    /// </remarks>
    [EventMessage]
    public sealed record ContainerMoved(string ContainerNumber, string FromSlot, string ToSlot, DateTimeOffset At);
}
