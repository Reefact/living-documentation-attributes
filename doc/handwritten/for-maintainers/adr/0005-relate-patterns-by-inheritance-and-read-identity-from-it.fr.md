# ADR-0005 | Relier les patterns par héritage, et y lire l'identité d'un pattern

🌍 🇬🇧 [English](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Un consommateur qui compte des patterns, dessine un diagramme ou applique une
règle doit décider quand deux annotations concernent le même pattern. Trois faits
rendent fausses les réponses évidentes.

Les noms de patterns ne sont pas uniques entre catalogues. *Adapter* nomme un
pattern chez le Gang of Four — convertir une interface — et un autre dans
l'architecture hexagonale, une position à une frontière architecturale. *Command*
nomme un pattern qui porte sa propre exécution et un autre qui est un message
dépourvu de tout comportement. Regrouper par nom fusionne des patterns qui n'ont
rien à voir entre eux, et le fait silencieusement.

Un pattern unique est réparti sur plusieurs types. Chaque rôle d'un pattern
multi-rôles est son propre attribut : regrouper par type d'attribut scinde donc
*Composite* en autant de patterns qu'il a de rôles.

Deux patterns peuvent en outre être reliés à dessein, de deux façons qu'il ne
faut pas traiter pareillement :

* **Le même pattern, catalogué deux fois**, sous le même nom ou sous un autre,
  par deux corpus. Un lecteur du second catalogue l'y cherche, il doit donc y
  exister ; mais les deux graphies sont un seul pattern et doivent être comptées
  une fois.
* **Un cas plus étroit d'un autre pattern.** Tout Null Object est un Special
  Case ; quantité de Special Cases — un client inconnu, un taux manquant — ne
  sont pas des Null Objects. Les deux restent des patterns distincts, chacun
  comptable, et une règle écrite pour le plus large s'applique aussi au plus
  étroit.

C# offre une seule construction pour les deux, et elle signifie déjà l'une
d'elles : `class B : A` dit *B est un A*, ce qui est exactement ce que dit la
seconde relation. Ce qu'elle ne peut pas dire, c'est *B est le même que A*, car
deux types ne peuvent pas n'en faire qu'un.

## Décision

Un pattern se relie à un autre par héritage — simple lorsqu'il en est un cas plus
étroit, marqué `[Declension]` lorsqu'il est le même pattern écrit deux fois — et
le pattern auquel appartient une annotation est le type atteint en remontant à
travers les bases abstraites et les déclinaisons, en s'arrêtant à toute autre
chose.

## Justification

L'héritage est le bon moyen pour les deux relations parce que toutes deux
exigent que l'attribut dérivé réponde à celui dont il dérive : une déclinaison
pour que l'une ou l'autre graphie puisse être filtrée par le type de la
définition, une spécialisation pour qu'une règle écrite pour le pattern large
atteigne le plus étroit sans être répétée. Les jeux de règles se composent alors
comme les patterns le font, et c'est là le gain pratique — un value object
d'Evans est soumis à la règle d'égalité par valeur de Fowler parce que l'attribut
dérive, et porte par-dessus une règle d'immuabilité qui lui est propre.

Marquer la déclinaison plutôt que la spécialisation découle de ce que l'héritage
signifie déjà. `:` dit *est un*, ce qui est la spécialisation : la laisser non
marquée laisse le langage porter son propre sens. L'identité, elle, est la
relation que le système de types ne peut pas énoncer ; l'héritage y est un moyen
plutôt qu'un énoncé, et enregistrer qu'il est un moyen est tout ce que fait le
marqueur. Marquer dans l'autre sens annoterait la lecture ordinaire de `:` pour
laisser inférer l'extraordinaire.

Sans cette distinction, l'identité est indécidable, et c'est pourquoi les deux
tiennent dans une seule décision. Une remontée jusqu'au sommet d'une chaîne
d'héritage fond une spécialisation dans le pattern qui la contient — juste pour
une déclinaison, faux pour une spécialisation — et rien dans la chaîne ne dit
laquelle on remonte. Le marqueur est ce qui permet à la remontée de s'arrêter au
bon endroit : aucune des deux moitiés ne tient sans l'autre.

L'identité est un type plutôt qu'un nom parce que les noms ne sont pas uniques et
que les types le sont. Deux homonymes déclarés dans deux catalogues sont deux
types, se comparent comme différents, et ne peuvent être confondus par aucun
consommateur, si négligent soit-il.

Remonter à travers une base abstraite est ce qui rassemble les rôles d'un même
pattern. La base de rôle abstraite du conteneur est la seule chose que tous les
rôles ont en commun : c'est donc ce à quoi tout rôle répond, sans que rien n'ait
à être énoncé rôle par rôle — ce qui aurait été un endroit où deux rôles peuvent
diverger.

Rien n'est écrit pour tout cela. Qu'une base soit abstraite et qu'un type soit
marqué sont deux faits déjà présents : l'identité se lit dans la déclaration
comme le reste (ADR-0004) et ne peut pas être énoncée de travers. Le marqueur est
la seule chose que la librairie porte au-delà de la base vide, et il gagne cette
place en étant irrécupérable : rien d'autre dans le graphe de types ne distingue
les deux relations.

Le sens de chaque relation est réglé par une règle différente, parce que ce sont
des questions différentes. Deux graphies d'un même pattern disent la même chose :
rien dans leur sens ne peut les ordonner, et c'est la publication la plus
ancienne qui détient la définition — ce qui rend la référence porteuse
(ADR-0006). L'inclusion, elle, s'ordonne d'elle-même : le plus étroit dérive du
plus large, quelles que soient les dates, puisqu'être publié plus tôt ne rend pas
un pattern plus large.

## Alternatives envisagées

### Regrouper par nom de pattern

Envisagé parce que c'est ce que tout le monde essaie d'abord, et que cela se lit
bien.

Rejeté parce que cela fusionne silencieusement les deux Adapters et les deux
Commands. Un regroupement faux sans échouer est pire qu'un regroupement peu
commode.

### Porter une identité canonique en couple catalogue-et-nom

Envisagé parce que c'est lisible, imprimable, sérialisable et indépendant du
graphe de types — cela survivrait donc à un futur découpage des catalogues en
packages séparés.

Rejeté parce que cela doit être énoncé, donc peut être mal énoncé, et parce que
chaque rôle d'un pattern devrait le répéter — une occasion de diverger par rôle.
L'argument du packaging se dissout d'ailleurs dès lors que les relations sont
exprimées par héritage, qui est un couplage plus fort qu'une référence de type.

### Remonter jusqu'au type immédiatement sous la base, quel qu'il soit

Envisagé parce que c'est une règle unique sans exception, et qu'elle n'exige rien
de déclaré nulle part — aucun marqueur, donc aucune distinction à enregistrer.

Rejeté parce qu'elle remonte au-delà d'une spécialisation : un Null Object cesse
d'être comptable et est rapporté comme un Special Case. Fusionner est juste pour
une déclinaison et faux pour une spécialisation ; une seule règle ne peut pas
servir les deux.

### Dupliquer le pattern à l'identique dans les deux catalogues

Envisagé parce que le code est généré : un lecteur de l'un ou l'autre catalogue
trouverait un pattern complet là où il regarde, sans coût de maintenance.

Rejeté parce que les deux copies seraient des types sans lien : rien ne les
rattacherait l'une à l'autre pour un consommateur, et une règle écrite pour l'une
n'atteindrait pas l'autre. L'héritage donne la même découvrabilité en quatre
lignes et conserve le lien.

### Marquer plutôt la spécialisation

Envisagé parce que cela laisserait la déclinaison en défaut non marqué, et que
les déclinaisons finiront peut-être par être les plus nombreuses.

Rejeté parce que l'héritage signifie déjà *est un* : marquer une spécialisation
annote ce que le langage dit tout seul, et laisse inférer le cas que le langage
ne peut pas dire.

### Porter la relation comme une liste de noms alternatifs sur l'attribut

Envisagé parce que cela n'exige aucun second type.

Rejeté parce qu'une chaîne dans une liste n'est découvrable par personne. Un
lecteur de l'autre catalogue cherche un type par son nom dans un éditeur, et ne
trouve rien ; un attribut qui dérive de la définition est trouvé là où on le
cherche, vérifié par le compilateur, et répond la bonne identité.

### N'énoncer la relation que dans la documentation

Envisagé parce que cela n'exige rien du tout dans la librairie.

Rejeté parce que la remontée a besoin de la distinction pour calculer une réponse
juste, et qu'une prose ne se consulte pas à l'exécution.

## Conséquences

### Positives

* Deux patterns qui partagent un nom ne sont jamais confondus.
* Tous les rôles d'un pattern répondent la même identité, sans rien d'énoncé rôle
  par rôle.
* Une spécialisation reste un pattern à part entière tout en répondant au pattern
  plus large dont elle dérive.
* Les règles se composent le long de la même hiérarchie que les patterns : une
  règle pour un pattern large s'écrit une fois.
* L'une ou l'autre graphie d'un pattern décliné se trouve là où un lecteur la
  cherche.
* Rien n'a à être écrit pour l'identité, donc rien ne peut être écrit de travers.

### Négatives

* La remontée n'est pas évidente, et un consommateur qui ne la lit pas regroupera
  par nom et se trompera sans bruit.
* L'identité d'un pattern multi-rôles est la base de rôle abstraite du conteneur
  quand celle d'un pattern à rôle unique est son propre type d'attribut : les
  deux ne sont donc pas des types de même nature — sans conséquence comme clé
  opaque, visible dès qu'on l'affiche.
* L'attribut dont on dérive ne peut pas être `sealed` : le générateur descelle
  exactement ceux-là et aucun autre — une différence entre fichiers générés
  expliquée par la donnée plutôt que lisible dans le fichier.
* Un filtre typé est asymétrique : filtrer sur la définition attrape la
  déclinaison, l'inverse n'est pas vrai. Sans conséquence uniquement parce que la
  remontée est l'instrument correct.

### Risques

* La remontée dépend de l'existence de la base de rôle abstraite dans chaque
  pattern multi-rôles. Elle est émise par le gabarit : un changement à cet
  endroit changerait silencieusement l'identité sur tout le catalogue.
* Une relation peut être enregistrée de travers — une spécialisation déclarée
  comme déclinaison fusionne deux patterns qui devaient rester distincts. Le
  marqueur est une affirmation, et une affirmation fausse est une décision
  fausse, pas une règle cassée.
* Une déclinaison d'un pattern multi-rôles n'est pas générée, et le générateur
  échoue plutôt que d'émettre quelque chose d'approximatif. Aucun cas n'existe
  encore ; le premier exigera d'étendre cette décision.

## Actions de suivi

* Garder exercée l'implémentation de la remontée dans le lecteur d'exemple,
  puisqu'elle est l'énoncé exécutable de cette règle.
* Étendre le générateur à l'apparition de la première déclinaison d'un pattern
  multi-rôles.

## Références

* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.fr.md) — pourquoi la
  remontée est documentée plutôt qu'implémentée dans la librairie.
* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) —
  l'antériorité qui ordonne une déclinaison.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) —
  comment se décide laquelle des deux relations s'applique.
