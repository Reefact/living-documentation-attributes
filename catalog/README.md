# Pattern catalog

The data behind the attributes. One file per pattern, mirroring the layout of
`Reefact.LivingDocumentation.Attributes/`:

```
catalog/GangOfFour/Composite.json  ──generate.py──▶  Reefact.LivingDocumentation.Attributes/GangOfFour/Composite.cs
                                                 └─▶  doc/generated/catalog-index.md
```

Browsing the catalog is what [`doc/generated/catalog-index.md`](../doc/generated/catalog-index.md)
is for: every pattern, what to type to annotate each of its roles, what each role
may be applied to, and a link to its source and its sample. A directory listing
stops being navigable long before the catalog stops growing.

The generated `.cs` files are committed and are what ships. This folder is a
development-time tool: it exists so that the structure of an attribute — base
class, targets, `AllowMultiple`, `Inherited`, the three read properties — is
written once and cannot drift across the catalog. Only the content of a pattern
is authored here.

## Regenerating

```
python3 catalog/generate.py
```

Rewrites every `.cs` from the JSON. Running it on an unchanged catalog must
leave the working tree clean; that round-trip is what keeps the two in step.

## Adding a pattern

Add `catalog/<Catalog>/<Pattern>.json`, regenerate, review the diff.

```json
{
  "catalog": "GangOfFour",
  "name": "Composite",
  "summary": "Composes objects into tree structures to represent part-whole hierarchies, and lets clients treat individual objects and compositions uniformly.",
  "inherited": false,
  "roles": [
    { "name": "Component", "targets": ["Interface", "Class"], "links": [],
      "summary": "Declares the interface shared by the leaves and the composites of the tree." },
    { "name": "Leaf", "targets": ["Class", "Struct"], "links": ["Component"],
      "summary": "A terminal element of the tree: it has no children." }
  ]
}
```

`pattern.schema.json` describes the format and documents every field. Entries
can be checked against it with any JSON Schema tool, which is what makes a
catalog written in bulk reviewable: a missing role or a bad target is a
validation error rather than something to notice by reading.

Two rules the schema cannot state on its own, and which are worth checking too:
every name in `links` must be a role of the same pattern, and role names must be
unique within a pattern.

## Patterns deliberately left out

A pattern named by a body of work and absent from its catalog here reads as an
oversight. These are decisions, taken for one of two reasons, and recorded so
that the absence can be told apart from a gap
([ADR-0011](../doc/handwritten/for-maintainers/adr/0011-leave-out-what-cannot-be-annotated.md)).

**Nothing to attach it to.** C# offers no attribute below the assembly and above
the type, and a marker type invented to carry one would put an artefact of this
system into the code it documents.

| Pattern | Work | What it qualifies |
|---|---|---|
| Module | Domain-Driven Design | a namespace |
| Conformist, Customer/Supplier, Partnership, Separate Ways | Domain-Driven Design | the relationship *between* two bounded contexts |
| Context Map | Domain-Driven Design | the whole landscape — it is what you draw *from* the annotations |
| Segregated Core, Abstract Core, Highlighted Core, Distillation Document, Domain Vision Statement | Domain-Driven Design | an act of refactoring, or a document; what they produce is already expressible, a distilled core being an assembly annotated `CoreDomain`, and the abstract core of a framework being a role of `PluggableComponentFramework` |
| Guard Clause | — | a shape a method body takes; nothing holds a role in it |

**Nothing a tool could check.** A role licenses no verifiable assertion, so an
attribute would name it without letting anything range over it — the criterion of
[ADR-0007](../doc/handwritten/for-maintainers/adr/0007-decide-sameness-by-the-assertions-a-pattern-carries.md),
applied to whether a pattern belongs here at all.

| Pattern | Work | Why |
|---|---|---|
| Intention-Revealing Interfaces | Domain-Driven Design | asks that names come from the ubiquitous language; nothing mechanical distinguishes a good name from a bad one |
| Conceptual Contours | Domain-Driven Design | a judgement about where a model's seams fall, not a property of a declaration |
| Ubiquitous Language, Continuous Integration, Evolving Order | Domain-Driven Design | practices of a team, not participants in code |
| Model-Driven Design, Hands-On Modellers, Declarative Design, System Metaphor | Domain-Driven Design | ways of working, or of thinking about a model; a codebase can follow all four and no declaration is a participant in any |
| Responsibility Layers | Domain-Driven Design | what the pattern asserts is an **order** — each layer depends only on those beneath it, and the layers are ranked by rate of change — and nothing in this vocabulary orders assemblies. Taking the five Evans names (Potential, Operations, Decision Support, Policy, Commitment) as fixed roles would supply one, but those are the layers he found in a shipping domain, offered as an illustration; the pattern is finding your own |
| Big Ball of Mud | Foote and Yoder, *Pattern Languages of Program Design 4*, 2000 | what it asserts about a participant is that it has no discernible structure, which is the absence of an assertion rather than one. Reached through Evans, who uses it to characterise a neighbouring context, and decided on the same criterion that admits Smart UI ([ADR-0023](../doc/handwritten/for-maintainers/adr/0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md)) |

**Anti-patterns are not excluded as a category.** `SmartUi` is catalogued, because
Evans names it, a class or an assembly holds it, and it licenses assertions — the
usual three. It is the only entry whose assertions *exempt* rather than constrain,
and [ADR-0023](../doc/handwritten/for-maintainers/adr/0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md)
records why that is admitted rather than special-cased.

## Named by another work

A book may present a pattern it did not name. It is catalogued where the work that
named it put it ([ADR-0006](../doc/handwritten/for-maintainers/adr/0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md)),
which means the catalog a reader reaches a pattern through is not always the one
that holds it.

| Pattern | Presented in | Named by | Held in |
|---|---|---|---|
| Knowledge Level | *Domain-Driven Design*, chapter 16 | Fowler, *Analysis Patterns*, 1997 — chapter 2, as the accountability knowledge level | `AnalysisPatterns` |

This is the case that opened `AnalysisPatterns`. Knowledge Level had been recorded
here as annotable and wanted, waiting on a catalog rather than on work: two classes
hold the two levels, and the assertions are the checkable kind — an operational
object refers to its knowledge-level counterpart and never the reverse, and the
knowledge level changes by configuration rather than by code. Admitting the book
was a decision of its own
([ADR-0024](../doc/handwritten/for-maintainers/adr/0024-admit-a-model-of-the-business-to-the-catalog.md)),
because its patterns are models of the business rather than shapes the code takes.

## Chapters catalogued whole, including what a contents page does not name

Chapter 2 of *Analysis Patterns* is complete: its nine sections are catalogued, and
so are two patterns the section titles do not name. **Chapters 3 and 5 are complete
too** — twelve sections and four, every one of which held up against the criteria,
which is not something to assume of the chapters still to come: chapter 7 is a worked
example whose sections are steps rather than patterns, and most of chapter 11
qualifies a package, which C# gives nothing to annotate.

Chapter 5 is worth a note of its own because its four entries are about *identity*,
and three of them are distinguished from one another only by what they permit.
`Name` permits several and permits none. `IdentificationScheme` permits several and
scopes uniqueness to an issuer. `ObjectMerge` asserts sameness on the system's own
authority; `ObjectEquivalence` records who asserts it and so admits disagreement —
which is the difference between a duplicate cleaned up and two catalogues that do
not concur.

One departure from the naming convention below is worth stating here rather than
leaving to be noticed. Section 3.10 is titled *Active Observation, Hypothesis, and
Projection*; its faithful PascalCase is unreadable, so the entry is `ActiveObservation`
and the other two are roles of it. That is a judgement against the rule, taken because
the rule produced an unusable name, and it is the kind of thing a maintainer may
reverse.

`Leveled Accountability Type` and `Directional Accountability Type` appear only in
figures 2.12 and 2.13, as «overlapping» siblings of the hierarchic one. They meet
the three criteria like any other entry — the work names them, a class holds them,
they license assertions — so the only question was whether a name given in a
diagram is *the name the work gave it* under
[ADR-0006](../doc/handwritten/for-maintainers/adr/0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md).
It is: the rule is about which work names a pattern, not about which part of the
work. So the convention this chapter sets is **the section title where the book has
one, the diagram's class name where it does not** — which is why
`HierarchicAccountability` carries the section-2.7 spelling while its two siblings
carry theirs from the figures.

Three entries were resolved rather than catalogued as they stood, and how they were
resolved is worth recording because each turned on a comparison rather than on a
judgement:

| Pattern | How it was settled |
|---|---|
| Party Type Generalizations | **An entry of its own.** Figure 2.10 asserts what `KnowledgeLevel` does not: a supertype / subtypes hierarchy on *Party Type* itself, plus a derived closure that the accountability type's constraint ranges over instead of the immediate type |
| Organization Structure | **A specialisation of `Accountability`.** Figures 2.6 and 2.7 are drawn as 2.8 is, with *organization* for *party* and *parent* / *subsidiary* for *commissioner* / *responsible*. Restricting both ends to organizations is an added assertion, so it is narrower rather than the same thing spelled differently — [ADR-0007](../doc/handwritten/for-maintainers/adr/0007-decide-sameness-by-the-assertions-a-pattern-carries.md) |
| Organization Hierarchies | **An entry of its own, and not that one.** Figure 2.4 has *no type object at all* — the admissible nesting is an invariant on each subtype — and figure 2.5 is Fowler showing why that collapses once a second structure appears. What it asserts is that one parent suffices, which is a claim about a participant and not merely a smaller version of the structure pattern |
## When a later catalog turns out to hold the narrower pattern

*Analysis Patterns* is 1997, which makes it the earliest of the four works
catalogued here. So cataloguing it does not only add entries: where it names a
pattern one of the later books also names, the **earlier publication holds the
definition** and the later entry declines or narrows from it
([ADR-0006](../doc/handwritten/for-maintainers/adr/0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md)).
That reaches back into catalogs already declared complete.

| Entry | Was | Is now | Why |
|---|---|---|---|
| `EnterpriseApplicationArchitecture/Money` | a pattern of its own | a **specialisation of `AnalysisPatterns/Quantity`** | money is an amount with a unit and arithmetic that refuses to mix units — Fowler's 1997 quantity. What makes it narrower rather than an instance is allocation: no other quantity has to divide without losing anything |

A rule written for quantities now finds money, which is the point of recording the
relation rather than leaving two unrelated entries. Nothing about the `Money`
annotation changes for a consumer that asks for money.

**This will happen again.** Chapter 12 of the same book names Two-Tier Architecture,
Three-Tier Architecture and Presentation and Application Logic; chapter 13 names
Application Facade. If those are the patterns `DomainDrivenDesign/LayeredArchitecture`
(2003) and the enterprise catalog's facades (2002) hold, then those entries are the
ones that move. Deciding it is ADR-0007 applied each time, and the anteriority is
not negotiable.

## Shape of the generated attribute

A pattern whose single role carries the pattern's own name is emitted flat, so
that it reads as ubiquitous language:

```csharp
[ValueObject] public readonly record struct Money(decimal Amount, string Currency);
```

Every other pattern is emitted as a static container holding one sealed
attribute per role, so that an annotation still reads as *this is a X* rather
than *this belongs to pattern X*:

```csharp
[Composite.Component]                       public interface INode { }
[Composite.Leaf(Component = typeof(INode))] public sealed class FileNode : INode { }
```

A role targeting `Method` is a member role. Nothing else distinguishes it: it is
generated exactly like the others, and consumers tell them apart by reading
`AttributeUsage`.

## Held back for want of a source

`Idioms` is for a pattern that has a **source** but no body of work of its own
(ADR-0013) — it names the absence of a catalog, not the absence of a source, and
every entry must record a reference with a year, because that reference is what
orders a declension (ADR-0006).

Two everyday practices fail that, and one of them is named in ADR-0013 itself as
the example of an Idioms candidate:

| Held back | What was looked for, and found |
|---|---|
| `Result` | No publication names it as a pattern. Its lineage runs through Haskell's `Either` — a general sum type, not error handling — and Rust's `Result`, a standard-library type rather than a named pattern. Recording either as *the work that named it* would be false. |
| `Option` / `Maybe` | Same shape of problem, with a better-looking answer that does not survive inspection: `option` is defined in *The Definition of Standard ML* and `Maybe` in the Haskell report, but those works define a **type**, not a pattern, and neither is a body of work about patterns. |

Both would be useful entries. Admitting them means one of two things, and neither
is a detail: find a publication that genuinely named the practice, or decide that
`Idioms` may hold a pattern whose provenance is a lineage rather than a work —
which is a change to ADR-0013 and to the schema's required reference, and belongs
in a record of its own rather than in a catalog entry written quietly.

`GuardClause` was looked at with them and is closed rather than held back: it is a
shape a method body takes, so nothing holds a role and nothing can be asserted
about a participant (ADR-0011).
