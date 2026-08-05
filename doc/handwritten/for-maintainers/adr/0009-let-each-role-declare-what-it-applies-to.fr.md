# ADR-0009 | Laisser chaque rôle déclarer ce à quoi il peut s'appliquer

🌍 🇬🇧 [English](0009-let-each-role-declare-what-it-applies-to.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Les rôles diffèrent par ce qui peut légitimement les tenir. Un composant est une
abstraction, donc une interface ou une classe ; une feuille n'a pas d'enfants et
peut être une structure d'enregistrement ; un composite porte des enfants et ne le
peut pas. Les opérations d'acceptation et de visite sont des méthodes. Un contexte
borné est un assembly.

Tant que la bibliothèque portait un attribut par pattern, l'applicabilité devait
être déclarée une fois pour tout le pattern, ce qui revenait à prendre l'union de
ce que chacun de ses rôles pouvait accepter — rien n'était donc réellement
contraint.

Les premières entrées écrites à la main ont montré ce qui arrive quand cela se
décide fichier par fichier plutôt que catalogue par catalogue : quatre attributs
ne portaient aucune déclaration et acceptaient donc toutes les cibles, et
`AttributeTargets.Struct` était absent de toutes les entrées. Le second point n'a
rien de cosmétique — `[ValueObject]` ne pouvait pas s'appliquer à un `readonly
record struct`, qui est l'objet-valeur le plus idiomatique du C# moderne, et le
pattern qu'on annotera le plus probablement en premier.

La multiplicité et l'héritage varient selon le pattern plutôt que selon une
convention. Un type peut tenir un rôle dans deux occurrences d'un pattern : un
rôle porté par un type est donc répétable ; une méthode tient un rôle de membre
une seule fois. Un sous-type d'une entité est une entité : le marqueur est donc
hérité ; un sous-type d'un composant n'est pas nécessairement une feuille : un
rôle du Gang of Four ne l'est donc pas.

## Décision

Chaque rôle déclare ses propres cibles, sa multiplicité et son héritage, pris dans
le catalogue plutôt que dans une convention.

## Justification

Cela transforme une annotation absurde en erreur de compilation. Un composite sur
une structure, une opération de visite sur une classe, un contexte borné sur un
type : chacun est désormais refusé là où une déclaration partagée devait tous les
permettre. Le modèle gagne en capacité d'affirmation, qui est le critère à l'aune
duquel tout le catalogue est jugé.

L'héritage est une propriété du pattern, non un style maison. Qu'un sous-type
tienne encore un rôle est un énoncé sur ce que le pattern signifie : le décider
pattern par pattern dans le catalogue, c'est consigner un fait ; appliquer une
valeur unique à toute la bibliothèque serait affirmer quelque chose de faux sur la
moitié d'entre elle.

Le déclarer comme donnée plutôt que fichier par fichier supprime la défaillance
qui a produit les premières entrées. Un ensemble de cibles manquant est une erreur
de schéma plutôt qu'un défaut silencieux valant *tout*, et le choix se fait une
fois par rôle, par qui écrit le pattern, et non une fois par fichier par qui se
trouve l'éditer.

`Struct` et `Assembly` figurent dans le vocabulaire des cibles parce que le modèle
exclurait sinon des participants légitimes : les objets-valeurs et les événements
de domaine sont couramment des structures d'enregistrement, et les patterns
stratégiques qualifient un assembly plutôt qu'un type quelconque en son sein.

## Alternatives envisagées

### Appliquer un ensemble de cibles et une règle d'héritage uniques à toute la bibliothèque

Envisagé parce que c'est une décision au lieu de plusieurs centaines, et qu'elle
ne peut pas être oubliée.

Rejeté parce que c'est faux dans les deux sens : cela interdirait l'objet-valeur
en structure d'enregistrement ou permettrait le composite en structure, et cela
affirmerait une règle d'héritage juste pour les marqueurs de domaine et fausse
pour les rôles structurels.

### Omettre `AttributeUsage` et accepter le défaut

Envisagé parce que le défaut est permissif et ne rejette jamais une annotation
légitime.

Rejeté parce que le défaut accepte tout, y compris les paramètres, les champs et
les assemblys : un attribut sans déclaration n'affirme donc rien sur l'endroit où
il a sa place. Quatre des premières entrées faisaient exactement cela, sans
intention.

### Décider des cibles dans le générateur d'après la nature du rôle

Envisagé parce que rôles portés par des membres et rôles portés par des types
diffèrent de façon prévisible.

Rejeté parce que les différences qui comptent ne sont pas prévisibles depuis la
nature : qu'une feuille puisse être une structure et un composite non est un fait
sur le pattern, et aucune règle portant sur les natures de rôles ne le retrouve.

## Conséquences

### Positives

* Une annotation fautive ne compile pas au lieu d'être enregistrée comme donnée.
* Les objets-valeurs et les événements de domaine peuvent être des structures
  d'enregistrement.
* Les patterns stratégiques peuvent annoter un assembly.
* La multiplicité et l'héritage disent quelque chose de vrai sur chaque pattern.

### Négatives

* Trois décisions éditoriales de plus par rôle, soit trois occasions de plus de
  consigner quelque chose de faux.
* Un ensemble de cibles trop étroit rejette une annotation légitime, et l'auteur
  le rencontre sous forme d'erreur de compilation sans autre remède évident que de
  modifier le catalogue.

### Risques

* Élargir un ensemble de cibles plus tard est additif, mais le rétrécir casse les
  consommateurs qui avaient annoté légitimement sous l'ancien ensemble. Le choix
  initial est donc de fait définitif, et pécher par étroitesse est l'erreur la plus
  coûteuse.

## Références

* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) — la
  forme qui rend possible la déclaration rôle par rôle.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  là où les déclarations sont rédigées.
