# Pattern catalog

The data behind the attributes. One file per pattern, mirroring the layout of
`Reefact.LivingDocumentation.Attributes.<Catalog>/`, one project per catalogued work:

```
catalog/GangOfFour/Composite.json  ──generate.py──▶  Reefact.LivingDocumentation.Attributes.GangOfFour/Composite.cs
                                                 └─▶  doc/generated/catalog-index.md
```

Browsing the catalog is what [`doc/generated/catalog-index.md`](../doc/generated/catalog-index.md)
is for: every pattern, what to type to annotate each of its roles, what each role
may be applied to, and a link to its source and its sample. A directory listing
stops being navigable long before the catalog stops growing.

The generated `.cs` files are committed and are what ships. This folder is a
development-time tool: it exists so that the structure of an attribute — base
class, targets, `AllowMultiple`, `Inherited`, the three read properties — is
written once and cannot drift across the catalog. Only the content of a pattern
is authored here.

## Regenerating

```
python3 catalog/generate.py
```

Rewrites every `.cs` from the JSON. Running it on an unchanged catalog must
leave the working tree clean; that round-trip is what keeps the two in step.

## Adding a pattern

Add `catalog/<Catalog>/<Pattern>.json`, regenerate, review the diff.

```json
{
  "catalog": "GangOfFour",
  "name": "Composite",
  "summary": "Composes objects into tree structures to represent part-whole hierarchies, and lets clients treat individual objects and compositions uniformly.",
  "inherited": false,
  "roles": [
    { "name": "Component", "targets": ["Interface", "Class"], "links": [],
      "summary": "Declares the interface shared by the leaves and the composites of the tree." },
    { "name": "Leaf", "targets": ["Class", "Struct"], "links": ["Component"],
      "summary": "A terminal element of the tree: it has no children." }
  ]
}
```

`pattern.schema.json` describes the format and documents every field. Entries
can be checked against it with any JSON Schema tool, which is what makes a
catalog written in bulk reviewable: a missing role or a bad target is a
validation error rather than something to notice by reading.

Two rules the schema cannot state on its own, and which are worth checking too:
every name in `links` must be a role of the same pattern, and role names must be
unique within a pattern.

## Patterns deliberately left out

A pattern named by a body of work and absent from its catalog here reads as an
oversight. These are decisions, taken for one of two reasons, and recorded so
that the absence can be told apart from a gap
([ADR-0011](../doc/handwritten/for-maintainers/adr/0011-leave-out-what-cannot-be-annotated.md)).

**Nothing to attach it to.** C# offers no attribute below the assembly and above
the type, and a marker type invented to carry one would put an artefact of this
system into the code it documents.

| Pattern | Work | What it qualifies |
|---|---|---|
| Module | Domain-Driven Design | a namespace |
| Conformist, Customer/Supplier, Partnership, Separate Ways | Domain-Driven Design | the relationship *between* two bounded contexts |
| Context Map | Domain-Driven Design | the whole landscape — it is what you draw *from* the annotations |
| Segregated Core, Abstract Core, Highlighted Core, Distillation Document, Domain Vision Statement | Domain-Driven Design | an act of refactoring, or a document; what they produce is already expressible, a distilled core being an assembly annotated `CoreDomain`, and the abstract core of a framework being a role of `PluggableComponentFramework` |
| Guard Clause | — | a shape a method body takes; nothing holds a role in it |

**Nothing a tool could check.** A role licenses no verifiable assertion, so an
attribute would name it without letting anything range over it — the criterion of
[ADR-0007](../doc/handwritten/for-maintainers/adr/0007-decide-sameness-by-the-assertions-a-pattern-carries.md),
applied to whether a pattern belongs here at all.

| Pattern | Work | Why |
|---|---|---|
| Intention-Revealing Interfaces | Domain-Driven Design | asks that names come from the ubiquitous language; nothing mechanical distinguishes a good name from a bad one |
| Conceptual Contours | Domain-Driven Design | a judgement about where a model's seams fall, not a property of a declaration |
| Ubiquitous Language, Continuous Integration, Evolving Order | Domain-Driven Design | practices of a team, not participants in code |
| Model-Driven Design, Hands-On Modellers, Declarative Design, System Metaphor | Domain-Driven Design | ways of working, or of thinking about a model; a codebase can follow all four and no declaration is a participant in any |
| Responsibility Layers | Domain-Driven Design | what the pattern asserts is an **order** — each layer depends only on those beneath it, and the layers are ranked by rate of change — and nothing in this vocabulary orders assemblies. Taking the five Evans names (Potential, Operations, Decision Support, Policy, Commitment) as fixed roles would supply one, but those are the layers he found in a shipping domain, offered as an illustration; the pattern is finding your own |
| Big Ball of Mud | Foote and Yoder, *Pattern Languages of Program Design 4*, 2000 | what it asserts about a participant is that it has no discernible structure, which is the absence of an assertion rather than one. Reached through Evans, who uses it to characterise a neighbouring context, and decided on the same criterion that admits Smart UI ([ADR-0023](../doc/handwritten/for-maintainers/adr/0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md)) |

**Anti-patterns are not excluded as a category.** `SmartUi` is catalogued, because
Evans names it, a class or an assembly holds it, and it licenses assertions — the
usual three. It is the only entry whose assertions *exempt* rather than constrain,
and [ADR-0023](../doc/handwritten/for-maintainers/adr/0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md)
records why that is admitted rather than special-cased.

## Chapters catalogued whole, including what a contents page does not name

Chapter 2 of *Analysis Patterns* is complete: its nine sections are catalogued, and
so are two patterns the section titles do not name. **Chapters 3 and 5 are complete
too** — twelve sections and four, every one of which held up against the criteria,
which is not something to assume of the chapters still to come: chapter 7 is a worked
example whose sections are steps rather than patterns, and most of chapter 11
qualifies a package, which C# gives nothing to annotate.

**Chapter 4 is complete at four of its five sections.** Enterprise Segment,
Measurement Protocol, Range and Phenomenon with Range are catalogued; §4.5 *Using the
Resulting Framework* is not, because it applies the four rather than adding a fifth —
the same shape as chapter 7, and the reason to say so here is that a reader counting
sections against entries would otherwise find one missing. `StatusType` is a fifth
entry from §4.2.3, which names it; it earns its own entry rather than roles on the
protocol because what it asserts is about an observation's claim on the world, and a
comparative status is itself observable.

Three things about this chapter are worth recording rather than leaving to be found
again. The chapter's figures spell one method *Casual Calculation*; the contents page
has **Causal**, which is what it is, so that is the spelling the role carries.
`Projection` and `AssociativeFunction` are not new here — chapter 3 already holds them
on `ActiveObservation` and `AssociatedObservation` — so figures 4.9 and 4.24 add only
`Plan` and the range function. And the UML companion redraws sixteen of the chapter's
twenty-four figures: 4.2, 4.6, 4.13–4.16, 4.18 and 4.23 are absent, and 4.13–4.16 fall
in *Creating a Measurement*, which is procedural. The entries rest on the sixteen.

Chapter 5 is worth a note of its own because its four entries are about *identity*,
and three of them are distinguished from one another only by what they permit.
`Name` permits several and permits none. `IdentificationScheme` permits several and
scopes uniqueness to an issuer. `ObjectMerge` asserts sameness on the system's own
authority; `ObjectEquivalence` records who asserts it and so admits disagreement —
which is the difference between a duplicate cleaned up and two catalogues that do
not concur.

**Chapter 8 is complete: all seven sections.** Proposed and Implemented Action,
Completed and Abandoned Actions, Suspension, Plan, Resource Allocation, Outcome and
Start Functions — and its Protocol, catalogued under another name for the reason below.
Its UML companion redraws all seventeen figures, so nothing here rests on inference.

Two departures from the naming convention below are worth stating here rather than
leaving to be noticed.

Section 3.10 is titled *Active Observation, Hypothesis, and Projection*; its faithful
PascalCase is unreadable, so the entry is `ActiveObservation` and the other two are
roles of it.

Section 8.5 is titled *Protocol*, and `AnalysisPatterns/Protocol` was already taken by
chapter 3. **They are two patterns, not one spelled twice.** Chapter 3's records the
method by which an observation was made, so that two results of one phenomenon type
obtained differently are not interchangeable — which is why `MeasurementProtocol`
narrows it. Chapter 8's is a composite of steps at the knowledge level whose operational
instance is a plan (figures 8.9, 8.10 and 8.12). Different assertions, so
[ADR-0007](../doc/handwritten/for-maintainers/adr/0007-decide-sameness-by-the-assertions-a-pattern-carries.md)
makes them two entries — and since one catalogue cannot hold the name twice, chapter 8's
is `PlanProtocol`, named after what it is the knowledge level *of*.

Both are judgements against the rule, taken because the rule produced an unusable or an
impossible name, and both are the kind of thing a maintainer may reverse.

`Leveled Accountability Type` and `Directional Accountability Type` appear only in
figures 2.12 and 2.13, as «overlapping» siblings of the hierarchic one. They meet
the three criteria like any other entry — the work names them, a class holds them,
they license assertions — so the only question was whether a name given in a
diagram is *the name the work gave it* under
[ADR-0006](../doc/handwritten/for-maintainers/adr/0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md).
It is: the rule is about which work names a pattern, not about which part of the
work. So the convention this chapter sets is **the section title where the book has
one, the diagram's class name where it does not** — which is why
`HierarchicAccountability` carries the section-2.7 spelling while its two siblings
carry theirs from the figures.

Three entries were resolved rather than catalogued as they stood, and how they were
resolved is worth recording because each turned on a comparison rather than on a
judgement:

| Pattern | How it was settled |
|---|---|
| Party Type Generalizations | **An entry of its own.** Figure 2.10 asserts what `KnowledgeLevel` does not: a supertype / subtypes hierarchy on *Party Type* itself, plus a derived closure that the accountability type's constraint ranges over instead of the immediate type |
| Organization Structure | **A specialisation of `Accountability`.** Figures 2.6 and 2.7 are drawn as 2.8 is, with *organization* for *party* and *parent* / *subsidiary* for *commissioner* / *responsible*. Restricting both ends to organizations is an added assertion, so it is narrower rather than the same thing spelled differently — [ADR-0007](../doc/handwritten/for-maintainers/adr/0007-decide-sameness-by-the-assertions-a-pattern-carries.md) |
| Organization Hierarchies | **An entry of its own, and not that one.** Figure 2.4 has *no type object at all* — the admissible nesting is an invariant on each subtype — and figure 2.5 is Fowler showing why that collapses once a second structure appears. What it asserts is that one parent suffices, which is a claim about a participant and not merely a smaller version of the structure pattern |
## When the author says a chapter is superseded

The UML companion Fowler publishes for chapter 6 of *Analysis Patterns* carries a note
in his own hand: a more up-to-date discussion of accounting patterns is at
`martinfowler.com/apsupp/accounting.pdf`, and the patterns there supersede the book's.
The note is on that chapter's companion and on no other.

So **chapter 6 is not catalogued from the book**, and the paper — *Accounting Patterns*,
seventy-two pages, its PDF created 8 December 2000 — is catalogued as
`AccountingPatterns` instead. The decision is
[ADR-0026](../doc/handwritten/for-maintainers/adr/0026-follow-an-authors-own-supersession-of-a-catalogued-chapter.md),
and its argument is that a rule protecting a work's authorship should not be turned
against the author himself: replacing one's own model is not a rival presentation of it.

The paper has **nine patterns**, read from its own section headings: Event (p11),
Accounting Entry (p15), Posting Rule (p19), Secondary Posting Rule (p33), Account (p39),
Accounting Transaction (p44), Reversal Adjustment (p53), Difference Adjustment (p59) and
Replacement Adjustment (p69).

### What became of chapter 6's fifteen sections

| Book section | In the paper |
|---|---|
| Account | **`Account`** — and the paper takes the entries *out* of it. Fowler says so in the text: "Account often goes with Accounting Entry. Indeed in *Analysis Patterns* I put them in the same pattern" |
| Transactions | **`AccountingTransaction`**, with the sum-to-zero invariant stated, and two-legged and multi-legged as separate roles |
| Posting Rules | **`PostingRule`**, with the host — a service agreement, a business unit — made a role, which is what carries "different rules for different business units" |
| Posting Rule Execution · Posting Rules for Many Accounts | folded into `PostingRule` and `SecondaryPostingRule` rather than kept as patterns of their own |
| Individual Instance Method · Summary Account · Memo Account · Choosing Entries · Accounting Practice · Sources of an Entry · Balance Sheet and Income Statement · Corresponding Account · Specialized Account Model · Booking Entries to Multiple Accounts | **no pattern of their own in the paper** |

The last row is the honest state of it, and it is where a maintainer's eye is wanted.
ADR-0026 records the risk in its own words: a book pattern with no successor is absent
for a reason that is *not* supersession, and whether any of those ten should be
catalogued from the book despite the note is not a question this file can settle.

Three of the paper's nine have no section in chapter 6 at all — `Event`, and the pair of
adjustments beyond the reversal. The paper is built around reacting to an event, which
is the shift the note is really about.

### The three adjustments are siblings

The three adjustments are siblings and not a hierarchy. Each is a whole strategy for one
problem — you cannot edit a booked entry — and they differ in what they cost:
`ReversalAdjustment` keeps everything and pays in entries, `DifferenceAdjustment` pays
one entry and loses the statement of what the figure should have been, and
`ReplacementAdjustment` trades the audit trail away on purpose.

## Enterprise Integration Patterns, and what a channel's annotation depends on

**Fifty-seven of its sixty-five are catalogued**: the integration styles, the base
patterns, the channels, and the whole of message routing, message construction, message
transformation and the messaging endpoints. A reader counting fifty-seven against
sixty-five is looking at work in progress and not at eight decisions. What remains is
system management.

**Routing was taken before construction**, and construction closed the gap that left.
Routing went first because it is the core of the work — the router, the splitter, the
aggregator and the process manager are what a messaging codebase is made of, while
message construction is mostly properties on a message. Chapter 5 was then filled behind
chapter 7, so the catalogue reads in the book's order again and what remains follows it.
The detour is recorded because it happened, not because anything is still missing from
the middle.

**A pattern the book builds out of other patterns gets one role, not a role per part.**
`ComposedMessageProcessor` is a splitter, a router and an aggregator; `Normalizer` is a
router and a translator per format. In both the entry names the assembled whole, and the
parts inside wear `Splitter`, `MessageRouter`, `MessageTranslator` and the rest
themselves. Giving a composite a role per constituent would count the same participant
twice — once under its own pattern, once under the composite — and a codebase with three
normalizers could no longer say how many translators it has.

Its admission is
[ADR-0029](../doc/handwritten/for-maintainers/adr/0029-admit-enterprise-integration-patterns-as-a-catalogue.md),
which also settled the two judgements this work forces.

**The channels are in, and their annotation depends on the codebase.** A channel is often a
configured queue name rather than a type, and where that is so there is nothing to annotate
— the pattern is simply not used, which is the ordinary condition of every role rather than
a defect in the entry. Where a codebase has a typed abstraction per channel, and that is
common in .NET, the role attaches to it. They are catalogued because the routing patterns
route *between* channels and the endpoint patterns consume *from* them: a catalogue without
channels describes half a mechanism, and an absence with no record reads as an oversight.

**The three message intents do not narrow `Message`, although the book calls them kinds
of message.** `CommandMessage`, `DocumentMessage` and `EventMessage` are separate
patterns here, with no `specialisationOf`. Not because the book is wrong: because a
relation targets a *pattern* and `Message` has three roles, so the inheritance it emits
would be from `Message.Role` — the base every role of `Message` answers, header and body
included. A consumer asking for `Message.Message` would not get the commands, and one
asking for `Message.Role` would get them beside headers. The relation would state
something the book does not, which is worse than stating nothing: a codebase that means
both writes both attributes.

**`CompetingConsumers` keeps a plural name for a role that annotates one class**, and that
is deliberate. It is the name the book gives, and the rule for names is to spell a pattern
as its work spelled it
([ADR-0028](../doc/handwritten/for-maintainers/adr/0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md)).
Reading `[CompetingConsumers]` on one consumer is also true to the pattern: a single
competing consumer is not one, the arrangement is, and the plural is what stops a reader
taking the annotation for a property of the class instead of a statement about the channel
it shares.

**Pipes and Filters and Message Broker are held here although POSA named them first** in
1996. The test is
[ADR-0028](../doc/handwritten/for-maintainers/adr/0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md)'s:
this work names each, describes it in full and gives it a place in its own pattern language,
which is a presentation. Crediting an earlier source is scholarship, not the passing mention
that rule excludes. If POSA is ever catalogued it holds its own entries and neither
catalogue refers to the other.

### Three homonyms, expected and unrelated

This work carries three names the catalogue already holds elsewhere. Two arrived with the
messaging endpoints; the third comes with system management. They are **different
patterns**, and since each catalogue ships as its own package nothing in the packages says
so — hence this table.

| Here | Already held as | Why they are not the same |
|---|---|---|
| `EnterpriseIntegration/MessagingGateway` | `EnterpriseApplicationArchitecture/Gateway` | Fowler's wraps any external resource behind a simple interface; this one hides a messaging API from application code |
| `EnterpriseIntegration/MessagingMapper` | `EnterpriseApplicationArchitecture/Mapper` | Fowler's moves data between two objects that should not know each other; this one moves it between a domain object and a message |
| `EnterpriseIntegration/SmartProxy` *(to come)* | `GangOfFour/Proxy` | the Gang of Four's stands in for an object; this one intercepts a request and its reply in order to observe them |

Three days ago each would have been a question of which publication held the definition.
Since [ADR-0027](../doc/handwritten/for-maintainers/adr/0027-ship-one-independent-package-per-catalogued-work.md)
made the catalogues independent, there is nothing to arbitrate — only this note to write.

## Shape of the generated attribute

A pattern whose single role carries the pattern's own name is emitted flat, so
that it reads as ubiquitous language:

```csharp
[ValueObject] public readonly record struct Money(decimal Amount, string Currency);
```

Every other pattern is emitted as a static container holding one sealed
attribute per role, so that an annotation still reads as *this is a X* rather
than *this belongs to pattern X*:

```csharp
[Composite.Component]                       public interface INode { }
[Composite.Leaf(Component = typeof(INode))] public sealed class FileNode : INode { }
```

A role targeting `Method` is a member role. Nothing else distinguishes it: it is
generated exactly like the others, and consumers tell them apart by reading
`AttributeUsage`.

## Held back for want of a source

`Idioms` is for a pattern that has a **source** but no body of work of its own
(ADR-0013) — it names the absence of a catalog, not the absence of a source, and
every entry must record a reference with a year, because that reference is what says
which work presents the pattern as its own (ADR-0028).

Two everyday practices fail that, and one of them is named in ADR-0013 itself as
the example of an Idioms candidate:

| Held back | What was looked for, and found |
|---|---|
| `Result` | No publication names it as a pattern. Its lineage runs through Haskell's `Either` — a general sum type, not error handling — and Rust's `Result`, a standard-library type rather than a named pattern. Recording either as *the work that named it* would be false. |
| `Option` / `Maybe` | Same shape of problem, with a better-looking answer that does not survive inspection: `option` is defined in *The Definition of Standard ML* and `Maybe` in the Haskell report, but those works define a **type**, not a pattern, and neither is a body of work about patterns. |

Both would be useful entries. Admitting them means one of two things, and neither
is a detail: find a publication that genuinely named the practice, or decide that
`Idioms` may hold a pattern whose provenance is a lineage rather than a work —
which is a change to ADR-0013 and to the schema's required reference, and belongs
in a record of its own rather than in a catalog entry written quietly.

`GuardClause` was looked at with them and is closed rather than held back: it is a
shape a method body takes, so nothing holds a role and nothing can be asserted
about a participant (ADR-0011).
