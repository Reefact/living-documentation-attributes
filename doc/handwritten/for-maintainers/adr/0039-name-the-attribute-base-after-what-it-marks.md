# ADR-0039 | Name the attribute base after what it marks

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0039-name-the-attribute-base-after-what-it-marks.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-11
**Accepted:** 2026-08-11
**Decision Makers:** Reefact

## Context

[ADR-0038](0038-name-the-packages-after-the-catalogue-rather-than-the-vendor.md) renamed the packages,
the namespaces, the projects, the solution and the repository to `DesignPatternCatalog`, on the ground
that a name should say what the packages carry rather than who publishes them. Its Decision sentence
enumerates those five things and **does not reach type names**, so the rename left every type as it
was.

One type is affected by that omission. `LivingDocumentationAttribute` is the **single public type of
`DesignPatternCatalog.Core`**, and every attribute in every catalogue descends from it — through the
per-pattern abstract `Role` base for a pattern with several roles, directly for a pattern with one.
[ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) keeps it a pure marker that declares no
member, and the four rules for reading the catalog back are documented on it so that they travel with
the package.

**It is the one identifier of this repository a consumer types by hand.** Everything else is offered by
completion once a package is installed; this type is what a reader climbs to in order to find every
annotation whatever mix of catalogues was installed, and that is the whole reason `Core` exists as a
package of its own.

A type name is read **unqualified**. A `using` brings the namespace in, and what appears at the call
site is the bare name — `typeof(LivingDocumentationAttribute)` in a codebase where nothing else of this
library is in view.

After ADR-0038 the phrase *living documentation* survives in exactly two places: this type name, and
the opening section of the README that argues why the library exists.

The name occurs 355 times. One is the hand-written declaration in `Core`; one line per pattern is
generated; five hand-written files reference it — the sample's reader in two files, and two test
files; six records name it — [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md),
[ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md) and
[ADR-0034](0034-let-a-specialisation-name-the-role-it-narrows.md), each with its translation; and the
README names it once.

The repository already accepts a stutter where its own rules produce one: the role a multi-role pattern
gives its own name is written `[MonitorObject.MonitorObject]` and `[Interceptor.Interceptor]`.

Nothing is published. A public type's name is part of what a consumer compiles against, so
[ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md)'s two contracts both
carry it: renaming it after a release is a breaking change, needing a major version and a shim for the
old name.

## Decision

The attribute base is renamed from `LivingDocumentationAttribute` to `DesignPatternAttribute`.

## Rationale

**This is ADR-0038's argument applied to the one name a consumer writes.** That record decided the
library should be named for what it carries; after it, the most visible identifier in the whole
repository was the one still named for what the library is *for*. A rule that stops at the package
boundary and reverses inside it is not a rule, and the type it stops short of is precisely the type a
reader meets first.

**That the name is read unqualified is what settles which name to use.** `DesignPatternAttribute`
stands on its own at a call site in somebody else's codebase: it says that what it marks participates
in a design pattern. `PatternAttribute` does not — read bare, *pattern* is as likely to mean matching
or a regular expression, and the namespace that would disambiguate it is not on screen. The extra word
is not redundancy; it is the only thing carrying the meaning where the name is actually read.

**The stutter is confined to where nobody looks.** `DesignPatternCatalog.DesignPatternAttribute` reads
awkwardly, and it is the fully-qualified form, which appears in a baseline file and almost nowhere
else. The repository has already accepted worse in a place that *is* read — a pattern's own role
stutters in the annotation itself — because the rule producing it was worth more than the reading. The
same holds here.

**ADR-0004 is untouched.** What changes is the name; the type stays a pure marker declaring no member,
and the reading rules stay documented on it, so a consumer who reaches the type still reaches the
rules.

**The cost is near nil now and permanent later.** Almost every occurrence is generated, so the change
is a constant in the generator and a regeneration; the hand-written references number five files and a
declaration, and a missed one does not compile. After the first release the same rename is a breaking
change to the compile-time contract, which is the one thing versioning makes expensive rather than
merely visible.

## Alternatives Considered

### Keep `LivingDocumentationAttribute`

Considered, and it is what ADR-0038 as accepted left standing. It is also the last place in the code
where the library's purpose is stated: *living documentation* is an established term, and the type
that every annotation descends from is a defensible place to say what all of them are for. Read
generously, `DesignPatternCatalog.LivingDocumentationAttribute` says the catalogue holds design
patterns and that annotating one is an act of documentation.

Rejected because it makes the library's most visible identifier the single exception to the naming rule
just adopted, and because the purpose is argued at length in the README and in this base, which is
where reasoning belongs. A type name has one job at a call site, and it is not to carry a motive.

### `PatternAttribute`

Considered because it is shorter and because the namespace already supplies the word *design*, so the
qualified name would read `DesignPatternCatalog.PatternAttribute` without stuttering.

Rejected on the fact that decides this record: the name is read unqualified, where `Pattern` alone is
ambiguous. Optimising the form nobody reads at the expense of the form everybody reads is the wrong
trade.

### `CatalogAttribute` or `DesignPatternCatalogAttribute`

Considered for symmetry with the root namespace.

Rejected because it names the collection rather than what an annotation asserts. An annotation says
that a declaration participates in a design pattern; it does not say that the declaration is in a
catalogue.

### Fold the rename into ADR-0038 rather than write a record

Considered because the two changes are one intention and land days apart from nobody's point of view
but this repository's.

Rejected because ADR-0038 is accepted and its Decision sentence enumerates what it renames.
Broadening an accepted record's scope in place is the one thing the base forbids outright, and the
distinction is not a formality: a package identifier is what a consumer *installs*, a public type name
is what it *compiles against*, and the second is the contract ADR-0021 treats as breaking.

## Consequences

### Positive

* One naming rule from the package identifier down to the type a consumer writes by hand.
* The identifier stands on its own where it is actually read — unqualified, at a call site, with no
  other name from this library in view.
* Free today. After the first release the same change is a breaking change to the compile-time
  contract.

### Negative

* **The phrase *living documentation* leaves the code entirely.** It survives in the README's opening
  argument and in this base, so a reader of the source alone no longer meets the idea that motivates
  the library.
* The fully-qualified name stutters, and it is the form a baseline file records.
* Three records — ADR-0004, ADR-0019 and ADR-0034, each with its translation — name the old type.
  Their occurrences are illustrative, so they are updated under the rule ADR-0038's follow-up states;
  but a reader who has an older checkout meets two names for one type.
* ADR-0038 remains the only record naming the old *package* root while no record names the old *type*
  root, so the two halves of one rename are not searchable the same way.

### Risks

* Of the 355 occurrences, the ones the compiler cannot check are the prose: the README, the records,
  and the XML documentation the packages ship. A missed reference there is a document that names a type
  which no longer exists, and nothing fails.

## Follow-up Actions

* Change the constant in the generator and regenerate; an unchanged catalogue must still leave the
  working tree clean.
* Rename the declaration and its file in `Core`, and update the sample's reader and the two test files.
* Update the affected lines of the twelve `PublicAPI.Unshipped.txt` baselines.
* Update the README, and the illustrative occurrences in ADR-0004, ADR-0019 and ADR-0034 with their
  translations.

## References

* [ADR-0038](0038-name-the-packages-after-the-catalogue-rather-than-the-vendor.md) — the argument this
  record extends, and the rule on which records are updated and which keep the old name.
* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — what the type is, and what this record
  does not change about it.
* [ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md) — why a public type
  name is expensive to change after a release and cheap before one.
* [ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md) and
  [ADR-0034](0034-let-a-specialisation-name-the-role-it-narrows.md) — the two other records that name
  the type, both illustratively.
