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


def base_of(pattern):
    """What the generated attribute inherits from."""
    target, _ = relation(pattern)
    if target is None:
        return "LivingDocumentationAttribute"
    return f"{target['catalog']}.{target['name']}Attribute"


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


def role_class(pattern, role, indent):
    pad = " " * indent
    is_member = role["targets"] == ["Method"]
    allow_multiple = "false" if is_member else "true"
    inherited = "true" if pattern["inherited"] else "false"

    out = [doc(role["summary"], indent)]
    out.append(f"{pad}[AttributeUsage({targets(role['targets'])}, "
               f"AllowMultiple = {allow_multiple}, Inherited = {inherited})]")
    if not role["links"]:
        out.append(f"{pad}public sealed class {role['name']}Attribute : Role {{ }}")
        return "\n".join(out)

    out.append(f"{pad}public sealed class {role['name']}Attribute : Role {{")
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
                        reference_doc(pattern)], 4))
    out.append(f"    public static class {name} {{")
    out.append("")
    out.append(doc(f"Role played by a type or a member in the {name} design pattern.", 8))
    out.append("        public abstract class Role : LivingDocumentationAttribute { }")
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


def main():
    patterns = []
    for path in sorted(glob.glob(os.path.join(HERE, "*", "*.json"))):
        with open(path, encoding="utf-8") as handle:
            patterns.append(json.load(handle))

    derived_from = set()
    for pattern in patterns:
        target, _ = relation(pattern)
        if target is not None:
            derived_from.add((target["catalog"], target["name"]))
    for pattern in patterns:
        pattern["_modifier"] = "" if (pattern["catalog"], pattern["name"]) in derived_from else "sealed "
        if relation(pattern)[0] is not None and not is_single_role(pattern):
            raise SystemExit(f"{pattern['name']}: a declension or specialisation of a multi-role pattern "
                             f"is not generated yet")

    for pattern in patterns:
        folder = os.path.join(ROOT, pattern["catalog"])
        os.makedirs(folder, exist_ok=True)
        body = flat_attribute(pattern) if is_single_role(pattern) else nested_container(pattern)
        with open(os.path.join(folder, pattern["name"] + ".cs"), "w", encoding="utf-8") as handle:
            handle.write(body)

    for catalog in sorted({pattern["catalog"] for pattern in patterns}):
        owned = [p for p in patterns if p["catalog"] == catalog]
        print(f"{catalog}: {len(owned)} patterns, {sum(len(p['roles']) for p in owned)} roles")
    print(f"TOTAL: {len(patterns)} patterns, {sum(len(p['roles']) for p in patterns)} roles")


if __name__ == "__main__":
    main()
