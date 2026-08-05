# ADR-0004 | Keep the attribute base a pure marker

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0004-keep-the-attribute-base-a-pure-marker.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

The library ships no consumer. Whoever wants an inventory, a diagram or an
architecture rule writes it themselves, which makes what an annotation exposes to
a reader the whole of the public contract.

Three things a reader wants — the catalog an annotation came from, the pattern,
the role — are already said by the declaration itself. The catalog is the
namespace, the pattern is the containing type, the role is the attribute's own
name. Nothing about them is a separate fact.

Two ways of exposing them were built and measured. Stating them as constants on
every generated attribute meant writing each name twice: once as the declaration,
once as a literal that had to agree with it. Scaled to a catalog of one hundred
and ninety patterns and seven hundred and sixty roles, that doubled the assembly,
from 51.5 KB to 104 KB — a figure small in absolute terms, and entirely
redundant. Reading them back inside property getters removed the duplication, at
the price of putting reflection and a convention about the library's own layout
inside a marker.

A consumer already holds the attribute's type: `GetCustomAttributes` is how it
obtained the annotation. Nothing it would be handed is anything it does not
already have one call away.

Three of the four reading rules are not obvious, and one is a trap: the catalog
is the **first** namespace segment below the root, not the last, so that an
organisational sub-namespace folds into the catalog it belongs to. A consumer
guessing the last segment gets a plausible wrong answer.

## Decision

`LivingDocumentationAttribute` declares no member, and the rules for reading a
pattern back from an attribute type are documented rather than implemented.

## Rationale

An attribute is declarative data, and the two rejected shapes each broke that in
their own way: one restated what the declaration already said, the other put
behaviour — and introspection of the library's own namespace layout — inside a
marker. Declaring nothing is the only shape that is neither redundant nor
behavioural.

Nothing is taken away from a consumer. The information is in the type, the
consumer holds the type, and the rules that turn one into the other are four
lines each. What it loses is a facade over its own reflection, which it was
already performing.

The trap is answered without an API. The rules live in the documentation of the
base class, so they travel with the package and appear in the editor, and a
working reader in the sample project applies them — code to copy and own rather
than an interface to depend on. A helper published from the library would have to
be versioned and kept compatible for a benefit measured in four lines.

Nothing is stated that could disagree with the declaration. This is the same
reasoning that put the structure of the catalog in a template rather than in each
file: the failure being removed is not effort, it is divergence between two
statements of one fact.

Extension stays open by staying absent. A team declaring a vocabulary of its own,
outside this namespace layout, inherits an empty marker and reads it with rules
of its own — where properties derived from our layout would have handed it wrong
answers it could not correct.

## Alternatives Considered

### State the catalog, pattern and role as constants on each attribute

Considered because it is obvious, costs nothing at read time, and survives
renaming.

Rejected because it writes every name twice, so the two can disagree; because it
doubles the assembly for information already present; and because the
duplication is exactly what the catalog and its template exist to remove
elsewhere.

### Read them back in property getters on the base

Considered because it removes the duplication while keeping a convenient surface,
and because it implements the convention once where everyone gets it right.

Rejected because it makes a marker introspect its own library, and because a
consumer holding the attribute already holds its type: the convenience saves it
nothing it was not already doing.

### Publish a reader as part of the library

Considered because the reading rules have to live somewhere, and one of them is a
trap.

Rejected because it would become public API to version and keep compatible, for a
handful of lines a consumer is better off owning — and because a published helper
computes, where a consumer outside our layout needs to be able to disagree.

## Consequences

### Positive

* The library is a vocabulary and nothing else: no behaviour, no reflection, no
  convention of its own to keep working.
* Nothing in it can contradict the declarations it accompanies.
* A consumer with its own layout is unconstrained.
* The public surface is as small as it can be, so there is nearly nothing to
  version.

### Negative

* Every consumer writes the reading rules, and the catalog rule is easy to get
  wrong.
* The rules are enforced by documentation alone; nothing detects a consumer that
  applies them differently.

### Risks

* The documented convention and the sample reader can drift apart, leaving two
  statements of the rules that disagree — the very failure this decision removes
  from the attributes. Mitigated by the sample reader being executed.

## Follow-up Actions

* Keep the sample reader working and exercised, since it is the only executable
  statement of the reading rules.

## References

* [ADR-0005](0005-identify-a-pattern-by-the-type-that-declares-it.md) — the
  fourth reading rule, and the one that cannot be guessed.
* [ADR-0013](0013-show-every-pattern-at-work-in-a-business-example.md) — the
  project that carries the reader.
* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) —
  the declaration the rules read from.
