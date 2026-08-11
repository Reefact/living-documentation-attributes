#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.ComposedMessageProcessorSample {

    // A discharge list mixes dry boxes, reefers and hazardous cargo, and each needs different validation. No
    // single step should understand all three.
    //
    // COMPOSED MESSAGE PROCESSOR is the splitter, the router and the aggregator assembled into one addressable
    // step — a composite of three patterns rather than a fourth mechanism.

    /// <summary>
    ///     Splits, routes each element to the processing it needs, and reassembles.
    /// </summary>
    /// <remarks>
    ///     Naming it is what stops the three being reinvented at every call site, and what lets the whole thing
    ///     be addressed as one step from outside.
    /// </remarks>
    [ComposedMessageProcessor]
    public sealed class DischargeValidation {

        public string Process(IReadOnlyList<(string Container, string Kind)> list) {
            // ... split by container, route on Kind, aggregate the verdicts
            return $"{list.Count} validated";
        }

    }
}
