# ADR-0006 | Catalogue a pattern where the work that named it put it

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

Patterns come from books and papers published over three decades, and the catalog
is organised along those bodies of work: a namespace per catalog, which is also
what a reader browses.

A pattern is not always where a reader expects it. Fowler names *Special Case*
what much of the industry calls *Null Object*. The same pattern is sometimes
catalogued by two works, sometimes under two names.

Placing a pattern by where it is best known was considered and would have meant
naming it as the industry does rather than as its source does — putting
`NullObject` under Patterns of Enterprise Application Architecture, where Fowler
wrote `SpecialCase`.

Two patterns being the same is settled separately, by the assertions they carry
(ADR-0007). When they are the same and are catalogued twice, one of the two has
to hold the definition and the other to derive from it (ADR-0005), and nothing in
their meaning orders them: they say the same thing.

The catalog is expected to span many more works, so the placement question is not
settled once but repeated for every pattern added.

## Decision

A pattern is catalogued in the body of work that named it, under the name that
work gave it, and where two works name the same pattern the earlier publication
holds the definition.

## Rationale

Provenance is a fact and popularity is not. Which work introduced a pattern under
which name is verifiable and stable; which name is better known varies by
community and by decade, and a catalog organised on it would need rearranging as
usage shifts. Placing by provenance also makes the catalog answer the question a
reader of a book actually has — *is the pattern I just read about in here* —
rather than a question about the industry's habits.

It costs nothing in discoverability, because that is what a declension is for. A
reader who knows the pattern under another name finds it under that name, in the
catalog they are reading, as an attribute deriving from the definition. Placement
by provenance and discoverability by declension are separate mechanisms, and
neither has to be compromised for the other.

Anteriority orders what meaning cannot. When two works say the same thing, no
argument from content can prefer one, and any other tie-break — which is more
cited, which the maintainer prefers — is a judgement that would be revisited.
Publication order is a fact, recorded, and checkable: the schema requires the
year, and a declension whose definition was published later is rejected.

The reference therefore stops being editorial. It is what fixes the direction of
an inheritance, and so the shape of the public API — which is why it is required
rather than nice to have, and why it must be accurate rather than approximate.

Where two works are contemporaneous, the tie-break is the name in wider use, and
it is recorded in the catalog as a decision rather than computed from a date. The
generator applies what is written; it does not arbitrate.

## Alternatives Considered

### Catalogue a pattern under the name it is best known by

Considered because it is what a developer types, and an editor searches type
names.

Rejected because it makes the catalog assert something false about provenance —
Fowler did not write `NullObject` — and because it would need revisiting as usage
moves. The discoverability it buys is bought instead by declensions, which cost
nothing in truth.

### Give the definition to the better-known of two works

Considered because it puts the canonical identity where most consumers would
expect to find it.

Rejected because it is a judgement rather than a fact, so it invites reopening
per pattern, and because it makes the identity of a pattern depend on something
that changes.

### Let the generator derive the direction from the recorded years

Considered because the years are in the catalog and the rule is mechanical.

Rejected because a contested or equal date would have the generator choose
silently, and because the decision that two patterns are the same is a human one
anyway — recording its outcome keeps it reviewable, where deriving it hides it in
arithmetic.

## Consequences

### Positive

* A reader of a book finds its patterns under its name, spelled as it spelled
  them.
* The organisation of the catalog rests on facts rather than on a reading of the
  industry.
* The direction of every declension is justified by something recorded and
  checkable.
* A reader who knows another name still finds the pattern where they look.

### Negative

* The canonical identity of a pattern is sometimes not the catalog it is
  associated with in practice, which will surprise anyone reading a grouped
  report.
* Every entry must carry an accurate reference, including a year, which is
  research rather than transcription.

### Risks

* A misdated reference silently reverses a declension. The schema checks the
  order but cannot check the dates themselves.
* Publication dates are occasionally contested or hard to pin to a year. The
  tie-break rule covers equality, not disagreement, and a disputed case has to be
  argued in the catalog entry.

## Follow-up Actions

* Keep the schema's rejection of an antedated declension in place as catalogs are
  added.

## References

* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) — the
  relation this orders.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — what
  must be settled before this rule applies.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) —
  where a pattern goes when no work claims it.
