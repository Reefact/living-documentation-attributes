# ADR-0010 | Annotate the declaration that introduces a role

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0010-annotate-the-declaration-that-introduces-a-role.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

A role is often introduced by an interface and then implemented several times.
The accept operation of a Visitor is declared once on the element interface and
implemented by every concrete element; the execute operation of a Command is
declared once and implemented by each command.

C# does not propagate an attribute from an interface member to the members that
implement it, and `Inherited` governs base classes rather than interfaces. So
nothing decides the question for the author: annotating the declaration, every
implementation, or both all compile and all produce different data.

Writing the sample for Visitor made the cost visible. The accept operation
appeared three times in the inventory — once for the interface, once for each
implementation — which is a fact about how the sample was written rather than
about the code being described.

Whichever choice an author makes, a consumer counting participants gets a
different answer. Left unstated, the same codebase yields different data
depending on who annotated it, and two codebases cannot be compared.

## Decision

A role is annotated on the declaration that introduces it, and not on the
declarations that implement or override it.

## Rationale

The declaration is where the intent is expressed. An interface member is the
statement that the operation exists and what it is for; an implementation is how
one type answers it. The pattern's role belongs to the first, and repeating it on
the second says nothing new.

It keeps the annotation from restating the code. That a class implements an
interface is already in the type graph, so a consumer that wants the
implementations can walk to them; an annotation on each is a second copy of a
fact the compiler already holds — the same redundancy this repository removes
from the attributes themselves.

An unstated convention would make the data incomparable. Counting is the simplest
thing a consumer does, and the count is meaningless unless every codebase
annotated the same way. Fixing the rule is what makes an inventory a measurement
rather than a reflection of habit.

It is the cheaper of the two consistent choices. Annotating every implementation
scales with the number of implementers, has to be repeated whenever one is added,
and is silently wrong when one is forgotten — where a single annotation on the
declaration cannot be partially applied.

The rule is carried in the documentation of every pattern, so it reaches an
author in the editor at the moment they annotate, rather than in a document they
would have to have read.

## Alternatives Considered

### Annotate every implementation

Considered because it makes each participant self-describing: a reader opening
one class sees its role without following an interface.

Rejected because it duplicates a fact the type graph already holds, scales with
the number of implementations, and is silently incomplete the first time one is
missed.

### Annotate both, and let consumers deduplicate

Considered because it serves both readings and asks no one to choose.

Rejected because deduplicating requires knowing that two annotations describe one
role, which is exactly the judgement the convention exists to avoid delegating to
every consumer.

### Leave it to the author

Considered because the model records what the author means, and either choice is
defensible in a given codebase.

Rejected because the data stops being comparable across codebases, which removes
most of the value of having a shared vocabulary at all.

## Consequences

### Positive

* One codebase yields one inventory, whoever annotated it.
* The annotation never restates what the type graph says.
* Adding an implementation requires no annotation.

### Negative

* A reader opening an implementation sees no role and must follow the interface
  to find it.
* Nothing enforces the convention; a codebase annotating every implementation
  produces inflated counts with no sign that anything is wrong.

### Risks

* Where a role is introduced by an abstract class rather than an interface, the
  boundary between introducing and overriding is less obvious, and two authors
  may draw it differently.

## Follow-up Actions

* Consider a rule that flags an annotation on a member that overrides or
  implements an annotated one, since it is mechanically detectable.

## References

* [ADR-0008](0008-bind-participants-with-typed-links.md) — the other half of
  keeping annotations from restating the code.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md) — where
  the convention is demonstrated.
