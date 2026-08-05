# ADR-0002 | Tenir le catalogue de patterns comme une donnée et en générer les attributs

🌍 🇬🇧 [English](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

La librairie publie un attribut par rôle, à travers des corpus qui décrivent à
eux tous plusieurs centaines de patterns. Chaque attribut a la même structure —
une base, un ensemble de cibles, une multiplicité, une règle d'héritage — et ne
diffère que par son contenu : quels rôles existent, et ce que fait chacun.

Les trente et un premiers patterns ont été écrits à la main avec l'aide d'un
agent. La structure a dérivé d'un fichier à l'autre par des voies qu'aucun
compilateur ne pouvait attraper : douze ensembles de rôles manquaient de rôles
canoniques, un attribut ne dérivait pas de la base et était donc invisible à tout
lecteur générique, quatre ne portaient aucune déclaration de cible, et les
modificateurs `sealed` et `Inherited` étaient appliqués de façon incohérente.
Chaque fichier était plausible lu seul ; les défauts n'apparaissaient qu'en
comparant les fichiers entre eux.

Une tentative de génération plus ancienne existait dans le dépôt et avait été
abandonnée : un *custom tool* Visual Studio, atteignable seulement depuis une
extension enregistrée sur un poste Windows, que rien ne pouvait lancer depuis une
ligne de commande, depuis un autre éditeur ou sur une autre machine. Son point
d'entrée n'a jamais été écrit. La forme de sa sortie avait entre-temps divergé
des fichiers écrits à la main, de sorte que les deux modèles coexistaient en se
contredisant.

Le contenu d'un pattern — ses rôles, ce que fait chaque participant, quel travail
l'a introduit — est éditorial. Il est écrit et relu par un humain, et c'est la
part qui porte la valeur de la librairie. Sa structure, non : c'est la même
décision, prise une fois, répétée plusieurs centaines de fois.

## Décision

Le catalogue est écrit comme une donnée, un fichier par pattern, et les sources
des attributs en sont produites par un générateur de temps de développement dont
la sortie est versionnée.

## Justification

Séparer les deux met chaque part là où elle peut être contrôlée. Un contenu écrit
comme une donnée peut être validé par un schéma — un rôle manquant, une cible
inconnue, un lien vers un rôle qui n'existe pas deviennent des erreurs au lieu de
choses à remarquer en lisant. Une structure émise par un unique gabarit ne peut
pas dériver, puisqu'il n'y en a qu'un : le mode de défaillance qui a produit les
trente et une premières entrées est supprimé par construction plutôt que par
vigilance.

Versionner la sortie et ne livrer qu'elle garde le coût de la génération à
l'intérieur du dépôt. Les consommateurs reçoivent une assembly et ne rencontrent
jamais le catalogue, le générateur ni ses dépendances. Rien ne change dans le
build d'un projet consommateur, et les fichiers générés restent lisibles et
relisibles dans une pull request — ce qui compte, puisqu'ils sont le seul
artefact livré.

Un outil de temps de développement évite l'échec de celui qui a été abandonné. Le
générateur est lancé délibérément, par qui édite le catalogue, et son résultat est
relu comme un diff au même titre que n'importe quel changement ; il n'exige ni
éditeur, ni extension, ni intégration au build sur une autre machine que celle
qui édite le catalogue.

La donnée survit aux attributs. Le même catalogue peut produire un index, un site
de documentation, un schéma pour des consommateurs qui déclarent leur propre
vocabulaire — autant de sorties qui exigeraient chacune de redériver le contenu
depuis le code source si ce contenu ne vivait que dans le code source.

## Alternatives envisagées

### Continuer à écrire les attributs à la main

Envisagé parce que les attributs sont petits, la structure simple, et que le
dépôt ne porterait aucun outil.

Rejeté au vu des trente et un premiers : la structure a bel et bien dérivé, de
cinq façons distinctes, et aucune n'a été attrapée. À l'échelle vers laquelle le
catalogue se dirige, le même échec se répéterait plusieurs centaines de fois, et
un relecteur qui compare les fichiers entre eux n'est pas un mécanisme.

### Émettre les attributs avec un générateur de source Roslyn

Envisagé parce que c'est l'instrument moderne pour cette forme de problème, et
parce qu'il retirerait entièrement les fichiers générés du dépôt.

Rejeté parce que les attributs générés sont l'artefact livré et que leur
lisibilité est une fonctionnalité : un mainteneur les lit, les relit dans un
diff, et un consommateur y navigue depuis son propre code. Un générateur de
source mettrait de plus la génération sur le build de chaque consommateur sans
bénéfice, puisque le catalogue qu'il lit est le nôtre et fixé à la publication.

### Conserver le *custom tool* abandonné

Envisagé parce qu'il existait et que son rendu fonctionnait.

Rejeté parce qu'il ne pourrait jamais tourner que dans un éditeur sur un système
d'exploitation, ce qui explique pourquoi son point d'entrée n'a jamais été écrit
et pourquoi les deux modèles avaient déjà divergé.

### Générer les attributs avec un agent plutôt qu'avec un gabarit

Envisagé parce qu'un agent écrit bien le contenu et n'exigerait aucun outil.

Rejeté parce que c'est précisément ainsi que les trente et un premiers ont été
produits. Un agent est le bon instrument pour le contenu et le mauvais pour la
structure : les défauts qu'il laisse sont plausibles, uniformes d'apparence, et
invisibles sans une règle à laquelle les confronter.

## Conséquences

### Positives

* La structure est écrite une fois et ne peut pas varier à travers le catalogue.
* Le contenu est validé par un schéma au lieu d'être relu à la lecture.
* Ajouter un pattern est l'édition d'un petit fichier de données, quelle que soit
  la taille du catalogue.
* Le catalogue peut alimenter d'autres sorties que les attributs.

### Négatives

* Le dépôt porte un outil et son langage, dont aucun n'est livré.
* Deux artefacts doivent rester en phase ; régénérer depuis un catalogue inchangé
  doit laisser l'arbre de travail propre, et rien ne l'impose sinon le mainteneur
  qui le lance.

### Risques

* Un fichier généré édité à la main serait silencieusement écrasé au run suivant.
  Atténué seulement par un aller-retour assez peu coûteux pour être lancé par
  habitude.
* Le gabarit devient un point de défaillance unique : un défaut qu'il porte
  atteint tous les patterns d'un coup. Atténué par cette même régénération, qui
  le fait apparaître sur l'ensemble du diff plutôt que dans un seul fichier.

## Actions de suivi

* Garder l'aller-retour vérifiable : régénérer un catalogue inchangé ne laisse
  aucun diff.
* Générer un index du catalogue, sans lequel plusieurs centaines de patterns ne
  sont pas navigables.

## Références

* `catalog/README.md` — comment le catalogue est écrit et régénéré.
* `catalog/pattern.schema.json` — ce qu'une entrée de catalogue doit satisfaire.
* [ADR-0001](0001-check-every-pull-request-against-the-adr-base.fr.md) — pourquoi
  la génération rend nécessaire l'enregistrement des décisions.
* [ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.fr.md) —
  la forme qu'émet le générateur.
