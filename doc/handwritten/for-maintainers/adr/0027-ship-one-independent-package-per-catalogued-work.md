# ADR-0027 | Ship one independent package per catalogued work

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](0027-ship-one-independent-package-per-catalogued-work.fr.md)

**Status:** Accepted
**Proposed:** 2026-08-09
**Accepted:** 2026-08-09
**Decision Makers:** Reefact

## Context

The library ships one assembly holding every catalogue. The namespaces partition it —
`GangOfFour`, `DomainDrivenDesign`, `EnterpriseApplicationArchitecture`,
`AnalysisPatterns`, `AccountingPatterns`, `Idioms` — and a consumer who installs the
package gets all of them whether or not any is wanted.

It holds 140 patterns and 286 roles, and the stated ambition is to grow by an order of
magnitude. The catalogues do not grow together: `AnalysisPatterns` took thirty-two
entries in two days, while `GangOfFour` has stood at twenty-three since it was written
and will not move.

Fourteen entries carry a relation to another. **Ten stay inside one catalogue.** **Four
cross one:**

```
DomainDrivenDesign/Repository   → EnterpriseApplicationArchitecture/Repository
DomainDrivenDesign/ValueObject  → EnterpriseApplicationArchitecture/ValueObject
EnterpriseApplicationArchitecture/Money → AnalysisPatterns/Quantity
Idioms/NullObject               → EnterpriseApplicationArchitecture/SpecialCase
```

A relation is emitted as inheritance, and a pattern's identity is read by climbing it.
Inheritance across assemblies requires an assembly reference, so a relation that crosses
a catalogue and a package boundary that separates catalogues cannot both exist.

The attributes carry no behaviour and no dependency. The whole library is tens of
kilobytes of IL, so what a consumer carries today is negligible in size and total in
scope.

Nothing is released.

## Decision

Each catalogued work is shipped as its own package, independent of the others, and no
pattern is related to a pattern of another work.

## Rationale

A project's dependencies should say what it uses. A codebase that declares
domain-driven design has no business carrying the Gang of Four, and the namespace
boundary does not deliver that — it filters after the fact, inside a file, whereas a
package is what a developer chooses before writing a line.

The catalogues have nothing in common but their shape. Each is one work's vocabulary,
learned from one book, coherent because that book is coherent. A developer adopts a
work, not a library, and the unit of distribution should be the unit of adoption.

**Independence is what makes that choice real.** One package per work with references
following publication anteriority would keep every relation and would even be acyclic by
construction — a later work narrows an earlier one, never the reverse, so the reference
graph would be the publication timeline and time does not loop. It is rejected because
the leaf a developer picks is the *latest* work, which would drag the three earlier ones
behind it. The split would be cosmetic.

What that costs is precise and small: a rule written for one work's pattern stops
reaching another work's narrower one. It reaches four relations out of a hundred and
forty, and the consumer who wants both names both attribute types — two lines in a test
written against attributes whose entire purpose is architecture tests. The library
already refuses to ship a reader on the grounds that a consumer should write its own
rules; this is the same reasoning applied one level up.

Size is not the argument and should not be offered as one. These are inert attributes
with no dependencies; the split saves almost nothing in bytes. It buys intent, and it
buys a release cadence per work — today a `GangOfFour` consumer sees a version bump for
every day of work on a book it does not read.

Independence is also a **simplification**, which is the part that settles it. The
declension mechanism loses its only use — the same pattern named by two works. The
anteriority rule loses its purpose, because it exists solely to arbitrate between two
works that name one pattern, and works that never meet have nothing to arbitrate. The
reach-back disappears with it: cataloguing a 1997 book fourth stops reaching into three
catalogues already declared complete. The reading rules go from four to three.

One package must still hold the base marker. A consumer's reader needs a single type to
find every annotation, and a base type per package would force it to know them all.

## Alternatives Considered

### Keep one assembly

Considered because it is what exists, it costs nothing, and the namespaces already
separate the catalogues.

Rejected because one version number makes every consumer see every catalogue's churn,
and because a namespace is not a choice — a developer cannot decline the patterns of a
book they have not read. At ten times the size, one assembly of fourteen hundred
patterns is a discoverability problem no `using` directive solves.

### One package per work, referencing the works it narrows

Considered because it preserves every relation, keeps identity readable by climbing, and
produces an acyclic graph by construction, since a relation always points from a later
work to an earlier one.

Rejected because it defeats the purpose of splitting. Four relations out of a hundred and
forty would make the newest catalogue depend transitively on all the older ones, so the
developer who wanted one vocabulary would still receive four.

### Keep the relations as catalogue data, emitted to the index rather than to IL

Considered because it preserves the comparison work without the coupling: the JSON
already carries the relation, and an index or a relations file could publish it for
whoever wants to deduplicate.

Rejected because a relation that nothing compiles and nothing checks is exactly what this
repository treats as worthless — the premise of the whole project is that information
kept outside the code drifts. The generic tool that would consume such a file has not
been asked for, and inventing it is the speculative generality the library declines
elsewhere.

### A bridge package per pair of works, making the coupling opt-in

Considered because it lets a consumer who wants the relation have it and leaves everyone
else alone.

Rejected because the identity of an annotation would then depend on which packages are
installed. The same `[ValueObject]` would answer one thing in a project that installed the
bridge and another in a project that did not, which is worse than either end of the
choice.

## Consequences

### Positive

* A project's dependencies state which vocabularies it uses, and a developer can decline
  a work rather than filter it.
* Each work releases on its own cadence, so a stable catalogue stops inheriting an active
  one's version churn.
* The anteriority rule and the reach-back lose their purpose. Cataloguing an earlier work
  later no longer reaches into catalogues already finished — which retires the heaviest
  outstanding obligation in this repository.
* The reading rules lose a clause, and the `[Declension]` marker loses its reason to
  exist.
* "A relation does not cross a catalogue" becomes a rule the catalogue validator checks,
  where the equivalent was prose.

### Negative

* A rule written for one work's pattern no longer reaches another work's narrower one.
  The consumer names both types.
* N packages means N sets of release notes and a compatibility question that did not
  exist.
* The same idea will be described in several catalogues, in each work's own words, with
  no mechanism to notice that the descriptions have drifted.
* The comparison work already done across catalogues is discarded rather than re-encoded.

### Risks

* The package count grows with the ambition. An order of magnitude more patterns means
  many more packages, and the naming and discovery of them becomes a problem of its own.
* A consumer who installs two catalogues receives two attributes for one idea with
  nothing saying so. That is accepted rather than solved.
* Versioning the packages independently is what buys the churn benefit, and it costs a
  compatibility matrix. The first release should version them in lockstep; loosening
  later is easy and tightening later is not.

## Follow-up Actions

* Split the projects: one per catalogued work, plus one holding the base marker that all
  reference, plus a meta-package for consumers who want everything.
* Delete the four cross-catalogue relations, and rewrite the summaries that cite another
  work — `EnterpriseApplicationArchitecture/Money`, and the two timestamp roles of
  `AccountingPatterns/Event`.
* Remove `declensionOf` from the catalogue schema and the declension marker from the
  attributes; neither has a use left.
* Teach `tools/catalog/validate.py` to reject a relation whose target names another
  catalogue.
* Rewrite the root README's section on relations around an intra-catalogue example, and
  drop the declension clause from the fourth reading rule.
* Strip `catalog/README.md` of its cross-catalogue comparisons.
* Give each package its own public API baseline, since the surface is now split.

## References

* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) — the
  catalogue is data and the assemblies are generated from it, which is what makes this a
  change to the generator rather than to a hundred and forty files by hand.
* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) — its
  anteriority half loses its purpose here; what each catalogue holds is decided anew in
  [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md).
* [ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md) — the identity
  climb, half of which was the climb through a declension.
* [ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md) — what
  is versioned, now over N packages rather than one.
* [ADR-0025](0025-let-an-earlier-work-reclaim-a-pattern-from-a-later-catalog.md) — the
  reach-back, which this decision retires.
