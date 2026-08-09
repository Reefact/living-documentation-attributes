# ADR-0028 | Hold a pattern in every catalogue whose work presents it as its own

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-09
**Accepted:** 2026-08-09
**Decision Makers:** Reefact

## Context

[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) decided that a
pattern is catalogued where the work that *named* it put it, and only there. Where a
second work presented the same pattern, the catalogue held one entry and a table in
`catalog/README.md` redirected a reader from the book they were holding to the catalogue
that held it.

`AnalysisPatterns/KnowledgeLevel` is the entry that table was written for. Evans devotes a
section of chapter 16 of *Domain-Driven Design* to Knowledge Level and credits Fowler,
who named it in 1997. So the entry sits under `AnalysisPatterns`, and the table says why.

[ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) ships each work as
its own independent package and removes every relation that crosses a catalogue. With it
goes the table: a comparison between two works contradicts the claim that each catalogue
stands alone.

That leaves a reader of *Domain-Driven Design* who installs the `DomainDrivenDesign`
package unable to find Knowledge Level, and with nothing to redirect them. The gap is not
hypothetical — it is the first case, and there are others wherever two of the catalogued
works cover the same ground.

## Decision

A pattern is held in every catalogue whose work presents it as one of its own patterns,
and a work that merely cites another's pattern does not hold it.

## Rationale

Independence promises that a catalogue is a complete rendering of one work. A reader who
adopts a book and installs its package must find what that book taught them, or the
promise is false and the redirection that used to repair it is gone.

The criterion has to be **authorship, not mention**, because mention is unbounded. Every
one of these books cites the others; a rule keyed on appearance would fill each catalogue
with patterns its author never claimed, and the vocabulary would stop telling a reader
what a work holds. The test is whether the work presents the pattern as one of its own —
names it, describes it, gives it a place in its own pattern language — not whether the
words occur in the text.

Applied to Knowledge Level, that is a question about Evans' book rather than about this
repository, which is the property that makes the rule usable: two people reading chapter
16 will agree on whether Evans is presenting a pattern or crediting one.

The duplication this admits is the price of independence and not a defect in it. Two
catalogues describing one idea in two works' words is what two books actually did, and a
consumer reading one of them wants that book's wording. What is lost is the assertion that
the two are one pattern — and that assertion was already downgraded to a comparison the
consumer cannot check, which is what ADR-0027 argues about at length.

Deciding this now rather than when the first gap is noticed matters, because the rule
changes what "complete" means for a catalogue already declared complete. Every catalogue
must be re-read against it once, and doing that once is cheaper than doing it per
complaint.

## Alternatives Considered

### Keep ADR-0006 as it stands: only the naming work holds the pattern

Considered because it needs no change, and because it keeps each pattern in exactly one
place, which is simpler to maintain and impossible to make inconsistent.

Rejected because the thing that made it workable is gone. It relied on a cross-catalogue
table to redirect the reader, and that table cannot survive independent catalogues. Left
alone, the rule silently produces packages that are incomplete renderings of their own
work — the reader of Evans finds no Knowledge Level and no explanation.

### Hold a pattern in every catalogue whose work mentions it

Considered because it is the widest reading of completeness, and it guarantees no reader
ever comes up empty.

Rejected because a citation is not authorship. These books cite each other constantly, and
the result would be catalogues stuffed with patterns their authors did not claim — which
destroys the one thing a catalogue is for, saying what a work holds.

### Hold it once, and let each package carry a pointer to where it lives

Considered because it avoids duplication while still answering the reader.

Rejected because the pointer is a cross-catalogue relation wearing a different coat. It
either compiles — and then the packages are coupled again — or it does not, and it is
unenforced data of exactly the kind ADR-0027 rejected.

## Consequences

### Positive

* Each package is a complete rendering of its work, which is what makes choosing one
  package a real choice rather than a partial one.
* Whether an entry belongs is settled by reading the work, not by consulting this
  repository's history of which catalogue was opened first.
* Membership stops depending on publication dates, so the order the books were catalogued
  in no longer shows through anywhere.

### Negative

* Entries multiply wherever two works cover the same ground, and each duplicate is written
  and maintained separately.
* Two renderings of one idea can drift apart in wording, and nothing detects it.
* Every catalogue already declared complete has to be re-read against this rule, including
  the four that were finished before it existed.

### Risks

* "Presents it as its own" is a judgement about a book, and two maintainers could differ on
  a borderline case. Knowledge Level is such a case: Evans gives it a section heading and
  also credits Fowler, and the answer decides whether `DomainDrivenDesign` gains an entry.
* The rule invites over-collection. A reviewer who is unsure will tend to add, and a
  catalogue inflated with patterns its author only borrowed is the failure this decision is
  supposed to prevent.

## Follow-up Actions

* Decide Knowledge Level: whether Evans presents it as one of his own patterns, or credits
  Fowler's. It is the first application of this rule and the one that prompted it.
* Re-read each of the five catalogues against the rule once, and list what it adds, so that
  a missing entry can be told from a decided absence.
* Record the outcome in `catalog/README.md` as a list of entries per catalogue, not as a
  comparison between catalogues — the file must stop putting two works side by side.

## References

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) — the rule
  this replaces, and the redirection table it depended on.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) — why the table is
  gone, and why duplication is accepted rather than repaired by a relation.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — still what
  decides whether two entries *inside* one catalogue are one pattern; it no longer reaches
  across catalogues.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) — `Idioms` holds
  what no work claims, which this rule leaves untouched and makes sharper.
