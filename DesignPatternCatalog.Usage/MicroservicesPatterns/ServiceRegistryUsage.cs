#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.ServiceRegistrySample {

    // Metering, billing, the grid and field work all move between hosts several times a day. Somebody has to
    // know where they are, and the answer has to be a running thing rather than a configuration file.
    //
    // SERVICE REGISTRY is that thing, and annotating it is worth more than it looks: this is the participant
    // whose own availability bounds the whole application's, and it is the participant that is wrong for a few
    // seconds every time an instance dies badly.

    /// <summary>
    ///     Where every instance of every service says it is.
    /// </summary>
    /// <remarks>
    ///     Everything that discovers depends on this, so its own availability bounds the application's. It
    ///     is also briefly wrong every time an instance dies without saying so, which is what the health
    ///     check is for.
    /// </remarks>
    [ServiceRegistry]
    public interface IGridServiceRegistry {

        void Register(string service, string instance);

        void Deregister(string service, string instance);

        IReadOnlyList<string> InstancesOf(string service);

    }
}
