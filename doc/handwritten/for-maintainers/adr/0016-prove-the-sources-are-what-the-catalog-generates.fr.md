# ADR-0016 | Prouver à chaque pull request que les sources sont ce que le catalogue génère

🌍 🇬🇧 [English](0016-prove-the-sources-are-what-the-catalog-generates.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Les attributs ne portent aucun comportement. Il n'y a rien à tester
unitairement, rien dont mesurer la couverture, et rien que des tests de mutation
puissent muter — les instruments habituels n'ont aucune prise ici.

Trois affirmations sont pourtant faites ailleurs dans cette base, et rien n'en
vérifie aucune.

L'ADR-0002 énonce que les sources des attributs sont générées depuis le catalogue
et que régénérer un catalogue inchangé laisse l'arbre de travail propre. Cet
invariant est tout ce qui sépare le dépôt d'un attribut généré discrètement
retouché à la main — ce qui survivrait à la revue, puisqu'une retouche plausible
d'un fichier généré ressemble exactement à un fichier généré.

L'ADR-0002 s'appuie aussi sur le schéma pour rendre relisible un catalogue écrit
en masse : un rôle manquant, une cible inconnue ou un lien vers un rôle qui
n'existe pas devraient être un échec plutôt que quelque chose à remarquer en
lisant. Le schéma existe et rien ne l'exécute.

L'ADR-0004 affirme que tout le catalogue peut être relu à travers le seul
attribut de base. Le projet d'exemple le démontre, et `AGENTS.md` dit déjà à un
contributeur que son inventaire est le contrôle qu'un changement de catalogue a
bien atterri — à la main, de sa propre initiative.

La librairie cible six frameworks : un changement qui compile sur le plus récent
peut ne pas compiler sur le plus ancien.

## Décision

Chaque pull request compile la solution sur les deux plateformes et prouve que le
catalogue est valide, qu'il se relit, et que les sources versionnées sont
exactement ce que sa régénération produit.

## Justification

Ces quatre contrôles sont ce que ce dépôt peut prouver, et chacun correspond à
une affirmation qui n'est sinon qu'affirmée. C'est le critère de leur inclusion :
non pas qu'ils soient les contrôles habituels, mais qu'un invariant énoncé
resterait sinon non vérifié.

L'aller-retour est celui qui compte le plus, parce que la défaillance qu'il
attrape est invisible à tous les autres instruments. Un fichier généré retouché à
la main compile, passe n'importe quel test, et se lit correctement ; seule la
régénération suivie d'une comparaison le révèle. Il est en outre quasi gratuit,
puisque le générateur est un script sur quelques dizaines de petits fichiers.

Compiler le projet d'exemple est ce qui ressemble le plus à un test dont dispose
le vocabulaire. Un rôle dont les cibles déclarées sont trop étroites ne peut pas
être appliqué à un participant plausible, et cela ne compile pas — c'est ainsi
qu'a été trouvée l'absence de `Struct` parmi les cibles. L'exécuter ensuite prouve
que les annotations ne sont pas seulement écrivables mais lisibles, à travers le
seul attribut de base.

Preuve positive plutôt que code de sortie nul. Un projet d'exemple qui
n'annoterait silencieusement rien sortirait proprement : l'étape lit donc la
ligne d'inventaire et échoue si elle est vide — le contrôle porte sur le fait que
quelque chose a été trouvé, pas sur le fait que rien n'a planté.

Les deux plateformes, parce que la gestion des chemins et les fins de ligne
diffèrent, et que le générateur écrit des fichiers. Un aller-retour qui tient sur
l'une et pas sur l'autre est un vrai défaut, que trouverait sinon la prochaine
personne à lancer le générateur sur l'autre.

Les instruments habituels sont délibérément absents. Une couverture sur une
librairie sans instruction exécutable rapporte un nombre qui ne signifie rien, et
un score de mutation sur la même est pire — un chiffre vert sur lequel personne
ne peut agir érode la confiance dans tous les autres chiffres à côté.

## Alternatives envisagées

### Ajouter un projet de tests unitaires avec des tests de convention par réflexion

Envisagé parce que c'est la forme conventionnelle, et que de tels tests
attraperaient un attribut généré qui aurait perdu sa classe de base ou sa
déclaration de cibles.

Rejeté comme insuffisant plutôt que faux, et différé plutôt que refusé : les
tests de convention vérifient que le générateur a fait ce qu'on lui a dit, là où
l'aller-retour vérifie que ce qui est versionné est ce qu'il produit — une
garantie strictement plus large pour moins de machinerie. Ils restent utiles à
ajouter pour leurs propres mérites.

### Faire confiance au contributeur pour régénérer

Envisagé parce qu'`AGENTS.md` le demande déjà, et que la séquence tient en trois
commandes.

Rejeté parce que la défaillance est silencieuse par construction. Un contributeur
qui oublie ne voit rien d'anormal, un relecteur voit un diff plausible, et la
divergence est trouvée bien plus tard par quelqu'un dont la régénération produit
un changement sans rapport qu'il n'a pas fait.

### Ne lancer les contrôles que sur une plateforme

Envisagé parce que le build est en principe indépendant de la plateforme et que
la seconde jambe double le coût d'un job bon marché.

Rejeté parce que le générateur écrit des fichiers et que le dépôt est édité
depuis Windows : les fins de ligne et la gestion des chemins sont exactement le
genre de choses qui diffèrent — et un aller-retour qui tient sur une plateforme
et pas sur l'autre est la défaillance que cette décision existe pour attraper.

## Conséquences

### Positives

* Un attribut généré retouché à la main ne peut pas être fusionné.
* Une entrée de catalogue invalide échoue avant qu'on en génère quoi que ce soit.
* L'affirmation que le catalogue se relit génériquement est vérifiée à chaque
  changement, non affirmée.
* Chaque framework supporté est compilé à chaque changement.

### Négatives

* Ajouter un pattern suppose de lancer le générateur avant de pousser, sous peine
  de rencontrer un build rouge.
* Le job de catalogue dépend de Python et d'un package épinglé : la CI du dépôt a
  donc besoin d'une chaîne d'outils dont ses consommateurs n'ont jamais besoin.

### Risques

* L'aller-retour prouve que les sources correspondent au catalogue, pas que l'un
  ou l'autre est juste. Un pattern faux, correctement généré, passe tous les
  contrôles d'ici.
* Un générateur rendu non déterministe — par un ordre, une locale, un horodatage
  — ferait échouer l'aller-retour pour une raison sans rapport avec le changement
  en revue. Rien ne l'empêche sinon que le générateur est assez petit pour être
  lu.

## Actions de suivi

* Ajouter des tests de convention par réflexion sur les attributs générés, que
  cette décision diffère plutôt qu'elle ne les rejette.

## Références

* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.fr.md) —
  l'invariant que l'aller-retour vérifie.
* [ADR-0004](0004-keep-the-attribute-base-a-pure-marker.fr.md) — le contrat de
  lecture que le projet d'exemple exerce.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.fr.md) —
  pourquoi les exemples sont le seul test dont dispose le vocabulaire.
