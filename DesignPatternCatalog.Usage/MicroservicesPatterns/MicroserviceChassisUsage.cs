#region Usings declarations


using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.MicroserviceChassisSample {

    // By the fourth service, the operator had four copies of the same startup code, three of which read
    // configuration slightly differently and one of which had no health endpoint at all.
    //
    // MICROSERVICE CHASSIS makes that one framework instead of four copies. The line to hold is stated
    // rather than assumed: no business logic here, ever — because anything in the chassis is in every
    // service.

    /// <summary>
    ///     What every service at the operator is built on.
    /// </summary>
    /// <remarks>
    ///     What makes it a chassis is that it holds no business logic. That is the claim worth keeping
    ///     true: a domain decision that leaks in here is a decision every service now shares, whether its
    ///     team was asked or not.
    /// </remarks>
    [MicroserviceChassis]
    public interface IGridChassis {

        void UseExternalizedConfiguration();

        void UseHealthChecks();

        void UseDistributedTracing();

        void UseSelfRegistration();

    }
}
