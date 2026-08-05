# living-documentation-attributes — guide for Claude Code

A vocabulary of attributes that let code state which design pattern a type or a
member participates in. The attributes carry no behaviour, so almost nothing this
repository decides is defended by the compiler.

**Read [`AGENTS.md`](AGENTS.md) before changing anything.** It is the operative
document: the ADR check, how to add a pattern, and the conventions the code will
not enforce for you. What follows is the short version.

## The rule that matters most

Check every pull request against the ADR base under
`doc/handwritten/for-maintainers/adr/`
([ADR-0001](doc/handwritten/for-maintainers/adr/0001-check-every-pull-request-against-the-adr-base.md)).
Most changes embark no decision and add no ADR — the check is mandatory, the
artifact is not. Draft as `Proposed`; never accept, supersede or deprecate an ADR
yourself.

The reasoning behind this repository is not recoverable from its output: the
attributes are generated, so a reader cannot tell a decided trait from an
incidental one. If it is not in the ADR base, it is lost.

## Never hand-edit a generated attribute

Everything under `Reefact.LivingDocumentation.Attributes/<Catalog>/` is generated
from `catalog/<Catalog>/<Pattern>.json`, and so is
`doc/generated/catalog-index.md`. Edit the catalog, then:

```
python3 catalog/generate.py
```

Regenerating an unchanged catalog must leave the working tree clean.

## Build & run

```
dotnet build Reefact.LivingDocumentation.Attributes.sln
dotnet run --project Reefact.LivingDocumentation.Attributes.Usage
```

The library multi-targets `netstandard2.0` through `net8.0`; a change that
compiles on the newest target may not compile on the oldest, so build the
solution rather than one framework. The sample project prints the whole catalog
read back through the base attribute alone — that inventory is the check that a
catalog change landed.

## Language

Repository content is English. The ADR base is bilingual, English canonical, each
ADR accompanied by `NNNN-title.fr.md`. Replying in French in the conversation is
fine; writing French into the repository is not.
