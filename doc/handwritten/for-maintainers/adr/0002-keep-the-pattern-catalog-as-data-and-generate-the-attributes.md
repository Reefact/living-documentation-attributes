# ADR-0002 | Keep the pattern catalog as data and generate the attributes from it

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-05
**Accepted:** 2026-08-05
**Decision Makers:** Reefact

## Context

The library publishes one attribute per role, across bodies of work that between
them describe several hundred patterns. Every attribute has the same structure —
a base, a target set, a multiplicity, an inheritance rule — and differs only in
its content: which roles exist, what each one does.

The first thirty-one patterns were written by hand with the assistance of an
agent. Structure drifted across them in ways no compiler could catch: twelve role
sets were missing canonical roles, one attribute did not derive from the base and
was therefore invisible to any generic reader, four carried no target declaration
at all, and the `sealed` and `Inherited` modifiers were applied inconsistently.
Every file was plausible read on its own; the defects were only visible by
comparing files against each other.

An earlier attempt at generation existed in the repository and was abandoned: a
Visual Studio custom tool, reachable only from a registered extension on a
Windows workstation, which nothing could run from a command line, from another
editor, or on another machine. Its entry point was never written. Its output
shape had meanwhile diverged from the hand-written files, so the two models
coexisted and disagreed.

The content of a pattern — its roles, what each participant does, which work
introduced it — is editorial. It is written and reviewed by a human, and is the
part that carries the value of the library. Its structure is not: it is the same
decision, taken once, repeated several hundred times.

## Decision

The catalog is authored as data, one file per pattern, and the attribute sources
are produced from it by a development-time generator whose output is committed.

## Rationale

Separating the two puts each part where it can be checked. Content authored as
data can be validated by a schema — a missing role, an unknown target, a link to
a role that does not exist become errors instead of things to notice by reading.
Structure emitted by one template cannot drift, because there is only one of it:
the failure mode that produced the first thirty-one entries is removed by
construction rather than by attention.

Committing the output and shipping only that keeps the cost of generation inside
the repository. Consumers receive an assembly and never meet the catalog, the
generator, or its dependencies. Nothing about the build of a consuming project
changes, and the generated files stay readable and reviewable in a pull request —
which matters, because they are the only artifact that ships.

A development-time tool avoids the failure of the abandoned one. The generator is
run deliberately, by whoever edits the catalog, and its result is reviewed as a
diff like any other change; it needs no editor, no extension, and no build
integration on any machine but the one editing the catalog.

The data outlives the attributes. The same catalog can produce an index, a
documentation site, a schema for consumers who declare vocabularies of their own —
outputs that would each require re-deriving the content from source code if the
content lived only in source code.

## Alternatives Considered

### Keep writing the attributes by hand

Considered because the attributes are small, the structure is simple, and the
repository would carry no tool at all.

Rejected on the evidence of the first thirty-one: the structure did drift, in
five distinct ways, and none of them was caught. At the scale the catalog is
heading for the same failure would repeat several hundred times, and a reviewer
comparing files against each other is not a mechanism.

### Emit the attributes with a Roslyn source generator

Considered because it is the modern instrument for this shape of problem, and
because it would remove the generated files from the repository entirely.

Rejected because the generated attributes are the shipped artifact and their
readability is a feature: a maintainer reads them, reviews them in a diff, and a
consumer navigates into them from their own code. A source generator would also
put the generation on every consumer's build for no benefit, since the catalog it
reads is ours and fixed at publication.

### Keep the abandoned custom tool

Considered because it existed and its rendering worked.

Rejected because it could only ever run inside one editor on one operating
system, which is why its entry point was never written and why the two models had
already diverged.

### Generate the attributes with an agent rather than a template

Considered because an agent writes the content well and would need no tool.

Rejected because it is precisely how the first thirty-one were produced. An agent
is the right instrument for the content and the wrong one for the structure: the
defects it leaves are plausible, uniform in appearance, and invisible without a
rule to check against.

## Consequences

### Positive

* Structure is written once and cannot vary across the catalog.
* Content is validated by a schema instead of being reviewed by reading.
* Adding a pattern is an edit to one small data file, whatever the size of the
  catalog.
* The catalog can feed outputs other than the attributes.

### Negative

* The repository carries a tool and its language, neither of which ships.
* Two artifacts must be kept in step; regenerating from an unchanged catalog must
  leave the working tree clean, and nothing enforces that but the maintainer
  running it.

### Risks

* A generated file edited by hand would be silently overwritten at the next run.
  Mitigated only by the round trip being cheap enough to run habitually.
* The template becomes a single point of failure: a defect in it reaches every
  pattern at once. Mitigated by the same regeneration surfacing it across the
  whole diff rather than in one file.

## Follow-up Actions

* Keep the round trip verifiable: regenerating an unchanged catalog leaves no
  diff.
* Generate an index of the catalog, without which several hundred patterns are
  not navigable.

## References

* `catalog/README.md` — how the catalog is authored and regenerated.
* `catalog/pattern.schema.json` — what a catalog entry must satisfy.
* [ADR-0001](0001-check-every-pull-request-against-the-adr-base.md) — why
  generation makes recorded decisions necessary.
* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) —
  the shape the generator emits.
