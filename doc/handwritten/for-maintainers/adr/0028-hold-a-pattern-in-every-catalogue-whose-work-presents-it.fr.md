# ADR-0028 | Détenir un pattern dans chaque catalogue dont l'œuvre le présente comme le sien

🌍 🇬🇧 [English](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-09
**Accepté :** 2026-08-09
**Décideurs :** Reefact

## Contexte

L'[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) décidait
qu'un pattern est catalogué là où l'œuvre qui l'a *nommé* l'a mis, et là seulement. Quand
une seconde œuvre présentait le même pattern, le catalogue tenait une entrée unique et une
table de `catalog/README.md` redirigeait le lecteur du livre qu'il avait en main vers le
catalogue qui la détenait.

`AnalysisPatterns/KnowledgeLevel` est l'entrée pour laquelle cette table a été écrite. Evans
consacre une section du chapitre 16 de *Domain-Driven Design* au Knowledge Level et crédite
Fowler, qui l'a nommé en 1997. L'entrée est donc sous `AnalysisPatterns`, et la table dit
pourquoi.

L'[ADR-0027](0027-ship-one-independent-package-per-catalogued-work.fr.md) livre chaque œuvre
comme son propre package indépendant et supprime toute relation traversant un catalogue.
Avec elle disparaît la table : une comparaison entre deux œuvres contredit la prétention que
chaque catalogue tient seul.

Reste un lecteur de *Domain-Driven Design* qui installe le package `DomainDrivenDesign`,
ne trouve pas Knowledge Level, et n'a plus rien pour le rediriger. Le trou n'est pas
hypothétique : c'est le premier cas, et il y en a d'autres partout où deux des œuvres
cataloguées couvrent le même terrain.

## Décision

Un pattern est détenu dans chaque catalogue dont l'œuvre le présente comme un de ses propres
patterns, et une œuvre qui cite seulement le pattern d'une autre ne le détient pas.

## Justification

L'indépendance promet qu'un catalogue est le rendu complet d'une œuvre. Un lecteur qui adopte
un livre et installe son package doit y trouver ce que ce livre lui a appris, sinon la
promesse est fausse et la redirection qui la réparait n'existe plus.

Le critère doit être la **paternité, non la mention**, parce que la mention est sans borne.
Chacun de ces livres cite les autres ; une règle fondée sur l'apparition remplirait chaque
catalogue de patterns que son auteur n'a jamais revendiqués, et le vocabulaire cesserait de
dire à un lecteur ce qu'une œuvre détient. Le test est de savoir si l'œuvre présente le
pattern comme le sien — le nomme, le décrit, lui donne une place dans son propre langage de
patterns — et non si les mots figurent dans le texte.

Appliqué au Knowledge Level, c'est une question sur le livre d'Evans et non sur ce dépôt,
et c'est cette propriété qui rend la règle utilisable : deux personnes lisant le chapitre 16
s'accorderont sur le fait qu'Evans présente un pattern ou en crédite un.

La duplication ainsi admise est le prix de l'indépendance et non un défaut de celle-ci. Deux
catalogues décrivant une idée avec les mots de deux œuvres, c'est ce que deux livres ont
effectivement fait, et un consommateur qui lit l'un des deux veut la formulation de ce
livre-là. Ce qui est perdu, c'est l'affirmation que les deux ne font qu'un pattern — et cette
affirmation était déjà ramenée à une comparaison que le consommateur ne peut pas vérifier,
ce dont l'ADR-0027 débat longuement.

Trancher maintenant plutôt qu'au premier trou constaté importe, parce que la règle change ce
que « complet » signifie pour un catalogue déjà déclaré complet. Chaque catalogue doit être
relu une fois à son aune, et le faire une fois coûte moins que le faire à chaque plainte.

## Alternatives envisagées

### Conserver l'ADR-0006 tel quel : seule l'œuvre qui nomme détient le pattern

Envisagée parce qu'elle ne demande aucun changement, et qu'elle garde chaque pattern en
exactement un endroit — plus simple à maintenir et impossible à rendre incohérent.

Rejetée parce que ce qui la rendait praticable a disparu. Elle reposait sur une table
inter-catalogue pour rediriger le lecteur, et cette table ne survit pas à des catalogues
indépendants. Laissée telle quelle, la règle produit silencieusement des packages qui sont
des rendus incomplets de leur propre œuvre : le lecteur d'Evans ne trouve pas de Knowledge
Level, et aucune explication.

### Détenir un pattern dans chaque catalogue dont l'œuvre le mentionne

Envisagée parce que c'est la lecture la plus large de la complétude, et qu'elle garantit
qu'aucun lecteur ne reparte les mains vides.

Rejetée parce qu'une citation n'est pas une paternité. Ces livres se citent constamment, et
le résultat serait des catalogues bourrés de patterns que leurs auteurs ne revendiquent pas
— ce qui détruit la seule chose à quoi sert un catalogue, dire ce qu'une œuvre détient.

### Le détenir une fois, et faire porter à chaque package un pointeur vers son emplacement

Envisagée parce qu'elle évite la duplication tout en répondant au lecteur.

Rejetée parce que ce pointeur est une relation inter-catalogue déguisée. Soit il compile, et
les packages sont couplés à nouveau ; soit il ne compile pas, et c'est de la donnée non
vérifiée exactement du genre que l'ADR-0027 a rejeté.

## Conséquences

### Positives

* Chaque package est le rendu complet de son œuvre, ce qui fait du choix d'un package un
  choix réel et non partiel.
* L'appartenance d'une entrée se tranche en lisant l'œuvre, non en consultant l'histoire de
  ce dépôt et l'ordre dans lequel les catalogues ont été ouverts.
* L'appartenance cesse de dépendre des dates de publication : l'ordre de catalogage ne
  transparaît plus nulle part.

### Négatives

* Les entrées se multiplient partout où deux œuvres couvrent le même terrain, et chaque
  doublon est écrit et maintenu séparément.
* Deux rendus d'une même idée peuvent diverger dans leur formulation, et rien ne le détecte.
* Chaque catalogue déjà déclaré complet doit être relu à l'aune de cette règle, y compris les
  quatre terminés avant qu'elle n'existe.

### Risques

* « Présente comme le sien » est un jugement sur un livre, et deux mainteneurs pourraient
  différer sur un cas limite. Knowledge Level en est un : Evans lui donne un titre de section
  *et* crédite Fowler, et la réponse décide si `DomainDrivenDesign` gagne une entrée.
* La règle invite à sur-collecter. Un relecteur incertain tendra à ajouter, et un catalogue
  gonflé de patterns que son auteur n'a fait qu'emprunter est précisément l'échec que cette
  décision doit empêcher.

## Actions de suivi

* Trancher le Knowledge Level : Evans le présente-t-il comme un de ses propres patterns, ou
  crédite-t-il celui de Fowler ? C'est la première application de la règle, et celle qui l'a
  provoquée.
* Relire une fois chacun des cinq catalogues à l'aune de la règle, et lister ce qu'elle
  ajoute, afin qu'une entrée manquante se distingue d'une absence décidée.
* Consigner le résultat dans `catalog/README.md` sous forme de liste par catalogue, et non de
  comparaison entre catalogues — ce fichier doit cesser de mettre deux œuvres côte à côte.

## Références

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) — la règle
  que celle-ci remplace, et la table de redirection dont elle dépendait.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.fr.md) — pourquoi la table
  disparaît, et pourquoi la duplication est assumée plutôt que réparée par une relation.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) — ce qui décide
  encore si deux entrées *d'un même catalogue* ne font qu'un pattern ; cela n'atteint plus
  l'inter-catalogue.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md) — `Idioms`
  détient ce qu'aucune œuvre ne réclame, ce que cette règle laisse intact et rend plus net.
