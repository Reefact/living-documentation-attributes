# ADR-0040 | Write the pattern guide by hand, in both languages

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0040-write-the-pattern-guide-by-hand-in-both-languages.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-12
**Accepted:** 2026-08-12
**Decision Makers:** Reefact

## Context

The repository publishes one document for a reader of the catalogue:
[`doc/generated/catalog-index.md`](../../../generated/catalog-index.md), 6091 lines, generated from
`catalog/<Catalog>/<Pattern>.json`. Per pattern it gives the summary, the table of roles with the
annotation to type, what each role applies to, whether it repeats and what it links, a sentence per
role, the reference, and links to the generated source and to the sample.

**It answers *what to type*. It does not answer *why*, *when*, or *when not*.** Nothing in the
repository tells a reader who does not already know Abstract Factory what problem it solves, what it
costs, or which situations make it the wrong choice. That knowledge is in the works, and a reader who
has not read the work has nowhere to go.

There are 348 sample files under `DesignPatternCatalog.Usage`, one per pattern
([ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md)), each in a business domain and
each carrying the annotations. They are compiled and run — the sample prints the whole catalogue read
back through the base attribute alone — so they cannot rot. What they carry in prose is a comment or
two.

[ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) keeps the catalog as
data and generates the attributes and the index from it. The schema carries a `summary` for the
pattern and one per role, and no other prose. Applicability, consequences, and the patterns a reader
confuses with this one are **not derivable** from that data: they come from the work.

**A pattern's definition does not move.** The works are published and fixed; what moves is this
repository's rendering of one — a role renamed, a sample rewritten, an entry admitted or excluded.
The generated index already carries everything that moves.

The repository is written in English. The ADR base is bilingual with English canonical and the
translation alongside as `NNNN-title.fr.md`, the suffix marking which text is the record.

## Decision

A guide of one page per pattern, written by hand in English and in French, is kept under
`doc/handwritten/for-users/`, is not generated from the catalog, and states nothing its work does not
say: a section the work does not support is marked empty rather than filled.

## Rationale

**Generating it would put prose in the catalog that no attribute carries.** The schema would grow
four or five fields per pattern — when to use, when not to, consequences, confusions — whose only
consumer is a markdown renderer. The catalog is the data the attributes are made of, and
[ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md) already refuses machinery
that serves nothing being emitted. Prose that no attribute carries does not belong in the file the
attributes are generated from.

**Handwriting is affordable precisely because the content does not move with the code.** What a
pattern is for was settled by its work and will not change; what changes is the catalogue's rendering,
and the generated index carries that. A page therefore ages against a fixed subject rather than a
moving one, which is what makes 343 hand-written pages a finite job rather than a treadmill.

**A reference and a tutorial are read differently, and merging them serves neither.** The index is
consulted — a reader arrives knowing the pattern and leaves with the annotation. The guide is read —
a reader arrives not knowing and leaves able to choose. Putting the second inside the first would
make 6091 lines into something nobody scans, and the two link to each other instead.

**Neither language is a translation of record, so the file names say so symmetrically.** The ADR base
marks one text canonical because a record is a decision and a decision needs one authoritative
wording. A guide carries no authority to protect: a French reader and an English reader are owed the
same page, not a page and its shadow. `Xxxx-en.md` beside `Xxxx-fr.md` states that; `Xxxx.md` beside
`Xxxx.fr.md` would not.

**An empty section is worth more than a plausible one, and this is the half of the decision that will
be tested most often.** *When not to use it* is the section a reader most needs and the one a work
least often states outright: the Gang of Four list benefits and no drawbacks, and several catalogued
works are not to hand at all. A page written to look complete would fill that section with prose that
sounds like the work and is not in it — and because the whole guide reads with one voice, a reader
has no way to tell the sourced sentence from the invented one. So a section the work does not support
is marked empty and says why, and a section that reports a judgement formed after the work says whose
judgement it is. The guide is allowed to be incomplete; it is not allowed to be plausible.

**The risk this creates is drift, and it is answered by an instruction rather than by a check.**
Nothing in the build compares a page against the catalog, so a renamed role leaves a page quietly
wrong. `CLAUDE.md` gains a standing rule that a change to a pattern's roles, sample or status obliges
its page. That is weaker than a test, and naming it here is the point: a reader of this record should
know the guard is a habit, not a gate.

## Alternatives Considered

### Generate the guide from new schema fields

Considered because it is what the repository does with everything else, and because generated pages
cannot drift from the data they are made of.

Rejected because the data would be prose that no attribute emits, carried in the file the attributes
are generated from, and because it would not buy what generation usually buys. The drift a generator
prevents is between the data and its rendering; here the drift worth preventing is between a page and
a work published thirty years ago, which no generator can check.

### Extend the generated index instead of adding a guide

Considered because it keeps one document rather than two.

Rejected because the index is already 6091 lines for 343 patterns, and a pedagogical page is longer
than the whole of an index entry. It would turn the one document a reader consults into one nobody
scans.

### English only

Considered because the repository is written in English and the ADR base makes English canonical.

Rejected by decision: the guide addresses users rather than maintainers, and the maintainer's audience
reads French.

### Follow the ADR base's `NNNN-title.fr.md` convention

Considered for consistency with the only bilingual material the repository already has.

Rejected because that convention exists to mark one text as the record, which is right for a decision
and wrong for a guide. Two peer files want two symmetric names.

### One page per catalogue rather than one per pattern

Considered because it is fewer files.

Rejected on the largest catalogue: 65 patterns of enterprise integration in one document, each with a
problem, a diagram, an example and two lists, is not a page anyone reads.

## Consequences

### Positive

* A reader who does not know a pattern has somewhere to go, which is the one thing the repository
  could not offer.
* Each page can be as long as its pattern deserves, without lengthening the document everyone else
  consults.
* Nothing is added to the generator, the build or the test suite.

### Negative

* **Nothing checks a page against the catalog.** A renamed role, a rewritten sample or a newly
  excluded pattern leaves its page wrong and silent. The guard is a standing instruction in
  `CLAUDE.md`, which is a habit rather than a gate.
* 343 patterns in two languages is 686 pages when it is finished, delivered catalogue by catalogue. In
  between, the guide is partial in a way the generated index never is, and a reader will meet patterns
  that have no page.
* Some works are not to hand. Where a page cannot state applicability from the work itself, it says so
  rather than filling the section, and a reader meets an admitted gap. On the catalogues whose works
  are hardest to reach, *When not to use it* may be empty for a long time — which is the intended
  outcome, not a defect to be fixed by writing something.

### Risks

* A page that reads with authority it does not have. A guide written from memory of a work rather than
  from the work states things the work does not, in a voice that sounds like it does. Two mitigations,
  both visible on the page: a section the work does not support is **marked empty**, and a judgement
  formed after the work is **attributed** — the criticism of Singleton is the clearest case, and its
  page says in terms that everything beyond the book's two conditions is the field's view rather than
  the authors'.

## Follow-up Actions

* Add two standing rules to `CLAUDE.md`: a change to a pattern's roles, its sample or its status
  obliges the corresponding pages; and a section no work supports is left marked empty rather than
  written.
* Deliver catalogue by catalogue, and category by category where a catalogue is large enough to
  warrant it.
* Decide, once several catalogues are covered, whether a test should assert that every catalogued
  pattern has a page in both languages. It is cheap and it would turn the standing rule into a gate;
  it is deferred rather than refused.

## References

* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) — what is generated
  from the catalog, and therefore what this guide is not.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md) — the samples the guide reads
  from.
* [ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md) — the refusal this record
  extends to catalog fields no attribute carries.
* [`doc/generated/catalog-index.md`](../../../generated/catalog-index.md) — the reference the guide
  links to rather than repeats.
