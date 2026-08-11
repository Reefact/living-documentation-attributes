# ADR-0037 | Admettre le catalogue Dependency Injection, avec ses code smells et ses lifestyles

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0037-admit-the-dependency-injection-catalogue.md)

**Statut :** Proposé
**Proposé :** 2026-08-11
**Décideurs :** Reefact

## Contexte

Neuf œuvres sont cataloguées, plus `Idioms` : **332 patterns pour 326 noms distincts, 557 noms de
rôles**, et sept des neuf complètes. La dernière, `Posa2`, atteint l'intérieur d'un processus pour
nommer un verrou, un moniteur, un pool de threads.

**Aucune des neuf ne dit comment une classe obtient les collaborateurs dont elle a besoin.** Le
catalogue tient les cinq patterns de création du Gang of Four, qui portent sur la façon dont un objet en
*fabrique* un autre ; `EnterpriseApplicationArchitecture` tient `Registry`, `Plugin`,
`SeparatedInterface` et `ServiceStub`, qui effleurent la question ; `DomainDrivenDesign` tient
`Factory`. Aucun ne nomme l'endroit où le graphe d'objets d'une application est assemblé, ni le
constructeur par lequel une dépendance arrive, ni la différence entre une dépendance qu'on peut coder en
dur et une qu'il faut injecter. Toute application .NET fait ce travail au démarrage, et le vocabulaire
pour en parler est celui de la plateforme que les paquets visent.

*Dependency Injection Principles, Practices, and Patterns* — Steven van Deursen et Mark Seemann,
Manning, mars 2019, ISBN 9781617294730, 552 pages — est l'œuvre. C'est la deuxième édition de
*Dependency Injection in .NET* de Seemann (2011), et elle porte un catalogue explicite. Son sommaire au
niveau des sections, lu sur l'édition en ligne de l'éditeur, nomme **quatorze éléments dans trois
sections de catalogue** :

| Section | Éléments |
|---|---|
| 4 — DI patterns | Composition Root, Constructor Injection, Method Injection, Property Injection |
| 5 — DI anti-patterns | Control Freak, Service Locator, Ambient Context, Constrained Construction |
| 6 — Code smells | Constructor Over-injection, Abuse of Abstract Factories, Cyclic Dependencies |
| 8.3 — « Lifestyle catalog » | Singleton Lifestyle, Transient Lifestyle, Scoped Lifestyle |

Quatre autres concepts nommés se trouvent hors de ces sections : **Captive Dependency** et **Leaky
Abstraction** (§8.4, les mauvais choix de durée de vie), et **Stable Dependency** et **Volatile
Dependency** (§1.3, la classification sur laquelle tout le livre repose — une dépendance volatile est
une dépendance qu'il faut injecter).

Cinq faits au sujet de cette liste comptent ici.

**Trois genres entrent ensemble, et un seul est déjà tranché.** Les patterns ne demandent aucune
décision. Les anti-patterns sont réglés par
l'[ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.fr.md), qui admet un
anti-pattern aux mêmes conditions que n'importe quel pattern. **Les code smells sont un troisième genre
sur lequel la base ne s'est jamais prononcée.** Et les lifestyles sont un quatrième : l'œuvre elle-même
appelle le §8.3 un « Lifestyle catalog » et non un ensemble de patterns, si bien que les admettre revient
à tenir quelque chose que ses propres auteurs n'appellent pas un pattern.

**Presque rien ici n'échoue à l'[ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md), parce que le
livre est écrit en C#.** Un composition root est une classe ou une méthode ; les trois patterns
d'injection *sont* un constructeur, une méthode et une propriété ; Control Freak est la classe qui
instancie elle-même ses dépendances ; Ambient Context est le point d'accès statique ; les lifestyles
contraignent une classe. Un élément échoue : **Cyclic Dependencies** n'a pas de déclaration unique — un
cycle est une propriété d'un graphe, et l'annoter sur chaque participant affirmerait une relation que
l'attribut ne porte pas.

**Quatre noms touchent `GangOfFour`, et aucune de ces collisions n'est forcée.** `Singleton`,
`AbstractFactory`, `Facade` et `Decorator` y sont déjà tenus. Mais les noms de l'œuvre sont
`SingletonLifestyle` et « Abuse of Abstract Factories », et Decorator et Facade sont ceux du Gang of
Four, que les chapitres 6 et 9 *citent* sans les présenter — exactement la distinction de
l'[ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.fr.md). Employer
l'orthographe de chaque œuvre, ce que l'ADR-0028 exige, ne laisse aucune collision.

**Un quasi-homonyme est un désaccord et non une coïncidence.** Le Singleton Lifestyle et le Singleton du
Gang of Four sont opposés par cette œuvre à dessein : son argument est qu'une instance unique doit être
décidée par le composition root et non imposée par le type, si bien que la lifestyle est ce qu'un
lecteur emploie *à la place* du pattern.

**C'est l'édition qui fixe la liste, et celle de l'édition précédente est différente.** La première
édition de 2011 a un seul auteur, un autre découpage en chapitres et un autre jeu d'anti-patterns.
Sources : l'édition en ligne de l'éditeur donne le sommaire au niveau des sections pour chaque chapitre ;
le blog de Seemann porte la définition canonique du Composition Root — « *a (preferably) unique location
in an application where modules are composed together* » — et des billets sur la plupart du reste ; les
articles libres de Manning, un par pattern, sont derrière un captcha et n'ont pas été lus.

## Décision

L'œuvre est admise comme catalogue sous le nom `DependencyInjection`, et **ses code smells et ses
lifestyles entrent aux mêmes conditions que ses patterns**.

## Justification

Le manque est le plus large qui reste et le plus employé. La composition est ce que toute application
fait avant de faire quoi que ce soit d'autre, et c'est la seule partie d'une base de code où une erreur
est invisible dans une signature de type : une classe qui prend une abstraction et une classe qui va
chercher une statique se ressemblent vues de l'extérieur, et diffèrent en tout ce qui compte. Nommer la
différence, c'est le but de l'[ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.fr.md)
appliqué à ce qu'un lecteur .NET fait le plus.

Les assertions sont les plus vérifiables de la base, et elles le sont d'une façon nouvelle. La règle que
Seemann donne lui-même pour un composition root est qu'**un conteneur de DI y est référencé et nulle part
ailleurs** — ce qu'une compilation peut faire respecter dès aujourd'hui, contre les références
d'assemblage, sans lire une ligne de logique. Ce n'est pas une règle sur la forme du code, c'est une
règle sur la configuration, et la seule affirmation comparable du catalogue est celle de
`ServicePerTeam`, vérifiable contre un `CODEOWNERS`. Les lifestyles sont du même genre :
`[SingletonLifestyle]` sur une classe dit *celle-ci doit être enregistrée une fois*, et l'enregistrement
du conteneur est d'accord ou ne l'est pas.

Le raisonnement de l'ADR-0023 s'étend à un code smell sans effort, et c'est pourquoi la décision les
prend au lieu de discuter du mot. Cet enregistrement a admis un anti-pattern parce que dire *voici la
forme dans laquelle nous sommes coincés* vaut autant que dire *voici la forme que nous avons choisie*. Un
smell est le même énoncé tenu avec moins de certitude, et une annotation est un meilleur endroit pour lui
qu'un wiki : elle se pose sur le constructeur en question plutôt que dans un document sur les
constructeurs en général. Ce qui est réellement nouveau, c'est le mot de degré — `ConstructorOverInjection`
dit *trop*, et *trop* est un jugement et non un fait. Ce n'est pas une objection mais la description de
l'entrée : l'annotation *est* le jugement, porté par quelqu'un, consigné là où il s'applique, ce qui est
la prémisse de toute la bibliothèque.

Les lifestyles sont admises quoique l'œuvre ne les appelle pas des patterns, et la raison est que le test
de l'[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) porte sur ce qu'une chose
affirme et non sur la façon dont on la nomme. Une lifestyle affirme quelque chose à quoi un relecteur
peut tenir une pull request, elle se pose sur une déclaration, et c'est l'entrée qu'un lecteur .NET
cherchera en premier. La refuser sur le mot *catalog* serait le genre de nominalisme que
l'[ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.fr.md) a dû défaire dans un autre
catalogue après neuf livraisons.

Les homonymes sont le meilleur cas que l'ADR-0028 ait eu. Ailleurs deux œuvres nomment la même idée et le
doublon est un coût ; ici deux œuvres nomment des idées **opposées** avec un seul mot, et le catalogue est
le seul endroit où une base de code peut dire laquelle elle veut dire. Un lecteur qui annote
`[SingletonLifestyle]` a énoncé que l'instance unique est la décision du composition root ; celui qui
annote le `[Singleton]` du Gang of Four a énoncé que le type l'impose. Cette distinction est invisible en
C#, et c'est exactement le genre de chose que cette bibliothèque existe pour rendre dicible.

## Alternatives envisagées

### Ne prendre que les patterns et les anti-patterns

Huit entrées, en laissant dehors les smells du chapitre 6 et les lifestyles du §8.3. Envisagé parce qu'un
code smell n'est un pattern dans aucune des neuf œuvres déjà cataloguées, et parce qu'une lifestyle n'en
est pas un dans les mots de cette œuvre — c'est donc l'option qui n'ajoute aucun genre nouveau.

Rejeté sur ce que cela coûterait. Cela laisse dehors `ConstructorOverInjection`, l'entrée qu'un relecteur
irait chercher le plus souvent des quatorze, et les trois lifestyles, qui portent les affirmations les
plus vérifiables du candidat. Et le motif du refus serait le *mot* plutôt que le test de l'ADR-0007, qui
demande quelles assertions une chose porte.

### Prendre les lifestyles et refuser les smells

Une position médiane : les lifestyles portent des affirmations vérifiables, les smells portent un
jugement de degré.

Rejeté, quoique ce soit le refus le plus défendable qui se présente et que le mainteneur puisse le
préférer. Le mot de degré est réel, mais une base de code qui marque sa propre dette connue fait ce que
l'ADR-0023 a admis les anti-patterns pour permettre. Si c'est là que passe la ligne, alors
`ConstructorOverInjection` est la seule entrée à laisser dehors et `AbuseOfAbstractFactories` — qui est
une forme et non une quantité — devrait quand même être tenue.

### Admettre *Release It!* à la place

Les patterns de stabilité de Nygard ont été contrôlés d'abord, sur instruction du mainteneur, et étaient
la recommandation avant que le contrôle soit fait.

Rejeté sur le contrôle, et consigné ici pour que le refus soit au dossier et non seulement dans une
conversation. Sept de ses vingt-quatre éléments survivent à l'ADR-0011 — les anti-patterns de Nygard sont
des modes de défaillance d'un système en marche plutôt que des formes dans du code — et les sept
survivants n'ont pas de participants où prendre des noms de rôles, parce que le livre est écrit en essais
et non dans la forme que la deuxième règle de l'ADR-0035 présuppose. Chaque rôle serait une invention de
ce catalogue. Il reste un candidat si le mainteneur l'accepte.

### Le nommer `Di`, ou `DependencyInjectionInDotNet`

Envisagés pour la brièveté et pour la précision respectivement.

Rejetés. `Di` est une abréviation que personne n'écrit en prose et, contrairement à `Posa2`, que personne
ne dit à voix haute. `DependencyInjectionInDotNet` nomme le titre de la **première** édition, celle dont
ce catalogue ne suit pas la liste.

### Ranger les patterns d'injection sous `Idioms`

Constructor Injection en particulier est souvent décrit comme un idiome de langage plutôt que comme un
pattern.

Rejeté : l'[ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md) réserve `Idioms`
à un pattern sans corpus à lui, et ceci est un corpus de 552 pages avec un catalogue explicite.

## Conséquences

### Positives

* Le catalogue peut dire comment une base de code est câblée, ce que toute application .NET fait et ce
  qu'aucune des neuf œuvres ne pouvait nommer.
* Les premières affirmations vérifiables contre la *configuration* plutôt que contre le code : un
  enregistrement de conteneur, une référence d'assemblage, un point d'entrée.
* L'ADR-0028 est exercé sur deux œuvres qui **se contredisent**, plutôt que sur deux qui se recouvrent,
  ce qui est la forme la plus forte de ce qu'il énonce.

### Négatives

* Trois entrées ne sont pas des patterns dans les mots de leur propre œuvre, et cet enregistrement est le
  seul endroit qui dise pourquoi elles sont tenues quand même.
* Un lecteur qui parcourt [l'index](../../../generated/catalog-index.md) croise `SingletonLifestyle` près
  du `Singleton` du Gang of Four et doit ouvrir deux paquets pour apprendre qu'ils sont opposés.
* `ConstructorOverInjection` porte un mot de degré. Deux équipes placeront la ligne ailleurs, et
  l'annotation se lira comme une affirmation sur un nombre alors qu'elle en est une sur un jugement.

### Risques

* L'écosystème des conteneurs bouge plus vite qu'un langage de patterns. *Scoped* est le mot d'ASP.NET
  Core aujourd'hui et n'était pas le mot en 2011 ; un nom de lifestyle peut vieillir plus vite que tout
  le reste de la base.
* C'est le premier candidat écrit pour une seule plateforme. Ses lifestyles présupposent un conteneur, et
  un lecteur qui pratique la Pure DI du livre lui-même a trois entrées qui ne le concernent pas.
* `CyclicDependencies` est exclu et c'est le problème que toute équipe rencontre. Un lecteur le
  cherchera, et `catalog/README.md` est là où il doit trouver la raison.

## Actions de suivi

* Remplir le catalogue par livraisons, en commençant par les DI patterns du chapitre 4.
* Consigner dans `catalog/README.md` que l'édition de 2019 fixe la liste et que celle de 2011 diffère —
  avant qu'une livraison ne repose sur la mauvaise.
* Trancher `StableDependency`, `VolatileDependency`, `CaptiveDependency` et `LeakyAbstraction` quand le
  chapitre 8 sera atteint : ils sont nommés dans l'œuvre mais hors de ses trois sections de catalogue,
  donc savoir s'ils sont des entrées est une question à laquelle cet enregistrement ne répond pas.
* Consigner chaque élément exclu dans `catalog/README.md` avec le critère auquel il a échoué,
  `CyclicDependencies` en premier.

## Références

* [ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.fr.md) — admet les quatre
  anti-patterns sans autre argument, et fournit le raisonnement que cet enregistrement étend aux smells.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) — le test qui fait entrer
  une lifestyle : ce qu'une chose affirme, non la rubrique où son auteur la classe.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) — exclut `CyclicDependencies` et rien d'autre
  ici.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.fr.md) — décide les quatre
  homonymes, et est exercé ici sur des œuvres qui se contredisent.
* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.fr.md) — le but : des patterns
  d'usage quotidien plutôt que davantage de patterns.
* [ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.fr.md) — la discipline de compte et de
  provenance, et la raison pour laquelle l'édition est nommée d'avance ici.
* La définition du Composition Root par Seemann lui-même, et la règle qu'un conteneur n'est référencé de
  nulle part ailleurs : <https://blog.ploeh.dk/2011/07/28/CompositionRoot/>.
