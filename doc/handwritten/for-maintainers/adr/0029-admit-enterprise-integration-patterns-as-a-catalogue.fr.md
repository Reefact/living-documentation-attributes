# ADR-0029 | Admettre Enterprise Integration Patterns comme catalogue

🌍 🇬🇧 [English](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-09
**Accepté :** 2026-08-09
**Décideurs :** Reefact

## Contexte

Cinq œuvres sont cataloguées — *Design Patterns* (1994), *Analysis Patterns* (1997),
*Accounting Patterns* (2000), *Patterns of Enterprise Application Architecture* (2002) et
*Domain-Driven Design* (2003) — plus `Idioms` pour les patterns sans corpus propre. 147
patterns, 316 rôles.

Deux de ces catalogues comptent 48 entrées et sont les moins lus des cinq : l'auteur
d'*Analysis Patterns* dit lui-même de son livre qu'il a vieilli. L'objectif énoncé par le
mainteneur est désormais les patterns effectivement employés, et non davantage de patterns.

*Enterprise Integration Patterns* — Hohpe et Woolf, Addison-Wesley, 2003 — porte **65
patterns**, et ses auteurs maintiennent l'index canonique sur
`enterpriseintegrationpatterns.com`, avec pour chacun son nom et la question qu'il résout.
La liste a été lue là, non reconstruite.

Ce que sont ces 65, comptés :

* environ **cinquante sont des composants** : une classe est un Content-Based Router, un
  Splitter, un Aggregator, un Resequencer, un Content Enricher, un Idempotent Receiver, un
  Service Activator ;
* **cinq sont des propriétés sur un message** — Correlation Identifier, Return Address,
  Message Expiration, Format Indicator, Message History ;
* **six sont des canaux** — Message Channel et les cinq espèces, dont Dead Letter Channel
  et Invalid Message Channel. Un canal est souvent un nom configuré plutôt qu'un type ;
* **quatre sont des styles d'intégration** — File Transfer, Shared Database, Remote
  Procedure Invocation, Messaging — choisis pour une intégration plutôt que détenus par un
  participant ;
* **Guaranteed Delivery** est une propriété du transport, et **Request-Reply** est une
  interaction sur deux canaux plutôt qu'un participant dans l'un.

Trois de ses noms entrent en collision avec des patterns catalogués : Messaging Gateway
avec `EnterpriseApplicationArchitecture/Gateway`, Messaging Mapper avec `Mapper`, et Smart
Proxy avec `GangOfFour/Proxy`. Depuis
l'[ADR-0027](0027-ship-one-independent-package-per-catalogued-work.fr.md) chaque catalogue
est livré comme son propre package et aucune relation n'en traverse un : une collision
entre deux catalogues ne demande donc aucun arbitrage.

**Pipes and Filters** et **Message Broker** viennent de *Pattern-Oriented Software
Architecture* (Buschmann et al., 1996). *Enterprise Integration Patterns* présente chacun
en entier — son propre énoncé de problème, sa propre discussion, adapté à la messagerie —
et en crédite POSA.

L'[ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) écarte du catalogue ce qui ne
peut être attaché à un type, un membre ou une assembly.
L'[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) fournit le
critère selon lequel un pattern doit licencier des assertions sur lesquelles quelque chose
peut porter. L'[ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.fr.md)
détient un pattern dans chaque catalogue dont l'œuvre le présente comme le sien, et non là
où une œuvre ne fait que citer celui d'une autre.

Rien n'est publié.

## Décision

*Enterprise Integration Patterns* est admis comme le catalogue `EnterpriseIntegration`,
détenant l'ensemble de ses soixante-cinq patterns.

## Justification

L'œuvre satisfait les trois critères que le catalogue applique déjà, et elle satisfait le
deuxième mieux qu'aucune œuvre admise jusqu'ici. Ses patterns sont des **composants** : une
classe *est* un routeur, un traducteur, un agrégateur — rien à approximer, aucun type
marqueur à inventer. Là où *Analysis Patterns* demande d'avoir construit un modèle avant
qu'une annotation signifie quelque chose, celle-ci demande seulement d'avoir écrit la classe
que le pattern nomme.

Les assertions sont du genre vérifiable, ce que l'ADR-0007 exige d'une admission et pas
seulement d'une comparaison. Un Idempotent Receiver affirme que le même message livré deux
fois ne produit qu'un effet. Un Splitter affirme qu'un message entrant en produit plusieurs.
Un Aggregator affirme une condition de complétude. Un Dead Letter Channel affirme qu'aucun
message n'est perdu en silence. Ce sont des choses contre lesquelles une règle s'écrit, et
c'est ce qui sépare cette œuvre d'un ensemble de styles d'architecture.

Cinq de ses patterns sont des **propriétés sur un message**, et cela compte au-delà de leur
nombre : ce vocabulaire gère les rôles de membre et presque rien en lui ne les exerce. Un
identifiant de corrélation annoté sur la propriété qui le porte est l'usage le plus net d'un
rôle de membre dans tout le catalogue.

**Les canaux sont admis plutôt que retenus.** Un canal est souvent un nom de file configuré
et pas un type, ce qui plaide pour l'exclusion sous l'ADR-0011 — mais l'argument ne tient
pas à l'examen. Là où une base de code a une abstraction typée par canal, ce qui est courant
en .NET, le rôle s'attache ; là où elle n'en a pas, le pattern n'est simplement pas annoté,
ce qui est la condition ordinaire de tout rôle et non un défaut de l'entrée. Les retenir
laisserait le vocabulaire de l'œuvre incomplet, puisque les patterns de routage et de
terminaison sont définis en termes de canaux — et un lecteur qui ne trouve pas Dead Letter
Channel ne peut pas distinguer une décision d'un oubli.

**Pipes and Filters et Message Broker sont détenus ici malgré leur origine POSA**, et le
fondement est le test de l'ADR-0028 lui-même plutôt que la cohérence de l'ensemble. Ce test
est de savoir si l'œuvre présente le pattern comme le sien — le nomme, le décrit, lui donne
une place dans son propre langage de patterns — et *Enterprise Integration Patterns* fait
les trois, retravaillés pour la messagerie. Créditer une source antérieure relève de
l'honnêteté savante, non de la mention de passage que l'ADR-0028 exclut. La cohérence serait
l'argument le plus faible et la plus mauvaise règle : elle admettrait n'importe quel pattern
que n'importe quelle œuvre aurait trouvé commode de décrire.

Le nom laisse tomber « Patterns » comme le fait `EnterpriseApplicationArchitecture`.
`Messaging` a été envisagé : plus court, exactement le sujet de l'ensemble, et le mot que les
auteurs emploient eux-mêmes ; il a été écarté parce que ces catalogues portent le nom d'une
œuvre, et qu'un lecteur qui tient le livre cherche le livre.

## Alternatives envisagées

### N'admettre que les cinquante composants

Envisagée parce que c'est la moitié indiscutable : chacun est une classe, chacun licencie
une assertion, et aucun ne demande de jugement.

Rejetée parce que le reste du catalogue se définit par ce qu'elle laisse dehors. Les
patterns de routage routent entre des canaux et ceux de terminaison y consomment : un
catalogue sans canaux décrit la moitié d'un mécanisme. Et une absence sans trace se lit
comme un oubli, ce que `catalog/README.md` existe pour empêcher.

### Nommer le catalogue `Messaging`

Envisagée parce que c'est plus court, précisément ce dont il s'agit, le mot des auteurs sur
leur propre site, et que cela éviterait deux catalogues commençant tous deux par
« Enterprise » — dans le namespace, dans l'identifiant du package et dans le `using` de
chaque consommateur.

Rejetée parce qu'ici un catalogue porte le nom de l'œuvre qui le détient, non celui de son
sujet. `GangOfFour` est les auteurs et `EnterpriseApplicationArchitecture` est le titre ;
nommer un catalogue par son thème rendrait la convention imprévisible pour le sixième.

### Laisser Pipes and Filters et Message Broker à POSA

Envisagée parce que l'ADR-0028 dit paternité et non mention, et que POSA les a nommés sept
ans plus tôt.

Rejetée parce que le test est la présentation et non la priorité. Les deux sont décrits en
entier ici et tiennent une place dans le langage de cette œuvre. Si POSA est catalogué plus
tard, il détient ses propres entrées pour eux, et aucun des deux catalogues ne référence
l'autre — l'arrangement que l'ADR-0027 rend peu coûteux.

### N'admettre rien, et approfondir l'existant

Envisagée sérieusement, et c'est la plus forte des quatre. Le catalogue porte déjà 51
entrées de *Patterns of Enterprise Application Architecture*, dont Table Module, Transform
View, Two Step View et Client Session State sont aussi rarement nommés aujourd'hui que
n'importe quoi dans *Analysis Patterns*. Rien ne lit encore les annotations — ni analyzer,
ni moteur de règles — donc un consommateur doit construire le bénéfice avant d'en avoir un,
et l'élargissement ne répond pas à cela.

Rejetée parce que ce n'est pas un élargissement dans la même direction. Ces patterns sont
nommés quotidiennement dans les bases de code de messagerie .NET, ce que Table Module n'est
pas : les admettre *est* le mouvement vers ce qui est employé. Et cela n'empêche pas le
lecteur : ce travail est indépendant du nombre de catalogues.

## Conséquences

### Positives

* Le premier catalogue dont les patterns sont massivement des composants : une annotation ne
  coûte au lecteur que l'attribut.
* Les rôles de membre sont enfin exercés, par cinq patterns qui sont des propriétés sur un
  message.
* Un sixième package, indépendant des autres, sans aucun arbitrage à faire sur les trois
  noms qu'il partage avec des entrées existantes.

### Négatives

* Soixante-cinq entrées et soixante-cinq exemples : la plus grosse admission jamais faite
  ici — plus grosse qu'*Analysis Patterns* sur quatre chapitres.
* Trois homonymes existeront entre packages sans que rien ne dise qu'ils sont sans rapport.
  Un consommateur qui installe celui-ci et le catalogue d'entreprise voit deux Gateway et
  doit savoir lequel est lequel.
* Les canaux seront inannotables dans certaines bases de code : une part du catalogue
  atteint donc moins de consommateurs que le reste.

### Risques

* Le test de présentation admet un pattern qu'une autre œuvre a nommé la première. Appliqué
  mollement, il laisserait n'importe quelle œuvre revendiquer tout ce qu'elle décrit bien, et
  le garde-fou est que l'œuvre doit lui donner une place dans son propre langage — ce qui est
  un jugement à chaque fois.
* Si POSA est catalogué, le recouvrement sur Pipes and Filters et Message Broker est visible
  dans deux packages et énoncé dans aucun : c'est le coût assumé de l'indépendance, et cela
  ressemblera quand même à un défaut à qui le rencontre pour la première fois.
* Cette œuvre a une traîne — Control Bus, Detour, Wire Tap, Channel Purger, Test Message —
  bien moins employée que son cœur de routage. L'inégalité que le mainteneur cherche à fuir
  entre catalogues pourrait réapparaître à l'intérieur de celui-ci.

## Actions de suivi

* Ajouter `EnterpriseIntegration` à l'énumération du schéma et à la table de libellés du
  générateur, sans quoi aucune entrée ne valide.
* Cataloguer dans l'ordre du livre — patterns de base, canaux, construction, routage,
  transformation, terminaisons, administration — afin qu'un lecteur puisse suivre le livre
  ouvert.
* Consigner dans `catalog/README.md` de quoi dépend l'annotabilité d'un canal, et les trois
  homonymes avec une phrase disant pourquoi ils sont sans rapport, puisque rien dans les
  packages ne le dit.
* Trancher si la traîne d'administration est cataloguée avec le reste ou retenue faute
  d'usage.

## Références

* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) — le critère
  selon lequel un pattern doit licencier des assertions, appliqué ici à savoir si une œuvre a
  sa place.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) — ce contre quoi les canaux et
  les styles d'intégration ont été pesés.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md) — pourquoi
  c'est un catalogue et non une étagère d'idiomes.
* [ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.fr.md) — la dernière fois
  qu'une œuvre a été admise, et les termes qu'elle a posés.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.fr.md) — pourquoi les
  trois noms en collision ne demandent aucun arbitrage, et pourquoi un futur catalogue POSA
  n'en demanderait pas davantage.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.fr.md) — le test
  de présentation qui maintient Pipes and Filters et Message Broker ici.
* Hohpe et Woolf, *Enterprise Integration Patterns*, Addison-Wesley, 2003, et l'index des
  auteurs sur `enterpriseintegrationpatterns.com/patterns/messaging/`.
