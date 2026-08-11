#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.MessageExpirationSample {

    // A gate instruction queued during a two-hour broker outage arrives after the truck has left. Obeying it
    // opens a lane for a vehicle that is no longer there, and the next one drives through.
    //
    // MESSAGE EXPIRATION lets the receiver know that acting late is worse than not acting.

    /// <summary>
    ///     An instruction that stops being worth obeying.
    /// </summary>
    public sealed class OpenGateLane {

        public OpenGateLane(string lane, DateTimeOffset validUntil) {
            Lane       = lane;
            ValidUntil = validUntil;
        }

        public string Lane { get; }

        /// <summary>
        ///     After this, do not process.
        /// </summary>
        /// <remarks>
        ///     A message queued through an outage may arrive after it has become wrong, and a receiver has no
        ///     other way to know that.
        /// </remarks>
        [MessageExpiration]
        public DateTimeOffset ValidUntil { get; }

    }
}
