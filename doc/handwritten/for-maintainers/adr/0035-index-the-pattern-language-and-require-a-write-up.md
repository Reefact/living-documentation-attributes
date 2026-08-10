# ADR-0035 | Index the pattern language, and admit on a write-up rather than a book citation

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0035-index-the-pattern-language-and-require-a-write-up.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-10
**Accepted:** 2026-08-10
**Decision Makers:** Reefact

## Context

[ADR-0033](0033-admit-microservices-patterns-as-a-catalogue.md) admitted *Microservices Patterns*
and, under *Risks*, stated a rule for keeping the `reference` field honest:

> The mitigation is the reference field, which names the work rather than the URL, and the rule
> that an entry is added only where the site states the book covers it or the pattern predates it.

Nine instalments later that rule has been applied to fifty-one pages, and three things about it are
now known that were not known when it was written.

**The first limb almost never fires.** Ten of the twenty-four pages catalogued before this record
point at the 2018 book in their body at all, and only seven of those say it "describes this
pattern". Every other entry rests on the second limb, or on a third ground never stated in
ADR-0033 — *this is the subject matter of a chapter* — which was used in
[#57](https://github.com/Reefact/living-documentation-attributes/pull/57) and
[#58](https://github.com/Reefact/living-documentation-attributes/pull/58) and is an inference from
a chapter title rather than a statement by the author.

**A book citation supplies nothing this library uses.** What an entry needs is a problem, a
solution and named participants, because those are what become roles and assertions. The book line
tells a reader where to read more. It is a courtesy, and it has been doing the work of a criterion.

**Three patterns are blocked, for two different reasons.** `SelfContainedService` and
`ServicePerTeam` have full write-ups — context, problem, forces, solution, resulting context,
related patterns — and are the only two of the fifty-three index bullets the author marks `new`,
which is his own signal that they postdate the book. `ConsumerSideContractTest` has no write-up at
all: one line of gloss in the index, on a bullet that links to the page for a different pattern.

The first two are blocked by a rule about the book. The third is blocked by something else
entirely, and the difference has been recorded in prose across two instalments without ever being
decided.

**ADR-0033's arithmetic is also wrong, and this record is where a reader is told so.** It states 48
patterns across fourteen groups, with a count per group. Recounting the index bullet by bullet gives
**53 bullets over 51 distinct pages in 15 groups** — two pages are listed twice, and one group,
*Architectural style*, was missed entirely and is now excluded. ADR-0033 is a historical record and
is not edited; `catalog/README.md` carries the true figures.

## Decision

**The work this catalogue indexes is Richardson's pattern language as he publishes it**, of which
the 2018 book is the principal edition. Three rules follow, and they replace the sentence quoted
above.

1. **An entry is admitted where the author presents the pattern**: a page of his own carrying at
   least a problem and a solution. Whether the 2018 book also carries it is recorded, not required.
2. **An index gloss is not a presentation.** A bullet with a one-line description and no page of
   its own does not admit an entry, however real the pattern is elsewhere.
3. **`reference` stays `Chris Richardson / Microservices Patterns / 2018` for every entry**, and in
   this catalogue that names the pattern language under the title and year of the edition that
   fixed most of it. `catalog/README.md` names every entry the 2018 book does not carry, so the
   exception is enumerated rather than implied.

Consequently `SelfContainedService` and `ServicePerTeam` are admitted, and
`ConsumerSideContractTest` is not — until the author writes it up.

## Rationale

The criterion should be the thing the catalogue actually consumes. An entry is built out of a
problem, a solution and named participants: those become the summary, the roles and the assertions
a reviewer can hold a pull request to. A sentence pointing at a book contributes none of them. Nine
instalments of applying the old rule produced exactly one useful signal — *is there a write-up?* —
and one persistent nuisance, which was inventing grounds for entries that plainly belonged.

The `new` marker is information about the pattern language's growth, not about whether a pattern is
real. `Service per team` has a context citing Conway's law, five forces, a solution in five clauses
and a resulting context with four benefits and two drawbacks. Refusing it because the author added
it after 2018 privileges the publication date of one edition over the author's own current
statement of his own pattern language — which is the opposite of what
[ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) asks, since ADR-0028's
question is whether *the work presents the pattern*, and the work is presenting it right now.

The write-up bar is where the honesty actually lives, and it is not a low bar. It is what has kept
twenty-four entries' roles traceable to participants the author names; only two names in the whole
catalogue are this catalogue's own invention, and both are flagged. An entry minted from an index
gloss would have a summary and every role summary written here, which is the provenance discipline
abandoned rather than stretched. `AntiCorruptionLayer` — a problem and a solution and nothing else —
is the thinnest thing that clears it, and it clears it.

Rule 3 is the compromise, and it is worth naming as one. The schema carries author, work and year,
and a living pattern language has no publication year. Stating in an ADR what the reference means
for this catalogue, and enumerating the exceptions in `catalog/README.md`, is less fiction than
inventing a year and less churn than rewriting twenty-four references — but it does mean the data
alone does not tell a reader that the 2018 book lacks `ServicePerTeam`. The prose does.

## Alternatives Considered

### Keep ADR-0033's rule as written

The status quo. Its argument was that the reference must not assert something unsupported, and that
argument was right — it is why the two patterns were held rather than quietly added.

Rejected because the rule has been failing in both directions. It excludes two patterns the author
presents in full, and it has been silently supplemented by a third ground — *chapter subject
matter* — that it never stated and that is weaker than either limb it did state. A rule being
worked around is worse than a rule being replaced.

### Rewrite `reference.work` for all twenty-four entries to name the pattern language

`work: "The Microservice Architecture Pattern Language"`, so that the reference is self-sufficient
and no prose is needed to keep it true.

Rejected, though it is the most honest option on its own terms and the maintainer may prefer it. It
costs a rewrite of every existing entry and a visible change to every generated documentation line
in the package, and it does not solve the year: 2018 would still be the year of a book, attached to
a work that has no year. It trades prose for churn without removing the convention.

### Admit `ConsumerSideContractTest` as well, on its index gloss

All three, on the ground that a gloss written by the author is still the author, and that a reader
searching for *consumer-side contract test* deserves to find it — which is the argument
ADR-0033's inclusive posture makes about homonyms.

Rejected. The inclusive posture is about *which work presents a pattern*, not about *how little a
presentation can be*. Here the entry's summary, its roles and every assertion would be written by
this catalogue, and the one line of authorial text points at a page describing something else. This
is the alternative to take if the bar in rule 2 proves wrong; taking it means accepting that some
entries' assertions are the catalogue's rather than the work's, and saying so in `catalog/README.md`.

### Add a schema field for the edition

`reference` gains an optional note saying which publication carries the pattern, so the data says
what rule 3 leaves to prose.

Deferred on [ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md)'s ground —
though weakly, since two entries would exercise it immediately. Worth taking if a second catalogue
ever has the same problem; one catalogue's exceptions fit in a paragraph.

### Split the site-only patterns into a second catalogue

`MicroservicesPatterns` for the book, another package for the pattern language's later additions.

Rejected on [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md): one package per
catalogued work, and this is one work. It would also put two patterns a reader thinks of as
Richardson's in a package they will not think to install.

## Consequences

### Positive

* The criterion becomes the thing the catalogue consumes — a write-up — instead of a courtesy line
  that ten pages in twenty-four happen to carry.
* Two patterns with good assertions are admitted: *no synchronous call while handling a request*,
  and *exactly one team may change this service*.
* The third ground used in practice stops being unstated. There is one rule, and it is the one
  being applied.

### Negative

* `reference` for two entries names an edition that does not carry them. That is a stated
  convention rather than an accident, and `catalog/README.md` enumerates it — but a consumer
  reading only the generated documentation will not know.
* The catalogue now tracks a living document. The author can add a pattern tomorrow, and *complete*
  will mean complete as of a date rather than complete as of a book.

### Risks

* A pattern the author later withdraws or renames leaves an entry behind that no source supports.
  Nothing detects that; the mitigation is the completeness audit that found this record's own
  errors, run again rather than trusted once.
* The write-up bar is a judgement about how much text is enough. `AntiCorruptionLayer` sets the
  floor at a problem and a solution; the next borderline page will be argued against it, which is
  what a precedent is for.

## Follow-up Actions

* Catalogue `SelfContainedService` and `ServicePerTeam`, with samples, on acceptance.
* Keep `ConsumerSideContractTest` in `catalog/README.md`'s held-back section, with rule 2 as the
  reason rather than a hesitation.
* Record in `catalog/README.md` which entries the 2018 book does not carry, per rule 3.

## References

* [ADR-0033](0033-admit-microservices-patterns-as-a-catalogue.md) — the record whose *Risks*
  sentence this replaces; its decision, its inclusive homonym posture and its exclusion criteria
  stand.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — asks whether the
  work presents the pattern, which is the question rule 1 makes answerable.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — unaffected: a presented pattern that no
  declaration can hold is still left out.
* `catalog/README.md` — the held-back section, and the record of what the book does not carry.
