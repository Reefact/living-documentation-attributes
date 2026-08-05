# ADR-0005 | Identify a pattern by the type that declares it

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0005-identify-a-pattern-by-the-type-that-declares-it.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

A consumer counting patterns, drawing a diagram or applying a rule has to decide
when two annotations concern the same pattern. Two facts make the obvious answers
wrong.

Pattern names are not unique across catalogs. *Adapter* names one pattern in the
Gang of Four — converting an interface — and another in ports and adapters, a
position at an architectural boundary. *Command* names one pattern that carries
its own execution and another that is a message with no behaviour at all.
Grouping by name merges patterns that have nothing to do with each other, and it
does so silently.

A single pattern is also spread over several types. Every role of a multi-role
pattern is its own attribute, so grouping by attribute type splits *Composite*
into as many patterns as it has roles.

Two patterns can further be related on purpose: the same pattern spelled by two
catalogs, or a narrower pattern derived from a broader one. Both are expressed by
inheritance, and they must not be treated alike (ADR-0006).

## Decision

The pattern an annotation belongs to is the attribute type reached by climbing
through abstract bases and declensions, and stopping at anything else.

## Rationale

The identity is a type rather than a name because names are not unique and types
are. Two homonyms declared in two catalogs are two types, compare unequal, and
cannot be conflated by any consumer, however careless.

Climbing through an abstract base is what gathers the roles of one pattern. The
container's abstract role base is the only thing every role of a pattern has in
common, so it is what every role of a pattern answers, without anything having to
be stated per role — which was a place two roles could have disagreed.

Climbing through a declension follows from what a declension is: the same pattern
spelled by another catalog, so both spellings must answer the same type. Stopping
anywhere else follows from what a specialisation is: a narrower pattern that is
not the pattern it derives from, and must stay countable on its own.

Nothing is written for any of this. Whether a base is abstract and whether a type
is marked are both already there, so the identity is derived from the declaration
like the rest (ADR-0004) and cannot be stated wrongly.

The identity is not something the library exposes, for the reasons in ADR-0004;
what this decision fixes is the rule, which is the one of the four that a
consumer would not guess.

## Alternatives Considered

### Group by the pattern name

Considered because it is what anyone tries first, and it reads well.

Rejected because it silently merges the two Adapters and the two Commands. A
grouping that is wrong without failing is worse than one that is inconvenient.

### Carry a canonical identity as a catalog-and-name pair

Considered because it is readable, printable, serialisable, and independent of
the type graph — so it would survive a future split of the catalogs into separate
packages.

Rejected because it has to be stated, which means it can be misstated, and
because each role of a pattern would have to repeat it — one occasion to diverge
per role. The packaging argument also dissolved once declensions were expressed
by inheritance, which is a stronger coupling than a type reference.

### Take the type immediately below the base, whatever it is

Considered because it is a single rule with no exceptions, and it was the first
shape built.

Rejected because it climbs past a specialisation, so a Null Object stops being
countable and is reported as a Special Case. Merging is right for a declension
and wrong for a specialisation, so one rule cannot serve both.

## Consequences

### Positive

* Two patterns that share a name are never conflated.
* Every role of a pattern answers the same identity, without anything stated per
  role.
* A specialisation stays a pattern in its own right while still answering to the
  broader pattern it derives from.
* Nothing has to be authored, so nothing can be authored wrongly.

### Negative

* The rule is not obvious, and a consumer that does not read it will group by
  name and be quietly wrong.
* The identity of a multi-role pattern is the container's abstract role base
  while a single-role pattern's is its own attribute type, so the two are not the
  same kind of type — harmless as an opaque key, visible if it is displayed.

### Risks

* The rule depends on the abstract role base existing in every multi-role
  pattern. It is emitted by the template, so a change there would silently change
  identity across the whole catalog.

## Follow-up Actions

* Keep the sample reader's implementation of the climb exercised, since it is the
  executable statement of this rule.

## References

* [ADR-0006](0006-distinguish-a-declension-from-a-specialisation.md) — why the
  climb has to distinguish two kinds of inheritance.
* [ADR-0008](0008-decide-sameness-by-the-assertions-a-pattern-carries.md) — how
  it is decided that two patterns are one.
* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — why the rule is
  documented rather than implemented in the library.
