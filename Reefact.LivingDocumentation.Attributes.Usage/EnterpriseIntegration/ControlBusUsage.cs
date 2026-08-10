#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.ControlBusSample {

    // The terminal's components run on the quay, in the gate house, and in a data centre two hundred
    // kilometres away. Asking the yard planner how it is, or telling it to reload its stacking rules, over
    // SSH and a runbook is how a Sunday incident becomes a four-hour incident.
    //
    // CONTROL BUS carries that over the same messaging system, on channels of its own.

    /// <summary>
    ///     The second messaging subsystem: the one that administers the first.
    /// </summary>
    /// <remarks>
    ///     Same mechanism, separate channels. Management traffic that has crept onto an application channel
    ///     is then a defect somebody can name rather than a habit nobody notices.
    /// </remarks>
    [ControlBus.ControlBus]
    public interface ITerminalControlBus {

        void Publish(string componentName, string statistic, string value);

        void Subscribe(string componentName, System.Action<string> onCommand);

    }

    /// <summary>
    ///     Connected to both flows: work orders on one, management on the other.
    /// </summary>
    /// <remarks>
    ///     Annotating it says which parts of the terminal can be told to reconfigure, asked how they are, or
    ///     heard from at all. An absence here is a component nobody can administer.
    /// </remarks>
    [ControlBus.ManagedComponent(ControlBus = typeof(ITerminalControlBus))]
    public sealed class YardPlanningService {

        public void ReloadStackingRules() { }

        public int MovesPlannedSinceStart() {
            return 0;
        }

    }
}
