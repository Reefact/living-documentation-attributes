#!/bin/sh
# Validate a commit message against this repository's commit convention.
#
# This is the single source of truth shared by the local `commit-msg` hook
# (.githooks/commit-msg) and the CI check (.github/workflows/commit-lint.yml),
# so the two can never diverge. The rules it enforces are documented in
# CONTRIBUTING.md; this script only mirrors them.
#
# Usage:
#   lint-commit-message.sh <path-to-message-file>        # the hook passes $1
#   git log -1 --format=%B <sha> | lint-commit-message.sh --ci -   # CI, per commit
#
# Options:
#   --ci / --strict   CI mode: reject autosquash placeholders (fixup!/squash!/
#                     amend!) instead of skipping them (see the exemptions below).
#
# Exit status: 0 = conforming (or intentionally exempt), 1 = violations found.
#
# Scope note: the header is validated in full — that is where the whole value
# lives. Bodies are prose and are left alone, except for two safe, high-value
# footer checks: the breaking-change double signal, and the shape of a `Refs:`
# footer when one is present.

set -u

TYPES='feat|fix|build|chore|ci|docs|perf|refactor|revert|style|test'
SCOPES='attributes|catalog|usage|doc|build'
TYPES_HUMAN='feat, fix, build, chore, ci, docs, perf, refactor, revert, style, test'
SCOPES_HUMAN='attributes, catalog, usage, doc, build'
MAX=72

# --- options ------------------------------------------------------------------
strict=0
case "${1:-}" in
  --ci|--strict) strict=1; shift ;;
  *) ;; # any other first argument is the message file / '-', handled below
esac

# --- read the message ---------------------------------------------------------
if [ "$#" -lt 1 ] || [ "$1" = "-" ]; then
  msg="$(cat)"
elif [ -f "$1" ]; then
  msg="$(cat "$1")"
else
  printf 'commit-lint: message file not found: %s\n' "$1" >&2
  exit 2
fi

# Strip what git itself strips: the scissors block (verbose commits) and any
# comment lines. Issue refs like "#142" never start a line with '#', so this is
# safe.
msg="$(printf '%s\n' "$msg" | sed -E -e '/^# -+ >8 -+$/,$d' -e '/^#/d')"

subject="$(printf '%s\n' "$msg" | sed -n '1p' | sed 's/[[:space:]]*$//')"

# --- exemptions ---------------------------------------------------------------
# Merge commits carry a git/GitHub-generated message and are always exempt (CI
# also filters them out with --no-merges).
case "$subject" in
  'Merge '*) exit 0 ;;
  *) ;; # not a merge commit: fall through to validation
esac
# Autosquash placeholders are rewritten by a later `git rebase --autosquash`, so
# the local hook lets them through. CI (--ci) rejects them instead: a placeholder
# merged before its rebase would otherwise land, unlinted, in protected history.
case "$subject" in
  'fixup! '*|'squash! '*|'amend! '*)
    if [ "$strict" = 0 ]; then
      exit 0
    fi
    printf 'commit-lint: autosquash placeholder must be squashed away before merge: %s\n' "$subject" >&2
    exit 1
    ;;
  *) ;; # not an autosquash placeholder: fall through to validation
esac

errors=0
errmsgs=''
err() {
  errmsgs="${errmsgs}  - ${1}
"
  errors=$((errors + 1))
  return 0
}

# --- header: presence ---------------------------------------------------------
if [ -z "$subject" ]; then
  err "the commit message is empty"
else
  # header length
  len=${#subject}
  if [ "$len" -gt "$MAX" ]; then
    err "the header is ${len} characters; keep the whole line within ${MAX}"
  fi

  # canonical shape: <type>[(<scope>[,<scope>...])][!]: <lowercase description>
  if ! printf '%s' "$subject" | grep -Eq "^(${TYPES})(\((${SCOPES})(,(${SCOPES}))*\))?!?: [a-z]"; then
    # --- targeted diagnostics so the author knows exactly what to fix ---
    if ! printf '%s' "$subject" | grep -Eq '^[^:]+: .'; then
      err "expected '<type>[(scope)][!]: <description>' — no ': ' after the type"
    fi

    typ="$(printf '%s' "$subject" | sed -E 's/^([a-zA-Z]+).*/\1/')"
    if ! printf '%s' "$typ" | grep -Eq "^(${TYPES})$"; then
      err "unknown or malformed type '${typ}' — use one of: ${TYPES_HUMAN}"
    fi

    if printf '%s' "$subject" | grep -Eq '^[a-z]+\([^)]*, '; then
      err "scopes are comma-separated with no space: '(catalog,usage)', not '(catalog, usage)'"
    fi

    if printf '%s' "$subject" | grep -Eq '^[a-zA-Z]+\('; then
      grp="$(printf '%s' "$subject" | sed -E 's/^[a-zA-Z]+\(([^)]*)\).*/\1/')"
      OLDIFS=$IFS
      IFS=','
      for s in $grp; do
        st="$(printf '%s' "$s" | sed 's/^[[:space:]]*//;s/[[:space:]]*$//')"
        if ! printf '%s' "$st" | grep -Eq "^(${SCOPES})$"; then
          err "unknown scope '${st}' — use one of: ${SCOPES_HUMAN} (a scope names a component, never a file or a class)"
        fi
      done
      IFS=$OLDIFS
    fi

    desc_start="$(printf '%s' "$subject" | sed -E 's/^[^:]*: ?//' | cut -c1)"
    case "$desc_start" in
      [A-Z]) err "the description must start with a lowercase letter (imperative: 'add', not 'Add'/'Added')" ;;
      *) ;; # already lowercase (or non-letter): nothing to report
    esac
  fi

  # trailing period
  case "$subject" in
    *.) err "the header must not end with a period" ;;
    *) ;; # no trailing period: nothing to report
  esac

  # scope order / duplicates (only meaningful when the group is well-formed)
  if printf '%s' "$subject" | grep -Eq "^(${TYPES})\((${SCOPES})(,(${SCOPES}))*\)"; then
    grp="$(printf '%s' "$subject" | sed -E 's/^[a-z]+\(([^)]*)\).*/\1/')"
    sorted="$(printf '%s' "$grp" | tr ',' '\n' | sort -u | tr '\n' ',' | sed 's/,$//')"
    if [ "$grp" != "$sorted" ]; then
      err "scopes must be unique and alphabetical: write '(${sorted})'"
    fi
  fi

  # a scope that repeats the type. Both sets are closed, so the repetition has
  # two forms: 'doc' behind 'docs', and 'build' behind 'build'. It is rejected
  # wherever it appears and not only where it stands alone — the type already
  # says the change is documentation, or the build, and a scope beside it names
  # the other components reached.
  if printf '%s' "$subject" | grep -Eq "^(${TYPES})\((${SCOPES})(,(${SCOPES}))*\)!?: "; then
    typ="$(printf '%s' "$subject" | sed -E 's/^([a-z]+)\(.*/\1/')"
    grp="$(printf '%s' "$subject" | sed -E 's/^[a-z]+\(([^)]*)\).*/\1/')"
    case "$typ" in
      docs) own='doc' ;;
      build) own='build' ;;
      *) own='' ;; # no scope names this type's own component
    esac
    if [ -n "$own" ] && printf '%s' "$grp" | tr ',' '\n' | grep -qx "$own"; then
      rest="$(printf '%s' "$grp" | tr ',' '\n' | grep -vx "$own" | tr '\n' ',' | sed 's/,$//')"
      if [ -n "$rest" ]; then
        err "drop '${own}' from the scope: it only repeats the type — write '${typ}(${rest}):' (a scope must say what the type does not)"
      else
        err "drop the scope: '${typ}(${own})' only repeats the type — write '${typ}:' (a scope must say what the type does not)"
      fi
    fi
  fi
fi

# --- blank line between header and body ---------------------------------------
nlines="$(printf '%s\n' "$msg" | awk 'END { print NR }')"
line2="$(printf '%s\n' "$msg" | sed -n '2p')"
if [ "$nlines" -ge 2 ] && [ -n "$line2" ]; then
  err "leave a blank line between the header and the body"
fi

# --- breaking-change double signal --------------------------------------------
prefix="${subject%%: *}"
has_bang=0
case "$prefix" in *!) has_bang=1 ;; *) ;; esac

has_breaking=0
if printf '%s\n' "$msg" | grep -Eq '^BREAKING CHANGE: '; then
  has_breaking=1
fi
if printf '%s\n' "$msg" | grep -Eq '^(BREAKING[-_]CHANGE|[Bb]reaking[ -][Cc]hange):'; then
  err "the breaking-change footer must read exactly 'BREAKING CHANGE:'"
fi
if [ "$has_bang" = 1 ] && [ "$has_breaking" = 0 ]; then
  err "a '!' in the header requires a 'BREAKING CHANGE:' footer describing the migration"
fi
if [ "$has_bang" = 0 ] && [ "$has_breaking" = 1 ]; then
  err "a 'BREAKING CHANGE:' footer requires a '!' before the colon in the header"
fi

# --- Refs: footer shape -------------------------------------------------------
# Anything that looks like the issue footer must read exactly 'Refs: #<number>'.
bad_refs="$(printf '%s\n' "$msg" | grep -Ei '^ref(s)?:' | grep -Ev '^Refs: #[0-9]+$' || true)"
if [ -n "$bad_refs" ]; then
  err "the issue footer must read 'Refs: #<number>' (e.g. 'Refs: #142')"
fi

# --- verdict ------------------------------------------------------------------
if [ "$errors" -gt 0 ]; then
  printf 'commit-lint: this message does not follow CONTRIBUTING.md\n\n%s\n  subject: %s\n' "$errmsgs" "$subject" >&2
  exit 1
fi
exit 0
