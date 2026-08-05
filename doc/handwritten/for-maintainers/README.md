# For maintainers

Documentation written by hand, for whoever maintains or contributes to this
repository. Generated documentation, when there is any, lives under
`doc/generated/`.

## Architecture Decision Records

[`adr/`](adr/README.md) — the dated record of every significant decision, its
context, and its consequences.

This base carries more weight here than in most repositories. The attributes are
generated from a catalog and carry no behaviour, so a reader of the output cannot
tell which of its traits were decided and which merely happened, and the compiler
defends almost none of them. The reasoning lives in the ADR base or nowhere.

Start with [ADR-0001](adr/0001-check-every-pull-request-against-the-adr-base.md),
which makes checking every pull request against the base mandatory, and with
[`AGENTS.md`](../../../AGENTS.md), which is the procedure to follow.
