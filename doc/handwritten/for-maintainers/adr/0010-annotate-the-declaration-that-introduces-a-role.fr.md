# ADR-0010 | Annoter la déclaration qui introduit un rôle

🌍 🇬🇧 [English](0010-annotate-the-declaration-that-introduces-a-role.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Un rôle est souvent introduit par une interface puis implémenté plusieurs fois.
L'opération d'acceptation d'un Visiteur est déclarée une fois sur l'interface
d'élément et implémentée par chaque élément concret ; l'opération d'exécution
d'une Commande est déclarée une fois et implémentée par chaque commande.

C# ne propage pas un attribut d'un membre d'interface vers les membres qui
l'implémentent, et `Inherited` gouverne les classes de base et non les interfaces.
Rien ne tranche donc la question pour l'auteur : annoter la déclaration, chaque
implémentation, ou les deux — tout compile et tout produit des données
différentes.

L'écriture de l'exemple du Visiteur a rendu le coût visible. L'opération
d'acceptation apparaissait trois fois dans l'inventaire — une pour l'interface,
une par implémentation — ce qui est un fait sur la manière dont l'exemple a été
écrit plutôt que sur le code décrit.

Quel que soit le choix de l'auteur, un consommateur qui compte les participants
obtient une réponse différente. Laissée non énoncée, une même base de code livre
des données différentes selon qui l'a annotée, et deux bases de code ne peuvent
pas être comparées.

## Décision

Un rôle est annoté sur la déclaration qui l'introduit, et non sur les déclarations
qui l'implémentent ou le redéfinissent.

## Justification

La déclaration est là où l'intention s'exprime. Un membre d'interface est l'énoncé
que l'opération existe et ce à quoi elle sert ; une implémentation est la façon
dont un type y répond. Le rôle du pattern appartient au premier, et le répéter sur
la seconde n'ajoute rien.

Cela empêche l'annotation de redire le code. Qu'une classe implémente une
interface est déjà dans le graphe de types : un consommateur qui veut les
implémentations peut y naviguer ; une annotation sur chacune est une seconde copie
d'un fait que le compilateur détient déjà — la redondance même que ce dépôt retire
des attributs eux-mêmes.

Une convention non énoncée rendrait les données incomparables. Compter est la
chose la plus simple que fasse un consommateur, et le compte n'a aucun sens si
toutes les bases de code n'ont pas été annotées de la même façon. Fixer la règle
est ce qui fait d'un inventaire une mesure plutôt que le reflet d'une habitude.

C'est le moins coûteux des deux choix cohérents. Annoter chaque implémentation
passe à l'échelle du nombre d'implémenteurs, doit être répété à chaque ajout, et
est silencieusement faux dès qu'on en oublie un — là où une annotation unique sur
la déclaration ne peut pas être appliquée à moitié.

La règle est portée par la documentation de chaque pattern : elle atteint donc un
auteur dans l'éditeur au moment où il annote, plutôt que dans un document qu'il
aurait fallu avoir lu.

## Alternatives envisagées

### Annoter chaque implémentation

Envisagé parce que cela rend chaque participant auto-descriptif : un lecteur qui
ouvre une classe voit son rôle sans suivre d'interface.

Rejeté parce que cela duplique un fait que le graphe de types détient déjà, passe
à l'échelle du nombre d'implémentations, et devient silencieusement incomplet dès
la première oubliée.

### Annoter les deux, et laisser les consommateurs dédupliquer

Envisagé parce que cela sert les deux lectures et ne demande à personne de
choisir.

Rejeté parce que dédupliquer suppose de savoir que deux annotations décrivent un
seul rôle, ce qui est exactement le jugement que la convention existe pour ne pas
déléguer à chaque consommateur.

### Laisser le choix à l'auteur

Envisagé parce que le modèle consigne ce que l'auteur veut dire, et que l'un ou
l'autre choix se défend dans une base de code donnée.

Rejeté parce que les données cessent d'être comparables d'une base de code à
l'autre, ce qui retire l'essentiel de l'intérêt d'avoir un vocabulaire partagé.

## Conséquences

### Positives

* Une base de code livre un inventaire, quel qu'en soit l'annotateur.
* L'annotation ne redit jamais ce que dit le graphe de types.
* Ajouter une implémentation n'exige aucune annotation.

### Négatives

* Un lecteur qui ouvre une implémentation ne voit aucun rôle et doit suivre
  l'interface pour le trouver.
* Rien n'impose la convention ; une base de code qui annote chaque implémentation
  produit des comptes gonflés sans qu'aucun signe n'indique d'anomalie.

### Risques

* Lorsqu'un rôle est introduit par une classe abstraite plutôt que par une
  interface, la frontière entre introduire et redéfinir est moins nette, et deux
  auteurs peuvent la tracer différemment.

## Actions de suivi

* Envisager une règle qui signale une annotation portée par un membre qui
  redéfinit ou implémente un membre annoté, puisque c'est mécaniquement
  détectable.

## Références

* [ADR-0008](0008-bind-participants-with-typed-links.md) — l'autre moitié de ce
  qui empêche les annotations de redire le code.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md) — là où la
  convention est démontrée.
