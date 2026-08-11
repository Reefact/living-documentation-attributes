# ADR-0039 | Nommer l'attribut de base d'après ce qu'il marque

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0039-name-the-attribute-base-after-what-it-marks.md)

**Statut :** Accepté
**Proposé :** 2026-08-11
**Accepté :** 2026-08-11
**Décideurs :** Reefact

## Contexte

[ADR-0038](0038-name-the-packages-after-the-catalogue-rather-than-the-vendor.md) a renommé les paquets,
les namespaces, les projets, la solution et le dépôt en `DesignPatternCatalog`, au motif qu'un nom doit
dire ce que les paquets portent plutôt que qui les publie. Sa phrase de décision énumère ces cinq
choses et **ne va pas jusqu'aux noms de types** : le renommage a donc laissé tous les types en place.

Un type est concerné par cette omission. `LivingDocumentationAttribute` est le **seul type public de
`DesignPatternCatalog.Core`**, et tout attribut de tout catalogue en descend — par la base abstraite
`Role` propre au pattern lorsqu'il a plusieurs rôles, directement lorsqu'il n'en a qu'un.
[ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) en fait un pur marqueur qui ne déclare aucun
membre, et les quatre règles de relecture du catalogue y sont documentées afin qu'elles voyagent avec
le paquet.

**C'est le seul identifiant de ce dépôt qu'un consommateur tape à la main.** Tout le reste lui est
proposé par la complétion dès qu'un paquet est installé ; ce type est celui auquel un lecteur remonte
pour trouver *toutes* les annotations quel que soit le mélange de catalogues installé, et c'est toute la
raison d'être de `Core` comme paquet distinct.

Un nom de type se lit **non qualifié**. Un `using` amène le namespace, et ce qui apparaît sur le site
d'appel est le nom nu — `typeof(LivingDocumentationAttribute)` dans une base de code où rien d'autre de
cette bibliothèque n'est sous les yeux.

Après ADR-0038, la locution *living documentation* survit à exactement deux endroits : ce nom de type,
et la section d'ouverture du README qui argumente la raison d'être de la bibliothèque.

Le nom apparaît 355 fois. Une occurrence est la déclaration écrite à la main dans `Core` ; une ligne par
pattern est générée ; cinq fichiers écrits à la main y font référence — le lecteur de l'exemple dans
deux fichiers, et deux fichiers de tests ; six records le nomment —
[ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md),
[ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md) et
[ADR-0034](0034-let-a-specialisation-name-the-role-it-narrows.md), chacun avec sa traduction ; et le
README le nomme une fois.

Le dépôt accepte déjà un bégaiement là où ses propres règles en produisent un : le rôle auquel un
pattern à plusieurs rôles donne son propre nom s'écrit `[MonitorObject.MonitorObject]` et
`[Interceptor.Interceptor]`.

Rien n'est publié. Le nom d'un type public fait partie de ce contre quoi un consommateur compile, donc
les deux contrats d'[ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md) le
portent tous les deux : le renommer après une release est un changement cassant, qui exige une version
majeure et une doublure pour l'ancien nom.

## Décision

L'attribut de base est renommé de `LivingDocumentationAttribute` en `DesignPatternAttribute`.

## Justification

**C'est l'argument d'ADR-0038 appliqué au seul nom qu'un consommateur écrit.** Ce record a décidé que la
bibliothèque devait être nommée d'après ce qu'elle porte ; après lui, l'identifiant le plus visible de
tout le dépôt était le seul encore nommé d'après ce à quoi la bibliothèque *sert*. Une règle qui
s'arrête à la frontière du paquet et s'inverse à l'intérieur n'est pas une règle, et le type devant
lequel elle s'arrête est précisément celui qu'un lecteur rencontre en premier.

**Que le nom se lise non qualifié est ce qui décide lequel choisir.** `DesignPatternAttribute` se tient
seul sur un site d'appel dans la base de code d'autrui : il dit que ce qu'il marque participe d'un
design pattern. `PatternAttribute` non — lu nu, *pattern* désigne aussi bien du filtrage ou une
expression régulière, et le namespace qui le désambiguïserait n'est pas à l'écran. Le mot en plus n'est
pas une redondance : c'est la seule chose qui porte le sens là où le nom est réellement lu.

**Le bégaiement est confiné là où personne ne regarde.** `DesignPatternCatalog.DesignPatternAttribute`
se lit mal, et c'est la forme pleinement qualifiée, qui apparaît dans un fichier de baseline et
pratiquement nulle part ailleurs. Le dépôt a déjà accepté pire à un endroit qui, lui, *est* lu — le rôle
propre d'un pattern bégaie dans l'annotation même — parce que la règle qui le produit valait plus que la
lecture. Il en va de même ici.

**ADR-0004 n'est pas touché.** Ce qui change est le nom ; le type reste un pur marqueur sans aucun
membre, et les règles de lecture y restent documentées, de sorte qu'un consommateur qui atteint le type
atteint encore les règles.

**Le coût est quasi nul maintenant et permanent plus tard.** La quasi-totalité des occurrences est
générée : le changement est une constante dans le générateur et une régénération ; les références
écrites à la main sont cinq fichiers et une déclaration, et une oubliée ne compile pas. Après la
première release, le même renommage est un changement cassant du contrat de compilation — la seule
chose que le versionnement rend coûteuse plutôt que simplement visible.

## Alternatives envisagées

### Garder `LivingDocumentationAttribute`

Envisagée, et c'est ce qu'ADR-0038 tel qu'accepté laissait en place. C'est aussi le dernier endroit du
code où la finalité de la bibliothèque est énoncée : *living documentation* est un terme établi, et le
type dont descend toute annotation est un endroit défendable pour dire à quoi elles servent toutes. Lu
avec bienveillance, `DesignPatternCatalog.LivingDocumentationAttribute` dit que le catalogue tient des
design patterns et qu'en annoter un est un acte de documentation.

Rejetée parce qu'elle fait de l'identifiant le plus visible de la bibliothèque la seule exception à la
règle de nommage qui vient d'être adoptée, et parce que la finalité est argumentée longuement dans le
README et dans cette base, là où le raisonnement appartient. Un nom de type a une seule tâche sur un
site d'appel, et ce n'est pas de porter un motif.

### `PatternAttribute`

Envisagée parce qu'elle est plus courte et que le namespace fournit déjà le mot *design*, de sorte que
le nom qualifié se lirait `DesignPatternCatalog.PatternAttribute` sans bégayer.

Rejetée sur le fait qui décide ce record : le nom se lit non qualifié, et `Pattern` seul y est ambigu.
Optimiser la forme que personne ne lit au détriment de celle que tout le monde lit est le mauvais
arbitrage.

### `CatalogAttribute` ou `DesignPatternCatalogAttribute`

Envisagées par symétrie avec le namespace racine.

Rejetées parce qu'elles nomment la collection plutôt que ce qu'une annotation affirme. Une annotation
dit qu'une déclaration participe d'un design pattern ; elle ne dit pas que la déclaration se trouve
dans un catalogue.

### Fondre le renommage dans ADR-0038 plutôt qu'écrire un record

Envisagée parce que les deux changements sont une seule intention et n'atterrissent à des moments
distincts du point de vue de personne, sinon de ce dépôt.

Rejetée parce qu'ADR-0038 est accepté et que sa phrase de décision énumère ce qu'il renomme. Élargir
sur place la portée d'un record accepté est la seule chose que la base interdise formellement, et la
distinction n'est pas une formalité : un identifiant de paquet est ce qu'un consommateur *installe*, un
nom de type public est ce contre quoi il *compile*, et le second est le contrat qu'ADR-0021 traite comme
cassant.

## Conséquences

### Positives

* Une seule règle de nommage, de l'identifiant de paquet jusqu'au type qu'un consommateur écrit à la
  main.
* L'identifiant se tient seul là où il est réellement lu — non qualifié, sur un site d'appel, sans
  aucun autre nom de cette bibliothèque sous les yeux.
* Gratuit aujourd'hui. Après la première release, le même changement casse le contrat de compilation.

### Négatives

* **La locution *living documentation* quitte entièrement le code.** Elle survit dans l'argument
  d'ouverture du README et dans cette base : un lecteur des seules sources ne rencontre donc plus l'idée
  qui motive la bibliothèque.
* Le nom pleinement qualifié bégaie, et c'est la forme qu'un fichier de baseline consigne.
* Trois records — ADR-0004, ADR-0019 et ADR-0034, chacun avec sa traduction — nomment l'ancien type.
  Leurs occurrences sont illustratives, donc mises à jour selon la règle qu'énonce l'action de suivi
  d'ADR-0038 ; mais un lecteur disposant d'une copie plus ancienne rencontre deux noms pour un type.
* ADR-0038 reste le seul record à nommer l'ancienne racine de *paquet* alors qu'aucun record ne nomme
  l'ancienne racine de *type* : les deux moitiés d'un même renommage ne se cherchent pas de la même
  façon.

### Risques

* Sur les 355 occurrences, celles que le compilateur ne peut pas vérifier sont la prose : le README, les
  records, et la documentation XML que les paquets expédient. Une référence oubliée là est un document
  qui nomme un type qui n'existe plus, et rien n'échoue.

## Actions de suivi

* Changer la constante dans le générateur et régénérer ; un catalogue inchangé doit toujours laisser
  l'arbre de travail propre.
* Renommer la déclaration et son fichier dans `Core`, et mettre à jour le lecteur de l'exemple et les
  deux fichiers de tests.
* Mettre à jour les lignes concernées des douze baselines `PublicAPI.Unshipped.txt`.
* Mettre à jour le README, et les occurrences illustratives d'ADR-0004, ADR-0019 et ADR-0034 avec leurs
  traductions.

## Références

* [ADR-0038](0038-name-the-packages-after-the-catalogue-rather-than-the-vendor.md) — l'argument que ce
  record prolonge, et la règle disant quels records sont mis à jour et lesquels gardent l'ancien nom.
* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — ce que le type est, et ce que ce record ne
  change pas à son sujet.
* [ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md) — pourquoi le nom d'un
  type public est coûteux à changer après une release et bon marché avant.
* [ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md) et
  [ADR-0034](0034-let-a-specialisation-name-the-role-it-narrows.md) — les deux autres records qui
  nomment le type, tous deux à titre d'illustration.
