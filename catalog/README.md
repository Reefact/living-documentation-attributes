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
| Segregated Core, Abstract Core, Highlighted Core, Distillation Document | Domain-Driven Design | an act of refactoring, or a document; what they produce is already expressible, a distilled core being an assembly annotated `CoreDomain` |
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

**Not settled either way.** Responsibility Layers, Knowledge Level and Pluggable
Component Framework could each plausibly be attached to something. They are absent
because nobody has written down the assertions they would license, not because they
were judged unannotable — the criterion for opening them is the one above, applied
honestly.

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
