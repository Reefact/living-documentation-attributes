#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.SeparatedInterfaceSample {

    // A laboratory information system: the analyser drivers nobody wants to depend on.
    //
    // The sample-tracking code needs to read results off the bench analysers. There are eleven models from
    // four manufacturers, each with its own serial protocol, and two of the drivers are supplied as binaries
    // by the vendor.
    //
    // Written the ordinary way, tracking references drivers, drivers reference vendor SDKs, and the
    // tracking code can no longer be compiled — let alone tested — without eleven dependencies it has no
    // interest in.
    //
    // A SEPARATED INTERFACE puts the contract with the CLIENT and leaves the implementation elsewhere. The
    // interface below lives in the tracking code's own space; every driver depends on it, and it depends on
    // no driver. The dependency arrow that used to point outward now points inward, which is the entire
    // pattern — everything else follows.
    //
    // What that buys is testable in one sentence: the tracking assembly compiles with zero references to
    // any driver, and an architecture rule can check exactly that. It is also what makes the service stub
    // in ServiceStubUsage.cs possible, and what a Plugin is chosen at configuration time against.

    /// <summary>
    ///     What sample tracking needs from an analyser — declared here, implemented in the drivers.
    /// </summary>
    /// <remarks>
    ///     Deliberately placed with the client rather than with the implementations: nothing in this
    ///     assembly may reference a driver, and this interface is what makes that possible.
    /// </remarks>
    [SeparatedInterface]
    public interface IAnalyser {

        string Model { get; }

        IReadOnlyCollection<AnalyteResult> Read(string sampleBarcode);

    }

    /// <summary>
    ///     One measurement, in the tracking code's terms rather than any vendor's.
    /// </summary>
    public sealed record AnalyteResult(string Analyte, decimal Value, string Unit);

}
