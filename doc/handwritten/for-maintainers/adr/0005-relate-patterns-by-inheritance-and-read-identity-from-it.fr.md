# ADR-0005 | Relier les patterns par héritage, et y lire l'identité d'un pattern

🌍 🇬🇧 [English](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Un consommateur qui compte des patterns, dessine un diagramme ou applique une
règle doit décider quand deux annotations concernent le même pattern. Trois faits
rendent fausses les réponses évidentes.

Les noms de patterns ne sont pas uniques d'un catalogue à l'autre. *Adapter*
désigne un pattern chez le Gang of Four — la conversion d'une interface — et un
autre dans l'architecture hexagonale, une position à une frontière architecturale.
*Command* désigne un pattern qui porte sa propre exécution et un autre qui est un
message sans aucun comportement. Regrouper par nom fusionne des patterns qui n'ont
rien à voir entre eux, et le fait en silence.

Un même pattern est réparti sur plusieurs types. Chaque rôle d'un pattern
multi-rôles est son propre attribut, si bien que regrouper par type d'attribut
scinde *Composite* en autant de patterns qu'il a de rôles.

Deux patterns peuvent en outre être reliés à dessein, de deux manières qui ne
doivent pas être traitées à l'identique :

* **Le même pattern, catalogué deux fois**, sous le même nom ou sous un autre, par
  deux corpus. Le lecteur du second catalogue l'y cherche : il doit donc y exister ;
  mais les deux graphies sont un seul pattern et doivent être comptées une fois.
* **Un cas plus étroit d'un autre pattern.** Tout Null Object est un Special Case ;
  quantité de Special Cases — un client inconnu, un tarif manquant — ne sont pas
  des Null Objects. Les deux restent des patterns distincts, chacun dénombrable, et
  une règle écrite pour le plus large s'applique aussi au plus étroit.

C# n'offre qu'une construction pour les deux, et elle signifie déjà l'une d'elles :
`class B : A` dit *B est un A*, ce qui est exactement ce que dit la seconde
relation. Ce qu'elle ne peut pas dire, c'est *B est le même que A*, car deux types
ne peuvent pas n'en faire qu'un.

## Décision

Un pattern se relie à un autre par héritage — simple lorsqu'il en est un cas plus
étroit, marqué `[Declension]` lorsqu'il est le même pattern écrit deux fois — et
le pattern auquel appartient une annotation est le type atteint en remontant à
travers les bases abstraites et les déclinaisons, en s'arrêtant à toute autre
chose.

## Justification

L'héritage est le bon moyen pour les deux relations, parce que toutes deux ont
besoin que l'attribut dérivé réponde à celui dont il dérive : une déclinaison pour
que l'une ou l'autre graphie puisse être filtrée par le type de la définition, une
spécialisation pour qu'une règle écrite pour le pattern le plus large atteigne le
plus étroit sans être répétée. Les ensembles de règles se composent alors comme le
font les patterns, et c'est là le gain pratique — un objet-valeur d'Evans est
soumis à la règle d'égalité par valeur de Fowler parce que l'attribut en dérive,
et porte en plus une règle d'immuabilité qui lui est propre.

Marquer la déclinaison plutôt que la spécialisation découle de ce que l'héritage
signifie déjà. `:` dit *est un*, c'est-à-dire la spécialisation : la laisser non
marquée laisse le langage porter son propre sens. L'identité est la relation que
le système de types ne sait pas énoncer ; l'héritage y est donc un moyen et non un
énoncé, et consigner qu'il n'est qu'un moyen est tout ce que fait le marqueur.
Marquer dans l'autre sens annoterait la lecture ordinaire de `:` pour laisser
déduire l'extraordinaire.

Sans cette distinction, l'identité est indécidable, et c'est pourquoi les deux
appartiennent à une même décision. Une remontée jusqu'au sommet d'une chaîne
d'héritage fusionne une spécialisation dans le pattern qui la contient — juste
pour une déclinaison, faux pour une spécialisation — et rien dans la chaîne ne dit
laquelle des deux on remonte. Le marqueur est ce qui permet à la remontée de
s'arrêter au bon endroit ; aucune des deux moitiés ne tient sans l'autre.

L'identité est un type plutôt qu'un nom parce que les noms ne sont pas uniques et
que les types le sont. Deux homonymes déclarés dans deux catalogues sont deux
types, comparent comme différents, et ne peuvent être confondus par aucun
consommateur, si négligent soit-il.

Remonter à travers une base abstraite est ce qui rassemble les rôles d'un pattern.
La base de rôle abstraite du conteneur est la seule chose que tous les rôles ont
en commun : c'est donc elle que tout rôle répond, sans que rien n'ait à être
énoncé rôle par rôle — ce qui aurait été un endroit où deux rôles auraient pu se
contredire.

Rien n'est écrit pour tout cela. Qu'une base soit abstraite et qu'un type soit
marqué sont deux faits déjà présents : l'identité se lit donc dans la déclaration
comme le reste (ADR-0004) et ne peut pas être énoncée de travers. Le marqueur est
la seule chose que la bibliothèque porte au-delà de la base vide, et il mérite
cette place en étant irrécupérable : rien d'autre dans le graphe de types ne
distingue les deux relations.

Le sens de chaque relation est fixé par une règle différente, parce que ce sont
des questions différentes. Deux graphies d'un même pattern disent la même chose :
rien dans leur sens ne peut donc les ordonner, et c'est la publication la plus
ancienne qui détient la définition — ce qui rend la référence porteuse (ADR-0006).
L'inclusion, elle, s'ordonne d'elle-même : le plus étroit dérive du plus large,
quelles que soient les dates, puisqu'être publié plus tôt ne rend pas un pattern
plus large.

## Alternatives envisagées

### Regrouper par le nom du pattern

Envisagé parce que c'est ce que tout le monde essaie en premier et que cela se lit
bien.

Rejeté parce que cela fusionne silencieusement les deux Adapters et les deux
Commands. Un regroupement faux sans échouer est pire qu'un regroupement peu
commode.

### Porter une identité canonique sous forme de paire catalogue-et-nom

Envisagé parce que c'est lisible, imprimable, sérialisable et indépendant du
graphe de types — cela survivrait donc à une future séparation des catalogues en
paquets distincts.

Rejeté parce que cela doit être énoncé, donc peut être mal énoncé, et parce que
chaque rôle d'un pattern devrait le répéter — une occasion de diverger par rôle.
L'argument d'empaquetage se dissout d'ailleurs dès lors que les relations sont
exprimées par héritage, qui est un couplage plus fort qu'une référence de type.

### Remonter jusqu'au type immédiatement sous la base, quel qu'il soit

Envisagé parce que c'est une règle unique sans exception, et qu'elle n'exige rien
de déclaré nulle part — pas de marqueur, donc aucune distinction à consigner.

Rejeté parce qu'elle remonte au-delà d'une spécialisation : un Null Object cesse
d'être dénombrable et est rapporté comme un Special Case. Fusionner est juste pour
une déclinaison et faux pour une spécialisation ; une seule règle ne peut pas
servir les deux.

### Dupliquer le pattern dans les deux catalogues, à l'identique

Envisagé parce que le code est généré : le lecteur de l'un ou l'autre catalogue y
trouverait un pattern complet là où il regarde, sans coût de maintenance.

Rejeté parce que les deux copies seraient des types sans relation : rien ne les
lierait pour un consommateur, et une règle écrite pour l'une n'atteindrait pas
l'autre. L'héritage donne la même découvrabilité en quatre lignes et conserve le
lien.

### Marquer plutôt la spécialisation

Envisagé parce que cela laisserait la déclinaison en défaut non marqué, et que les
déclinaisons pourraient bien finir plus nombreuses.

Rejeté parce que l'héritage signifie déjà *est un* : marquer une spécialisation
annote ce que le langage dit tout seul, et laisse déduire le cas que le langage ne
sait pas dire.

### Porter la relation comme une liste de noms alternatifs sur l'attribut

Envisagé parce que cela n'exige aucun second type.

Rejeté parce qu'une chaîne dans une liste n'est découvrable par personne. Le
lecteur de l'autre catalogue cherche un type par son nom dans un éditeur et ne
trouve rien ; un attribut qui dérive de la définition se trouve là où on le
cherche, est vérifié par le compilateur et répond la bonne identité.

### Énoncer la relation dans la seule documentation

Envisagé parce que cela n'exige rien du tout dans la bibliothèque.

Rejeté parce que la remontée a besoin de la distinction pour calculer une réponse
juste, et qu'on ne consulte pas de la prose à l'exécution.

## Conséquences

### Positives

* Deux patterns qui partagent un nom ne sont jamais confondus.
* Tous les rôles d'un pattern répondent la même identité, sans rien d'énoncé rôle
  par rôle.
* Une spécialisation reste un pattern à part entière tout en répondant au pattern
  plus large dont elle dérive.
* Les règles se composent le long de la même hiérarchie que les patterns : une
  règle pour un pattern plus large s'écrit une fois.
* L'une ou l'autre graphie d'un pattern décliné se trouve là où un lecteur la
  cherche.
* Rien n'a à être rédigé pour l'identité, rien ne peut donc être rédigé de travers.

### Négatives

* La remontée n'est pas évidente, et un consommateur qui ne la lit pas regroupera
  par nom et se trompera sans bruit.
* L'identité d'un pattern multi-rôles est la base de rôle abstraite du conteneur
  alors que celle d'un pattern à rôle unique est son propre type d'attribut : les
  deux ne sont donc pas des types de même nature — inoffensif comme clé opaque,
  visible dès qu'on l'affiche.
* L'attribut dont on dérive ne peut pas être scellé : le générateur descelle
  exactement ceux-là et aucun autre — une différence entre fichiers générés
  expliquée par la donnée plutôt que lisible dans le fichier.
* Un filtre typé est asymétrique : filtrer sur la définition attrape la
  déclinaison, non l'inverse. Inoffensif seulement parce que la remontée est
  l'instrument correct.

### Risques

* La remontée dépend de l'existence de la base de rôle abstraite dans tout pattern
  multi-rôles. Elle est émise par le gabarit : un changement à cet endroit
  changerait silencieusement l'identité sur tout le catalogue.
* Une relation peut être consignée à tort — une spécialisation déclarée comme
  déclinaison fusionne deux patterns qui auraient dû rester distincts. Le marqueur
  est une affirmation, et une affirmation fausse est une décision fausse, pas une
  règle cassée.
* La déclinaison d'un pattern multi-rôles n'est pas générée, et le générateur
  échoue plutôt que d'émettre quelque chose d'approximatif. Aucun cas n'existe
  encore ; le premier exigera d'étendre cette décision.

## Actions de suivi

* Garder exercée l'implémentation de la remontée dans le lecteur d'exemple,
  puisqu'elle est l'énoncé exécutable de cette règle.
* Étendre le générateur lorsqu'apparaîtra la première déclinaison d'un pattern
  multi-rôles.

## Références

* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — pourquoi la remontée
  est documentée plutôt qu'implémentée dans la bibliothèque.
* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) —
  l'antériorité qui ordonne une déclinaison.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) —
  comment se décide laquelle des deux relations s'applique.
