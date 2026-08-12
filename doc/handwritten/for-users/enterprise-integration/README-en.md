# Enterprise Integration Patterns — the pattern guide

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](README-fr.md)

*Enterprise Integration Patterns: Designing, Building, and Deploying Messaging Solutions* — Gregor
Hohpe and Bobby Woolf, Addison-Wesley, 2003. Sixty-five patterns catalogued, and it is the largest
catalogue here; **ten of them are written up so far**, and the six chapters still missing are named
below with what stands in for them until they exist.

This guide is not the catalogue index. The
[index](../../../generated/catalog-index.md#enterprise-integration-patterns) gives the annotation to
type, what each role applies to, and where the sample is; it is generated, complete, and consulted.
These pages give what a pattern is for, when to reach for it, when not to, and what it costs. They are
written by hand, and they arrive one instalment at a time
([ADR-0040](../../for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.md)).

Every sample in this catalogue is one system — a container terminal, with its customs declarations, its
cranes, its gate kiosks and its EDI manifests — and the pages cross-refer because the code does.

## The integration styles

Chapter 2. Four ways two applications can be made to work together, presented by the book as one choice
rather than as four techniques. The pages cross-refer for that reason: each is best read against the
other three, and the book's own recommendation is the fourth.

| Style | What it is for |
|---|---|
| [File Transfer](FileTransfer-en.md) | One application produces a file the other consumes, so that neither knows anything of the other beyond an agreed format. |
| [Shared Database](SharedDatabase-en.md) | Both read and write one schema, so that there is no data to transfer and nothing to fall out of step. |
| [Remote Procedure Invocation](RemoteProcedureInvocation-en.md) | One calls a procedure the other exposes, so that data and behaviour travel together and the caller learns the answer at once. |
| [Messaging](Messaging-en.md) | Packets of data travel over channels, so that sender and receiver are decoupled in time as well as in technology. |

## The root patterns

Chapter 3. Six patterns out of which the rest of the book is composed: every later chapter specialises
one of these. They are the shortest pages in the catalogue and the ones the others lean on hardest.

| Pattern | What it is for |
|---|---|
| [Message Channel](MessageChannel-en.md) | The logical path a message travels, so that a sender addresses a channel rather than a receiver. |
| [Message](Message-en.md) | Data wrapped in a packet the channel can carry, so that what is sent is a thing in its own right rather than a call's arguments. |
| [Pipes and Filters](PipesAndFilters-en.md) | A larger task divided into independent steps joined by channels, so that a step can be reordered, reused or replaced. |
| [Message Router](MessageRouter-en.md) | Where a message goes next, decided in one place, so that the steps of a process need not know one another's addresses. |
| [Message Translator](MessageTranslator-en.md) | A message converted from one format to another, so that applications with different formats can talk without either being changed. |
| [Message Endpoint](MessageEndpoint-en.md) | How an application attaches to a channel, so that its code sends and receives without holding the messaging system's API. |

## Messaging Channels

Not written yet. Point-to-Point Channel, Publish-Subscribe Channel, Datatype Channel, Invalid Message
Channel, Dead Letter Channel, Guaranteed Delivery, Channel Adapter, Messaging Bridge, Message Bus — all
nine are catalogued and annotated; only their guide pages are missing. Until they exist, the
[index entries](../../../generated/catalog-index.md#enterprise-integration-patterns) and the samples
under
[`DesignPatternCatalog.Usage/EnterpriseIntegration`](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration)
are what there is.

## Message Construction

Not written yet. Command Message, Document Message, Event Message, Request-Reply, Return Address,
Correlation Identifier, Message Sequence, Message Expiration, Format Indicator — same as above.

## Message Routing

Not written yet. Content-Based Router, Message Filter, Dynamic Router, Recipient List, Splitter,
Aggregator, Resequencer, Composed Message Processor, Scatter-Gather, Routing Slip, Process Manager,
Message Broker — twelve, and [Message Router](MessageRouter-en.md) is the root they specialise.

## Message Transformation

Not written yet. Envelope Wrapper, Content Enricher, Content Filter, Claim Check, Normalizer, Canonical
Data Model — six, specialising [Message Translator](MessageTranslator-en.md) as the twelve above specialise
the router.

## Messaging Endpoints

Not written yet. Messaging Gateway, Messaging Mapper, Transactional Client, Polling Consumer,
Event-Driven Consumer, Competing Consumers, Message Dispatcher, Selective Consumer, Durable Subscriber,
Idempotent Receiver, Service Activator — eleven, specialising
[Message Endpoint](MessageEndpoint-en.md).

## System Management

Not written yet. Control Bus, Detour, Wire Tap, Message History, Message Store, Smart Proxy, Test
Message, Channel Purger — eight, and the only chapter here that is about operating a messaging solution
rather than building one.

## How a page is organised

Every page follows the same order.

| | |
|---|---|
| **Intent** | one sentence |
| **Problem** | the situation that makes the pattern worth considering, in code |
| **Solution** | what the pattern does about it |
| **Structure** | a diagram of the roles — a class diagram, or a flow diagram where the pattern is a step in a pipeline |
| **The roles** | one line each, and the annotation that marks it |
| **The example** | the sample from `DesignPatternCatalog.Usage`, in pieces |
| **Applicability** | what the work itself states |
| **When not to use it** | the cases where the pattern costs more than it earns |
| **Advantages** and **Drawbacks** | two lists |
| **Relations with other patterns** | the neighbours, and what separates them |
| **Source** | the work, and links back to the index and the code |

## What these pages do not do

They do not invent. Where the book does not state something, the page says so rather than filling the
section. Four consequences are worth naming for this catalogue in particular.

**The four integration styles are alternatives, and the pages do not flatten them into one
recommendation.** The book prefers messaging and says so, but it also gives File Transfer and Shared
Database real advantages, and those pages carry them as the authors' own rather than converting them
into warnings.

**One of those names is an anti-pattern in another catalogue.** Hohpe and Woolf present Shared Database
as a style to choose; Richardson presents it as the thing *Database per Service* exists to escape.
The catalogue holds both entries — the same schema, the opposite recommendation — and the
[Shared Database](SharedDatabase-en.md) page follows this book's reading and names the other.

**The samples in this catalogue are terser than in the others.** Most elide their bodies with `// ...`,
because what a pattern here asserts is usually a signature and an absence rather than an
implementation — a router that returns a channel name has nowhere to put a payload. The pages therefore
lean more on explaining what is *missing* from a signature than on quoting what is in it.

**One sample takes a shortcut, and its page says so.** The
[Pipes and Filters](PipesAndFilters-en.md) sample's pipeline calls its filters directly rather than
through the pipes it declares. The book admits both arrangements, so the sample is not wrong — but a
reader comparing the diagram to the code would notice, and the page names it first.
