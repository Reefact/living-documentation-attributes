#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DomainDrivenDesign.DomainEventSample {

    // Container terminal: a container comes off a ship, and customs later releases it.
    //
    // A terminal is not one system. The yard planner, the customs broker, the haulier's booking desk
    // and the invoicing back office all need to know that a container was discharged, and none of
    // them can be called synchronously by the crane. What the model publishes is therefore not an
    // instruction to anybody in particular — it is a statement that something happened.
    //
    // Three properties follow from that, and they are why a domain event is its own pattern rather
    // than a message with a nice name:
    //
    //   * It is in the past tense. `ContainerDischarged`, not `DischargeContainer`. The second is a
    //     command, addressed to someone, and it can be refused; the first has already happened and
    //     cannot.
    //   * It is immutable. A subscriber that could edit the event would be rewriting history for
    //     every subscriber after it.
    //   * It carries when it happened, distinctly from when it is handled. Customs may release a
    //     container on Friday and the invoicing run may see it on Monday; the demurrage calculation
    //     needs the Friday.
    //
    // Note that the event carries values, not entity references. A handler waking up on Monday must
    // not be shown the container as it is on Monday — it needs what was true when the event occurred.

    [DomainEvent]
    public sealed record ContainerDischarged(string ContainerNumber, string Vessel, string YardSlot, DateTimeOffset OccurredOn);

    [DomainEvent]
    public sealed record ContainerReleasedByCustoms(string ContainerNumber, string DeclarationNumber, DateTimeOffset OccurredOn);

}
