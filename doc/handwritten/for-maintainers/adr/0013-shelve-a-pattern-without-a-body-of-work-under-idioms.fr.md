# ADR-0013 | Ranger sous Idioms un pattern sans corpus propre

🌍 🇬🇧 [English](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Le catalogue est organisé par corpus, et un pattern est placé dans celui qui l'a
nommé (ADR-0006). Certains patterns n'ont pas de corpus. Null Object a une source
— Woolf, dans le troisième volume de *Pattern Languages of Program Design* — mais
aucun catalogue propre ; Result et un certain nombre de pratiques quotidiennes ont
une filiation et aucune publication unique.

Un espace de noms par source donnerait à chacun d'eux un catalogue d'une seule
entrée, et la liste des catalogues se remplirait d'ouvrages et d'articles qui ne
partagent rien sinon de n'appartenir à rien d'autre.

L'espace de noms racine a été envisagé pour les accueillir. Il porte l'attribut de
base et le marqueur de déclinaison, et c'est ce qu'un consommateur importe en
premier.

Une source et un corpus ne sont pas la même chose. La provenance peut être
consignée pour un pattern qui n'a pas de catalogue, et le champ de référence la
porte, qu'un espace de noms en porte le nom ou non.

## Décision

Un pattern doté d'une source mais sans corpus propre est catalogué sous `Idioms`.

## Justification

Cela préserve le principe d'organisation sans inventer de catalogues à entrée
unique. Tout autre espace de noms répond *de quel ouvrage ceci vient-il* ;
`Idioms` répond *d'aucun ouvrage en particulier*, ce qui est une véritable réponse
et non une absence, et la référence consigne toujours d'où le pattern vient
réellement.

Le terme est établi pour désigner une solution récurrente située en dessous du
niveau d'un pattern architectural publié : il dit donc quelque chose des entrées
plutôt qu'il ne se contente de regrouper les restes. Un lecteur qui le parcourt
apprend quelle sorte de chose il contient.

Il reste hors de la racine parce que la racine est un hall d'entrée. Y placer des
patterns mêlerait le vocabulaire aux deux types qui décrivent le vocabulaire, et
n'offrirait de toute façon aucune protection : un pattern qui acquiert plus tard
un corpus change d'espace de noms dans les deux cas, et c'est une rupture de
compatibilité depuis la racine autant que depuis `Idioms`.

Il nomme l'absence de catalogue, non l'absence de source. Cette distinction est ce
qui permet à une entrée d'y être tout de même placée par provenance et tout de
même ordonnée face à un autre catalogue par date de publication : les règles de
l'ADR-0006 s'y appliquent inchangées.

## Alternatives envisagées

### Donner à chaque source son propre espace de noms

Envisagé parce que cela applique l'ADR-0006 sans exception et garde la provenance
visible dans l'espace de noms lui-même.

Rejeté parce que cela produit des catalogues d'une entrée, et une liste d'espaces
de noms qu'un lecteur ne peut pas utiliser pour naviguer — le principe
d'organisation survivrait dans la forme et échouerait dans sa finalité.

### Placer ces patterns à la racine

Envisagé parce que cela n'exige aucun nom nouveau, et qu'un lecteur qui importe la
racine les voit immédiatement.

Rejeté parce que la racine porte les types qui décrivent le vocabulaire plutôt que
le vocabulaire lui-même, et parce que cela ne protège de rien : déplacer un
pattern plus tard casse les consommateurs quel que soit son point de départ.

### Rattacher chacun au catalogue existant le plus proche

Envisagé parce que cela évite un nouvel espace de noms et place chaque pattern
près de ses parents.

Rejeté parce que cela affirme une provenance fausse. Null Object sous Patterns of
Enterprise Application Architecture dirait que Fowler l'a nommé, ce qui est
précisément l'affirmation que l'ADR-0006 existe pour empêcher.

## Conséquences

### Positives

* Le placement par provenance tient pour tous les patterns, avec une exception
  honnête plutôt qu'une prolifération de catalogues à entrée unique.
* Le nom dit ce que les entrées ont en commun.
* La provenance reste consignée, dans la référence, pour les patterns dont
  l'espace de noms ne peut pas la porter.

### Négatives

* `Idioms` se définit par ce qu'il n'est pas : sa frontière relève donc du
  jugement et sera discutée pattern par pattern.
* Un pattern qui acquiert plus tard un corpus doit être déplacé, ce qui casse les
  consommateurs.

### Risques

* L'espace de noms peut devenir le défaut de tout ce qui est difficile à placer,
  et glisser de « sans corpus » à « pas encore cherché ». Seule l'obligation de
  consigner une référence y résiste, en forçant la question de l'origine du
  pattern.

## Références

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) — la
  règle de placement que ceci complète.
* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) —
  la relation de Null Object à Special Case, qui vit dans un catalogue propre.
