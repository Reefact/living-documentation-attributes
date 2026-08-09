# ADR-0025 | Let an earlier work reclaim a pattern from a later catalog

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0025-let-an-earlier-work-reclaim-a-pattern-from-a-later-catalog.fr.md)

**Status:** Superseded by [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md)
**Proposed:** 2026-08-08
**Accepted:** 2026-08-09
**Decision Makers:** Reefact

## Context

[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) decides
which of two catalogs holds the definition when both name the same pattern: the
earlier publication, the other declining or narrowing from it. The catalog schema
documents the reference field as load-bearing for exactly that reason rather than as
an editorial nicety.

The catalogs were opened in an order that has nothing to do with when the books were
published. *Design Patterns* is 1994, *Patterns of Enterprise Application
Architecture* 2002, *Domain-Driven Design* 2003, and *Analysis Patterns* 1997 — and
that last one was catalogued fourth, after the other three had been declared
complete.

Cataloguing its third chapter therefore required changing an entry in a completed
catalog. `EnterpriseApplicationArchitecture/Money` became a specialisation of
`AnalysisPatterns/Quantity`, because money is an amount with a unit and arithmetic
that refuses to mix units, which is what Fowler named a quantity in 1997.

Two more of the same are foreseeable from the book's contents page. Chapter 12 names
Two-Tier Architecture, Three-Tier Architecture and Presentation and Application
Logic, against `DomainDrivenDesign/LayeredArchitecture` of 2003. Chapter 13 names
Application Facade, against the facades of 2002.

A change of this kind is not confined to the catalog data. The relation is generated
as inheritance, so the attribute's identity — the type a consumer reaches by climbing
([ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md)) — becomes a
different type. A consumer grouping by identity gets a different answer for an entry
it has already read.

Nothing is released yet: the README states that the first release is still ahead.
[ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md)
holds that what a consumer reads is versioned, not only what it compiles.

## Decision

When a work catalogued later turns out to have named a pattern first, the relation is
recorded rather than avoided, and the entry that changes is the one in the catalog of
the later work.

## Rationale

ADR-0006 already decides which side holds the definition. What was not decided is
whether an entry already written may be changed to honour that, and the answer has to
be yes, because the alternative makes the catalog's answer depend on the order the
books happened to be catalogued in. That order is an accident of this repository's
history. A vocabulary in which two entries are related or unrelated according to load
order is not a vocabulary, and a reader has no way to tell which case they are looking
at.

Recording the relation the other way round — the 1997 entry declining from the 2002
one — would honour the load order instead, and would make the later presentation the
definition of the earlier pattern. That is the exact thing ADR-0006 exists to prevent,
and the reference year is in the schema so that the question is never settled by
whoever wrote first here.

What makes this cheap enough to do is that **only the relation moves**. The later
entry keeps its name, its roles, its targets and its catalog: a reader of *Patterns of
Enterprise Application Architecture* still finds `Money` under that catalog, spelled
as that book spells it, which is ADR-0006's other half. Moving the entry itself would
break that, and would be a different and worse decision.

The cost is real and it is on the consumer's side: an identity a consumer has read
changes. It is bounded here by the fact that nothing is released, so no consumer holds
the old answer today. It will not be bounded later, and that is the part worth writing
down — after the first release this class of change is a breaking change to what a
consumer reads, and ADR-0021 already says such a change is versioned rather than
slipped in.

Freezing a catalog once it is declared complete would avoid all of this and would mean
the catalog is knowingly wrong. Completeness is a statement about a book's contents —
that every pattern in it has been decided — and not a claim that nothing about those
entries can be learned from another book.

## Alternatives Considered

### Leave the completed catalog untouched and catalogue the earlier pattern as unrelated

Considered because it is the smallest change: the new entry lands, nothing that exists
moves, and no consumer is affected.

Rejected because it leaves two unrelated entries for one pattern, which is what
[ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) and
ADR-0019 exist to prevent. A consumer grouping by identity counts the pattern twice,
and a rule written for quantities does not reach money — the two things the relation
is for.

### Record the relation on the earlier entry instead, pointing at the later one

Considered because it also produces one identity, and it touches only the entry being
added rather than one already written.

Rejected because it inverts ADR-0006. The direction of the relation is not a matter of
convenience: it states which work defines the pattern, and pointing the 1997 entry at
the 2002 one asserts that *Patterns of Enterprise Application Architecture* defines a
pattern *Analysis Patterns* named five years earlier.

### Move the entry to the earlier work's catalog

Considered because it appears to follow ADR-0006 to its conclusion — if the earlier
work holds the definition, perhaps the entry belongs there.

Rejected because ADR-0006 says the opposite in its other half: a pattern is catalogued
where the work that named it put it, and *Patterns of Enterprise Application
Architecture* does name Money. A reader of that book must find it under that catalog,
spelled as that book spells it. Both halves hold at once, which is precisely what a
relation expresses and a move does not.

### Declare a catalog immune once it is complete

Considered because it would make "complete" mean something strong, and would protect
consumers from exactly the change described here.

Rejected because it buys that protection with a wrong catalog. It would also make the
protection arbitrary: whether an entry is immune would depend on whether its book was
finished before or after the book that reclaims its pattern.

## Consequences

### Positive

* The catalog's answer stops depending on the order the books were catalogued. Two
  entries are related because of what they assert and when they were published, which
  is the only account of it a reader can check.
* A rule written for the broader pattern reaches the narrower one, which is the whole
  purpose of recording a relation rather than two entries.
* The scope of the change is stated: the relation, and nothing else. That is what a
  reviewer needs to know to review one of these quickly.

### Negative

* A catalog declared complete is never final in this respect. Every catalog already
  written is exposed to any earlier work catalogued afterwards.
* Reviewing a chapter of an earlier work now means checking it against the other
  catalogs rather than only against the book. That is a heavier review and it is the
  reviewer's, not the author's — the collisions are found by knowing both books.
* After the first release, this class of change is a breaking change to what a consumer
  reads, and has to be versioned as one. The change is cheap now and will not be.

### Risks

* The temptation to declare sameness in order to tidy an overlap that is not really
  one. ADR-0007 is the guard — the assertions decide, not the names — and it has to be
  applied against the earlier work's own text rather than a summary of it.
* How many collisions remain is unknown until each chapter is read. Two are foreseeable
  from a contents page; a third that is only visible in the body of a chapter would be
  found late, after the catalog it affects had been declared complete a second time.

## Follow-up Actions

* Decide chapters 12 and 13 of *Analysis Patterns* against
  `DomainDrivenDesign/LayeredArchitecture` and the facades of the enterprise catalog,
  under ADR-0007, before cataloguing either.
* Keep the record of what has moved in `catalog/README.md`, which already carries the
  `Money` entry and what is expected next, so an absence can be told from an oversight.

## References

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) — which
  catalog holds a pattern, and which publication holds its definition. This record
  decides only what happens when the two are learned in the wrong order.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — what
  decides that two entries are one pattern at all, and the guard against tidying.
* [ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md) — how a relation
  becomes an identity, and therefore why this reaches a consumer.
* [ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md) —
  why this will be a breaking change after the first release.
* `catalog/README.md` — the `Money` move, and the collisions expected from chapters 12
  and 13.
