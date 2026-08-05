# ADR-0018 | Tenir la surface publique à une référence versionnée

🌍 🇬🇧 [English](0018-hold-the-public-surface-to-a-committed-baseline.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Cette librairie livre des types publics et rien d'autre. Il n'y a aucune
implémentation derrière eux : sa surface publique n'est donc pas un aspect du
produit — elle en est la totalité, et chaque changement qu'elle subit est un
changement de ce dont les consommateurs dépendent.

La surface est générée. Une modification du gabarit altère tous les patterns d'un
coup, et le diff qu'elle produit couvre tout le catalogue, c'est-à-dire là où
l'attention d'un relecteur est la moins capable de remarquer qu'un rôle a perdu
une propriété ou qu'une classe de base a changé.

L'ADR-0003 énonce qu'ajouter un rôle à un pattern publié est additif, et que
c'est l'une des raisons pour lesquelles les énumérations ont été abandonnées.
Rien ne le vérifie. La même décision laisse la réciproque sans garde : retirer un
rôle, en renommer un, ou restreindre ce à quoi il s'applique sont des ruptures de
compatibilité qui produisent un build vert.

Le catalogue doit croître d'un ordre de grandeur, et une bonne part de cette
croissance sera écrite en masse. Une surface qui grandit de plusieurs centaines
de types sous une revue à la lecture est une surface que personne ne relit
vraiment.

La librairie cible six frameworks, et les attributs sont les mêmes sur les six.

## Décision

La surface publique est déclarée dans une référence versionnée que chaque build
contrôle, et une modification de cette surface échoue tant que le même changement
ne met pas la référence à jour.

## Justification

Cela transforme la surface en un diff relu, seule forme sous laquelle elle peut
réellement l'être. Une pull request qui ajoute un pattern présente quelques
dizaines de lignes de référence à côté des fichiers générés, et ces lignes sont
lisibles d'une façon que le code généré n'est pas — ce sont exactement les noms
publics, sans rien autour.

Cela met une garde sur ce que l'ADR-0003 affirme, au lieu de le laisser à l'état
d'intention. Ajouter un rôle ajoute désormais à la référence et rien ne casse ;
en retirer, en renommer ou en restreindre un lève un diagnostic qui nomme le
symbole. La promesse et son application cessent d'être deux choses séparées.

Une référence unique partagée par les six frameworks, plutôt qu'une par
framework, énonce que la surface est censée être identique partout. Une référence
par framework laisserait deux cibles s'éloigner et absorberait silencieusement la
différence ; une référence partagée en fait un échec, ce qui est une garantie
gagnée plutôt qu'un coût payé.

**Le générateur ne doit pas écrire la référence.** Elle serait alors toujours
d'accord avec elle-même, et le contrôle ne confirmerait que le déterminisme du
générateur — ce que l'aller-retour prouve déjà. La référence est mise à jour par
un acte délibéré de qui modifie la surface, et cet acte est la revue.

Avertir en local et échouer en CI suit le cliquet déjà en place : un changement à
moitié fait peut laisser la référence périmée pendant qu'il prend forme, et
rencontre la garde à l'entrée.

Tout se trouve aujourd'hui dans le fichier des entrées non publiées parce que
rien n'a été publié. La distinction gagne son intérêt à la première version,
lorsque les entrées accumulées sont promues et que le fichier des entrées
publiées devient le relevé de ce qui a réellement été donné aux consommateurs.

## Alternatives envisagées

### Relire la surface en lisant le diff généré

Envisagé parce que les fichiers générés sont versionnés et apparaissent dans
chaque pull request : l'information est donc déjà sous les yeux du relecteur.

Rejeté parce qu'elle est sous ses yeux parmi plusieurs centaines d'autres
fichiers modifiés pour la même raison. La défaillance dont il s'agit de se garder
— une propriété perdue, une classe de base changée, une cible restreinte — est
invisible à cette échelle, c'est-à-dire précisément quand elle est la plus
probable.

### Faire émettre la référence par le générateur, à côté des sources

Envisagé parce que cela supprimerait la corvée : ajouter un pattern mettrait la
référence à jour automatiquement, et les deux ne pourraient jamais diverger.

Rejeté parce que « ne pourraient jamais diverger » est tout le problème. Une
référence écrite par ce qu'elle contrôle ne confirme rien, et le changement
qu'elle aurait attrapé — une modification de gabarit qui altère la surface de
chaque rôle — réécrirait simplement la référence pour s'y accorder. La corvée est
le mécanisme.

### S'en remettre à la validation de package contre une version publiée

Envisagé parce que cela vérifie la compatibilité avec ce que les consommateurs
ont réellement, qui est la question qui compte en dernier ressort.

Rejeté comme indisponible plutôt que comme faux : cela exige une version de
référence publiée, et cette librairie n'a encore aucune métadonnée de packaging.
C'est complémentaire et cela vaudra d'être ajouté dès qu'il y aura quelque chose
à quoi se comparer.

### Écrire des tests par réflexion affirmant la forme de chaque attribut généré

Envisagé parce que de tels tests pourraient vérifier la classe de base, les
cibles et la multiplicité de chaque rôle, ce que la référence ne fait pas.

Rejeté comme répondant à une autre question, et différé plutôt que refusé. Ces
tests vérifieraient que le générateur a fait ce qu'on lui a dit, là où la
référence vérifie ce que les consommateurs voient. Les deux valent d'être
obtenus, et aucun ne remplace l'autre.

## Conséquences

### Positives

* Chaque changement de ce dont les consommateurs dépendent est un diff court et
  lisible.
* Un rôle retiré ou renommé fait échouer le build au lieu d'être livré.
* Les six cibles sont tenues à une seule surface : elles ne peuvent pas
  s'éloigner.
* L'ensemble des types publics est une question à laquelle une réponse est
  versionnée.

### Négatives

* Ajouter un pattern comporte désormais une quatrième étape, et l'oublier signifie
  un build rouge.
* La référence est volumineuse et grossit avec le catalogue : une pull request
  qui ajoute un catalogue porte donc un diff de référence de taille
  correspondante.

### Risques

* La mise à jour est mécanique — un outil ajoute les entrées — elle peut donc
  être appliquée sans être lue, ce qui ferait du diff une formalité plutôt qu'une
  revue. Rien ici ne l'empêche, sinon un diff assez court pour être lu.
* La référence enregistre ce qui est public, pas ce qui devrait l'être. Un type
  faux, correctement déclaré, passe.

## Actions de suivi

* Promouvoir les entrées non publiées vers le fichier des entrées publiées à la
  première version, et activer la validation de package contre cette version dès
  qu'il y en aura une.
* Ajouter des tests de convention par réflexion sur les attributs générés, que
  cette décision diffère plutôt qu'elle ne les rejette — comme le fait
  l'ADR-0016.

## Références

* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.fr.md) —
  l'affirmation qu'ajouter un rôle est additif, que ceci garde.
* [ADR-0015](0015-turn-a-warning-into-an-error-in-ci.fr.md) — le cliquet qui rend
  les diagnostics bloquants à l'entrée.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.fr.md) —
  l'aller-retour, qui prouve le déterminisme et ne peut donc pas prouver ceci
  également.
* `CONTRIBUTING.md` — comment un changement de surface est accepté.
