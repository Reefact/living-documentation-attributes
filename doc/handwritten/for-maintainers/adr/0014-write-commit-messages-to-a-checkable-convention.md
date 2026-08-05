# ADR-0014 | Write commit messages to a convention a script can check

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0014-write-commit-messages-to-a-checkable-convention.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

The repository has no commit convention. Its history reads as prose written to
whatever standard each commit's author held at the time.

Much of what this repository produces is generated, so a change to the template
or the catalog rewrites a great many files at once. The commit message is often
the only place that says which of the two happened and why — a diff spanning the
whole catalog looks identical whether a role was added or the emission changed.

The decisions are recorded in the ADR base, and the ADR check runs per pull
request; the commit history is the finer grain below that, and it is what a
maintainer reads when bisecting or when asking why one file looks the way it
does.

A convention that is not checked degrades. That is recorded elsewhere in this
repository about coding rules, and it holds for messages: the effort of writing
one well is invisible, and nothing rewards it at the moment it is skipped.

## Decision

Every non-merge commit follows Conventional Commits, validated by one script
shared by a local `commit-msg` hook and a per-pull-request check.

## Rationale

One script rather than two implementations is the part that makes this hold. A
hook and a workflow that each encode the rules will disagree eventually, and the
disagreement surfaces as a commit that passed locally and fails on the way in —
which teaches contributors to distrust the local check rather than to fix the
message. Sharing the script makes divergence impossible rather than unlikely.

Checking in CI as well as locally is what makes the hook worth enabling. A hook
is opt-in and bypassable, so it cannot be the enforcement; it is the fast
feedback. The pull-request check is the enforcement, and it catches
`--no-verify`.

The header is validated in full and bodies are left alone. The header is where
the value is — it is what a log, a bisect and a changelog read — and it is short
enough to have an unambiguous shape. A body is prose, and a rule over prose would
be a rule about style rather than about information.

Two footer rules survive that reasoning. A breaking change must be signalled both
by `!` and by a `BREAKING CHANGE:` footer, because each alone is missable — the
`!` by a reader skimming, the footer by anything reading only headers — and a
version that promises compatibility while breaking it is the most expensive
mistake a library of public types can make. An issue footer is checked for shape
only, so that references stay machine-readable.

Scopes are validated but not required. Requiring one on every change would ask
authors to invent a component for changes that touch none. The set is closed so
that a scope stays a component rather than becoming a free-text field, which is
what makes it usable later for routing release notes.

## Alternatives Considered

### Adopt an off-the-shelf linter such as commitlint

Considered because the convention is standard and the tool is maintained by
others.

Rejected because it brings a Node toolchain into a repository whose only tooling
is .NET and a Python script, for a rule set of about a hundred lines. The shared
script also lets the exact diagnostics be written for this repository — naming
its scopes, explaining why a rule exists — which a generic linter reports as rule
identifiers.

### Enforce the convention only in CI, with no local hook

Considered because CI is where enforcement actually lives, and a hook is one more
thing to enable per clone.

Rejected because it moves every violation to after the commit is written, when
fixing it means an interactive rebase rather than editing the message in front of
you. The hook costs one `git config` and removes almost all of that.

### Keep writing messages by judgement, without a convention

Considered because the recent history is already carefully written, and a
convention adds ceremony to every commit.

Rejected because judgement is exactly what does not survive a repository growing
by an order of magnitude, and because the value of a convention is not in any one
message but in the whole history being queryable the same way.

## Consequences

### Positive

* The history states what kind of change each commit is, and which component it
  touched, in a form a tool can read.
* A breaking change cannot be signalled by halves.
* The local check and the enforced check cannot disagree.

### Negative

* Every commit carries the cost of the convention, including trivial ones.
* The scope list has to be maintained as the repository gains components, and a
  stale list rejects a legitimate message.

### Risks

* The convention is enforced on the header only, so a body can still say nothing
  useful. Nothing here changes that, and nothing should try to.

## Follow-up Actions

* Revisit whether a scope should become mandatory on the version-driving types if
  release notes are ever routed by scope.

## References

* `CONTRIBUTING.md` — the convention as an author reads it.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.md) — the other
  per-pull-request check.
