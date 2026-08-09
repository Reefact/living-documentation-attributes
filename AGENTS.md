# Working in this repository

Read this before changing anything. It states what an agent — or a contributor —
is expected to do on its own initiative, without being asked.

## What this repository is

A vocabulary. It publishes attributes that let code state which pattern a type or
a member participates in, and nothing else: the attributes carry no behaviour,
the library exposes no reader, and almost nothing it decides is defended by the
compiler.

That last point governs everything below. A role's shape, what it may be applied
to, which catalog holds a pattern, whether two patterns are one — none of these
produces an error when written differently. They are decisions, and the only
place they survive is the ADR base.

## Architecture decisions

**Before finalizing a pull request, check it against the ADR base** under
`doc/handwritten/for-maintainers/adr/` (format and conventions:
[`adr/README.md`](doc/handwritten/for-maintainers/adr/README.md); the decision
that makes the check mandatory is
[ADR-0001](doc/handwritten/for-maintainers/adr/0001-check-every-pull-request-against-the-adr-base.md)).

An ADR records a **significant, lasting decision** — one a future maintainer
would ask "why did they do it this way?" about — not every change. Apply the
test: *if the implementation changed but the decision stood, the ADR should not
need editing.* Most pull requests embark no such decision; the **check** is
mandatory, the **ADR** is not.

The check has three outcomes — state the result in the pull request description:

- **Create** — the pull request embarks a new lasting decision. Draft one ADR per
  decision from `template.md` with **`Status: Proposed`**, add it to the index in
  `adr/README.md`, and link it from the pull request.
- **Supersede** — the decision replaces one already recorded. Never edit the
  existing ADR in place or change its status yourself: name it in the pull
  request, draft the successor as `Proposed`, and leave the status flip to the
  maintainer. Accepted ADRs are immutable historical records.
- **Alert** — the pull request contradicts an accepted ADR. Do not proceed
  silently: flag it in the description — `⚠️ Conflicts with ADR-NNNN (<title>)` —
  with the precise conflict, and let the maintainer decide.

The description is written against
[`.github/pull_request_template.md`](.github/pull_request_template.md), which
carries the outcome alongside what changed, what the catalog gained and how it
was verified. **Read it and fill it in.** GitHub inserts it into the web form
only: a pull request opened through the API carries whatever body it was given,
so an agent that does not open the template ships a description that answers
none of it.

An agent **drafts and proposes**; it never accepts, supersedes or deprecates an
ADR on its own authority — that is the maintainer's call, exactly as no agent
merges a pull request. When it is genuinely unclear whether a change is
significant, say so and let the maintainer judge rather than guessing.

### Decisions you will meet immediately

These come up in nearly every change to the catalog. Read them before touching
it, rather than rediscovering them:

- The catalog is data; the attributes are generated from it and the output is
  committed ([ADR-0002](doc/handwritten/for-maintainers/adr/0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md)).
  **Never hand-edit a generated attribute** — edit the catalog and regenerate.
- Whether two patterns are the same is decided by the assertions they carry, not
  by their names ([ADR-0007](doc/handwritten/for-maintainers/adr/0007-decide-sameness-by-the-assertions-a-pattern-carries.md)).
- A pattern is held by every catalogue whose work presents it as its own; a work
  that merely cites another's pattern does not hold it
  ([ADR-0028](doc/handwritten/for-maintainers/adr/0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md)).
- **A relation never crosses a catalogue.** Each catalogued work ships as its own
  independent package, so the inheritance a cross-catalogue relation would emit
  cannot exist; `validate.py` and the generator both refuse one
  ([ADR-0027](doc/handwritten/for-maintainers/adr/0027-ship-one-independent-package-per-catalogued-work.md)).
- What cannot be attached to a type, a member or an assembly does not enter the
  catalog ([ADR-0011](doc/handwritten/for-maintainers/adr/0011-leave-out-what-cannot-be-annotated.md)).
- Every pattern gets one sample file ([ADR-0012](doc/handwritten/for-maintainers/adr/0012-show-every-pattern-at-work-in-a-business-example.md)).

## Adding or changing a pattern

1. Edit or add the entry under `catalog/<Catalog>/<Pattern>.json`. It must satisfy
   `catalog/pattern.schema.json`, including its `reference` with a year.
2. Regenerate: `python3 catalog/generate.py`. It rewrites the attribute sources
   **and** `doc/generated/catalog-index.md`. Regenerating an **unchanged**
   catalog must leave the working tree clean — if it does not, a generated file
   was edited by hand.
3. Add or update the sample in
   `Reefact.LivingDocumentation.Attributes.Usage/<Catalog>/<Pattern>Usage.cs`.
4. Build the solution, run `dotnet test`, and run the sample project. The tests
   assert what a generated attribute must look like; the inventory is the check
   that the catalog reads back.
5. Update the public API baseline, which the build will demand — see
   `CONTRIBUTING.md`. Read the entries it appends: on a catalog change they are
   the whole of what a consumer gains, stated in a few readable lines rather than
   buried in the generated diff.
6. Review the generated diff. On a template change it spans the whole catalog —
   read it as one change, not two hundred.

## Language

- Repository content is **English**: code, comments, documentation, commit
  messages, pull requests. The ADR base is bilingual — English is canonical, and
  each ADR carries a French translation alongside as `NNNN-title.fr.md`.
- Replying in French in a conversation is fine; writing French into the
  repository is not, unless the French documentation is what is being updated.

## Conventions the code will not enforce

- An attribute is inert. It declares no member, holds no behaviour, and performs
  no reflection ([ADR-0004](doc/handwritten/for-maintainers/adr/0004-keep-the-attribute-base-a-pure-marker.md)).
- A role is annotated on the declaration that introduces it, never on the
  implementations ([ADR-0010](doc/handwritten/for-maintainers/adr/0010-annotate-the-declaration-that-introduces-a-role.md)).
- A link between participants is a `Type`, never a string
  ([ADR-0008](doc/handwritten/for-maintainers/adr/0008-bind-participants-with-typed-links.md)).
- The generator never writes the public API baseline; a human does, and that act
  is the review
  ([ADR-0018](doc/handwritten/for-maintainers/adr/0018-hold-the-public-surface-to-a-committed-baseline.md)).
- Samples use realistic business domains, varied across the catalog, and the
  domain is chosen to fit the pattern rather than the reverse
  ([ADR-0012](doc/handwritten/for-maintainers/adr/0012-show-every-pattern-at-work-in-a-business-example.md)).
