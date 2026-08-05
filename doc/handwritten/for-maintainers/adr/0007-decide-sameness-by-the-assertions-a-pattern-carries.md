# ADR-0007 | Decide that two patterns are the same by the assertions they carry

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

Whether two entries are one pattern, two related patterns, or two unrelated
patterns that share a name decides how they are catalogued (ADR-0006), how they
are related in the code (ADR-0005), and whether a consumer counts them once or
twice (ADR-0005). It is the question everything else depends on, and it is asked
again for every catalog added.

Names answer it badly in both directions. *Adapter* and *Command* each name two
patterns that have nothing in common, so identical names do not imply identity.
*Null Object* and *Special Case* name closely related patterns, so different
names do not imply difference.

An audit by name alone, over the catalogs planned, produced nine apparent
duplicates. Examined one by one, seven were not duplicates at all — the name had
been the whole of the evidence.

The library exists so that an annotation can be checked. The value of a role is
the assertion it lets someone state and a tool verify; a role nothing can be
asserted about carries no information a reader did not already have.

Value Object reads as one pattern held by two works. Fowler's is about
comparison — equality not based on identity — and tolerates a mutable date range.
Evans' adds immutability and, above all, is a modelling decision: it exists
because it says something about the domain. A rule written for one does not hold
for the other, which was demonstrated rather than argued: a mutable date range
passes Fowler's rule and fails Evans'.

## Decision

Two entries are the same pattern when they carry the same verifiable assertions,
and neither the name nor the informal description settles it.

## Rationale

The criterion is the one thing that does not vary with vocabulary. Two authors
describing one idea in different words carry the same assertions; two authors
using one word for different ideas do not, and asking what could be checked
separates them where reading the prose does not.

It answers the question in the terms the library is for. A pattern here exists so
that something can be asserted about a participant, so two patterns are the same
exactly when they license the same assertions — the criterion is not a proxy for
sameness, it is what sameness means in this catalog.

It is testable rather than editorial. The Value Object case was settled by writing
the two rules and running them: the mutable date range passing one and failing the
other is evidence, where a comparison of two definitions in prose would have been
an opinion. A contributor unsure whether two entries are one can do the same.

It scales to catalogs nobody here has read closely. Judging sameness by
familiarity does not survive Enterprise Integration Patterns or the concurrency
literature; asking what rule each entry would license is a question a contributor
can answer from the source text without being steeped in the field.

It also settles which of the two relations of ADR-0005 applies, and does so
before publication order is consulted. Value Object reads as one pattern held by
two works until the assertions are written down; once they are, it is two
patterns in an inclusion, and the question of which work published first never
arises — inclusion orders itself.

## Alternatives Considered

### Treat identical names as identical patterns

Considered because it needs no judgement, and it is what an automated pass would
do.

Rejected on the two Adapters and the two Commands: it merges patterns that share
nothing, silently, and a consumer would count them together with no sign that
anything was wrong.

### Compare the authors' definitions in prose

Considered because it is what the sources actually offer, and it is how a person
reads a catalog.

Rejected because prose from different decades and different communities describes
one idea in incompatible language and different ideas in identical language.
Value Object read as the same pattern in both books until the rules were written
down.

### Ask whether practitioners consider them the same

Considered because adoption is what the vocabulary serves.

Rejected because it varies by community and by decade, so the answer would need
revisiting, and because it is unanswerable for the parts of the catalog with few
practitioners.

## Consequences

### Positive

* Homonyms stay apart and synonyms come together, for a reason that can be
  stated.
* The question can be settled by evidence rather than by seniority.
* The criterion is the same one that decides whether a pattern belongs in the
  vocabulary at all, so one judgement serves both.

### Negative

* It demands more of a contributor than reading a definition: they must imagine
  the rule each entry would license.
* Entries whose assertions are hard to pin down are hard to classify, and the
  criterion offers no shortcut for them.

### Risks

* A rule can be imagined narrowly or generously, so two contributors can reach
  different answers about the same pair. Mitigated by writing the rules down in
  the pull request rather than asserting the conclusion.
* Applying the criterion to catalogs already published may reclassify entries and
  move a canonical identity, which is a breaking change for consumers grouping by
  it.

## Follow-up Actions

* Apply the criterion to the apparent duplicates identified across the planned
  catalogs before those catalogs are generated, rather than after.

## References

* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) — what
  sameness decides for a consumer, and the two relations this criterion chooses
  between.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — the same criterion,
  applied to whether a pattern belongs here at all.
