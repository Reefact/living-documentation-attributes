#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.ClientSideServiceDiscoverySample {

    // The billing run calls metering a few hundred thousand times a night, and metering runs on however many
    // instances the autoscaler felt like this evening. There is no fixed address to call.
    //
    // CLIENT-SIDE SERVICE DISCOVERY has billing ask the registry and choose for itself. What it buys is one
    // less hop; what it takes on is the balancing, the retries and the staleness — the registry can hand out
    // an instance that died a second ago.

    /// <summary>
    ///     Resolves a metering instance and picks one.
    /// </summary>
    /// <remarks>
    ///     The balancing, the retry and the staleness all live here, and none of them appears in the call
    ///     this class eventually makes. It can pick an instance the registry has not yet noticed is gone.
    /// </remarks>
    [ClientSideServiceDiscovery]
    public sealed class MeteringLocator {

        private readonly IReadOnlyList<string> _instances;

        private int _next;

        public MeteringLocator(IReadOnlyList<string> instances) {
            _instances = instances;
        }

        public string Resolve() => _instances[_next++ % _instances.Count];

    }
}
