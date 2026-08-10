# ADR-0031 | Carry no generator machinery for an unused capability

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0031-carry-no-generator-machinery-for-an-unused-capability.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-10
**Accepted:** 2026-08-10
**Decision Makers:** Reefact

## Context

The generator sealed each role attribute conditionally: it held a set of roles to emit
unsealed, consulted that set when choosing every role's modifier, and never put anything
into it. Every role has therefore always been emitted `sealed`, by a branch that could not
take its other side.

The set existed for a capability the catalog does not have. A `specialisationOf` names a
pattern, never a role, so nothing can derive from a single role and nothing needs one
unsealed. [ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.md) considered
letting a relation name a role — `CommandMessageAttribute : Message.MessageAttribute`
rather than `: Message.Role` — deferred it, and left this question open behind it: is the
set dead machinery, or the seam that alternative would need?

Removing it changes no generated file. That is the fact that decides the case: the branch
had one reachable side, so deleting it and writing `sealed` outright produces byte-identical
output across all 212 patterns.

The repository's difficulty is stated in its own guide: the attributes are generated, so a
reader of the output cannot tell a decided trait from an incidental one. A conditional whose
condition is always false is the same problem inside the generator — it reads as a decision
somebody made about roles, and it is not one.

## Decision

The generator carries no machinery for a capability no catalog entry exercises; an
alternative that is deferred is re-implemented if it is ever taken.

## Rationale

A seam kept warm for a deferred alternative is a bet that the alternative arrives and that
this is the shape it needs. Both halves are doubtful here: role-targeted relations may never
be taken, and if they are, what they need is a change to the schema, the validator and the
emission of the base — of which the sealing of the target role is the smallest part and the
easiest to add. The machinery saved nothing worth the confusion it caused.

The confusion is the real cost. Nothing in the generator is tested against a case that
cannot occur, so the false branch was not merely unused but unverifiable: had it been wrong,
nothing would have said so. A future contributor taking the deferred alternative would have
had to read it, decide whether to trust it, and test it anyway — which is the whole of the
work it appeared to save.

Deleting it is safe to assert rather than to hope, because the generated output is committed:
regenerating after the removal leaves the working tree clean, which is the repository's
standing check that the catalog and the sources are in step, applied here as proof that the
branch was dead.

## Alternatives Considered

### Keep the set, and add a test that exercises it

Give the generator a test that unseals a role and checks the emission, so the branch stops
being unverifiable while the seam stays.

Rejected: it tests a capability the catalog cannot ask for, so the test asserts the
generator's behaviour on input no catalog file can produce. That is machinery guarding
machinery, and it makes the deferred alternative look decided.

### Keep it, and say in a comment that it is unused

Cheapest, and it addresses the confusion directly.

Rejected: a comment explaining why unreachable code is there is a smell the repository has
elsewhere ruled against — the ADR base exists so that reasoning lives in records rather than
in asides, and an unreachable branch with a note is still an unreachable branch.

## Consequences

### Positive

* The generator's sealing rule is now stated rather than computed: a pattern's modifier is
  conditional because a pattern can be narrowed, a role's is not because a role cannot be
  named.
* No generated file changes, so the removal is verifiable by the round-trip check the
  repository already runs.

### Negative

* If role-targeted relations are taken up, this small piece is written again. The cost is a
  few lines, and it is paid by whoever is already changing the schema and the validator for
  the same feature.

### Risks

* None identified. The removed branch could not be reached from any catalog input, and the
  generated output is committed, so a mistake would have shown as a dirty tree.

## Follow-up Actions

* None. ADR-0030's open question about this hook is discharged by this record.

## References

* [ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.md) — deferred
  role-targeted relations and left this question behind it.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) — the
  generator exists so that the shape of an attribute is written once.
