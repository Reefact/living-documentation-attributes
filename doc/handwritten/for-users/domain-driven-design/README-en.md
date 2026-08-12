# Domain-Driven Design — the pattern guide

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](README-fr.md)

*Domain-Driven Design: Tackling Complexity in the Heart of Software* — Eric Evans, Addison-Wesley, 2003.
Twenty-three patterns catalogued; the ones the book names and that C# has somewhere to put
([the list of deliberate omissions](../../../../catalog/README.md#patterns-deliberately-left-out) says
which are missing and why).

This guide is not the catalogue index. The
[index](../../../generated/catalog-index.md#domain-driven-design) gives the annotation to type, what each
role applies to, and where the sample is; it is generated, complete, and consulted. These pages give what
a pattern is for, when to reach for it, when not to, and what it costs. They are written by hand, they
are read rather than consulted, and they arrive one catalogue at a time
([ADR-0040](../../for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.md)).

## The building blocks of a model-driven design

These are the parts a model is made of, and the two patterns that decide whether there is a model at all.

| Pattern | What it is for |
|---|---|
| [Layered Architecture](LayeredArchitecture-en.md) | Isolating the model from the screen, the coordination and the plumbing, so that a rule can live in one place every caller reaches. |
| [Smart UI](SmartUi-en.md) | The opposite, named by the book as the anti-pattern — and given the circumstances under which it is nonetheless right. |
| [Entity](Entity-en.md) | An object the domain needs to point at: *this one*, whatever has changed about it since. |
| [Value Object](ValueObject-en.md) | An object described only by its values, with no identity and nothing to track. |
| [Service](Service-en.md) | An operation that is genuinely an operation, belonging to no entity and to no value object. |
| [Aggregate](Aggregate-en.md) | A boundary with one object in charge of it, so that an invariant spanning several can actually be enforced. |
| [Factory](Factory-en.md) | Creation as an act in its own right, so that what comes out was never half built. |
| [Repository](Repository-en.md) | The illusion of a collection, so that the model can ask for aggregates without learning where they are kept. |

## Refactoring toward deeper insight

These are about making a model supple once it exists: what a rule is allowed to be, what an operation
promises, and how much a reader has to hold in mind before trusting either.

| Pattern | What it is for |
|---|---|
| [Specification](Specification-en.md) | A business rule as an object — named, combinable, and asked by everyone instead of reimplemented by each. |
| [Assertion](Assertion-en.md) | The contract stated rather than inferred: what an operation promises, and what is true of a type at every instant. |
| [Side-Effect-Free Function](SideEffectFreeFunction-en.md) | An operation that answers and changes nothing, so it can be tried, repeated and discarded freely. |
| [Closure of Operation](ClosureOfOperation-en.md) | An operation that takes and returns its own type, so results feed back in and no dependency is introduced. |
| [Standalone Class](StandaloneClass-en.md) | A type that depends on nothing, and can therefore be read in one sitting and tested with values alone. |

## Strategic design

These apply above the type: where one model stops and another begins, how two models meet, and which of
them is worth the effort. Most of them are annotated on an assembly, because that is the smallest thing
in C# that can make one claim about all the code it holds.

**Where a model stops, and how two models meet** — chapter 14.

| Pattern | What it is for |
|---|---|
| [Bounded Context](BoundedContext-en.md) | The boundary of one model, inside which a word has one meaning and outside which it may mean something else. |
| [Shared Kernel](SharedKernel-en.md) | The deliberate exception: a small subset two teams share and change only by agreement. |
| [Anticorruption Layer](AnticorruptionLayer-en.md) | A wall with three jobs in it, so that an upstream model you cannot change never reaches yours. |
| [Open Host Service](OpenHostService-en.md) | One protocol designed for all comers, instead of one integration negotiated per consumer. |
| [Published Language](PublishedLanguage-en.md) | A documented vocabulary for exchange — not the internal model with a serialiser bolted on. |

**Which part is worth the effort** — chapters 15 and 16.

| Pattern | What it is for |
|---|---|
| [Core Domain](CoreDomain-en.md) | The part that makes the product worth writing, marked so that effort can be directed rather than distributed. |
| [Generic Subdomain](GenericSubdomain-en.md) | Necessary and undistinctive — the part that could be bought, and that the best people should leave alone. |
| [Cohesive Mechanism](CohesiveMechanism-en.md) | A solver taken out from behind the concepts, so that a pipe stays a pipe. |
| [Pluggable Component Framework](PluggableComponentFramework-en.md) | A frozen core several teams implement, so that a component built in 2031 runs under an application from 2011. |

## Held

**Domain Event** is catalogued and has no page. The catalog credits it to *Domain-Driven Design*, 2003,
and the pattern does not appear under that name in the 2003 book — Evans names it in the *Domain-Driven
Design Reference* of 2015, and Martin Fowler had published a *Domain Event* on his site in 2005. A page
here would have to state a source, and this guide does not state one it cannot stand behind. The page
waits on the catalog rather than the other way round.

## How a page is organised

Every page follows the same order.

| | |
|---|---|
| **Intent** | one sentence |
| **Problem** | the situation that makes the pattern worth considering, in code |
| **Solution** | what the pattern does about it |
| **Structure** | a diagram of the roles — a class diagram, or a diagram of assemblies where the roles apply to assemblies |
| **The roles** | one line each, and the annotation that marks it |
| **The example** | the sample from `DesignPatternCatalog.Usage`, in pieces |
| **Applicability** | what the work itself states |
| **When not to use it** | the cases where the pattern costs more than it earns |
| **Advantages** and **Drawbacks** | two lists |
| **Relations with other patterns** | the neighbours, and what separates them |
| **Source** | the work, and links back to the index and the code |

## What these pages do not do

They do not invent. Where the book does not state something, the page says so rather than filling the
section, and where a page reports a judgement the field formed after 2003 it says whose judgement it is —
the aggregate boundary and contention, the anaemic domain model, the entity mistaken for a table.

Two consequences are worth naming for this catalogue in particular.

**Evans does not write an *Applicability* section.** The book argues in prose and ends each pattern with a
*Therefore*. What appears here under *Applicability* is drawn from those, not from a list the book
provides, and it is kept to what the book actually says.

**The book states its own limits more often than most.** The *Smart UI* page is the clearest case: Evans
names it the anti-pattern and then gives it a list of real advantages, and the page carries that list as
his rather than converting it into a warning.
