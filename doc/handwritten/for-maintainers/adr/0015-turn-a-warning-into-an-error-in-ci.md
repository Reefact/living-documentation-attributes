# ADR-0015 | Turn a warning into an error in CI

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0015-turn-a-warning-into-an-error-in-ci.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

The solution builds with zero warnings across all six target frameworks.

Almost everything it compiles is generated from one template, so a warning
introduced there does not appear once: it appears in every pattern of the
catalog, which is heading for several hundred. A build log carrying hundreds of
identical warnings is a log nobody reads, and the first genuine warning after
that is invisible.

Warning counts do not stay put. They only ever move in one direction unless
something holds them, because each individual warning is defensible at the moment
it is added and no single one is worth blocking a change for.

The library ships to consumers who compile against it. A warning in a public
attribute's declaration — an obsolete usage, a nullability mismatch, a
documentation reference that does not resolve — reaches their build, not only
ours.

## Decision

A warning fails the build in CI, and stays a warning locally.

## Rationale

Ratcheting from zero is the only moment this is free. Every warning that exists
today would have to be triaged first if the state had already drifted; from zero
there is nothing to clean, and the decision costs nothing to adopt.

The generated-code multiplier is what makes it worth more here than in an
ordinary repository. A warning that would be an annoyance in one hand-written
file becomes several hundred identical lines in a generated catalog, so the
signal degrades much faster and recovers much more slowly.

Keeping it advisory locally protects the inner loop. A half-finished change
should be buildable and runnable while it is half-finished; making the ratchet
bite at the pull-request boundary gets the guarantee without making iteration
hostile.

Both switches are needed because they cover different producers: one promotes
compiler diagnostics, the other promotes warnings raised by build tasks. With
only the first, a task-emitted warning still merges unnoticed — which is the
failure mode this exists to remove, arriving through a door left open.

Security advisories are excluded deliberately. An advisory published overnight
against a dependency would otherwise turn every pull request red without anything
having changed in the repository, which trains contributors to ignore a red
build. The advisory still appears in the log, and acting on it is a change of its
own rather than a blockage of unrelated work.

## Alternatives Considered

### Fail on warnings everywhere, including local builds

Considered because it is simpler to explain, and it removes any window in which a
warning exists.

Rejected because it makes iteration hostile: a temporarily unused variable in a
half-written change stops the build, so the friction lands on the moment of
thinking rather than the moment of proposing.

### Leave warnings as warnings and rely on review

Considered because the current count is zero and the team is small.

Rejected because a reviewer reading a diff sees the code, not the build log, and
a warning introduced in a template is not visible in the diff at all — it is
visible in several hundred files nobody opens.

### Promote only the compiler switch

Considered because it is the well-known one and covers the diagnostics people
think of.

Rejected because it silently leaves the SDK and pack warnings unratcheted, so the
guarantee would be narrower than it reads.

## Consequences

### Positive

* The zero-warning state is locked in rather than maintained by attention.
* A warning introduced through the template is caught once, at the boundary,
  rather than multiplied across the catalog.
* Consumers do not inherit a warning from a shipped declaration.

### Negative

* A change that is legitimate but warns must be resolved — suppressed with a
  reason, or fixed — before it can merge, even when the warning is not the point
  of the change.
* The local and CI builds behave differently, which surprises anyone who has not
  read this.

### Risks

* The pressure to silence a warning quickly can produce a blanket suppression
  rather than a fix. Nothing here prevents that; a suppression is a change like
  any other and is reviewed as one.

## References

* `Directory.Build.props` — where the ratchet is wired.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  why a defect in the template reaches the whole catalog at once.
