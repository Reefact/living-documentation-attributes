# ADR-0001 | Contrôler chaque pull request au regard de la base d'ADR

🌍 🇬🇧 [English](0001-check-every-pull-request-against-the-adr-base.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Ce dépôt publie un vocabulaire. Ses attributs ne portent aucun comportement, si
bien que presque rien de ce qu'il décide n'est défendu par le code : la forme
d'un rôle, ce à quoi ce rôle peut s'appliquer, quel catalogue accueille un
pattern, si deux patterns n'en font qu'un — rien de tout cela ne produit
d'erreur de compilation lorsque c'est écrit autrement. Un relecteur qui lit un
fichier généré voit le résultat d'une décision sans pouvoir le distinguer d'un
accident.

Les attributs sont générés depuis un catalogue, ce qui creuse encore l'écart. Le
générateur émettrait tout aussi volontiers une autre forme, et une modification
qui le touche réécrit tous les patterns d'un coup. Une décision prise une fois et
laissée sans trace n'est donc pas seulement non documentée : elle est invisible
dans un diff de deux cents fichiers qui ont tous changé pour la même raison.

Le catalogue doit par ailleurs croître d'un ordre de grandeur, à travers des
corpus publiés à des décennies d'intervalle. Des questions déjà tranchées ici —
comment se décide la provenance, ce qui fait que deux patterns n'en sont qu'un,
ce qui peut être annoté — se reposeront pour chaque catalogue ajouté, à des
contributeurs et à des agents qui n'auront pas participé à les trancher.

Les premières entrées ont été produites avec l'aide d'un agent, et les défauts
qui ont survécu étaient exactement de cette nature : ensembles de rôles
silencieusement incomplets, classe de base silencieusement non héritée, cibles
silencieusement incohérentes. Chacun était plausible pris isolément et n'était
visible qu'au regard d'une règle que personne n'avait écrite.

## Décision

Chaque pull request est contrôlée au regard de la base d'ADR, et une pull request
qui embarque une décision durable porte l'ADR qui l'enregistre.

## Justification

Le contrôle est obligatoire et l'artefact ne l'est pas, parce que la plupart des
changements n'embarquent aucune décision : ajouter un pattern au catalogue
exerce des décisions déjà prises plutôt qu'il n'en prend de nouvelles. Ce qui ne
doit pas arriver, c'est qu'une décision entre en silence, et seul un contrôle
systématique au moment où le code est proposé peut l'attraper.

L'alternative à l'enregistrement, c'est la mémoire, et ce dépôt est le mauvais
endroit pour s'y fier. Rien dans un attribut généré n'explique pourquoi il a la
forme qu'il a : un mainteneur qui y revient, ou un agent qui y travaille, n'a
aucun chemin de retour vers le raisonnement en dehors de cette base. Ce
raisonnement n'est pas récupérable depuis la sortie ; il faut le conserver.

Signaler un conflit plutôt que le résoudre garde la base honnête. Un changement
qui contredit un ADR accepté est soit une erreur, soit une meilleure idée, et
seul un mainteneur peut dire laquelle. Un agent qui remodèle discrètement le code
pour l'accorder à une intuition nouvelle efface le relevé qu'il était censé
consulter.

Rédiger en *Proposé* sans jamais accepter découle du même raisonnement. Un ADR
accepté est une position ratifiée ; ratifier relève d'un jugement sur le projet,
pas sur le changement en cours de revue, et ce n'est pas à son auteur de le
porter — pas plus qu'il ne fusionnerait sa propre pull request.

## Alternatives envisagées

### Documenter les décisions dans le code, en documentation XML

Envisagé parce que le dépôt documente déjà abondamment, et que le lecteur est
juste là.

Rejeté parce que la documentation portée par un type généré est régénérée avec
lui, et parce qu'elle ne peut énoncer que la décision, jamais ce qui a été pesé
contre elle. Les alternatives envisagées sont la part dont un futur mainteneur a
le plus besoin, et elles n'ont nulle part où loger dans un résumé.

### Consigner les décisions dans les messages de commit

Envisagé parce que les décisions prises jusqu'ici ont effectivement été
argumentées dans des messages de commit, et longuement.

Rejeté parce qu'un message de commit s'adresse à un relecteur à un instant donné
et n'est retrouvé ensuite que par quelqu'un qui soupçonne déjà son existence. Une
base indexée, consultée avant d'écrire du code, est un instrument différent d'un
historique fouillé après coup.

### N'écrire un ADR que lorsqu'une décision est contestée

Envisagé parce que cela garderait la base réduite et chaque entrée porteuse.

Rejeté parce que le caractère contesté d'une décision devient invisible plus
tard ; plusieurs des décisions enregistrées ici ont été renversées deux ou trois
fois avant de se fixer, et le relevé de ce qui a été tenté et de la raison de
l'échec est précisément ce qui empêche le prochain contributeur de le retenter.

## Conséquences

### Positives

* Le raisonnement derrière un vocabulaire incapable de se défendre est écrit.
* Un contributeur ou un agent dispose d'un seul endroit à consulter avant de
  décider, et d'un seul endroit où regarder quand une forme paraît arbitraire.
* Un changement qui contredit une position établie remonte comme un conflit
  plutôt que comme un renversement silencieux.

### Négatives

* Chaque pull request porte le coût du contrôle, y compris les nombreuses qui
  concluent qu'il n'y a rien à enregistrer.
* La base doit être maintenue, indexée et traduite, et elle grossit avec le
  projet.

### Risques

* Le contrôle peut dégénérer en formalité déclarée plutôt qu'effectuée. Rien ici
  ne l'empêche ; seul le mainteneur qui en lit le résultat le peut.
* Un agent peut mal juger ce qui est significatif, et soit inonder la base, soit
  manquer une vraie décision. Atténué en demandant plutôt qu'en devinant lorsque
  ce n'est pas clair.

## Actions de suivi

* Maintenir [`AGENTS.md`](../../../../AGENTS.md) comme énoncé opératoire de la
  procédure, afin qu'un agent l'applique sans qu'on le lui demande.
* Traduire en français chaque ADR accepté, à côté du fichier canonique anglais.

## Références

* [`AGENTS.md`](../../../../AGENTS.md) — la procédure que suit un agent.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.fr.md) —
  la génération qui rend les décisions invisibles dans un diff.
* Reefact, `first-class-errors`, ADR-0004 — la pratique que ce dépôt adopte.
