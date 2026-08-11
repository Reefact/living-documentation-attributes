#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.Posa2;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.Posa2.ComponentConfiguratorSample {

    // The traffic service runs a rule per hazard: under-keel clearance, tug escort for laden tankers,
    // the seasonal whale-avoidance limit that applies from November to April. Rules are added by the
    // harbour master's office, not by the software team.
    //
    // Restarting to change a rule means every vessel track is rebuilt from the AIS history, which takes
    // eleven minutes during which the operators watch a loading bar instead of the harbour. So rules are
    // never changed during daylight, which means they are changed at 02:00 by whoever is on call.
    //
    // COMPONENT CONFIGURATOR makes a rule something the running service can be told to load, suspend or
    // drop. The whale limit is now suspended in April by somebody awake.

    /// <summary>
    ///     What the service can do to a rule without knowing what the rule is.
    /// </summary>
    /// <remarks>
    ///     Everything the configurator has authority over is on this interface. A rule that needs anything
    ///     else — a warm-up, a second phase, an ordering guarantee against another rule — is a rule the
    ///     configurator cannot honestly manage, and the gap will show up as a rule that is loaded but not
    ///     working.
    /// </remarks>
    [ComponentConfigurator.Component]
    public interface ITrafficRule {

        void Initialise(IReadOnlyDictionary<string, string> settings);

        void Suspend();

        void Resume();

        void Terminate();

        bool Permits(string vesselId, decimal draughtMetres);

    }

    /// <summary>
    ///     The seasonal speed limit for the whale-avoidance area.
    /// </summary>
    /// <remarks>
    ///     Its initialisation runs when the harbour master loads it, not at start-up — so anything it
    ///     assumes about the tide model already being there is an assumption about the order somebody
    ///     types two commands in.
    /// </remarks>
    [ComponentConfigurator.ConcreteComponent(Component = typeof(ITrafficRule))]
    public sealed class WhaleSeasonLimit : ITrafficRule {

        private bool    _suspended;
        private decimal _maximumDraught = 12.0m;

        public void Initialise(IReadOnlyDictionary<string, string> settings) {
            if (settings.TryGetValue("maximumDraught", out string? value)) {
                _maximumDraught = decimal.Parse(value);
            }
        }

        public void Suspend() {
            _suspended = true;
        }

        public void Resume() {
            _suspended = false;
        }

        public void Terminate() { }

        public bool Permits(string vesselId, decimal draughtMetres) {
            return _suspended || draughtMetres <= _maximumDraught;
        }

    }

    /// <summary>
    ///     The rules configured into the running service.
    /// </summary>
    /// <remarks>
    ///     A rule absent from here is not merely unreachable: it cannot be suspended, resumed or
    ///     terminated either, because there is nothing to name. That is the failure mode of a rule that
    ///     was loaded and never registered — it runs, and no command reaches it.
    /// </remarks>
    [ComponentConfigurator.ComponentRepository(Component = typeof(ITrafficRule))]
    public sealed class RuleRepository {

        private readonly Dictionary<string, ITrafficRule> _configured = new Dictionary<string, ITrafficRule>();

        public void Add(string name, ITrafficRule rule) {
            _configured[name] = rule;
        }

        public bool Remove(string name, out ITrafficRule? rule) {
            return _configured.Remove(name, out rule);
        }

        public IEnumerable<ITrafficRule> All() {
            return _configured.Values;
        }

    }

    /// <summary>
    ///     Loads and drops rules while the service is running.
    /// </summary>
    /// <remarks>
    ///     The one participant that can change what the service is made of after it has started, which is
    ///     the authority the pattern grants and the reason the annotation is worth having: a reader
    ///     looking for what can change under them at 02:00 finds it here.
    /// </remarks>
    [ComponentConfigurator.ComponentConfigurator(Component = typeof(ITrafficRule))]
    public sealed class RuleConfigurator {

        private readonly RuleRepository _repository;

        public RuleConfigurator(RuleRepository repository) {
            _repository = repository;
        }

        public void Load(string name, ITrafficRule rule, IReadOnlyDictionary<string, string> settings) {
            rule.Initialise(settings);
            _repository.Add(name, rule);
        }

        public void Unload(string name) {
            if (_repository.Remove(name, out ITrafficRule? rule)) { rule!.Terminate(); }
        }

    }

}
