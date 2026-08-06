#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.PluginSample {

    // Laboratory information system: which analyser driver runs is decided by a config file, not by a build.
    //
    // The same software runs in nineteen hospital labs. Each has a different bench: one has two Roche
    // analysers, another has a Siemens and a legacy Beckman kept alive by one engineer. Recompiling per
    // site is not a deployment strategy — the software is one artefact, and the site decides what it talks
    // to.
    //
    // A PLUGIN is that decision point: the implementation is chosen at CONFIGURATION time, by name, from
    // whatever is present.
    //
    // What separates it from an interface that merely has two implementations is where the choice is made.
    // If the composition root picks one with a `new`, the choice is in the code and this is not a plugin.
    // Here nothing in the system names a driver at all; the site's configuration does, and a driver that
    // did not exist when this shipped can be dropped in beside it.
    //
    // That indirection is worth its cost here and is not worth it often. An interface given to a single
    // implementation out of habit is not this pattern — it is one implementation with a ceremony.

    /// <summary>
    ///     The contract a driver satisfies to be usable by name from a site's configuration.
    /// </summary>
    /// <remarks>
    ///     Nothing in the core references an implementation of this. A site's file says
    ///     <c>analyser = "roche-cobas-8000"</c>, and the factory resolves it at start-up.
    /// </remarks>
    [Plugin]
    public interface IAnalyserDriver {

        /// <summary>The name a configuration file uses to ask for this driver.</summary>
        string ConfigurationKey { get; }

        void Connect(string port);

    }

}
