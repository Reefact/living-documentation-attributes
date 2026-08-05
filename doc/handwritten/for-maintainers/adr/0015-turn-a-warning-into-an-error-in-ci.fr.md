# ADR-0015 | Transformer un avertissement en erreur dans la CI

🌍 🇬🇧 [English](0015-turn-a-warning-into-an-error-in-ci.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

La solution compile sans aucun avertissement sur les six frameworks cibles.

Presque tout ce qu'elle compile est généré depuis un unique gabarit : un
avertissement introduit à cet endroit n'apparaît donc pas une fois, il apparaît
dans chaque pattern du catalogue, lequel se dirige vers plusieurs centaines. Un
journal de build portant des centaines d'avertissements identiques est un journal
que personne ne lit, et le premier avertissement véritable qui le suit est
invisible.

Les compteurs d'avertissements ne restent pas en place. Ils ne bougent jamais que
dans un sens tant que rien ne les retient, parce que chaque avertissement pris
isolément est défendable au moment où il est ajouté et qu'aucun ne vaut à lui
seul qu'on bloque un changement.

La librairie est livrée à des consommateurs qui compilent contre elle. Un
avertissement dans la déclaration d'un attribut public — un usage obsolète, une
incohérence de nullabilité, une référence de documentation qui ne résout pas —
atteint leur build, pas seulement le nôtre.

## Décision

Un avertissement fait échouer le build dans la CI, et reste un avertissement en
local.

## Justification

Poser le cliquet à zéro est le seul moment où cela est gratuit. Chaque
avertissement existant devrait être trié d'abord si l'état avait déjà dérivé ;
depuis zéro il n'y a rien à nettoyer, et la décision ne coûte rien à adopter.

Le multiplicateur du code généré est ce qui rend cela plus rentable ici que dans
un dépôt ordinaire. Un avertissement qui serait un désagrément dans un fichier
écrit à la main devient plusieurs centaines de lignes identiques dans un
catalogue généré : le signal se dégrade donc beaucoup plus vite et se rétablit
beaucoup plus lentement.

Le garder consultatif en local protège la boucle courte. Un changement à moitié
fait doit rester compilable et exécutable tant qu'il est à moitié fait ; faire
mordre le cliquet à la frontière de la pull request donne la garantie sans rendre
l'itération hostile.

Les deux interrupteurs sont nécessaires parce qu'ils couvrent des producteurs
différents : l'un promeut les diagnostics du compilateur, l'autre les
avertissements levés par les tâches de build. Avec le premier seul, un
avertissement émis par une tâche est encore fusionné sans qu'on le remarque — ce
qui est le mode de défaillance que ceci existe pour supprimer, arrivant par une
porte laissée ouverte.

Les avis de sécurité sont exclus délibérément. Un avis publié dans la nuit contre
une dépendance ferait sinon passer au rouge chaque pull request sans que rien
n'ait changé dans le dépôt, ce qui entraîne les contributeurs à ignorer un build
rouge. L'avis apparaît toujours dans le journal, et y donner suite est un
changement à part entière plutôt qu'un blocage de travaux sans rapport.

## Alternatives envisagées

### Échouer sur les avertissements partout, y compris en local

Envisagé parce que c'est plus simple à expliquer, et que cela supprime toute
fenêtre pendant laquelle un avertissement existe.

Rejeté parce que cela rend l'itération hostile : une variable temporairement
inutilisée dans un changement à moitié écrit arrête le build, de sorte que la
friction retombe sur le moment de la réflexion plutôt que sur celui de la
proposition.

### Laisser les avertissements comme avertissements et s'en remettre à la revue

Envisagé parce que le compteur actuel est à zéro et que l'équipe est petite.

Rejeté parce qu'un relecteur qui lit un diff voit le code, pas le journal de
build, et parce qu'un avertissement introduit dans un gabarit n'est pas visible
dans le diff du tout — il est visible dans plusieurs centaines de fichiers que
personne n'ouvre.

### Ne promouvoir que l'interrupteur du compilateur

Envisagé parce que c'est le plus connu et qu'il couvre les diagnostics auxquels
on pense.

Rejeté parce qu'il laisse silencieusement les avertissements du SDK et du
packaging hors du cliquet : la garantie serait plus étroite qu'elle ne se lit.

## Conséquences

### Positives

* L'état sans avertissement est verrouillé plutôt que maintenu par la vigilance.
* Un avertissement introduit par le gabarit est attrapé une fois, à la frontière,
  plutôt que multiplié sur le catalogue.
* Les consommateurs n'héritent pas d'un avertissement issu d'une déclaration
  livrée.

### Négatives

* Un changement légitime mais qui avertit doit être résolu — supprimé avec une
  raison, ou corrigé — avant de pouvoir être fusionné, même quand
  l'avertissement n'est pas le propos du changement.
* Le build local et celui de la CI se comportent différemment, ce qui surprend
  quiconque n'a pas lu ceci.

### Risques

* La pression à faire taire vite un avertissement peut produire une suppression
  globale plutôt qu'une correction. Rien ici ne l'empêche ; une suppression est
  un changement comme un autre et se relit comme tel.

## Références

* `Directory.Build.props` — là où le cliquet est câblé.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.fr.md) —
  pourquoi un défaut du gabarit atteint tout le catalogue d'un coup.
