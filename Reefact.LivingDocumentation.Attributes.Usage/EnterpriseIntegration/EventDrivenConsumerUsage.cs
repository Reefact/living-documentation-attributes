#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.EventDrivenConsumerSample {

    // A reefer alarm means a container of frozen fish is warming up. Waiting for the next poll is waiting too
    // long, and nothing about the handling is limited by a person.
    //
    // EVENT-DRIVEN CONSUMER is handed the alarm as it arrives. The contrast with the customs desk next door
    // is the point: same terminal, opposite choice, and the annotation is what makes the two legible as a
    // decision rather than an accident of who wrote which class.

    /// <summary>
    ///     Invoked by the messaging system the moment an alarm is delivered.
    /// </summary>
    /// <remarks>
    ///     The asynchronous receiver: dormant with no thread of its own, and nothing in it limits how many
    ///     alarms it may be handling at once — which is what a reefer desk wants and a customs desk does not.
    /// </remarks>
    [EventDrivenConsumer]
    public interface IReeferAlarmConsumer {

        void OnAlarm(ReeferAlarm alarm);

    }

    public sealed record ReeferAlarm(string ContainerNumber, decimal ObservedCelsius, decimal SetPointCelsius);
}
