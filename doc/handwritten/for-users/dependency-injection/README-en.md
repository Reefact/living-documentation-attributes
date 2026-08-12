# Dependency Injection — the pattern guide

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](README-fr.md)

*Dependency Injection Principles, Practices, and Patterns* — Steven van Deursen and Mark Seemann, Manning,
2019. Eleven items catalogued, and all eleven written up here.

The book names fourteen in three catalogue sections. The three **code smells** of chapter 6 are deliberately
not catalogued, on a reason
[ADR-0037](../../for-maintainers/adr/0037-admit-the-dependency-injection-catalogue.md) records: the degree
word. *Over*-injection is a judgement about how much, and this catalogue holds shapes rather than amounts.

This guide is not the catalogue index. The
[index](../../../generated/catalog-index.md#dependency-injection-principles-practices-and-patterns) gives the
annotation to type, what each role applies to, and where the sample is; it is generated, complete, and
consulted. These pages give what a pattern is for, when to reach for it, when not to, and what it costs. They
are written by hand
([ADR-0040](../../for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.md)).

Every sample in this catalogue is one system — a community radio station's playout — and the pages cross-refer
because the code does. The nineteen resolve calls in the composition root's story are the same nineteen the
service locator page counts down from.

## The patterns

Chapter 4. How a class is given what it needs, and where the giving happens.

| Pattern | What it is for |
|---|---|
| [Composition Root](CompositionRoot-en.md) | One place where the object graph is assembled, so that everything else is composed rather than composing. |
| [Constructor Injection](ConstructorInjection-en.md) | The default: what a class requires, declared where an instance cannot exist without it. |
| [Method Injection](MethodInjection-en.md) | For a dependency that belongs to the call rather than to the instance. |
| [Property Injection](PropertyInjection-en.md) | For a genuinely optional dependency, behind a default that genuinely works. |

## The anti-patterns

Chapter 5. Four ways a class stops declaring what it depends on. The book presents all four as defects, and
these pages do not soften that — but they do record why each one appears in code nobody was careless about.

| Anti-pattern | What it is |
|---|---|
| [Control Freak](ControlFreak-en.md) | A class that constructs its own dependencies, so nothing outside — including a test — can replace them. |
| [Service Locator](ServiceLocator-en.md) | A class that resolves what it needs, so its contract states none of its preconditions. |
| [Ambient Context](AmbientContext-en.md) | A static access point, so the dependency is declared by nobody and reachable by everybody. |
| [Constrained Construction](ConstrainedConstruction-en.md) | A constructor signature imposed from outside, so its emptiness proves nothing. |

## The lifestyles

Chapter 8. One question — how long may an instance live, and what may it therefore hold — answered three ways.
Read together: most of what each page says is a contrast with the other two.

| Lifestyle | What it means |
|---|---|
| [Singleton Lifestyle](SingletonLifestyle-en.md) | One instance for the process. Must be thread-safe, and may depend on nothing shorter-lived. |
| [Scoped Lifestyle](ScopedLifestyle-en.md) | One per request or unit of work. Safe within a scope, and lost if reached from outside one. |
| [Transient Lifestyle](TransientLifestyle-en.md) | One per consumer. May hold state freely, and if disposable is disposed by nobody in particular. |

## How a page is organised

Every page follows the same order.

| | |
|---|---|
| **Intent** | one sentence |
| **Problem** | the situation that makes the pattern worth considering, in code |
| **Solution** | what the pattern does about it — or, for an anti-pattern, what the annotation does |
| **Structure** | a diagram of the roles |
| **The roles** | one line each, and the annotation that marks it |
| **The example** | the sample from `DesignPatternCatalog.Usage`, in pieces |
| **Applicability** | what the work itself states |
| **When not to use it** | the cases where the pattern costs more than it earns |
| **Advantages** and **Drawbacks** | two lists |
| **Relations with other patterns** | the neighbours, and what separates them |
| **Source** | the work, and links back to the index and the code |

## What these pages do not do

They do not invent, and this catalogue tests that rule harder than the other two.

**Four entries are anti-patterns, and the book gives them no advantages.** *Domain-Driven Design* gives Smart
UI eight, and the guide carries them as Evans'. Seemann and van Deursen give these four none — so the
*Advantages* section on each of the four says the book lists none, states the one or two circumstantial facts
that are honestly true, and stops. Filling them from the field's arguments would put words in the authors'
mouths.

**Two entries carry a disagreement rather than a verdict.** Fowler named the service locator as a pattern and
Seemann calls it an anti-pattern; the same author called the ambient context a pattern in 2011 and an
anti-pattern in 2019. The pages name both readings and follow the catalogued edition, which is why ADR-0037
names an edition rather than a work.

**One entry shares a name with a pattern in another catalogue and is not it.** The Gang of Four's Singleton and
this catalogue's Singleton Lifestyle are different things — one is a class enforcing its own uniqueness, the
other a registration decision made outside the class. The lifestyle page says so, because a reader who
conflates them writes the one with the drawbacks.
