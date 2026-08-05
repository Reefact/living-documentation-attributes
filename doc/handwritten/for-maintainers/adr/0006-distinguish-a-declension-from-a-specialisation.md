# ADR-0006 | Distinguish a declension from a specialisation

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0006-distinguish-a-declension-from-a-specialisation.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

Two patterns of the catalog can be related in two different ways.

The same pattern is sometimes catalogued twice, under the same name or another
one, by two bodies of work. A reader of the second catalog looks for it there, so
it has to exist there; but the two spellings are one pattern and must be counted
once.

A pattern is sometimes a narrower case of another. Every Null Object is a Special
Case; plenty of Special Cases — an unknown customer, a missing rate — are not
Null Objects. The two remain distinct patterns, each countable, and a rule
written for the broader one applies to the narrower one as well.

Both were generated as plain inheritance, which made them indistinguishable in
the code: only the prose of the remarks said which was which, and prose is not
structure. It also leaves the identity a consumer groups by undecidable: a climb
to the top of an inheritance chain merges a specialisation into the pattern that
contains it, which is right for a declension and wrong for a specialisation, and
nothing in the chain says which one is being climbed.

C# offers one construct for both, and it already means one of them: `class B : A`
says *B is an A*, which is exactly what a specialisation says. What it cannot say
is *B is the same as A*, because two types cannot be one.

## Decision

A specialisation is plain inheritance and carries no marker; a declension is
inheritance marked with `[Declension]`.

## Rationale

The unmarked case is the one inheritance already expresses. Making specialisation
the default costs nothing to state and lets the language carry its own meaning;
marking it instead would annotate the ordinary reading of `:` in order to leave
the extraordinary one unmarked.

The marked case is the one that needs a marker, precisely because it is not
expressible. Sameness cannot be declared in the type system, so inheritance is a
means rather than a statement there — and recording that it is a means is all the
marker does.

Both relations use inheritance because both need the derived attribute to answer
to the one it derives from: a declension so that either spelling can be filtered
by the definition's type, a specialisation so that a rule written on the broader
pattern reaches the narrower one without being repeated. The rule sets then
compose the way the patterns do, which is the practical payoff — a value object
of Evans is subject to the value-equality rule of Fowler because the attribute
derives, and carries an immutability rule of its own on top.

The direction of each relation is settled by a different rule, because they are
different questions. Two spellings of one pattern say the same thing, so nothing
in their meaning can order them, and the earlier publication holds the definition
— which makes the reference load-bearing (ADR-0007). Inclusion, on the other
hand, orders itself: the narrower derives from the broader, whatever the dates,
since being published earlier does not make a pattern broader.

The marker is the only thing the library carries beyond the empty base
(ADR-0004), and it earns that place by being unrecoverable: nothing in the type
graph distinguishes the two relations, and the identity rule needs the
distinction to be correct.

## Alternatives Considered

### Duplicate the pattern in both catalogs, identically

Considered because the code is generated: a reader of either catalog would find a
complete pattern where they looked, at no maintenance cost.

Rejected because the two copies would be unrelated types, so nothing would tie
them together for a consumer, and a rule written for one would not reach the
other. Inheritance gives the same discoverability in four lines and keeps the
link.

### Mark the specialisation instead

Considered because it would leave the declension as the unmarked default, and
declensions may well end up more numerous.

Rejected because inheritance already means *is a*: marking a specialisation
annotates what the language says on its own, and leaves the case the language
cannot say to be inferred.

### Carry the relation as a list of alternative names on the attribute

Considered because it needs no second type, and it was the shape first proposed
for alternative names.

Rejected because a string in a list is discoverable by nobody. A reader of the
other catalog searches for a type by name in an editor, and finds nothing; an
attribute deriving from the definition is found where it is looked for, checked
by the compiler, and resolves to the right identity.

### State the relation in the documentation only

Considered because it needs nothing in the library at all.

Rejected because the identity rule needs the distinction to compute a correct
answer, and prose cannot be consulted at run time.

## Consequences

### Positive

* The two relations are distinguishable in the code, not only in prose.
* The identity a consumer groups by is decidable, and correct for both relations.
* Rules compose along the same hierarchy as the patterns, so a rule for a broader
  pattern is written once.
* Either spelling of a declined pattern is found where a reader looks for it.

### Negative

* The attribute that is derived from cannot be sealed, so the generator unseals
  exactly those and no others — a difference between generated files that has to
  be explained by data rather than read from the file.
* A typed filter is asymmetric: filtering on the definition catches the
  declension, not the other way round. Harmless only because the identity rule is
  the correct instrument.

### Risks

* A declension of a multi-role pattern is not generated, and the generator fails
  rather than emitting something approximate. No case exists yet; the first one
  will require the decision to be extended.
* A relation can be recorded wrongly — a specialisation declared as a declension
  merges two patterns that should have stayed apart. The marker is a claim, and a
  wrong claim is a wrong decision, not a broken rule.

## Follow-up Actions

* Extend the generator when a declension of a multi-role pattern first appears.

## References

* [ADR-0005](0005-identify-a-pattern-by-the-type-that-declares-it.md) — the rule
  that needs this distinction.
* [ADR-0007](0007-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) —
  the anteriority that orders a declension.
* [ADR-0008](0008-decide-sameness-by-the-assertions-a-pattern-carries.md) — how
  it is decided which of the two relations applies.
