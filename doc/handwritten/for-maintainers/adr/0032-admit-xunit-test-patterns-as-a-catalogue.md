# ADR-0032 | Admit xUnit Test Patterns as a catalogue

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0032-admit-xunit-test-patterns-as-a-catalogue.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-10
**Decision Makers:** Reefact

## Context

Six works are catalogued — *Design Patterns* (1994), *Analysis Patterns* (1997),
*Accounting Patterns* (2000), *Patterns of Enterprise Application Architecture* (2002),
*Domain-Driven Design* (2003) and *Enterprise Integration Patterns* (2003) — plus `Idioms`
for patterns with no body of work of their own. 212 patterns, 414 roles.

*Enterprise Integration Patterns* was admitted on a stated aim: patterns in daily use
rather than more patterns ([ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md)).
It is now complete at 65, and the same aim points at test code, where the vocabulary is
used constantly and used wrongly. "Mock" is what most codebases call all five kinds of
stand-in, and a reader of `FakeClock` cannot tell whether it answers questions, records
calls, or judges.

*xUnit Test Patterns* — Meszaros, Addison-Wesley, 2007 — holds **68 patterns**, counted
from the publisher's table of contents rather than reconstructed: Test Strategy (10), xUnit
Basics (11), Fixture Setup (9), Result Verification (6), Fixture Teardown (4), Test Double
(8), Test Organization (8), Database (4), Design-for-Testability (4) and Value (4).

Three facts about that list matter here.

**The book separates its patterns from its smells itself.** Part III is *The Patterns*; the
smells — Obscure Test, Fragile Test, Assertion Roulette — live in Part II and in an appendix
of their own. No sorting is needed to keep out what
[ADR-0011](0011-leave-out-what-cannot-be-annotated.md) would exclude on the grounds of being
a defect rather than a participant.

**Not all 68 can be annotated.** Roughly ten are shapes a method body takes rather than
participants a declaration holds — Four-Phase Test, In-line Setup, In-line Teardown, Literal
Value — which is the ground on which Guard Clause is already left out. A first estimate puts
the admissible count at forty to forty-five, and it is an estimate: it will be settled entry
by entry, not by this record.

**No name collides.** The 68 were checked against the 212 catalogued names: zero exact
matches. Three near-homonyms exist — `TestStub` beside `EnterpriseApplicationArchitecture/ServiceStub`,
`CustomAssertion` beside `DomainDrivenDesign/Assertion`, and the book's test data builders
beside `GangOfFour/Builder` — none of which needs arbitration since each catalogue ships as
its own package ([ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md)).

**Object Mother is not one of the 68.** `Idioms/ObjectMother` is held under Schuh and Punke,
2001, and the question [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md)
would ask — does this work present it as its own? — is answered by the table of contents:
Meszaros does not list it among his patterns. The `Idioms` entry stands alone.

The nature question is already settled for this exact book.
[ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md) decided that a pattern of
test design enters on the same terms as any other, and it was written while cataloguing
Object Mother.

## Decision

*xUnit Test Patterns* is admitted as a catalogue under the name `XUnitTestPatterns`, and its
patterns enter on the criteria already applied to every other work.

## Rationale

The vocabulary is more useful where the naming is worse, which is ADR-0022's argument and it
applies here at scale rather than to one entry. A repository is recognised by everyone; the
difference between a stub, a spy and a mock is argued about weekly and settled nowhere. The
annotation puts the answer in the class rather than in the reviewer's head, which is what
this library is for.

The assertions are the useful kind and they are unusually sharp. A test stub supplies
indirect inputs and is never consulted afterwards; a test spy records and judges nothing; a
mock object carries expectations and fails on a call nobody wrote down; a fake object has
behaviour of its own, which makes it the only kind that can be wrong while every test using
it passes. Each of those is a rule a reviewer can hold a pull request to, and none of them
restates the annotation — the test of
[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md).

The work is a catalogue in its author's own hands: the patterns are numbered, cross-
referenced by page, and given aliases and variations in an appendix, so what belongs to it
is a matter of record rather than of reading. That is what made *Enterprise Integration
Patterns* tractable and it is true here for the same reason.

The size is right for the aim. Forty-odd admissible entries is the second-largest catalogue
here and the first that annotates test code at scale, which is where a codebase's naming
conventions are weakest and where nothing else in this vocabulary currently reaches.

## Alternatives Considered

### Catalogue only the Test Double chapter

Eight patterns, the part everyone argues about, shelved under `Idioms` or as a catalogue of
its own.

Rejected: the eight are a chapter of a book that has sixty more, and `Idioms` exists for
patterns with no body of work of their own
([ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md)) — which is the
opposite of this case. Admitting the work and filling it in instalments is what was done for
*Enterprise Integration Patterns*, and it worked.

### Admit Microservices Patterns instead

Richardson, 2018: Saga, Transactional Outbox, Circuit Breaker, API Gateway — words said as
often as these and more current.

Deferred rather than rejected. Two things count against going first: half of that catalogue
is infrastructure no C# type holds — Sidecar, Service Mesh, Log Aggregation — and much of the
rest restates *Enterprise Integration Patterns* and *Domain-Driven Design* under new names,
so ADR-0028 would duplicate a large part of what is already here. Neither is fatal; both make
it the second thing to do rather than the first.

### Leave test code out of the catalog

Consistent with the first forty-six entries, all of which describe production code.

Rejected by ADR-0022 before this record existed. Nothing states the catalog is about
production code; it is what every entry happened to be.

## Consequences

### Positive

* The five kinds of test double become distinguishable in the code, which is the argument
  this library makes about production patterns applied where the naming is worse.
* A rule can range over a test tree: *nothing outside it depends on a double*, *a stub
  carries no assertion*, *a fake is tested where the real thing is*.
* The catalogue is filled in instalments, as *Enterprise Integration Patterns* was, so each
  part is reviewable on its own.

### Negative

* Around a third of the book will be left out, and each exclusion is a judgement that must be
  recorded in `catalog/README.md` or read as an oversight — the same tail of work every
  partial catalogue has carried.
* `DependencyInjection` and `HumbleObject` are patterns of chapter 26 whose names are widely
  associated with Fowler rather than Meszaros. Whether he presents them as his own is an
  ADR-0028 question, and it is not answered here; it falls due when that chapter is
  catalogued.

### Risks

* Test code changes more often than production code, so an annotation there has more chances
  to go stale. The mitigation is the one the library already relies on: the attribute sits on
  the declaration, so it moves with it or fails to compile.

## Follow-up Actions

* Fill the catalogue in instalments, beginning with the Test Double chapter.
* Answer the ADR-0028 question for `DependencyInjection` and `HumbleObject` when chapter 26
  is reached.
* Record every excluded pattern in `catalog/README.md` with the criterion it failed.

## References

* [ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md) — a pattern of test
  design enters on the same terms as any other.
* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — the admission
  this one follows, and the aim it stated.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — what cannot be annotated is left
  out.
* `catalog/README.md` — the entries left out and why.
