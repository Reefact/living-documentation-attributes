#region Usings declarations

using System;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.AuditLoggingSample {

    // The regulator asks, twice a year, who changed a published tariff and when. The answer used to be
    // reconstructed from application logs, which is to say guessed at.
    //
    // AUDIT LOGGING records user activity as a first-class thing, in a database. The reason to annotate it
    // is not the writing; it is that these records are evidence and must survive a retention policy written
    // for diagnostics.

    /// <summary>
    ///     Who changed a tariff, and when.
    /// </summary>
    /// <remarks>
    ///     These records are evidence rather than diagnostics, and nothing in a logging call shows the
    ///     difference. It matters at exactly one moment: when somebody applies the log retention policy
    ///     and deletes seven years of regulatory obligation along with last week's stack traces.
    /// </remarks>
    [AuditLogging]
    public interface ITariffAudit {

        void Recorded(string user, string tariffCode, decimal oldPrice, decimal newPrice, DateTime at);

    }
}
