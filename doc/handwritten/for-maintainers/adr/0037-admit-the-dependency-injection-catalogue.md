# ADR-0037 | Admit the Dependency Injection catalogue, with its code smells and its lifestyles

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0037-admit-the-dependency-injection-catalogue.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-11
**Decision Makers:** Reefact

## Context

Nine works are catalogued, plus `Idioms`: **332 patterns over 326 distinct names, 557 role names**, and
seven of the nine complete. The most recent, `Posa2`, reaches inside a process to name a lock, a
monitor, a thread pool.

**None of the nine says how a class gets the collaborators it needs.** The catalogue holds Gang of
Four's five creational patterns, which are about how an object *makes* another;
`EnterpriseApplicationArchitecture` holds `Registry`, `Plugin`, `SeparatedInterface` and `ServiceStub`,
which touch the edges of the question; `DomainDrivenDesign` holds `Factory`. Not one of them names the
place where an application's object graph is assembled, or the constructor through which a dependency
arrives, or the difference between a dependency it is safe to hard-code and one that must be injected.
Every .NET application does that work at start-up, and the vocabulary for it is the vocabulary of the
platform the packages ship for.

*Dependency Injection Principles, Practices, and Patterns* — Steven van Deursen and Mark Seemann,
Manning, March 2019, ISBN 9781617294730, 552 pages — is the work. It is the second edition of Seemann's
*Dependency Injection in .NET* (2011), and it carries an explicit catalogue. Its section-level contents,
read from the publisher's own online edition, name **fourteen items in three catalogue sections**:

| Section | Items |
|---|---|
| 4 — DI patterns | Composition Root, Constructor Injection, Method Injection, Property Injection |
| 5 — DI anti-patterns | Control Freak, Service Locator, Ambient Context, Constrained Construction |
| 6 — Code smells | Constructor Over-injection, Abuse of Abstract Factories, Cyclic Dependencies |
| 8.3 — "Lifestyle catalog" | Singleton Lifestyle, Transient Lifestyle, Scoped Lifestyle |

Four more named concepts sit outside those sections: **Captive Dependency** and **Leaky Abstraction**
(§8.4, the bad lifestyle choices), and **Stable Dependency** and **Volatile Dependency** (§1.3, the
classification on which the whole book turns — a volatile dependency is one that must be injected).

Five facts about that list matter here.

**Three kinds of thing enter together, and only one of them is already decided.** The patterns need no
decision. The anti-patterns are settled by
[ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md), which admits an
anti-pattern on the same terms as any pattern. **Code smells are a third kind the base has never
ruled on.** And the lifestyles are a fourth: the work itself calls §8.3 a *"Lifestyle catalog"* rather
than a set of patterns, so admitting them means holding something its own authors do not call a
pattern.

**Almost nothing here fails [ADR-0011](0011-leave-out-what-cannot-be-annotated.md), because the book is
written in C#.** A composition root is a class or a method; the three injection patterns *are* a
constructor, a method and a property; Control Freak is the class that news up its own dependencies;
Ambient Context is the static access point; the lifestyles constrain a class. One item fails:
**Cyclic Dependencies** has no single declaration — a cycle is a property of a graph, and annotating
each participant would assert a relation the attribute cannot carry.

**Four names touch `GangOfFour`, and none of the collisions is forced.** `Singleton`,
`AbstractFactory`, `Facade` and `Decorator` are all already held there. But the work's own names are
`SingletonLifestyle` and `Abuse of Abstract Factories`, and Decorator and Facade are Gang of Four's,
which chapters 6 and 9 *cite* rather than present —
[ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md)'s distinction exactly.
Using each work's own spelling, as ADR-0028 requires, leaves no collision at all.

**One near-homonym is a disagreement rather than a coincidence.** The Singleton Lifestyle and Gang of
Four's Singleton are opposed by this work on purpose: its argument is that a single instance should be
decided by the composition root, not enforced by the type, so the lifestyle is what a reader uses
*instead of* the pattern.

**The edition fixes the list, and the earlier edition's list is different.** The 2011 first edition has
one author, a different chapter arrangement and a different set of anti-patterns. Sources: the
publisher's online edition gives section-level contents for every chapter; Seemann's blog carries the
canonical definition of Composition Root — *"a (preferably) unique location in an application where
modules are composed together"* — and posts on most of the rest; Manning's free per-pattern articles
are behind a captcha and were not read.

## Decision

The work is admitted as a catalogue named `DependencyInjection`, and **its code smells and its
lifestyles enter on the same terms as its patterns**.

## Rationale

The gap is the widest one left and the most used. Composition is what every application does before it
does anything else, and it is the one part of a codebase where a mistake is invisible in a type
signature: a class that takes an abstraction and a class that reaches for a static one look identical
from the outside, and differ in every way that matters. Naming the difference is
[ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md)'s aim applied to the thing a
.NET reader does most.

The assertions are the most checkable in the base, and they are checkable in a new way. Seemann's own
rule for a composition root is that **a DI container is referenced from there and from nowhere else** —
which a build can enforce today, against assembly references, without reading a line of logic. That is
not a rule about the shape of the code; it is a rule about configuration, and the only comparable claim
in the catalogue is `ServicePerTeam`'s, checkable against `CODEOWNERS`. The lifestyles are the same
kind: `[SingletonLifestyle]` on a class says *this must be registered once*, and the container's
registration either agrees or does not.

ADR-0023's reasoning extends to a code smell without strain, which is why the decision takes them
rather than arguing about the word. That record admitted an anti-pattern because *this is the shape we
are stuck with* is worth as much as *this is the shape we chose*. A smell is the same statement held
with less certainty, and an annotation is a better place for it than a wiki: it sits on the constructor
in question rather than in a document about constructors in general. What is genuinely new is the
degree word — `ConstructorOverInjection` says *too many*, and *too many* is a judgement rather than a
fact. That is not an objection but a description of the entry: the annotation is the judgement, made by
somebody, recorded where it applies, which is the premise of the whole library.

The lifestyles are admitted although the work does not call them patterns, and the reason is that
[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md)'s test is what a thing asserts
rather than what it is called. A lifestyle asserts something a reviewer can hold a pull request to, it
sits on a declaration, and it is the entry a .NET reader will reach for first. Refusing it on the word
*catalog* would be the kind of nominalism that
[ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.md) had to undo in another catalogue
nine instalments in.

The homonyms are the best case ADR-0028 has had. Elsewhere two works name the same idea and the
duplicate is a cost; here two works name *opposed* ideas with one word, and the catalogue is the only
place a codebase can say which it means. A reader who annotates `[SingletonLifestyle]` has stated that
the single instance is the composition root's decision; one who annotates Gang of Four's `[Singleton]`
has stated that the type enforces it. That distinction is invisible in C# and is exactly the sort of
thing this library exists to make sayable.

## Alternatives Considered

### Take only the patterns and the anti-patterns

Eight entries, leaving chapter 6's smells and §8.3's lifestyles out. Considered because a code smell is
not a pattern in any of the nine works already catalogued, and because a lifestyle is not one in this
work's own words — so this is the option that adds no new kind at all.

Rejected on what it would cost. It leaves out `ConstructorOverInjection`, which is the entry a reviewer
would reach for most often of the fourteen, and the three lifestyles, which carry the most checkable
claims in the candidate. And the ground for refusing them would be the *word* rather than ADR-0007's
test, which asks what assertions a thing carries.

### Take the lifestyles and refuse the smells

A middle position: lifestyles carry checkable claims, smells carry a judgement of degree.

Rejected, though it is the most defensible refusal on offer and the maintainer may prefer it. The
degree word is real, but a codebase that marks its own known debt is doing what ADR-0023 admitted
anti-patterns in order to allow. If this is the line, then `ConstructorOverInjection` is the one entry
to leave out and `AbuseOfAbstractFactories` — which is a shape, not a quantity — should still be held.

### Admit *Release It!* instead

Nygard's stability patterns were checked first, on the maintainer's instruction, and were the
recommendation before the check was run.

Rejected on the check, and recorded here so the refusal is on the record rather than only in a
conversation. Seven of its twenty-four items survive ADR-0011 — Nygard's antipatterns are failure modes
of a running system rather than shapes in code — and the seven survivors have no participants to take
role names from, because the book is written as essays rather than in the form
[ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.md)'s second rule presumes. Every
role would be this catalogue's invention. It remains a candidate if the maintainer will accept that.

### Name it `Di`, or `DependencyInjectionInDotNet`

Considered for brevity and for precision respectively.

Rejected. `Di` is an abbreviation nobody writes in prose, and unlike `Posa2` it is not what anyone says
aloud. `DependencyInjectionInDotNet` names the **first** edition's title, which is the edition whose
list this catalogue does not follow.

### Shelve the injection patterns under `Idioms`

Constructor Injection in particular is often described as a language idiom rather than a pattern.

Rejected: [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) reserves `Idioms`
for a pattern with no body of work of its own, and this is a 552-page body of work with an explicit
catalogue.

## Consequences

### Positive

* The catalogue can say how a codebase is wired, which is the one thing every .NET application does and
  the one thing nine works could not name.
* The first claims checkable against *configuration* rather than against code: a container registration,
  an assembly reference, an entry point.
* ADR-0028 is exercised on two works that **disagree**, rather than on two that overlap, which is the
  strongest form of the case it makes.

### Negative

* Three entries are not patterns in their own work's words, and this record is the only place that says
  why they are held anyway.
* A reader browsing [the index](../../../generated/catalog-index.md) meets `SingletonLifestyle` near
  Gang of Four's `Singleton` and must open two packages to learn that they are opposites.
* `ConstructorOverInjection` carries a degree word. Two teams will put the line in different places, and
  the annotation will read as a claim about a number when it is a claim about a judgement.

### Risks

* The container ecosystem moves faster than a pattern language. *Scoped* is ASP.NET Core's word today
  and was not the word in 2011; a lifestyle name may age faster than anything else in the base.
* This is the first candidate written for a single platform. Its lifestyles presume a container, and a
  reader practising the book's own Pure DI has three entries that do not apply to them.
* `CyclicDependencies` is excluded and is the problem every team meets. A reader will look for it, and
  `catalog/README.md` is where they must find the reason.

## Follow-up Actions

* Fill the catalogue in instalments, beginning with chapter 4's DI patterns.
* Record in `catalog/README.md` that the 2019 edition fixes the list, and that the 2011 edition's
  differs — before an instalment rests on the wrong one.
* Decide `StableDependency`, `VolatileDependency`, `CaptiveDependency` and `LeakyAbstraction` when
  chapter 8 is reached: they are named in the work but outside its three catalogue sections, so whether
  they are entries is a question this record does not answer.
* Record every excluded item in `catalog/README.md` with the criterion it failed, `CyclicDependencies`
  first.

## References

* [ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md) — admits the four
  anti-patterns without further argument, and supplies the reasoning this record extends to smells.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — the test that lets a
  lifestyle in: what a thing asserts, not what its author files it under.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — excludes `CyclicDependencies` and nothing
  else here.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — decides the four
  homonyms, and is exercised here on works that contradict each other.
* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — the aim: patterns in daily
  use rather than more patterns.
* [ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.md) — the counting and provenance
  discipline, and the reason the edition is named in advance here.
* Seemann's own definition of the Composition Root, and the rule that a container is referenced from
  nowhere else: <https://blog.ploeh.dk/2011/07/28/CompositionRoot/>.
