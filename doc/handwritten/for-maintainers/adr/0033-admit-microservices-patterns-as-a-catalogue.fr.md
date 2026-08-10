# ADR-0033 | Admettre Microservices Patterns comme catalogue

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0033-admit-microservices-patterns-as-a-catalogue.md)

**Statut :** Accepté
**Proposé :** 2026-08-10
**Accepté :** 2026-08-10
**Décideurs :** Reefact

## Contexte

Sept œuvres sont cataloguées — *Design Patterns* (1994), *Analysis Patterns* (1997),
*Accounting Patterns* (2000), *Patterns of Enterprise Application Architecture* (2002),
*Domain-Driven Design* (2003), *Enterprise Integration Patterns* (2003) et *xUnit Test
Patterns* (2007) — plus `Idioms`. 274 patterns, 476 rôles. Six des sept sont complètes.

Toutes ont au moins dix-huit ans. Ce n'est pas un hasard de goût : un catalogue ancien est un
catalogue stabilisé, et c'est cette stabilité qui rend un vocabulaire digne d'être gravé dans
des attributs. Mais il en résulte que la bibliothèque ne dit rien de la façon dont la plupart
de ses lecteurs construisent des systèmes depuis dix ans, et
[l'ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) a énoncé l'objectif
qui gouverne ici — des patterns d'usage quotidien plutôt que plus de patterns. *Saga*, *CQRS*,
*Transactional outbox*, *Circuit breaker* et *API gateway* se disent en réunion d'équipe ;
rien dans ce vocabulaire ne sait les porter.

*Microservices Patterns* — Chris Richardson, Manning, 2018 — est l'œuvre, et
[microservices.io](https://microservices.io/patterns/index.html) est le même langage de
patterns, maintenu par son auteur, rangé par groupes et tenu à jour. L'index contient
**48 patterns** répartis en quatorze groupes : Architectural style (2), Service boundaries (4),
Refactoring to services (2), Service collaboration (8), Transactional messaging (3), Testing
(3), Deployment (5), Cross-cutting concerns (3), Communication styles (4), External API (2),
Service discovery (3), Reliability (1), Observability (5), Security (1) et UI design (2).

Quatre faits sur cette liste comptent ici.

**Environ la moitié est de l'infrastructure qu'aucune déclaration ne porte.** Sidecar, Service
mesh, Log aggregation, Distributed tracing, Exception tracking, Multiple service instances per
host, Serverless deployment — ce sont des formes d'une topologie de déploiement, pas des
participants qu'un type C# joue, ce qui est le terrain de
[l'ADR-0011](0011-leave-out-what-cannot-be-annotated.md). Une première estimation situe le
nombre d'entrées admissibles entre vingt-cinq et trente, et c'est une estimation : elle se
tranchera groupe par groupe, pas par ce document.

**Cinq noms existent déjà dans le catalogue.** Anti-corruption layer et Domain event sont des
entrées `DomainDrivenDesign` ; Shared database, Remote Procedure Invocation et Messaging sont
des entrées `EnterpriseIntegration`.
[L'ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) les tranche une
par une et pose une seule question — *cette* œuvre présente-t-elle le pattern comme le sien, ou
se contente-t-elle de le citer ? Rien d'autre n'est à arbitrer, puisque chaque catalogue est
livré comme son propre paquet
([ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md)) et que deux paquets
peuvent porter le même nom.

**L'œuvre est davantage qu'un jeu de noms.** La moitié des groupes pose une question explicite
— *How to implement operations that span multiple services ?*, *How to send messages as part
of a database transaction ?* — et chaque pattern y répond avec des participants nommés : une
saga a des transactions locales, des transactions de compensation et un orchestrateur ; l'API
composition a un composeur et les services qui détiennent les données ; le command-side replica
a un service de commande, un service fournisseur et une base répliquée. Ce sont des rôles, et
c'est ce que cette bibliothèque annote.

**Certaines entrées sont les anti-patterns de l'auteur lui-même.** Shared database est présenté
comme un pattern à part entière et désigné depuis *Database per Service* comme « the Shared
Database anti-pattern ».
[L'ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md) a déjà tranché :
un anti-pattern entre aux mêmes conditions que n'importe quel pattern, parce que dire *voici la
forme dans laquelle nous sommes coincés* vaut autant que dire *voici la forme que nous avons
choisie*.

## Décision

*Microservices Patterns* est admis comme catalogue sous le nom `MicroservicesPatterns`, et ses
patterns entrent selon les critères déjà appliqués à toutes les autres œuvres.

Là où la question de l'ADR-0028 est réellement serrée — l'œuvre présente le pattern, mais un
lecteur pourrait soutenir qu'elle s'appuie sur une source antérieure — **elle est tranchée de
façon inclusive** : l'entrée est retenue. Un développeur qui cherche `[Saga]` ou
`[DomainEvent]` dans une codebase microservices et ne les trouve pas dans le paquet
microservices a été mal servi par le catalogue, quoi qu'en dise l'argument de provenance.
L'inclusion coûte un nom en double dans un paquet séparé ; l'exclusion coûte à un lecteur le
mot qu'il était venu chercher.

## Justification

Le vocabulaire vaut le plus là où le nommage vaut le moins, ce qui est l'argument de
[l'ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md) et qui vaut ici pour une
seconde raison : le code microservices est réparti sur plusieurs dépôts, si bien que le
relecteur qui aurait attrapé une classe mal nommée ne le lit pas. `OrderSaga` peut être un
orchestrateur, un participant, ou une classe qui a simplement le mot dans son nom, et il
n'existe aucun endroit où la réponse se trouve.

Les assertions sont de la bonne sorte. La transaction locale d'un participant de saga doit
avoir une transaction de compensation, sinon la saga ne peut pas revenir en arrière ; une vue
CQRS est en lecture seule et son écrivain est le gestionnaire d'événements, pas la requête ; un
service sous *Database per Service* doit être le seul à toucher son schéma ; un command-side
replica est périmé par construction et rien de ce qui le lit ne peut supposer le contraire.
Chacune est une règle à laquelle un relecteur peut tenir une pull request, et aucune ne
paraphrase l'annotation — le test de
[l'ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md).

Admettre l'œuvre permet aussi au catalogue de dire ce qu'il ne sait pas dire aujourd'hui : que
ces patterns sont les mêmes, renommés. Le Domain event de Richardson est celui d'Evans, appliqué
à un problème qu'Evans n'avait pas ; sa Shared database est le style d'intégration de Hohpe et
Woolf vu de l'autre bout, comme une chose à fuir plutôt qu'une chose à choisir. Les tenir tous
les deux, chacun sous le nom de son œuvre, c'est ce à quoi sert l'ADR-0028, et ce catalogue est
le premier à l'exercer à grande échelle.

La taille est la bonne. Vingt-cinq à trente entrées admissibles en font un catalogue moyen, à
remplir par tranches comme l'ont été *Enterprise Integration Patterns* et *xUnit Test Patterns*.

## Alternatives envisagées

### Attendre que le domaine se stabilise

Toutes les autres œuvres ici sont stabilisées. Le vocabulaire microservices a quinze ans au
plus, il bouge encore, et le site lui-même porte des entrées marquées *new*.

Rejeté. Les patterns proposés à l'admission sont justement la part stabilisée : Saga, CQRS,
Event sourcing, API composition et Database per Service n'ont pas changé de sens depuis 2018,
et plusieurs sont plus anciens. Ce qui bouge encore, c'est la moitié déploiement et
observabilité — que l'ADR-0011 exclut de toute façon, pour une autre raison. Attendre protège
d'un risque que la moitié admissible de ce catalogue ne porte pas.

### Ne prendre que les groupes de gestion des données

Service collaboration et Transactional messaging : onze patterns, la part qui parle de code
plutôt que de topologie, rangée comme un petit catalogue à elle.

Rejeté sur la forme plutôt que sur le fond. C'est bien à cela que le catalogue reviendra en
pratique — mais le décider d'avance préjugerait des groupes External API, Communication styles
et Reliability, où Circuit breaker, API gateway et Idempotent consumer sont exactement le genre
de participant qu'une classe porte. Chaque groupe est jugé quand on l'atteint, comme tous les
autres catalogues ont été remplis.

### Refuser les cinq homonymes

Ne tenir Domain event que sous `DomainDrivenDesign`, Shared database que sous
`EnterpriseIntegration`, au motif qu'un nom devrait désigner une seule chose.

Rejeté, et c'est l'arête la plus vive de cette décision. Le refus général était déjà celui de
l'ADR-0028 ; ce qui est nouveau, c'est la posture énoncée plus haut. Un nom désigne une seule
chose *à l'intérieur d'une œuvre*, et cette bibliothèque indexe des œuvres.
`Reefact.LivingDocumentation.Attributes.MicroservicesPatterns` est installé par quelqu'un qui
construit des microservices, et le mot qu'il cherchera est celui qu'emploie son schéma
d'architecture.

Deux des cinq ont été vérifiés contre le test de l'ADR-0028 avant la rédaction de ce document.
`shared-database.html` et `domain-event.html` sont des exposés complets — contexte, problème,
solution, patterns liés — et Domain event répond à un problème qu'Evans n'a jamais posé :
*how does a service publish an event when it updates its data ?* Créditer le DDD dès la première
ligne relève de l'érudition, pas de la citation au sens que l'ADR-0028 exclut.

### L'admettre sous `Idioms`

Ranger Saga, CQRS et les autres comme des idiomes individuels.

Rejeté : [l'ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) réserve
`Idioms` aux patterns sans corpus à eux, ce qui est le contraire de ce cas.

## Conséquences

### Positives

* Le catalogue gagne un vocabulaire issu de la décennie dans laquelle ses lecteurs travaillent,
  ce qui est l'objectif de l'ADR-0029 poussé d'un cran.
* Une règle peut parcourir une codebase distribuée : *toute transaction locale d'une saga a une
  transaction de compensation*, *rien hors d'un service ne lit son schéma*, *une vue CQRS a
  exactement un écrivain*.
* L'ADR-0028 est exercé là où il compte le plus — plusieurs œuvres présentant le même pattern,
  chacune avec son nom et son accent — ce qui rend le catalogue lisible comme un ensemble
  d'œuvres plutôt que comme une liste aplatie.

### Négatives

* Environ la moitié de l'œuvre sera laissée de côté, et chaque exclusion est un jugement qui
  doit être consigné dans `catalog/README.md` sous peine d'être lu comme un oubli.
* Cinq noms existeront deux fois dans le catalogue : un lecteur qui parcourt
  [l'index](../../../generated/catalog-index.md) rencontrera `DomainEvent` sous deux œuvres et
  devra lire le paquet pour savoir duquel il s'agit. C'est le coût assumé de la posture énoncée
  dans la décision.
* Le langage de patterns est maintenu sur un site plutôt que figé dans un livre : *ce qui
  appartient à l'œuvre* peut donc changer sous le catalogue, ce qui n'est pas le cas des sept
  autres.

### Risques

* Le site est celui de l'auteur et le livre est celui de l'auteur, mais ils ne sont pas
  identiques : le site porte des entrées que le livre de 2018 n'a pas, et une seconde édition
  est en cours. L'atténuation est le champ `reference`, qui nomme l'œuvre et non l'URL, et la
  règle qu'une entrée n'est ajoutée que là où le site indique que le livre la traite ou que le
  pattern lui est antérieur.

## Actions de suivi

* Remplir le catalogue par tranches, en commençant par Service collaboration.
* Répondre à la question de l'ADR-0028 dans le commit qui ajoute chaque homonyme, pas d'avance.
* Consigner chaque pattern exclu dans `catalog/README.md` avec le critère auquel il échoue.

## Références

* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — l'objectif que
  celui-ci suit : des patterns d'usage quotidien plutôt que plus de patterns.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — la règle qui
  tranche les homonymes, et la posture que ce document énonce pour les cas serrés.
* [ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md) — Shared database
  comme anti-pattern entre aux mêmes conditions.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — ce qui ne peut pas être annoté est
  laissé de côté, ce qui est la moitié de cette œuvre.
* `catalog/README.md` — les entrées laissées de côté et pourquoi.
