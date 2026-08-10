# ADR-0036 | Admettre Pattern-Oriented Software Architecture Volume 2 comme catalogue

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0036-admit-posa2-as-a-catalogue.md)

**Statut :** Proposé
**Proposé :** 2026-08-10
**Décideurs :** Reefact

## Contexte

Huit œuvres sont cataloguées — *Design Patterns* (1994), *Analysis Patterns* (1997), *Accounting
Patterns* (2000), *Patterns of Enterprise Application Architecture* (2002), *Domain-Driven Design*
(2003), *Enterprise Integration Patterns* (2003), *xUnit Test Patterns* (2007) et *Microservices
Patterns* (2018) — plus `Idioms`. **315 patterns pour 309 noms distincts, 544 rôles**, en comptant les
deux entrées que l'[ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.fr.md) admet. Sept
des huit sont lues en entier ; `AnalysisPatterns` est en pause volontaire.

**Aucune d'elles ne nomme un participant de la synchronisation intra-processus.** Le catalogue tient
bien deux autres formes de concurrence : *Patterns of Enterprise Application Architecture* donne quatre
verrous hors connexion, qui portent sur une transaction s'étendant sur plusieurs requêtes, et
*Enterprise Integration Patterns* donne Competing Consumers, Message Dispatcher et les deux
consommateurs, qui portent sur qui prend le message suivant. Ni l'un ni l'autre ne dit quoi que ce soit
d'un verrou détenu par un objet, d'une méthode qui suppose le verrou déjà pris, d'un champ confiné à un
seul thread, ou d'un pool de threads prenant leur tour sur une ressource partagée. *Microservices
Patterns* a apporté le vocabulaire de la distribution et s'arrête à la frontière du service ; à
l'intérieur, une classe dont toute la raison d'être est une discipline de verrouillage n'a aucun moyen
de le dire.

*Pattern-Oriented Software Architecture, Volume 2 : Patterns for Concurrent and Networked Objects* —
Schmidt, Stal, Rohnert et Buschmann, Wiley, 2000 — est l'œuvre. Schmidt maintient une page de
présentation du volume qui énonce que **« The book presents 17 interrelated patterns »**, les nomme
tous les dix-sept, et reproduit la table des matières :

| Chapitre | Patterns |
|---|---|
| 2 — Service Access and Configuration | Wrapper Facade, Component Configurator, Interceptor, Extension Interface |
| 3 — Event Handling | Reactor, Proactor, Asynchronous Completion Token, Acceptor-Connector |
| 4 — Synchronization | Scoped Locking, Strategized Locking, Thread-Safe Interface, Double-Checked Locking Optimization |
| 5 — Concurrency | Active Object, Monitor Object, Half-Sync/Half-Async, Leader/Followers, Thread-Specific Storage |

Cinq faits au sujet de cette liste comptent ici.

**Le compte est un fait et non une estimation, et l'œuvre est figée.** Un livre imprimé de 2000, avec
la liste de ses dix-sept patterns tenue par un auteur, ne laisse rien à recompter plus tard. Ce n'est
pas ainsi que la dernière admission s'est passée : l'ADR-0033 annonçait 48 patterns sur quatorze
groupes d'après l'index d'un site, et le chiffre réel était de 53 puces sur 51 pages et 15 groupes, ce
que l'[ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.fr.md) consigne. Rien ici n'a
besoin non plus de la troisième règle de l'ADR-0035 : il y a une édition, et elle porte tous les
patterns.

**Aucun de ses noms n'est déjà dans le catalogue.** Les dix-sept ont été passés contre les 309 noms de
patterns et contre tous les noms de rôles : **aucune collision de nom de pattern**, ce qu'aucun autre
candidat n'a réussi. Un nom de rôle coïncide — `ActiveObject` est un rôle de
`AnalysisPatterns/ObjectMerge`, où il désigne l'enregistrement survivant d'une fusion et non un objet
doté de son propre thread — et il vit à l'intérieur d'un pattern d'un autre paquet. Voisins proches
sans arbitrage nécessaire : Wrapper Facade à côté du Facade du Gang of Four, Strategized Locking à côté
de Strategy, et Reactor et Proactor à côté des consommateurs d'*Enterprise Integration Patterns*.

**Les participants sont des classes et des membres.** Un réacteur a un handle, un démultiplexeur
d'événements synchrone, un gestionnaire d'événements et ses implémentations concrètes ; un objet actif a
un proxy, une requête de méthode, une liste d'activation, un ordonnanceur, un servant et un futur.
C'est ce que l'[ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) demande à un pattern, et c'est
vrai de presque tous les dix-sept — là où les deux dernières admissions ont laissé de côté six entrées
sur 68 et dix sur 51, celle-ci devrait en laisser une.

**Une entrée est réellement ouverte.** Ce que décrit Scoped Locking est la forme que prend un corps de
méthode — prendre à l'entrée, relâcher à chaque sortie — et c'est le terrain sur lequel Guard Clause et
Four-Phase Test ont été écartés. Le garde, lui, est une déclaration : l'entrée pourrait donc être tenue
sur le garde plutôt que sur la discipline. Cela se tranche au chapitre 4, pas ici.

**Le livre appelle deux de ses patterns des idiomes.** La page de Schmidt décrit le matériau comme
allant « from idioms to architecture designs », et le volume qualifie d'idiomes Scoped Locking et
Double-Checked Locking Optimization. Ce dépôt emploie ce mot pour autre chose :
l'[ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md) réserve `Idioms` à un
pattern sans corpus à lui.

Enfin, **POSA est une série de cinq volumes**. Le volume 1 (1996) tient Layers, Broker,
Model-View-Controller et Blackboard ; le volume 4 (2007) reformule une bonne part des volumes 1 et 2 en
un seul langage de patterns. La même page note que le volume 1 « was published in 1996 and hence this
book is referred to as POSA2 ».

## Décision

*Pattern-Oriented Software Architecture, Volume 2* est admis comme catalogue sous le nom **`Posa2`** —
le surnom que ses propres auteurs lui donnent — et ses patterns entrent selon les critères déjà
appliqués à toutes les autres œuvres.

## Justification

Le manque que cela comble n'a rien d'exotique, ce qui est le but de
l'[ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.fr.md) plutôt qu'un but
nouveau. Toute base de code .NET d'une certaine taille tient un verrou, une tâche de fond, un cache
confiné à un thread et une classe qui n'est sûre que parce que les appelants prennent leur tour ; aucune
ne peut le dire aujourd'hui, alors que la même base de code sait déjà déclarer ses verrous hors
connexion et ses consommateurs concurrents. Un vocabulaire qui atteint la frontière d'un service et
s'y arrête laisse dehors la moitié où vivent les bugs de concurrence.

Les assertions sont les meilleures du catalogue au niveau du membre. Thread-Safe Interface dit qu'une
méthode d'interface prend le verrou et n'appelle jamais une autre méthode d'interface, tandis qu'une
méthode d'implémentation suppose le verrou tenu et ne le prend jamais — deux règles, vérifiables, et
entre elles exactement la discipline dont la violation est un auto-interblocage. Double-Checked Locking
Optimization est le pattern célèbre pour être *faux* sans barrière mémoire : l'annoter marque les
endroits à relire plutôt que de simplement les nommer. Les méthodes d'un objet moniteur sont
sérialisées par son propre verrou ; un champ propre à un thread ne doit jamais être publié ; le tour
d'un leader se termine avant qu'il ne traite l'événement. Chacune est une règle à laquelle un relecteur
peut tenir une pull request, et aucune ne redit l'annotation — le test de
l'[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md).

La provenance ne coûte rien ici, pour une fois, et cela vaut d'être pris. Un livre figé, accompagné de
la liste des dix-sept par les auteurs eux-mêmes, signifie que la question de la complétude a une réponse
*avant* que la première entrée soit écrite : *complet* voudra dire complet contre une table des matières
imprimée, comme cela le veut pour le Gang of Four et comme cela ne peut pas le vouloir pour un site
maintenu. Les deux erreurs que l'ADR-0035 a dû consigner — un mauvais compte, et un critère à remplacer
après neuf livraisons — sont deux erreurs que la forme de cette œuvre n'admet pas.

Rien d'autre dans le catalogue n'est dérangé. Zéro homonyme signifie que
l'[ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.fr.md) n'est pas sollicité et
que la posture inclusive de l'ADR-0033 n'a rien à trancher ; ce sont des collaborations entre classes,
exactement comme celles du Gang of Four, donc les ADR-0022, 0023 et 0024 ne sont pas sollicités non
plus. C'est l'admission la plus simple depuis la première.

`Posa2` est le bon nom parce que c'est celui des auteurs, énoncé sur leur propre page — ce qui en fait
le même instrument que `GangOfFour`, et un cas plus solide, puisque ce surnom-là est celui de la
communauté et celui-ci celui des auteurs. Un lecteur qui installe un paquet pour annoter un réacteur
cherche POSA2 : c'est le mot sur la diapositive de conférence, dans la bibliographie, et dans la
discussion qu'il est en train d'avoir sur sa boucle d'événements. Le titre de la série ne peut pas
servir, car cinq volumes publiés sur vingt ans y répondraient tous. La casse suit la règle qui a
produit `Cqrs` — un acronyme de trois lettres ou plus s'écrit en Pascal — donc `Posa2` et non `POSA2`.
L'identifiant court ne voyage jamais seul : la documentation générée écrit le volume en entier sur
chaque ligne, si bien qu'un lecteur qui ignore le surnom apprend le titre là où il le rencontre.

Dire ce que le livre entend par *idiome* n'est pas du pédantisme, car les deux sens tirent en sens
inverse. Le mot du volume porte sur l'*échelle* d'un pattern — assez petit pour être au niveau du
langage. Les `Idioms` de ce dépôt portent sur la *provenance* d'un pattern — pas de corpus à lui. Scoped
Locking a un corpus, ce livre, donc il appartient au catalogue du volume ; le ranger sous `Idioms`
mettrait un chapitre d'un livre catalogué dans le bac réservé aux patterns qui n'ont pas de livre, et
perdrait la citation qui fait toute la valeur de l'entrée.

## Alternatives envisagées

### Le nommer `ConcurrentAndNetworkedObjects`

Le sous-titre du volume. Envisagé parce qu'il est sans ambiguïté sur les cinq volumes sans recourir à un
numéro, qu'il se lit comme de l'anglais plutôt que comme un code, et qu'il suit le même instrument que
`EnterpriseApplicationArchitecture` et `EnterpriseIntegration`, tirés eux aussi de titres.

Rejeté parce que personne n'appelle le livre comme ça. Le nom qu'un lecteur cherche est celui par lequel
les auteurs disent que le livre est désigné, et un nom de paquet techniquement meilleur mais
pratiquement introuvable ne sert à rien. C'était l'option d'abord proposée, et elle était mauvaise pour
la raison que le mainteneur a donnée : les gens connaissent POSA.

### Le nommer `PatternOrientedSoftwareArchitectureVolume2`

Le titre complet de la série plus le numéro du volume. Envisagé parce qu'il est sans ambiguïté et
n'exige aucune connaissance du surnom — la meilleure option pour un lecteur qui découvre la série.

Rejeté sur la longueur. Il produit un segment de namespace et un nom de paquet de 46 caractères, près du
double du plus long de l'ensemble, dans un namespace que tout fichier annoté importe ; et il n'apporte
rien que l'étiquette ne donne déjà, puisque la documentation générée porte le titre complet dans les
deux cas. Le coût retombe sur chaque consommateur, le bénéfice sur la seule première lecture.

### Un seul catalogue pour toute la série POSA

`Posa`, tenant les cinq volumes, au motif que POSA est une série avec un langage de patterns continu et
que le volume 4 reformule explicitement une bonne part des volumes 1 et 2.

Rejeté sur l'[ADR-0027](0027-ship-one-independent-package-per-catalogued-work.fr.md) : un paquet par
œuvre cataloguée, et cinq livres publiés sur vingt ans sont cinq œuvres avec cinq jeux de mots
d'auteurs. Cela obligerait aussi un lecteur qui veut un vocabulaire pour ses verrous à installer Layers
et Broker pour l'obtenir, et cela mettrait les reformulations du volume 4 dans le même paquet que les
originaux qu'elles reformulent — précisément le genre de question que l'ADR-0028 règle en tenant les
œuvres séparées.

### Prendre POSA1 d'abord

Le volume 1 est le plus ancien et le plus cité, et Layers, Broker et Model-View-Controller se disent
plus souvent que Leader/Followers.

Rejeté comme question d'ordre et non d'admission, et l'ordre favorise le volume 2 : plusieurs patterns
du volume 1 qualifient une application ou une topologie de déploiement plutôt qu'une déclaration, ce qui
est l'exclusion de l'ADR-0011 et le terrain sur lequel la moitié de *Microservices Patterns* est restée
dehors, alors que ceux du volume 2 sont des patterns d'objets de bout en bout. Le volume 1 reste un
candidat, à ses propres conditions et avec son propre contrôle d'admission — lequel n'a pas pu être mené
à son terme lorsqu'il a été tenté, les hôtes qui portent sa table des matières étant refusés par le
proxy réseau sortant.

### Ne prendre que les chapitres Synchronization et Concurrency

Huit patterns, la part qui porte sur une discipline de verrouillage dans un seul processus, mise de côté
comme petit catalogue à part, en laissant l'accès aux services et la gestion d'événements dehors.

Rejeté sur la forme et non sur le contenu, comme l'ADR-0033 a rejeté le même geste. Cela préjuge des
chapitres 2 et 3, où Reactor, Acceptor-Connector, Component Configurator et Extension Interface sont
exactement le genre de participant qu'une classe tient — et Reactor est le pattern le plus cité du
livre. Chaque chapitre est jugé quand on l'atteint, ce qui est la façon dont tous les autres catalogues
ont été remplis.

## Conséquences

### Positives

* Le vocabulaire atteint l'intérieur d'un service, là où le catalogue s'arrête aujourd'hui à sa
  frontière, et il le fait au niveau du membre plutôt qu'à celui du type.
* La complétude est décidable avant que le travail commence, contre une table des matières imprimée et
  le compte des auteurs. Aucun catalogue admis depuis le Gang of Four n'a eu cela.
* Aucun homonyme, aucun arbitrage, aucune table d'exclusion attendue au-delà d'une ligne ou deux — la
  moins coûteuse des admissions de la base à mener, quoi qu'elle coûte à décider.

### Négatives

* `Posa2` est un surnom porteur d'un numéro : un lecteur qui ignore la série apprend le titre par la
  documentation plutôt que par le nom. C'est le coût accepté de l'emploi du mot que les gens disent.
* L'usage que le livre fait du mot *idiome* n'est pas celui de ce dépôt, et cet enregistrement est le
  seul endroit qui le dise. Rien n'empêche un contributeur futur de ranger Scoped Locking sous `Idioms`.
* Scoped Locking peut ne pas survivre à l'ADR-0011. Si ce n'est pas le cas, le catalogue en tient seize
  sur dix-sept et l'exclusion est consignée comme n'importe quelle autre.

### Risques

* *Presque tous les dix-sept sont admissibles* est une estimation, et la dernière du genre — vingt-cinq
  à trente sur quarante-huit — s'est révélée fausse au numérateur comme au dénominateur. La mitigation
  est la même que là-bas : chapitre par chapitre, chaque exclusion consignée dans `catalog/README.md`
  avec le critère auquel elle a échoué.
* Les usages connus sont en C++, en C et en Java, et .NET a depuis absorbé plusieurs de ces patterns
  dans le langage et l'exécution. Proactor est ce que `async`/`await` au-dessus des ports de complétion
  est déjà ; Scoped Locking est `lock` ; Monitor Object est proche de ce que `lock` sur un champ privé
  fait d'une classe. Une entrée qui nomme une forme que le langage donne gratuitement peut n'être la
  documentation de rien, et cela se tranche entrée par entrée plutôt que d'être supposé dans un sens ou
  dans l'autre ici.
* C'est le premier catalogue dont les patterns s'emboîtent en langage — les auteurs disent les dix-sept
  interdépendants, et le chapitre 6 les tisse. L'[ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.fr.md)
  n'admet que les restrictions énoncées franchement, donc l'essentiel de cette structure ne sera pas
  exprimable, et un lecteur du paquet verra dix-sept entrées indépendantes là où le livre a une carte.

## Actions de suivi

* Remplir le catalogue par livraisons, chapitre par chapitre. Synchronization et Concurrency d'abord est
  la suggestion — ce sont les chapitres qu'une base de code .NET mono-processus tient — mais l'ordre
  appartient au mainteneur.
* Ajouter `Posa2` à la liste des œuvres du catalogue et à l'étiquette que la documentation générée
  imprime, avec la première livraison et non avec cet enregistrement.
* Trancher Scoped Locking contre l'ADR-0011 quand le chapitre 4 est atteint, et consigner l'issue dans
  un sens comme dans l'autre dans `catalog/README.md`.
* Consigner chaque pattern exclu dans `catalog/README.md` avec le critère auquel il a échoué.

## Références

* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.fr.md) — le but que celui-ci
  suit : des patterns d'usage quotidien plutôt que davantage de patterns.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) — ce qui ne peut pas être annoté reste
  dehors ; Scoped Locking est la seule entrée que cet enregistrement laisse ouverte à cet égard.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md) — réserve `Idioms` à un
  pattern sans corpus à lui, ce qui n'est pas ce que ce livre entend par le mot.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.fr.md) — un paquet par œuvre
  cataloguée, ce qui fait du volume et non de la série l'unité.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.fr.md) — non sollicité par
  cette admission : aucun nom de cette œuvre n'est déjà tenu par une autre.
* [ADR-0035](0035-index-the-pattern-language-and-require-a-write-up.fr.md) — la discipline de compte et
  de provenance dont celui-ci part au lieu d'y arriver.
* La présentation du volume par Schmidt, qui énonce le compte, nomme les dix-sept et reproduit la table
  des matières : <https://www.dre.vanderbilt.edu/~schmidt/POSA/POSA2/>.
