# File Transfer

🌍 🇬🇧 English (this file) · 🇫🇷 [Français](FileTransfer-fr.md)

## Intent

File Transfer integrates applications by having one produce a file the other consumes, so that neither needs
to know anything of the other beyond an agreed format.

## Problem

A container terminal and the customs authority. Customs will not open a socket to a terminal, and the terminal
will not be given a login to customs.

There is no shared technology to build on, and no prospect of one: two organisations, two procurement cycles,
two security policies. Anything that requires both sides to run the same middleware, or to be up at the same
moment, is not available.

What crosses is a file.

## Solution

The pattern shares a format and nothing else.

One application writes a file at an agreed place, in an agreed layout, at an agreed time. The other finds it
and reads it. Neither holds a reference to the other, neither needs the other to be running, and neither
learns anything about how the other is built.

The cost is timeliness. Nothing is known until somebody writes a file and somebody else notices — so a
declaration lodged one minute after the export waits a day.

## Structure

```mermaid
flowchart LR
    T["Terminal<br/>DeclarationFileExport"]
    F["/outbound/customs-YYYYMMDD.edi"]
    C["Customs<br/>reads on its own schedule"]
    T -->|"writes at 04:00"| F
    F -->|"found, later"| C
```

Two boxes with no arrow between them. The file is the only thing both sides touch, and the gap in the middle
is where the day goes.

## The roles

| Role | Annotation | Applies to | What it carries |
|---|---|---|---|
| FileTransfer | `[FileTransfer]` | interface, class, assembly | The participant that produces or consumes the shared file. |

One role, and it covers both ends: the exporter and the importer are the same claim seen from two sides.
Assembly is among the targets because a whole integration project is sometimes the honest scope.

## The example

From [`FileTransferUsage.cs`](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration/FileTransferUsage.cs).

```csharp
[FileTransfer]
public sealed class DeclarationFileExport {

    public string WriteFor(DateOnly day, IReadOnlyList<string> declarations) {
        string path = $"/outbound/customs-{day:yyyyMMdd}.edi";
        // ... writes one line per declaration, in the agreed layout
        return path;
    }

}
```

The whole integration is one method that returns a path. That is not the sample being brief — it is the
pattern: there is no client, no connection, no protocol, and nothing to mock in a test.

`{day:yyyyMMdd}` in the name is the contract's other half. The file's *name* is as much a shared agreement as
its contents, because it is how the receiver knows which day it holds and that it has not read this one
already.

The sample's remark states the trade in one line: *the two systems share no technology at all, which is the
whole benefit. The cost is timeliness: a declaration lodged at 04:01 waits a day.*

## Applicability

The book compares the four integration styles on the same handful of criteria, and File Transfer's profile is
the reason to choose it:

**Use File Transfer where the two applications can share no technology.** It requires the least of both sides —
a filesystem and an agreed layout — which is why it reaches across organisations that will agree on nothing
else.

**Use it where the data does not have to be current.** The style's own consequence is a delay of one transfer
interval, and choosing it is accepting that.

**Use it where what crosses is data rather than behaviour.** A file carries information; it cannot ask the
other side to do anything.

## When not to use it

**Do not use it where the answer is needed now.** The crane waiting for a release check cannot wait for
tomorrow's file. That is [Remote Procedure Invocation](RemoteProcedureInvocation-en.md)'s case.

**Do not use it where the two copies must agree at every moment.** Between two transfers the receiver's view
is stale by construction, and no amount of care in the export changes that. Where staleness is unacceptable,
the book's answer is [Shared Database](SharedDatabase-en.md).

**Do not use it where the format will change often.** The layout is the contract, and it is a contract with no
version negotiation and no compiler: a column added on one side is a silent misparse on the other.

**Do not use it to move very large volumes frequently.** The style's granularity is a whole file, so a change
of one record means writing and reading everything — which is why the transfer interval tends to grow rather
than shrink.

## Advantages

* It requires the least of both sides: no shared middleware, no shared runtime, no simultaneous availability.
* Neither application needs to know the other exists beyond a path and a layout.
* Everything is inspectable: the integration's entire state is a file somebody can open.
* It survives outages on either side without loss — the file waits.

## Drawbacks

* Data is stale between transfers, by exactly the transfer interval.
* The format is a contract that nothing checks, and a mismatch is a misparse rather than an error.
* Somebody has to decide when a file is complete, when it has been read, and what happens if it is read twice.
* Only data crosses; nothing can be asked of the other side.

## Relations with other patterns

**`SharedDatabase`**, **`RemoteProcedureInvocation`** and **`Messaging`** are the other three styles, and the
four are meant to be read as one choice.

**`Messaging`** is what this becomes when the interval shrinks and the granularity drops to one event —
which is most of the rest of this catalogue.

**`MessageTranslator`** is what the agreed layout usually needs on arrival, once the receiving side has a model
of its own.

## Source

*Enterprise Integration Patterns*, Gregor Hohpe and Bobby Woolf, Addison-Wesley, 2003 — chapter 2, integration
styles.

* [Index entry](../../../generated/catalog-index.md#filetransfer-enterprise-integration-patterns)
* [Generated attribute](../../../../DesignPatternCatalog.EnterpriseIntegration/FileTransfer.cs)
* [Example](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration/FileTransferUsage.cs)
