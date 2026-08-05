# Architecture Decision Records

Dated records of significant decisions — their context, the option chosen, and
the consequences. An ADR is a historical log: once accepted it is not edited in
place; a decision is revisited by writing a **new** ADR that supersedes the old
one, and the old one's status changes to *Superseded* with a link to its
successor.

## This base was written as one set

Records 0001 to 0013 carry the same proposal date. They were written together,
after the decisions they describe had been taken together, and they describe one
coherent position rather than a sequence of revisions. **None of them amends,
refines or supersedes another**, and the numbering is an index, not a chronology.

Where one of them argues against a shape this repository briefly held, that shape
appears under *Alternatives Considered*, which is where an option weighed and set
aside belongs. A superseding record is a different instrument, reserved for a
decision that replaces one already **accepted** — and no record here has been
accepted yet.

## When is an ADR written?

Every pull request is checked against this base — the moment new decisions enter
the codebase. Most pull requests embark no architectural decision and add no ADR;
the check is what is mandatory, not the artifact. The test for "significant": *if
the implementation changed but the decision stood, the ADR should not need
editing.* A new decision is **recorded** here, a decision that replaces another is
written as a **superseding** ADR, and a change that **conflicts** with an accepted
ADR is raised for the maintainer. The agent procedure — draft as *Proposed*, never
flip a status unilaterally — is in [`AGENTS.md`](../../../../AGENTS.md), and the
decision that makes the check mandatory is
[ADR-0001](0001-check-every-pull-request-against-the-adr-base.md).

This base carries a particular weight in this repository. The attributes are
generated from a catalog, so almost nothing here is defended by the code itself:
the same generator would happily emit a different shape, and a reader of the
output cannot tell which of its traits were decided and which merely happened.
The reasoning lives here or nowhere.

## An ADR is a decision record, not a specification

An ADR captures a **decision and the reasoning behind it** — not how that
decision is implemented. Implementation mechanics (code, configuration, exact
flags, command snippets, step-by-step walkthroughs) live in the code, in the
catalog, and in the reference documentation the ADR links to — never in the ADR
itself. In particular, **Rationale is argument, not a design document**: if a
paragraph explains *how something is built* rather than *why the decision is
right*, it belongs elsewhere and the ADR links to it.

## File conventions

* One file per decision, under `doc/handwritten/for-maintainers/adr/`, named
  `NNNN-short-title.md` — a four-digit sequence number and a lowercase,
  kebab-case title.
* ADRs are written in **English** — the canonical version — with a French
  translation kept alongside as `NNNN-short-title.fr.md`. The English version is
  authoritative; the French one follows it. Each file carries a language banner
  linking to its counterpart.
* Every ADR follows the format below; [`template.md`](template.md) is a
  copy-ready skeleton.

## Format

### Title and header

```markdown
# ADR-{number} | {Short Title}

**Status:** Proposed | Accepted | Superseded | Deprecated
**Proposed:** YYYY-MM-DD
**Accepted:** YYYY-MM-DD
**Decision Makers:** {Names or team}
```

The header carries **one dated line per state the decision actually reached in
this repository**, and no date is ever overwritten. A record drafted as
*Proposed* carries `Proposed:` alone; accepting it adds `Accepted:` below and
leaves the first line untouched. A supersession moves no date and introduces
none — what connects the two records is the link, not the date.

### Context

Everything that led to the decision, so that someone unfamiliar with the project
understands why a decision had to be made. **Facts only** — it does not justify
the chosen solution. Everything the Rationale argues from must appear here first.

### Decision

**One single sentence.** No justification, no alternatives, no history.

### Rationale

Why this decision is the best choice given the Context. Each argument traces back
to a fact already stated there. **Argument only** — naming a mechanism's role and
why it exists belongs here; documenting how it is wired does not.

### Alternatives Considered

Every serious alternative, each with **why it was considered** and **why it was
ultimately rejected** — not simply that it was.

### Consequences

Under **Positive**, **Negative** and **Risks**.

### Follow-up Actions

Work that becomes necessary because of the decision.

### References

Related ADRs, sources, catalog entries, code.

## Index

| ADR | Title | Status |
|---|---|---|
| [ADR-0001](0001-check-every-pull-request-against-the-adr-base.md) | Check every pull request against the ADR base | Proposed |
| [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) | Keep the pattern catalog as data and generate the attributes from it | Proposed |
| [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) | Give each role its own attribute, nested in the pattern it belongs to | Proposed |
| [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) | Keep the attribute base a pure marker | Proposed |
| [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) | Relate patterns by inheritance, and read a pattern's identity from it | Proposed |
| [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) | Catalogue a pattern where the work that named it put it | Proposed |
| [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) | Decide that two patterns are the same by the assertions they carry | Proposed |
| [ADR-0008](0008-bind-participants-with-typed-links.md) | Bind participants of one pattern occurrence with typed links | Proposed |
| [ADR-0009](0009-let-each-role-declare-what-it-applies-to.md) | Let each role declare what it can be applied to | Proposed |
| [ADR-0010](0010-annotate-the-declaration-that-introduces-a-role.md) | Annotate the declaration that introduces a role | Proposed |
| [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) | Leave out of the catalog what cannot be annotated | Proposed |
| [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md) | Show every pattern at work in a business example | Proposed |
| [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) | Shelve a pattern without a body of work of its own under Idioms | Proposed |
| [ADR-0014](0014-write-commit-messages-to-a-checkable-convention.md) | Write commit messages to a convention a script can check | Proposed |
| [ADR-0015](0015-turn-a-warning-into-an-error-in-ci.md) | Turn a warning into an error in CI | Proposed |
| [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.md) | Prove on every pull request that the sources are what the catalog generates | Proposed |
| [ADR-0017](0017-pin-every-action-to-a-commit.md) | Pin every GitHub action to a commit | Proposed |
| [ADR-0018](0018-hold-the-public-surface-to-a-committed-baseline.md) | Hold the public surface to a committed baseline | Proposed |
