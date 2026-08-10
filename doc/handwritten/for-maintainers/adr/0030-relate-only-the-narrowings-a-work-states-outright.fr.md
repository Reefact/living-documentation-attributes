# ADR-0030 | Ne relier que les restrictions qu'une œuvre énonce explicitement

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0030-relate-only-the-narrowings-a-work-states-outright.md)

**Statut :** Accepté
**Proposé :** 2026-08-10
**Accepté :** 2026-08-10
**Décideurs :** Reefact

## Contexte

Une entrée du catalogue peut déclarer `specialisationOf` et nommer un autre pattern du même
catalogue qu'elle restreint. La relation est émise sous forme d'héritage, et la
documentation générée dit ce qu'elle signifie : *tout participant annoté ici en est aussi un
de ceux-là, et un consommateur qui demande le pattern plus large obtient ceux-ci en plus*.

Dix relations existent aujourd'hui, dans trois catalogues. Elles prennent deux formes :

* quatre visent un pattern à **un seul rôle**, et font dériver un attribut d'un attribut —
  `PostAttribute : PartyAttribute`, `RowDataGatewayAttribute : GatewayAttribute` ;
* six visent un pattern à **plusieurs rôles**, et dérivent de la base abstraite que ses
  rôles partagent — `HierarchicAccountabilityAttribute : Accountability.Role`,
  `SecondaryPostingRuleAttribute : PostingRule.Role`.

Les deux formes sont livrées et correctes. La seconde dit « un participant du pattern plus
large » sans nommer lequel, ce qui est précisément le sens d'une restriction portant sur le
pattern entier.

`EnterpriseIntegration` contient les **65** patterns de son livre et, avant cette décision,
aucune relation. Ce n'était pas parce que le livre n'en énonce aucune. Il en énonce de deux
manières très différentes :

**Par la structure.** Le chapitre *Message Routing* présente douze patterns sous
`MessageRouter`, le pattern de base donné au chapitre 3 ; *Message Transformation* en
présente six sous `MessageTranslator` ; les canaux sont sous `MessageChannel`, les
consommateurs sous `MessageEndpoint`. Une trentaine d'entrées sont arrangées ainsi.

**Explicitement, dans le texte d'un pattern.** Quatre entrées ont une phrase portant sur les
deux patterns :

* le livre affirme qu'un **Wire Tap** *est* une `RecipientList` figée à deux canaux de
  sortie ;
* il présente **Command Message**, **Document Message** et **Event Message** comme trois
  sortes de `Message`.

Le `README.md` du catalogue donnait jusqu'ici, pour les trois dernières, une raison — que la
relation affirmerait quelque chose que le livre ne dit pas, puisque `Message` a trois rôles
et que l'héritage partirait de `Message.Role`. Les six relations livrées de même forme
montrent que c'est faux : la relation **sous-spécifie, elle n'affirme rien de faux**. Un
command message est un participant du pattern Message ; ce qui est perdu, c'est seulement
lequel.

[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) fixe déjà la façon
de trancher ce genre de question : par les affirmations que porte une entrée, jamais par son
nom ni par son voisinage.

## Décision

Une restriction est consignée en `specialisationOf` là où l'œuvre l'énonce à propos des deux
patterns, et non là où elle est seulement suggérée par la façon dont l'œuvre les range.

## Justification

Les deux façons dont un livre énonce une famille ne sont pas la même preuve, et ADR-0007 est
le test qui les sépare. Une phrase disant qu'un wire tap *est* une recipient list est une
affirmation sur un pattern, faite par l'auteur, et c'est exactement ce que ce catalogue
existe pour reporter. Un titre de chapitre, c'est du voisinage : douze patterns sont imprimés
sous `MessageRouter` parce qu'ils parlent de routage, et un lecteur qui en déduirait que
chacun *est* un routeur lirait la table des matières plutôt que le texte.

La distinction correspond aussi à ce que la relation coûte. `specialisationOf` est émis en
héritage, ce qui est une promesse faite à tout consommateur futur : dès qu'un attribut dérive
d'un autre, une règle qui demande le pattern plus large change silencieusement de sens.
Quatre relations, chacune avec une phrase à montrer, se relisent avec le livre en main.
Trente dérivées d'un rangement, non — et l'une d'elles fausse ne se retire plus sans casser
qui a écrit une règle dessus.

La sous-spécification est acceptée plutôt que contournée. `WireTapAttribute` dérive de
`RecipientList.Role` et répond donc comme *un participant d'*une recipient list, non comme la
recipient list elle-même ; de même pour les trois intentions de message. C'est ce que disent
déjà les six relations livrées de cette forme : cette décision n'ajoute aucun sens nouveau au
vocabulaire, elle applique quatre fois de plus celui qui existe. Un codebase qui veut le rôle
précis écrit les deux attributs, ce qui coûte une ligne.

## Alternatives envisagées

### Ne rien consigner, et écrire les restrictions en prose

L'état antérieur, et ce que cet ADR proposait avant que la décision soit prise. Son argument
était la prudence : des relations peuvent être ajoutées plus tard sans rien changer de ce qui
est émis, alors qu'en retirer une casse un consommateur.

Écartée : elle traite l'affirmation explicite d'un auteur et un titre de chapitre comme des
preuves aussi faibles l'une que l'autre, alors que tout l'objet d'ADR-0007 est qu'elles ne le
sont pas. Une absence là où le livre dit *est* coûte quelque chose de réel au lecteur et
n'achète que le report d'une décision.

### Relier tout ce que la structure des chapitres suggère

Une trentaine d'entrées, pour que la hiérarchie émise corresponde au rangement du livre.

Écartée sur le test d'ADR-0007 : un titre de chapitre n'est pas une affirmation portée par
une entrée. Cela ferait aussi répondre `MessageRouter` et `MessageTranslator` pour la moitié
du catalogue, ce qui est une grosse revendication à tirer d'une mise en page.

### Laisser `specialisationOf` nommer un rôle plutôt qu'un pattern

`{"catalog": …, "name": "Message", "role": "Message"}`, émis en
`CommandMessageAttribute : Message.MessageAttribute`, plus précis que dériver de
`Message.Role`. Le générateur porte un point d'accroche inutilisé dont ce serait le besoin —
un ensemble de rôles à émettre non scellés, lu au moment de choisir le modificateur de chaque
rôle et jamais alimenté.

Reportée, non rejetée : elle ajoute une seconde forme d'émission au seul mécanisme de
relation du vocabulaire, et l'imprécision qu'elle corrige n'a encore rien coûté à personne.
C'est le changement à faire si la perte de précision commence à se faire sentir maintenant
que ces quatre-là existent.

## Conséquences

### Positives

* Ce qu'un auteur affirme sur deux de ses propres patterns est porté dans le vocabulaire au
  lieu de s'arrêter à la prose.
* Un consommateur qui demande une recipient list obtient les wire taps, et un qui demande le
  pattern Message obtient les commandes, documents et événements.
* La règle pour ajouter une relation est un test qu'un contributeur applique seul : trouver
  la phrase, ou ne pas relier.

### Négatives

* Les quatre relations sont émises à la précision de la base de rôle, donc une règle qui
  demande précisément `Message.Message` n'atteint toujours pas les intentions. Un codebase
  qui veut les deux écrit les deux attributs.
* Le catalogue a désormais des entrées reliées et non reliées côte à côte, et rien dans la
  sortie générée ne dit de quel côté du test est tombée une paire non reliée. C'est à quoi
  servent cet ADR et `catalog/README.md`.

### Risques

* « L'énonce explicitement » est un jugement sur une phrase, et un contributeur, le livre
  ouvert, peut en lire une là où l'auteur faisait une remarque en passant. La parade est la
  même que pour toute entrée : l'affirmation doit être citable dans la pull request.

## Actions de suivi

* Reconsidérer les relations visant un rôle si les quatre émises ici s'avèrent trop
  grossières à l'usage.
* Décider séparément si le point d'accroche inutilisé du générateur est du code mort à
  retirer ou la couture dont cette alternative aurait besoin ; il est aujourd'hui lu et jamais
  alimenté, ce qui n'est ni l'un ni l'autre.

## Références

* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — trancher par les
  affirmations qu'un pattern porte.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) — aucune relation ne
  traverse un catalogue, la question est donc interne à une œuvre.
* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — l'admission de
  l'œuvre.
* `catalog/README.md` — les quatre relations, et ce que la structure des chapitres énonce et
  qui n'est délibérément pas porté.
