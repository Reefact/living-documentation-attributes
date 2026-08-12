# design-pattern-catalog — guide for Claude Code

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

Everything under `DesignPatternCatalog.<Catalog>/` is generated
from `catalog/<Catalog>/<Pattern>.json`, and so is
`doc/generated/catalog-index.md`. One project per catalogued work, each shipped as
its own package. Edit the catalog, then:

```
python3 catalog/generate.py
```

Regenerating an unchanged catalog must leave the working tree clean.

## The pattern guide is hand-written, and nothing checks it

`doc/handwritten/for-users/<catalog>/<Pattern>-en.md` and `-fr.md` teach a pattern —
what it is for, when to reach for it, when not to, what it costs — and they are
written by hand
([ADR-0040](doc/handwritten/for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.md)).
The generator does not touch them and no test compares them to the catalog, so two
rules have to be kept by hand:

* **A change to a pattern obliges its pages.** Rename a role, rewrite its sample,
  admit or exclude an entry — then update `<Pattern>-en.md` *and* `<Pattern>-fr.md`,
  and the catalog's `README-*.md` if the list of pages changed. A page quoting a
  role that no longer exists fails nothing and misleads everyone.
* **Never fill a section the work does not support.** *When not to use it* is the
  section most worth having and the one many works leave unsaid. Mark it empty and
  say why. Where a page reports a judgement formed after the work was published,
  say whose it is — the [Singleton](doc/handwritten/for-users/gang-of-four/Singleton-en.md)
  page does, because the book lists benefits for it and no drawbacks. **The guide
  may be incomplete; it may not be plausible.**

## Build & run

```
dotnet build DesignPatternCatalog.sln
dotnet run --project DesignPatternCatalog.Usage
```

The library multi-targets `netstandard2.0` through `net8.0`; a change that
compiles on the newest target may not compile on the oldest, so build the
solution rather than one framework. The sample project prints the whole catalog
read back through the base attribute alone — that inventory is the check that a
catalog change landed.

## Language

Repository content is English — code, comments, the catalog, the generated index,
`README.md`, `AGENTS.md`, `CONTRIBUTING.md`.

**Two exceptions, and both are bilingual rather than French.**

| | |
|---|---|
| The **ADR base** | English canonical, each record accompanied by `NNNN-title.fr.md`. The suffix says which text is the record. |
| The **pattern guide** | `<Pattern>-en.md` beside `<Pattern>-fr.md`. Neither is canonical: a guide carries no authority to protect, so the two files are peers and the symmetric names say so. Write both, or write neither. |

Anywhere else, French in the repository is a defect. Replying in French in the
conversation is fine.
