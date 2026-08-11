#region Usings declarations


using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.ExternalizedConfigurationSample {

    // The same metering binary runs on a developer's laptop, in three test environments and in production.
    // The only thing that differs is where the database is and how tolerant the validation is.
    //
    // EXTERNALIZED CONFIGURATION keeps that difference outside the build. What it buys is that the artifact
    // promoted to production is the artifact that was tested, byte for byte.

    /// <summary>
    ///     Everything metering needs to know that differs between environments.
    /// </summary>
    /// <remarks>
    ///     The claim is that none of it is compiled in, which is what lets one artifact be promoted from
    ///     test to production rather than rebuilt. A single hard-coded fallback ends that quietly, without
    ///     failing anything, and this annotation is what it contradicts.
    /// </remarks>
    [ExternalizedConfiguration]
    public sealed class MeteringSettings {

        public MeteringSettings(string readingStore, string brokerAddress, int validationTolerance) {
            ReadingStore        = readingStore;
            BrokerAddress       = brokerAddress;
            ValidationTolerance = validationTolerance;
        }

        public string ReadingStore { get; }

        public string BrokerAddress { get; }

        public int ValidationTolerance { get; }

    }
}
