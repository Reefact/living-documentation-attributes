# Contributing

Read [`AGENTS.md`](AGENTS.md) first: it carries the ADR check, the sequence for
adding a pattern, and the conventions the compiler will not enforce. This guide
covers the rest — how the repository is built, and how commits are written.

## Building

```
dotnet build Reefact.LivingDocumentation.Attributes.sln
dotnet test Reefact.LivingDocumentation.Attributes.sln
dotnet run --project Reefact.LivingDocumentation.Attributes.Usage
```

The library multi-targets `netstandard2.0` through `net8.0`; a change that
compiles on the newest target may not compile on the oldest, so build the
solution rather than one framework. The sample project prints the whole catalog
read back through the base attribute alone — that inventory is the check that a
catalog change landed.

The attributes carry no behaviour, so nothing tests what code does. The suite
asserts what every generated attribute must *look* like — it derives from the
base, it declares what it applies to, every role of one pattern answers one
identity, a link names a role of the same pattern — and what the reading rules
answer for each shape the generator emits. A defect written into the template is
emitted uniformly across the catalog and survives the round trip; these are what
catch it.

Alongside them, CI proves that every role compiles onto a plausible participant,
that the catalog is valid, that the whole of it reads back, and that regenerating
an unchanged catalog leaves the working tree clean.

## Changing the catalog

Never hand-edit a generated attribute, nor the catalog index. Edit
`catalog/<Catalog>/<Pattern>.json`, then:

```
python3 catalog/generate.py
python3 tools/catalog/validate.py     # needs: pip install -r tools/catalog/requirements.txt
```

## The public API baseline

These libraries ship public types and nothing else, so their public surface is the
whole of the product. It is declared in `<project>/PublicAPI/` — **one baseline per
package** since ADR-0027 split the catalogues, each shared by all six target
frameworks. The attributes are the same on every framework, so a shared file makes a
divergence between two targets a failure rather than something two baselines would
absorb; and a baseline per package means a change to one work's surface cannot hide
in another's diff.

An undeclared public symbol raises `RS0016`; a declared symbol that no longer
exists raises `RS0017`. Both are warnings locally and errors in CI, so a surface
change cannot merge until the same change updates the baseline.

**Accepting an intended surface change.** Update the baseline in the same commit:

```
dotnet format analyzers Reefact.LivingDocumentation.Attributes.<Catalog>/Reefact.LivingDocumentation.Attributes.<Catalog>.csproj \
  --diagnostics RS0016 --severity warn
```

That appends the new entries to `PublicAPI.Unshipped.txt`. A **removal** is
deleted by hand — deliberately, since a removal is a breaking change and deleting
the line is the moment to notice it.

The generator does not write the baseline, and must not: a baseline written by
the thing it checks would always agree with itself, and would rewrite itself to
match exactly the template change it exists to catch (ADR-0018).

Everything sits in `PublicAPI.Unshipped.txt` today because nothing has been
published. At the first release the accumulated entries are promoted to
`PublicAPI.Shipped.txt`.

## Versioning

The packages follow Semantic Versioning over **two** contracts, not one: what a
consumer compiles against, and what it reads back. The attributes carry no
behaviour and the libraries ship no reader, so a change can leave the public
surface byte-identical and still change every consumer's answers.

**One version number for all of them.** ADR-0027 splits the catalogues into
independent packages and prescribes releasing them in lockstep at first: loosening
that later is easy, and tightening it later is not. It lives in
`build/Packaging.props`.

| | |
|---|---|
| **Major** | a role or pattern removed or renamed · a target set narrowed · `AllowMultiple` or `Inherited` changed · a pattern moved between catalogs, which now moves it between packages · a relation added or removed · a reading rule changed |
| **Minor** | a pattern added · a role added to a published pattern · a target set widened · a link added to a role |
| **Patch** | documentation, samples, the catalog index, anything that reaches no consumer |

The two rows worth reading twice are in the major line. **A relation** — declaring
that one pattern narrows another — reads as an editorial remark and is in fact a
change to what `IdentityOf` answers for annotations already written. **A reading
rule** reads as documentation and is what consumers copy.

The version is below `1.0.0` and stays there until no catalogued pattern is
expected to move catalog: a pattern sits in `Idioms` because no body of work
claims it yet, and the day one does it changes namespace and package both. Below `1.0.0` the table
applies one step down — a breaking change moves the minor, everything else the
patch. Reasoning: ADR-0021.

## Enabling the commit-message hook

A `commit-msg` hook checks every message against the convention below before it
is recorded. It is versioned under `.githooks/`; enable it once per clone:

```
git config core.hooksPath .githooks
```

The same script runs in CI on every pull request, so a bypassed hook
(`git commit --no-verify`) is caught before merge. Merge commits are exempt.

## Commit convention

```
<type>[(<scope>[,<scope>...])][!]: <description>

<body>

<footers>
```

The header is validated in full and must fit within **72 characters**.

### Type

One of `feat`, `fix`, `build`, `chore`, `ci`, `docs`, `perf`, `refactor`,
`revert`, `style`, `test`.

### Scope

Optional, and names a component — never a file or a class. One of:

| Scope | What it covers |
|---|---|
| `attributes` | the generated attribute sources and the base marker |
| `catalog` | the JSON catalog, its schema and the generator |
| `usage` | the sample project |
| `doc` | the ADR base and the handwritten documentation |
| `build` | the build, the workflows and the development-time tooling |

Several scopes are comma-separated, with no space, unique and alphabetical:
`feat(attributes,catalog): …`.

A scope must say what the type does not, so one that repeats the type is left out
wherever it appears — not only where it stands alone. The type already says the
change is documentation, or the build; a scope beside it names the *other*
components reached. So `docs(doc)` is `docs:`, `docs(build,doc)` is
`docs(build):`, and `build(build)` is `build:`. The repetition is rejected.

### Description

Imperative and lowercase — *add*, not *Add* or *Added* — and no trailing period.

### Breaking changes

A breaking change is signalled **twice**: a `!` before the colon, and a
`BREAKING CHANGE:` footer describing the migration. One without the other is
rejected, because either alone is easy to miss — the `!` by a reader skimming
headers, the footer by tooling reading only the header.

```
feat(attributes)!: drop the role enumerations

BREAKING CHANGE: a consumer switching on a role enumeration now switches on
the attribute type instead.
```

### Issue footer

When a commit refers to an issue, the footer reads exactly `Refs: #<number>`.

### Autosquash placeholders

`fixup!`, `squash!` and `amend!` headers are allowed locally — the hook lets them
through so that a rebase can still absorb them — and rejected in CI, so that one
cannot land unsquashed in `main`.
