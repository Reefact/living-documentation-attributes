# Gang of Four — the pattern guide

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](README-fr.md)

*Design Patterns: Elements of Reusable Object-Oriented Software* — Erich Gamma, Richard Helm, Ralph
Johnson and John Vlissides, Addison-Wesley, 1994. Twenty-three patterns, all twenty-three catalogued.

This guide is not the catalogue index. The [index](../../../generated/catalog-index.md#gang-of-four)
gives the annotation to type, what each role applies to, and where the sample is; it is generated,
complete, and consulted. These pages give what a pattern is for, when to reach for it, when not to, and
what it costs. They are written by hand, they are read rather than consulted, and they arrive one
catalogue at a time
([ADR-0040](../../for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.md)).

## Creational patterns

These concern how objects come into being: who decides the class, who calls the constructor, and how
much the calling code has to know.

| Pattern | What it is for |
|---|---|
| [Abstract Factory](AbstractFactory-en.md) | Creating whole families of parts that must be used together, the family being chosen once rather than at every `new`. |
| [Builder](Builder-en.md) | One construction sequence, several representations — the same steps producing text, HTML or a file. |
| [Factory Method](FactoryMethod-en.md) | A class knows when to create; a subclass decides what. |
| [Prototype](Prototype-en.md) | New objects by copying a configured instance, so the kinds that can be created become data rather than types. |
| [Singleton](Singleton-en.md) | One instance and a global point of access — and the page sets out why usually only the first half is wanted. |

## Structural patterns

These concern how objects are put together: what wraps what, what holds what, and what a caller is allowed
to see of the arrangement.

| Pattern | What it is for |
|---|---|
| [Adapter](Adapter-en.md) | Making a type usable through the interface a client expects, by translating between the two. |
| [Bridge](Bridge-en.md) | Two axes that vary separately — what a notification says, how it is delivered — kept apart instead of multiplied. |
| [Composite](Composite-en.md) | A tree whose whole and whose parts answer the same questions, so the recursion lives in the structure rather than in every caller. |
| [Decorator](Decorator-en.md) | Adding a responsibility around an object at run time, without touching it and without a class per combination. |
| [Facade](Facade-en.md) | One entry point in front of several subsystems, holding the order of operations that every caller would otherwise remember. |
| [Flyweight](Flyweight-en.md) | Forty thousand objects served by eleven, by separating what is shareable from what is not. |
| [Proxy](Proxy-en.md) | A stand-in with the same interface, controlling access to the real object — deferring, checking, or reaching across a network. |

## Behavioural patterns

Not written yet. Chain of Responsibility, Command, Interpreter, Iterator, Mediator, Memento, Observer,
State, Strategy, Template Method and Visitor — same as above.

## How a page is organised

Every page follows the same order.

| | |
|---|---|
| **Intent** | one sentence |
| **Problem** | the situation that makes the pattern worth considering, in code |
| **Solution** | what the pattern does about it |
| **Structure** | a class diagram of the roles |
| **The roles** | one line each, and the annotation that marks it |
| **The example** | the sample from `DesignPatternCatalog.Usage`, in pieces |
| **Applicability** | what the work itself states |
| **When not to use it** | the cases where the pattern costs more than it earns |
| **Advantages** and **Drawbacks** | two lists |
| **Relations with other patterns** | the neighbours, and what separates them |
| **Source** | the work, and links back to the index and the code |

## What these pages do not do

They do not invent. Where a work does not state something, the page says so rather than filling the
section — most often in *When not to use it*, which many works leave to the reader. Where a page reports
a judgement the field formed after the work was published, it says whose judgement it is. The
[Singleton](Singleton-en.md) page is the clearest case: the book lists benefits for that pattern and no
drawbacks at all, and the page marks the difference.
