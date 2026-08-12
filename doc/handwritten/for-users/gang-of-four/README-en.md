# Gang of Four — the pattern guide

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](README-fr.md)

*Design Patterns: Elements of Reusable Object-Oriented Software* — Erich Gamma, Richard Helm, Ralph
Johnson and John Vlissides, Addison-Wesley, 1994. Twenty-three patterns, all twenty-three catalogued.

**This guide is not the catalogue index.** The
[index](../../../generated/catalog-index.md#gang-of-four) tells you the annotation to type, what each
role applies to, and where the sample is — it is generated, complete, and consulted. These pages tell
you what a pattern is for, when to reach for it, **when not to**, and what it costs. They are written
by hand, they are read rather than consulted, and they arrive one catalogue at a time
([ADR-0040](../../for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.md)).

## Creational patterns

They are about *how objects come into being* — who decides the class, who calls the constructor, and
how much the calling code has to know.

| Pattern | What it is for |
|---|---|
| [Abstract Factory](AbstractFactory-en.md) | Create whole **families** of parts that must be used together, choosing the family once instead of at every `new`. |
| [Builder](Builder-en.md) | One construction **sequence**, several representations — the same steps producing text, HTML or a file. |
| [Factory Method](FactoryMethod-en.md) | A class knows **when** to create, a subclass decides **what**. |
| [Prototype](Prototype-en.md) | New objects by **copying a configured instance**, so the kinds you can create become data rather than types. |
| [Singleton](Singleton-en.md) | One instance, and a global point of access — **and the page explains why you usually want only the first half**. |

## Structural patterns

Not written yet. Adapter, Bridge, Composite, Decorator, Facade, Flyweight, Proxy — all seven are
catalogued and annotated; only their guide pages are missing. Until they exist, the
[index entries](../../../generated/catalog-index.md#gang-of-four) and the samples under
[`DesignPatternCatalog.Usage/GangOfFour`](../../../../DesignPatternCatalog.Usage/GangOfFour) are what
there is.

## Behavioural patterns

Not written yet. Chain of Responsibility, Command, Interpreter, Iterator, Mediator, Memento, Observer,
State, Strategy, Template Method, Visitor — same as above.

## How to read a page

Each page follows the same order, and you can stop as soon as you have what you came for.

| | |
|---|---|
| **The problem** | the situation that makes the pattern worth considering, in code |
| **The solution** | what the pattern does about it |
| **Structure** | a class diagram of the roles |
| **The roles** | one line each, and the annotation that marks it |
| **The example** | the sample from `DesignPatternCatalog.Usage`, cut into pieces and read |
| **When to use it** | what the work itself says |
| **When not to use it** | the cases where it costs more than it earns |
| **What it costs** | gains and charges, side by side |
| **Patterns it is confused with** | the neighbours, and what actually separates them |
| **Where this comes from** | the work, and links back to the index and the code |

## What these pages will not do

**They do not invent.** Where a work does not state something, the page says so rather than filling
the section — most often in *When not to use it*, which many works leave to the reader. Where a page
reports a judgement the field formed after the work was published, it says whose judgement it is; the
[Singleton](Singleton-en.md) page is the clearest case, because the book lists benefits for it and no
drawbacks at all.
