#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.HealthCheckApiSample {

    // A metering instance lost its connection to the reading store and kept accepting requests, failing
    // every one of them, for eleven minutes — because the process was up and nothing asked it anything
    // harder than that.
    //
    // HEALTH CHECK API is the endpoint that answers something harder. What it checks is a decision with
    // teeth: whatever it leaves out is what will keep receiving traffic while broken.

    /// <summary>
    ///     Whether this metering instance can actually work.
    /// </summary>
    /// <remarks>
    ///     Its verdict takes the instance out of rotation, so what it does not check is what will keep
    ///     receiving traffic while broken. A handler that returns healthy unconditionally is
    ///     indistinguishable from one that works — until the night it matters.
    /// </remarks>
    public sealed class MeteringHealth {

        private readonly IReadOnlyList<Func<bool>> _checks;

        public MeteringHealth(IReadOnlyList<Func<bool>> checks) {
            _checks = checks;
        }

        [HealthCheckApi]
        public bool Get() {
            foreach (Func<bool> check in _checks) {
                if (!check()) { return false; }
            }

            return true;
        }

    }
}
