#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.CommandMessageSample {

    // The container terminal again. A customs hold must be applied to a container: that is an instruction, it
    // has one rightful handler, and ignoring it is a defect rather than a choice.
    //
    // COMMAND MESSAGE says so in the type. A reader who sees this name knows that nothing may quietly decide
    // not to act.

    /// <summary>
    ///     A message whose content is an imperative.
    /// </summary>
    /// <remarks>
    ///     It expects one handler and, usually, a reply saying what happened. Naming it a command is what tells
    ///     a reader that dropping it is a fault.
    /// </remarks>
    [CommandMessage]
    public sealed record ApplyCustomsHold(string ContainerNumber, string Reason, string LodgedBy);
}
