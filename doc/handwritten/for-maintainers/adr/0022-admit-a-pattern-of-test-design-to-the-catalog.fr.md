# ADR-0022 | Admettre au catalogue un pattern de conception de tests

🌍 🇬🇧 [English](0022-admit-a-pattern-of-test-design-to-the-catalog.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-06
**Décideurs :** Reefact

## Contexte

Chacun des quarante-six patterns catalogués jusqu'ici décrit du code de production.
Le catalogue du Gang of Four, les patterns tactiques et stratégiques du
Domain-Driven Design et les entrées tirées de Patterns of Enterprise Application
Architecture décrivent tous la façon dont un système est construit, jamais la façon
dont il est testé.

Object Mother, non. Nommé par Schuh et Punke à XP Universe en 2001, il décrit une
classe qui construit des objets complets pour les tests, afin qu'un test énonce ce
qui compte dans ses données et rien d'autre. Il a une source, il n'a pas de corpus
propre, et il est porté par une classe — il satisfait donc tous les critères que le
catalogue applique déjà
([ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md),
[ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md),
[ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md)).

Rien n'énonce que le catalogue porte sur du code de production. C'est simplement ce
que toutes les entrées se trouvent être, c'est-à-dire le genre de trait qu'un
lecteur de la sortie ne peut pas distinguer d'une décision
([ADR-0001](0001-check-every-pull-request-against-the-adr-base.fr.md)).

Ce qu'un pattern doit porter pour avoir sa place est tranché : des assertions
vérifiables sur un participant
([ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md)). Object
Mother en autorise plusieurs — chaque méthode retourne un objet déjà valide, les
méthodes sont nommées d'après des situations et non d'après des formes, et rien hors
de l'arborescence de tests n'en dépend.

Le code de test est du code qu'un consommateur écrit, compile et relit, et les
attributs ne portent ni comportement ni dépendance : rien, mécaniquement, ne
distingue l'annotation d'un test de celle de quoi que ce soit d'autre.

## Décision

Un pattern de conception de tests entre au catalogue aux mêmes conditions que
n'importe quel autre, et le catalogue n'est pas restreint aux patterns de code de
production.

## Justification

Les critères d'admission répondent déjà à la question, et ils ne mentionnent pas la
nature du code qu'un pattern décrit. Une règle excluant les patterns de test serait
une restriction nouvelle, et rien ne la justifie sinon l'accident que personne n'en
avait encore proposé.

Le vocabulaire est d'autant plus utile que le nommage est mauvais. Un dépôt est
reconnu de tous ; une classe qui construit des objets de test s'appelle fabrique,
helper, builder ou fixture selon qui l'a écrite, et le pattern qu'elle implémente
réellement est invisible. C'est exactement l'écart qu'une annotation comble.

Les assertions sont réelles et de la bonne espèce. *Rien hors de l'arborescence de
tests ne dépend d'un object mother* est une règle de dépendance qu'un build peut
vérifier. *Chaque méthode retourne un objet valide* est une convention à laquelle un
relecteur peut tenir une pull request. Aucune ne redit l'annotation.

L'exclure coûterait plus que cela n'épargne. L'alternative est une règle sur des
catégories de code, appliquée à la frontière de chaque proposition future — un
doublure de test en est-elle un, une fixture, un builder employé des deux côtés — là
où les critères existants tranchent chaque cas sur ce qu'il porte.

Les exemples rendent la distinction visible sans règle. L'exemple d'un pattern de
test est du code en forme de test, dans un projet d'exemples métier, et un lecteur
le rencontre pour ce qu'il est.

## Alternatives envisagées

### Restreindre le catalogue au code de production

Envisagé parce que c'est ce qu'est le catalogue aujourd'hui, parce que cela garde le
vocabulaire sur la conception d'un système plutôt que sur celle de ses tests, et
parce qu'un consommateur qui compte des patterns ne verrait pas un utilitaire de
test apparaître parmi ses agrégats.

Rejeté parce que la restriction devrait être inventée, et qu'elle répond à une
question — *de quelle nature est ce code ?* — que les critères d'admission se gardent
délibérément de poser. Le regroupement relève du consommateur, et le catalogue lui en
donne déjà les moyens : une annotation porte son catalogue, et qui ne veut que les
patterns de production peut le dire.

### Placer les patterns de test dans un catalogue à eux

Envisagé parce que cela permettrait de prendre l'un sans l'autre, et parce que la
frontière serait explicite au lieu d'être implicite.

Rejeté parce qu'un catalogue nomme un corpus
([ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md)), non
une catégorie de code. Un espace de noms `Testing` serait le premier catalogue de la
bibliothèque organisé par sujet, et il rencontrerait immédiatement des patterns
appartenant aux deux — un builder est un pattern du Gang of Four et un idiome de
données de test, et il ne peut pas être dans deux espaces de noms.

### L'admettre, en marquant les patterns de test par un drapeau sur l'entrée

Envisagé parce que cela permettrait de filtrer sans inventer de catalogue.

Rejeté parce que cela énonce en donnée ce que la description du pattern dit déjà, et
qu'il faudrait le trancher pour les quarante-six entrées existantes — dont plusieurs
servent réellement des deux côtés. C'est aussi un champ ajouté pour un consommateur
qui n'existe pas encore.

## Conséquences

### Positives

* Une base de code peut annoter la conception de ses tests avec le même vocabulaire
  que le reste, là où le nommage est le plus incohérent.
* Les critères d'entrée au catalogue restent ceux déjà écrits, sans qu'une règle sur
  les catégories vienne s'y ajouter.
* Une assertion comme *rien hors de l'arborescence de tests n'en dépend* devient
  vérifiable.

### Négatives

* Un consommateur qui compte des patterns récupère les patterns de test mêlés aux
  autres s'il ne filtre pas, et rien ne signale la différence hormis la description
  de l'entrée.
* Le projet d'exemples gagne du code en forme de test sans en être un, ce qui se lit
  étrangement à côté d'une exploitation agricole, d'un chemin de fer et d'un
  assureur.

### Risques

* La frontière relève du jugement, et le candidat suivant sera plus difficile : une
  doublure de test, une fixture, un builder employé des deux côtés. Les critères le
  tranchent, mais « ceci porte-t-il des assertions vérifiables » est une question
  plus lente que « ceci est-il du code de production ».
* Un ensemble de patterns de test pourrait finir par dominer un espace de noms
  partagé avec tout ce qui n'a pas de corpus, ce qui ferait d'`Idioms` un mélange
  plutôt qu'une étagère.

## Actions de suivi

* Réexaminer la forme d'`Idioms` si les patterns de conception de tests s'y
  accumulent — un mélange de choses sans rapport est ce que l'ADR-0013 redoutait pour
  cet espace de noms.

## Références

* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md) —
  l'étagère où atterrit cette entrée, et son risque de devenir un défaut.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) — le
  critère qui l'admet.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) — le critère qu'il
  satisfait également, étant porté par une classe.
* `catalog/Idioms/ObjectMother.json` — l'entrée.
