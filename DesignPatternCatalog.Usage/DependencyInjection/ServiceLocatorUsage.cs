#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.DependencyInjection;

#endregion

namespace DesignPatternCatalog.Usage.DependencyInjection.ServiceLocatorSample {

    // The nineteen resolve calls the composition root replaced did not all go away. Four are in the
    // schedule editor, which runs inside a plug-in host the station does not control: the host constructs
    // the editor and there is no seam to inject through. Those four are staying until the host is
    // replaced, and marking them is how they stop being invisible.
    //
    // The annotation is on both sides, and the two sides say different things. On the registry it says
    // where the boundary is, so a rule can range over everything that touches it. On the consumer it says
    // the thing that actually costs.
    //
    // Whether this is an anti-pattern is a live disagreement between two authors — Fowler named it as a
    // pattern and leans toward it for application code; Seemann calls it an anti-pattern. Note what the
    // annotation does NOT do: it does not take a side. It records a structural fact — this class does not
    // state its preconditions — which is true either way, and leaves the verdict to whoever writes the
    // rule.

    /// <summary>
    ///     The registry the plug-in host gives the editor.
    /// </summary>
    /// <remarks>
    ///     One of these against many consumers, which is why annotating it is about the boundary rather
    ///     than about the cost: it is the thing a rule looks for references to.
    /// </remarks>
    [ServiceLocator.ServiceLocator]
    public interface IHostServices {

        T Resolve<T>() where T : class;

    }

    /// <summary>
    ///     Edits the running schedule from inside the plug-in host.
    /// </summary>
    /// <remarks>
    ///     Its constructor takes the registry and nothing else, so **nothing about it says what must be
    ///     registered for it to work**. Two things follow, and the second is the one that bites somebody
    ///     other than its author: a missing registration is a failure at run time rather than a broken
    ///     build, and adding a dependency inside this class is a breaking change that breaks no build at
    ///     all — every host compiles and the one that forgot to register fails when a producer opens the
    ///     editor.
    ///     <para>
    ///         In Seemann's later formulation: the class does not communicate its preconditions, so its
    ///         contract is incomplete. That is the fact recorded here. Whether it is a defect is the
    ///         station's rule to state, not this annotation's.
    ///     </para>
    /// </remarks>
    [ServiceLocator.Consumer(ServiceLocator = typeof(IHostServices))]
    public sealed class ScheduleEditor {

        private readonly IHostServices _host;

        public ScheduleEditor(IHostServices host) {
            _host = host;
        }

        public IReadOnlyList<string> Open(DateOnly day) {
            // Neither of these appears in the constructor, so neither appears in the contract.
            IScheduleRepository schedules = _host.Resolve<IScheduleRepository>();
            IProducerDirectory  producers = _host.Resolve<IProducerDirectory>();

            return schedules.For(day, producers.OnDuty(day));
        }

    }

    public interface IScheduleRepository {

        IReadOnlyList<string> For(DateOnly day, string producer);

    }

    public interface IProducerDirectory {

        string OnDuty(DateOnly day);

    }

}
