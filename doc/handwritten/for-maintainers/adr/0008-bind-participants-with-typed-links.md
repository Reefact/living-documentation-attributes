# ADR-0008 | Bind participants of one pattern occurrence with typed links

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0008-bind-participants-with-typed-links.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

A real codebase contains several occurrences of the same pattern: three chains of
responsibility, two composites, a handful of visitors. An annotation that only
says *this class is a Handler* leaves them in one undifferentiated set.

That set is enough for the rules that treat a role as a category — *no entity
depends on infrastructure*, *every repository exposes an interface*. It is not
enough for anything that needs the pattern's structure: *every leaf implements
its component*, *every concrete visitor implements its visitor*. Nor is it enough
to draw a diagram, since a diagram of nine classes with no edges is a list.

Most of the time the type graph already holds the answer. A leaf implements the
component's interface, so which composite it belongs to is derivable without
anything being annotated. It is only where a type participates in several
occurrences, or where the hierarchy does not express the link, that the graph
falls short.

A string key naming the occurrence was proposed and rejected: it is a magic
value, unchecked by the compiler, and it desynchronises at the first rename.

## Decision

A role may declare optional links to other roles of its pattern, each carried as
a `Type`.

## Rationale

A `Type` is checked, followed by refactoring and navigable, which a key naming an
occurrence is not. It also names something that already exists rather than
inventing an identifier that has to be kept consistent by hand across every
participant.

Optional is the right default because the graph usually suffices. Requiring a
link on every participant would make the common case verbose in order to serve
the exception, and would ask an author to restate what their own type declaration
already says.

Declaring links per role rather than on the pattern keeps them meaningful. A
component has no component; a link declared once for the whole pattern would let
that be written, and a shape that permits nonsense invites it.

The links are what turns the model from a set of tags into something with edges,
which is what the rules that need structure and the diagrams both require. That
is the reason to carry them at all, and the reason they are worth the extra
surface on the roles that have them.

## Alternatives Considered

### Name each occurrence with a string key

Considered because it is the smallest possible addition and groups participants
without referring to any type.

Rejected because it is a magic value: nothing checks it, a typo splits an
occurrence in two, and a rename leaves it pointing at a name that no longer
exists.

### Require the link on every role

Considered because it would make every occurrence explicit and leave nothing to
inference.

Rejected because it duplicates what the type hierarchy says in the ordinary case,
and an annotation that restates the declaration is the failure this repository
removes elsewhere.

### Declare the links once on the pattern's role base

Considered because it is less to generate and puts the links in one place.

Rejected because it makes every role accept every link, including the ones that
make no sense, and a shape that can express nonsense will eventually be used to.

## Consequences

### Positive

* Occurrences of one pattern can be told apart, so structural rules and diagrams
  become possible.
* Nothing is written that the compiler does not check.
* The ordinary case stays free of ceremony.

### Negative

* A link is optional, so a consumer cannot rely on it being present and must fall
  back on the type graph.
* Which roles carry which links is a per-pattern editorial decision, and an
  omission is only visible to someone who needed the link.

### Risks

* A link can point at a type that plays no such role, since nothing checks the
  target's own annotation. That is a wrong claim rather than a broken rule, and
  only review or a convention test would catch it.

## References

* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) —
  the shape the links are declared on.
* [ADR-0010](0010-annotate-the-declaration-that-introduces-a-role.md) — the other
  half of keeping annotations from restating the code.
