# ADR-0030 | Décider si Enterprise Integration Patterns porte les relations de son livre

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0030-decide-whether-enterprise-integration-carries-its-books-relations.md)

**Statut :** Proposé
**Proposé :** 2026-08-10
**Décideurs :** Reefact

## Contexte

Une entrée du catalogue peut déclarer `specialisationOf` et nommer un autre pattern du
même catalogue qu'elle restreint. La relation est émise sous forme d'héritage, et la
documentation générée dit ce qu'elle signifie : *tout participant annoté ici en est aussi
un de ceux-là, et un consommateur qui demande le pattern plus large obtient ceux-ci en
plus*.

Dix relations existent aujourd'hui, dans trois catalogues. Elles prennent deux formes :

* quatre visent un pattern à **un seul rôle**, et font dériver un attribut d'un attribut —
  `PostAttribute : PartyAttribute`, `RowDataGatewayAttribute : GatewayAttribute` ;
* six visent un pattern à **plusieurs rôles**, et dérivent de la base abstraite que ses
  rôles partagent — `HierarchicAccountabilityAttribute : Accountability.Role`,
  `SecondaryPostingRuleAttribute : PostingRule.Role`.

Les deux formes sont livrées et correctes. La seconde dit « un participant du pattern plus
large » sans nommer lequel, ce qui est précisément le sens d'une restriction portant sur le
pattern entier.

`EnterpriseIntegration` contient désormais les **65** patterns de son livre et **aucune
relation**. Ce n'est pas parce que le livre n'en énonce aucune. Sa structure en est
largement l'énoncé :

* le chapitre *Message Routing* présente douze patterns comme des sortes de
  `MessageRouter`, le pattern de base donné au chapitre 3 ;
* le chapitre *Message Transformation* en présente six comme des sortes de
  `MessageTranslator`, de même ;
* le chapitre *Messaging Channels* présente ses canaux comme des sortes de
  `MessageChannel` ;
* les consommateurs du chapitre *Messaging Endpoints* sont des sortes de
  `MessageEndpoint` ;
* et certaines entrées le disent explicitement — le livre affirme qu'un **Wire Tap** *est*
  une `RecipientList` figée à deux canaux de sortie, et présente **Command**, **Document**
  et **Event Message** comme trois sortes de `Message`.

Une trentaine d'entrées sont concernées. Aucune ne porte de relation, et le `README.md` du
catalogue donnait jusqu'ici, pour deux d'entre elles, une raison — que la relation
affirmerait quelque chose que le livre ne dit pas — que les six relations livrées de même
forme démontrent fausse. Elle sous-spécifie ; elle n'affirme rien de faux. Ce paragraphe
est corrigé dans le même changement que celui qui propose cet ADR.

L'absence n'est donc pas une décision prise et consignée. C'est une décision jamais prise.

## Décision

Enterprise Integration Patterns est catalogué sans relation `specialisationOf`, et les
restrictions que son livre énonce sont consignées en prose dans `catalog/README.md`,
jusqu'à ce qu'un mainteneur en décide autrement.

## Justification

L'absence doit être une décision plutôt qu'un oubli, et cet ADR existe d'abord pour qu'elle
en devienne une. Entre consigner l'état actuel et rétro-ajouter trente relations, la
première est ce qu'une proposition doit porter : la seconde modifie le graphe d'héritage
d'un paquet livré sur la foi d'une lecture d'une table des matières, et cela relève du
mainteneur et non d'un agent.

Il existe un vrai argument pour l'état actuel, au-delà de la prudence. Une relation est
émise en héritage, et l'héritage dans un vocabulaire est une promesse faite à tout
consommateur futur : dès que `ContentBasedRouterAttribute` dérive de
`MessageRouterAttribute`, une règle qui demande les routeurs change silencieusement de
sens, et un codebase qui aurait annoté les deux — plusieurs le feront, puisque ce sont deux
affirmations qu'un relecteur peut vouloir toutes les deux — se met à compter double. Les
catalogues qui portent des relations en portent dix, choisies une par une ; trente d'un
coup, dérivées d'une structure de chapitres plutôt que de ce que chaque entrée affirme, est
un acte d'une autre nature, et le test de
[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — *les affirmations
portées, non les noms ni le voisinage* — est exactement celui qu'un titre de chapitre ne
passe pas.

En sens inverse : un lecteur du livre s'attendra à ce qu'un routeur par contenu réponde
comme un routeur de messages, et aujourd'hui il ne le fait pas. La prose du README du
catalogue lui dit pourquoi, mais la prose n'est pas ce que lit une règle.

## Alternatives envisagées

### Relier ce qu'énonce la structure des chapitres

Ajouter `specialisationOf` aux patterns de routage, à ceux de transformation, aux canaux et
aux consommateurs — une trentaine d'entrées — pour que la hiérarchie émise corresponde à
celle du livre.

Le meilleur argument pour : c'est un vocabulaire fait pour dire à quoi un codebase
participe, et une œuvre qui organise ses patterns en familles a dit quelque chose qu'un
lecteur voudra interroger. Écartée ici seulement parce que c'est la plus grande et la moins
réversible des deux — des relations peuvent être ajoutées plus tard sans rien changer de ce
qui est déjà émis, alors qu'en retirer une casse celui qui a écrit une règle dessus.

### Ne relier que les entrées dont le texte le dit explicitement

Wire Tap *est* une recipient list ; les trois intentions de message *sont* des sortes de
message. Quatre entrées, chacune avec une phrase du livre à montrer, sans recours à la
structure des chapitres.

C'est l'alternative la plus probablement juste, et ce n'est délibérément pas la décision :
c'est un milieu cohérent qu'un mainteneur doit choisir en connaissance de cause plutôt
qu'hériter de ce qu'un agent a trouvé commode un mardi. Si elle est retenue, elle remplace
cet ADR au lieu de l'amender.

### Laisser `specialisationOf` nommer un rôle plutôt qu'un pattern

`{"catalog": …, "name": "Message", "role": "Message"}`, émis en
`CommandMessageAttribute : Message.MessageAttribute`, plus précis que dériver de
`Message.Role`. Le générateur porte un point d'accroche inutilisé dont ce serait le besoin
— un ensemble de rôles à émettre non scellés, lu au moment de choisir le modificateur de
chaque rôle et jamais alimenté.

Écartée comme prématurée : elle ajoute une seconde forme d'émission au seul mécanisme de
relation du vocabulaire, et l'imprécision qu'elle corrige n'a encore rien coûté à personne.
À reconsidérer seulement si l'alternative précédente est retenue et que la perte de
précision se fait alors sentir.

## Conséquences

### Positives

* La seule affirmation du catalogue sur ses propres relations cesse d'être une raison
  fausse pour devenir une décision consignée, alternatives écrites.
* Rien de ce qui est déjà émis ne change, et toutes les alternatives restent ouvertes au
  même coût qu'aujourd'hui.

### Négatives

* Une règle écrite pour `MessageRouter` n'atteint pas les douze routeurs du chapitre 7, et
  rien dans le paquet ne dit pourquoi. La raison vit dans cet ADR et dans
  `catalog/README.md`.
* `EnterpriseIntegration` est le plus gros catalogue et le seul sans aucune relation, ce
  qui se lit comme un oubli tant que cet ADR n'est pas trouvé.

### Risques

* Décider plus tard est bon marché pour le code et pas pour les lecteurs : un consommateur
  qui a écrit sa propre règle « est un routeur » contre le graphe actuel l'aura écrite en
  énumérant douze attributs, et ajouter les relations n'enlève pas cette énumération.

## Actions de suivi

* Trancher entre l'état actuel, les quatre cas explicites et la structure complète des
  chapitres — et remplacer cet ADR par celui qui est retenu.
* Décider séparément si le point d'accroche inutilisé du générateur est du code mort à
  retirer ou une couture à garder ; il est aujourd'hui lu et jamais alimenté, ce qui n'est
  ni l'un ni l'autre.

## Références

* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — l'identité se
  décide par les affirmations portées.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) — aucune relation ne
  traverse un catalogue, la question est donc interne à une œuvre.
* [ADR-0029](0029-admit-enterprise-integration-patterns-as-a-catalogue.md) — l'admission de
  l'œuvre.
* `catalog/README.md` — où sont écrites les restrictions qu'énonce le livre.
