# ADR-0007 | Décider que deux patterns sont le même par les assertions qu'ils portent

🌍 🇬🇧 [English](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Savoir si deux entrées sont un seul pattern, deux patterns apparentés, ou deux
patterns sans rapport qui partagent un nom décide de la façon dont elles sont
cataloguées (ADR-0006), de la façon dont elles sont reliées dans le code
(ADR-0005), et du fait qu'un consommateur les compte une ou deux fois (ADR-0005).
C'est la question dont tout le reste dépend, et elle se repose pour chaque
catalogue ajouté.

Les noms y répondent mal dans les deux sens. *Adapter* et *Command* nomment chacun
deux patterns qui n'ont rien en commun : des noms identiques n'impliquent donc pas
l'identité. *Null Object* et *Special Case* nomment des patterns étroitement
apparentés : des noms différents n'impliquent donc pas la différence.

Un audit par les seuls noms, sur les catalogues prévus, a produit neuf doublons
apparents. Examinés un par un, sept n'étaient pas des doublons du tout — le nom
avait constitué toute la preuve.

La bibliothèque existe pour qu'une annotation puisse être vérifiée. La valeur d'un
rôle est l'assertion qu'il permet d'énoncer et à un outil de vérifier ; un rôle
sur lequel rien ne peut être affirmé ne porte aucune information qu'un lecteur
n'avait déjà.

Value Object se lit comme un seul pattern tenu par deux ouvrages. Celui de Fowler
porte sur la comparaison — une égalité qui ne repose pas sur l'identité — et
tolère un intervalle de dates mutable. Celui d'Evans y ajoute l'immuabilité et,
surtout, est une décision de modélisation : il existe parce qu'il dit quelque
chose du domaine. Une règle écrite pour l'un ne tient pas pour l'autre, ce qui a
été démontré plutôt qu'argumenté : un intervalle de dates mutable passe la règle
de Fowler et échoue à celle d'Evans.

## Décision

Deux entrées sont le même pattern lorsqu'elles portent les mêmes assertions
vérifiables, et ni le nom ni la description informelle ne le tranchent.

## Justification

Le critère est la seule chose qui ne varie pas avec le vocabulaire. Deux auteurs
décrivant une même idée avec des mots différents portent les mêmes assertions ;
deux auteurs employant un même mot pour des idées différentes n'y parviennent pas,
et demander ce qui pourrait être vérifié les sépare là où la lecture de la prose
n'y arrive pas.

Il répond à la question dans les termes qui sont la raison d'être de la
bibliothèque. Un pattern existe ici pour que quelque chose puisse être affirmé
d'un participant : deux patterns sont donc le même exactement lorsqu'ils
autorisent les mêmes assertions — le critère n'est pas un substitut de l'identité,
il est ce que l'identité signifie dans ce catalogue.

Il est testable plutôt qu'éditorial. Le cas de Value Object a été tranché en
écrivant les deux règles et en les exécutant : l'intervalle de dates mutable qui
passe l'une et échoue à l'autre est une preuve, là où la comparaison de deux
définitions en prose aurait été une opinion. Un contributeur qui doute que deux
entrées n'en fassent qu'une peut procéder de même.

Il passe à l'échelle de catalogues que personne ici n'a lus de près. Juger de
l'identité à la familiarité ne survit pas à Enterprise Integration Patterns ni à
la littérature sur la concurrence ; demander quelle règle chaque entrée
autoriserait est une question à laquelle un contributeur peut répondre depuis le
texte source sans être aguerri au domaine.

Il tranche aussi laquelle des deux relations de l'ADR-0005 s'applique, et le fait
avant que l'ordre de publication ne soit consulté. Value Object se lit comme un
seul pattern tenu par deux ouvrages jusqu'à ce que les assertions soient écrites ;
une fois qu'elles le sont, ce sont deux patterns dans une inclusion, et la
question de savoir quel ouvrage a publié le premier ne se pose jamais —
l'inclusion s'ordonne d'elle-même.

## Alternatives envisagées

### Tenir des noms identiques pour des patterns identiques

Envisagé parce que cela n'exige aucun jugement, et que c'est ce que ferait une
passe automatisée.

Rejeté au vu des deux Adapters et des deux Commands : cela fusionne en silence des
patterns qui ne partagent rien, et un consommateur les compterait ensemble sans
qu'aucun signe n'indique quoi que ce soit d'anormal.

### Comparer en prose les définitions des auteurs

Envisagé parce que c'est ce que les sources offrent réellement, et que c'est ainsi
qu'une personne lit un catalogue.

Rejeté parce que la prose issue de décennies et de communautés différentes décrit
une même idée dans des langages incompatibles et des idées différentes dans un
langage identique. Value Object se lisait comme le même pattern dans les deux
livres jusqu'à ce que les règles soient écrites.

### Demander si les praticiens les considèrent comme le même

Envisagé parce que l'adoption est ce que sert le vocabulaire.

Rejeté parce que cela varie selon les communautés et les décennies — la réponse
demanderait donc à être revue — et parce que c'est sans réponse pour les parties
du catalogue qui comptent peu de praticiens.

## Conséquences

### Positives

* Les homonymes restent séparés et les synonymes se rejoignent, pour une raison
  qui peut être énoncée.
* La question peut être tranchée par la preuve plutôt que par l'ancienneté.
* Le critère est le même que celui qui décide si un pattern a sa place dans le
  vocabulaire : un seul jugement sert les deux.

### Négatives

* Il exige davantage d'un contributeur que la lecture d'une définition : il lui
  faut imaginer la règle que chaque entrée autoriserait.
* Les entrées dont les assertions sont difficiles à cerner sont difficiles à
  classer, et le critère n'offre aucun raccourci pour elles.

### Risques

* Une règle peut être imaginée étroitement ou généreusement : deux contributeurs
  peuvent donc aboutir à des réponses différentes sur une même paire. Atténué en
  écrivant les règles dans la pull request plutôt qu'en y affirmant la conclusion.
* Appliquer le critère à des catalogues déjà publiés peut reclasser des entrées et
  déplacer une identité canonique, ce qui est une rupture de compatibilité pour
  les consommateurs qui regroupent dessus.

## Actions de suivi

* Appliquer le critère aux doublons apparents identifiés dans les catalogues
  prévus avant que ces catalogues ne soient générés, plutôt qu'après.

## Références

* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) —
  ce que l'identité décide pour un consommateur, et les deux relations entre
  lesquelles ce critère choisit.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — le même critère,
  appliqué à la question de savoir si un pattern a sa place ici.
