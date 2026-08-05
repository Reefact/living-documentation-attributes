# ADR-0017 | Épingler chaque action GitHub à un commit

🌍 🇬🇧 [English](0017-pin-every-action-to-a-commit.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Les workflows exécutent des actions tierces, qui s'exécutent avec le dépôt extrait
et avec le jeton accordé au job.

Une étiquette est un pointeur mutable. `@v7` résout vers le commit que l'étiquette
désigne au moment où le workflow s'exécute, et le propriétaire de l'action peut la
déplacer — y compris vers du code que personne ici n'a relu. Ce n'est pas
hypothétique : des actions compromises ont servi à exfiltrer des secrets depuis
des dépôts qui épinglaient par étiquette.

Ce dépôt est la moindre moitié du risque. Ses workflows détiennent aujourd'hui un
jeton en lecture seule et aucun identifiant de publication, mais c'est une
bibliothèque destinée à être publiée : un workflow de publication porteur d'un
identifiant de paquet est un ajout prévisible plutôt que lointain.

Épingler par commit n'est qu'une demi-pratique. Une action épinglée cesse de
recevoir des correctifs, y compris de sécurité, à moins que quelque chose ne mette
l'épingle à jour.

## Décision

Toute action employée par un workflow est référencée par son hachage de commit
complet, la version lisible par un humain figurant dans un commentaire de fin de
ligne.

## Justification

Cela supprime toute une classe d'exposition à la chaîne d'approvisionnement pour
le prix d'une ligne plus longue. Un hachage de commit ne peut pas être déplacé :
ce qui s'exécutait hier est ce qui s'exécute aujourd'hui, et une mise à jour
devient un changement relu plutôt que quelque chose qui arrive au dépôt pendant
que personne ne regarde.

L'adopter dès le premier workflow est ce qui la rend gratuite. Une politique
d'épinglage appliquée plus tard est une migration sur tous les workflows et une
décision sur chacun d'eux ; appliquée maintenant, il n'y a rien à migrer, et la
convention est ce qu'un contributeur recopie lorsqu'il ajoute le workflow suivant.

Le commentaire de version n'est pas décoratif. Un hachage nu est illisible : sans
lui, un relecteur ne peut pas distinguer une montée de version de routine d'une
régression, ni voir d'un coup d'œil que deux workflows ne s'accordent pas sur la
version d'une action qu'ils exécutent.

L'obsolescence ainsi introduite est réelle, et la réponse est un outillage plutôt
que la vigilance — un metteur à jour de dépendances propose la montée de version,
et la pull request qu'il ouvre est relue comme une autre. C'est le marché que ceci
accepte : la fraîcheur par un acte explicite plutôt que la fraîcheur par défaut.

Préférer la chaîne d'outils propre au runner là où elle suffit est le même
raisonnement appliqué un cran plus tôt. Une action qu'on n'emploie pas n'exige
aucune épingle, aucune revue et aucune mise à jour : le job de catalogue utilise
donc le Python déjà présent sur l'image plutôt qu'une action pour en installer un.

## Alternatives envisagées

### Référencer les actions par étiquette

Envisagé parce que c'est ce que font la plupart des dépôts, que cela se lit bien
et que cela maintient les actions à jour sans maintenance.

Rejeté parce qu'une étiquette est mutable : cela délègue au propriétaire de
l'action la décision de ce qui s'exécute ici. La fraîcheur vaut d'être obtenue,
mais pas au prix de ne pas savoir ce qui s'est exécuté.

### N'épingler que les actions employées par les workflows privilégiés

Envisagé parce que les workflows d'aujourd'hui sont en lecture seule, et que
l'exposition se concentre là où sont les identifiants.

Rejeté parce que cela met la sécurité d'un futur workflow entre les mains de qui
l'écrira, au moment où il pense à autre chose. Une règle qui s'applique partout
n'exige aucun jugement au point d'usage.

### Interner les actions dans le dépôt

Envisagé parce que cela retire entièrement le tiers de la chaîne de confiance.

Rejeté comme disproportionné : cela fait de chaque mise à jour un portage manuel,
pour un dépôt dont les workflows emploient trois actions bien connues.

## Conséquences

### Positives

* Ce qu'exécute un workflow est fixé et relisible, et ne peut pas changer sans un
  commit ici.
* La convention est établie avant qu'il n'y ait quoi que ce soit à migrer.
* Un relecteur peut lire à quelle version se trouve chaque action.

### Négatives

* Les épingles vieillissent : un metteur à jour de dépendances devient nécessaire
  plutôt que facultatif.
* Chaque montée de version d'action est une pull request, y compris les plus
  routinières.

### Risques

* Un hachage et son commentaire de version peuvent diverger si une montée de
  version édite l'un et pas l'autre, ce qui égarerait un relecteur précisément là
  où le commentaire existe pour l'informer.
* Sans metteur à jour configuré, l'épinglage devient silencieusement un gel, et un
  correctif de sécurité dans une action n'arrive jamais.

## Actions de suivi

* Configurer un metteur à jour de dépendances pour les actions des workflows, sans
  lequel cette décision dégénère en gel.

## Références

* `.github/workflows/` — là où vivent les épingles.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.md) — les
  workflows auxquels ceci s'applique.
