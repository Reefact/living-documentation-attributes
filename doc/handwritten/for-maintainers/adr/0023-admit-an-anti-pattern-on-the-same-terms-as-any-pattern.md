# ADR-0023 | Admit an anti-pattern on the same terms as any pattern

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-06
**Accepted:** 2026-08-06
**Decision Makers:** Reefact

## Context

Every one of the ninety-five entries catalogued before this one names something a
team would be pleased to declare. A codebase says it has an aggregate, a
repository, a data mapper; nothing in the vocabulary so far is a name anyone
would rather not carry.

Finishing Evans' catalog reached one that is. *Domain-Driven Design* names Smart
UI in its fourth chapter and calls it the anti-pattern, and then does something
the label does not prepare a reader for: it presents it in the pattern form, with
a *therefore*, and states the circumstances under which it is the right answer —
a simple project, a short life, a single channel, a team for whom a model would
cost more than it returns.

It is annotable. A class or an assembly holds it, so
[ADR-0011](0011-leave-out-what-cannot-be-annotated.md) does not exclude it. It
has a source and that source has a catalog, so
[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md)
places it, and [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md)
does not apply.

The assertions it licenses are real, and they are of a kind no other entry
carries. Every other annotation *constrains* what a rule may find: a value object
is immutable, an aggregate member is unreachable from outside. This one
*exempts*: a rule about where business logic may live stops at the declaration
that carries it. A layering rule with no way to be told where it does not apply
either admits no exception, which no real codebase survives, or keeps its
exceptions in a list that lives outside the code.

Nothing written down says whether a catalog of design patterns holds the ones a
work names as mistakes. It is simply what every entry so far happened to be,
which is the kind of trait a reader of the output cannot tell from a decision
([ADR-0001](0001-check-every-pull-request-against-the-adr-base.md)).

The question does not stop at Evans. Big Ball of Mud is named by Foote and Yoder,
and Evans himself uses it to characterise a neighbouring context on a context
map — so any answer given here decides more than one entry.

## Decision

An anti-pattern enters the catalog on the same terms as any other entry — a
source, something that can hold the role, and verifiable assertions about a
participant — and being named as a mistake by the work that named it excludes
nothing.

## Rationale

The admission criteria already answer the question, and they do not ask whether a
pattern is one to be proud of. Smart UI satisfies every one of them. A rule
excluding anti-patterns would have to be invented, and the only thing supporting
it is the accident that none had come up yet — the same argument that was
rejected for patterns of test design in
[ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md).

Leaving it out would misrepresent the book. Evans gives the circumstances under
which Smart UI is right; a catalog of his patterns that silently dropped the one
he flagged would be publishing an opinion while presenting an inventory, and the
absence would read as an oversight rather than as a position
([ADR-0011](0011-leave-out-what-cannot-be-annotated.md) exists because that
distinction matters).

The exemption is the useful part, and nothing else provides it. Code with its
rules in the screen and no annotation is indistinguishable from code that drifted
there, and the correct instinct — extract a model — is applied to the one case
where it is wrong. A declaration turns that into a decision with a scope, which a
reviewer can argue with and a rule can honour.

The criteria stay discriminating without a new rule, which is the test of whether
one is needed. Big Ball of Mud fails them where Smart UI passes: what it asserts
about a participant is that it has no discernible structure, which is the absence
of an assertion rather than one, so there is nothing for a rule to range over
([ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md)). An
added prohibition on anti-patterns would exclude it twice and Smart UI wrongly.

The declaration is never written by accident. Nobody annotates a class as a
mistake without meaning to, so the risk in this direction is under-use, not
misuse — and an annotation that goes unwritten costs a name in a namespace, where
the one that goes unwritten today costs a model.

## Alternatives Considered

### Exclude anti-patterns as a category

Considered because every other entry names something a team declares willingly,
because "anti-pattern" is a label that invites a catalogue of failures rather than
of designs, and because a consumer counting patterns would find one among its
aggregates that is not a design at all.

Rejected because the exclusion would have to be invented and nothing supports it.
It would also decide by label rather than by content: the same book presents Smart
UI with a context in which it is correct, so the label is the author's judgement
about a trade-off, not a statement about what the pattern can carry. And it buys
nothing — the case it would be written for, Big Ball of Mud, is already excluded
by the assertion criterion.

### Admit it, and mark it as an anti-pattern in the data

Considered because a consumer could then filter, and because the catalog would
state the distinction rather than leave it to a description.

Rejected because it states in data what the entry's own summary already says, for
a consumer that does not exist yet — the reasoning that rejected the same shape in
[ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md). It would also
have to be decided for every existing entry, and the boundary is not sharp: a
transaction script is a fine choice and a common mistake, and nothing in the data
should have to say which.

### Catalogue it under a different name, without the label

Considered because Evans' own title carries the words *anti-pattern*, and dropping
them is a small departure from
[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md)'s rule
that a pattern is spelled as its work spelled it.

Rejected because the departure runs the other way: `SmartUi` **is** the name in the
book, and *anti-pattern* is what the book calls it, not part of what it is called.
A reader of the chapter looks for Smart UI and finds it.

## Consequences

### Positive

* Evans' catalog can be completed without an unexplained gap, and the entry a
  reader of chapter four looks for is where they look for it.
* A codebase gains a way to declare a deliberate exception to an architecture rule
  in the code the exception applies to, rather than in a configuration file
  alongside it.
* The criteria for entering the catalog stay the ones already written down, and
  are not joined by a rule about categories.

### Negative

* A consumer counting patterns finds one entry that is not a design to aspire to,
  and nothing but the summary says so.
* The vocabulary now contains a name a team may be reluctant to write, so the
  annotation most useful for spotting a scope will often be missing exactly where
  it is most needed.

### Risks

* The next candidate will be harder. God Object, Anemic Domain Model and Big Ball
  of Mud all have sources; the assertion criterion decides each, but *does this
  carry a checkable claim about a participant* is a slower question than *is this
  an anti-pattern*.
* An exempting annotation can be used to silence a rule rather than to record a
  decision. Nothing in the vocabulary can tell the two apart — only review can,
  which is true of every annotation here and more consequential for this one.

## Follow-up Actions

* Revisit if a second anti-pattern is proposed, and check that the assertion
  criterion is still doing the discriminating rather than the reviewer's taste.

## References

* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — the
  criterion that admits Smart UI and excludes Big Ball of Mud.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — why an absence must be
  told apart from an oversight.
* [ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md) — the same
  shape of question, answered the same way, for patterns of test design.
* `catalog/DomainDrivenDesign/SmartUi.json` — the entry.
* `catalog/README.md` — where Big Ball of Mud is recorded as left out, and why.
