# ADR-0019 | Stop the identity climb at the pattern boundary

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0019-stop-the-identity-climb-at-the-pattern-boundary.fr.md)

**Status:** Superseded by [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md)
**Proposed:** 2026-08-05
**Accepted:** 2026-08-06
**Decision Makers:** Reefact

## Context

[ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) states
that the pattern an annotation belongs to is the type reached by climbing through
abstract bases and declensions, stopping at anything else. Every pattern in the
catalog today satisfies that rule, and the reference reader implements it.

The rule was written against the shapes that existed. A pattern with several roles
is a container holding one abstract role base and one attribute per role; a
pattern with one role is a flat attribute. The only relation in the catalog —
Evans' value object narrowing Fowler's — holds between two flat attributes, where
the derived attribute's base is concrete and the climb therefore stops on its own.

A multi-role pattern in a relation has no such shape. Its roles must answer one
identity, which is what the abstract role base is for, so the derived container's
role base is what inherits: `Derived.Role : Base.Role`, both abstract. Climbing
through abstract bases then passes through both and reports the derived pattern as
the pattern it derives from. Measured on two probe entries declined and specialised
from Composite: the reader counted 37 patterns where 38 were annotated, the
specialisation having been absorbed.

The generator refuses the case rather than emitting it, for both relations, and
fails with *a declension or specialisation of a multi-role pattern is not
generated yet*. ADR-0005 anticipated this only for a declension.

Two documents that ship with the library contradicted the accepted decision. The
identity rule published on `LivingDocumentationAttribute` read *the type
immediately below `LivingDocumentationAttribute`* — which is the alternative
ADR-0005 considered and rejected, and which returns the wrong answer for the one
relation the catalog holds. `DeclensionAttribute` illustrated a declension with
Fowler's and Evans' value objects, which
[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) settled as
two patterns in an inclusion, and which the catalog declares a specialisation.

The catalogs still to come are full of multi-role patterns that other works name
differently, so the case is ordinary rather than exotic.

## Decision

A pattern's identity is the type reached by climbing through an abstract base
declared in the same pattern and through a declension, and a pattern with several
roles is declined role by role while a specialisation derives pattern by pattern.

## Rationale

The boundary between two patterns is what the climb has to recognise, and an
abstract base does not mark it. What ADR-0005 meant by *abstract base* was always
the role base of the pattern being read — the thing that makes every role of one
pattern answer one type. That the rule worked was an accident of no abstract base
ever having an abstract base; the first multi-role relation removes the accident.
Comparing declaring types names the boundary directly, and leaves every existing
answer unchanged.

A declension still crosses the boundary, and now visibly for the reason it always
had: because it is marked, not because it is abstract. The two ways of climbing
stop being one mechanism that happened to cover both.

Declining role by role keeps a declension from restating anything. Each role
derives from its counterpart, so it inherits that role's targets, its multiplicity
and its links; the container is a spelling and nothing more. A declension whose
roles each declared their own targets would be two statements of one pattern's
applicability, free to drift — which is what ADR-0005 rejected a canonical
identity for, and what
[ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) rejects generally. It is
also what a flat declension already does; the multi-role case now does the same
thing rather than something else.

Specialising pattern by pattern follows from a specialisation being a pattern of
its own. Its roles are its own — it may narrow what they apply to, and
[ADR-0009](0009-let-each-role-declare-what-it-applies-to.md) exists so that it
can — so they cannot inherit the broader pattern's declarations. Inheriting at the
role base is enough for the guarantee ADR-0005 asks for: a rule written for the
broader pattern reaches the narrower one, because every derived role still answers
the broader role base.

Requiring a declension to hold the same roles, in the same order, is the same
claim read backwards. A declension asserts that two entries are one pattern; two
entries with different roles are not one pattern, and the generator refuses them
rather than emitting a shape that would quietly say otherwise.

This supersedes rather than amends because the sentence that changes is ADR-0005's
Decision. Everything else it decided — inheritance as the means for both relations,
the marker on the declension rather than the specialisation, identity as a type
rather than a name, anteriority ordering a declension — is untouched and is
restated here only by reference.

## Alternatives Considered

### Keep refusing a multi-role pattern in a relation

Considered because it is the state ADR-0005 chose deliberately: the generator
fails rather than emitting something approximate, and no catalog entry needs the
case today.

Rejected because the refusal was a deferral, not a position — ADR-0005 says the
first case will require the decision to be extended. The catalogs ahead are mostly
multi-role, so the first one to name a Gang of Four pattern differently would stop
the generator, and the answer would have to be designed under the pressure of
being blocked.

### Stop the climb after one abstract base

Considered because it is a smaller change than comparing declaring types, and it
fixes the merging just as well: one step up from a role attribute reaches its role
base, and stopping there is correct.

Rejected because it is a rule about distance rather than about meaning. It happens
to give the right answer for the shapes emitted today and says nothing about why;
a declension chain two deep — one pattern spelled by three catalogs — would break
it, and nothing in the rule would explain the breakage.

### Mark the pattern boundary with a second attribute

Considered because an explicit marker on each container's role base would make the
boundary a declared fact rather than an inferred one, symmetric with the declension
marker.

Rejected because the boundary is already declared: a nested type says which type
declares it, and that is exactly the question being asked. A marker would restate
it on every pattern in the catalog, and could be omitted — a second statement of
one fact, which is the failure this repository removes everywhere else.

### Have a multi-role specialisation derive role by role, like a declension

Considered because it would make one shape serve both relations, and because a
rule written for the broader pattern's *Leaf* would then reach the narrower
pattern's *Leaf* rather than only its role base.

Rejected because a specialisation is not obliged to hold the same roles as the
pattern it narrows, and inheriting role by role would either force it to or leave
some roles related and others not. It would also make the narrower pattern inherit
targets it may exist precisely to restrict.

## Consequences

### Positive

* A multi-role pattern can be declined or specialised, which the catalogs ahead
  need.
* A specialisation stays countable in every shape, not only when it is flat.
* The reason a climb crosses a boundary is now the marker alone, so the two
  relations are distinguished by one mechanism rather than two that overlap.
* A declension restates nothing, whatever its shape.
* The published identity rule, the reference reader and the ADR base now agree.

### Negative

* The rule takes one more clause to state, and *an abstract base declared in the
  same pattern* is a mouthful for something that reads, in code, as one comparison.
* A consumer that implemented the previous rule keeps compiling and starts
  answering differently the day a multi-role relation enters the catalog.

### Risks

* No catalog entry exercises the new shapes, so both are generated and read back
  under probe conditions rather than in CI. The first real case is where they are
  proven — reflection-based convention tests, deferred by
  [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.md), would close
  this.
* A declension of a declension is now expressible and has not been thought
  through; the climb would carry it, and nothing else has been checked.

## Follow-up Actions

* Exercise both shapes with a real catalog entry when the first multi-role
  relation is catalogued, and add its sample alongside.
* Reconsider the reflection-based convention tests, which would cover the shapes
  a probe can only check once.

## References

* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) —
  the record this supersedes; everything it decided beyond the climb still holds.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — what
  decides which relation applies, and what the shipped documentation contradicted.
* [ADR-0009](0009-let-each-role-declare-what-it-applies-to.md) — why a
  specialisation cannot inherit its roles' declarations.
* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — the reading rules
  the library publishes, which this corrects.
