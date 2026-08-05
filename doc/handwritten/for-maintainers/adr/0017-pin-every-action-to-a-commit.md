# ADR-0017 | Pin every GitHub action to a commit

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0017-pin-every-action-to-a-commit.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-05
**Decision Makers:** Reefact

## Context

The workflows run third-party actions, which execute with the repository checked
out and with whatever token the job was granted.

A tag is a mutable pointer. `@v7` resolves to whatever commit the tag names at
the moment the workflow runs, and the owner of the action can move it — including
onto code that was never reviewed by anyone here. That is not hypothetical:
compromised actions have been used to exfiltrate secrets from repositories that
pinned by tag.

This repository is the smaller half of the risk. Its workflows hold a read-only
token and no publishing credentials today, but it is a library meant to be
published, so a release workflow with a package credential is a foreseeable
addition rather than a distant one.

Pinning by commit is only half a practice. A pinned action stops receiving fixes,
including security fixes, unless something updates the pin.

## Decision

Every action a workflow uses is referenced by its full commit hash, with the
human-readable version in a trailing comment.

## Rationale

It removes an entire class of supply-chain exposure for the cost of a longer
line. A commit hash cannot be moved, so what ran yesterday is what runs today,
and an update becomes a reviewed change rather than something that happens to a
repository while nobody is looking.

Adopting it at the first workflow is what makes it free. A pinning policy applied
later is a migration across every workflow and a decision about each one; applied
now there is nothing to migrate, and the convention is what a contributor copies
when they add the next workflow.

The version comment is not decoration. A bare hash is unreadable, so without it a
reviewer cannot tell a routine bump from a downgrade, and cannot see at a glance
that two workflows disagree about which version of an action they run.

The staleness it introduces is real and is answered by tooling rather than by
attention — a dependency updater proposes the bump, and the pull request it opens
is reviewed like any other. That is the trade this accepts: freshness by an
explicit act instead of freshness by default.

Preferring the runner's own toolchain where it suffices is the same reasoning
applied one step earlier. An action that is not used needs no pin, no review and
no update, so the catalog job uses the Python already on the image rather than an
action to install one.

## Alternatives Considered

### Reference actions by tag

Considered because it is what most repositories do, it reads well, and it keeps
actions current with no maintenance.

Rejected because a tag is mutable, so it delegates to the action's owner the
decision of what code runs here. Currency is worth having, but not at the price
of not knowing what ran.

### Pin only the actions used by privileged workflows

Considered because today's workflows are read-only, and the exposure is
concentrated where credentials are.

Rejected because it puts the security of a future workflow in the hands of
whoever writes it, at the moment they are thinking about something else. A rule
that applies everywhere needs no judgement at the point of use.

### Vendor the actions into the repository

Considered because it removes the third party from the trust chain entirely.

Rejected as disproportionate: it makes every update a manual port, for a
repository whose workflows use three well-known actions.

## Consequences

### Positive

* What a workflow runs is fixed and reviewable, and cannot change without a
  commit here.
* The convention is established before there is anything to migrate.
* A reviewer can read which version each action is at.

### Negative

* Pins go stale, so a dependency updater becomes necessary rather than optional.
* Every action bump is a pull request, including the routine ones.

### Risks

* A hash and its version comment can disagree if a bump edits one and not the
  other, which would mislead a reviewer precisely where the comment exists to
  inform them.
* Without an updater configured, pinning silently becomes freezing, and a
  security fix in an action never arrives.

## Follow-up Actions

* Configure a dependency updater for the workflow actions, without which this
  decision degrades into freezing them.

## References

* `.github/workflows/` — where the pins live.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.md) — the
  workflows this applies to.
