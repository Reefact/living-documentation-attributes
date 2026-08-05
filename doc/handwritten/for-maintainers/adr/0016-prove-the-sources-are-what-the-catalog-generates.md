# ADR-0016 | Prove on every pull request that the sources are what the catalog generates

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0016-prove-the-sources-are-what-the-catalog-generates.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

The attributes carry no behaviour. There is nothing to unit test, nothing to
measure coverage of, and nothing for mutation testing to mutate — the usual
instruments have no purchase here.

Three claims are nevertheless made elsewhere in this base, and nothing checks any
of them.

ADR-0002 states that the attribute sources are generated from the catalog and
that regenerating an unchanged catalog leaves the working tree clean. That
invariant is the whole of what stands between the repository and a generated
attribute quietly edited by hand — which would survive review, since a plausible
edit to a generated file looks exactly like a generated one.

ADR-0002 also rests on the schema to make a catalog written in bulk reviewable: a
missing role, an unknown target or a link to a role that does not exist should be
a failure rather than something to notice by reading. The schema exists and
nothing runs it.

ADR-0004 claims the whole catalog can be read back through the base attribute
alone. The sample project demonstrates it, and `AGENTS.md` already tells a
contributor that its inventory is the check that a catalog change landed — by
hand, on their own initiative.

The library multi-targets six frameworks, so a change that compiles on the newest
may not compile on the oldest.

## Decision

Every pull request builds the solution on both platforms and proves that the
catalog is valid, that it reads back, and that the committed sources are exactly
what regenerating it produces.

## Rationale

These four checks are what this repository can prove, and each maps to a claim
that is otherwise only asserted. That is the criterion for including them: not
that they are the usual checks, but that a stated invariant would otherwise go
unverified.

The round trip is the one that matters most, because the failure it catches is
invisible to every other instrument. A hand-edited generated file compiles,
passes any test, and reads correctly; only regenerating and comparing reveals it.
It is also nearly free, since the generator is a script over a few dozen small
files.

Compiling the sample project is the closest thing to a test the vocabulary has. A
role whose declared targets are too narrow cannot be applied to a plausible
participant, and that fails to compile — which is how the absence of `Struct` as
a target was found. Running it afterwards proves the annotations are not only
writable but readable, through the base attribute alone.

Positive proof rather than a zero exit status. A sample project that silently
annotated nothing would exit cleanly, so the step reads the inventory line and
fails on an empty one — the check is that something was found, not that nothing
crashed.

Both platforms because path handling and line endings differ, and the generator
writes files. A round trip that holds on one and not the other is a real defect
and would otherwise be found by whoever next runs the generator on the other.

The usual instruments are deliberately absent. Coverage over a library with no
executable statements reports a number that means nothing, and a mutation score
over the same is worse — a green figure that no one can act on erodes trust in
every other figure beside it.

## Alternatives Considered

### Add a unit test project with reflection-based convention tests

Considered because it is the conventional shape, and such tests would catch a
generated attribute that lost its base class or its target declaration.

Rejected as insufficient rather than wrong, and deferred rather than refused:
convention tests check that the generator did what it was told, where the round
trip checks that what is committed is what it produces — a strictly larger
guarantee for less machinery. They remain worth adding on their own merits.

### Trust the contributor to regenerate

Considered because `AGENTS.md` already says to, and the sequence is three
commands.

Rejected because the failure is silent by construction. A contributor who forgets
sees nothing wrong, a reviewer sees a plausible diff, and the divergence is found
much later by someone whose regeneration produces an unrelated change they did
not make.

### Run the checks on one platform only

Considered because the build is platform-independent in principle and the second
leg doubles the cost of a cheap job.

Rejected because the generator writes files and the repository is edited from
Windows, so line endings and path handling are exactly the kind of thing that
differs — and a round trip that holds on one platform and not the other is the
failure this decision exists to catch.

## Consequences

### Positive

* A hand-edited generated attribute cannot merge.
* An invalid catalog entry fails before it is generated from.
* The claim that the catalog reads back generically is verified on every change,
  not asserted.
* Every supported framework is compiled on every change.

### Negative

* Adding a pattern means running the generator before pushing, or meeting a red
  build.
* The catalog job depends on Python and one pinned package, so the repository's
  CI needs a toolchain its consumers never do.

### Risks

* The round trip proves the sources match the catalog, not that either is right.
  A wrong pattern, correctly generated, passes every check here.
* A generator made non-deterministic — by ordering, by locale, by a timestamp —
  would fail the round trip for a reason unrelated to the change under review.
  Nothing prevents that beyond the generator being small enough to read.

## Follow-up Actions

* Add reflection-based convention tests over the generated attributes, which this
  decision defers rather than rejects.

## References

* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  the invariant the round trip verifies.
* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — the reading
  contract the sample project exercises.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md) — why the
  samples are the only test the vocabulary has.
