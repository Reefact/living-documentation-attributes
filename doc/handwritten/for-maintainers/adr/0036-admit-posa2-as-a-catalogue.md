# ADR-0036 | Admit Pattern-Oriented Software Architecture Volume 2 as a catalogue

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0036-admit-posa2-as-a-catalogue.fr.md)

**Status:** Proposed
**Proposed:** 2026-08-10
**Decision Makers:** Reefact

## Context

Eight works are catalogued — *Design Patterns* (1994), *Analysis Patterns* (1997), *Accounting
Patterns* (2000), *Patterns of Enterprise Application Architecture* (2002), *Domain-Driven Design*
(2003), *Enterprise Integration Patterns* (2003), *xUnit Test Patterns* (2007) and *Microservices
Patterns* (2018) — plus `Idioms`. **315 patterns over 309 distinct names, 544 roles**, counting the
two entries [ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.md) admits. Seven of
the eight are read whole; `AnalysisPatterns` is paused on purpose.

**Between them they name no participant in in-process synchronisation.** The catalogue does hold
concurrency of two other kinds: *Patterns of Enterprise Application Architecture* gives four offline
locks, which are about a transaction spanning several requests, and *Enterprise Integration Patterns*
gives Competing Consumers, Message Dispatcher and the two consumers, which are about who takes the
next message. Neither says anything about a lock held by an object, a method that assumes the lock is
already held, a field confined to one thread, or a pool of threads taking turns at a shared handle.
*Microservices Patterns* brought the vocabulary of distribution and stops at the service boundary;
inside a service, a class whose whole point is a locking discipline has no way to say so.

*Pattern-Oriented Software Architecture, Volume 2: Patterns for Concurrent and Networked Objects* —
Schmidt, Stal, Rohnert and Buschmann, Wiley, 2000 — is the work. Schmidt maintains an overview page
for the volume which states that **"The book presents 17 interrelated patterns"**, names all
seventeen, and reproduces the table of contents:

| Chapter | Patterns |
|---|---|
| 2 — Service Access and Configuration | Wrapper Facade, Component Configurator, Interceptor, Extension Interface |
| 3 — Event Handling | Reactor, Proactor, Asynchronous Completion Token, Acceptor-Connector |
| 4 — Synchronization | Scoped Locking, Strategized Locking, Thread-Safe Interface, Double-Checked Locking Optimization |
| 5 — Concurrency | Active Object, Monitor Object, Half-Sync/Half-Async, Leader/Followers, Thread-Specific Storage |

Five facts about that list matter here.

**The count is a fact rather than an estimate, and the work is frozen.** A printed book of 2000 with
an author-maintained list of its seventeen patterns leaves nothing to recount later. That is not how
the last admission went: ADR-0033 stated 48 patterns across fourteen groups from a website index, and
the true figure was 53 bullets over 51 pages in 15 groups, which
[ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.md) records. Nothing here needs
ADR-0035's third rule either — there is one edition and it carries every pattern.

**No name in it is already in the catalogue.** The seventeen were checked against all 309 pattern
names and every role name: **no pattern-name collision at all**, which no other candidate has
managed. One role name coincides — `ActiveObject` is a role of `AnalysisPatterns/ObjectMerge`, where
it means the surviving record of a merge rather than an object with its own thread — and it sits
inside a pattern of another package. Close neighbours needing no arbitration: Wrapper Facade beside
Gang of Four's Facade, Strategized Locking beside Strategy, and Reactor and Proactor beside the
Enterprise Integration consumers.

**The participants are classes and members.** A reactor has a handle, a synchronous event
demultiplexer, an event handler and its concrete implementations; an active object has a proxy, a
method request, an activation list, a scheduler, a servant and a future. That is what
[ADR-0011](0011-leave-out-what-cannot-be-annotated.md) asks of a pattern, and it is true of nearly
every one of the seventeen — where the last two admissions left out six of 68 and ten of 51, this one
is expected to leave out about one.

**One entry is genuinely open.** What Scoped Locking describes is the shape a method body takes —
acquire on entry, release on every exit — which is the ground on which Guard Clause and Four-Phase
Test were left out. The guard is a declaration, so an entry could be held on the guard rather than on
the discipline. It is decided when chapter 4 is reached, not here.

**The book calls two of its patterns idioms.** Schmidt's page describes the material as spanning "a
range of patterns from idioms to architecture designs", and the volume calls Scoped Locking and
Double-Checked Locking Optimization idioms. This repository uses that word for something else:
[ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) reserves `Idioms` for a
pattern with no body of work of its own.

Finally, **POSA is a series of five volumes**. Volume 1 (1996) holds Layers, Broker,
Model-View-Controller and Blackboard; volume 4 (2007) restates much of volumes 1 and 2 as a single
pattern language. The same page notes that volume 1 "was published in 1996 and hence this book is
referred to as POSA2".

## Decision

*Pattern-Oriented Software Architecture, Volume 2* is admitted as a catalogue under the name
**`Posa2`** — the nickname its own authors give it — and its patterns enter on the criteria already
applied to every other work.

## Rationale

The gap this fills is not an exotic one, which is [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md)'s
aim rather than a new one. Every .NET codebase of any size holds a lock, a background worker, a cache
confined to one thread and a class that is only safe because callers take turns; none of them can say
so today, while the same codebase can already declare its offline locks and its competing consumers.
A vocabulary that reaches the boundary of a service and stops there is missing the half where the
concurrency bugs live.

The assertions are the best in the catalogue at the level of a member. Thread-Safe Interface says that
an interface method acquires the lock and never calls another interface method, while an
implementation method assumes the lock is held and never acquires it — two rules, checkable, and
between them exactly the discipline whose breach is a self-deadlock. Double-Checked Locking
Optimization is the pattern famous for being wrong without a memory barrier, so annotating it marks
the places to re-read rather than merely naming them. A monitor object's methods are serialised by its
own lock; a thread-specific field must never be published; a leader's turn ends before it processes
the event. Each is a rule a reviewer can hold a pull request to, and none restates the annotation —
the test of [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md).

Provenance costs nothing here, for once, and that is worth taking. A frozen book with the authors'
own list of seventeen means the completeness question has an answer *before* the first entry is
written: *complete* will mean complete against a printed table of contents, in the way it means that
for Gang of Four and cannot mean it for a maintained website. The two errors ADR-0035 had to record —
a miscount and a criterion that had to be replaced nine instalments in — are both errors this work's
shape does not admit.

Nothing else in the catalogue is disturbed. Zero homonyms means
[ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) is not exercised and
ADR-0033's inclusive posture has nothing to decide; these are collaborations between classes, exactly
as Gang of Four's are, so ADR-0022, ADR-0023 and ADR-0024 are not solicited either. It is the plainest
admission since the first.

`Posa2` is the right name because it is the authors' own, stated on their own page — which makes it
the same instrument as `GangOfFour`, and a stronger case, since that nickname is the community's and
this one is the writers'. A reader installing a package to annotate a reactor is looking for POSA2:
that is the word on the conference slide, in the bibliography and in the argument they are having
about their event loop. The series title cannot serve, because five volumes published over twenty
years would all answer to it. Casing follows the rule that produced `Cqrs` — an acronym of three
letters or more is Pascal-cased — so `Posa2` rather than `POSA2`. The short identifier never travels
alone: the generated documentation spells the volume out in full on every line, so a reader who does
not know the nickname is told the title where they meet it.

Stating what the book means by *idiom* is not pedantry, because the two meanings pull in opposite
directions. The volume's word is about a pattern's *scale* — small enough to be language-level. This
repository's `Idioms` is about a pattern's *provenance* — no body of work of its own. Scoped Locking
has a body of work, this book, so it belongs in the volume's catalogue; shelving it under `Idioms`
would put a chapter of a catalogued book into the bucket for patterns that have no book, and lose the
citation that makes the entry worth anything.

## Alternatives Considered

### Name it `ConcurrentAndNetworkedObjects`

The volume's own subtitle. Considered because it is unambiguous across the five volumes without a
numeral, reads as English rather than as a code, and follows the same instrument as
`EnterpriseApplicationArchitecture` and `EnterpriseIntegration`, which are also drawn from titles.

Rejected because nobody calls the book that. The name a reader searches for is the one the authors
say the book is referred to by, and a package name that is technically better and practically unfound
serves nothing. This was the option first proposed and it was wrong for the reason the maintainer gave:
people know POSA.

### Name it `PatternOrientedSoftwareArchitectureVolume2`

The full series title plus the volume number. Considered because it is unambiguous and needs no
knowledge of the nickname at all — the strongest option for a reader meeting the series for the first
time.

Rejected on length. It makes a 46-character namespace segment and package name, close to twice the
longest in the set, in a namespace that every annotated file imports; and it buys nothing the label
does not already give, since the generated documentation carries the full title either way. The cost
lands on every consumer, the benefit on the first reading only.

### One catalogue for the whole POSA series

`Posa`, holding all five volumes, on the ground that POSA is one series with a continuing pattern
language and that volume 4 explicitly restates much of volumes 1 and 2.

Rejected on [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md): one package per
catalogued work, and five books published across twenty years are five works with five sets of
authors' words. It would also make a reader who wants a vocabulary for locks install Layers and Broker
to get it, and it would put volume 4's restatements in the same package as the originals they restate,
which is precisely the kind of question ADR-0028 answers by keeping works apart.

### Take POSA1 first

Volume 1 is the older and the more cited book, and Layers, Broker and Model-View-Controller are said
more often than Leader/Followers.

Rejected as an ordering question rather than an admission one, and the ordering favours volume 2:
several of volume 1's patterns qualify an application or a deployment topology rather than a
declaration, which is ADR-0011's exclusion and the ground on which half of *Microservices Patterns*
was left out, whereas volume 2's are object patterns almost throughout. Volume 1 remains a candidate
on its own terms and on its own admission check — one which could not be completed when it was
attempted, because the hosts carrying its contents are refused by the network egress proxy.

### Take only the Synchronization and Concurrency chapters

Eight patterns, the part that is about a locking discipline inside one process, shelved as a small
catalogue of its own and leaving service access and event handling aside.

Rejected on the shape rather than the content, which is how ADR-0033 rejected the same move. It
prejudges chapters 2 and 3, where Reactor, Acceptor-Connector, Component Configurator and Extension
Interface are exactly the kind of participant a class holds — and Reactor is the most cited pattern in
the book. Each chapter is judged when it is reached, which is how every other catalogue was filled.

## Consequences

### Positive

* The vocabulary reaches inside a service, where the catalogue currently stops at its boundary, and
  it does so at the level of a member rather than a type.
* Completeness is decidable before the work starts, against a printed table of contents and the
  authors' own count. No other catalogue admitted since Gang of Four has had that.
* No homonym, no arbitration, no exclusion table expected beyond a line or two — the cheapest
  admission in the base to carry out, whatever it costs to decide.

### Negative

* `Posa2` is a nickname carrying a numeral, so a reader who does not know the series learns the title
  from the documentation rather than from the name. That is the accepted cost of using the word people
  say.
* The book's own use of *idiom* is not this repository's, and this record is the only place that says
  so. Nothing prevents a future contributor from shelving Scoped Locking under `Idioms`.
* Scoped Locking may not survive ADR-0011. If it does not, the catalogue is sixteen of seventeen and
  the exclusion is recorded like any other.

### Risks

* *Nearly all seventeen are admissible* is an estimate, and the last such estimate — twenty-five to
  thirty of forty-eight — was wrong in both its numerator and its denominator. The mitigation is the
  same as there: chapter by chapter, each exclusion recorded in `catalog/README.md` with the criterion
  it failed.
* The known uses are C++, C and Java, and .NET has since absorbed several of these patterns into the
  language and the runtime. Proactor is what `async`/`await` over completion ports already is;
  Scoped Locking is `lock`; Monitor Object is close to what `lock` on a private field makes of a
  class. An entry that names a shape the language gives for free may be documentation of nothing, and
  that is decided per entry rather than assumed either way here.
* This is the first catalogue whose patterns interlock as a language — the authors say the seventeen
  are interrelated, and chapter 6 weaves them. [ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.md)
  admits only outright narrowings, so most of that structure will not be expressible, and a reader of
  the package will see seventeen independent entries where the book has a map.

## Follow-up Actions

* Fill the catalogue in instalments, chapter by chapter. Synchronization and Concurrency first is the
  suggestion — they are the chapters a single-process .NET codebase holds — but the order is the
  maintainer's.
* Add `Posa2` to the catalog's list of works and to the label the generated documentation prints, with
  the first instalment rather than with this record.
* Decide Scoped Locking against ADR-0011 when chapter 4 is reached, and record the outcome either way
  in `catalog/README.md`.
* Record every excluded pattern in `catalog/README.md` with the criterion it failed.

## References

* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — the aim this one follows:
  patterns in daily use rather than more patterns.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — what cannot be annotated is left out;
  Scoped Locking is the one entry this record leaves open against it.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) — reserves `Idioms` for a
  pattern with no body of work of its own, which is not what this book means by the word.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) — one package per catalogued
  work, which is what makes the volume rather than the series the unit.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — not exercised by this
  admission: no name in this work is already held by another.
* [ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.md) — the counting and provenance
  discipline this record starts with rather than arrives at.
* Schmidt's overview of the volume, which states the count, names the seventeen and reproduces the
  table of contents: <https://www.dre.vanderbilt.edu/~schmidt/POSA/POSA2/>.
