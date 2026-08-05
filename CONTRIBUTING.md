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

Never hand-edit a generated attribute. Edit `catalog/<Catalog>/<Pattern>.json`,
then:

```
python3 catalog/generate.py
python3 tools/catalog/validate.py     # needs: pip install -r tools/catalog/requirements.txt
```

## The public API baseline

This library ships public types and nothing else, so its public surface is the
whole of the product. It is declared in
`Reefact.LivingDocumentation.Attributes/PublicAPI/`, one baseline shared by all
six target frameworks — the attributes are the same on every one of them, and a
shared file makes a divergence between two targets a failure rather than
something two baselines would absorb.

An undeclared public symbol raises `RS0016`; a declared symbol that no longer
exists raises `RS0017`. Both are warnings locally and errors in CI, so a surface
change cannot merge until the same change updates the baseline.

**Accepting an intended surface change.** Update the baseline in the same commit:

```
dotnet format analyzers Reefact.LivingDocumentation.Attributes/Reefact.LivingDocumentation.Attributes.csproj \
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
