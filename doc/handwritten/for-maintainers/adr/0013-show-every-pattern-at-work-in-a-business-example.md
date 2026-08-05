# ADR-0013 | Show every pattern at work in a business example

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0013-show-every-pattern-at-work-in-a-business-example.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

The library ships attributes and nothing else. An attribute cannot be exercised
by a unit test in any meaningful sense — there is no behaviour to assert — so the
usual proof that a library works does not apply here.

Two things nevertheless need proving. That every role can actually be applied to
something plausible: a target set that is too narrow is only discovered when
someone tries. And that the whole catalog can be read back generically, which is
the contract of ADR-0004 and is otherwise only a claim.

The reading rules live in documentation rather than in code (ADR-0004), so
something has to state them executably or they are unverified prose.

The vocabulary is also meant to teach. The role descriptions carry what a
participant does, and they are the main reason a reader chooses one role over
another; but a description in isolation does not show why a pattern was reached
for, or when it would be the wrong choice.

Textbook examples make poor teaching material here. An expression tree
demonstrates the mechanics of Visitor and says nothing about when a team should
use it, which is the part that is actually hard.

## Decision

Every pattern is exercised by one sample file that annotates a realistic business
example, documented so that the example explains the pattern.

## Rationale

Compiling the samples is the only check available. A role that cannot be applied
to anything sensible fails to compile in the sample, which turns the target sets
of ADR-0010 from declarations into something exercised — and it is the sample
suite, not a test suite, that caught the absence of `Struct` as a target.

A generic reader running over the samples verifies the claim of ADR-0004 end to
end: it walks the whole catalog through the base attribute alone, and its output
is what a consumer would get. Because it applies the documented reading rules, it
is also their executable statement, which is what keeps documented conventions
from drifting.

Realistic business examples carry what a description cannot. A pattern is chosen
because of a property of a situation — the cargo kinds are fixed by regulation
while the calculations over them keep arriving — and that property is what a
reader needs in order to recognise the situation in their own work. A textbook
example has no situation.

Varying the domains serves the same end from the other side. A reader who works
in agriculture and only ever meets banking examples learns the mechanics and not
the recognition; spreading the samples across cattle farming, logistics,
mathematics, freight, finance and the rest gives more readers a situation they
know. The domain is chosen to fit the pattern rather than the reverse — a pattern
forced into a domain it does not suit teaches the wrong lesson twice.

One file per pattern makes the correspondence with the catalog navigable, and
makes the sample the obvious place to look when a role's description is not
enough.

## Alternatives Considered

### Write conventional unit tests over the attributes

Considered because it is what a library normally ships, and reflection could
assert the structure of every generated type.

Rejected as insufficient rather than wrong: such tests would check that the
generator did what it was told, not that what it was told is usable. Whether a
role can be applied to a plausible participant is only answered by applying it.
Convention tests remain worth adding, and would complement this rather than
replace it.

### Write one minimal example per pattern

Considered because it would be far less work and would compile just as well.

Rejected because it proves applicability and teaches nothing. The description in
the attribute already states what a role is; a sample that adds only syntax
duplicates it.

### Use one coherent domain for the whole catalog

Considered because a single running example would let patterns build on each
other, and would read as one system.

Rejected because two hundred patterns do not fit one domain without distortion,
and the distortion is exactly what misleads: a pattern bent to fit teaches that
it applies where it does not.

## Consequences

### Positive

* Every role is proven applicable, in the only way available.
* The generic reading contract is verified rather than asserted.
* The documented reading rules have an executable counterpart.
* A reader learns when to reach for a pattern, not only how to spell it.

### Negative

* The samples are a large body of hand-written code — of the order of the library
  itself — and it grows with the catalog.
* Writing a realistic example demands domain knowledge the author may not have,
  and a wrong one teaches something false about the domain.

### Risks

* Samples can drift from the catalog: a pattern added without its sample is only
  caught by review.
* A sample that is realistic but wrong about its domain is worse than a neutral
  one, and nothing in the repository can detect that.

## Follow-up Actions

* Name each sample after its pattern, so that a missing one is visible by
  comparing two directories.

## References

* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — the reading
  contract the sample reader verifies.
* [ADR-0010](0010-let-each-role-declare-what-it-applies-to.md) — the target sets
  the samples exercise.
* [ADR-0011](0011-annotate-the-declaration-that-introduces-a-role.md) — the
  convention the samples demonstrate.
