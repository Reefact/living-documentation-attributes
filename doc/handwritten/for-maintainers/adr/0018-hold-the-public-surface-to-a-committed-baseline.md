# ADR-0018 | Hold the public surface to a committed baseline

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0018-hold-the-public-surface-to-a-committed-baseline.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

This library ships public types and nothing else. There is no implementation
behind them, so its public surface is not one aspect of the product — it is the
whole of it, and every change to it is a change to what consumers depend on.

The surface is generated. A change to the template alters every pattern at once,
and the diff it produces spans the whole catalog, which is where a reviewer's
attention is least able to notice that one role lost a property or that a base
class changed.

ADR-0003 states that adding a role to a published pattern is additive, and that
this is one of the reasons the enumerations were dropped. Nothing verifies it.
The same decision leaves the converse unguarded: removing a role, renaming one,
or narrowing what it applies to are breaking changes that produce a green build.

The catalog is expected to grow by an order of magnitude, and much of that growth
will be authored in bulk. A surface that grows by several hundred types under
review by reading is a surface nobody is really reviewing.

The library multi-targets six frameworks, and the attributes are the same on all
six.

## Decision

The public surface is declared in a committed baseline that every build checks,
and a change to it fails until the same change updates the baseline.

## Rationale

It turns the surface into a reviewed diff, which is the only form in which it can
actually be reviewed. A pull request that adds a pattern shows a few dozen lines
of baseline alongside the generated files, and those lines are readable in a way
that the generated code is not — they are exactly the public names, with nothing
else around them.

It puts a guard on the claim ADR-0003 makes rather than leaving it as an
intention. Adding a role now appends to the baseline and nothing breaks;
removing, renaming or narrowing one raises a diagnostic naming the symbol. The
promise and its enforcement stop being separate things.

One baseline shared by all six frameworks, rather than one per framework, states
that the surface is meant to be identical everywhere. A per-framework baseline
would let two targets drift apart and absorb the difference silently; a shared
one makes that a failure, which is a guarantee gained rather than a cost paid.

**The generator must not write the baseline.** It would then always agree with
itself, and the check would confirm only that the generator is deterministic —
which the round trip already proves. The baseline is updated by a deliberate act
by whoever changes the surface, and that act is the review.

Warning locally and error in CI follows the ratchet already in place: a
half-finished change may leave the baseline stale while it is being shaped, and
meets the guard on the way in.

Everything sits in the unshipped file today because nothing has been published.
The distinction earns its keep at the first release, when the accumulated entries
are promoted and the shipped file becomes the record of what consumers were
actually given.

## Alternatives Considered

### Review the surface by reading the generated diff

Considered because the generated files are committed and appear in every pull
request, so the information is already in front of the reviewer.

Rejected because it is in front of them among several hundred other files that
changed for the same reason. The failure this guards against — a property lost, a
base class changed, a target narrowed — is invisible at that scale, which is
precisely when it is most likely to happen.

### Have the generator emit the baseline alongside the sources

Considered because it would remove the chore: adding a pattern would update the
baseline automatically, and the two could never disagree.

Rejected because "could never disagree" is the whole problem. A baseline written
by the thing it checks confirms nothing, and the change it would have caught — a
template change that alters every role's surface — would simply rewrite the
baseline to match. The chore is the mechanism.

### Rely on package validation against a published version

Considered because it checks compatibility against what consumers actually have,
which is the question that ultimately matters.

Rejected as unavailable rather than wrong: it needs a published baseline version,
and this library has no packaging metadata at all yet. It is complementary and
worth adding once there is something to compare against.

### Write reflection tests asserting the shape of every generated attribute

Considered because such tests could check the base class, the targets and the
multiplicity of every role, which the baseline does not.

Rejected as answering a different question, and deferred rather than refused.
Those tests would check that the generator did what it was told; the baseline
checks what consumers can see. Both are worth having, and neither replaces the
other.

## Consequences

### Positive

* Every change to what consumers depend on is a small, readable diff.
* A removed or renamed role fails the build instead of shipping.
* The six targets are held to one surface, so they cannot drift apart.
* The set of public types is a question with a committed answer.

### Negative

* Adding a pattern now has a fourth step, and forgetting it means a red build.
* The baseline is large and grows with the catalog, so a pull request that adds a
  catalog carries a correspondingly large baseline diff.

### Risks

* The update is mechanical — a tool appends the entries — so it can be applied
  without being read, which would make the diff a formality rather than a review.
  Nothing here prevents that beyond the diff being small enough to read.
* The baseline records what is public, not whether it should be. A wrong type,
  correctly declared, passes.

## Follow-up Actions

* Promote the unshipped entries to the shipped file at the first release, and
  turn on package validation against that version once there is one.
* Add reflection-based convention tests over the generated attributes, which this
  decision defers rather than rejects — as does ADR-0016.

## References

* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) — the
  claim that adding a role is additive, which this guards.
* [ADR-0015](0015-turn-a-warning-into-an-error-in-ci.md) — the ratchet that makes
  the diagnostics blocking on the way in.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.md) — the
  round trip, which proves determinism and therefore cannot also prove this.
* `CONTRIBUTING.md` — how a surface change is accepted.
