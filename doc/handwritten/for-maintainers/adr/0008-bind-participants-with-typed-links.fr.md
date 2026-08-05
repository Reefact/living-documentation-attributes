# ADR-0008 | Lier les participants d'une occurrence de pattern par des liens typés

🌍 🇬🇧 [English](0008-bind-participants-with-typed-links.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Une base de code réelle contient plusieurs occurrences du même pattern : trois
chaînes de responsabilité, deux composites, une poignée de visiteurs. Une
annotation qui dit seulement *cette classe est un Handler* les laisse dans un
ensemble indifférencié.

Cet ensemble suffit aux règles qui traitent un rôle comme une catégorie —
*aucune entité ne dépend de l'infrastructure*, *tout dépôt expose une interface*.
Il ne suffit pas à ce qui a besoin de la structure du pattern : *toute feuille
implémente son composant*, *tout visiteur concret implémente son visiteur*. Il ne
suffit pas non plus à dessiner un diagramme, puisqu'un diagramme de neuf classes
sans arêtes est une liste.

La plupart du temps, le graphe de types détient déjà la réponse. Une feuille
implémente l'interface du composant : à quel composite elle appartient est donc
dérivable sans que rien ne soit annoté. Ce n'est que là où un type participe à
plusieurs occurrences, ou là où la hiérarchie n'exprime pas le lien, que le graphe
ne suffit plus.

Une clé de type chaîne nommant l'occurrence a été proposée puis rejetée : c'est
une valeur magique, non vérifiée par le compilateur, qui se désynchronise au
premier renommage.

## Décision

Un rôle peut déclarer des liens optionnels vers d'autres rôles de son pattern,
portés chacun par un `Type`.

## Justification

Un `Type` est vérifié, suivi par le refactoring et navigable, ce qu'une clé
nommant une occurrence n'est pas. Il nomme en outre quelque chose qui existe
déjà, au lieu d'inventer un identifiant qu'il faudrait garder cohérent à la main
chez chaque participant.

L'optionnalité est le bon défaut parce que le graphe suffit d'ordinaire. Exiger
un lien sur chaque participant rendrait le cas courant verbeux pour servir
l'exception, et demanderait à un auteur de répéter ce que sa propre déclaration
de type dit déjà.

Déclarer les liens par rôle plutôt que sur le pattern les garde signifiants. Un
composant n'a pas de composant ; un lien déclaré une fois pour tout le pattern
laisserait l'écrire, et une forme qui permet l'absurde y invite.

Les liens sont ce qui fait passer le modèle d'un ensemble d'étiquettes à quelque
chose qui a des arêtes, ce qu'exigent à la fois les règles qui ont besoin de la
structure et les diagrammes. C'est la raison de les porter, et la raison pour
laquelle ils valent la surface supplémentaire sur les rôles qui en ont.

## Alternatives envisagées

### Nommer chaque occurrence par une clé de type chaîne

Envisagé parce que c'est le plus petit ajout possible et que cela regroupe les
participants sans référence à aucun type.

Rejeté parce que c'est une valeur magique : rien ne la vérifie, une faute de
frappe scinde une occurrence en deux, et un renommage la laisse pointer vers un
nom qui n'existe plus.

### Exiger le lien sur chaque rôle

Envisagé parce que cela rendrait chaque occurrence explicite et ne laisserait
rien à l'inférence.

Rejeté parce que cela duplique ce que la hiérarchie de types dit dans le cas
ordinaire, et qu'une annotation qui répète la déclaration est la défaillance que
ce dépôt supprime ailleurs.

### Déclarer les liens une fois sur la base de rôle du pattern

Envisagé parce qu'il y a moins à générer et que cela met les liens au même
endroit.

Rejeté parce que cela fait accepter à chaque rôle tous les liens, y compris ceux
qui n'ont aucun sens, et qu'une forme capable d'exprimer l'absurde finira par
servir à cela.

## Conséquences

### Positives

* Les occurrences d'un même pattern peuvent être distinguées : les règles
  structurelles et les diagrammes deviennent possibles.
* Rien n'est écrit que le compilateur ne vérifie.
* Le cas ordinaire reste sans cérémonie.

### Négatives

* Un lien est optionnel : un consommateur ne peut pas compter sur sa présence et
  doit se rabattre sur le graphe de types.
* Quels rôles portent quels liens est une décision éditoriale propre à chaque
  pattern, et une omission n'est visible que de qui avait besoin du lien.

### Risques

* Un lien peut désigner un type qui ne tient pas ce rôle, puisque rien ne
  vérifie l'annotation de la cible. C'est une affirmation fausse plutôt qu'une
  règle cassée, et seules une revue ou un test de convention l'attraperaient.

## Références

* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.fr.md) —
  la forme sur laquelle les liens sont déclarés.
* [ADR-0010](0010-annotate-the-declaration-that-introduces-a-role.fr.md) —
  l'autre moitié du travail qui empêche les annotations de répéter le code.
