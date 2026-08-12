# Channel Adapter

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](ChannelAdapter-fr.md)

## Intent

Channel Adapter connects an application to the messaging system from outside it, so that an application that
knows nothing of messaging can still take part.

## Problem

The weighbridge is a twenty-year-old system with a serial port and no notion of a message.

Every truck crosses it and every weight matters, so the terminal's integration needs those readings. The obvious
answer — add a publisher to the weighbridge — is not available: the vendor is gone, the source is not certain to
exist, and the certification the scale carries is not worth reopening to add a message.

The application cannot be changed. The integration still has to happen.

## Solution

The pattern reaches in from outside.

A channel adapter speaks the application's own interface on one side — a serial port, a database table, a file
drop, a proprietary API — and a channel on the other. It converts between them, and it lives outside the
application, so the application is not modified and does not know it is being integrated.

The book presents this as often the only option there is, and that is the honest framing: a channel adapter is not
the arrangement anybody would design from nothing, it is the arrangement available when one side cannot change.

## Structure

```mermaid
flowchart LR
    W["weighbridge<br/>serial port"]
    A["WeighbridgeAdapter<br/>[ChannelAdapter]"]
    C{{"terminal.weights"}}
    R["billing"]
    W -->|"serial, unchanged"| A
    A -->|"weight message"| C
    C --> R
```

The arrow into the adapter is the application's own interface, and the arrow out is a channel. The weighbridge box
has no messaging in it, which is the whole arrangement.

## The roles

| Role | Annotation | Applies to | What it carries |
|---|---|---|---|
| ChannelAdapter | `[ChannelAdapter]` | interface, class | The participant that reads or writes an application's own interface on one side and a channel on the other. |

One role, and it marks the participant on the *messaging* side of the boundary. The application it adapts carries
no annotation, because the application carries no code of ours — that asymmetry is the pattern.

## The example

From [`ChannelAdapterUsage.cs`](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration/ChannelAdapterUsage.cs).

```csharp
[ChannelAdapter]
public sealed class WeighbridgeAdapter {

    public void Poll() {
        // ... reads the serial port, publishes a weight message
    }

}
```

The method is `Poll`, and that word is the pattern's cost stated as a signature. A weighbridge with no notion of a
message cannot announce anything, so the adapter has to go and ask — which means a schedule, an interval, and a
window in which a reading exists and has not been published yet.

The comment names both sides in one line: *reads the serial port, publishes a weight message.* Those are the two
faces of the adapter, and there is nothing else in the class.

It is a class rather than an interface, which is the right way round here. There is no seam to swap: this adapter
is bound to one real system's real interface, and pretending otherwise would suggest the serial protocol is
replaceable.

The sample states the reason the pattern exists rather than only what it does: *it is what lets a system take part
in an integration without being modified, which is often the only option there is.*

## Applicability

**Use a channel adapter where the application cannot be changed.** A vendor system, a certified device, a
mainframe, anything whose source is unavailable or whose modification is not worth the cost.

**Use it where the application predates messaging.** The book's case: a system built with no notion of a channel
can still be integrated, from outside.

**Use it in either direction.** Reading the application's data onto a channel, and taking messages off a channel
into the application's own interface, are both this pattern.

**Keep it outside the application.** The adapter being separate is what leaves the application unmodified, which
is the whole point rather than an implementation detail.

## When not to use it

**Do not use it where the application can simply be given an endpoint.** If the source is available and
modifiable, [Message Endpoint](MessageEndpoint-en.md) is the honest arrangement: the application says what it
sends, rather than having its data read out from underneath it.

**Do not let it read the application's private state as though it were an interface.** An adapter selecting
straight from another application's tables is coupled to that schema, and a schema nobody promised is a schema
that changes without notice. That coupling is [Shared Database](SharedDatabase-en.md)'s, acquired without
choosing it.

**Do not put business rules in it.** An adapter that decides which weights are billable has put a domain decision
outside the domain, in a class whose subject is a serial port.

**Do not treat polling as free.** An adapter that polls introduces latency, load and a duplicate-suppression
problem — knowing which readings it has already published — and none of those exist in the application it adapts.

**Do not use it to bridge two messaging systems.** That is [Messaging Bridge](MessagingBridge-en.md): both sides
there are channels, and neither is an application's own interface.

## Advantages

* An application that cannot be changed can still take part in the integration.
* The change is entirely on the messaging side, so the adapted application carries no risk from it.
* It works in both directions with the same shape.
* The coupling to a legacy interface is concentrated in one named class, which is where it can be reviewed.

## Drawbacks

* It is usually a poll, with the latency, the load and the duplicate-detection problem that come with one.
* It is coupled to an interface nobody promised to keep, and a serial layout or a table can change without
  warning.
* It knows the legacy system's quirks, so its correctness is hard to argue and hard to test.
* Reaching into another application's data can amount to a shared database without the agreement one implies.
* The adapted application cannot report a problem, because it does not know it is part of an integration.

## Relations with other patterns

**`MessageEndpoint`** is the arrangement this replaces when the application cannot be modified — an endpoint is
inside the application, an adapter is outside it.

**`MessagingBridge`** is the same shape between two messaging systems rather than between an application and one.

**`MessagingGateway`** is the endpoint form that hides messaging from application code, which is the opposite
direction of hiding: there, the application is spared the messaging; here, it is spared everything.

**`SharedDatabase`** is what a channel adapter becomes when it reads another application's tables directly, and
that is worth naming because it happens by accident.

**`Messaging`** is the style this lets a non-messaging system join, which is why the book puts it among the
channels rather than among the endpoints.

## Source

*Enterprise Integration Patterns*, Gregor Hohpe and Bobby Woolf, Addison-Wesley, 2003 — the messaging-channels
chapter.

* [Index entry](../../../generated/catalog-index.md#channeladapter-enterprise-integration-patterns)
* [Generated attribute](../../../../DesignPatternCatalog.EnterpriseIntegration/ChannelAdapter.cs)
* [Example](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration/ChannelAdapterUsage.cs)
