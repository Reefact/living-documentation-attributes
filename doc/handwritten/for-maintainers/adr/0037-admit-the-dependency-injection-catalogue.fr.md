# ADR-0037 | Admettre le catalogue Dependency Injection, avec ses lifestyles mais sans ses code smells

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0037-admit-the-dependency-injection-catalogue.md)

**Statut :** Accepté
**Proposé :** 2026-08-11
**Accepté :** 2026-08-11
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

L'œuvre est admise comme catalogue sous le nom `DependencyInjection`, et **ses lifestyles entrent aux
mêmes conditions que ses patterns, ses code smells non**.

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

Les code smells sont refusés, et la raison est le mot de degré et non la catégorie. Le raisonnement de
l'ADR-0023 s'étend bien à eux — dire *voici la forme dans laquelle nous sommes coincés* vaut autant que
dire *voici la forme que nous avons choisie*, et un smell est cet énoncé tenu avec moins de certitude. Ce
qui ne passe pas, c'est qu'un anti-pattern est une **forme**, présente ou absente, tandis que
`ConstructorOverInjection` dit *trop*, et *trop* est un jugement de degré et non un fait au sujet d'une
déclaration.

Cela compte à cause de ce à quoi sert réellement une annotation d'anti-pattern. Ce n'est pas la
détection : un anti-pattern auto-déclaré ne trouve que le contrevenant honnête, et celui qu'il faudrait
attraper est celui que personne n'a annoté. Son usage est une **baseline** : le compte de ce qui est
connu et accepté, qu'une compilation peut tenir à *pas plus que ceci, et jamais plus*. C'est
l'instrument dont ce dépôt vit déjà, dans `PublicAPI.Shipped.txt` et RS0016. Un cliquet a besoin d'un
nombre sur lequel deux personnes s'accordent ; une forme en donne un, un degré non : le même
constructeur est sur-injecté pour un relecteur et convenable pour le suivant, si bien que la baseline
bouge sans que le code bouge.

Les refuser coûte deux entrées au catalogue et aucune cohérence. `AbuseOfAbstractFactories` est celui
qui est sans doute une forme plutôt qu'une quantité, et il sort avec les deux autres parce que la
décision porte sur le **genre** : une règle qui admet un genre est vérifiable par quiconque ajoute
l'entrée suivante, là où une règle qui admet certains membres d'un genre est un jugement à rejouer
chaque fois — et cet enregistrement trancherait alors cas par cas ce qu'il prétend trancher une fois.

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

### Prendre aussi les code smells

Onze entrées plus les trois du chapitre 6. Envisagé parce que le raisonnement de l'ADR-0023 s'étend bien
à eux, et parce que `ConstructorOverInjection` est l'entrée qu'un relecteur irait chercher le plus
souvent des quatorze — la refuser est le coût réel de cette décision et non un arrondi.

Rejeté sur le mot de degré, pour la raison que donne la Justification : un cliquet a besoin d'un nombre
qui ne bouge pas quand le relecteur change. C'était la décision telle que d'abord rédigée, et
l'enregistrement a été amendé avant acceptation plutôt qu'accepté tel quel.

### Ne prendre que les patterns et les anti-patterns

Huit entrées, en laissant aussi dehors les lifestyles du §8.3. Envisagé parce qu'une lifestyle n'est pas
un pattern dans les mots de cette œuvre, c'est donc l'option qui n'ajoute aucun genre nouveau.

Rejeté sur ce que cela coûterait. Les trois lifestyles portent les affirmations les plus vérifiables du
candidat, et le motif du refus serait le *mot* — la rubrique sous laquelle leur auteur les a classées —
plutôt que le test de l'ADR-0007, qui demande quelles assertions une chose porte. C'est la distinction
sur laquelle cette décision tourne deux fois : les lifestyles entrent parce que leur affirmation est un
fait au sujet d'une déclaration, et les smells sortent parce que l'une des leurs n'en est pas un.

### Garder `AbuseOfAbstractFactories` en refusant les deux autres smells

C'est une forme plutôt qu'une quantité, donc l'argument contre `ConstructorOverInjection` ne l'atteint
pas, et il survivrait comme la seule entrée utile du chapitre 6.

Rejeté parce que cela fait de la règle un jugement cas par cas. Une décision sur un genre s'applique par
quiconque écrit l'entrée suivante sans rouvrir cet enregistrement ; une décision sur deux membres sur
trois, non. Si le mainteneur préfère l'entrée à la règle, c'est un amendement d'une ligne et l'entrée est
`AbuseOfAbstractFactories`.

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
* **Trois des quatorze éléments de l'œuvre restent dehors, et un seul des trois échoue à l'ADR-0011.**
  `ConstructorOverInjection` et `AbuseOfAbstractFactories` sont annotables et portent bien des
  assertions ; ils sont absents par décision. Un lecteur qui compte le chapitre contre le paquet trouve
  un chapitre entier manquant, et `catalog/README.md` est là où il doit apprendre qu'il a été refusé et
  non oublié.
* `ConstructorOverInjection` est l'entrée qu'un relecteur aurait cherchée le plus souvent. La refuser est
  le coût d'une règle applicable sans rediscussion, et c'est un coût réel.

### Risques

* L'écosystème des conteneurs bouge plus vite qu'un langage de patterns. *Scoped* est le mot d'ASP.NET
  Core aujourd'hui et n'était pas le mot en 2011 ; un nom de lifestyle peut vieillir plus vite que tout
  le reste de la base.
* C'est le premier candidat écrit pour une seule plateforme. Ses lifestyles présupposent un conteneur, et
  un lecteur qui pratique la Pure DI du livre lui-même a trois entrées qui ne le concernent pas.
* `CyclicDependencies` est exclu et c'est le problème que toute équipe rencontre. Un lecteur le
  cherchera, et `catalog/README.md` est là où il doit trouver la raison.

## Actions de suivi

* Remplir le catalogue par livraisons : les DI patterns du chapitre 4 sont là, restent les anti-patterns
  du chapitre 5 et les lifestyles du §8.3. Onze entrées une fois complet, pas quatorze.
* Consigner dans `catalog/README.md` que l'édition de 2019 fixe la liste et que celle de 2011 diffère —
  avant qu'une livraison ne repose sur la mauvaise.
* Consigner le chapitre 6 dans les tables d'exclusion, en séparant les deux raisons :
  `CyclicDependencies` échoue à l'ADR-0011, tandis que `ConstructorOverInjection` et
  `AbuseOfAbstractFactories` sont refusés par cette décision alors qu'ils y satisferaient.
* Trancher `StableDependency`, `VolatileDependency`, `CaptiveDependency` et `LeakyAbstraction` quand le
  chapitre 8 sera atteint : ils sont nommés dans l'œuvre mais hors de ses trois sections de catalogue,
  donc savoir s'ils sont des entrées est une question à laquelle cet enregistrement ne répond pas.

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
