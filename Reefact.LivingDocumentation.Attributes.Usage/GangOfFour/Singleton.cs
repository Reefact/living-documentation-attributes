#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.SingletonSample {

    // Singleton has a single role, so the attribute takes no argument: [Singleton], not [Singleton.Singleton].

    [Singleton]
    public sealed class FeatureFlags {

        private static readonly Lazy<FeatureFlags> Instance = new(() => new FeatureFlags());

        private FeatureFlags() { }

        public static FeatureFlags Current => Instance.Value;

        public bool IsEnabled(string flag) => false;

    }

}
