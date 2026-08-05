# ADR-0013 | Ranger sous Idioms un pattern sans corpus propre

🌍 🇬🇧 [English](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Le catalogue est organisé par corpus, et un pattern est placé dans celui qui l'a
nommé (ADR-0006). Certains patterns n'ont pas de corpus de ce genre. Null Object
a une source — Woolf, dans le troisième volume de *Pattern Languages of Program
Design* — mais pas de catalogue propre ; Result et quantité de pratiques
quotidiennes ont une filiation et aucune publication unique.

Un espace de noms par source donnerait à chacun d'eux un catalogue d'une seule
entrée, et la liste des catalogues se remplirait de volumes et d'articles qui ne
partagent rien sinon de n'appartenir à aucun autre.

L'espace de noms racine a été envisagé comme foyer. Il porte l'attribut de base
et le marqueur de déclinaison, et c'est ce qu'un consommateur importe en premier.

Une source et un corpus ne sont pas la même chose. La provenance peut être
enregistrée pour un pattern qui n'a pas de catalogue, et le champ de référence la
porte, qu'un espace de noms en arbore le nom ou non.

## Décision

Un pattern qui a une source mais pas de corpus propre est catalogué sous
`Idioms`.

## Justification

Cela garde le principe d'organisation intact sans inventer des catalogues d'une
seule entrée. Tous les autres espaces de noms répondent à *de quel travail cela
vient-il* ; `Idioms` répond *d'aucun travail en particulier*, ce qui est une
vraie réponse plutôt qu'une absence, et la référence enregistre malgré tout d'où
le pattern vient réellement.

Le terme est établi pour une solution récurrente située sous le niveau d'un
pattern architectural publié : il dit donc quelque chose des entrées plutôt que
de simplement regrouper les restes. Un lecteur qui le parcourt apprend quel genre
de choses il abrite.

Il reste hors de la racine parce que la racine est un hall d'entrée. Y mettre des
patterns mêlerait le vocabulaire aux deux types qui décrivent le vocabulaire, et
n'offrirait de toute façon aucune protection : un pattern qui gagne plus tard un
corpus change d'espace de noms dans les deux cas, et c'est une rupture de
compatibilité depuis la racine autant que depuis `Idioms`.

Il nomme l'absence de catalogue, pas l'absence de source. C'est cette distinction
qui permet à une entrée d'être encore placée par provenance et encore ordonnée
face à un autre catalogue par date de publication : les règles de l'ADR-0006 s'y
appliquent inchangées.

## Alternatives envisagées

### Donner à chaque source son propre espace de noms

Envisagé parce que cela applique l'ADR-0006 sans exception et garde la provenance
visible dans l'espace de noms lui-même.

Rejeté parce que cela produit des catalogues d'une entrée, et une liste d'espaces
de noms dont un lecteur ne peut pas se servir pour naviguer — le principe
d'organisation survivrait dans la forme et échouerait dans son but.

### Mettre ces patterns à la racine

Envisagé parce que cela n'exige aucun nom nouveau, et qu'un lecteur qui importe
la racine les voit immédiatement.

Rejeté parce que la racine abrite les types qui décrivent le vocabulaire plutôt
que le vocabulaire lui-même, et parce qu'elle ne protège rien : déplacer un
pattern plus tard casse les consommateurs quel que soit son point de départ.

### Rattacher chacun au catalogue existant le plus proche

Envisagé parce que cela évite un espace de noms nouveau et met chaque pattern
près de ses parents.

Rejeté parce que cela affirme une provenance fausse. Null Object sous *Patterns
of Enterprise Application Architecture* dirait que Fowler l'a nommé, ce qui est
l'affirmation que l'ADR-0006 existe pour empêcher.

## Conséquences

### Positives

* Le placement par provenance vaut pour chaque pattern, avec une exception
  honnête plutôt qu'une prolifération de catalogues d'une entrée.
* Le nom dit ce que les entrées ont en commun.
* La provenance reste enregistrée, dans la référence, pour les patterns dont
  l'espace de noms ne peut pas la porter.

### Négatives

* `Idioms` se définit par ce qu'il n'est pas : sa frontière relève du jugement et
  sera discutée pattern par pattern.
* Un pattern qui acquiert plus tard un corpus doit déménager, ce qui casse les
  consommateurs.

### Risques

* L'espace de noms peut devenir le défaut de tout ce qui est difficile à placer,
  et glisser de « pas de corpus » à « pas encore cherché ». Seule l'exigence
  d'enregistrer une référence y résiste, en forçant la question de savoir d'où le
  pattern vient.

## Références

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) —
  la règle de placement que ceci complète.
* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.fr.md) —
  la relation de Null Object à Special Case, qui vit dans un catalogue propre.
