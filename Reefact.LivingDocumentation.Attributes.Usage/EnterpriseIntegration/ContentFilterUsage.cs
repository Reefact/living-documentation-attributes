#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.ContentFilterSample {

    // A vessel manifest arrives with two hundred fields per container, nested four levels deep because it is
    // modelled on the carrier's database. The reefer desk needs four of them: the box, the set point, the
    // plug and whether it is running.
    //
    // CONTENT FILTER cuts the message down and flattens it. Note the difference from a MESSAGE FILTER, which
    // drops whole messages and never touches the ones it keeps.

    public sealed record ManifestLine(string ContainerNumber, ManifestCargo Cargo, ManifestReefer? Reefer);

    public sealed record ManifestCargo(string Description, string HsCode, decimal ValueUsd, string Shipper);

    public sealed record ManifestReefer(decimal SetPointCelsius, string PlugType, bool RunningOnArrival);

    /// <summary>
    ///     What the reefer desk actually reads.
    /// </summary>
    public sealed record ReeferInstruction(string ContainerNumber, decimal SetPointCelsius, string PlugType, bool Running);

    /// <summary>
    ///     Strips a manifest line down to what the reefer desk needs.
    /// </summary>
    /// <remarks>
    ///     It removes items and flattens the nesting at the same time — both are the pattern. The opposite of
    ///     a content enricher, and not a message filter: nothing here decides whether a message travels.
    /// </remarks>
    [ContentFilter]
    public sealed class ReeferManifestFilter {

        public IEnumerable<ReeferInstruction> Filter(IEnumerable<ManifestLine> manifest) {
            foreach (ManifestLine line in manifest) {
                if (line.Reefer is null) { continue; }

                yield return new ReeferInstruction(line.ContainerNumber,
                                                   line.Reefer.SetPointCelsius,
                                                   line.Reefer.PlugType,
                                                   line.Reefer.RunningOnArrival);
            }
        }

    }
}
