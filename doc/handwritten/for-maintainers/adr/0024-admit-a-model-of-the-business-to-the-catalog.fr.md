# ADR-0024 | Admettre un modèle du métier dans le catalogue

🌍 🇬🇧 [English](0024-admit-a-model-of-the-business-to-the-catalog.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-08
**Accepté :** 2026-08-08
**Décideurs :** Reefact

## Contexte

Les trois catalogues tenus jusqu'ici portent tous sur la façon dont le code est
agencé. Un pattern du Gang of Four est une collaboration entre classes ; un pattern
d'architecture applicative d'entreprise est un mécanisme dont l'application est
faite ; un pattern de Domain-Driven Design est une position prise sur une
déclaration — ce type a une identité, celui-là est une valeur.

*Analysis Patterns* est un livre d'autre chose. Ses patterns sont des modèles : ils
énoncent quels sont les concepts du métier et comment ils se relient. Party affirme
que personne et organisation sont une seule chose partout où un système enregistre
avec qui il traite. Accountability affirme qu'une responsabilité est un objet
plutôt qu'une référence. Post affirme qu'un poste est une partie à part
entière. Aucun n'est une forme que prend le code ; chacun est une affirmation
sur le domaine modélisé.

Leurs participants sont donc nommés entièrement par le lecteur. Annoter le
`Subscriber` d'un service des eaux comme Party dit que cette classe joue le rôle que
le modèle de Fowler donne à Party. Rien dans la déclaration ne suggère
l'annotation, et rien dans le build ne peut la contredire.

Trois critères d'admission d'un pattern sont déjà consignés : l'œuvre le nomme
([ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md)),
une déclaration peut le porter
([ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md)), et il autorise des
assertions qu'un outil pourrait vérifier
([ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md)). Ils
ont été appliqués deux fois à une question de nature plutôt que de contenu — à un
pattern de conception de tests
([ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.fr.md)) et à un
anti-pattern
([ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.fr.md)) —
et dans les deux cas la réponse a été qu'aucun des trois ne demande de quelle
nature est un pattern.

Knowledge Level est consigné dans `catalog/README.md` comme annotable, souhaité et
en attente : Evans le présente dans *Domain-Driven Design* et renvoie à Fowler, qui
l'a nommé, de sorte que l'ADR-0006 le place dans un catalogue que ce dépôt n'avait
pas.

Le catalogue Domain-Driven Design annote déjà les classes métier du lecteur.
`Entity` et `ValueObject` s'appliquent à `Customer` et à `Money`, non à quoi que ce
soit appartenant à cette bibliothèque.

## Décision

Un pattern dont le contenu est un modèle du métier plutôt qu'une forme du code est
admis selon les mêmes trois critères que n'importe quel autre pattern, et ses rôles
sont les participants du modèle.

## Justification

Les critères ne demandent pas de quelle nature est un pattern, et c'est la
troisième fois que cela tranche une question de nature. Deux exceptions seraient un
motif ; trois est un test qui fonctionne comme prévu. Ajouter une règle maintenant
— qu'un pattern doive porter sur le code — devrait se justifier par quelque chose
que les trois existants laissent passer, et c'est l'inverse : ils attrapent plus
qu'une règle sur le code, parce que ce sur quoi ils portent est la vérifiabilité
d'une affirmation.

Les assertions qu'un modèle autorise sont de la sorte ordinaire, et par endroits
plus tranchantes que celles d'un pattern structurel. Le meilleur exemple est celui
qui serait le moins visible sans annotation : les deux extrémités d'une
accountability sont des parties, donc rien dans le système de types ne distingue le
commissionnaire du responsable, et un modèle qui les inverse compile, passe ses
tests, et rapporte qu'un conseil d'administration répond devant chacune de ses
écoles. Nommer les deux extrémités est le seul endroit où cette affirmation existe.
Knowledge Level a la même forme — tout le pattern repose sur une référence qui va
dans un seul sens, et une référence ajoutée dans l'autre est une ligne qui compile
et réunit silencieusement les deux niveaux.

Que les participants soient nommés par le lecteur est ce à quoi servent les
annotations plutôt qu'une objection contre elles. Le vocabulaire existe parce que
presque rien de ce que décide ce dépôt n'est défendu par le compilateur ; un modèle
du métier est simplement le cas où c'est le plus vrai. Le catalogue Domain-Driven
Design annote déjà les classes du lecteur : la nouveauté n'est donc pas le
participant, mais ce qui est dit de lui — qu'il signifie quelque chose dans le
métier, plutôt qu'il possède une propriété de conception.

Le risque qu'un pattern conceptuel dégénère en étiquette est réel, et les critères
l'excluent déjà sans aide : un rôle qui n'autorise aucune assertion n'entre pas,
ce qui est l'ADR-0011 et l'ADR-0007 faisant le travail pour lequel ils ont été
écrits. Les exclusions déjà consignées le montrent à l'œuvre : Responsibility Layers
et Big Ball of Mud sont tous deux des modèles, tous deux nommés, et tous deux dehors
— parce que ce que chacun affirme d'un participant est un ordre dans un cas et une
absence de structure dans l'autre, et que ni l'un ni l'autre n'est de ce sur quoi
une règle peut porter. Le critère fait le travail sans qu'on lui dise qu'un pattern
est conceptuel.

## Alternatives envisagées

### Réserver le catalogue aux patterns portant sur le code, et laisser *Analysis Patterns* dehors

Envisagée parce que c'est la frontière que les trois premiers catalogues tracent de
fait, et qu'un vocabulaire sur l'agencement du code est une chose cohérente à être.

Rejetée parce que cette frontière est un accident de l'ordre dans lequel les livres
ont été catalogués, et parce qu'elle exclurait un pattern que ce dépôt a déjà
consigné comme annotable et souhaité. Knowledge Level est atteint par Evans, qui
renvoie le lecteur à Fowler. Un catalogue qui tient les structures à grande échelle
d'Evans mais refuse celle qu'il attribue ailleurs publie une limitation en
présentant un inventaire.

### Cataloguer ces patterns sous `DomainDrivenDesign`, là où le lecteur les a rencontrés

Envisagée parce que Knowledge Level arrive par *Domain-Driven Design* pour la
plupart des lecteurs, et que l'y mettre est ce qu'attendrait un lecteur qui suit
Evans.

Rejetée par l'ADR-0006, qui existe pour ce cas : un pattern est catalogué là où
l'œuvre qui l'a nommé l'a mis, et la publication la plus ancienne détient la
définition. Le placer sous Evans ferait de la présentation de 2003 la définition
d'un pattern de 1997, et laisserait le reste du livre sans destination.

### Marquer un pattern conceptuel comme une nature distincte

Envisagée parce que la différence entre « cette classe est un Repository » et
« cette classe est une Party » est réelle, et que la consigner permettrait à un
consommateur de les traiter différemment.

Rejetée parce que rien ne porterait sur cette distinction. Ce serait une propriété
du catalogue qu'aucune règle ne lit et sur laquelle aucune annotation n'agit — ce
que l'[ADR-0004](0004-keep-the-attribute-base-a-pure-marker.fr.md) rejette en
général, et ce que l'ADR-0007 rejette pour décider de l'identité. Un consommateur
qui ne veut que les patterns d'une œuvre a déjà le catalogue pour le demander.

## Conséquences

### Positives

* Knowledge Level peut être catalogué, ce qui clôt la seule entrée consignée comme
  attendant une décision plutôt qu'un travail.
* Un quatrième corpus entre, et le premier dont les patterns sont des modèles
  plutôt que des mécanismes — la nature la plus susceptible de mériter une
  annotation, parce que c'est la moins visible dans une déclaration.
* Les trois critères se montrent généraux plutôt qu'ajustés aux patterns
  structurels, ayant désormais tranché des questions d'admission portant sur la
  conception de tests, un anti-pattern et un modèle.

### Négatives

* Le catalogue tient désormais des patterns dont les participants sont nommés
  entièrement par le lecteur, de sorte qu'une annotation fausse est une affirmation
  fausse sur le métier qu'aucun build ne peut contredire. Tous les autres
  catalogues ont cette propriété ; ici, c'est la totalité de ce qui est dit.
* Le relecteur d'une entrée doit connaître le domaine dont l'exemple est tiré assez
  bien pour distinguer une assertion réelle d'une assertion plausible, ce qui est
  une relecture plus lourde que de vérifier qu'un rôle a un endroit où s'appliquer.

### Risques

* *Analysis Patterns* contient plusieurs fois plus de patterns qu'il n'en a été
  catalogué ici, la plupart des modèles de négoce, de comptabilité et de mesure.
  Admettre la nature ouvre cette surface, et la tentation sera de cataloguer un
  chapitre mécaniquement — neuf sections, neuf entrées — plutôt que pattern par
  pattern face aux critères.
* Les patterns du livre se recouvrent entre eux et avec le catalogue
  d'architecture applicative d'entreprise davantage que ceux d'un catalogue
  structurel : les questions d'identité (ADR-0007) seront fréquentes plutôt
  qu'exceptionnelles. Trois se sont posées dans le seul chapitre 2.

## Actions de suivi

* Cataloguer le reste du livre chapitre par chapitre, en décidant chaque pattern
  face aux critères plutôt que par son appartenance à un chapitre.
* Trancher les trois patterns du chapitre 2 laissés indécis ici — Organization
  Hierarchies, Organization Structure et Party Type Generalizations — consignés
  dans `catalog/README.md` avec ce que chacun attend.
* Reprendre `Result` et `Option`, retenus dans `catalog/README.md` faute d'une
  publication les nommant comme patterns. Cette recherche a été faite sans accès aux
  sources ; elle mérite une seconde tentative maintenant qu'il existe.

## Références

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) —
  pourquoi ces entrées sont ici plutôt que sous Evans, et ce qui rend la référence
  porteuse.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) — le
  critère qui admet un pattern et celui qui décide que deux n'en font qu'un.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) — l'autre critère
  d'admission, celui sur lequel se jouent trois patterns du chapitre 2.
* [ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.fr.md) et
  [ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.fr.md) —
  les deux questions de nature antérieures, tranchées de la même façon.
* `catalog/README.md` — là où Knowledge Level était consigné en attente, et là où
  les patterns indécis du chapitre 2 sont désormais consignés.
