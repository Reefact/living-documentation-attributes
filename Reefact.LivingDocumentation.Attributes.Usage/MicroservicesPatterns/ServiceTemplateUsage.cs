#region Usings declarations


using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.ServiceTemplateSample {

    // The chassis stopped the duplication in the framework; it did not stop the eleventh team from spending
    // two days on a csproj, a Dockerfile, a pipeline and a health endpoint.
    //
    // SERVICE TEMPLATE is a runnable service with all of that and no business logic, meant to be copied.
    // Its cost is the one every template has: eleven copies drift, and nothing tells you they came from
    // here.

    /// <summary>
    ///     The skeleton a new service is copied from.
    /// </summary>
    /// <remarks>
    ///     It exists to be copied and never deployed, which is the one thing a reader cannot tell from the
    ///     code: it builds, it runs, it has a health endpoint and it serves one meaningless resource.
    ///     Copies diverge from it the moment they are taken, so knowing which participant is the original
    ///     is the difference between fixing one thing and fixing eleven.
    /// </remarks>
    [ServiceTemplate]
    public sealed class NewGridServiceTemplate {

        public string Name => "grid-service-template";

        public bool IsHealthy => true;

    }
}
