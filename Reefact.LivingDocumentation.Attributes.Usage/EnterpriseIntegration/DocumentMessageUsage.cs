#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.DocumentMessageSample {

    // The shipping line sends the stowage plan for a vessel. It is not an order — the terminal will use it to
    // plan cranes, the billing system will use it to count moves, and the line does not care which.
    //
    // DOCUMENT MESSAGE transfers a thing rather than an instruction, and that indifference is the point.

    /// <summary>
    ///     A message that transfers data with no instruction attached.
    /// </summary>
    /// <remarks>
    ///     The sender is indifferent to what happens next, which is what lets a document be used by a receiver
    ///     the sender never imagined.
    /// </remarks>
    [DocumentMessage]
    public sealed record StowagePlan(string VesselCall, IReadOnlyList<string> Slots);
}
