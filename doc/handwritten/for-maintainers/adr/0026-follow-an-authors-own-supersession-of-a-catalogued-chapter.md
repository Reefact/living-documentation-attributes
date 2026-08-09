# ADR-0026 | Follow an author's own supersession of a catalogued chapter

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0026-follow-an-authors-own-supersession-of-a-catalogued-chapter.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-09
**Accepted:** 2026-08-09
**Decision Makers:** Reefact

## Context

[ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.md) admitted Fowler's
*Analysis Patterns* (1997) to the catalog. Chapters 2, 3, 4 and 5 are catalogued.
Chapter 6, *Inventory and Accounting*, is the largest in the book at fifteen
pattern sections, and it is what a reader of the book most often comes for.

The UML companion Fowler publishes for that chapter carries a note in his own hand:
a more up-to-date discussion of accounting patterns exists at
`martinfowler.com/apsupp/accounting.pdf`, and "the patterns there supercede the
patterns in the *Analysis Patterns* book". The note appears on the companion for
chapter 6 and on no other.

That paper is *Accounting Patterns*: seventy-two pages, its PDF created on
8 December 2000 — later than the book, earlier than *Patterns of Enterprise
Application Architecture* (2002) and *Domain-Driven Design* (2003). It is not a
published book; it is a draft Fowler keeps on his own site.

Its vocabulary is not the book's. The paper works in Accounting Entry, Accounting
Event, Event Type, Posting Rule, Adjustment and Event Process Log. Chapter 6 names
Account, Transactions, Summary Account, Memo Account, Posting Rules, Individual
Instance Method, Posting Rule Execution and nine more. Only Posting Rule is spelled
the same in both.

[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) decides
two things: a pattern is catalogued where the work that named it put it, and where
two works name the same pattern the **earlier publication holds the definition**.
By the second of those, read alone, the 1997 book would hold the definition against
a 2000 paper.
[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) decides that
sameness is settled by the assertions two entries carry, never by their names.

Nothing is released yet
([ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md)).

## Decision

Where the author of a catalogued work states that a later work of his own supersedes
part of it, the catalog follows the later work, and that later work is catalogued as
a catalog of its own.

## Rationale

ADR-0006's anteriority rule exists to stop a later presentation **by someone else**
from redefining a pattern an earlier work named — that is the failure it was written
against, and the reason the reference year is load-bearing in the schema. An author
withdrawing his own model is not a competing presentation. It is the same voice
saying the earlier one was wrong, and applying anteriority to it would turn a rule
that protects a work's authorship into a rule against it.

The catalog's whole claim is that a pattern is what the work says it is. Cataloguing
fifteen sections whose author states they are superseded would publish, as current
vocabulary, a model that author has replaced — and because the attributes are
generated, nothing in the output could tell a reader which entries those were. That
is precisely the class of fact this repository keeps in records rather than in code.

The later work is catalogued separately rather than folded into `AnalysisPatterns`
because ADR-0006's *other* half still holds. A pattern is catalogued where the work
that named it put it, and the paper is a different work under a different name.
Putting `AccountingEntry` under `AnalysisPatterns` would assert that the book named
it, which it does not, and would leave a reader of the paper looking for its
patterns under the title of a book.

The paper's date is worth recording rather than treating as incidental. At 2000 it is
earlier than both the enterprise and the domain-driven catalogs, so where it names a
pattern one of those also names, it is the paper that holds the definition. That is
ADR-0006 applied as written and a reach-back to expect under
[ADR-0025](0025-let-an-earlier-work-reclaim-a-pattern-from-a-later-catalog.md), not a
new rule.

The decision is bounded three ways, and the bounds are the reason it is safe. It
takes only the **author** of the work — not a commentator, however good. It takes only
an **explicit** statement of supersession in the work's own material, not an inference
from a later book covering similar ground. And it reaches only the **part** the author
names: nothing here touches chapters 2 to 5, and the note appears on chapter 6's
companion alone.

## Alternatives Considered

### Catalogue chapter 6 from the book anyway

Considered because it is ADR-0006 read literally — the book is the admitted work, the
paper is not, and the book is the earlier publication.

Rejected because it publishes a withdrawn model as current vocabulary, and the reader
of the generated output cannot tell. It also mistakes what the anteriority rule is
for: it settles a contest between two presentations, and there is no contest when one
author replaces his own.

### Skip chapter 6 and catalogue nothing in its place

Considered because it is the smallest change, and it avoids admitting a second work.

Rejected because the accounting material is the most cited part of the book's models,
and a fifteen-section hole is not a decision about those patterns — it is the absence
of one. Following the author costs a catalog and answers the question.

### Put the paper's patterns inside `AnalysisPatterns`

Considered because the two works are by one author on one subject, and one catalog is
cheaper than two.

Rejected because it asserts that *Analysis Patterns* named patterns it never used the
words for. ADR-0006's first half is about where a reader looks, and a reader of the
paper does not look under the book.

### Shelve the paper's patterns under `Idioms`

Considered because the paper is a draft rather than a published book, which is a
weaker title than the four works already catalogued.

Rejected because [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md)
shelves a pattern that has **no body of work of its own**. Seventy-two pages with an
internally cross-referenced pattern language is a body of work; being unpublished
makes it a weaker source, not an orphan.

## Consequences

### Positive

* The catalog states what the author currently holds, rather than what he held in
  1997 and has said he no longer does.
* A reader of either work finds its patterns under its own name, which is ADR-0006
  kept whole rather than traded away.
* The paper's 2000 date places it ahead of two of the four existing catalogs, so the
  collisions it produces resolve in a direction that is already decided.

### Negative

* A fifth catalog, and the ADR-0024 argument has to be made again for a source that
  is a draft rather than a published book.
* Chapter 6 of the book stays uncatalogued. `catalog/README.md` has to say so, or a
  reader counting sections against entries reads a decision as an oversight.
* The paper has no ISBN and no publication date of its own, so its reference rests on
  a PDF creation date. That is a weaker citation than every other entry carries.

### Risks

* The supersession is one sentence on a support page. If it were withdrawn or
  rewritten, this decision would need revisiting — and the sentence is not versioned
  anywhere this repository controls.
* A draft's patterns are less settled than a book's, so entries taken from it may
  need to change more often than entries taken from the four books.
* Deciding which of chapter 6's fifteen sections have a successor in the paper is
  ADR-0007 applied fifteen times, and some may have none. Those would then be absent
  for a reason that is *not* supersession, and saying which is which is work this
  decision creates rather than avoids.

## Follow-up Actions

* Add `AccountingPatterns` to the catalog schema's enum and to the generator's label
  map, without which no entry of the new catalog validates.
* Enumerate the paper's patterns from the paper itself, and record in
  `catalog/README.md` which of chapter 6's fifteen sections have a successor there and
  which do not, so an absence can be told from an oversight.
* Check the paper against `EnterpriseApplicationArchitecture` and `DomainDrivenDesign`
  under ADR-0025 before cataloguing: at 2000 it is the earlier publication, so any
  collision moves the later entry.

## References

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) — both
  halves: where a pattern is catalogued, and which publication holds its definition.
  This record decides what happens when the two works are by one author and he
  replaces the first.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — what will
  decide, section by section, whether a chapter-6 pattern has a successor in the
  paper.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) — why the
  paper is a catalog rather than a shelf of idioms.
* [ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.md) — admitting
  *Analysis Patterns*, and the terms on which a work enters the catalog at all.
* [ADR-0025](0025-let-an-earlier-work-reclaim-a-pattern-from-a-later-catalog.md) — why
  the paper's 2000 date reaches into the 2002 and 2003 catalogs.
* Fowler, *Accounting Patterns*, `martinfowler.com/apsupp/accounting.pdf`, and the
  supersession note on `martinfowler.com/apsupp/apchap6.pdf`.
