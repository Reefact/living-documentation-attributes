# ADR-0034 | Let a specialisation name the role it narrows

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0034-let-a-specialisation-name-the-role-it-narrows.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-10
**Accepted:** 2026-08-10
**Decision Makers:** Reefact

## Context

`specialisationOf` names a pattern, and the generated attribute derives from one of two
things depending on the target
([ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md),
[ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md)):

* a target with **one** role is flat, so the narrowing derives attribute from attribute —
  `TestStubAttribute : TestDoubleAttribute`. Full precision;
* a target with **several** roles is a container, so the narrowing derives from the abstract
  `Role` base — `WireTapAttribute : RecipientList.Role`. It answers as *a participant in* a
  recipient list rather than as the recipient list itself.

Twenty-four relations ship today: fifteen precise and nine coarse. The nine are
`SecondaryPostingRule`, the four accountability narrowings, the three message intents and
`WireTap`.

[ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.md) accepted the coarse
shape rather than working around it, and listed the fix under *Alternatives Considered* with
the exact schema it would need — `{"catalog": …, "name": "Message", "role": "Message"}`. It
deferred it on one ground: *the imprecision it fixes has not yet cost anybody anything*.
[ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md) then removed the
generator's unused hook for it, and said in terms that a deferred alternative is
re-implemented if it is ever taken.

**It has now cost something.** Cataloguing the *Transactional messaging* group of
*Microservices Patterns* produced a relation the work states outright and the data cannot
carry at all:

> There are two patterns for implementing the Message relay: the Transaction log tailing
> pattern, the Polling publisher pattern.

`MessageRelay` is one of the four roles of `TransactionalOutbox`, beside `Sender`, `Database`
and `MessageOutbox`. So the two are not narrowings of the pattern — a polling publisher is not
a kind of transactional outbox — they are two ways of being **one of its participants**.

That is a different failure from the nine. Where the work says *pattern A is a kind of pattern
B*, deriving from `B.Role` is true and merely coarse: a command message really is a message.
Where the work says *pattern A is a way of being role R of pattern B*, there is no true
pattern-level statement to record, so recording one would overstate the work — which is the
thing ADR-0030 exists to prevent. So nothing was recorded, and the relation was first written
as a paragraph of `catalog/README.md` instead — which is what prompted this record.

So the deferred alternative now has two demonstrated uses rather than none: it would **sharpen**
nine relations that are already emitted, and it would **enable** two that cannot be emitted at
all.

## Decision

`specialisationOf` may name a **role** of the target pattern in addition to the pattern:

```json
"specialisationOf": { "catalog": "MicroservicesPatterns", "name": "TransactionalOutbox", "role": "MessageRelay" }
```

Where `role` is given, the narrowing pattern's attribute derives from **that role's
attribute**, which is emitted unsealed:

```csharp
public sealed class PollingPublisherAttribute : TransactionalOutbox.MessageRelayAttribute { }
```

Where `role` is omitted nothing changes: the two existing shapes stand, and no shipped relation
is rewritten by this decision.

## Rationale

The relation mechanism exists to carry a statement an author made, and there are two kinds of
statement about narrowing in the works catalogued here. One is *this pattern is a kind of that
one*, which the current shape carries. The other is *this pattern is one of that one's
participants*, which it cannot carry at all. Adding a field is a smaller change than leaving a
whole kind of authorial statement unrepresentable.

The precision is not decoration. What a relation buys is that a rule written for the broader
thing reaches the narrower one without naming it. Today a consumer asking for every message
relay — *is anything draining this outbox?* — finds nothing, because the two answers are
unrelated types. That is the exact question the group is about.

The cost is bounded and known. `sealed` on a role attribute is not part of the public API
baseline, so no baseline is rewritten; the identity climb is unchanged, because a role
attribute is neither abstract nor declared in the narrowing pattern, so the climb stops at the
narrower attribute exactly as it does today
([ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md)'s rule as restated on
`DesignPatternAttribute`); and the convention test
`A_role_is_sealed_unless_something_derives_from_it` was written to allow precisely this — it
asserts sealed *unless* something derives, which is why it survives the change unedited.

The nine coarse relations are **not** retro-fitted by this decision. ADR-0030 still governs
what may be recorded: a role is named where the work names one, and not because the mechanism
now allows it. Each of the nine is revisited on its own evidence or not at all.

## Alternatives Considered

### Leave it in prose

The status quo, and what the branch that found this first did rather than decide unilaterally:
the relation written as a paragraph of `catalog/README.md`, where a reader of that file finds
it and nothing else does.

Rejected. Prose is the right place for a thing the data *cannot* say; it is a poor place for a
thing the data is one field away from saying. And the asymmetry is what makes it wrong here:
the catalogue would carry *command message is a message* as inheritance and *polling publisher
is a message relay* as a paragraph, when the author states both in the same voice.

### Relate to the pattern and accept the under-specification

`PollingPublisherAttribute : TransactionalOutbox.Role`, which is what today's mechanism would
emit.

Rejected, and this is the alternative worth being precise about, because it is exactly what
ADR-0030 *accepted* for the other nine. It works there and fails here. `CommandMessage` derives
from `Message.Role` and answers as *a participant in a message*, which is true and merely less
than the whole truth. `PollingPublisher` deriving from `TransactionalOutbox.Role` would make
the generator write *"A narrower case of TransactionalOutbox: every participant annotated here
is one of those too"* — and a polling publisher is not a narrower case of a transactional
outbox. Recording it would be the overstatement ADR-0030 forbids.

### Change only the generated sentence

Keep `<Target>.Role` and rewrite the documentation it emits, so that it reads *a participant of
X, though which of its roles is not stated* rather than *a narrower case of X*.

Rejected, though it is the cheapest option and it does fix the prose. It leaves the
type-level claim untouched, so a consumer asking for every message relay still finds nothing —
and a rule that can be written is worth more here than a sentence that is honest. Worth doing
anyway as a separate tidy if this record is refused.

### Derive from the own-name role by default, with no schema change

In every one of the nine coarse relations the role meant is the one bearing the pattern's own
name — `Message.Message`, `RecipientList.RecipientList`, `Accountability.Accountability`,
`PostingRule.PostingRule`. So the generator could simply prefer that role over `Role` when the
target has one, and no schema field would be needed.

Rejected on two counts. It silently changes what nine shipped relations mean, which is the one
thing ADR-0030 warns cannot be undone once a consumer has written a rule against it. And it
does not solve the case that prompted this record: `TransactionalOutbox` has no role called
`TransactionalOutbox`, so the default would not fire and the outbox relation would still be
unwritable. It is a convenience for the easy nine that misses the hard two.

### Make Message relay a pattern of its own

Promote the role to an entry, so that pattern-to-pattern specialisation works unchanged.

Rejected. The work presents it as one of four participants in a numbered pattern, not as a
pattern; inventing an entry so that the mechanism fits is the catalogue telling the book what
it said. A pattern is held under the name and the shape its work gave it
([ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md)).

## Consequences

### Positive

* A statement an author makes about a pattern and a *role* becomes expressible, where today
  only statements about two patterns are.
* A rule can ask for every implementation of a participant — *is anything draining this
  outbox?* — and get an answer from the type system rather than from a paragraph.
* Nine existing relations become sharpenable, one at a time, each on the evidence ADR-0030
  requires.

### Negative

* A second emission shape for one relation mechanism, which is what ADR-0030 weighed against
  it. Three shapes now depend on the target: flat attribute, `Role` base, named role.
* The generator regains machinery ADR-0031 removed. That is the outcome ADR-0031 described
  rather than a contradiction of it, but it does mean a hook is being written back a fortnight
  after being deleted, and the deletion was right at the time.
* An unsealed role attribute is a wider public surface than a sealed one: anything can now
  derive from `MessageRelayAttribute`, including code outside this repository.

### Risks

* **Inherited link properties.** A role attribute with links — `MessageRelayAttribute` has
  `MessageOutbox` — passes them to whatever derives from it, so `[PollingPublisher]` now accepts
  an argument its own entry does not declare. Checked rather than assumed before this record was
  accepted: the analyzer wants declared symbols, an inherited member is not one, and the
  baseline is unchanged by the relation. The surface still grew, and it grew somewhere the
  catalog does not show it.
* **Incoherent targets.** Nothing would stop a narrowing that targets `Method` from naming a
  role that only targets `Class`. The generated code compiles and the assertion is nonsense.
* **A relation is a promise.** Once `[PollingPublisher]` answers as a message relay, a
  consumer may write a rule against it, and withdrawing the relation later breaks them. Same
  warning as ADR-0030, and it applies with more force here because the relation is finer.

## Follow-up Actions

All of these are carried out in the pull request that records this decision, rather than left
for later: the case that prompted the record is in the same branch, and an accepted decision
whose implementation is deferred is how a catalog and its generator drift apart.

* Add `role` to `patternRef` in `catalog/pattern.schema.json`, optional.
* Teach the validator two rules: the named role must exist on the target pattern, and the
  narrowing pattern's role targets must not be wider than those of the role it narrows.
* Restore the generator's unsealing hook, this time driven by the catalog rather than by an
  empty set — a role is emitted unsealed exactly when some entry names it.
* Verify the PublicAPI baseline of a package whose entry narrows a role carrying links, before
  the first such entry is committed.
* Record the two relations that prompted this — `PollingPublisher` and `TransactionLogTailing`
  onto `TransactionalOutbox.MessageRelay` — and remove the paragraph in `catalog/README.md`
  that stands in for them.

## References

* [ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.md) — deferred this exact
  alternative, with the schema it would need, and still governs *what* may be recorded.
* [ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md) — removed the
  machinery, and said it is re-implemented if the alternative is taken.
* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) — the relation
  is emitted as inheritance, which is what makes the target's shape matter.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — the test that
  separates *is a kind of* from *is a participant in*.
* `catalog/README.md` — where the group is written up, and where the paragraph this record
  replaces stood.
