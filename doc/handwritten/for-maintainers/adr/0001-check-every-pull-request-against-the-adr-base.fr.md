# ADR-0001 | Contrôler chaque pull request au regard de la base d'ADR

🌍 🇬🇧 [English](0001-check-every-pull-request-against-the-adr-base.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Ce dépôt publie un vocabulaire. Ses attributs ne portent aucun comportement, si
bien que presque rien de ce qu'il décide n'est défendu par le code : la forme d'un
rôle, ce à quoi un rôle peut s'appliquer, le catalogue qui héberge un pattern, le
fait que deux patterns n'en fassent qu'un — rien de tout cela ne produit d'erreur
de compilation lorsque c'est écrit autrement. Un relecteur qui parcourt un fichier
généré voit le résultat d'une décision sans pouvoir le distinguer d'un accident.

Les attributs sont générés depuis un catalogue, ce qui creuse encore l'écart. Le
générateur émettrait une autre forme avec la même docilité, et une modification
qui le touche réécrit tous les patterns d'un coup. Une décision prise une fois et
laissée sans trace n'est donc pas seulement non documentée : elle est invisible
dans un diff de deux cents fichiers qui ont tous changé pour la même raison.

Le catalogue est par ailleurs appelé à croître d'un ordre de grandeur, à travers
des corpus publiés à des décennies d'intervalle. Des questions déjà tranchées ici
— comment se décide la provenance, ce qui fait que deux patterns sont le même, ce
qui peut être annoté — se reposeront pour chaque catalogue ajouté, à des
contributeurs et à des agents qui n'auront pas participé à les trancher.

Les premières entrées ont été produites avec l'assistance d'un agent, et les
défauts qui ont survécu étaient exactement de cette nature : des ensembles de
rôles silencieusement incomplets, une classe de base silencieusement non héritée,
des cibles silencieusement incohérentes. Chacun était plausible pris isolément et
n'apparaissait qu'au regard d'une règle que personne n'avait écrite.

## Décision

Chaque pull request est contrôlée au regard de la base d'ADR, et une pull request
qui embarque une décision durable porte l'ADR qui l'enregistre.

## Justification

Le contrôle est obligatoire et l'artefact ne l'est pas, parce que la plupart des
changements n'embarquent aucune décision : ajouter un pattern au catalogue met en
œuvre des décisions déjà prises plutôt qu'il n'en prend de nouvelles. Ce qui ne
doit pas arriver, c'est qu'une décision entre en silence, et seul un contrôle
systématique au moment où le code est proposé peut l'attraper.

L'alternative à l'enregistrement est la mémoire, et ce dépôt est le mauvais
endroit pour s'y fier. Rien dans un attribut généré n'explique pourquoi il a la
forme qu'il a : un mainteneur qui y revient, ou un agent qui y travaille, n'a
aucun chemin de retour vers le raisonnement en dehors de cette base. Le
raisonnement n'est pas reconstituable depuis le résultat ; il faut le conserver.

Signaler un conflit plutôt que le résoudre maintient la base honnête. Un
changement qui contredit un ADR accepté est soit une erreur, soit une meilleure
idée, et seul un mainteneur peut dire laquelle. Un agent qui remodèle discrètement
le code pour l'accorder à une intuition nouvelle efface le registre qu'il était
censé consulter.

Rédiger en *Proposé* et ne jamais accepter procède du même raisonnement. Un ADR
accepté est une position ratifiée ; ratifier relève d'un jugement porté sur le
projet et non sur le changement en cours de revue, et cela n'appartient pas à son
auteur — pas plus que ne lui appartiendrait la fusion de sa propre pull request.

## Alternatives envisagées

### Documenter les décisions dans le code, en documentation XML

Envisagé parce que le dépôt documente déjà abondamment et que le lecteur est
précisément là.

Rejeté parce que la documentation portée par un type généré est régénérée avec
lui, et parce qu'elle ne peut énoncer que la décision, jamais ce qui a été pesé
face à elle. Les alternatives envisagées sont la partie dont un futur mainteneur a
le plus besoin, et elles n'ont nulle part où vivre dans un résumé.

### Consigner les décisions dans les messages de commit

Envisagé parce que les décisions prises jusqu'ici ont de fait été argumentées dans
les messages de commit, et longuement.

Rejeté parce qu'un message de commit s'adresse à un relecteur à un instant donné
et n'est ensuite retrouvé que par quelqu'un qui soupçonne déjà son existence. Une
base indexée et consultée avant d'écrire du code est un tout autre instrument
qu'un historique fouillé après coup.

### N'écrire un ADR que lorsqu'une décision est contestée

Envisagé parce que la base resterait petite et que chaque entrée y serait
porteuse.

Rejeté parce que le caractère contesté d'une décision est invisible plus tard ;
plusieurs des décisions consignées ici ont été renversées deux ou trois fois avant
de se stabiliser, et le registre de ce qui a été tenté et de la raison de son échec
est précisément ce qui évite au contributeur suivant de le retenter.

## Conséquences

### Positives

* Le raisonnement qui sous-tend un vocabulaire incapable de se défendre est écrit.
* Un contributeur ou un agent dispose d'un seul endroit à consulter avant de
  décider, et d'un seul endroit où regarder quand une forme paraît arbitraire.
* Un changement qui contredit une position acquise émerge comme un conflit plutôt
  que comme un revirement silencieux.

### Négatives

* Chaque pull request supporte le coût du contrôle, y compris les nombreuses qui
  se concluent sans rien à enregistrer.
* La base doit être maintenue, indexée et traduite, et elle grandit avec le
  projet.

### Risques

* Le contrôle peut dégénérer en formalité déclarée plutôt qu'exécutée. Rien ici ne
  l'empêche ; seule la lecture du résultat par le mainteneur le fait.
* Un agent peut mal juger de ce qui est significatif, et soit inonder la base,
  soit manquer une véritable décision. Atténué en demandant plutôt qu'en devinant
  lorsque le doute subsiste.

## Actions de suivi

* Maintenir [`AGENTS.md`](../../../../AGENTS.md) comme énoncé opératoire de la
  procédure, afin qu'un agent l'applique sans qu'on le lui demande.
* Traduire en français chaque ADR accepté, à côté du fichier canonique anglais.

## Références

* [`AGENTS.md`](../../../../AGENTS.md) — la procédure que suit un agent.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  la génération qui rend les décisions invisibles dans un diff.
* Reefact, `first-class-errors`, ADR-0004 — la pratique que ce dépôt adopte.
