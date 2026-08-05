# ADR-0011 | Laisser hors du catalogue ce qui ne peut pas être annoté

🌍 🇬🇧 [English](0011-leave-out-what-cannot-be-annotated.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Tout ce que la littérature appelle un pattern ne peut pas être attaché à quelque
chose qu'un attribut C# sait atteindre. Un *Module* en Domain-Driven Design
qualifie un espace de noms, et C# n'a pas d'attribut au niveau d'un espace de
noms. Une relation *Conformist* qualifie le lien entre deux contextes bornés, qui
n'est ni un type, ni un membre, ni un assembly.

Des contournements existent. Un type marqueur conventionnel pourrait tenir lieu
d'espace de noms ; une propriété sur un attribut au niveau assembly pourrait en
nommer un ; un lien pourrait porter l'autre extrémité d'une relation. Chacun
mettrait dans le code quelque chose dont l'unique raison d'être serait d'être
annoté.

Le catalogue se veut aussi un ouvrage de référence, et la tentation est de le
vouloir complet : un pattern que la littérature nomme, absent d'un catalogue qui
prétend couvrir sa source, se lit comme un oubli.

Le vocabulaire est jugé par ce qui peut être affirmé au travers de lui (ADR-0007).
Un rôle que rien ne peut tenir n'autorise aucune assertion, puisqu'il n'y a rien
sur quoi une règle puisse porter.

## Décision

Un pattern qui ne peut être attaché ni à un type, ni à un membre, ni à un assembly
n'est pas dans le catalogue.

## Justification

Une entrée que rien ne peut porter est une entrée que rien ne peut vérifier : elle
échoue donc au critère sur lequel repose tout le reste du catalogue. L'inclure
mettrait dans le vocabulaire un nom qui n'apparaîtrait jamais dans aucune base de
code et ne participerait jamais à aucune règle.

Un type marqueur conventionnel serait pire que l'absence. Il invente une
déclaration dont l'unique raison d'exister est d'être annotée : le code porterait
alors un artefact du système de documentation plutôt que de la conception
documentée — ce qui inverse la prémisse selon laquelle les annotations décrivent
du code qui existerait de toute façon.

Refuser le contournement maintient la frontière du modèle honnête et visible. Le
catalogue n'approxime pas en silence : ce qu'il ne sait pas exprimer, il ne
prétend pas l'exprimer, et un contributeur qui rencontre la lacune y voit une
limite plutôt qu'une convention à imiter.

L'argument de complétude trouve sa réponse ailleurs. Un pattern laissé de côté
appartient toujours à son corpus et a sa place dans la documentation et dans
l'index ; ce qu'il n'obtient pas, c'est un attribut, parce qu'il n'y aurait rien
sur quoi le poser.

## Alternatives envisagées

### Introduire un type marqueur conventionnel par espace de noms

Envisagé parce que c'est une petite convention, qu'elle est découvrable, et
qu'elle permettrait d'exprimer `Module` et les patterns stratégiques de relation.

Rejeté parce que cela demande à une base de code d'ajouter un type qui n'existe
que pour être annoté. L'annotation décrirait alors le système de documentation
plutôt que la conception, et le marqueur devrait être tenu en phase avec un espace
de noms auquel rien ne le lie.

### Porter l'espace de noms comme une chaîne sur un attribut de niveau assembly

Envisagé parce que cela n'exige aucun type nouveau et atteint la granularité de
l'espace de noms.

Rejeté parce que c'est une chaîne magique, non vérifiée et désynchronisée au
premier renommage — la raison même pour laquelle une clé sous forme de chaîne a
été rejetée pour les occurrences de patterns.

### Inclure le pattern sans aucune cible, par souci de complétude

Envisagé parce que le catalogue est aussi une référence, et qu'un pattern absent
se lit comme une omission.

Rejeté parce qu'un attribut que personne ne peut appliquer est un nom dans un
assembly et rien de plus, et parce qu'un ouvrage de référence est mieux servi par
une documentation capable de dire pourquoi le pattern n'est pas annotable.

## Conséquences

### Positives

* Toute entrée du catalogue peut être appliquée, et donc vérifiée.
* Le code documenté ne gagne aucun artefact issu du système de documentation.
* Les limites du modèle sont visibles plutôt que masquées.

### Négatives

* Le catalogue n'est pas une transcription complète de ses sources : un lecteur
  peut donc chercher un pattern délibérément absent.
* Le Domain-Driven Design y perd `Module`, et les patterns stratégiques de
  relation sont réduits à ceux qui qualifient un assembly.

### Risques

* La règle s'applique pattern par pattern au jugé, et un pattern qui aurait pu
  être exprimé maladroitement peut être écarté alors qu'une meilleure forme
  existait.
* Une future fonctionnalité C# — un attribut au niveau d'un espace de noms —
  rouvrirait des entrées closes sous cette décision, ce qui exigerait alors un
  enregistrement de remplacement plutôt qu'un ajout discret.

## Actions de suivi

* Consigner dans la documentation du catalogue quels patterns ont été laissés de
  côté pour cette raison, afin que l'absence se lise comme une décision plutôt que
  comme un oubli.

## Références

* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — le
  critère que ceci applique.
* [ADR-0009](0009-let-each-role-declare-what-it-applies-to.md) — ce à quoi un rôle
  peut être attaché.
