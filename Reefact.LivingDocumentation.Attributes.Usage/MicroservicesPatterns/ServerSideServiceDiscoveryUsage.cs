#region Usings declarations


using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.ServerSideServiceDiscoverySample {

    // The tablets in the field cannot hold a registry client: they are on a mobile network, behind a firewall,
    // and updated when a van comes back to the depot. They need one address that always works.
    //
    // SERVER-SIDE SERVICE DISCOVERY puts a router at that address and lets it do the asking. The callers get
    // simple; the router gets the balancing, the health and the staleness — and here the router is the
    // operator's own code rather than a load balancer, which is exactly when this annotation earns its keep.

    /// <summary>
    ///     The router the callers address instead of the instances.
    /// </summary>
    /// <remarks>
    ///     Most companies do not own this participant — it is a load balancer — and this annotation is for
    ///     the case where they do. Here the resolving and the staleness are this code's problem rather than
    ///     the platform's, which is the whole reason to be able to find it.
    /// </remarks>
    [ServerSideServiceDiscovery]
    public interface IMeteringRouter {

        string Forward(string path);

    }
}
