#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

// A shared telescope, with instruments built by different institutes over twenty years. Three assemblies:
// this one, Observatory.Spectrograph and Observatory.WideFieldCamera.
//
// This is the ABSTRACT CORE of a PLUGGABLE COMPONENT FRAMEWORK — the interfaces every instrument implements
// and the scheduler calls through. Nothing else is shared.
//
// The circumstance the pattern answers is specific, and it is the reason not to reach for it casually. Several
// teams that do not report to each other must interoperate over a long period; nobody can be told to rebuild;
// and an instrument commissioned in 2031 must run under a scheduler written in 2011 without either being
// changed. That is a real constraint at an observatory and it is rare elsewhere.
//
// What the arrangement costs is where the discipline is. Anything added to this assembly must be implemented
// by every component, including the ones whose authors have moved on — so the core is DISTILLED, not
// accumulated. `IInstrument` below has three members, and the pressure to add a fourth is permanent: the
// spectrograph team wants a slit width here, the camera team wants a filter wheel, and each request is
// reasonable on its own. A core that grants them is a core that no new component can implement.
//
// The annotations exist because the two rules that keep this working are dependency rules and nothing else
// states them: a component may reference this assembly, and no component may reference a sibling. Both are
// invisible in a diff — the reference that breaks the framework is one line in a project file, added because
// one instrument genuinely needed something another had already written.

[assembly: PluggableComponentFramework.AbstractCore]

namespace DesignPatternCatalog.Usage.Observatory.Instruments.PluggableComponentFrameworkSample {

    /// <summary>
    ///     What every instrument on the telescope is, as far as the scheduler is concerned.
    /// </summary>
    /// <remarks>
    ///     Three members, and it stays at three. Each addition would have to be implemented by instruments
    ///     whose teams no longer exist, which is the constraint that makes distillation a requirement rather
    ///     than good taste.
    /// </remarks>
    public interface IInstrument {

        string Name { get; }

        bool CanObserve(ObservationRequest request);

        ObservationResult Observe(ObservationRequest request);

    }

    /// <summary>
    ///     What an astronomer asks for, in terms no instrument is privileged by.
    /// </summary>
    public sealed record ObservationRequest(string Target, TimeSpan Exposure, string Band);

    /// <summary>
    ///     What comes back, in the same shared vocabulary.
    /// </summary>
    public sealed record ObservationResult(string Instrument, string ArchivePath, bool Usable);

}
