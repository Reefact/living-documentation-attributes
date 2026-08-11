#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;
using DesignPatternCatalog.Usage.Observatory.Instruments.PluggableComponentFrameworkSample;

#endregion

// The shared telescope, third of three assemblies. The story is in
// Observatory.Instruments/PluggableComponentFrameworkUsage.cs.
//
// A second COMPONENT, and the one that shows what the first bought. The wide-field camera was commissioned
// years after the spectrograph, by a different institute, and it needed nothing from the scheduler and nothing
// from the spectrograph — it implements the same three members and the telescope schedules it.
//
// The camera also answers a very different observing question: it wants short exposures over a large field,
// where the spectrograph wants long ones on a single object. Neither is privileged by the core, and that is
// the distillation working. A core written after only the spectrograph existed would almost certainly have had
// a slit width on it, and this instrument would have had to lie about one.
//
// The annotation is identical to the spectrograph's, deliberately. A component makes no claim to be different
// in kind from its siblings — its whole property is being interchangeable with them.

[assembly: PluggableComponentFramework.Component]

namespace DesignPatternCatalog.Usage.Observatory.WideFieldCamera.PluggableComponentFrameworkSample {

    /// <summary>
    ///     A wide-field survey camera, seen through the same three members as the spectrograph.
    /// </summary>
    public sealed class SurveyCamera : IInstrument {

        public string Name => "Wide-field survey camera";

        public bool CanObserve(ObservationRequest request) {
            return request.Exposure <= TimeSpan.FromMinutes(2);
        }

        public ObservationResult Observe(ObservationRequest request) {
            return new ObservationResult(Name, $"/archive/survey/{request.Target}", CanObserve(request));
        }

    }

}
