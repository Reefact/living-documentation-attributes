# ADR-0038 | Name the packages after the catalogue rather than after the vendor

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0038-name-the-packages-after-the-catalogue-rather-than-the-vendor.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-11
**Accepted:** 2026-08-11
**Decision Makers:** Reefact

## Context

Ten works are catalogued, plus `Idioms`: **343 patterns, 619 role names**, eight of the ten complete,
and thirty-seven records behind them. **Nothing is published.** The version is a development
placeholder, there is no release workflow, and there is no consumer anywhere.

The packages are named `Reefact.LivingDocumentation.Attributes.<Catalog>`. The longest identifier a
consumer types is **seventy-two characters**; the solution, the twenty-seven projects and the GitHub
repository all carry the same root. The `Attributes` segment names what the types already are: every
public type in every package is an attribute, and a consumer sees that in the first line of use.

**No record has ever decided package identity.** Thirty-seven records cover the catalog format, the
generator, the versioning policy, the public-surface baseline and eleven admissions, and none of them
names a package. Two records quote the current namespace in prose ([ADR-0033](0033-admit-microservices-patterns-as-a-catalogue.md)
and its translation); nothing else in the base depends on it.

The name occurs **3 934 times across 755 tracked files**. Almost all of that is generated code and
sample code, both of which follow mechanically. The twelve `PublicAPI.Unshipped.txt` baselines are the
exception — nothing has shipped, so the *Shipped* files are empty and the whole public surface sits in
the *Unshipped* ones, every line beginning with the namespace, and
[ADR-0018](0018-hold-the-public-surface-to-a-committed-baseline.md) forbids deriving them with the
generator.

**Four facts about nuget.org bear on the name.**

* `Reefact.` is a **reserved, verified prefix**. Four packages ship under it, owned by `Reefact` and
  `SylvainAurat`, and each carries the verified marker.
* `DesignPatternCatalog` returns **zero results**. The identifier is free.
* NuGet has no ownership of a name except prefix reservation, and reservation is granted on a prefix
  whose ownership the applicant can show. A generic prefix is not ordinarily reserved.
* **The vendor already publishes a prefix-less catalogue family.** `DiagnosticCatalog` and its seven
  siblings — `.NetAnalyzers`, `.Sonar`, `.StyleCop`, `.CodeStyle`, `.Trimming`, `.AspNetCore`, `.Self`
  — are owned by `Reefact`, carry no vendor prefix, are unverified, and take the shape of a bare
  meta-package plus one package per sub-catalogue. A catalogue of other people's analyzer rules is
  published under the catalogue's name rather than the vendor's.

**What the catalogue holds is wider than one kind of pattern.** It carries 62 patterns of test design
([ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md)), 48 entries that are models of the
business ([ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.md)), anti-patterns
([ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md)), three lifestyles
([ADR-0037](0037-admit-the-dependency-injection-catalogue.md)) and idioms
([ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md)). Those records share one
reasoning, stated in the third of them: the admission criteria *"do not ask what kind of thing a
pattern is"*. The same record draws a distinction the name has to live with — of Fowler's analysis
patterns it says, in terms, **"None of them is a shape the code takes; each is a claim about the domain
being modelled."**

The four reading rules published on the base attribute define **Catalog** as *the first namespace
segment below the root*. Under a root named for the whole, `DesignPatternCatalog.GangOfFour` still
resolves the way the rule says: the root is `DesignPatternCatalog`, the catalog is `GangOfFour`.

A GitHub repository rename leaves a permanent redirect. `PackageProjectUrl` and `RepositoryUrl` name
the repository, so every package carries its name.

## Decision

Every package, namespace, project, the solution and the repository are renamed from
`Reefact.LivingDocumentation.Attributes` to `DesignPatternCatalog`, naming what the packages carry
rather than who publishes them.

## Rationale

**The patterns are not the vendor's, and a vendor prefix says they are.** This library exists to
attribute each pattern to the work that named it — [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md)
places a pattern where its work put it, and [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md)
holds it under that work's own spelling so that a reader of a book finds its patterns spelled as it
spelled them. A package identifier beginning with the vendor's name contradicts that discipline in the
one string a consumer reads before anything else: it puts the publisher's name in front of Gamma's, of
Fowler's, of Evans', of Schmidt's. The vendor's own practice already answers this — the eight
`DiagnosticCatalog` packages carry no prefix, for the same reason, on a catalogue of rules that are
likewise not the vendor's.

**The convention chosen here is the vendor's, not an exception to it.** Reefact has two families:
`Reefact.*` for its own libraries, and `<Domain>Catalog.*` for catalogues of other people's material.
The decision applies the second family to a second catalogue rather than inventing anything, and the
shape follows it exactly — a bare meta-package for those who want everything, one package per
sub-catalogue for those who want one work.

**The cost is real and is already borne.** The reserved `Reefact.` prefix does not extend to the new
identifiers, so the packages ship without the verified marker. That is not a cost this decision
introduces; it is the cost the vendor accepted when the eight `DiagnosticCatalog` packages were
published, and accepting it a second time keeps one rule rather than two.

**"Design pattern" is used here in its everyday sense** — a named, recurring solution in software
design — under which a pattern of test design, a pattern of business modelling and an idiom are all
design patterns. That sense is deliberately wider than the contrast ADR-0024 draws between a model of
the business and a shape of the code: that record distinguishes kinds *inside* the catalogue, this one
names the catalogue from *outside*, and a name that has to cover ten works cannot carry the
distinction the finer record makes. Naming the field is what a reader searching nuget.org has to go
on, and it is what the bare word `Patterns` fails to give.

**Nothing is published, which is the whole of why this is decided now.** Package identity is the one
thing [ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md)'s versioning
cannot carry: a new identifier is a new package, never a new version of the old one. Renaming today
costs a mechanical pass over generated files and twelve baselines rewritten by hand. Renaming after
the first release costs a deprecation cycle across thirteen published packages and splits every
consumer's history in two. The window closes at the first release and does not reopen.

**The reading rules survive unchanged**, which is what makes the word `Catalog` usable at the root
despite already naming a work elsewhere in the repository. The rule reads a catalog as the first
segment below the root; the new root is one segment, so the catalog is still the work, and
`DesignPatternCatalog.GangOfFour` states that the assembly is the catalogue of the Gang of Four's
design patterns.

**The repository is renamed with the packages** because it is named inside them.
`PackageProjectUrl` and `RepositoryUrl` are shipped metadata, so a repository keeping the old name
would leave the old identity in every package that carries the new one, and GitHub's permanent
redirect makes the change free for anyone holding an old link.

## Alternatives Considered

### Keep the vendor prefix and shorten the middle — `Reefact.Patterns.<Catalog>`

Considered because the reserved `Reefact.` prefix is an asset already earned, because the verified
marker is a real signal to a consumer choosing between two packages of the same name, and because
`Company.Product.Feature` is the ordinary .NET shape. It is also the shortest of the candidates —
fifty characters at worst against fifty-four.

Rejected because it keeps the vendor's name in front of works that are not the vendor's, which is the
objection this decision is about, and because the vendor's own catalogue family already answers the
question the other way. Choosing it would mean publishing `DiagnosticCatalog` without a prefix and
`Reefact.Patterns` with one, on the same argument, in the same account.

### `Patterns` rather than `DesignPatterns`

Considered, and it is the alternative with the strongest single fact behind it: ADR-0024 states of the
analysis patterns that **none of them is a shape the code takes**, and the catalogue holds 48 such
entries plus 62 patterns of test design. On that reading the unifying word is `Pattern`, and
`DesignPattern` claims a narrower category than the base admits.

Rejected because a bare `Patterns` names no field at all — it says the package contains patterns and
not what of — and because the everyday sense of "design pattern" is the phrase a reader searches for
and the one under which all ten works are shelved. The tension with ADR-0024's wording is real and is
recorded below as a consequence rather than argued away.

### Drop only `.Attributes` — `Reefact.LivingDocumentation.<Catalog>`

Considered because it is the smallest possible change, removes the segment that genuinely says
nothing, and keeps *living documentation*, which is the argument the README's opening section makes
and the reason the library exists at all.

Rejected because it keeps the vendor prefix and still runs to sixty-one characters, and because
"living documentation" names the purpose rather than the contents. A consumer searching nuget.org
searches for what is in the package.

### Rename nothing

Considered because the change touches 755 tracked files and the current names work.

Rejected because the cost never falls and becomes irreversible: at the first release the same rename
turns into a deprecation cycle over thirteen published packages. Deferring is choosing the expensive
version of the same decision.

### Rename the packages but not the repository

Considered because a repository rename touches every external link.

Rejected because `PackageProjectUrl` and `RepositoryUrl` ship inside the packages, so the old identity
would survive in the metadata of every package published under the new one.

## Consequences

### Positive

* The longest identifier falls from seventy-two characters to fifty-four, and the segment that said
  nothing is gone.
* No package identifier claims authorship over a work it catalogues.
* One naming rule across the vendor's catalogues instead of two.
* The four reading rules are unchanged, so nothing a consumer was taught about reading the annotations
  back has to be relearned.
* The change is free today and cannot be made free again.

### Negative

* **The packages ship without the verified marker.** The reserved `Reefact.` prefix does not reach the
  new identifiers, and a generic prefix is not ordinarily reserved.
* `DesignPatternCatalog` is a generic and desirable identifier with no reservation behind it. Nothing
  stops a third party publishing `DesignPatternCatalog.Something` that appears to belong to this
  family.
* **The name is wider than ADR-0024's own words.** A reader who meets *"None of them is a shape the
  code takes"* in an accepted record and `DesignPatternCatalog.AnalysisPatterns` on nuget.org finds two
  accepted records using "design pattern" in two senses. This record is the only place that says the
  wider sense is deliberate.
* The twelve public-surface baselines are rewritten by hand, since ADR-0018 forbids generating them.
* **The old name survives in this record alone** — in its Context, its Decision and one rejected
  alternative, where it is the subject rather than an illustration. A reader grepping the base for the
  old identifier finds only this record, and has to know that the illustrative occurrences elsewhere
  were updated rather than overlooked.
* Every external link to the repository resolves through a redirect rather than directly.

### Risks

* A rename spanning 755 tracked files can miss a string that is not part of a namespace — a documentation
  link, a workflow path, a cross-reference in an XML comment. The guards are that regenerating an
  unchanged catalogue must leave the tree clean and that the sample must still print the whole
  catalogue read back through the base attribute alone.
* The catalogue may later admit a work whose contents strain the name further than the analysis
  patterns already do. The name is then wrong in the one place that cannot be corrected cheaply.

## Follow-up Actions

* Rename in a single pass, regenerate, and confirm that an unchanged catalogue leaves the working tree
  clean.
* Rewrite the twelve `PublicAPI.Unshipped.txt` baselines by hand.
* Rename the GitHub repository to `design-pattern-catalog` and update `PackageProjectUrl` and
  `RepositoryUrl`.
* Apply for an identifier prefix reservation on `DesignPatternCatalog.`, and record the answer here —
  a generic prefix may well be refused, and a refusal is the fact that makes the squatting risk
  permanent.
* Update the four root documents and `catalog/README.md`, which name the packages throughout.
* Update the occurrences of the old name that are illustrative rather than decided — ADR-0033's
  package name and ADR-0035's pull-request links — and leave this record's own Context, Decision and
  rejected alternative as they stand.

## References

* [ADR-0001](0001-check-every-pull-request-against-the-adr-base.md) — why a change of this size cannot
  land without a record, and why this one exists at all.
* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) and
  [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — the attribution
  discipline the vendor prefix contradicted.
* [ADR-0018](0018-hold-the-public-surface-to-a-committed-baseline.md) — why the twelve baselines are
  rewritten by hand rather than regenerated.
* [ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md) — versioning cannot
  carry a change of identifier, which is what makes the first release the deadline.
* [ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md),
  [ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md) and
  [ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.md) — the three records that decide the
  catalogue does not filter by kind, and the source of the tension recorded above.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) — one package per work, which
  is the shape the new family keeps.
* The vendor's existing prefix-less catalogue family on nuget.org: `DiagnosticCatalog` and its seven
  siblings.
