#region Usings declarations

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.AntiCorruptionLayerSample {

    // The legacy customer information system calls a supply point an SP_REC, keys it by a nine-digit
    // internal number, and encodes whether it is live in the sign of a balance field. The new metering
    // service has a supply point with a national identifier and a status. Neither model is going to change:
    // one is frozen, the other is the point of the migration.
    //
    // ANTI-CORRUPTION LAYER keeps the frozen one out. It is the only place that knows both, and it is
    // written to be deleted — when the monolith goes, this goes. A codebase that can list its layers can
    // say how much of the migration is still owed.

    /// <summary>
    ///     What the legacy system hands over.
    /// </summary>
    /// <remarks>
    ///     Its shape is not this codebase's decision, which is exactly why it must not travel inwards.
    /// </remarks>
    public sealed class SpRec {

        public SpRec(string spNumber, decimal balance) {
            SpNumber = spNumber;
            Balance  = balance;
        }

        public string SpNumber { get; }

        public decimal Balance { get; }

    }

    /// <summary>
    ///     What metering means by a supply point.
    /// </summary>
    public sealed class SupplyPoint {

        public SupplyPoint(string nationalIdentifier, bool isLive) {
            NationalIdentifier = nationalIdentifier;
            IsLive             = isLive;
        }

        public string NationalIdentifier { get; }

        public bool IsLive { get; }

    }

    /// <summary>
    ///     The layer, and the only thing that knows both models.
    /// </summary>
    /// <remarks>
    ///     Flat, one role, where the Domain-Driven Design entry of the same name has three — Richardson's
    ///     page states the translation and stops, and an entry carries the assertions its own work makes.
    ///     Reaching for <c>Balance</c> anywhere but here is what this annotation exists to make reviewable.
    /// </remarks>
    [AntiCorruptionLayer]
    public sealed class LegacySupplyPointTranslator {

        public SupplyPoint Translate(SpRec record) =>
            new SupplyPoint($"GB{record.SpNumber}", record.Balance >= 0m);

    }
}
