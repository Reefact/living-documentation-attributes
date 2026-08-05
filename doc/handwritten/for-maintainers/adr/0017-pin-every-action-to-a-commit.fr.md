# ADR-0017 | Épingler chaque action GitHub à un commit

🌍 🇬🇧 [English](0017-pin-every-action-to-a-commit.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Les workflows exécutent des actions tierces, qui tournent avec le dépôt sorti sur
le disque et avec le jeton que le job s'est vu accorder.

Une étiquette est un pointeur mutable. `@v7` résout vers le commit que
l'étiquette désigne au moment où le workflow tourne, et le propriétaire de
l'action peut la déplacer — y compris vers du code que personne ici n'a jamais
relu. Ce n'est pas hypothétique : des actions compromises ont servi à exfiltrer
des secrets depuis des dépôts qui épinglaient par étiquette.

Ce dépôt est la plus petite moitié du risque. Ses workflows détiennent
aujourd'hui un jeton en lecture seule et aucune information d'identification de
publication, mais c'est une librairie destinée à être publiée : un workflow de
release porteur d'une clé de package est un ajout prévisible plutôt que lointain.

Épingler par commit n'est qu'une demi-pratique. Une action épinglée cesse de
recevoir les correctifs, y compris de sécurité, tant que rien ne met l'épingle à
jour.

## Décision

Chaque action utilisée par un workflow est référencée par son empreinte de commit
complète, la version lisible par un humain figurant en commentaire de fin de
ligne.

## Justification

Cela supprime toute une classe d'exposition de chaîne d'approvisionnement pour le
prix d'une ligne plus longue. Une empreinte de commit ne peut pas être déplacée :
ce qui tournait hier est ce qui tourne aujourd'hui, et une mise à jour devient un
changement relu plutôt que quelque chose qui arrive à un dépôt pendant que
personne ne regarde.

L'adopter dès le premier workflow est ce qui la rend gratuite. Une politique
d'épinglage appliquée plus tard est une migration sur tous les workflows et une
décision sur chacun ; appliquée maintenant, il n'y a rien à migrer, et la
convention est ce qu'un contributeur recopie en ajoutant le workflow suivant.

Le commentaire de version n'est pas un ornement. Une empreinte nue est illisible :
sans lui, un relecteur ne peut pas distinguer une montée de version de routine
d'un retour en arrière, ni voir d'un coup d'œil que deux workflows sont en
désaccord sur la version d'une action qu'ils exécutent.

L'obsolescence que cela introduit est réelle, et la réponse est l'outillage
plutôt que la vigilance — un outil de mise à jour des dépendances propose la
montée de version, et la pull request qu'il ouvre se relit comme une autre. C'est
le marché accepté ici : la fraîcheur par un acte explicite plutôt que la
fraîcheur par défaut.

Préférer la chaîne d'outils du runner là où elle suffit est le même raisonnement
appliqué un cran plus tôt. Une action non utilisée n'a besoin ni d'épingle, ni de
revue, ni de mise à jour : le job de catalogue utilise donc le Python déjà
présent sur l'image plutôt qu'une action pour en installer un.

## Alternatives envisagées

### Référencer les actions par étiquette

Envisagé parce que c'est ce que font la plupart des dépôts, que cela se lit bien,
et que cela garde les actions à jour sans maintenance.

Rejeté parce qu'une étiquette est mutable : cela délègue au propriétaire de
l'action la décision du code qui tourne ici. La fraîcheur vaut d'être obtenue,
mais pas au prix de ne pas savoir ce qui a tourné.

### N'épingler que les actions employées par les workflows privilégiés

Envisagé parce que les workflows actuels sont en lecture seule, et que
l'exposition se concentre là où sont les informations d'identification.

Rejeté parce que cela remet la sécurité d'un futur workflow entre les mains de
qui l'écrira, au moment où il pense à autre chose. Une règle qui s'applique
partout n'exige aucun jugement au point d'usage.

### Internaliser les actions dans le dépôt

Envisagé parce que cela retire entièrement le tiers de la chaîne de confiance.

Rejeté comme disproportionné : cela fait de chaque mise à jour un portage manuel,
pour un dépôt dont les workflows emploient trois actions très connues.

## Conséquences

### Positives

* Ce qu'un workflow exécute est figé et relisible, et ne peut pas changer sans un
  commit ici.
* La convention est établie avant qu'il y ait quoi que ce soit à migrer.
* Un relecteur peut lire à quelle version se trouve chaque action.

### Négatives

* Les épingles vieillissent : un outil de mise à jour des dépendances devient
  nécessaire plutôt qu'optionnel.
* Chaque montée de version d'action est une pull request, y compris les routines.

### Risques

* Une empreinte et son commentaire de version peuvent diverger si une montée de
  version édite l'une et pas l'autre, ce qui tromperait un relecteur précisément
  là où le commentaire existe pour l'informer.
* Sans outil de mise à jour configuré, épingler devient silencieusement geler, et
  un correctif de sécurité dans une action n'arrive jamais.

## Actions de suivi

* Configurer un outil de mise à jour des dépendances pour les actions des
  workflows, sans lequel cette décision dégénère en gel.

## Références

* `.github/workflows/` — là où vivent les épingles.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.fr.md) — les
  workflows auxquels ceci s'applique.
