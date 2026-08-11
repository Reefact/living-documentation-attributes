# ADR-0004 | Garder la base des attributs comme pur marqueur

🌍 🇬🇧 [English](0004-keep-the-attribute-base-a-pure-marker.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

La librairie ne livre aucun consommateur. Qui veut un inventaire, un diagramme ou
une règle d'architecture l'écrit lui-même, ce qui fait de ce qu'une annotation
expose à un lecteur la totalité du contrat public.

Trois choses que veut un lecteur — le catalogue d'où vient une annotation, le
pattern, le rôle — sont déjà dites par la déclaration elle-même. Le catalogue est
l'espace de noms, le pattern est le type conteneur, le rôle est le nom propre de
l'attribut. Aucune n'est un fait distinct.

Deux façons de les exposer ont été mesurées. Les énoncer comme constantes sur
chaque attribut généré revient à écrire chaque nom deux fois : une fois comme
déclaration, une fois comme littéral qui doit s'accorder avec elle. Ramené à un
catalogue de cent quatre-vingt-dix patterns et sept cent soixante rôles, cela
double l'assembly, de 51,5 Ko à 104 Ko — un chiffre faible dans l'absolu, et
entièrement redondant. Les relire dans des accesseurs de propriétés supprime la
duplication, au prix de mettre de la réflexion, et une convention sur
l'agencement interne de la librairie, à l'intérieur d'un marqueur.

Un consommateur détient déjà le type de l'attribut : `GetCustomAttributes` est la
façon dont il a obtenu l'annotation. Rien de ce qu'on lui tendrait n'est autre
chose que ce qu'il a déjà à un appel de distance.

Trois des quatre règles de lecture ne sont pas évidentes, et l'une est un piège :
le catalogue est le **premier** segment d'espace de noms sous la racine, pas le
dernier, afin qu'un sous-espace d'organisation se replie dans le catalogue auquel
il appartient. Un consommateur qui devine le dernier segment obtient une réponse
fausse et plausible.

## Décision

`DesignPatternAttribute` ne déclare aucun membre, et les règles permettant
de relire un pattern depuis un type d'attribut sont documentées plutôt
qu'implémentées.

## Justification

Un attribut est une donnée déclarative, et les deux formes rejetées brisaient
cela chacune à sa manière : l'une répétait ce que la déclaration disait déjà,
l'autre mettait du comportement — et l'introspection de l'agencement d'espaces de
noms de la librairie — à l'intérieur d'un marqueur. Ne rien déclarer est la seule
forme qui ne soit ni redondante ni comportementale.

Rien n'est retiré à un consommateur. L'information est dans le type, le
consommateur détient le type, et les règles qui font passer de l'un à l'autre
tiennent en quatre lignes chacune. Ce qu'il perd, c'est une façade sur sa propre
réflexion, qu'il pratiquait déjà.

Le piège reçoit une réponse sans API. Les règles vivent dans la documentation de
la classe de base, donc elles voyagent avec le package et apparaissent dans
l'éditeur, et un lecteur fonctionnel du projet d'exemple les applique — du code à
copier et à s'approprier plutôt qu'une interface dont dépendre. Un utilitaire
publié depuis la librairie devrait être versionné et maintenu compatible pour un
bénéfice mesuré en quatre lignes.

Rien n'est énoncé qui pourrait contredire la déclaration. C'est le raisonnement
qui a mis la structure du catalogue dans un gabarit plutôt que dans chaque
fichier : la défaillance supprimée n'est pas l'effort, c'est la divergence entre
deux énoncés d'un même fait.

L'extension reste ouverte en restant absente. Une équipe qui déclare son propre
vocabulaire, hors de cet agencement d'espaces de noms, hérite d'un marqueur vide
et le lit avec ses propres règles — là où des propriétés dérivées de notre
agencement lui auraient tendu des réponses fausses qu'elle ne pourrait pas
corriger.

## Alternatives envisagées

### Énoncer le catalogue, le pattern et le rôle comme constantes sur chaque attribut

Envisagé parce que c'est évident, que cela ne coûte rien à la lecture et que cela
survit à un renommage.

Rejeté parce que cela écrit chaque nom deux fois, donc que les deux peuvent
diverger ; parce que cela double l'assembly pour une information déjà présente ;
et parce que cette duplication est exactement ce que le catalogue et son gabarit
existent pour supprimer ailleurs.

### Les relire dans des accesseurs de propriétés sur la base

Envisagé parce que cela supprime la duplication tout en gardant une surface
commode, et parce que cela implémente la convention une fois, là où tout le monde
la respecte.

Rejeté parce que cela fait introspecter sa propre librairie à un marqueur, et
parce qu'un consommateur qui détient l'attribut détient déjà son type : la
commodité ne lui épargne rien qu'il ne faisait pas déjà.

### Publier un lecteur au sein de la librairie

Envisagé parce que les règles de lecture doivent bien vivre quelque part, et que
l'une d'elles est un piège.

Rejeté parce que cela deviendrait une API publique à versionner et à maintenir
compatible, pour une poignée de lignes qu'un consommateur a intérêt à posséder —
et parce qu'un utilitaire publié calcule, là où un consommateur hors de notre
agencement a besoin de pouvoir être en désaccord.

## Conséquences

### Positives

* La librairie est un vocabulaire et rien d'autre : aucun comportement, aucune
  réflexion, aucune convention propre à maintenir en état de marche.
* Rien en elle ne peut contredire les déclarations qu'elle accompagne.
* Un consommateur ayant son propre agencement n'est pas contraint.
* La surface publique est aussi petite que possible : il n'y a presque rien à
  versionner.

### Négatives

* Chaque consommateur écrit les règles de lecture, et la règle du catalogue est
  facile à manquer.
* Les règles ne sont imposées que par la documentation ; rien ne détecte un
  consommateur qui les applique autrement.

### Risques

* La convention documentée et le lecteur d'exemple peuvent diverger, laissant
  deux énoncés des règles en désaccord — la défaillance même que cette décision
  retire des attributs. Atténué par le fait que le lecteur d'exemple est exécuté.

## Actions de suivi

* Garder le lecteur d'exemple fonctionnel et exercé, puisqu'il est le seul énoncé
  exécutable des règles de lecture.

## Références

* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.fr.md) —
  la quatrième règle de lecture, celle qui ne peut pas se deviner.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.fr.md) — le
  projet qui porte le lecteur.
* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.fr.md) —
  la déclaration depuis laquelle les règles lisent.
