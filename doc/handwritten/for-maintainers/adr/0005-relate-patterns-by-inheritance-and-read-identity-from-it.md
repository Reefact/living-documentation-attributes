# ADR-0005 | Relate patterns by inheritance, and read a pattern's identity from it

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0005-relate-patterns-by-inheritance-and-read-identity-from-it.fr.md)

**Status:** Superseded by [ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md)
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

A consumer counting patterns, drawing a diagram or applying a rule has to decide
when two annotations concern the same pattern. Three facts make the obvious
answers wrong.

Pattern names are not unique across catalogs. *Adapter* names one pattern in the
Gang of Four — converting an interface — and another in ports and adapters, a
position at an architectural boundary. *Command* names one pattern that carries
its own execution and another that is a message with no behaviour at all.
Grouping by name merges patterns that have nothing to do with each other, and it
does so silently.

A single pattern is spread over several types. Every role of a multi-role pattern
is its own attribute, so grouping by attribute type splits *Composite* into as
many patterns as it has roles.

Two patterns can further be related on purpose, in two ways that must not be
treated alike:

* **The same pattern, catalogued twice**, under the same name or another one, by
  two bodies of work. A reader of the second catalog looks for it there, so it
  has to exist there; but the two spellings are one pattern and must be counted
  once.
* **A narrower case of another pattern.** Every Null Object is a Special Case;
  plenty of Special Cases — an unknown customer, a missing rate — are not Null
  Objects. The two remain distinct patterns, each countable, and a rule written
  for the broader one applies to the narrower one as well.

C# offers one construct for both, and it already means one of them: `class B : A`
says *B is an A*, which is exactly what the second relation says. What it cannot
say is *B is the same as A*, because two types cannot be one.

## Decision

A pattern relates to another by inheritance — plain when it is a narrower case of
it, marked `[Declension]` when it is the same pattern spelled twice — and the
pattern an annotation belongs to is the type reached by climbing through abstract
bases and declensions, stopping at anything else.

## Rationale

Inheritance is the right means for both relations because both need the derived
attribute to answer to the one it derives from: a declension so that either
spelling can be filtered by the definition's type, a specialisation so that a
rule written for the broader pattern reaches the narrower one without being
repeated. The rule sets then compose the way the patterns do, which is the
practical payoff — a value object of Evans is subject to the value-equality rule
of Fowler because the attribute derives, and carries an immutability rule of its
own on top.

Marking the declension rather than the specialisation follows from what
inheritance already means. `:` says *is a*, which is the specialisation, so
leaving that unmarked lets the language carry its own meaning; sameness is the
relation the type system cannot state, so inheritance is a means rather than a
statement there, and recording that it is a means is all the marker does. Marking
the other way round would annotate the ordinary reading of `:` in order to leave
the extraordinary one inferred.

Without the distinction the identity is undecidable, which is why the two belong
in one decision. A climb to the top of an inheritance chain merges a
specialisation into the pattern that contains it — right for a declension, wrong
for a specialisation — and nothing in the chain says which one is being climbed.
The marker is what makes the climb able to stop in the right place, so neither
half stands without the other.

The identity is a type rather than a name because names are not unique and types
are. Two homonyms declared in two catalogs are two types, compare unequal, and
cannot be conflated by any consumer, however careless.

Climbing through an abstract base is what gathers the roles of one pattern. The
container's abstract role base is the only thing every role has in common, so it
is what every role answers, without anything having to be stated per role — which
would have been a place two roles could disagree.

Nothing is written for any of this. Whether a base is abstract and whether a type
is marked are both already there, so the identity is read from the declaration
like the rest (ADR-0004) and cannot be stated wrongly. The marker is the only
thing the library carries beyond the empty base, and it earns that place by being
unrecoverable: nothing else in the type graph tells the two relations apart.

The direction of each relation is settled by a different rule, because they are
different questions. Two spellings of one pattern say the same thing, so nothing
in their meaning can order them, and the earlier publication holds the definition
— which makes the reference load-bearing (ADR-0006). Inclusion, on the other
hand, orders itself: the narrower derives from the broader, whatever the dates,
since being published earlier does not make a pattern broader.

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
per role. The packaging argument also dissolves once the relations are expressed
by inheritance, which is a stronger coupling than a type reference.

### Climb to the type immediately below the base, whatever it is

Considered because it is a single rule with no exceptions, and it needs nothing
declared anywhere — no marker, and therefore no distinction to record.

Rejected because it climbs past a specialisation, so a Null Object stops being
countable and is reported as a Special Case. Merging is right for a declension
and wrong for a specialisation, so one rule cannot serve both.

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

Considered because it needs no second type.

Rejected because a string in a list is discoverable by nobody. A reader of the
other catalog searches for a type by name in an editor, and finds nothing; an
attribute deriving from the definition is found where it is looked for, checked
by the compiler, and answers the right identity.

### State the relation in the documentation only

Considered because it needs nothing in the library at all.

Rejected because the climb needs the distinction to compute a correct answer, and
prose cannot be consulted at run time.

## Consequences

### Positive

* Two patterns that share a name are never conflated.
* Every role of a pattern answers the same identity, without anything stated per
  role.
* A specialisation stays a pattern in its own right while still answering to the
  broader pattern it derives from.
* Rules compose along the same hierarchy as the patterns, so a rule for a broader
  pattern is written once.
* Either spelling of a declined pattern is found where a reader looks for it.
* Nothing has to be authored for the identity, so nothing can be authored wrongly.

### Negative

* The climb is not obvious, and a consumer that does not read it will group by
  name and be quietly wrong.
* The identity of a multi-role pattern is the container's abstract role base
  while a single-role pattern's is its own attribute type, so the two are not the
  same kind of type — harmless as an opaque key, visible if it is displayed.
* The attribute that is derived from cannot be sealed, so the generator unseals
  exactly those and no others — a difference between generated files explained by
  data rather than readable in the file.
* A typed filter is asymmetric: filtering on the definition catches the
  declension, not the other way round. Harmless only because the climb is the
  correct instrument.

### Risks

* The climb depends on the abstract role base existing in every multi-role
  pattern. It is emitted by the template, so a change there would silently change
  identity across the whole catalog.
* A relation can be recorded wrongly — a specialisation declared as a declension
  merges two patterns that should have stayed apart. The marker is a claim, and a
  wrong claim is a wrong decision, not a broken rule.
* A declension of a multi-role pattern is not generated, and the generator fails
  rather than emitting something approximate. No case exists yet; the first one
  will require this decision to be extended.

## Follow-up Actions

* Keep the sample reader's implementation of the climb exercised, since it is the
  executable statement of this rule.
* Extend the generator when a declension of a multi-role pattern first appears.

## References

* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — why the climb is
  documented rather than implemented in the library.
* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) —
  the anteriority that orders a declension.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — how
  it is decided which of the two relations applies.
