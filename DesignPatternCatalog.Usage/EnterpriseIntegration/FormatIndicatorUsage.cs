#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.FormatIndicatorSample {

    // The terminal's crane message gained a field. Six consumers read the old shape, and they will not all be
    // redeployed on the same afternoon.
    //
    // FORMAT INDICATOR is the cheapest thing to add before the first version ships and the most expensive
    // afterwards: without it, a message can only be read by guessing.

    /// <summary>
    ///     A message that says which shape it is in.
    /// </summary>
    public sealed class CraneMoveMessage {

        public CraneMoveMessage(string schemaVersion, string containerNumber) {
            SchemaVersion   = schemaVersion;
            ContainerNumber = containerNumber;
        }

        /// <summary>
        ///     Which version or format this message is in.
        /// </summary>
        /// <remarks>
        ///     It lets a receiver accept more than one shape and a sender move to a third, without either
        ///     guessing.
        /// </remarks>
        [FormatIndicator]
        public string SchemaVersion { get; }

        public string ContainerNumber { get; }

    }
}
