# ADR-0029 | Admit Enterprise Integration Patterns as a catalogue

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0029-admit-enterprise-integration-patterns-as-a-catalogue.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-09
**Accepted:** 2026-08-09
**Decision Makers:** Reefact

## Context

Five works are catalogued — *Design Patterns* (1994), *Analysis Patterns* (1997),
*Accounting Patterns* (2000), *Patterns of Enterprise Application Architecture* (2002)
and *Domain-Driven Design* (2003) — plus `Idioms` for patterns with no body of work of
their own. 147 patterns, 316 roles.

Two of those catalogues account for 48 entries and are the least read of the five: the
author of *Analysis Patterns* says of it himself that it is showing its age. The
maintainer's stated aim is now patterns in daily use rather than more patterns.

*Enterprise Integration Patterns* — Hohpe and Woolf, Addison-Wesley, 2003 — holds **65
patterns**, and its authors maintain the canonical index on
`enterpriseintegrationpatterns.com`, giving each pattern's name and the question it
answers. The list was read from there rather than reconstructed.

What those 65 are, counted:

* about **fifty are components**: a class is a Content-Based Router, a Splitter, an
  Aggregator, a Resequencer, a Content Enricher, an Idempotent Receiver, a Service
  Activator;
* **five are properties on a message** — Correlation Identifier, Return Address, Message
  Expiration, Format Indicator, Message History;
* **six are channels** — Message Channel and the five kinds, including Dead Letter
  Channel and Invalid Message Channel. A channel is often a configured name rather than a
  type;
* **four are integration styles** — File Transfer, Shared Database, Remote Procedure
  Invocation, Messaging — chosen for an integration rather than held by a participant;
* **Guaranteed Delivery** is a property of the transport, and **Request-Reply** is an
  interaction over two channels rather than a participant in one.

Three of its names collide with catalogued patterns: Messaging Gateway with
`EnterpriseApplicationArchitecture/Gateway`, Messaging Mapper with `Mapper`, and Smart
Proxy with `GangOfFour/Proxy`. Since
[ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) each catalogue ships
as its own package and no relation crosses one, so a collision between two catalogues
needs no arbitration.

**Pipes and Filters** and **Message Broker** originate in *Pattern-Oriented Software
Architecture* (Buschmann et al., 1996). *Enterprise Integration Patterns* presents each
in full — its own problem statement, its own discussion, adapted to messaging — and
credits POSA for it.

[ADR-0011](0011-leave-out-what-cannot-be-annotated.md) leaves out of the catalogue what
cannot be attached to a type, a member or an assembly.
[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) supplies the
criterion that a pattern must license assertions something can range over.
[ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) holds a
pattern in every catalogue whose work presents it as its own, and not where a work merely
cites another's.

Nothing is released.

## Decision

*Enterprise Integration Patterns* is admitted as the catalogue `EnterpriseIntegration`,
holding all sixty-five of its patterns.

## Rationale

It meets the three criteria the catalogue already applies, and it meets the second of them
better than any work admitted so far. Its patterns are **components**: a class *is* a
router, a translator, an aggregator, with nothing to approximate and no marker type to
invent. Where *Analysis Patterns* asks a reader to have built a model before an annotation
means anything, this asks only that they have written the class the pattern names.

The assertions are the checkable kind, which is what ADR-0007 requires of an admission
rather than of a comparison. An Idempotent Receiver asserts that the same message
delivered twice has one effect. A Splitter asserts that one message in yields many out. An
Aggregator asserts a completeness condition. A Dead Letter Channel asserts that no message
is lost in silence. Those are things a rule can be written against, and they are what
separates this work from a set of architectural styles.

Five of its patterns are **properties on a message**, and that matters beyond their
number: this vocabulary supports member roles and little in it exercises them. A
correlation identifier annotated on the property that carries it is the clearest use of a
member role in the catalogue.

**The channels are admitted rather than held back.** A channel is often a configured queue
name and not a type at all, which is an argument for excluding it under ADR-0011 — but the
argument fails on inspection. Where a codebase has a typed abstraction per channel, and
that is common in .NET, the role attaches; where it has not, the pattern is simply not
annotated, which is the ordinary condition of every role rather than a defect in an entry.
Holding them back would leave the work's own vocabulary incomplete, since the routing and
endpoint patterns are defined in terms of channels — and a reader who finds Dead Letter
Channel absent cannot tell a decision from an oversight.

**Pipes and Filters and Message Broker are held here despite originating in POSA**, and
the ground is ADR-0028's own test rather than the coherence of the set. That test is
whether the work presents the pattern as one of its own — names it, describes it, gives it
a place in its own pattern language — and *Enterprise Integration Patterns* does all
three, reworked for messaging. Crediting an earlier source is scholarship, not the passing
mention ADR-0028 excludes. Coherence would be the weaker argument and a worse rule: it
would admit any pattern any work found convenient to describe.

The name drops "Patterns" as `EnterpriseApplicationArchitecture` does. `Messaging` was
considered and is shorter, is exactly what the set is about, and is the authors' own word
for it; it was set aside because these catalogues are named after works, and a reader
holding the book looks for the book.

## Alternatives Considered

### Admit only the fifty components

Considered because they are the unarguable half: every one is a class, every one licenses
an assertion, and nothing about them needs a judgement.

Rejected because the rest of the catalogue is defined in terms of what it leaves out. The
routing patterns route between channels and the endpoint patterns consume from them, so a
catalogue without channels describes half of a mechanism. And an absence with no record
reads as an oversight, which is what `catalog/README.md` exists to prevent.

### Name the catalogue `Messaging`

Considered because it is shorter, it is precisely what the sixty-five are about, it is the
word the authors use for the set on their own site, and it avoids two catalogues whose
names both begin with "Enterprise" — in the namespace, in the package identifier and in
every consumer's `using`.

Rejected because a catalogue here is named after the work that holds it, not after its
subject. `GangOfFour` is the authors and `EnterpriseApplicationArchitecture` is the title;
naming one catalogue by its topic would make the convention unpredictable for the sixth.

### Leave Pipes and Filters and Message Broker to POSA

Considered because ADR-0028 says paternity and not mention, and POSA named both seven
years earlier.

Rejected because the test is presentation rather than priority. Both are described in full
here and hold a place in this work's pattern language. If POSA is catalogued later it
holds its own entries for them, and neither catalogue refers to the other, which is the
arrangement ADR-0027 makes cheap.

### Admit nothing, and deepen what is already catalogued

Considered seriously, and it is the strongest of the four. The catalogue already holds 51
entries from *Patterns of Enterprise Application Architecture*, of which Table Module,
Transform View, Two Step View and Client Session State are as rarely named today as
anything in *Analysis Patterns*. Nothing reads the annotations yet — no analyzer, no rule
engine — so a consumer must build the payoff before getting one, and breadth does not
address that.

Rejected because this is not breadth in the same direction. These patterns are named daily
in .NET messaging codebases in a way that Table Module is not, so admitting them *is* the
move toward what is used. It also does not preclude the reader: that work is independent
of how many catalogues exist.

## Consequences

### Positive

* The first catalogue whose patterns are overwhelmingly components, so an annotation costs
  a reader nothing but the attribute.
* Member roles are finally exercised, by five patterns that are properties on a message.
* A sixth catalogue package, independent of the others, with no arbitration needed against
  the three names it shares with existing entries.

### Negative

* Sixty-five entries and sixty-five samples is the largest single admission this
  repository has made — larger than *Analysis Patterns* took over four chapters.
* Three homonyms will exist across packages with nothing saying they are unrelated. A
  consumer installing this and the enterprise catalogue sees two Gateways and must know
  which is which.
* The channels will be unannotatable in some codebases, so part of the catalogue reaches
  fewer consumers than the rest.

### Risks

* The presentation test admits a pattern another work named first. Applied loosely it
  would let any work claim anything it described well, and the guard is that the work must
  give the pattern a place in its own language — which is a judgement each time.
* If POSA is catalogued, the overlap on Pipes and Filters and Message Broker is visible in
  two packages and stated in neither, which is the accepted cost of independence and will
  still look like a defect to someone meeting it for the first time.
* This work has a tail — Control Bus, Detour, Wire Tap, Channel Purger, Test Message —
  that is used far less than its routing core. The unevenness the maintainer is trying to
  escape between catalogues could reappear inside this one.

## Follow-up Actions

* Add `EnterpriseIntegration` to the catalogue schema's enum and the generator's label map,
  without which no entry validates.
* Catalogue in the book's own order — base patterns, channels, construction, routing,
  transformation, endpoints, system management — so that a reader can follow it with the
  book open.
* Record in `catalog/README.md` what a channel's annotability depends on, and the three
  homonyms with a sentence saying why they are unrelated, since nothing in the packages
  says it.
* Decide whether the system-management tail is catalogued with the rest or held back for
  want of use.

## References

* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — the criterion
  that a pattern must license assertions, applied here to whether a work belongs at all.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — what the channels and the
  integration styles were weighed against.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) — why this is a
  catalogue and not a shelf of idioms.
* [ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.md) — the last time a work
  was admitted, and the terms it set.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) — why the three
  colliding names need no arbitration, and why a later POSA catalogue would need none
  either.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — the
  presentation test that keeps Pipes and Filters and Message Broker here.
* Hohpe and Woolf, *Enterprise Integration Patterns*, Addison-Wesley, 2003, and the
  authors' index at `enterpriseintegrationpatterns.com/patterns/messaging/`.
