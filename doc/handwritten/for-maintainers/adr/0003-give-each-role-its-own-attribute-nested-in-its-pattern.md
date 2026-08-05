# ADR-0003 | Give each role its own attribute, nested in the pattern it belongs to

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0003-give-each-role-its-own-attribute-nested-in-its-pattern.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

A participant holds a role within a pattern: a class is the *Leaf* of a
*Composite*, an interface is the *Element* of a *Visitor*. The annotation has to
say both.

The library first carried one attribute per pattern, taking the role as an
enumeration argument. On a class that is a leaf, that reads `[CompositePattern(
CompositeParticipant.Leaf)]`, and an attribute is read as *this class is a X* —
so it says the class is a Composite, which is the opposite of what a leaf is. The
`Pattern` suffix was what kept it merely ambiguous rather than plainly wrong.

Role names collide heavily across patterns. *Component*, *Context*, *Subject*,
*Product*, *Handler* and *Target* are each used by four or five patterns of the
Gang of Four alone, so a flat attribute per role is not available.

Roles are not all held by types. The visit and accept operations of a Visitor,
the factory method, the template method and its primitive operations are members,
and the two hand-written method attributes that survived in the library were
evidence that the model needed them.

The catalog is expected to grow by an order of magnitude. Enumeration members
carry ordinals into the metadata of every consuming assembly, so inserting a role
into an existing enumeration silently reassigns the meaning of code already
compiled elsewhere — and a growing catalog inserts roles constantly.

## Decision

Each role is its own sealed attribute, nested in a static container named after
the pattern, except for a pattern that has a single role, which is a flat
attribute carrying the pattern's name.

## Rationale

It restores the only reading an attribute supports. `[Composite.Leaf]` says *this
class is a Composite.Leaf*, and `[ValueObject]` says *this class is a value
object*; both are the *is a* form, where the parameterised version had to invent
a second grammar the syntax does not carry.

Nesting is what makes one attribute per role possible at all. `Composite.
Component` and `Decorator.Component` are distinct types without a namespace of
their own, without an alias, and without the collisions that sank the flat form —
the container is the qualifier.

It lets each role state its own applicability rather than the union of them all.
A single parameterised attribute must accept every target any of its roles needs;
separate attributes let a leaf accept a struct while a composite does not, which
turns a nonsensical annotation into a compilation error.

It removes the ordinal hazard entirely rather than managing it. With no
enumeration there is nothing to renumber: adding a role adds a type, which is
additive for assemblies already compiled. On a catalog whose whole purpose is to
keep growing, that is the difference between a versioning discipline and a
non-question.

A member role needs no new concept under this shape — it is a role like the
others, distinguished only by what it may be applied to. The parallel enumeration
the parameterised form required disappears.

A pattern with one role stays flat because there is nothing to choose. Nesting
exists to carry a choice, and `[Entity]` reads as the ubiquitous language where
`[Entity.Entity]` reads as a machine.

## Alternatives Considered

### Keep one attribute per pattern with a role enumeration

Considered because it is the conventional shape, it is compact, and it was what
the library already had.

Rejected on the reading — `[Composite(Leaf)]` states the wrong thing about a leaf
— and on three consequences of the enumeration: shared targets across roles,
ordinals that make a growing catalog a breaking-change treadmill, and a parallel
enumeration needed for member roles.

### Keep the enumeration but restore the `Pattern` suffix

Considered because `[CompositePattern(Leaf)]` is unambiguous, and it is what the
library did before.

Rejected because it fixes only the ambiguity. The suffix reads as a textbook
reference rather than as the language of the domain — `[EntityPattern]` where
nobody says "the Entity pattern" — and it leaves the targets, the ordinals and
the member roles untouched.

### One flat attribute per role, without nesting

Considered because `[Leaf]` and `[Component]` read best of all.

Rejected because the names collide: *Component*, *Context*, *Subject*, *Product*
and *Handler* each belong to several patterns, and the library would have to
either mangle the names or scatter them across namespaces.

### A single generic attribute taking strings

Considered because it would scale to any catalog with no generated types at all.

Rejected because it gives up compile-time checking and editor discovery, which
are the two things that make the vocabulary usable while writing code, and
because a rule engine needs a closed role vocabulary to be reliable.

## Consequences

### Positive

* Every annotation reads as *this is a X*, whether the pattern has one role or
  seven.
* Role names may repeat across patterns without ceremony.
* Each role constrains what it can be applied to, so a wrong annotation fails to
  compile.
* Adding a role to a published pattern is additive.
* Member roles need no separate concept.

### Negative

* Many more types: roughly four per pattern instead of one plus an enumeration.
* Filtering by role is done on types rather than by switching on an enumeration,
  which is a change of habit.
* A role that carries the name of its own pattern reads awkwardly —
  `Visitor.Visitor`, `Composite.Composite` — and that is irreducible, since it is
  the name the role actually has.

### Risks

* The container occupies its name in the namespace, so a consumer with a type of
  the same name must qualify. Unavoidable under any shape that puts the pattern
  name in scope.

## Follow-up Actions

* Keep the flat form reserved for patterns whose single role carries the
  pattern's own name, so that the choice between the two shapes stays derivable
  rather than declared.

## References

* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — what the generated
  attributes do not carry.
* [ADR-0009](0009-let-each-role-declare-what-it-applies-to.md) — the target
  declaration this shape makes possible.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  the generation that emits it.
