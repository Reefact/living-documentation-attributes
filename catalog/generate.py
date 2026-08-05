#!/usr/bin/env python3
"""Regenerates the attribute sources from the pattern catalog.

    python3 catalog/generate.py

Reads every catalog/<Catalog>/<Pattern>.json and rewrites the matching
Reefact.LivingDocumentation.Attributes/<Catalog>/<Pattern>.cs. The generated
sources are committed and are what ships; this script only keeps them uniform.
A pattern whose single role carries the pattern's own name is emitted as a flat
attribute, every other pattern as a static container holding one attribute per
role.
"""

import glob
import json
import os
import textwrap

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.dirname(HERE)
ROOT = os.path.join(REPO, "Reefact.LivingDocumentation.Attributes")
INDEX_DIR = os.path.join(REPO, "doc", "generated")
NS = "Reefact.LivingDocumentation.Attributes"

CATALOG_LABEL = {
    "GangOfFour": "Gang of Four",
    "DomainDrivenDesign": "Domain-Driven Design",
    "EnterpriseApplicationArchitecture": "Patterns of Enterprise Application Architecture",
    "Idioms": "no catalog of its own",
}


def relation(pattern):
    """The pattern this one derives from, if any, with the nature of the relation."""
    if "declensionOf" in pattern:
        return pattern["declensionOf"], "declension"
    if "specialisationOf" in pattern:
        return pattern["specialisationOf"], "specialisation"
    return None, None


def key(pattern):
    return pattern["catalog"], pattern["name"]


def role_names(pattern):
    return [role["name"] for role in pattern["roles"]]


def base_of(pattern):
    """What the generated construct inherits from.

    A pattern with one role inherits attribute-to-attribute; a pattern with several inherits through the
    abstract role base its container declares, which is what every one of its roles answers. Resolved in
    main(), where the whole catalog is known, since only the target's own entry says which of the two it is.
    """
    return pattern["_base"]


def relation_doc(pattern):
    target, nature = relation(pattern)
    if target is None:
        return None
    label = CATALOG_LABEL[target["catalog"]]
    if nature == "declension":
        return (f"The same pattern as {target['name']}, in {label}, which published it first and holds its "
                f"definition. Written from either catalog, an annotation resolves to that one identity — so a "
                f"reader of this catalog finds the pattern where it looks for it, without the two spellings "
                f"drifting apart.")
    return (f"A narrower case of {target['name']}, in {label}: every participant annotated here is one of those "
            f"too, and a consumer asking for the broader pattern gets these as well.")


def reference_doc(pattern):
    ref = pattern["reference"]
    return f"{ref['author']}, <i>{ref['work']}</i>, {ref['year']}."


def remarks(paragraphs, indent):
    """A single <remarks> block, one <para> per paragraph."""
    pad = " " * indent
    lines = [f"{pad}/// <remarks>"]
    for paragraph in paragraphs:
        if paragraph is None:
            continue
        lines.append(f"{pad}///     <para>")
        lines += [f"{pad}///         {line}" for line in textwrap.wrap(paragraph, width=108 - indent)]
        lines.append(f"{pad}///     </para>")
    lines.append(f"{pad}/// </remarks>")
    return "\n".join(lines)


def doc(text, indent, tag="summary"):
    """XML documentation block, wrapped the way the rest of the sources are."""
    pad = " " * indent
    lines = [f"{pad}/// <{tag}>"]
    lines += [f"{pad}///     {line}" for line in textwrap.wrap(text, width=112 - indent)]
    lines.append(f"{pad}/// </{tag}>")
    return "\n".join(lines)


def targets(names):
    return " | ".join(f"AttributeTargets.{name}" for name in names)


def header(catalog):
    return [
        "#region Usings declarations",
        "",
        "using System;",
        "",
        "#endregion",
        "",
        f"namespace {NS}.{catalog} {{",
        "",
    ]


def declined_role_class(pattern, role, indent):
    """A role of a declension: the same role, spelled by another catalog.

    It derives from its counterpart rather than from a role base of its own, so it inherits that role's
    targets, its multiplicity and its links instead of restating them — the declension is the same pattern,
    and two spellings that each declared their own could drift apart. Only the marker is added, which is the
    one thing inheritance cannot say.
    """
    pad = " " * indent
    target = relation(pattern)[0]

    out = [doc(role["summary"], indent)]
    out.append(f"{pad}[Declension]")
    out.append(f"{pad}public {role['_modifier']}class {role['name']}Attribute : "
               f"{target['catalog']}.{target['name']}.{role['name']}Attribute {{ }}")
    return "\n".join(out)


def role_class(pattern, role, indent):
    if relation(pattern)[1] == "declension":
        return declined_role_class(pattern, role, indent)

    pad = " " * indent
    # Multiplicity comes from the catalog, not from the targets. Deriving it worked only for as long as the
    # rule "a member holds its role once, anything else may repeat" happened to hold; an assembly is one
    # bounded context rather than several, and no rule over target kinds recovers that (ADR-0009).
    allow_multiple = "true" if role["repeatable"] else "false"
    inherited = "true" if pattern["inherited"] else "false"

    out = [doc(role["summary"], indent)]
    out.append(f"{pad}[AttributeUsage({targets(role['targets'])}, "
               f"AllowMultiple = {allow_multiple}, Inherited = {inherited})]")
    if not role["links"]:
        out.append(f"{pad}public {role['_modifier']}class {role['name']}Attribute : Role {{ }}")
        return "\n".join(out)

    out.append(f"{pad}public {role['_modifier']}class {role['name']}Attribute : Role {{")
    for link in role["links"]:
        out.append("")
        out.append(doc(f'The <see cref="{link}Attribute" /> this role is bound to. Optional: it is only needed '
                       f"when the type hierarchy alone does not tell which occurrence of the pattern is meant.",
                       indent + 4))
        out.append(f"{pad}    public Type? {link} {{ get; init; }}")
    out.append("")
    out.append(f"{pad}}}")
    return "\n".join(out)


def flat_attribute(pattern):
    """Single-role pattern: a flat attribute, with neither nesting nor argument."""
    name = pattern["name"]
    inherited = "true" if pattern["inherited"] else "false"

    out = header(pattern["catalog"])
    out.append(doc(f"{name} ({CATALOG_LABEL[pattern['catalog']]}) — {pattern['summary']}", 4))
    out.append(remarks(["This pattern has a single role, so there is nothing to choose: the attribute is "
                        "applied on its own.",
                        relation_doc(pattern),
                        reference_doc(pattern)], 4))
    _, nature = relation(pattern)
    if nature == "declension":
        # One pattern, two spellings: they must not end up accepting different targets, so the declension
        # inherits AttributeUsage instead of restating it, and says what it is.
        out.append("    [Declension]")
    else:
        # A specialisation is a pattern of its own, and may legitimately accept fewer targets than the
        # pattern that contains it.
        out.append(f"    [AttributeUsage({targets(pattern['roles'][0]['targets'])}, "
                   f"AllowMultiple = false, Inherited = {inherited})]")
    out.append(f"    public {pattern['_modifier']}class {name}Attribute : {base_of(pattern)} {{ }}")
    out.append("")
    out.append("}")
    return "\n".join(out) + "\n"


def nested_container(pattern):
    """Multi-role pattern: a static container plus one attribute per role."""
    name = pattern["name"]

    out = header(pattern["catalog"])
    out.append(doc(f"{name} ({CATALOG_LABEL[pattern['catalog']]}) — {pattern['summary']}", 4))
    out.append(remarks(["Annotate the declaration that introduces the role. When a role is introduced by an "
                        "interface, annotate that interface rather than each of its implementations.",
                        relation_doc(pattern),
                        reference_doc(pattern)], 4))
    out.append(f"    public static class {name} {{")
    # A declension declares no role base: each of its roles derives from its counterpart, so the container is
    # a spelling and nothing more. Every other pattern gathers its roles under one abstract base — the type
    # every role of the pattern answers, and what a specialisation inherits from to answer the broader one.
    if relation(pattern)[1] != "declension":
        out.append("")
        out.append(doc(f"Role played by a type or a member in the {name} design pattern.", 8))
        out.append(f"        public abstract class Role : {base_of(pattern)} {{ }}")
    for role in pattern["roles"]:
        out.append("")
        out.append(role_class(pattern, role, 8))
    out.append("")
    out.append("    }")
    out.append("")
    out.append("}")
    return "\n".join(out) + "\n"


def is_single_role(pattern):
    return len(pattern["roles"]) == 1 and pattern["roles"][0]["name"] == pattern["name"]


def annotation_of(pattern, role):
    """How the role is written at a declaration — the thing a reader of the index is looking for."""
    return f"[{role['name']}]" if is_single_role(pattern) else f"[{pattern['name']}.{role['name']}]"


def anchor_of(pattern):
    """A heading unique across catalogs: ValueObject is held by two of them."""
    label = CATALOG_LABEL[pattern["catalog"]]
    slug = f"{pattern['name']} ({label})".lower().replace(" ", "-")

    return "".join(c for c in slug if c.isalnum() or c == "-")


def relation_line(pattern):
    target, nature = relation(pattern)
    if target is None:
        return None
    where = CATALOG_LABEL[target["catalog"]]
    if nature == "declension":
        return f"The same pattern as **{target['name']}** ({where}), which holds its definition."
    return f"A narrower case of **{target['name']}** ({where}): every participant here is one of those too."


def index_entry(pattern):
    """One pattern, as the index shows it."""
    name = pattern["name"]
    label = CATALOG_LABEL[pattern["catalog"]]

    out = [f"### {name} ({label})", "", pattern["summary"], "", f"*{reference_doc(pattern)}*".replace("<i>", "").replace("</i>", "")]

    line = relation_line(pattern)
    if line is not None:
        out += ["", line]

    out += ["", "| Role | Annotation | Applies to | Repeatable | Links |", "|---|---|---|---|---|"]
    for role in pattern["roles"]:
        targets_read = ", ".join(target.lower() for target in role["targets"])
        repeatable = "no" if role["targets"] == ["Method"] else "yes"
        links = ", ".join(f"`{link}`" for link in role["links"]) or "—"
        out.append(f"| {role['name']} | `{annotation_of(pattern, role)}` | {targets_read} | {repeatable} | {links} |")

    for role in pattern["roles"]:
        out += ["", f"**{role['name']}** — {role['summary']}"]

    source = f"../../Reefact.LivingDocumentation.Attributes/{pattern['catalog']}/{name}.cs"
    sample = f"../../Reefact.LivingDocumentation.Attributes.Usage/{pattern['catalog']}/{name}Usage.cs"
    out += ["",
            f"Held by a subtype: {'yes' if pattern['inherited'] else 'no'} · "
            f"[Source]({source}) · [Sample]({sample})"]

    return "\n".join(out)


def index_document(patterns):
    """The catalog as a reader browses it, rather than as the compiler consumes it."""
    roles = sum(len(pattern["roles"]) for pattern in patterns)
    catalogs = sorted({pattern["catalog"] for pattern in patterns})

    out = ["# The catalog",
           "",
           "<!-- Generated by catalog/generate.py from catalog/<Catalog>/<Pattern>.json. Do not edit. -->",
           "",
           f"**{len(patterns)} patterns, {roles} roles**, across {len(catalogs)} catalogs. A pattern is "
           "catalogued in the body of work that named it, under the name that work gave it — so a reader of a "
           "book finds its patterns spelled as it spelled them, and two works that name one pattern are "
           "related rather than merged.",
           "",
           "## Every pattern",
           "",
           "| Pattern | Catalog | Roles | Related to |",
           "|---|---|---|---|"]

    for pattern in sorted(patterns, key=lambda p: (p["name"], p["catalog"])):
        target, nature = relation(pattern)
        related = "—" if target is None else f"{target['name']} ({nature})"
        out.append(f"| [{pattern['name']}](#{anchor_of(pattern)}) | {CATALOG_LABEL[pattern['catalog']]} | "
                   f"{len(pattern['roles'])} | {related} |")

    for catalog in catalogs:
        owned = sorted((p for p in patterns if p["catalog"] == catalog), key=lambda p: p["name"])
        out += ["", f"## {CATALOG_LABEL[catalog]}", "",
                f"`{NS}.{catalog}` — {len(owned)} patterns, "
                f"{sum(len(p['roles']) for p in owned)} roles.", ""]
        out += ["\n".join([index_entry(pattern), ""]) for pattern in owned]

    return "\n".join(out).rstrip() + "\n"


def main():
    patterns = []
    for path in sorted(glob.glob(os.path.join(HERE, "*", "*.json"))):
        with open(path, encoding="utf-8") as handle:
            patterns.append(json.load(handle))

    index = {key(pattern): pattern for pattern in patterns}

    # Nothing is unsealed that is not actually derived from, so the exceptions in the generated sources are
    # explained by the catalog rather than by the template. A declension is inherited role by role, a
    # specialisation pattern by pattern, so the two unseal different things.
    unsealed_patterns = set()
    unsealed_roles = set()
    for pattern in patterns:
        target, nature = relation(pattern)
        if target is None:
            continue
        base = index.get((target["catalog"], target["name"]))
        if base is None:
            raise SystemExit(f"{pattern['name']}: derives from {target['catalog']}.{target['name']}, "
                             f"which is not in the catalog")
        if nature == "declension" and (is_single_role(pattern) != is_single_role(base)
                                       or role_names(pattern) != role_names(base)):
            raise SystemExit(f"{pattern['name']}: a declension is the same pattern spelled again, so it holds "
                             f"the same roles, in the same order, as {base['name']}")
        if is_single_role(base):
            unsealed_patterns.add(key(base))
        elif nature == "declension":
            unsealed_roles |= {(*key(base), name) for name in role_names(base)}

    for pattern in patterns:
        target, _ = relation(pattern)
        if target is None:
            pattern["_base"] = "LivingDocumentationAttribute"
        else:
            base = index[(target["catalog"], target["name"])]
            pattern["_base"] = (f"{base['catalog']}.{base['name']}Attribute" if is_single_role(base)
                                else f"{base['catalog']}.{base['name']}.Role")
        pattern["_modifier"] = "" if key(pattern) in unsealed_patterns else "sealed "
        for role in pattern["roles"]:
            role["_modifier"] = "" if (*key(pattern), role["name"]) in unsealed_roles else "sealed "

    for pattern in patterns:
        folder = os.path.join(ROOT, pattern["catalog"])
        os.makedirs(folder, exist_ok=True)
        body = flat_attribute(pattern) if is_single_role(pattern) else nested_container(pattern)
        with open(os.path.join(folder, pattern["name"] + ".cs"), "w", encoding="utf-8") as handle:
            handle.write(body)

    # Several hundred patterns are not navigable through a directory listing, and the generated sources are
    # the wrong place to browse: they are read by a compiler, one file at a time. The index is the catalog
    # as a reader meets it — what exists, under which name, with which roles, and where to see it at work.
    os.makedirs(INDEX_DIR, exist_ok=True)
    with open(os.path.join(INDEX_DIR, "catalog-index.md"), "w", encoding="utf-8", newline="\n") as handle:
        handle.write(index_document(patterns))

    for catalog in sorted({pattern["catalog"] for pattern in patterns}):
        owned = [p for p in patterns if p["catalog"] == catalog]
        print(f"{catalog}: {len(owned)} patterns, {sum(len(p['roles']) for p in owned)} roles")
    print(f"TOTAL: {len(patterns)} patterns, {sum(len(p['roles']) for p in patterns)} roles")


if __name__ == "__main__":
    main()
