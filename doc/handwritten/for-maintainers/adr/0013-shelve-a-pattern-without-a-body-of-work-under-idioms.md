# ADR-0013 | Shelve a pattern without a body of work of its own under Idioms

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

The catalog is organised by body of work, and a pattern is placed in the one that
named it (ADR-0006). Some patterns have no such body of work. Null Object has a
source — Woolf, in the third volume of *Pattern Languages of Program Design* — but
no catalog of its own; Result and a number of everyday practices have a lineage
and no single publication at all.

A namespace per source would give each of these a catalog holding one entry, and
the catalog list would fill with volumes and papers that share nothing but not
belonging elsewhere.

The root namespace was considered as their home. It carries the base attribute
and the declension marker, and is what a consumer imports first.

A source and a body of work are not the same thing. Provenance can be recorded
for a pattern that has no catalog, and the reference field carries it whether or
not a namespace bears its name.

## Decision

A pattern with a source but no body of work of its own is catalogued under
`Idioms`.

## Rationale

It keeps the organising principle intact without inventing single-entry catalogs.
Every other namespace answers *which work is this from*; `Idioms` answers *no work
in particular*, which is a real answer rather than an absence, and the reference
still records where the pattern actually came from.

The term is established for a recurring solution below the level of a published
architectural pattern, so it says something about the entries rather than merely
grouping the leftovers. A reader browsing it learns what kind of thing it holds.

It stays out of the root because the root is a lobby. Putting patterns there
would mix the vocabulary with the two types that describe the vocabulary, and it
would offer no protection anyway: a pattern that later gains a body of work moves
namespace either way, and that is a breaking change from the root just as much as
from `Idioms`.

It names the absence of a catalog, not the absence of a source. That distinction
is what lets an entry here still be placed by provenance and still be ordered
against another catalog by publication date, so the rules of ADR-0006 apply to it
unchanged.

## Alternatives Considered

### Give each source its own namespace

Considered because it applies ADR-0006 without exception and keeps provenance
visible in the namespace itself.

Rejected because it produces catalogs of one entry, and a list of namespaces that
a reader cannot use to navigate — the organising principle would survive in form
and fail in purpose.

### Put these patterns at the root

Considered because it needs no new name, and a reader importing the root sees
them immediately.

Rejected because the root holds the types that describe the vocabulary rather
than the vocabulary itself, and because it protects nothing: moving a pattern out
later breaks consumers wherever it started.

### Attach each to the closest existing catalog

Considered because it avoids a new namespace and puts every pattern near
relatives.

Rejected because it asserts a provenance that is false. Null Object under
Patterns of Enterprise Application Architecture would say Fowler named it, which
is the claim ADR-0006 exists to prevent.

## Consequences

### Positive

* Placement by provenance holds for every pattern, with one honest exception
  rather than a proliferation of single-entry catalogs.
* The name says what the entries have in common.
* Provenance is still recorded, in the reference, for patterns whose namespace
  cannot carry it.

### Negative

* `Idioms` is defined by what it is not, so its boundary is a judgement and will
  be argued per pattern.
* A pattern that later acquires a body of work has to move, which breaks
  consumers.

### Risks

* The namespace can become a default for anything hard to place, and drift from
  "no body of work" to "not researched yet". Only the requirement to record a
  reference resists that, by forcing the question of where the pattern came from.

## References

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) —
  the placement rule this completes.
* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) — Null
  Object's relation to Special Case, which lives in a catalog of its own.
