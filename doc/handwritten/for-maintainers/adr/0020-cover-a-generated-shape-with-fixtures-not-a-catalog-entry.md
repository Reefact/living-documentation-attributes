# ADR-0020 | Cover a generated shape with fixtures, not with a catalog entry

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0020-cover-a-generated-shape-with-fixtures-not-a-catalog-entry.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-06
**Decision Makers:** Reefact

## Context

The generator emits four shapes: a flat attribute, a container of roles, a
declension, a specialisation. The catalog uses two of them. One entry — Evans'
value object narrowing Fowler's — uses a third, in its flat form only. No entry
declines anything, and none relates a multi-role pattern
([ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md)).

A shape nothing uses is a shape nothing checks. The rule that reads it, the
branch that emits it and the sample that would exercise it are all absent from
every run, and the defect ADR-0019 corrected — a specialisation absorbed into the
pattern it narrows — was of exactly that kind: it compiled, it produced a
plausible count, and it was one pattern short.

What a pattern must satisfy to enter the catalog is settled and is not about
tooling. It carries verifiable assertions
([ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md)), it can
be attached to something ([ADR-0011](0011-leave-out-what-cannot-be-annotated.md)),
and it comes from a body of work that named it
([ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md)).
ADR-0011 refused a marker type whose only reason to exist was to be annotated, on
the grounds that the code being documented must gain no artefact from the
documentation system.

The samples are not available either. They are a teaching artefact: one realistic
business example per pattern, in a domain chosen to fit
([ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md)), and their
inventory is a measurement — the count means something only because every codebase
annotates the same way
([ADR-0010](0010-annotate-the-declaration-that-introduces-a-role.md)).

Convention tests were deferred three times and are now written, which is what puts
the question. They need something to read.

## Decision

A shape the generator can emit is covered by fixtures declared in the test
project, and never by a catalog entry written in order to be tested.

## Rationale

The catalog answers a question about design patterns, and coverage is not part of
it. An entry added because the generator has an untested branch would be a claim
that a pattern exists, made in order to satisfy a tool — which is ADR-0011's
rejected marker type wearing another hat, and it would ship. Fixtures make the
same shapes readable without asserting anything about any pattern, and nothing
about them leaves the test assembly.

Keeping them out of the samples protects a measurement. The inventory is what
proves the catalog reads back, and its numbers are meaningful because they count
real annotations on realistic code; a fixture pattern would inflate them, and the
sample suite would teach a pattern that does not exist.

Declaring them by hand is what makes them independent of the thing they check. A
fixture written from the template is a second statement of the shape, so a test
over it fails when the reading rules stop agreeing with it — which is the failure
being guarded. That independence is also the limit, and it belongs in the open:
these fixtures prove the rules hold over the shapes, not that the generator still
emits them. The round trip proves that the sources are what the catalog produces;
neither check subsumes the other, and the pair is what covers a shape in use.

For a shape nothing uses, the pair is incomplete, and that is accepted rather than
hidden. The template could change how it emits a declension while the fixtures
keep passing, because no generated declension exists to disagree with them. The
alternative that closes it is real and is recorded below; it costs machinery
proportionate to the two shapes it would cover, and that trade is worth revisiting
when the catalog stops making it hypothetical.

## Alternatives Considered

### Add a catalog entry that exercises the shape

Considered because it is the shortest path, and because the entry would be
generated, sampled and read back exactly like every other — the coverage would be
complete rather than partial.

Rejected because it corrupts the answer the catalog gives. The entry would be a
pattern the literature does not name, present because a branch was untested, and
it would ship to consumers as part of the vocabulary. ADR-0011 refused a marker
type for the same reason: nothing exists in the documented artefact for the
convenience of the documentation system.

### Generate the fixtures from a fixture catalog

Considered because it removes the limit above, and it is the strongest of the
alternatives. A second, test-only catalog run through the same generator into the
test project would prove that what the template emits — not only what the rules
read — is right for every shape, including the ones no pattern uses.

Rejected as disproportionate today, not as wrong. It asks the generator for an
output path and an input path it has no other use for, adds a second generated
tree that the round trip must then cover or watch drift, and does all of it for
two shapes that no catalog entry needs yet. It becomes the better trade as soon as
either the number of unused shapes grows or the first real relation stays absent,
which is why it is a follow-up rather than a rejection.

### Put the fixtures in the sample project

Considered because the reader already runs there, and the inventory would pick
them up with no new project at all.

Rejected because the samples teach and the inventory measures. A fixture pattern
would appear in an inventory that is meant to count real annotations, and a reader
opening the sample directory would find a pattern that belongs to no body of work,
in a project whose every other file is a realistic business example.

### Wait for the first real entry that uses the shape

Considered because the coverage would then come for free, through the ordinary
machinery, with nothing invented.

Rejected because it leaves the interval unguarded, and the interval is where the
defect lives. ADR-0019 shipped a rule change whose two new shapes nothing
exercised; waiting means the next such change is verified once, by hand, by
whoever makes it — which is how the absorbed specialisation survived in the first
place.

## Consequences

### Positive

* A capability can be covered before the catalog needs it, so a shape is never
  emitted with nothing reading it.
* The catalog keeps answering only about patterns, and the sample inventory keeps
  counting only real annotations.
* The fixtures state the shapes compactly, in one file, where a reader can compare
  all four.

### Negative

* The fixtures are a hand copy of the template, so adding a shape means writing it
  twice — once in the generator, once here.
* A shape no catalog entry uses is covered on the reading side only.

### Risks

* The fixtures can drift from the template silently: for a shape in use, the
  convention tests over the shipped catalog catch it; for a shape not in use,
  nothing does.
* Fixtures are cheap, so they invite covering hypothetical shapes the generator
  cannot actually emit. Only the discipline of adding one alongside a real
  generator change resists that.

## Follow-up Actions

* Generate the fixtures from a fixture catalog if the unused shapes multiply, or
  if no real multi-role relation is catalogued — it is the alternative that closes
  the remaining gap.
* Delete a fixture when a catalog entry covers its shape through the ordinary
  machinery, so that the fixture set stays the set of things nothing else reaches.

## References

* [ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md) — the shapes
  this covers, and the defect that showed why they need covering.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — the same refusal,
  applied to what may enter the catalog.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md) — why the
  samples cannot serve as fixtures.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.md) — the round
  trip, which is the half of the pair these fixtures do not provide.
