# ADR-0012 | Leave out of the catalog what cannot be annotated

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0012-leave-out-what-cannot-be-annotated.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

Not everything the literature calls a pattern can be attached to something C#
lets an attribute reach. A *Module* in Domain-Driven Design qualifies a
namespace, and C# has no namespace-level attribute. A *Conformist* relationship
qualifies the link between two bounded contexts, which is neither a type, a
member nor an assembly.

Workarounds exist. A conventional marker type could stand in for a namespace; a
property on an assembly-level attribute could name one; a link could carry the
other end of a relationship. Each would put something in the code whose only
purpose is to be annotated.

The catalog is also meant to be a reference work, and the temptation is to make
it complete: a pattern the literature names, absent from a catalog that claims to
cover its source, reads as an oversight.

The vocabulary is judged by what can be asserted through it (ADR-0008). A role
nothing can hold licenses no assertion, since there is nothing for a rule to
range over.

## Decision

A pattern that cannot be attached to a type, a member or an assembly is not in
the catalog.

## Rationale

An entry nothing can carry is an entry nothing can check, so it fails the
criterion the rest of the catalog is built on. Including it would put a name in
the vocabulary that never appears in any codebase and never participates in any
rule.

A conventional marker type would be worse than absence. It invents a declaration
whose only reason to exist is to be annotated, so the code would carry an
artefact of the documentation system rather than of the design being documented —
which inverts the premise that annotations describe code that would exist anyway.

Refusing the workaround keeps the boundary of the model honest and visible. The
catalog does not silently approximate: what it cannot express, it does not claim
to, and a contributor meeting the gap sees a limit rather than a convention to
imitate.

The completeness argument is answered elsewhere. A pattern left out is still part
of its body of work and belongs in the documentation and the index; what it does
not get is an attribute, because there would be nothing to put it on.

## Alternatives Considered

### Introduce a conventional marker type per namespace

Considered because it is a small convention, it is discoverable, and it would let
`Module` and the strategic relationship patterns be expressed.

Rejected because it asks a codebase to add a type that exists only to be
annotated. The annotation would then describe the documentation system rather
than the design, and the marker would have to be kept in step with a namespace
nothing ties it to.

### Carry the namespace as a string on an assembly-level attribute

Considered because it needs no new type and reaches namespace granularity.

Rejected because it is a magic string, unchecked and desynchronised by the first
rename — the same reason a string key was rejected for pattern occurrences.

### Include the pattern with no target at all, for completeness

Considered because the catalog is also a reference, and an absent pattern reads
as an omission.

Rejected because an attribute nobody can apply is a name in an assembly and
nothing more, and a reference work is better served by documentation that can say
why the pattern is not annotable.

## Consequences

### Positive

* Every entry in the catalog can be applied, and therefore checked.
* The code being documented gains no artefact from the documentation system.
* The limits of the model are visible rather than papered over.

### Negative

* The catalog is not a complete transcription of its sources, so a reader may
  look for a pattern that is deliberately absent.
* Domain-Driven Design loses `Module`, and the strategic relationship patterns
  are reduced to those that qualify an assembly.

### Risks

* The rule is applied per pattern by judgement, and a pattern that could have
  been expressed awkwardly may be dropped where a better shape existed.
* A future C# feature — a namespace-level attribute — would reopen entries closed
  under this decision, which would then need a superseding record rather than a
  quiet addition.

## Follow-up Actions

* Record in the catalog documentation which patterns were left out for this
  reason, so that the absence reads as a decision rather than an oversight.

## References

* [ADR-0008](0008-decide-sameness-by-the-assertions-a-pattern-carries.md) — the
  criterion this applies.
* [ADR-0010](0010-let-each-role-declare-what-it-applies-to.md) — what a role can
  be attached to.
