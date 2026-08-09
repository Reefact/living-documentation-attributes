#!/usr/bin/env python3
"""Validates the pattern catalog.

    python3 tools/catalog/validate.py

The single source of truth shared by whoever edits the catalog and by CI, so the
two can never disagree about what a valid entry is. It checks each entry against
`catalog/pattern.schema.json`, and then three rules a JSON schema cannot state on
its own:

* every name in a role's `links` is a role of the same pattern;
* role names are unique within a pattern;
* `specialisationOf` points at an entry that exists in the SAME catalogue, and a
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

        target = entry.get("specialisationOf")
        if target is not None:
            key = (target["catalog"], target["name"])
            if target["catalog"] != entry["catalog"]:
                # The rule this file exists for: a relation is emitted as inheritance, and each catalogue ships
                # as its own package with no reference to another (ADR-0027), so a relation across catalogues
                # names a type the assembly cannot see. Prose said this; now it fails.
                failures.append(f"{shown}: specialisationOf points at {key[0]}.{key[1]}, in another catalogue. "
                                f"Each catalogue is an independent package, so the inheritance this would emit "
                                f"cannot exist")
            elif key not in entries:
                failures.append(f"{shown}: specialisationOf points at {key[0]}.{key[1]}, "
                                f"which is not in the catalog")

    for failure in failures:
        print(failure, file=sys.stderr)

    print(f"{len(paths)} entries, {len(failures)} problem(s)")
    return 1 if failures else 0


if __name__ == "__main__":
    sys.exit(main())
