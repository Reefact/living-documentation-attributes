# ADR-0022 | Admit a pattern of test design to the catalog

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0022-admit-a-pattern-of-test-design-to-the-catalog.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-06
**Decision Makers:** Reefact

## Context

Every one of the forty-six patterns catalogued before this one describes production
code. The Gang of Four catalog, the tactical and strategic patterns of
Domain-Driven Design, and the entries taken from Patterns of Enterprise Application
Architecture all describe how a system is built, never how it is tested.

Object Mother is not. Named by Schuh and Punke at XP Universe in 2001, it describes
a class that builds fully formed objects for tests, so that a test states what
matters about its data and nothing else. It has a source, it has no body of work of
its own, and it is held by a class — so it satisfies every criterion the catalog
already applies
([ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md),
[ADR-0011](0011-leave-out-what-cannot-be-annotated.md),
[ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md)).

Nothing states that the catalog is about production code. It is simply what every
entry so far happens to be, which is the kind of trait a reader of the output
cannot tell from a decision ([ADR-0001](0001-check-every-pull-request-against-the-adr-base.md)).

What a pattern must carry to belong is settled: verifiable assertions about a
participant ([ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md)).
Object Mother licenses several — every method returns an object already valid, the
methods are named for situations rather than for shapes, and nothing outside the
test tree depends on it.

Test code is code a consumer writes, compiles and reviews, and the attributes carry
no behaviour and no dependency, so nothing about annotating a test differs
mechanically from annotating anything else.

## Decision

A pattern of test design enters the catalog on the same terms as any other, and the
catalog is not restricted to patterns of production code.

## Rationale

The admission criteria already answer the question, and they do not mention what
kind of code a pattern describes. A rule that excluded test patterns would be a new
restriction, and there is nothing to justify it beyond the accident that nobody had
proposed one yet.

The vocabulary is more useful where the naming is worse. A repository is recognised
by everyone; a class that builds test objects is called a factory, a helper, a
builder or a fixture depending on who wrote it, and the pattern it actually
implements is invisible. That is precisely the gap an annotation closes.

The assertions are real and they are the useful kind. *Nothing outside the test tree
depends on an object mother* is a dependency rule a build can check. *Every method
returns a valid object* is a convention a reviewer can hold a pull request to.
Neither restates the annotation.

Keeping it out would cost more than it saves. The alternative is a rule about
categories of code, applied at the boundary of every future proposal — is a test
double a test pattern, is a fixture, is a builder used by both — where the existing
criteria decide each case on what it carries.

The samples make the distinction visible without a rule. A sample for a test pattern
is test-shaped code, in a project of business examples, and a reader meets it as
what it is.

## Alternatives Considered

### Restrict the catalog to production code

Considered because it is what the catalog is today, because it keeps the vocabulary
about the design of a system rather than about the design of its tests, and because
a consumer counting patterns would not have a test helper appear among its
aggregates.

Rejected because the restriction would have to be invented, and it answers a
question — *what kind of code is this?* — that the admission criteria deliberately
do not ask. Grouping is a consumer's concern and the catalog already gives it the
means: an annotation carries its catalog, and a consumer that wants production
patterns only can say so.

### Put test patterns in a catalog of their own

Considered because it would let a consumer take one and not the other, and because
the boundary would then be explicit rather than implied.

Rejected because a catalog names a body of work
([ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md)), not a
category of code. A `Testing` namespace would be the first catalog in the library
organised by subject, and it would immediately face patterns that belong to both —
a builder is a Gang of Four pattern and a test-data idiom, and it cannot be in two
namespaces.

### Admit it, and mark test patterns with a flag on the entry

Considered because it would let a consumer filter without inventing a catalog.

Rejected because it states in data what the pattern's own description already says,
and it would have to be decided for all forty-six existing entries — several of
which are genuinely used on both sides. It is also a field added for a consumer that
does not exist yet.

## Consequences

### Positive

* A codebase can annotate its test design with the same vocabulary as the rest,
  where naming is at its most inconsistent.
* The criteria for entering the catalog stay the ones already written down, and are
  not joined by a rule about categories.
* An assertion like *nothing outside the test tree depends on this* becomes
  checkable.

### Negative

* A consumer that counts patterns gets test patterns mixed in with the rest unless
  it filters, and nothing signals the difference except the entry's description.
* The sample project gains code that is test-shaped without being a test, which
  reads oddly beside a farm, a railway and an insurer.

### Risks

* The boundary is judgement, and the next candidate will be harder: a test double, a
  fixture, a builder used on both sides. The criteria decide it, but "does this carry
  verifiable assertions" is a slower question than "is this production code".
* A catalog of test patterns could grow to dominate a namespace shared with
  everything else that has no body of work, which would make `Idioms` a mixture
  rather than a shelf.

## Follow-up Actions

* Revisit the shape of `Idioms` if patterns of test design accumulate there — a
  mixture of unrelated things is what ADR-0013 warned the namespace could become.

## References

* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) — the
  shelf this entry lands on, and its risk of becoming a default.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — the
  criterion that admits it.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — the criterion it also
  satisfies, being held by a class.
* `catalog/Idioms/ObjectMother.json` — the entry.
