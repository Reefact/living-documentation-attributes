# ADR-0008 | Lier les participants d'une occurrence de pattern par des liens typés

🌍 🇬🇧 [English](0008-bind-participants-with-typed-links.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Une base de code réelle contient plusieurs occurrences d'un même pattern : trois
chaînes de responsabilité, deux composites, une poignée de visiteurs. Une
annotation qui dit seulement *cette classe est un Handler* les laisse dans un
unique ensemble indifférencié.

Cet ensemble suffit aux règles qui traitent un rôle comme une catégorie — *aucune
entité ne dépend de l'infrastructure*, *tout dépôt expose une interface*. Il ne
suffit pas à ce qui a besoin de la structure du pattern : *toute feuille implémente
son composant*, *tout visiteur concret implémente son visiteur*. Il ne suffit pas
davantage à dessiner un diagramme, puisqu'un diagramme de neuf classes sans arête
est une liste.

La plupart du temps, le graphe de types détient déjà la réponse. Une feuille
implémente l'interface du composant : le composite auquel elle appartient est donc
déductible sans que rien ne soit annoté. Ce n'est que là où un type participe à
plusieurs occurrences, ou là où la hiérarchie n'exprime pas le lien, que le graphe
est insuffisant.

Une clé sous forme de chaîne nommant l'occurrence a été proposée et rejetée :
c'est une valeur magique, non vérifiée par le compilateur, qui se désynchronise au
premier renommage.

## Décision

Un rôle peut déclarer des liens facultatifs vers d'autres rôles de son pattern,
chacun porté par un `Type`.

## Justification

Un `Type` est vérifié, suivi par les refactorisations et navigable, ce qu'une clé
nommant une occurrence n'est pas. Il nomme en outre quelque chose qui existe déjà
plutôt qu'il n'invente un identifiant qu'il faudrait tenir cohérent à la main sur
chaque participant.

Facultatif est le bon défaut, parce que le graphe suffit d'ordinaire. Exiger un
lien sur chaque participant rendrait le cas courant verbeux pour servir
l'exception, et demanderait à un auteur de redire ce que sa propre déclaration de
type énonce déjà.

Déclarer les liens rôle par rôle plutôt que sur le pattern les garde porteurs de
sens. Un composant n'a pas de composant ; un lien déclaré une fois pour tout le
pattern permettrait de l'écrire, et une forme qui autorise l'absurde y invite.

Les liens sont ce qui fait passer le modèle d'un ensemble d'étiquettes à quelque
chose doté d'arêtes, ce dont ont besoin à la fois les règles qui exigent de la
structure et les diagrammes. C'est la raison de les porter, et la raison pour
laquelle ils valent la surface supplémentaire sur les rôles qui en ont.

## Alternatives envisagées

### Nommer chaque occurrence par une clé sous forme de chaîne

Envisagé parce que c'est le plus petit ajout possible et que cela regroupe les
participants sans référence à aucun type.

Rejeté parce que c'est une valeur magique : rien ne la vérifie, une faute de
frappe scinde une occurrence en deux, et un renommage la laisse pointer vers un
nom qui n'existe plus.

### Exiger le lien sur chaque rôle

Envisagé parce que cela rendrait chaque occurrence explicite et ne laisserait rien
à l'inférence.

Rejeté parce que cela duplique ce que dit la hiérarchie de types dans le cas
ordinaire, et qu'une annotation qui redit la déclaration est la défaillance que ce
dépôt supprime ailleurs.

### Déclarer les liens une fois sur la base de rôle du pattern

Envisagé parce qu'il y a moins à générer et que cela place les liens en un seul
endroit.

Rejeté parce que cela fait accepter à chaque rôle tous les liens, y compris ceux
qui n'ont aucun sens, et qu'une forme capable d'exprimer l'absurde finira par
servir à l'exprimer.

## Conséquences

### Positives

* Les occurrences d'un même pattern peuvent être distinguées : les règles
  structurelles et les diagrammes deviennent donc possibles.
* Rien n'est écrit que le compilateur ne vérifie pas.
* Le cas ordinaire reste sans cérémonie.

### Négatives

* Un lien est facultatif : un consommateur ne peut donc pas compter sur sa
  présence et doit se rabattre sur le graphe de types.
* Quels rôles portent quels liens est une décision éditoriale propre à chaque
  pattern, et une omission n'est visible que de qui avait besoin du lien.

### Risques

* Un lien peut pointer vers un type qui ne tient pas ce rôle, puisque rien ne
  vérifie l'annotation de la cible. C'est une affirmation fausse et non une règle
  cassée, et seuls une revue ou un test de convention l'attraperaient.

## Références

* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) — la
  forme sur laquelle les liens sont déclarés.
* [ADR-0010](0010-annotate-the-declaration-that-introduces-a-role.md) — l'autre
  moitié de ce qui empêche les annotations de redire le code.
