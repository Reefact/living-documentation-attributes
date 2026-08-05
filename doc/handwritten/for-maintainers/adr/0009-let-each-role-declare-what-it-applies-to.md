# ADR-0009 | Let each role declare what it can be applied to

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0009-let-each-role-declare-what-it-applies-to.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

Roles differ in what can legitimately hold them. A component is an abstraction,
so an interface or a class; a leaf has no children and can be a record struct; a
composite holds children and cannot. The accept and visit operations are methods.
A bounded context is an assembly.

While the library carried one attribute per pattern, applicability had to be
declared once for the whole pattern, which meant taking the union of what any of
its roles could accept — so nothing was really constrained.

The first hand-written entries showed what happens when this is decided
per file rather than per catalog: four attributes carried no declaration at all
and therefore accepted every target, and `AttributeTargets.Struct` was absent
from every entry. The second is not cosmetic — `[ValueObject]` could not be
applied to a `readonly record struct`, which is the most idiomatic value object in
modern C#, and the pattern most likely to be annotated first.

Multiplicity and inheritance vary by pattern rather than by convention. A type can
hold one role in two occurrences of a pattern, so a type role is repeatable; a
method holds a member role once. A subtype of an entity is an entity, so the
marker is inherited; a subtype of a component is not necessarily a leaf, so a
Gang of Four role is not.

## Decision

Each role declares its own targets, multiplicity and inheritance, taken from the
catalog rather than from a convention.

## Rationale

It turns a nonsensical annotation into a compilation error. A composite on a
struct, a visit operation on a class, a bounded context on a type: each is now
refused where a shared declaration had to permit them all. The model gains
capacity to assert, which is the criterion the whole catalog is judged by.

Inheritance is a property of the pattern, not a house style. Whether a subtype
still holds a role is a statement about what the pattern means, so deciding it
per pattern in the catalog is recording a fact; applying one value across the
library would be asserting something false about half of it.

Declaring it as data rather than per file removes the failure that produced the
first entries. A missing target set is a schema error rather than a silent
default of *everything*, and the choice is made once per role by whoever writes
the pattern, not once per file by whoever happens to be editing.

`Struct` and `Assembly` are in the target vocabulary because the model would
otherwise exclude legitimate participants: value objects and domain events are
routinely record structs, and the strategic patterns qualify an assembly rather
than any type in it.

## Alternatives Considered

### Apply one target set and one inheritance rule across the library

Considered because it is one decision instead of several hundred, and it cannot
be forgotten.

Rejected because it is false in both directions: it would either forbid a record
struct value object or permit a struct composite, and it would assert an
inheritance rule that is right for the domain markers and wrong for the
structural roles.

### Leave `AttributeUsage` off and accept the default

Considered because the default is permissive and never rejects a legitimate
annotation.

Rejected because the default accepts everything, including parameters, fields and
assemblies, so an attribute without a declaration asserts nothing about where it
belongs. Four of the first entries did exactly this, unintentionally.

### Decide targets in the generator by the kind of role

Considered because member roles and type roles differ predictably.

Rejected because the differences that matter are not predictable from the kind:
whether a leaf may be a struct and a composite may not is a fact about the
pattern, and no rule over role kinds recovers it.

## Consequences

### Positive

* A wrong annotation fails to compile rather than being recorded as data.
* Value objects and domain events can be record structs.
* Strategic patterns can annotate an assembly.
* Multiplicity and inheritance say something true about each pattern.

### Negative

* Three more editorial decisions per role, which is three more chances to record
  something wrong.
* A target set that is too narrow rejects a legitimate annotation, and the author
  meets it as a compilation error with no obvious remedy but to change the
  catalog.

### Risks

* Widening a target set later is additive, but narrowing one breaks consumers who
  had annotated legitimately under the old set. The initial choice is therefore
  effectively permanent, and erring narrow is the more expensive mistake.

## References

* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) —
  the shape that makes per-role declaration possible.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  where the declarations are authored.
