#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.MessageFilterSample {

    // The reefer desk cares about refrigerated containers and nothing else. Ninety per cent of what the crane
    // channel carries is dry boxes it will look at and drop.
    //
    // MESSAGE FILTER drops them before they arrive, for everyone reading that channel.

    /// <summary>
    ///     A router with one output and the option of none.
    /// </summary>
    /// <remarks>
    ///     The distinction from a selective consumer is where it sits: a filter is in the channel and drops for
    ///     everyone; a selective consumer chooses for itself and leaves the rest for others.
    /// </remarks>
    [MessageFilter]
    public sealed class ReeferOnlyFilter {

        public bool Passes(string containerType) => containerType is "RE" or "RF";

    }
}
