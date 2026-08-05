#!/usr/bin/env python3
"""Validates the pattern catalog.

    python3 tools/catalog/validate.py

The single source of truth shared by whoever edits the catalog and by CI, so the
two can never disagree about what a valid entry is. It checks each entry against
`catalog/pattern.schema.json`, and then three rules a JSON schema cannot state on
its own:

* every name in a role's `links` is a role of the same pattern;
* role names are unique within a pattern;
* `declensionOf` and `specialisationOf` point at an entry that exists, and a
  declension never derives from a work published later than its own — the
  anteriority that decides which side holds the definition (ADR-0006).

Exit status: 0 when every entry is valid, 1 otherwise.
"""

import glob
import json
import os
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.dirname(os.path.dirname(HERE))
CATALOG = os.path.join(REPO, "catalog")


def main():
    try:
        from jsonschema import Draft202012Validator
    except ImportError:
        print("validate: jsonschema is not installed — pip install jsonschema", file=sys.stderr)
        return 2

    with open(os.path.join(CATALOG, "pattern.schema.json"), encoding="utf-8") as handle:
        schema = json.load(handle)
    Draft202012Validator.check_schema(schema)
    validator = Draft202012Validator(schema)

    entries = {}
    failures = []
    paths = sorted(glob.glob(os.path.join(CATALOG, "*", "*.json")))

    for path in paths:
        shown = os.path.relpath(path, REPO)
        with open(path, encoding="utf-8") as handle:
            entry = json.load(handle)
        entries[(entry.get("catalog"), entry.get("name"))] = (shown, entry)

        for error in validator.iter_errors(entry):
            where = "/".join(str(part) for part in error.path) or "(root)"
            failures.append(f"{shown}: {where}: {error.message}")

    for shown, entry in entries.values():
        names = [role["name"] for role in entry.get("roles", [])]
        if len(names) != len(set(names)):
            failures.append(f"{shown}: two roles share a name")

        for role in entry.get("roles", []):
            for link in role.get("links", []):
                if link not in names:
                    failures.append(f"{shown}: role {role['name']} links to '{link}', "
                                    f"which is not a role of this pattern")

        for field in ("declensionOf", "specialisationOf"):
            target = entry.get(field)
            if target is None:
                continue
            key = (target["catalog"], target["name"])
            if key not in entries:
                failures.append(f"{shown}: {field} points at {key[0]}.{key[1]}, which is not in the catalog")
                continue
            if field == "declensionOf":
                theirs = entries[key][1]["reference"]["year"]
                ours = entry["reference"]["year"]
                if theirs > ours:
                    failures.append(
                        f"{shown}: declensionOf points at {key[0]}.{key[1]}, published in {theirs}, "
                        f"which is later than this entry's {ours} — the earlier work holds the definition")

    for failure in failures:
        print(failure, file=sys.stderr)

    print(f"{len(paths)} entries, {len(failures)} problem(s)")
    return 1 if failures else 0


if __name__ == "__main__":
    sys.exit(main())
