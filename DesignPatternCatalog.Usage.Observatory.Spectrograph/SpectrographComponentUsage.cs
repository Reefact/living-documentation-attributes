#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;
using DesignPatternCatalog.Usage.Observatory.Instruments.PluggableComponentFrameworkSample;

#endregion

// The shared telescope, second of three assemblies. The story is in
// Observatory.Instruments/PluggableComponentFrameworkUsage.cs.
//
// A COMPONENT: the échelle spectrograph, delivered in 2011 by the institute that built it.
//
// Everything specific to it is behind the shared interface — the grating angles, the calibration lamps, the
// fact that it is useless in bright moonlight. The scheduler knows none of that; it asks whether the
// instrument can take a request, and the instrument answers.
//
// This assembly references the abstract core and nothing else, and the important half of that is the "nothing
// else". The wide-field camera next door has a good exposure-time calculator, and using it from here would be
// two lines and would work. It would also mean the spectrograph could no longer be deployed without the
// camera, and the property the whole arrangement was bought for — swap one instrument, leave the rest — would
// be gone with no error message and no failing test.

[assembly: PluggableComponentFramework.Component]

namespace DesignPatternCatalog.Usage.Observatory.Spectrograph.PluggableComponentFrameworkSample {

    /// <summary>
    ///     A high-resolution échelle spectrograph, seen through the core's three members.
    /// </summary>
    public sealed class EchelleSpectrograph : IInstrument {

        public string Name => "Échelle spectrograph";

        /// <summary>
        ///     Refuses what it cannot do well, in its own terms, without the scheduler knowing any of them.
        /// </summary>
        public bool CanObserve(ObservationRequest request) {
            return request.Band is "optical" or "near-infrared" && request.Exposure >= TimeSpan.FromMinutes(5);
        }

        public ObservationResult Observe(ObservationRequest request) {
            return new ObservationResult(Name, $"/archive/echelle/{request.Target}", CanObserve(request));
        }

    }

}
