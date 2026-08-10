# ADR-0032 | Admettre xUnit Test Patterns comme catalogue

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0032-admit-xunit-test-patterns-as-a-catalogue.md)

**Statut :** Accepté
**Proposé :** 2026-08-10
**Accepté :** 2026-08-10
**Décideurs :** Reefact

## Contexte

Six œuvres sont cataloguées — *Design Patterns* (1994), *Analysis Patterns* (1997),
*Accounting Patterns* (2000), *Patterns of Enterprise Application Architecture* (2002),
*Domain-Driven Design* (2003) et *Enterprise Integration Patterns* (2003) — plus `Idioms`
pour les patterns sans corpus à eux. 212 patterns, 414 rôles.

*Enterprise Integration Patterns* a été admis sur un objectif énoncé : des patterns d'usage
quotidien plutôt que plus de patterns
([ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md)). Il est complet à
65, et le même objectif désigne le code de test, où le vocabulaire est employé constamment
et mal employé. « Mock » est le mot que la plupart des codebases donnent aux cinq sortes de
doublure, et un lecteur de `FakeClock` ne peut pas dire si elle répond, enregistre ou juge.

*xUnit Test Patterns* — Meszaros, Addison-Wesley, 2007 — contient **68 patterns**, comptés
sur la table des matières de l'éditeur plutôt que reconstruits : Test Strategy (10), xUnit
Basics (11), Fixture Setup (9), Result Verification (6), Fixture Teardown (4), Test Double
(8), Test Organization (8), Database (4), Design-for-Testability (4) et Value (4).

Trois faits sur cette liste comptent ici.

**Le livre sépare lui-même ses patterns de ses smells.** La partie III est *The Patterns* ;
les smells — Obscure Test, Fragile Test, Assertion Roulette — vivent en partie II et dans une
annexe à eux. Aucun tri n'est nécessaire pour écarter ce que
[ADR-0011](0011-leave-out-what-cannot-be-annotated.md) exclurait au motif d'être un défaut
plutôt qu'un participant.

**Les 68 ne sont pas tous annotables.** Une dizaine sont des formes que prend un corps de
méthode plutôt que des participants portés par une déclaration — Four-Phase Test, In-line
Setup, In-line Teardown, Literal Value — le terrain sur lequel Guard Clause est déjà écarté.
Une première estimation situe le compte admissible entre quarante et quarante-cinq, et c'est
une estimation : elle se tranchera entrée par entrée, pas par cet enregistrement.

**Aucun nom n'entre en collision.** Les 68 ont été confrontés aux 212 noms catalogués : zéro
correspondance exacte. Trois quasi-homonymes existent — `TestStub` à côté de
`EnterpriseApplicationArchitecture/ServiceStub`, `CustomAssertion` à côté de
`DomainDrivenDesign/Assertion`, et les test data builders du livre à côté de
`GangOfFour/Builder` — dont aucun ne demande d'arbitrage puisque chaque catalogue est livré
comme son propre paquet ([ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md)).

**Object Mother ne fait pas partie des 68.** `Idioms/ObjectMother` est tenu sous Schuh et
Punke, 2001, et la question que poserait
[ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — cette œuvre le
présente-t-elle comme sien ? — est tranchée par la table des matières : Meszaros ne le range
pas parmi ses patterns. L'entrée `Idioms` reste seule.

La question de nature est déjà réglée pour ce livre précis.
[ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md) a décidé qu'un pattern de
conception de tests entre aux mêmes conditions que n'importe quel autre, et il a été écrit en
cataloguant Object Mother.

## Décision

*xUnit Test Patterns* est admis comme catalogue sous le nom `XUnitTestPatterns`, et ses
patterns entrent selon les critères déjà appliqués à toutes les autres œuvres.

## Justification

Le vocabulaire sert le plus là où le nommage est le pire : c'est l'argument d'ADR-0022, et il
s'applique ici à l'échelle plutôt qu'à une entrée. Un repository, tout le monde le reconnaît ;
la différence entre un stub, un spy et un mock se dispute toutes les semaines et ne se règle
nulle part. L'annotation met la réponse dans la classe au lieu de la tête du relecteur, ce à
quoi cette bibliothèque sert.

Les assertions sont de la bonne sorte et exceptionnellement tranchantes. Un test stub fournit
des entrées indirectes et n'est jamais consulté ensuite ; un test spy enregistre et ne juge
rien ; un mock object porte des attentes et échoue sur un appel que personne n'avait écrit ;
un fake object a un comportement à lui, ce qui en fait la seule sorte qui puisse être fausse
pendant que tous les tests qui l'utilisent passent. Chacune est une règle à laquelle un
relecteur peut tenir une pull request, et aucune ne reformule l'annotation — le test
d'[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md).

L'œuvre est un catalogue de la main de son auteur : patterns numérotés, renvois par page,
alias et variantes en annexe, de sorte que ce qui lui appartient relève du relevé et non de
l'interprétation. C'est ce qui a rendu *Enterprise Integration Patterns* praticable, pour la
même raison.

La taille correspond à l'objectif. Une quarantaine d'entrées admissibles, c'est le deuxième
catalogue d'ici en volume et le premier qui annote du code de test à l'échelle — là où les
conventions de nommage d'un codebase sont les plus faibles et où rien d'autre dans ce
vocabulaire n'atteint aujourd'hui.

## Alternatives envisagées

### Ne cataloguer que le chapitre Test Double

Huit patterns, la partie dont tout le monde débat, rangés sous `Idioms` ou en catalogue à
part.

Écartée : les huit sont un chapitre d'un livre qui en a soixante autres, et `Idioms` existe
pour les patterns sans corpus à eux
([ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md)) — soit l'inverse de
ce cas. Admettre l'œuvre et la remplir par tranches est ce qui a été fait pour *Enterprise
Integration Patterns*, et cela a fonctionné.

### Admettre Microservices Patterns à la place

Richardson, 2018 : Saga, Transactional Outbox, Circuit Breaker, API Gateway — des mots dits
aussi souvent que ceux-ci, et plus actuels.

Reportée plutôt qu'écartée. Deux choses jouent contre le fait de commencer par là : la moitié
de ce catalogue est de l'infrastructure qu'aucun type C# ne porte — Sidecar, Service Mesh, Log
Aggregation — et une bonne part du reste redit *Enterprise Integration Patterns* et
*Domain-Driven Design* sous d'autres noms, de sorte qu'ADR-0028 dupliquerait beaucoup de ce
qui est déjà là. Ni l'un ni l'autre n'est rédhibitoire ; les deux en font la deuxième chose à
faire plutôt que la première.

### Laisser le code de test hors du catalogue

Cohérent avec les quarante-six premières entrées, qui décrivent toutes du code de production.

Écartée par ADR-0022 avant que cet enregistrement existe. Rien n'énonce que le catalogue
porte sur le code de production ; c'est ce que toutes les entrées se trouvaient être.

## Conséquences

### Positives

* Les cinq sortes de doublure deviennent distinguables dans le code, ce qui est l'argument de
  cette bibliothèque sur les patterns de production, appliqué là où le nommage est pire.
* Une règle peut porter sur un arbre de tests : *rien à l'extérieur n'en dépend*, *un stub ne
  porte pas d'assertion*, *un fake est testé là où l'est la vraie chose*.
* Le catalogue se remplit par tranches, comme *Enterprise Integration Patterns*, donc chaque
  partie se relit seule.

### Négatives

* Environ un tiers du livre restera dehors, et chaque exclusion est un jugement à consigner
  dans `catalog/README.md` sous peine d'être lue comme un oubli — la même traîne de travail
  que porte tout catalogue partiel.
* `DependencyInjection` et `HumbleObject` sont des patterns du chapitre 26 dont les noms sont
  largement associés à Fowler plutôt qu'à Meszaros. Savoir s'il les présente comme siens est
  une question ADR-0028, et elle n'est pas tranchée ici ; elle échoit quand ce chapitre sera
  catalogué.

### Risques

* Le code de test change plus souvent que le code de production, donc une annotation y a plus
  d'occasions de se périmer. La parade est celle sur laquelle la bibliothèque repose déjà :
  l'attribut est posé sur la déclaration, donc il la suit ou cesse de compiler.

## Actions de suivi

* Remplir le catalogue par tranches, en commençant par le chapitre Test Double.
* Répondre à la question ADR-0028 pour `DependencyInjection` et `HumbleObject` quand le
  chapitre 26 sera atteint.
* Consigner chaque pattern exclu dans `catalog/README.md` avec le critère auquel il échoue.

## Références

* [ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md) — un pattern de conception
  de tests entre aux mêmes conditions que n'importe quel autre.
* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — l'admission dont
  celle-ci suit la forme, et l'objectif qu'elle a énoncé.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — ce qui ne peut pas être annoté est
  laissé de côté.
* `catalog/README.md` — les entrées laissées dehors et pourquoi.
