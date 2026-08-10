#region Usings declarations


using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.SelfRegistrationSample {

    // An instance has to be in the registry before it takes traffic and out of it before it stops. The
    // question is who does the writing.
    //
    // SELF REGISTRATION says the instance does. It knows things nobody outside it knows — whether its
    // migrations ran, whether its cache is warm — and it pays for that with the one case it cannot handle:
    // a process that dies does not deregister itself.

    /// <summary>
    ///     Metering, putting itself in the registry.
    /// </summary>
    /// <remarks>
    ///     It knows its own state, which is the argument for the pattern. What it cannot do is deregister
    ///     after it has crashed — so the registry stays wrong until the lease expires, and a caller with
    ///     client-side discovery meets that gap first.
    /// </remarks>
    [SelfRegistration]
    public sealed class MeteringRegistration {

        private readonly IServiceRegistry _registry;
        private readonly string           _instance;

        public MeteringRegistration(IServiceRegistry registry, string instance) {
            _registry = registry;
            _instance = instance;
        }

        public void OnStarted() => _registry.Register("metering", _instance);

        public void OnStopping() => _registry.Deregister("metering", _instance);

    }

    /// <summary>What the registration talks to.</summary>
    public interface IServiceRegistry {

        void Register(string service, string instance);

        void Deregister(string service, string instance);

    }
}
