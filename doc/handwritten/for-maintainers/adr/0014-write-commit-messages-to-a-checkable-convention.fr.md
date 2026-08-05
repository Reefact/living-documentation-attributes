# ADR-0014 | Écrire les messages de commit selon une convention qu'un script peut vérifier

🌍 🇬🇧 [English](0014-write-commit-messages-to-a-checkable-convention.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Le dépôt n'a aucune convention de commit. Son historique se lit comme de la prose
écrite selon la norme que l'auteur de chaque commit portait à ce moment-là.

Une grande part de ce que produit ce dépôt est générée : une modification du
gabarit ou du catalogue réécrit donc un très grand nombre de fichiers d'un coup.
Le message de commit est souvent le seul endroit qui dise lequel des deux s'est
produit et pourquoi — un diff couvrant tout le catalogue a la même allure qu'un
rôle ait été ajouté ou que l'émission ait changé.

Les décisions sont consignées dans la base d'ADR, et le contrôle ADR s'exécute par
pull request ; l'historique des commits est la granularité plus fine en dessous,
et c'est ce que lit un mainteneur lorsqu'il bissecte ou qu'il se demande pourquoi
un fichier a l'allure qu'il a.

Une convention qui n'est pas vérifiée se dégrade. C'est consigné ailleurs dans ce
dépôt à propos des règles de codage, et cela vaut pour les messages : l'effort d'en
écrire un correctement est invisible, et rien ne le récompense au moment où on
l'escamote.

## Décision

Tout commit hors fusion suit Conventional Commits, validé par un script unique
partagé entre un hook local `commit-msg` et un contrôle par pull request.

## Justification

Un script plutôt que deux implémentations est ce qui fait tenir l'ensemble. Un
hook et un workflow qui encodent chacun les règles finiront par diverger, et le
désaccord se manifeste par un commit qui passait en local et échoue à l'entrée —
ce qui apprend aux contributeurs à se méfier du contrôle local plutôt qu'à
corriger le message. Partager le script rend la divergence impossible plutôt
qu'improbable.

Vérifier en CI autant qu'en local est ce qui rend le hook digne d'être activé. Un
hook est facultatif et contournable : il ne peut donc pas être l'exécution de la
règle ; il en est le retour rapide. Le contrôle de pull request est l'exécution,
et il attrape `--no-verify`.

L'en-tête est validé en entier et les corps sont laissés tranquilles. L'en-tête
est là où se trouve la valeur — c'est lui que lisent un journal, une bissection et
un changelog — et il est assez court pour avoir une forme sans ambiguïté. Un corps
est de la prose, et une règle sur de la prose serait une règle de style plutôt
qu'une règle d'information.

Deux règles de pied de message survivent à ce raisonnement. Une rupture de
compatibilité doit être signalée à la fois par `!` et par un pied `BREAKING
CHANGE:`, parce que chacun pris seul se manque — le `!` par un lecteur qui
survole, le pied par tout ce qui ne lit que les en-têtes — et parce qu'une version
qui promet la compatibilité tout en la rompant est l'erreur la plus coûteuse que
puisse commettre une bibliothèque de types publics. Un pied d'issue n'est vérifié
que dans sa forme, afin que les références restent lisibles par une machine.

Les scopes sont validés mais non exigés. En exiger un sur chaque changement
demanderait aux auteurs d'inventer un composant pour des changements qui n'en
touchent aucun. L'ensemble est fermé afin qu'un scope reste un composant plutôt
que de devenir un champ de texte libre, ce qui est ce qui le rendra utilisable
plus tard pour aiguiller des notes de version.

## Alternatives envisagées

### Adopter un linter tout fait, tel que commitlint

Envisagé parce que la convention est standard et que l'outil est maintenu par
d'autres.

Rejeté parce qu'il apporte une chaîne d'outils Node dans un dépôt dont l'outillage
se limite à .NET et à un script Python, pour un jeu de règles d'une centaine de
lignes. Le script partagé permet en outre d'écrire les diagnostics exacts de ce
dépôt — en nommant ses scopes, en expliquant pourquoi une règle existe — là où un
linter générique rapporte des identifiants de règles.

### N'imposer la convention qu'en CI, sans hook local

Envisagé parce que la CI est là où vit réellement l'exécution de la règle, et
qu'un hook est une chose de plus à activer par clone.

Rejeté parce que cela reporte toute violation après l'écriture du commit, quand la
corriger suppose un rebase interactif plutôt que l'édition du message qu'on a sous
les yeux. Le hook coûte un `git config` et supprime presque tout cela.

### Continuer d'écrire les messages au jugé, sans convention

Envisagé parce que l'historique récent est déjà écrit avec soin, et qu'une
convention ajoute de la cérémonie à chaque commit.

Rejeté parce que le jugement est précisément ce qui ne survit pas à un dépôt qui
croît d'un ordre de grandeur, et parce que la valeur d'une convention ne tient pas
à un message en particulier mais au fait que tout l'historique s'interroge de la
même façon.

## Conséquences

### Positives

* L'historique énonce de quelle nature est chaque commit, et quel composant il a
  touché, sous une forme qu'un outil sait lire.
* Une rupture de compatibilité ne peut pas être signalée à moitié.
* Le contrôle local et le contrôle imposé ne peuvent pas diverger.

### Négatives

* Chaque commit supporte le coût de la convention, y compris les plus triviaux.
* La liste des scopes doit être maintenue à mesure que le dépôt gagne des
  composants, et une liste périmée rejette un message légitime.

### Risques

* La convention n'est imposée que sur l'en-tête : un corps peut donc encore ne
  rien dire d'utile. Rien ici n'y change quoi que ce soit, et rien ne devrait
  essayer.

## Actions de suivi

* Réexaminer si un scope doit devenir obligatoire sur les types qui pilotent la
  version, si des notes de version venaient à être aiguillées par scope.

## Références

* `CONTRIBUTING.md` — la convention telle qu'un auteur la lit.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.md) — l'autre
  contrôle par pull request.
