# ADR-0030 | Relate only the narrowings a work states outright

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0030-relate-only-the-narrowings-a-work-states-outright.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-10
**Accepted:** 2026-08-10
**Decision Makers:** Reefact

## Context

A catalog entry may declare `specialisationOf`, naming another pattern of the same
catalogue that it narrows. It is emitted as inheritance, and the generated documentation
says what it means: *every participant annotated here is one of those too, and a consumer
asking for the broader pattern gets these as well*.

Ten relations exist today, in three catalogues. They take two shapes:

* four target a pattern with **one role**, and derive attribute from attribute —
  `PostAttribute : PartyAttribute`, `RowDataGatewayAttribute : GatewayAttribute`;
* six target a pattern with **several roles**, and derive from the abstract base its roles
  share — `HierarchicAccountabilityAttribute : Accountability.Role`,
  `SecondaryPostingRuleAttribute : PostingRule.Role`.

Both shapes are shipped and correct. The second says "a participant in the broader
pattern" rather than naming which role, which is what a whole-pattern narrowing means.

`EnterpriseIntegration` holds all **65** patterns of its book and, before this decision,
not one relation. That was not because the book states none. It states them in two very
different ways:

**By structure.** The *Message Routing* chapter presents twelve patterns under
`MessageRouter`, the base pattern given in chapter 3; *Message Transformation* presents six
under `MessageTranslator`; the channels sit under `MessageChannel`, the consumers under
`MessageEndpoint`. Around thirty entries are arranged this way.

**Outright, in the text of a pattern.** Four entries have a sentence about the two
patterns:

* the book states that a **Wire Tap** *is* a fixed `RecipientList` with two output
  channels;
* it presents **Command Message**, **Document Message** and **Event Message** as three
  kinds of `Message`.

The catalogue's `README.md` had until now given a reason for the last three — that the
relation would assert something the book does not, since `Message` has three roles and the
inheritance would be from `Message.Role`. The six shipped relations of that same shape show
this to be wrong: the relation **under-specifies rather than misstates**. A command message
is a participant in the Message pattern; what is lost is only which participant.

[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) already fixes how
questions of this kind are settled: by the assertions an entry carries, never by its name
or its neighbourhood.

## Decision

A narrowing is recorded as `specialisationOf` where the work states it about the two
patterns, and not where it is merely implied by the work's arrangement of them.

## Rationale

The two ways a book states a family are not the same evidence, and ADR-0007 is the test
that separates them. A sentence saying a wire tap *is* a recipient list is an assertion
about a pattern, made by the author, and it is exactly what this catalogue exists to carry
over. A chapter heading is neighbourhood: twelve patterns are printed under `MessageRouter`
because they are about routing, and a reader who inferred that each *is* a message router
would be reading the table of contents rather than the text.

The distinction also matches what the relation costs. `specialisationOf` is emitted as
inheritance, which is a promise to every future consumer: once an attribute derives from
another, a rule asking for the broader pattern silently changes meaning. Four relations,
each with a sentence to point at, can be audited by a reviewer holding the book. Thirty
derived from an arrangement cannot, and a wrong one among them is not removable later
without breaking whoever wrote a rule against it.

The under-specification is accepted rather than worked around. `WireTapAttribute` derives
from `RecipientList.Role` and so answers as *a participant in* a recipient list, not as the
recipient list itself; the three message intents likewise. That is the same thing the six
shipped relations of this shape say, so this decision adds no new meaning to the
vocabulary — it only applies the existing one four more times. A codebase that wants the
precise role writes both attributes, which costs one line.

## Alternatives Considered

### Record nothing, and write the narrowings down in prose

The status quo, and what this ADR proposed before the decision was taken. Its argument was
caution: relations can be added later without changing what is already emitted, while
removing one breaks a consumer.

Rejected: it treats an author's explicit statement and a chapter heading as equally weak
evidence, when the whole point of ADR-0007 is that they are not. An absence where the book
says *is* costs a reader something real and buys only the avoidance of a decision.

### Relate everything the chapter structure implies

Around thirty entries, so that the emitted hierarchy matches the book's arrangement.

Rejected on ADR-0007's test: a chapter heading is not an assertion an entry carries. It
would also make `MessageRouter` and `MessageTranslator` answer for half the catalogue,
which is a large claim to derive from typography.

### Let `specialisationOf` name a role rather than a pattern

`{"catalog": …, "name": "Message", "role": "Message"}`, emitted as
`CommandMessageAttribute : Message.MessageAttribute`, which is more precise than deriving
from `Message.Role`. The generator carries an unused hook this would need — a set of roles
to emit unsealed, read when each role's modifier is chosen and never written to.

Deferred, not rejected: it adds a second emission shape to the vocabulary's one relation
mechanism, and the imprecision it fixes has not yet cost anybody anything. It is the change
to make if the loss of precision starts to bite now that these four exist.

## Consequences

### Positive

* What an author states about two of their own patterns is carried into the vocabulary
  instead of stopping at prose.
* A consumer asking for a recipient list gets the wire taps, and one asking for the Message
  pattern gets the commands, documents and events.
* The rule for adding a relation is a test a contributor can apply alone: find the sentence,
  or do not relate.

### Negative

* Four relations are emitted at role-base precision, so a rule asking specifically for
  `Message.Message` still does not reach the intents. A codebase that means both writes
  both attributes.
* The catalogue now has related and unrelated entries side by side, and nothing in the
  generated output says which side of the test an unrelated pair fell on. That is what this
  ADR and `catalog/README.md` are for.

### Risks

* "States it outright" is a judgement about a sentence, and a contributor with the book open
  may read one where the author meant an aside. The mitigation is the same as for every
  entry: the assertion has to be quotable in the pull request.

## Follow-up Actions

* Revisit role-targeted relations if the four emitted here prove too coarse in practice.
* Decide separately whether the generator's unused unsealed-roles hook is dead machinery to
  remove or the seam that alternative would need; it is currently read and never written,
  which is neither.

## References

* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — decide by the
  assertions a pattern carries.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) — no relation crosses
  a catalogue, so this question is internal to one work.
* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — the admission of
  the work.
* `catalog/README.md` — the four relations, and what the chapter structure states that is
  deliberately not carried.
