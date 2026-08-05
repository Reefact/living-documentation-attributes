# ADR-0001 | Check every pull request against the ADR base

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0001-check-every-pull-request-against-the-adr-base.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

This repository publishes a vocabulary. Its attributes carry no behaviour, so
almost nothing it decides is defended by the code: the shape of a role, what a
role may be applied to, which catalog holds a pattern, whether two patterns are
one — none of these produces a compiler error when written differently. A
reviewer reading a generated file sees the outcome of a decision and cannot tell
it apart from an accident.

The attributes are generated from a catalog, which widens the gap. The generator
would emit a different shape just as willingly, and a change to it rewrites every
pattern at once. A decision taken once and left unrecorded is therefore not
merely undocumented — it is invisible in a diff of two hundred files that all
changed for the same reason.

The catalog is also expected to grow by an order of magnitude, across bodies of
work published decades apart. Questions that have already been settled here — how
provenance is decided, what makes two patterns the same, what may be annotated —
will be asked again for every catalog added, by contributors and agents who were
not part of settling them.

The first entries were produced with the assistance of an agent, and the defects
that survived were of exactly this kind: role sets silently incomplete, a base
class silently not inherited, targets silently inconsistent. Each was plausible
in isolation and only visible against a rule nobody had written down.

## Decision

Every pull request is checked against the ADR base, and a pull request that
embarks a lasting decision carries the ADR that records it.

## Rationale

The check is mandatory and the artifact is not, because most changes embark no
decision at all — adding a pattern to the catalog exercises decisions already
taken rather than taking new ones. What must not happen is a decision entering
silently, and only a systematic check at the moment code is proposed can catch
that.

The alternative to recording is remembering, and this repository is the wrong
place for it. Nothing in a generated attribute explains why it has the shape it
has, so a maintainer returning to it, or an agent working on it, has no way back
to the reasoning except this base. The reasoning is not recoverable from the
output; it has to be kept.

Raising a conflict rather than resolving it keeps the base honest. A change that
contradicts an accepted ADR is either a mistake or a better idea, and only a
maintainer can say which. An agent that quietly reshapes the code to match a new
intuition erases the record it was meant to consult.

Drafting as *Proposed* and never accepting follows from the same reasoning. An
accepted ADR is a ratified position; ratifying one is a judgement about the
project, not about the change under review, and it is not the author's to make —
no more than merging their own pull request would be.

## Alternatives Considered

### Document decisions in the code, in XML documentation

Considered because the repository already documents heavily, and the reader is
right there.

Rejected because documentation on a generated type is regenerated with it, and
because it can only state what the decision is, never what was weighed against
it. The alternatives considered are the part a future maintainer needs most, and
they have nowhere to live in a summary.

### Record decisions in commit messages

Considered because the decisions taken so far were in fact argued in commit
messages, at length.

Rejected because a commit message is addressed to a reviewer at one moment and
found afterwards only by someone who already suspects it exists. A base that is
indexed and consulted before writing code is a different instrument from a
history that is searched after the fact.

### Write ADRs only when a decision is contested

Considered because it would keep the base small and every entry load-bearing.

Rejected because whether a decision was contested is invisible later; several of
the decisions recorded here were reversed two or three times before settling, and
the record of what was tried and why it failed is precisely what stops the next
contributor from trying it again.

## Consequences

### Positive

* The reasoning behind a vocabulary that cannot defend itself is written down.
* A contributor or an agent has one place to consult before deciding, and one
  place to look when a shape seems arbitrary.
* A change that contradicts a settled position surfaces as a conflict rather than
  as a silent reversal.

### Negative

* Every pull request carries the cost of the check, including the many that
  conclude with nothing to record.
* The base has to be maintained, indexed and translated, and grows with the
  project.

### Risks

* The check can degrade into a formality that is declared rather than performed.
  Nothing here prevents that; only the maintainer reading the result does.
* An agent can misjudge what is significant, and either flood the base or miss a
  real decision. Mitigated by asking rather than guessing when it is unclear.

## Follow-up Actions

* Keep [`AGENTS.md`](../../../../AGENTS.md) the operative statement of the
  procedure, so that an agent acts on it without being asked.
* Translate each accepted ADR into French alongside the English canonical file.

## References

* [`AGENTS.md`](../../../../AGENTS.md) — the procedure an agent follows.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  the generation that makes decisions invisible in a diff.
* Reefact, `first-class-errors`, ADR-0004 — the practice this repository adopts.
