# living-documentation-attributes

A vocabulary of .NET attributes that lets code state **which design pattern a type
or a member participates in**.

```csharp
[Composite.Component]
public interface INode { }

[Composite.Leaf(Component = typeof(INode))]
public sealed class FileNode : INode { }

[Composite.Composite(Component = typeof(INode))]
public sealed class FolderNode : INode { }
```

That is the whole of it. The attributes carry no behaviour, hold no dependency and
run no code — they are declarative data, and what you build on top of them is
yours: an inventory, a diagram, an architecture rule, a review checklist.

## Why

A codebase already contains its patterns; what it does not contain is the fact
that it contains them. `OrderRepository` is a repository because of its name, and
`Cache` might be a decorator, a proxy, or neither. The information lives in a
reviewer's head, in a wiki that is out of date, or nowhere.

An annotation puts it in the one place that cannot drift from the code, and makes
it machine-readable on the way — which is what turns "we use a hexagonal
architecture" from a claim into something a build can check.

## What is in it

| Catalog | Patterns | Source |
|---|---|---|
| `GangOfFour` | 23 | Gamma, Helm, Johnson, Vlissides — *Design Patterns*, 1994 |
| `DomainDrivenDesign` | 23 | Evans — *Domain-Driven Design*, 2003 |
| `EnterpriseApplicationArchitecture` | 51 | Fowler — *Patterns of Enterprise Application Architecture*, 2002 |
| `AnalysisPatterns` | 23 | Fowler — *Analysis Patterns*, 1997 |
| `Idioms` | 2 | patterns with a source but no catalog of their own — each entry names its own |

**122 patterns, 225 roles** today, and the catalog is meant to grow by an order of
magnitude. A pattern is catalogued where the work that named it put it, under the
name that work gave it — so a reader of a book finds its patterns spelled as it
spelled them.

**[Browse the catalog](doc/generated/catalog-index.md)** — every pattern, the
annotation to type for each of its roles, what each role may be applied to, and a
link to its source and to a worked example.

## Installing

```
dotnet add package Reefact.LivingDocumentation.Attributes
```

Targets `netstandard2.0` through `net8.0`, so it reaches .NET Framework 4.6.1 and
everything after it. It has no dependencies.

*Nothing is published yet* — the first release is still ahead, and until then the
package is built from source.

## How an annotation is written

A pattern with several roles is a container, and each role is its own attribute
nested in it:

```csharp
[Visitor.Element]     public interface IExpression { }
[Visitor.AcceptMethod] TResult Accept<TResult>(IExpressionVisitor<TResult> visitor);
```

A pattern with a single role is flat, and reads as the ubiquitous language:

```csharp
[ValueObject]  public readonly record struct ParcelId(string CadastralReference);
[Entity]       public sealed class Parcel { }
```

Two conventions are worth knowing, because nothing enforces them:

* **Annotate the declaration that introduces a role**, never the implementations.
  A role introduced by an interface is annotated on that interface — the type
  graph already says which classes implement it, and annotating each would count
  one role several times.
* **A link is a `Type`**, not a name. `Component = typeof(INode)` binds
  participants of *one* occurrence of a pattern, so a codebase with three
  composites can still be told apart. It is optional, and only needed where the
  type hierarchy does not already say it.

## How it is read back

The library publishes no reader, on purpose: a consumer already holds the
attribute's type, and everything about a pattern is carried by the shape of its
declaration rather than stored a second time. Four rules turn one into the other,
and they are documented on `LivingDocumentationAttribute` so they travel with the
package:

| | |
|---|---|
| **Catalog** | the **first** namespace segment below the root — the first, so an organisational sub-namespace folds into the catalog it belongs to |
| **Pattern name** | the declaring type; a single-role pattern has no container and carries its own name |
| **Role name** | the attribute type name, without its `Attribute` suffix |
| **Pattern identity** | the type reached by climbing through an abstract base *declared in the same pattern*, and through a declension |

Group by the **identity**, never by the pattern name: `Adapter` names one pattern
in Gang of Four and an unrelated one in ports and adapters, and grouping by name
merges them silently.

The sample project carries a working reader — around a hundred lines — that
applies all four. It is meant to be copied and owned, not depended upon.

## Two patterns can be related, in two ways

The distinction is the reason this is a vocabulary rather than a list of tags.

* **Specialisation** — the narrower pattern derives from the broader one. Evans'
  value object narrows Fowler's: every one of the first is one of the second,
  while a mutable date range satisfies Fowler's rule and fails Evans'. Both stay
  countable patterns, and a rule written for the broader one reaches the narrower.
* **Declension** — the same pattern, catalogued twice, under the same name or
  another one. Neither is narrower; both spellings resolve to a single identity,
  so a reader of either catalog finds the pattern where they look for it and
  nothing is counted twice. It carries a `[Declension]` marker, because that is
  the one thing inheritance cannot say on its own.

Whether two entries are one pattern is decided by **the assertions they carry**,
never by their names.

## What it deliberately does not do

* No behaviour, no reflection, no runtime cost. An attribute is inert.
* No reader, no analyzer, no rule engine. Those are yours to write, against a
  vocabulary that is stable and checked by the compiler.
* No pattern that cannot be attached to a type, a member or an assembly. A
  `Module` qualifies a namespace, and C# has no namespace-level attribute, so it
  is absent rather than approximated.

## The reasoning

Almost nothing this library decides is defended by the compiler, and the
attributes are generated from a catalog, so a reader of the output cannot tell a
decided trait from an incidental one. The reasoning is kept in the
[ADR base](doc/handwritten/for-maintainers/adr/) — 24 records, English
canonical with a French translation alongside — and that is where to look when a
shape seems arbitrary.

Contributions are welcome: [`CONTRIBUTING.md`](CONTRIBUTING.md) covers the build
and the commit convention, and [`AGENTS.md`](AGENTS.md) states what an agent — or
a contributor — is expected to do without being asked.

## Licence

Apache-2.0.
