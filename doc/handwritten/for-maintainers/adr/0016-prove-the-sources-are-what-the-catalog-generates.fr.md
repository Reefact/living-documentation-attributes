# ADR-0016 | Prouver à chaque pull request que les sources sont ce que le catalogue génère

🌍 🇬🇧 [English](0016-prove-the-sources-are-what-the-catalog-generates.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Les attributs ne portent aucun comportement. Il n'y a rien à tester unitairement,
rien dont mesurer la couverture, et rien que des tests de mutation puissent muter
— les instruments habituels n'ont aucune prise ici.

Trois affirmations sont pourtant faites ailleurs dans cette base, et rien n'en
vérifie aucune.

L'ADR-0002 énonce que les sources des attributs sont générées depuis le catalogue
et que régénérer depuis un catalogue inchangé laisse l'arbre de travail propre.
Cet invariant est tout ce qui sépare le dépôt d'un attribut généré discrètement
édité à la main — ce qui survivrait à la revue, puisqu'une édition plausible dans
un fichier généré ressemble exactement à du généré.

L'ADR-0002 s'appuie aussi sur le schéma pour rendre relisible un catalogue écrit
en masse : un rôle manquant, une cible inconnue ou un lien vers un rôle inexistant
devraient être un échec plutôt que quelque chose à remarquer en lisant. Le schéma
existe et rien ne l'exécute.

L'ADR-0004 affirme que tout le catalogue peut être relu au travers du seul
attribut de base. Le projet d'exemples le démontre, et `AGENTS.md` dit déjà à un
contributeur que son inventaire est la vérification qu'un changement de catalogue
a bien atterri — à la main, de sa propre initiative.

La bibliothèque cible six frameworks : un changement qui compile sur le plus
récent peut ne pas compiler sur le plus ancien.

## Décision

Chaque pull request compile la solution sur les deux plateformes et prouve que le
catalogue est valide, qu'il se relit, et que les sources commitées sont exactement
ce que sa régénération produit.

## Justification

Ces quatre vérifications sont ce que ce dépôt peut prouver, et chacune correspond
à une affirmation qui ne serait sinon qu'affirmée. C'est le critère qui les fait
retenir : non qu'elles soient les vérifications habituelles, mais qu'un invariant
énoncé resterait sinon non vérifié.

L'aller-retour est celui qui compte le plus, parce que la défaillance qu'il
attrape est invisible à tout autre instrument. Un fichier généré édité à la main
compile, passe n'importe quel test et se lit correctement ; seul le fait de
régénérer et de comparer le révèle. Il est en outre presque gratuit, le générateur
étant un script sur quelques dizaines de petits fichiers.

Compiler le projet d'exemples est ce qui se rapproche le plus d'un test dont
dispose le vocabulaire. Un rôle dont les cibles déclarées sont trop étroites ne
peut pas être appliqué à un participant plausible, et cela ne compile pas — c'est
ainsi qu'a été trouvée l'absence de `Struct` parmi les cibles. L'exécuter ensuite
prouve que les annotations ne sont pas seulement écrivables mais lisibles, au
travers du seul attribut de base.

Une preuve positive plutôt qu'un code de sortie nul. Un projet d'exemples qui
n'annoterait silencieusement rien sortirait proprement : l'étape lit donc la ligne
d'inventaire et échoue sur une ligne vide — la vérification est que quelque chose
a été trouvé, non que rien n'a planté.

Les deux plateformes, parce que la gestion des chemins et les fins de ligne
diffèrent, et que le générateur écrit des fichiers. Un aller-retour qui tient sur
l'une et pas sur l'autre est un défaut réel, et il serait sinon trouvé par la
prochaine personne qui exécuterait le générateur sur l'autre.

Les instruments habituels sont délibérément absents. La couverture d'une
bibliothèque sans instruction exécutable rapporte un nombre qui ne signifie rien,
et un score de mutation sur la même chose est pire — un chiffre au vert sur lequel
personne ne peut agir érode la confiance dans tous les autres chiffres à côté de
lui.

## Alternatives envisagées

### Ajouter un projet de tests unitaires avec des tests de convention par réflexion

Envisagé parce que c'est la forme conventionnelle, et que de tels tests
attraperaient un attribut généré ayant perdu sa classe de base ou sa déclaration
de cibles.

Rejeté comme insuffisant plutôt que faux, et différé plutôt que refusé : les tests
de convention vérifient que le générateur a fait ce qu'on lui a dit, là où
l'aller-retour vérifie que ce qui est commité est ce qu'il produit — une garantie
strictement plus large pour moins de machinerie. Ils restent bons à ajouter pour
leurs mérites propres.

### Faire confiance au contributeur pour régénérer

Envisagé parce que `AGENTS.md` le dit déjà, et que la séquence tient en trois
commandes.

Rejeté parce que la défaillance est silencieuse par construction. Un contributeur
qui oublie ne voit rien d'anormal, un relecteur voit un diff plausible, et la
divergence est trouvée bien plus tard par quelqu'un dont la régénération produit
un changement sans rapport qu'il n'a pas fait.

### N'exécuter les vérifications que sur une seule plateforme

Envisagé parce que la compilation est en principe indépendante de la plateforme et
que la seconde branche double le coût d'un job peu coûteux.

Rejeté parce que le générateur écrit des fichiers et que le dépôt est édité depuis
Windows : les fins de ligne et la gestion des chemins sont exactement le genre de
chose qui diffère — et un aller-retour qui tient sur une plateforme et pas sur
l'autre est la défaillance que cette décision existe pour attraper.

## Conséquences

### Positives

* Un attribut généré édité à la main ne peut pas être fusionné.
* Une entrée de catalogue invalide échoue avant qu'on ne génère depuis elle.
* L'affirmation selon laquelle le catalogue se relit génériquement est vérifiée à
  chaque changement, non affirmée.
* Chaque framework pris en charge est compilé à chaque changement.

### Négatives

* Ajouter un pattern suppose d'exécuter le générateur avant de pousser, ou de
  rencontrer une compilation rouge.
* Le job de catalogue dépend de Python et d'un paquet épinglé : la CI du dépôt a
  donc besoin d'une chaîne d'outils dont ses consommateurs n'ont jamais besoin.

### Risques

* L'aller-retour prouve que les sources correspondent au catalogue, non que l'un
  ou l'autre est juste. Un pattern faux, correctement généré, passe toutes les
  vérifications d'ici.
* Un générateur rendu non déterministe — par un ordre, par une locale, par un
  horodatage — ferait échouer l'aller-retour pour une raison étrangère au
  changement en cours de revue. Rien ne l'empêche sinon un générateur assez petit
  pour être lu.

## Actions de suivi

* Ajouter des tests de convention par réflexion sur les attributs générés, que
  cette décision diffère plutôt qu'elle ne les rejette.

## Références

* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  l'invariant que vérifie l'aller-retour.
* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.md) — le contrat de
  lecture qu'exerce le projet d'exemples.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md) — pourquoi
  les exemples sont le seul test dont dispose le vocabulaire.
