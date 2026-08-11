# ADR-0019 | Arrêter la remontée d'identité à la frontière du pattern

🌍 🇬🇧 [English](0019-stop-the-identity-climb-at-the-pattern-boundary.md) · 🇫🇷 Français (ce fichier)

**Statut :** Remplacé par l'[ADR-0027](0027-ship-one-independent-package-per-catalogued-work.fr.md)
**Proposé :** 2026-08-05
**Accepté :** 2026-08-06
**Décideurs :** Reefact

## Contexte

L'[ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.fr.md)
énonce que le pattern auquel appartient une annotation est le type atteint en
remontant à travers les bases abstraites et les déclinaisons, en s'arrêtant à
toute autre chose. Tous les patterns du catalogue satisfont aujourd'hui cette
règle, et le lecteur de référence l'implémente.

La règle a été écrite face aux formes qui existaient. Un pattern à plusieurs rôles
est un conteneur portant une base de rôle abstraite et un attribut par rôle ; un
pattern à rôle unique est un attribut plat. La seule relation du catalogue —
l'objet-valeur d'Evans restreignant celui de Fowler — lie deux attributs plats, où
la base de l'attribut dérivé est concrète et où la remontée s'arrête donc d'elle-même.

Un pattern multi-rôles engagé dans une relation n'a pas cette forme. Ses rôles
doivent répondre une seule identité, ce à quoi sert la base de rôle abstraite :
c'est donc la base de rôle du conteneur dérivé qui hérite — `Derived.Role :
Base.Role`, toutes deux abstraites. Remonter à travers les bases abstraites
traverse alors les deux et rapporte le pattern dérivé comme étant celui dont il
dérive. Mesuré sur deux entrées de sonde déclinées et spécialisées depuis
Composite : le lecteur a compté 37 patterns là où 38 étaient annotés, la
spécialisation ayant été absorbée.

Le générateur refuse le cas plutôt que de l'émettre, pour les deux relations, et
échoue sur *a declension or specialisation of a multi-role pattern is not
generated yet*. L'ADR-0005 n'avait anticipé cela que pour une déclinaison.

Deux documents livrés avec la bibliothèque contredisaient la décision acceptée. La
règle d'identité publiée sur `DesignPatternAttribute` disait *le type
immédiatement sous `DesignPatternAttribute`* — soit l'alternative que
l'ADR-0005 a envisagée puis rejetée, et qui donne la mauvaise réponse pour la
seule relation que porte le catalogue. `DeclensionAttribute` illustrait une
déclinaison par les objets-valeurs de Fowler et d'Evans, dont
l'[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) a
tranché qu'ils sont deux patterns dans une inclusion, et que le catalogue déclare
comme une spécialisation.

Les catalogues à venir regorgent de patterns multi-rôles que d'autres ouvrages
nomment autrement : le cas est ordinaire et non exotique.

## Décision

L'identité d'un pattern est le type atteint en remontant à travers une base
abstraite déclarée dans le même pattern et à travers une déclinaison, et un
pattern à plusieurs rôles se décline rôle par rôle tandis qu'une spécialisation
dérive pattern par pattern.

## Justification

La frontière entre deux patterns est ce que la remontée doit reconnaître, et une
base abstraite ne la marque pas. Ce que l'ADR-0005 entendait par *base abstraite*
a toujours été la base de rôle du pattern qu'on lit — ce qui fait que tous les
rôles d'un pattern répondent un seul type. Que la règle fonctionne tenait à
l'accident qu'aucune base abstraite n'avait jamais de base abstraite ; la première
relation multi-rôles supprime l'accident. Comparer les types déclarants nomme la
frontière directement, et laisse inchangée toutes les réponses existantes.

Une déclinaison franchit toujours la frontière, et désormais visiblement pour la
raison qui a toujours été la sienne : parce qu'elle est marquée, non parce qu'elle
est abstraite. Les deux façons de remonter cessent d'être un mécanisme unique qui
couvrait les deux par hasard.

Décliner rôle par rôle empêche une déclinaison de rien redire. Chaque rôle dérive
de son homologue : il en hérite les cibles, la multiplicité et les liens ; le
conteneur n'est qu'une graphie. Une déclinaison dont chaque rôle déclarerait ses
propres cibles serait deux énoncés de l'applicabilité d'un même pattern, libres de
diverger — ce pour quoi l'ADR-0005 a rejeté une identité canonique, et ce que
l'[ADR-0004](0004-keep-the-attribute-base-a-pure-marker.fr.md) rejette
généralement. C'est aussi ce que fait déjà une déclinaison plate : le cas
multi-rôles fait désormais la même chose plutôt qu'une autre.

Spécialiser pattern par pattern découle de ce qu'une spécialisation est un pattern
à part entière. Ses rôles sont les siens — elle peut restreindre ce à quoi ils
s'appliquent, et l'[ADR-0009](0009-let-each-role-declare-what-it-applies-to.fr.md)
existe pour qu'elle le puisse — ils ne peuvent donc pas hériter des déclarations
du pattern plus large. Hériter au niveau de la base de rôle suffit à la garantie
que demande l'ADR-0005 : une règle écrite pour le pattern plus large atteint le
plus étroit, puisque tout rôle dérivé répond encore la base de rôle plus large.

Exiger d'une déclinaison qu'elle porte les mêmes rôles, dans le même ordre, est la
même affirmation lue à l'envers. Une déclinaison affirme que deux entrées sont un
seul pattern ; deux entrées aux rôles différents ne sont pas un seul pattern, et
le générateur les refuse plutôt que d'émettre une forme qui dirait discrètement le
contraire.

Ceci remplace au lieu d'amender parce que la phrase qui change est la Décision de
l'ADR-0005. Tout le reste de ce qu'il a décidé — l'héritage comme moyen des deux
relations, le marqueur sur la déclinaison plutôt que sur la spécialisation,
l'identité comme type plutôt que comme nom, l'antériorité ordonnant une
déclinaison — est intact et n'est ici repris que par référence.

## Alternatives envisagées

### Continuer de refuser un pattern multi-rôles dans une relation

Envisagé parce que c'est l'état que l'ADR-0005 a délibérément choisi : le
générateur échoue plutôt que d'émettre quelque chose d'approximatif, et aucune
entrée du catalogue n'a besoin du cas aujourd'hui.

Rejeté parce que le refus était un report, non une position — l'ADR-0005 dit que
le premier cas exigera d'étendre la décision. Les catalogues à venir sont
majoritairement multi-rôles : le premier qui nommerait autrement un pattern du
Gang of Four arrêterait le générateur, et la réponse devrait être conçue sous la
pression du blocage.

### Arrêter la remontée après une seule base abstraite

Envisagé parce que c'est un changement plus petit que la comparaison des types
déclarants, et que cela corrige la fusion tout aussi bien : un pas au-dessus d'un
attribut de rôle atteint sa base de rôle, et s'y arrêter est juste.

Rejeté parce que c'est une règle de distance et non de sens. Elle donne par hasard
la bonne réponse pour les formes émises aujourd'hui et ne dit rien du pourquoi ;
une chaîne de déclinaisons à deux niveaux — un pattern épelé par trois catalogues
— la casserait, et rien dans la règle n'expliquerait la casse.

### Marquer la frontière de pattern par un second attribut

Envisagé parce qu'un marqueur explicite sur la base de rôle de chaque conteneur
ferait de la frontière un fait déclaré plutôt que déduit, symétrique du marqueur
de déclinaison.

Rejeté parce que la frontière est déjà déclarée : un type imbriqué dit quel type
le déclare, et c'est exactement la question posée. Un marqueur le redirait sur
chaque pattern du catalogue, et pourrait être omis — un second énoncé d'un même
fait, soit la défaillance que ce dépôt supprime partout ailleurs.

### Faire dériver une spécialisation multi-rôles rôle par rôle, comme une déclinaison

Envisagé parce qu'une seule forme servirait les deux relations, et parce qu'une
règle écrite pour la *Feuille* du pattern plus large atteindrait alors la
*Feuille* du plus étroit et non sa seule base de rôle.

Rejeté parce qu'une spécialisation n'est pas tenue de porter les mêmes rôles que
le pattern qu'elle restreint : hériter rôle par rôle l'y forcerait, ou laisserait
certains rôles reliés et d'autres non. Cela ferait aussi hériter au pattern plus
étroit des cibles qu'il existe peut-être précisément pour restreindre.

## Conséquences

### Positives

* Un pattern multi-rôles peut être décliné ou spécialisé, ce dont les catalogues à
  venir ont besoin.
* Une spécialisation reste dénombrable sous toutes ses formes, et non seulement
  quand elle est plate.
* La raison pour laquelle une remontée franchit une frontière est désormais le
  seul marqueur : les deux relations sont distinguées par un mécanisme unique
  plutôt que par deux qui se recouvrent.
* Une déclinaison ne redit rien, quelle que soit sa forme.
* La règle d'identité publiée, le lecteur de référence et la base d'ADR
  s'accordent.

### Négatives

* La règle prend une proposition de plus à énoncer, et *une base abstraite
  déclarée dans le même pattern* est lourd pour ce qui se lit, dans le code, comme
  une comparaison.
* Un consommateur qui avait implémenté la règle précédente continue de compiler et
  se met à répondre autrement le jour où une relation multi-rôles entre au
  catalogue.

### Risques

* Aucune entrée du catalogue n'exerce les nouvelles formes : toutes deux sont donc
  générées et relues en conditions de sonde plutôt qu'en CI. Le premier cas réel
  est là où elles seront prouvées — des tests de convention par réflexion, différés
  par l'[ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.fr.md),
  fermeraient ce risque.
* Une déclinaison de déclinaison est désormais exprimable et n'a pas été pensée ;
  la remontée la porterait, et rien d'autre n'a été vérifié.

## Actions de suivi

* Exercer les deux formes avec une véritable entrée de catalogue lorsque la
  première relation multi-rôles sera cataloguée, et ajouter son exemple à côté.
* Reconsidérer les tests de convention par réflexion, qui couvriraient les formes
  qu'une sonde ne peut vérifier qu'une fois.

## Références

* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.fr.md) —
  l'enregistrement que ceci remplace ; tout ce qu'il décide au-delà de la remontée
  tient toujours.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) — ce
  qui décide de la relation applicable, et ce que la documentation livrée
  contredisait.
* [ADR-0009](0009-let-each-role-declare-what-it-applies-to.fr.md) — pourquoi une
  spécialisation ne peut pas hériter des déclarations de ses rôles.
* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.fr.md) — les règles de
  lecture que la bibliothèque publie, que ceci corrige.
