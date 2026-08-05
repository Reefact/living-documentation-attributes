# ADR-0018 | Tenir la surface publique à une baseline commitée

🌍 🇬🇧 [English](0018-hold-the-public-surface-to-a-committed-baseline.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Cette bibliothèque livre des types publics et rien d'autre. Il n'y a aucune
implémentation derrière eux : sa surface publique n'est donc pas un aspect du
produit, elle en est la totalité, et tout changement qui l'affecte est un
changement de ce dont les consommateurs dépendent.

La surface est générée. Une modification du gabarit altère tous les patterns d'un
coup, et le diff qu'elle produit couvre tout le catalogue, c'est-à-dire là où
l'attention d'un relecteur est le moins en mesure de remarquer qu'un rôle a perdu
une propriété ou qu'une classe de base a changé.

L'ADR-0003 énonce qu'ajouter un rôle à un pattern publié est additif, et que c'est
l'une des raisons pour lesquelles les énumérations ont été abandonnées. Rien ne le
vérifie. La même décision laisse la réciproque sans garde : retirer un rôle, en
renommer un ou restreindre ce à quoi il s'applique sont des ruptures de
compatibilité qui produisent une compilation verte.

Le catalogue est appelé à croître d'un ordre de grandeur, et une bonne part de
cette croissance sera rédigée en masse. Une surface qui grandit de plusieurs
centaines de types sous une revue faite à la lecture est une surface que personne
ne relit réellement.

La bibliothèque cible six frameworks, et les attributs sont les mêmes sur les six.

## Décision

La surface publique est déclarée dans une baseline commitée que chaque compilation
vérifie, et un changement qui l'affecte échoue tant que le même changement ne met
pas la baseline à jour.

## Justification

Cela transforme la surface en un diff relu, seule forme sous laquelle elle peut
effectivement l'être. Une pull request qui ajoute un pattern montre quelques
dizaines de lignes de baseline à côté des fichiers générés, et ces lignes sont
lisibles d'une façon dont le code généré ne l'est pas — ce sont exactement les
noms publics, sans rien d'autre autour.

Cela pose une garde sur l'affirmation que fait l'ADR-0003 plutôt que de la laisser
à l'état d'intention. Ajouter un rôle ajoute désormais à la baseline et rien ne
casse ; retirer, renommer ou restreindre un rôle lève un diagnostic qui nomme le
symbole. La promesse et son application cessent d'être deux choses distinctes.

Une baseline unique partagée par les six frameworks, plutôt qu'une par framework,
énonce que la surface est censée être identique partout. Une baseline par
framework laisserait deux cibles diverger et absorberait la différence en
silence ; une baseline partagée en fait un échec, ce qui est une garantie gagnée
plutôt qu'un coût consenti.

**Le générateur ne doit pas écrire la baseline.** Il serait alors toujours
d'accord avec lui-même, et la vérification ne confirmerait que le déterminisme du
générateur — ce que l'aller-retour prouve déjà. La baseline est mise à jour par un
acte délibéré de qui modifie la surface, et cet acte est la revue.

Avertir en local et échouer en CI suit le cliquet déjà en place : un changement à
moitié fait peut laisser la baseline périmée pendant qu'on le façonne, et il
rencontre la garde à l'entrée.

Tout se trouve aujourd'hui dans le fichier « non livré » parce que rien n'a été
publié. La distinction gagnera sa place à la première version, lorsque les entrées
accumulées seront promues et que le fichier « livré » deviendra le registre de ce
que les consommateurs ont réellement reçu.

## Alternatives envisagées

### Relire la surface en lisant le diff généré

Envisagé parce que les fichiers générés sont commités et apparaissent dans chaque
pull request : l'information est donc déjà sous les yeux du relecteur.

Rejeté parce qu'elle est sous ses yeux parmi plusieurs centaines d'autres fichiers
qui ont changé pour la même raison. La défaillance dont il s'agit de se prémunir —
une propriété perdue, une classe de base changée, une cible restreinte — est
invisible à cette échelle, c'est-à-dire précisément quand elle est la plus
probable.

### Faire émettre la baseline par le générateur, à côté des sources

Envisagé parce que cela supprimerait la corvée : ajouter un pattern mettrait la
baseline à jour automatiquement, et les deux ne pourraient jamais diverger.

Rejeté parce que « ne pourraient jamais diverger » est tout le problème. Une
baseline écrite par ce qu'elle vérifie ne confirme rien, et le changement qu'elle
aurait attrapé — une modification de gabarit qui altère la surface de chaque rôle
— se contenterait de réécrire la baseline pour lui correspondre. La corvée est le
mécanisme.

### S'en remettre à la validation de paquet face à une version publiée

Envisagé parce que cela vérifie la compatibilité face à ce que les consommateurs
détiennent réellement, qui est la question qui compte en dernier ressort.

Rejeté comme indisponible plutôt que faux : cela exige une version de référence
publiée, et cette bibliothèque n'a encore aucune métadonnée d'empaquetage. C'est
complémentaire et cela vaudra d'être ajouté dès qu'il y aura quelque chose à
comparer.

### Écrire des tests par réflexion affirmant la forme de chaque attribut généré

Envisagé parce que de tels tests pourraient vérifier la classe de base, les cibles
et la multiplicité de chaque rôle, ce que la baseline ne fait pas.

Rejeté comme répondant à une autre question, et différé plutôt que refusé. Ces
tests vérifieraient que le générateur a fait ce qu'on lui a dit ; la baseline
vérifie ce que les consommateurs voient. Les deux valent d'être eus, et aucun ne
remplace l'autre.

## Conséquences

### Positives

* Tout changement de ce dont les consommateurs dépendent est un diff court et
  lisible.
* Un rôle retiré ou renommé fait échouer la compilation au lieu d'être livré.
* Les six cibles sont tenues à une seule surface : elles ne peuvent donc pas
  diverger.
* L'ensemble des types publics est une question dont la réponse est commitée.

### Négatives

* Ajouter un pattern comporte désormais une quatrième étape, et l'oublier signifie
  une compilation rouge.
* La baseline est volumineuse et croît avec le catalogue : une pull request qui
  ajoute un catalogue porte donc un diff de baseline d'autant plus grand.

### Risques

* La mise à jour est mécanique — un outil ajoute les entrées — elle peut donc être
  appliquée sans être lue, ce qui ferait du diff une formalité plutôt qu'une
  revue. Rien ici ne l'empêche, sinon un diff assez court pour être lu.
* La baseline consigne ce qui est public, non ce qui devrait l'être. Un type
  fautif, correctement déclaré, passe.

## Actions de suivi

* Promouvoir les entrées non livrées vers le fichier des entrées livrées à la
  première version, et activer la validation de paquet face à cette version dès
  qu'il y en aura une.
* Ajouter des tests de convention par réflexion sur les attributs générés, que
  cette décision diffère plutôt qu'elle ne les rejette — comme le fait
  l'ADR-0016.

## Références

* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) —
  l'affirmation selon laquelle ajouter un rôle est additif, que ceci garde.
* [ADR-0015](0015-turn-a-warning-into-an-error-in-ci.md) — le cliquet qui rend les
  diagnostics bloquants à l'entrée.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.md) —
  l'aller-retour, qui prouve le déterminisme et ne peut donc pas prouver ceci
  également.
* `CONTRIBUTING.md` — comment un changement de surface est accepté.
