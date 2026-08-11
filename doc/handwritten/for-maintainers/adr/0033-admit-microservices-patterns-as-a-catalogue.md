# ADR-0033 | Admit Microservices Patterns as a catalogue

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0033-admit-microservices-patterns-as-a-catalogue.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-10
**Accepted:** 2026-08-10
**Decision Makers:** Reefact

## Context

Seven works are catalogued — *Design Patterns* (1994), *Analysis Patterns* (1997),
*Accounting Patterns* (2000), *Patterns of Enterprise Application Architecture* (2002),
*Domain-Driven Design* (2003), *Enterprise Integration Patterns* (2003) and *xUnit Test
Patterns* (2007) — plus `Idioms`. 274 patterns, 476 roles. Six of the seven are complete.

Every one of them is at least eighteen years old. That is not an accident of taste: an old
catalogue is a settled one, and settled is what makes a vocabulary worth carving into
attributes. But it leaves the library saying nothing about the way most of its readers have
built systems for the last decade, and
[ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) stated the aim that
governs here — patterns in daily use rather than more patterns. *Saga*, *CQRS*,
*Transactional outbox*, *Circuit breaker* and *API gateway* are said in standups; nothing in
this vocabulary can hold them.

*Microservices Patterns* — Chris Richardson, Manning, 2018 — is the work, and
[microservices.io](https://microservices.io/patterns/index.html) is the same pattern
language maintained by its author, arranged in groups and kept current. The index holds
**48 patterns** across fourteen groups: Architectural style (2), Service boundaries (4),
Refactoring to services (2), Service collaboration (8), Transactional messaging (3), Testing
(3), Deployment (5), Cross-cutting concerns (3), Communication styles (4), External API (2),
Service discovery (3), Reliability (1), Observability (5), Security (1) and UI design (2).

Four facts about that list matter here.

**Roughly half is infrastructure no declaration holds.** Sidecar, Service mesh, Log
aggregation, Distributed tracing, Exception tracking, Multiple service instances per host,
Serverless deployment — these are shapes of a deployment topology, not participants a C#
type plays, which is the ground of
[ADR-0011](0011-leave-out-what-cannot-be-annotated.md). A first estimate puts the admissible
count between twenty-five and thirty, and it is an estimate: it will be settled group by
group, not by this record.

**Five names already exist in the catalog.** Anti-corruption layer and Domain event are
`DomainDrivenDesign` entries; Shared database, Remote Procedure Invocation and Messaging are
`EnterpriseIntegration` entries. [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md)
decides these one at a time and asks a single question — does *this* work present the pattern
as one of its own, or merely cite it? Nothing needs arbitrating beyond that, because each
catalogue ships as its own package
([ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md)) and two packages may
hold the same name.

**The work is more than a set of names.** Half the groups pose an explicit question —
*How to implement operations that span multiple services?*, *How to send messages as part of
a database transaction?* — and each pattern answers it with named participants: a saga has
local transactions, compensating transactions and an orchestrator; API composition has an API
composer and the services that own the data; the command-side replica has a command service,
a provider service and a replica database. Those are roles, which is what this library
annotates.

**Some entries are the author's own anti-patterns.** Shared database is presented as a
pattern in its own right and referred to from *Database per Service* as "the Shared Database
anti-pattern". [ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md)
already settles that: an anti-pattern enters on the same terms as any pattern, because saying
*this is the shape we are stuck with* is worth as much as saying *this is the shape we chose*.

## Decision

*Microservices Patterns* is admitted as a catalogue under the name `MicroservicesPatterns`,
and its patterns enter on the criteria already applied to every other work.

Where ADR-0028's question is genuinely close — the work presents the pattern, but a reader
could argue it is leaning on an earlier source — **it is answered inclusively**: the entry is
held. A developer who reaches for `[Saga]` or `[DomainEvent]` in a microservices codebase and
does not find it in the microservices package has been failed by the catalogue, whatever the
provenance argument says. Inclusion costs a duplicate name in a separate package; exclusion
costs a reader the word they came for.

## Rationale

The vocabulary is worth most where the naming is worst, which is
[ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md)'s argument and it applies
here for a second reason: microservices code is spread across repositories, so the reviewer
who would have caught a misnamed class is not reading it. `OrderSaga` might be an
orchestrator, a participant, or a class that happens to have the word in its name, and there
is no one place where the answer lives.

The assertions are the useful kind. A saga participant's local transaction must have a
compensating transaction, or the saga cannot roll back; a CQRS view is read-only and its
writer is the event handler, not the query; a service under *Database per Service* must be
the only thing that touches its schema; a command-side replica is stale by construction and
nothing that reads it may assume otherwise. Each is a rule a reviewer can hold a pull request
to, and none of them restates the annotation — the test of
[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md).

Admitting the work also lets the catalog say something it currently cannot: that these
patterns are the same ones, renamed. Richardson's Domain event is Evans's, applied to a
problem Evans did not have; his Shared database is Hohpe and Woolf's integration style seen
from the other end, as a thing to escape rather than a thing to choose. Holding both, each
under its own work's name, is what ADR-0028 is for, and this catalogue is the first that
exercises it at scale.

The size is right. Twenty-five to thirty admissible entries makes it a mid-sized catalogue,
fillable in instalments as *Enterprise Integration Patterns* and *xUnit Test Patterns* were.

## Alternatives Considered

### Keep waiting for the field to settle

Every other catalogue here is settled work. Microservices vocabulary is fifteen years old at
most, still moving, and the site itself carries entries marked *new*.

Rejected. The patterns proposed for admission are the settled part: Saga, CQRS, Event
sourcing, API composition and Database per Service have not changed meaning since 2018, and
several are older than that. What still moves is the deployment and observability half —
which ADR-0011 excludes anyway, for a different reason. Waiting protects against a risk this
catalogue's admissible half does not carry.

### Take only the data-management groups

Service collaboration and Transactional messaging: eleven patterns, the part that is about
code rather than about topology, shelved as a small catalogue of its own.

Rejected on the shape rather than the content. That is what the catalogue will amount to in
practice — but deciding it in advance would prejudge the External API, Communication styles
and Reliability groups, where Circuit breaker, API gateway and Idempotent consumer are exactly
the kind of participant a class holds. Each group is judged when it is reached, which is how
every other catalogue was filled.

### Refuse the five homonyms

Hold Domain event only under `DomainDrivenDesign`, Shared database only under
`EnterpriseIntegration`, on the grounds that a name should mean one thing.

Rejected, and this is the decision's sharpest edge. It was already refused in general by
ADR-0028; what is new is the posture stated above. A name means one thing *within a work*,
and this library indexes works. `DesignPatternCatalog.MicroservicesPatterns`
is installed by somebody building microservices, and the word they will search for is the word
their architecture diagram uses.

Two of the five were checked against ADR-0028's test before this record was written.
`shared-database.html` and `domain-event.html` are full write-ups — context, problem, solution,
related patterns — and Domain event answers a problem Evans never posed: *how does a service
publish an event when it updates its data?* Crediting DDD in the first line is scholarship, not
a citation in the sense ADR-0028 excludes.

### Admit it under `Idioms`

Shelve Saga, CQRS and the rest as individual idioms.

Rejected: [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) reserves
`Idioms` for patterns with no body of work of their own, which is the opposite of this case.

## Consequences

### Positive

* The catalog gains a vocabulary from the decade its readers are working in, which is
  ADR-0029's aim carried one step further.
* A rule can range over a distributed codebase: *every local transaction of a saga has a
  compensating transaction*, *nothing outside a service reads its schema*, *a CQRS view has
  exactly one writer*.
* ADR-0028 is exercised where it matters most — several works presenting the same pattern,
  each with its own name and its own emphasis — which is what makes the catalog readable as
  a set of works rather than as one flattened list.

### Negative

* Around half the work will be left out, and each exclusion is a judgement that must be
  recorded in `catalog/README.md` or read as an oversight.
* Five names will exist twice in the catalog, so a reader browsing
  [the index](../../../generated/catalog-index.md) will meet `DomainEvent` under two works and
  must read the package to know which is meant. That is the accepted cost of the posture
  stated in the decision.
* The pattern language is maintained on a website rather than frozen in a book, so *what
  belongs to the work* can change under the catalogue in a way it cannot for the other seven.

### Risks

* The site is the author's and the book is the author's, but they are not identical: the site
  carries entries the 2018 book does not, and a second edition is in progress. The mitigation
  is the reference field, which names the work rather than the URL, and the rule that an entry
  is added only where the site states the book covers it or the pattern predates it.

## Follow-up Actions

* Fill the catalogue in instalments, beginning with Service collaboration.
* Answer ADR-0028's question in the commit that adds each homonym, not in advance.
* Record every excluded pattern in `catalog/README.md` with the criterion it failed.

## References

* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — the aim this one
  follows: patterns in daily use rather than more patterns.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — the rule the
  homonyms are decided by, and the posture this record states for the close cases.
* [ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md) — Shared database
  as an anti-pattern enters on the same terms.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — what cannot be annotated is left
  out, which is half of this work.
* `catalog/README.md` — the entries left out and why.
