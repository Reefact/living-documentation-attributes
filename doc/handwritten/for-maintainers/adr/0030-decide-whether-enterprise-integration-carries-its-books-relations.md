# ADR-0030 | Decide whether Enterprise Integration Patterns carries its book's relations

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0030-decide-whether-enterprise-integration-carries-its-books-relations.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-10
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

`EnterpriseIntegration` now holds all **65** patterns of its book and **not one relation**.
That is not because the book states none. Its structure is largely a statement of them:

* the *Message Routing* chapter presents twelve patterns as kinds of `MessageRouter`, the
  base pattern given in chapter 3;
* the *Message Transformation* chapter presents six as kinds of `MessageTranslator`,
  likewise;
* the *Messaging Channels* chapter presents its channels as kinds of `MessageChannel`;
* the consumers of the *Messaging Endpoints* chapter are kinds of `MessageEndpoint`;
* and individual entries say it outright — the book states that a **Wire Tap** *is* a fixed
  `RecipientList` with two output channels, and presents **Command**, **Document** and
  **Event Message** as three kinds of `Message`.

On the order of thirty entries are concerned. None of them carries a relation, and the
catalogue's `README.md` has until now given a reason for two of them — that the relation
would assert something the book does not — which the six shipped relations of the same
shape show to be wrong. It under-specifies; it does not misstate. That paragraph is
corrected in the same change that proposes this ADR.

So the absence is not a decision that was taken and recorded. It is a decision that was
never made.

## Decision

Enterprise Integration Patterns is catalogued without `specialisationOf` relations, and
the narrowings its book states are recorded in `catalog/README.md` as prose, until a
maintainer decides otherwise.

## Rationale

The absence should be a decision rather than an oversight, and this ADR exists mainly to
make it one. Between recording the status quo and retrofitting thirty relations, the first
is what a proposal should carry: the second changes a shipped package's inheritance graph
on the strength of a reading of a book's table of contents, and that is a maintainer's call
and not an agent's.

There is a real argument for the status quo beyond caution. A relation is emitted as
inheritance, and inheritance in a vocabulary is a promise about every future consumer: once
`ContentBasedRouterAttribute` derives from `MessageRouterAttribute`, a rule asking for
routers silently changes meaning, and a codebase that annotated both — which several will
have, since the two are different statements a reviewer might both want — starts
double-counting. The catalogues that carry relations carry ten of them, chosen one at a
time; thirty at once, derived from chapter structure rather than from what each entry
asserts, is a different kind of act, and [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md)'s
test — *the assertions they carry, not their names or their neighbourhood* — is exactly the
test a chapter heading does not pass.

Against it: a reader who knows the book will expect a content-based router to answer as a
message router, and today it does not. Prose in the catalogue's README tells them why, but
prose is not what a rule reads.

## Alternatives Considered

### Relate what the book's chapter structure states

Add `specialisationOf` to the routing patterns, the transformation patterns, the channels
and the consumers — on the order of thirty entries — so that the emitted hierarchy matches
the book's own.

The best argument for it: this is a vocabulary for stating what a codebase participates in,
and a work that organises its patterns into families has said something a reader will want
to ask about. Rejected here only because it is the larger and less reversible of the two —
relations can be added later without changing anything already emitted, while removing one
breaks whoever wrote a rule against it.

### Relate only the entries whose text says it outright

Wire Tap *is* a recipient list; the three message intents *are* kinds of message. Four
entries, each with a sentence in the book to point at, and no reliance on chapter
structure.

This is the alternative most likely to be right, and it is deliberately not the decision:
it is a coherent middle that a maintainer should choose knowingly rather than inherit from
whatever an agent found convenient on a Tuesday. If it is chosen, it supersedes this ADR
rather than amending it.

### Let `specialisationOf` name a role rather than a pattern

`{"catalog": …, "name": "Message", "role": "Message"}`, emitted as
`CommandMessageAttribute : Message.MessageAttribute`, which is more precise than deriving
from `Message.Role`. The generator carries an unused hook that this would need — a set of
roles to emit unsealed, read when each role's modifier is chosen and never written to.

Rejected as premature: it adds a second emission shape to the one mechanism the vocabulary
has for relations, and the imprecision it fixes has not yet cost anybody anything. Worth
revisiting only if the previous alternative is taken and the loss of precision then bites.

## Consequences

### Positive

* The catalogue's only statement about its own relations stops being a wrong reason and
  becomes a recorded decision with the alternatives written down.
* Nothing already emitted changes, and every alternative here remains open at the same
  cost as today.

### Negative

* A rule written for `MessageRouter` does not reach the twelve routers of chapter 7, and
  nothing in the package says why. The reason lives in this ADR and in
  `catalog/README.md`.
* `EnterpriseIntegration` is the largest catalogue and the only one with no relations at
  all, which reads as an omission until this ADR is found.

### Risks

* Deciding later is cheap for the code and not for the readers: a consumer who has written
  their own "is a router" rule against the current graph will have written it by listing
  twelve attributes, and adding the relations does not remove that list.

## Follow-up Actions

* Decide between the status quo, the four outright cases, and the full chapter structure —
  and supersede this ADR with whichever is chosen.
* Decide separately whether the generator's unused unsealed-roles hook is dead machinery
  to remove or a seam to keep; it is currently read and never written, which is neither.

## References

* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — identity is
  decided by the assertions an entry carries.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) — no relation
  crosses a catalogue, so this question is internal to one work.
* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — the admission
  of the work.
* `catalog/README.md` — where the narrowings the book states are written down.
