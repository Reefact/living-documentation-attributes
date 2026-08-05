# ADR-0012 | Montrer chaque pattern à l'œuvre dans un exemple métier

🌍 🇬🇧 [English](0012-show-every-pattern-at-work-in-a-business-example.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

La librairie livre des attributs et rien d'autre. Un attribut ne peut être exercé
par un test unitaire en aucun sens utile — il n'y a aucun comportement à
affirmer — de sorte que la preuve habituelle qu'une librairie fonctionne ne
s'applique pas ici.

Deux choses demandent pourtant à être prouvées. Que chaque rôle peut réellement
être appliqué à quelque chose de plausible : un ensemble de cibles trop étroit ne
se découvre que lorsque quelqu'un essaie. Et que tout le catalogue peut être relu
génériquement, ce qui est le contrat de l'ADR-0004 et n'est sinon qu'une
affirmation.

Les règles de lecture vivent dans la documentation plutôt que dans le code
(ADR-0004) : il faut donc que quelque chose les énonce de façon exécutable,
faute de quoi ce ne sont que des phrases non vérifiées.

Le vocabulaire est aussi censé enseigner. Les descriptions de rôles portent ce
que fait un participant, et elles sont la principale raison pour laquelle un
lecteur choisit un rôle plutôt qu'un autre ; mais une description isolée ne
montre pas pourquoi on a eu recours à un pattern, ni quand il serait le mauvais
choix.

Les exemples de manuel font ici un mauvais matériau pédagogique. Un arbre
d'expressions démontre la mécanique du Visitor et ne dit rien du moment où une
équipe devrait l'employer, qui est la part réellement difficile.

## Décision

Chaque pattern est exercé par un fichier d'exemple qui annote un cas métier
réaliste, documenté de sorte que l'exemple explique le pattern.

## Justification

Compiler les exemples est le seul contrôle disponible. Un rôle qui ne peut être
appliqué à rien de sensé ne compile pas dans l'exemple, ce qui fait passer les
ensembles de cibles de l'ADR-0009 de la déclaration à l'exercice — et c'est la
suite d'exemples, non une suite de tests, qui a attrapé l'absence de `Struct`
parmi les cibles.

Un lecteur générique qui parcourt les exemples vérifie de bout en bout ce
qu'affirme l'ADR-0004 : il traverse tout le catalogue à travers le seul attribut
de base, et sa sortie est ce qu'obtiendrait un consommateur. Parce qu'il applique
les règles de lecture documentées, il en est aussi l'énoncé exécutable, ce qui
est ce qui empêche des conventions documentées de dériver.

Des exemples métier réalistes portent ce qu'une description ne peut pas porter.
Un pattern est choisi à cause d'une propriété d'une situation — les natures de
cargaison sont figées par la réglementation quand les calculs qui portent dessus
n'arrêtent pas d'arriver — et c'est cette propriété dont un lecteur a besoin pour
reconnaître la situation dans son propre travail. Un exemple de manuel n'a pas de
situation.

Varier les domaines sert la même fin par l'autre bout. Un lecteur qui travaille
dans l'agriculture et ne rencontre jamais que des exemples bancaires apprend la
mécanique et non la reconnaissance ; répartir les exemples entre l'élevage
bovin, la logistique, les mathématiques, le fret, la finance et le reste offre à
davantage de lecteurs une situation qu'ils connaissent. Le domaine est choisi
pour convenir au pattern et non l'inverse — un pattern forcé dans un domaine qui
ne lui convient pas enseigne deux fois la mauvaise leçon.

Un fichier par pattern rend navigable la correspondance avec le catalogue, et
fait de l'exemple l'endroit évident où regarder quand la description d'un rôle ne
suffit pas.

## Alternatives envisagées

### Écrire des tests unitaires classiques sur les attributs

Envisagé parce que c'est ce qu'une librairie livre normalement, et que la
réflexion pourrait affirmer la structure de chaque type généré.

Rejeté comme insuffisant plutôt que faux : de tels tests vérifieraient que le
générateur a fait ce qu'on lui a dit, pas que ce qu'on lui a dit est utilisable.
Qu'un rôle puisse être appliqué à un participant plausible ne trouve sa réponse
qu'en l'appliquant. Les tests de convention restent utiles à ajouter, et
compléteraient ceci plutôt que de le remplacer.

### Écrire un exemple minimal par pattern

Envisagé parce que ce serait beaucoup moins de travail et que cela compilerait
tout aussi bien.

Rejeté parce que cela prouve l'applicabilité et n'enseigne rien. La description
dans l'attribut énonce déjà ce qu'est un rôle ; un exemple qui n'ajoute que de la
syntaxe la duplique.

### Employer un seul domaine cohérent pour tout le catalogue

Envisagé parce qu'un exemple filé permettrait aux patterns de s'appuyer les uns
sur les autres, et se lirait comme un système unique.

Rejeté parce que deux cents patterns ne tiennent pas dans un domaine sans
distorsion, et que la distorsion est précisément ce qui induit en erreur : un
pattern tordu pour entrer enseigne qu'il s'applique là où il ne s'applique pas.

## Conséquences

### Positives

* Chaque rôle est prouvé applicable, de la seule façon disponible.
* Le contrat de lecture générique est vérifié plutôt qu'affirmé.
* Les règles de lecture documentées ont un pendant exécutable.
* Un lecteur apprend quand recourir à un pattern, pas seulement comment
  l'orthographier.

### Négatives

* Les exemples forment un vaste corps de code écrit à la main — de l'ordre de la
  librairie elle-même — et il grossit avec le catalogue.
* Écrire un exemple réaliste exige une connaissance du domaine que l'auteur peut
  ne pas avoir, et un exemple faux enseigne quelque chose de faux sur le domaine.

### Risques

* Les exemples peuvent dériver du catalogue : un pattern ajouté sans son exemple
  n'est attrapé que par la revue.
* Un exemple réaliste mais faux sur son domaine est pire qu'un exemple neutre, et
  rien dans le dépôt ne peut le détecter.

## Actions de suivi

* Nommer chaque exemple d'après son pattern, afin qu'un exemple manquant soit
  visible en comparant deux répertoires.

## Références

* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.fr.md) — le contrat de
  lecture que le lecteur d'exemple vérifie.
* [ADR-0009](0009-let-each-role-declare-what-it-applies-to.fr.md) — les ensembles
  de cibles que les exemples exercent.
* [ADR-0010](0010-annotate-the-declaration-that-introduces-a-role.fr.md) — la
  convention que les exemples démontrent.
