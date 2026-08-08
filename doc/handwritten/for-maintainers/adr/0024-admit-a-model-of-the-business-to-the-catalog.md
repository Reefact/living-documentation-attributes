# ADR-0024 | Admit a model of the business to the catalog

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0024-admit-a-model-of-the-business-to-the-catalog.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-08
**Accepted:** 2026-08-08
**Decision Makers:** Reefact

## Context

The three catalogs held until now are all about how code is arranged. A Gang of
Four pattern is a collaboration between classes; a pattern of enterprise
application architecture is a mechanism the application is built out of; a
Domain-Driven Design pattern is a stance taken on a declaration — this type has
identity, that one is a value.

*Analysis Patterns* is a book of something else. Its patterns are models: they say
what the business concepts are and how they relate. Party asserts that person and
organization are one thing wherever a system records who it deals with.
Accountability asserts that a responsibility is an object rather than a reference.
Post asserts that a position is a party in its own right. None of them is
a shape the code takes; each is a claim about the domain being modelled.

Their participants are therefore named entirely by the reader. Annotating a water
utility's `Subscriber` as a Party says that this class plays the part Fowler's
model gives to Party. Nothing in the declaration suggests the annotation, and
nothing in the build can contradict it.

Three criteria for admitting a pattern are already recorded: the work names it
([ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md)), a
declaration can hold it
([ADR-0011](0011-leave-out-what-cannot-be-annotated.md)), and it licenses
assertions a tool could check
([ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md)). They
have been applied twice to a question of kind rather than of content — to a
pattern of test design ([ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md))
and to an anti-pattern ([ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md))
— and in both cases the answer was that none of the three asks what kind of thing
a pattern is.

Knowledge Level is recorded in `catalog/README.md` as annotable, wanted, and
waiting: Evans presents it in *Domain-Driven Design* and points back to Fowler,
who named it, so ADR-0006 places it in a catalog this repository did not have.

The Domain-Driven Design catalog already annotates the reader's own business
classes. `Entity` and `ValueObject` are applied to `Customer` and to `Money`, not
to anything belonging to this library.

## Decision

A pattern whose content is a model of the business rather than a shape of the code
is admitted on the same three criteria as any other pattern, and its roles are the
participants of the model.

## Rationale

The criteria do not ask what kind of thing a pattern is, and this is the third
time that has settled a question of kind. Two exceptions would be a pattern; three
is a test working as intended. Adding a rule now — that a pattern must be about
code — would have to be justified by something the existing three fail to catch,
and the reverse is the case: they catch more than a rule about code would, because
what they range over is whether a claim can be checked.

The assertions a model licenses are the ordinary kind, and in places sharper than
a structural pattern's. The strongest example is the one that would be least
visible without an annotation: both ends of an accountability are parties, so
nothing in the type system distinguishes the commissioner from the responsible
party, and a model with them the wrong way round compiles, passes its tests, and
reports that a trust board answers to each of its schools. Naming the two ends is
the only place that claim exists. Knowledge Level is the same shape — the whole
pattern rests on a reference running one way, and a reference added the other way
is one line that compiles and quietly collapses the two levels into one.

That the participants are named by the reader is what the annotations are for
rather than an objection to them. The vocabulary exists because almost nothing
this repository decides is defended by the compiler; a model of the business is
simply the case where that is most true. The Domain-Driven Design catalog already
annotates the reader's classes, so the novelty is not the participant but what is
being said about it — that it means something in the business, rather than that it
has a design property.

The risk that a conceptual pattern degenerates into a label is real, and the
criteria already exclude it without help: a role that licenses no assertion does
not enter, which is ADR-0011 and ADR-0007 doing the work they were written for.
The exclusions already recorded show it holding: Responsibility Layers and Big Ball
of Mud are both models, both named, and both out — because what each asserts about a
participant is an order in one case and an absence of structure in the other, and
neither is something a rule can range over. The criterion does the work without
being told that a pattern is conceptual.

## Alternatives Considered

### Keep the catalog to patterns about code, and leave *Analysis Patterns* out

Considered because it is the boundary the first three catalogs happen to draw, and
a vocabulary about code arrangement is a coherent thing to be.

Rejected because the boundary is an accident of which books were catalogued first,
and because it would exclude a pattern this repository has already recorded as
annotable and wanted. Knowledge Level is reached through Evans, who sends the
reader to Fowler for it. A catalog that holds Evans' large-scale structures but
refuses the one he attributes elsewhere is publishing a limitation while
presenting an inventory.

### Catalogue the patterns under `DomainDrivenDesign`, where the reader met them

Considered because Knowledge Level arrives through *Domain-Driven Design* for most
readers, and putting it there is what a reader following Evans would expect.

Rejected by ADR-0006, which exists for this case: a pattern is catalogued where
the work that named it put it, and the earlier publication holds the definition.
Placing it under Evans would make the 2003 presentation the definition of a 1997
pattern, and would leave the rest of the book with nowhere to go.

### Mark a conceptual pattern as a distinct kind

Considered because the difference between "this class is a Repository" and "this
class is a Party" is real, and recording it would let a consumer treat them
differently.

Rejected because nothing would range over the distinction. It would be a property
of the catalog that no rule reads and no annotation acts on — which is what
[ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) rejects generally, and
what ADR-0007 rejects for deciding sameness. A consumer that wants only the
patterns of one work already has the catalog to ask about.

## Consequences

### Positive

* Knowledge Level can be catalogued, closing the one entry recorded as waiting on
  a decision rather than on work.
* A fourth body of work enters, and the first whose patterns are models rather
  than mechanisms — which is the kind most likely to be worth annotating, because
  it is the kind least visible in a declaration.
* The three criteria are shown to be general rather than tuned to structural
  patterns, having now settled admission questions about test design, an
  anti-pattern and a model.

### Negative

* The catalog now holds patterns whose participants are named entirely by the
  reader, so a wrong annotation is a false claim about the business that no build
  can contradict. Every other catalog has this property; here it is the whole of
  what is being said.
* A reviewer of a catalog entry has to know the domain the sample is drawn from
  well enough to tell a real assertion from a plausible one, which is a heavier
  review than checking that a role has somewhere to be applied.

### Risks

* *Analysis Patterns* holds several times more patterns than were catalogued here,
  most of them models of trading, accounting and measurement. Admitting the kind
  opens that surface, and the temptation will be to catalogue a chapter
  mechanically — nine sections, nine entries — rather than pattern by pattern
  against the criteria.
* The book's patterns overlap the enterprise application architecture catalog and
  each other more than a structural catalog's do, so sameness questions
  (ADR-0007) will be frequent rather than exceptional. Three arose in chapter 2
  alone.

## Follow-up Actions

* Catalogue the rest of the book chapter by chapter, deciding each pattern against
  the criteria rather than by its chapter's membership.
* Settle the three patterns of chapter 2 left undecided here — Organization
  Hierarchies, Organization Structure and Party Type Generalizations — which are
  recorded in `catalog/README.md` with what each is waiting on.
* Revisit `Result` and `Option`, held back in `catalog/README.md` for want of a
  publication naming them as patterns. That search was made without access to the
  sources; it deserves a second attempt now that one exists.

## References

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) —
  why the entries are here rather than under Evans, and what makes the reference
  load-bearing.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — the
  criterion that admits a pattern and the one that decides two are the same.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — the other admission
  criterion, and the one three patterns of chapter 2 turn on.
* [ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md) and
  [ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md) — the
  two earlier questions of kind, settled the same way.
* `catalog/README.md` — where Knowledge Level was recorded as waiting, and where
  the undecided patterns of chapter 2 are recorded now.
