# Contributing

Read [`AGENTS.md`](AGENTS.md) first: it carries the ADR check, the sequence for
adding a pattern, and the conventions the compiler will not enforce. This guide
covers the rest — how the repository is built, and how commits are written.

## Building

```
dotnet build Reefact.LivingDocumentation.Attributes.sln
dotnet run --project Reefact.LivingDocumentation.Attributes.Usage
```

The library multi-targets `netstandard2.0` through `net8.0`; a change that
compiles on the newest target may not compile on the oldest, so build the
solution rather than one framework. The sample project prints the whole catalog
read back through the base attribute alone — that inventory is the check that a
catalog change landed.

The attributes carry no behaviour, so there is no unit test suite to run. What CI
proves instead is that every role compiles onto a plausible participant, that the
catalog is valid, that the whole of it reads back, and that regenerating an
unchanged catalog leaves the working tree clean.

## Changing the catalog

Never hand-edit a generated attribute. Edit `catalog/<Catalog>/<Pattern>.json`,
then:

```
python3 catalog/generate.py
python3 tools/catalog/validate.py     # needs: pip install -r tools/catalog/requirements.txt
```

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
