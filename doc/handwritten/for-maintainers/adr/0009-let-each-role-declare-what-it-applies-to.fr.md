# ADR-0009 | Laisser chaque rôle déclarer ce à quoi il peut s'appliquer

🌍 🇬🇧 [English](0009-let-each-role-declare-what-it-applies-to.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Les rôles diffèrent par ce qui peut légitimement les tenir. Un composant est une
abstraction, donc une interface ou une classe ; une feuille n'a pas d'enfants et
peut être un `record struct` ; un composite détient des enfants et ne le peut
pas. Les opérations *accept* et *visit* sont des méthodes. Un contexte borné est
une assembly.

Tant que la librairie portait un attribut par pattern, l'applicabilité devait
être déclarée une fois pour tout le pattern, ce qui revenait à prendre l'union de
ce que n'importe lequel de ses rôles pouvait accepter — donc à ne rien
contraindre réellement.

Les premières entrées écrites à la main ont montré ce qui arrive quand cela se
décide par fichier plutôt que par catalogue : quatre attributs ne portaient
aucune déclaration et acceptaient donc toutes les cibles, et
`AttributeTargets.Struct` était absent de toutes les entrées. Le second point
n'est pas cosmétique — `[ValueObject]` ne pouvait pas s'appliquer à un `readonly
record struct`, qui est le value object le plus idiomatique du C# moderne, et le
pattern le plus susceptible d'être annoté en premier.

Multiplicité et héritage varient selon le pattern plutôt que selon une
convention. Un type peut tenir un rôle dans deux occurrences d'un pattern : un
rôle de type est donc répétable ; une méthode tient un rôle de membre une seule
fois. Un sous-type d'une entité est une entité : le marqueur est donc hérité ; un
sous-type d'un composant n'est pas nécessairement une feuille : un rôle du Gang
of Four ne l'est donc pas.

## Décision

Chaque rôle déclare ses propres cibles, sa multiplicité et son héritage, pris
dans le catalogue plutôt que dans une convention.

## Justification

Cela transforme une annotation absurde en erreur de compilation. Un composite sur
une structure, une opération *visit* sur une classe, un contexte borné sur un
type : chacun est désormais refusé là où une déclaration partagée devait tous les
permettre. Le modèle gagne en capacité d'affirmer, ce qui est le critère auquel
tout le catalogue est jugé.

L'héritage est une propriété du pattern, pas un style maison. Qu'un sous-type
tienne encore un rôle est un énoncé sur ce que le pattern signifie : le décider
par pattern dans le catalogue, c'est enregistrer un fait ; appliquer une valeur
unique à toute la librairie serait affirmer quelque chose de faux sur la moitié
d'entre eux.

Le déclarer comme donnée plutôt que par fichier supprime la défaillance qui a
produit les premières entrées. Un ensemble de cibles manquant est une erreur de
schéma plutôt qu'un défaut silencieux à *tout*, et le choix est fait une fois par
rôle par qui écrit le pattern, pas une fois par fichier par qui se trouve
l'éditer.

`Struct` et `Assembly` figurent dans le vocabulaire de cibles parce que le modèle
exclurait sinon des participants légitimes : les value objects et les événements
de domaine sont couramment des `record struct`, et les patterns stratégiques
qualifient une assembly plutôt qu'un type qu'elle contient.

## Alternatives envisagées

### Appliquer un ensemble de cibles et une règle d'héritage uniques à toute la librairie

Envisagé parce que c'est une décision au lieu de plusieurs centaines, et qu'elle
ne peut pas être oubliée.

Rejeté parce que c'est faux dans les deux sens : cela interdirait soit un value
object en `record struct`, soit permettrait un composite en structure, et cela
affirmerait une règle d'héritage juste pour les marqueurs de domaine et fausse
pour les rôles structurels.

### Omettre `AttributeUsage` et accepter le défaut

Envisagé parce que le défaut est permissif et ne rejette jamais une annotation
légitime.

Rejeté parce que le défaut accepte tout, y compris les paramètres, les champs et
les assemblies : un attribut sans déclaration n'affirme donc rien sur là où il a
sa place. Quatre des premières entrées faisaient exactement cela, sans le
vouloir.

### Décider des cibles dans le générateur, selon la nature du rôle

Envisagé parce que rôles de membre et rôles de type diffèrent de façon
prévisible.

Rejeté parce que les différences qui comptent ne sont pas prévisibles depuis la
nature du rôle : qu'une feuille puisse être une structure et un composite non est
un fait sur le pattern, qu'aucune règle sur les natures de rôles ne restitue.

## Conséquences

### Positives

* Une annotation fausse ne compile pas au lieu d'être enregistrée comme donnée.
* Les value objects et les événements de domaine peuvent être des `record
  struct`.
* Les patterns stratégiques peuvent annoter une assembly.
* Multiplicité et héritage disent quelque chose de vrai sur chaque pattern.

### Négatives

* Trois décisions éditoriales de plus par rôle, donc trois occasions de plus
  d'enregistrer quelque chose de faux.
* Un ensemble de cibles trop étroit rejette une annotation légitime, et l'auteur
  la rencontre comme une erreur de compilation sans autre remède évident que de
  modifier le catalogue.

### Risques

* Élargir un ensemble de cibles plus tard est additif, mais le rétrécir casse les
  consommateurs qui avaient annoté légitimement sous l'ancien. Le choix initial
  est donc de fait définitif, et se tromper par étroitesse est l'erreur la plus
  coûteuse.

## Références

* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.fr.md) —
  la forme qui rend possible la déclaration par rôle.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.fr.md) —
  là où les déclarations sont écrites.
