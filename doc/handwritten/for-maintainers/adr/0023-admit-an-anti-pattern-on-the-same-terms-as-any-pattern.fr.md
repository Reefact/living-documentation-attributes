# ADR-0023 | Admettre un anti-pattern aux mêmes conditions que n'importe quel pattern

🌍 🇬🇧 [English](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-06
**Accepté :** 2026-08-06
**Décideurs :** Reefact

## Contexte

Chacune des quatre-vingt-quinze entrées cataloguées jusqu'ici nomme quelque chose
qu'une équipe déclare volontiers. Un code dit qu'il a un agrégat, un dépôt, un
data mapper ; rien dans le vocabulaire n'est jusqu'ici un nom que l'on préférerait
ne pas porter.

Achever le catalogue d'Evans en a rencontré un qui l'est. *Domain-Driven Design*
nomme Smart UI dans son quatrième chapitre et l'appelle l'anti-pattern, puis fait
ce à quoi l'étiquette ne prépare pas : il le présente sous la forme d'un pattern,
avec un *donc*, et énonce les circonstances dans lesquelles il est la bonne
réponse — un projet simple, une vie courte, un seul canal, une équipe pour
laquelle un modèle coûterait plus qu'il ne rapporte.

Il est annotable. Une classe ou un assembly le porte, donc
[ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) ne l'exclut pas. Il a une
source et cette source a un catalogue, donc
[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) le
place, et [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md)
ne s'applique pas.

Les assertions qu'il autorise sont réelles, et d'une espèce qu'aucune autre entrée
ne porte. Toutes les autres annotations *contraignent* ce qu'une règle peut
trouver : un value object est immuable, un membre d'agrégat est inatteignable de
l'extérieur. Celle-ci *exempte* : une règle sur l'endroit où la logique métier a le
droit de vivre s'arrête à la déclaration qui la porte. Une règle de couches sans
moyen de se voir dire où elle ne s'applique pas n'admet aucune exception — ce
qu'aucun code réel ne supporte — ou tient ses exceptions dans une liste qui vit
hors du code.

Rien d'écrit ne dit si un catalogue de patterns de conception porte ceux qu'un
corpus nomme comme des erreurs. C'est simplement ce que toutes les entrées se
trouvent être, c'est-à-dire le genre de trait qu'un lecteur de la sortie ne peut
pas distinguer d'une décision
([ADR-0001](0001-check-every-pull-request-against-the-adr-base.fr.md)).

La question ne s'arrête pas à Evans. Big Ball of Mud est nommé par Foote et Yoder,
et Evans lui-même s'en sert pour caractériser un contexte voisin sur une carte des
contextes — toute réponse donnée ici tranche donc plus d'une entrée.

## Décision

Un anti-pattern entre au catalogue aux mêmes conditions que n'importe quelle autre
entrée — une source, quelque chose qui puisse porter le rôle, et des assertions
vérifiables sur un participant — et être nommé comme une erreur par le corpus qui
l'a nommé n'exclut rien.

## Justification

Les critères d'admission répondent déjà à la question, et ils ne demandent pas si
un pattern est de ceux dont on est fier. Smart UI les satisfait tous. Une règle
excluant les anti-patterns devrait être inventée, et la seule chose qui la
soutienne est l'accident qu'aucun ne s'était encore présenté — l'argument même qui
a été rejeté pour les patterns de conception de tests dans
[ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.fr.md).

L'exclure trahirait le livre. Evans donne les circonstances dans lesquelles Smart
UI est juste ; un catalogue de ses patterns qui écarterait silencieusement celui
qu'il a signalé publierait une opinion en la présentant comme un inventaire, et
l'absence se lirait comme un oubli plutôt que comme une position — c'est
précisément parce que cette distinction compte qu'existe
[ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md).

L'exemption est la partie utile, et rien d'autre ne la fournit. Du code dont les
règles sont dans l'écran et qui ne porte pas d'annotation est indiscernable d'un
code qui a dérivé jusque-là, et le bon réflexe — extraire un modèle — s'applique
alors au seul cas où il est mauvais. Une déclaration en fait une décision avec une
portée, qu'un relecteur peut contester et qu'une règle peut honorer.

Les critères restent discriminants sans règle nouvelle, ce qui est le test de sa
nécessité. Big Ball of Mud y échoue là où Smart UI passe : ce qu'il affirme d'un
participant, c'est qu'il n'a aucune structure discernable, ce qui est l'absence
d'assertion plutôt qu'une assertion, et il n'y a donc rien sur quoi une règle
puisse porter
([ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md)). Une
interdiction ajoutée sur les anti-patterns l'exclurait deux fois et exclurait Smart
UI à tort.

La déclaration ne s'écrit jamais par accident. Personne n'annote une classe comme
une erreur sans le vouloir : le risque, de ce côté, est le sous-emploi et non le
mésusage — et une annotation qui n'est pas écrite coûte un nom dans un espace de
noms, là où celle qui n'est pas écrite aujourd'hui coûte un modèle.

## Alternatives envisagées

### Exclure les anti-patterns en tant que catégorie

Envisagé parce que toutes les autres entrées nomment quelque chose qu'une équipe
déclare volontiers, parce qu'« anti-pattern » est une étiquette qui appelle un
catalogue d'échecs plutôt que de conceptions, et parce qu'un consommateur comptant
des patterns en trouverait un, au milieu de ses agrégats, qui n'est pas une
conception du tout.

Rejeté parce que l'exclusion devrait être inventée et que rien ne la soutient. Elle
trancherait aussi par l'étiquette plutôt que par le contenu : le même livre
présente Smart UI avec un contexte où il est correct, l'étiquette est donc le
jugement de l'auteur sur un arbitrage, pas un énoncé sur ce que le pattern peut
porter. Et elle n'apporte rien — le cas pour lequel elle serait écrite, Big Ball of
Mud, est déjà exclu par le critère d'assertion.

### L'admettre, et le marquer comme anti-pattern dans les données

Envisagé parce qu'un consommateur pourrait alors filtrer, et parce que le catalogue
énoncerait la distinction au lieu de la laisser à une description.

Rejeté parce que cela énonce en données ce que le résumé de l'entrée dit déjà, pour
un consommateur qui n'existe pas encore — le raisonnement qui a rejeté la même
forme dans [ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.fr.md). Il
faudrait en outre trancher pour chaque entrée existante, et la frontière n'est pas
nette : un transaction script est un bon choix et une erreur fréquente, et rien
dans les données ne devrait avoir à dire lequel.

### Le cataloguer sous un autre nom, sans l'étiquette

Envisagé parce que le titre d'Evans porte lui-même les mots *anti-pattern*, et que
les retirer s'écarte un peu de la règle d'
[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) selon
laquelle un pattern s'écrit comme son corpus l'a écrit.

Rejeté parce que l'écart va dans l'autre sens : `SmartUi` **est** le nom dans le
livre, et *anti-pattern* est ce que le livre en dit, non une partie de son nom. Un
lecteur du chapitre cherche Smart UI et le trouve.

## Conséquences

### Positives

* Le catalogue d'Evans peut être achevé sans lacune inexpliquée, et l'entrée qu'un
  lecteur du quatrième chapitre cherche se trouve là où il la cherche.
* Un code gagne un moyen de déclarer une exception délibérée à une règle
  d'architecture dans le code même auquel l'exception s'applique, plutôt que dans
  un fichier de configuration à côté.
* Les critères d'entrée au catalogue restent ceux déjà écrits, sans qu'une règle
  sur les catégories vienne s'y ajouter.

### Négatives

* Un consommateur qui compte des patterns trouve une entrée qui n'est pas une
  conception à viser, et rien d'autre que le résumé ne le dit.
* Le vocabulaire contient désormais un nom qu'une équipe peut répugner à écrire :
  l'annotation la plus utile pour repérer une portée manquera souvent là,
  précisément, où elle serait la plus nécessaire.

### Risques

* Le prochain candidat sera plus difficile. God Object, Anemic Domain Model et Big
  Ball of Mud ont tous une source ; le critère d'assertion tranche chacun d'eux,
  mais *porte-t-il une affirmation vérifiable sur un participant* est une question
  plus lente que *est-ce un anti-pattern*.
* Une annotation qui exempte peut servir à faire taire une règle plutôt qu'à
  consigner une décision. Rien dans le vocabulaire ne distingue les deux — seule la
  relecture le peut, ce qui vaut de toute annotation ici et pèse davantage pour
  celle-ci.

## Actions de suivi

* Réexaminer si un second anti-pattern est proposé, et vérifier que c'est encore le
  critère d'assertion qui discrimine, et non le goût du relecteur.

## Références

* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) — le
  critère qui admet Smart UI et exclut Big Ball of Mud.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) — pourquoi une absence
  doit se distinguer d'un oubli.
* [ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.fr.md) — la même
  forme de question, tranchée de la même façon, pour les patterns de conception de
  tests.
* `catalog/DomainDrivenDesign/SmartUi.json` — l'entrée.
* `catalog/README.md` — où Big Ball of Mud est consigné comme laissé de côté, et
  pourquoi.
