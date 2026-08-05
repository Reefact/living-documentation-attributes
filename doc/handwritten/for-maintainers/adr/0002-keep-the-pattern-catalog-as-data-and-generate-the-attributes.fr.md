# ADR-0002 | Tenir le catalogue de patterns comme donnée et en générer les attributs

🌍 🇬🇧 [English](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

La bibliothèque publie un attribut par rôle, à travers des corpus qui décrivent
ensemble plusieurs centaines de patterns. Tous les attributs ont la même structure
— une base, un ensemble de cibles, une multiplicité, une règle d'héritage — et ne
diffèrent que par leur contenu : quels rôles existent, ce que fait chacun.

Les trente et un premiers patterns ont été écrits à la main avec l'assistance d'un
agent. La structure a dérivé d'un fichier à l'autre de façons qu'aucun compilateur
ne pouvait détecter : douze ensembles de rôles étaient amputés de rôles
canoniques, un attribut ne dérivait pas de la base et restait donc invisible à
tout lecteur générique, quatre ne portaient aucune déclaration de cible, et les
modificateurs `sealed` et `Inherited` étaient appliqués de manière incohérente.
Chaque fichier était plausible lu seul ; les défauts n'apparaissaient qu'en
comparant les fichiers entre eux.

Une tentative de génération antérieure existait dans le dépôt et avait été
abandonnée : un outil personnalisé Visual Studio, accessible depuis une seule
extension enregistrée sur un poste Windows, que rien ne pouvait lancer depuis une
ligne de commande, depuis un autre éditeur ou sur une autre machine. Son point
d'entrée n'a jamais été écrit. La forme de sa sortie avait entre-temps divergé des
fichiers écrits à la main, si bien que deux modèles coexistaient et se
contredisaient.

Le contenu d'un pattern — ses rôles, ce que fait chaque participant, l'ouvrage qui
l'a introduit — est éditorial. Il est écrit et relu par un humain, et c'est la
partie qui porte la valeur de la bibliothèque. Sa structure ne l'est pas : c'est
la même décision, prise une fois, répétée plusieurs centaines de fois.

## Décision

Le catalogue est rédigé comme de la donnée, un fichier par pattern, et les sources
des attributs en sont produites par un générateur de développement dont la sortie
est commitée.

## Justification

Séparer les deux place chaque partie là où elle peut être vérifiée. Un contenu
rédigé comme de la donnée est validable par un schéma — un rôle manquant, une
cible inconnue, un lien vers un rôle inexistant deviennent des erreurs au lieu
d'être des choses à remarquer en lisant. Une structure émise par un unique gabarit
ne peut pas dériver, puisqu'il n'y en a qu'un : le mode de défaillance qui a
produit les trente et une premières entrées est supprimé par construction plutôt
que par vigilance.

Commiter la sortie et ne livrer qu'elle maintient le coût de la génération à
l'intérieur du dépôt. Les consommateurs reçoivent un assembly et ne rencontrent
jamais le catalogue, le générateur ni ses dépendances. Rien ne change dans la
compilation d'un projet consommateur, et les fichiers générés restent lisibles et
relisibles dans une pull request — ce qui compte, puisqu'ils sont le seul artefact
livré.

Un outil de développement évite l'échec de celui qui fut abandonné. Le générateur
est lancé délibérément, par qui édite le catalogue, et son résultat est relu comme
un diff parmi d'autres ; il n'exige ni éditeur, ni extension, ni intégration au
build sur une autre machine que celle qui édite le catalogue.

La donnée survit aux attributs. Le même catalogue peut produire un index, un site
de documentation, un schéma pour les consommateurs qui déclareraient leurs propres
vocabulaires — autant de sorties qui exigeraient chacune de ré-extraire le contenu
depuis le code source s'il ne vivait que dans le code source.

## Alternatives envisagées

### Continuer d'écrire les attributs à la main

Envisagé parce que les attributs sont petits, que la structure est simple et que
le dépôt ne porterait aucun outil.

Rejeté au vu des trente et un premiers : la structure a bel et bien dérivé, de
cinq manières distinctes, et aucune n'a été attrapée. À l'échelle vers laquelle va
le catalogue, la même défaillance se répéterait plusieurs centaines de fois, et un
relecteur comparant les fichiers entre eux n'est pas un mécanisme.

### Émettre les attributs par un générateur de source Roslyn

Envisagé parce que c'est l'instrument moderne pour ce type de problème, et parce
qu'il retirerait entièrement les fichiers générés du dépôt.

Rejeté parce que les attributs générés sont l'artefact livré et que leur
lisibilité est une fonctionnalité : un mainteneur les lit, les relit dans un diff,
et un consommateur y navigue depuis son propre code. Un générateur de source
imposerait de surcroît la génération à la compilation de chaque consommateur sans
contrepartie, puisque le catalogue qu'il lit est le nôtre et fixé à la
publication.

### Conserver l'outil personnalisé abandonné

Envisagé parce qu'il existait et que son rendu fonctionnait.

Rejeté parce qu'il ne pouvait s'exécuter que dans un seul éditeur sur un seul
système d'exploitation, ce qui explique que son point d'entrée n'ait jamais été
écrit et que les deux modèles aient déjà divergé.

### Générer les attributs par un agent plutôt que par un gabarit

Envisagé parce qu'un agent écrit bien le contenu et n'exigerait aucun outil.

Rejeté parce que c'est précisément ainsi que les trente et un premiers ont été
produits. Un agent est le bon instrument pour le contenu et le mauvais pour la
structure : les défauts qu'il laisse sont plausibles, uniformes d'apparence et
invisibles sans une règle à laquelle les confronter.

## Conséquences

### Positives

* La structure est écrite une fois et ne peut pas varier d'un bout à l'autre du
  catalogue.
* Le contenu est validé par un schéma au lieu d'être relu à la lecture.
* Ajouter un pattern est l'édition d'un petit fichier de données, quelle que soit
  la taille du catalogue.
* Le catalogue peut alimenter d'autres sorties que les attributs.

### Négatives

* Le dépôt porte un outil et son langage, dont aucun n'est livré.
* Deux artefacts doivent rester en phase ; régénérer depuis un catalogue inchangé
  doit laisser l'arbre de travail propre, et rien ne l'impose sinon le mainteneur
  qui l'exécute.

### Risques

* Un fichier généré édité à la main serait silencieusement écrasé à l'exécution
  suivante. Atténué seulement par un aller-retour assez peu coûteux pour être
  exécuté par habitude.
* Le gabarit devient un point de défaillance unique : un défaut qui l'atteint
  touche tous les patterns d'un coup. Atténué par cette même régénération, qui le
  fait apparaître sur l'ensemble du diff plutôt que dans un seul fichier.

## Actions de suivi

* Garder l'aller-retour vérifiable : régénérer depuis un catalogue inchangé ne
  laisse aucun diff.
* Générer un index du catalogue, sans lequel plusieurs centaines de patterns ne
  sont pas navigables.

## Références

* `catalog/README.md` — comment le catalogue est rédigé et régénéré.
* `catalog/pattern.schema.json` — ce qu'une entrée de catalogue doit satisfaire.
* [ADR-0001](0001-check-every-pull-request-against-the-adr-base.md) — pourquoi la
  génération rend nécessaire l'enregistrement des décisions.
* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md) — la
  forme qu'émet le générateur.
