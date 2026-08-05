# ADR-0003 | Donner à chaque rôle son propre attribut, imbriqué dans le pattern auquel il appartient

🌍 🇬🇧 [English](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Un participant tient un rôle au sein d'un pattern : une classe est la *Feuille*
d'un *Composite*, une interface est l'*Élément* d'un *Visiteur*. L'annotation doit
dire les deux.

La bibliothèque a d'abord porté un attribut par pattern, le rôle étant passé en
argument d'énumération. Sur une classe qui est une feuille, cela se lit
`[CompositePattern(CompositeParticipant.Leaf)]`, et un attribut se lit *cette
classe est un X* — il dit donc que la classe est un Composite, soit le contraire
de ce qu'est une feuille. Le suffixe `Pattern` est ce qui maintenait la formule
seulement ambiguë au lieu de franchement fausse.

Les noms de rôles entrent massivement en collision d'un pattern à l'autre.
*Component*, *Context*, *Subject*, *Product*, *Handler* et *Target* sont chacun
employés par quatre ou cinq patterns du seul Gang of Four : un attribut plat par
rôle n'est donc pas disponible.

Les rôles ne sont pas tous tenus par des types. Les opérations de visite et
d'acceptation d'un Visiteur, la méthode fabrique, la méthode patron et ses
opérations primitives sont des membres, et les deux attributs de méthode écrits à
la main qui subsistaient dans la bibliothèque montraient que le modèle en avait
besoin.

Le catalogue est appelé à croître d'un ordre de grandeur. Les membres d'une
énumération portent des ordinaux jusque dans les métadonnées de chaque assembly
consommateur : insérer un rôle dans une énumération existante réaffecte donc
silencieusement le sens de code déjà compilé ailleurs — et un catalogue qui
grandit insère des rôles en permanence.

## Décision

Chaque rôle est son propre attribut scellé, imbriqué dans un conteneur statique
portant le nom du pattern, exception faite d'un pattern à rôle unique, qui est un
attribut plat portant le nom du pattern.

## Justification

Cela restaure la seule lecture qu'un attribut autorise. `[Composite.Leaf]` dit
*cette classe est un Composite.Leaf*, et `[ValueObject]` dit *cette classe est un
objet-valeur* ; les deux sont de la forme *est un*, là où la version paramétrée
devait inventer une seconde grammaire que la syntaxe ne porte pas.

L'imbrication est ce qui rend possible un attribut par rôle.
`Composite.Component` et `Decorator.Component` sont des types distincts sans
espace de noms dédié, sans alias, et sans les collisions qui ont coulé la forme
plate — le conteneur fait office de qualifiant.

Elle permet à chaque rôle d'énoncer sa propre applicabilité plutôt que l'union de
toutes. Un attribut paramétré unique doit accepter toutes les cibles dont l'un
quelconque de ses rôles a besoin ; des attributs séparés laissent une feuille
accepter une structure là où un composite ne l'accepte pas, ce qui transforme une
annotation absurde en erreur de compilation.

Elle supprime entièrement le danger des ordinaux au lieu de le gérer. Sans
énumération, il n'y a rien à renuméroter : ajouter un rôle ajoute un type, ce qui
est additif pour les assemblys déjà compilés. Sur un catalogue dont la raison
d'être est de continuer à croître, c'est la différence entre une discipline de
versionnage et une non-question.

Un rôle porté par un membre n'exige aucun concept nouveau sous cette forme — c'est
un rôle comme les autres, distingué seulement par ce à quoi il peut s'appliquer.
L'énumération parallèle qu'imposait la forme paramétrée disparaît.

Un pattern à rôle unique reste plat parce qu'il n'y a rien à choisir.
L'imbrication existe pour porter un choix, et `[Entity]` se lit comme le langage
omniprésent là où `[Entity.Entity]` se lit comme une machine.

## Alternatives envisagées

### Conserver un attribut par pattern avec une énumération de rôles

Envisagé parce que c'est la forme conventionnelle, qu'elle est compacte et que
c'est ce que la bibliothèque avait déjà.

Rejeté sur la lecture — `[Composite(Leaf)]` énonce le contraire de ce qu'est une
feuille — et sur trois conséquences de l'énumération : des cibles partagées entre
rôles, des ordinaux qui font d'un catalogue en croissance un tapis roulant de
ruptures de compatibilité, et une énumération parallèle nécessaire aux rôles
portés par des membres.

### Conserver l'énumération mais restaurer le suffixe `Pattern`

Envisagé parce que `[CompositePattern(Leaf)]` est sans ambiguïté, et parce que
c'est ce que faisait la bibliothèque auparavant.

Rejeté parce que cela ne corrige que l'ambiguïté. Le suffixe se lit comme une
référence de manuel plutôt que comme le langage du domaine — `[EntityPattern]`
quand personne ne dit « le pattern Entity » — et il laisse intacts les cibles, les
ordinaux et les rôles portés par des membres.

### Un attribut plat par rôle, sans imbrication

Envisagé parce que `[Leaf]` et `[Component]` sont ce qui se lit le mieux.

Rejeté parce que les noms entrent en collision : *Component*, *Context*,
*Subject*, *Product* et *Handler* appartiennent chacun à plusieurs patterns, et la
bibliothèque devrait soit déformer les noms, soit les disperser entre des espaces
de noms.

### Un attribut générique unique prenant des chaînes

Envisagé parce qu'il passerait à l'échelle de n'importe quel catalogue sans aucun
type généré.

Rejeté parce qu'il abandonne la vérification à la compilation et la découverte
dans l'éditeur, qui sont les deux choses rendant le vocabulaire utilisable pendant
qu'on écrit du code, et parce qu'un moteur de règles a besoin d'un vocabulaire de
rôles fermé pour être fiable.

## Conséquences

### Positives

* Toute annotation se lit *ceci est un X*, que le pattern ait un rôle ou sept.
* Les noms de rôles peuvent se répéter d'un pattern à l'autre sans cérémonie.
* Chaque rôle contraint ce à quoi il peut s'appliquer, si bien qu'une annotation
  fautive ne compile pas.
* Ajouter un rôle à un pattern publié est additif.
* Les rôles portés par des membres n'exigent aucun concept séparé.

### Négatives

* Beaucoup plus de types : environ quatre par pattern au lieu d'un plus une
  énumération.
* Le filtrage par rôle se fait sur des types plutôt que par un `switch` sur une
  énumération, ce qui est un changement d'habitude.
* Un rôle qui porte le nom de son propre pattern se lit maladroitement —
  `Visitor.Visitor`, `Composite.Composite` — et c'est irréductible, puisque c'est
  le nom que le rôle porte réellement.

### Risques

* Le conteneur occupe son nom dans l'espace de noms : un consommateur qui possède
  un type homonyme devra qualifier. Inévitable sous toute forme qui met le nom du
  pattern dans la portée.

## Actions de suivi

* Réserver la forme plate aux patterns dont le rôle unique porte le nom du pattern
  lui-même, afin que le choix entre les deux formes reste déductible plutôt que
  déclaré.

## Références

* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — ce que les attributs
  générés ne portent pas.
* [ADR-0009](0009-let-each-role-declare-what-it-applies-to.md) — la déclaration de
  cibles que cette forme rend possible.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  la génération qui l'émet.
