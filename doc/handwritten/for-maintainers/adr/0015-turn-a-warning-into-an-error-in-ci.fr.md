# ADR-0015 | Transformer un avertissement en erreur dans la CI

🌍 🇬🇧 [English](0015-turn-a-warning-into-an-error-in-ci.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

La solution compile sans le moindre avertissement sur les six frameworks cibles.

Presque tout ce qu'elle compile est généré depuis un unique gabarit : un
avertissement introduit à cet endroit n'apparaît donc pas une fois, il apparaît
dans chaque pattern du catalogue, lequel se dirige vers plusieurs centaines. Un
journal de compilation portant des centaines d'avertissements identiques est un
journal que personne ne lit, et le premier avertissement véritable qui suit y est
invisible.

Les décomptes d'avertissements ne restent pas en place. Ils ne bougent jamais que
dans un sens à moins que quelque chose ne les retienne, parce que chaque
avertissement pris isolément est défendable au moment où il est ajouté et
qu'aucun, à lui seul, ne vaut de bloquer un changement.

La bibliothèque est livrée à des consommateurs qui compilent contre elle. Un
avertissement dans la déclaration d'un attribut public — un usage obsolète, une
incohérence de nullabilité, une référence de documentation qui ne résout pas —
atteint leur compilation, et pas seulement la nôtre.

## Décision

Un avertissement fait échouer la compilation dans la CI, et reste un avertissement
en local.

## Justification

Enclencher le cliquet depuis zéro est le seul moment où cela est gratuit. Chaque
avertissement existant aujourd'hui devrait d'abord être trié si l'état avait déjà
dérivé ; depuis zéro il n'y a rien à nettoyer, et la décision ne coûte rien à
adopter.

Le multiplicateur du code généré est ce qui rend la mesure plus rentable ici que
dans un dépôt ordinaire. Un avertissement qui serait un désagrément dans un
fichier écrit à la main devient plusieurs centaines de lignes identiques dans un
catalogue généré : le signal se dégrade donc bien plus vite et se rétablit bien
plus lentement.

Le garder consultatif en local protège la boucle courte. Un changement à moitié
fait doit rester compilable et exécutable pendant qu'il est à moitié fait ; faire
mordre le cliquet à la frontière de la pull request donne la garantie sans rendre
l'itération hostile.

Les deux interrupteurs sont nécessaires parce qu'ils couvrent des producteurs
différents : l'un promeut les diagnostics du compilateur, l'autre les
avertissements levés par les tâches de build. Avec le premier seul, un
avertissement émis par une tâche est encore fusionné sans qu'on le remarque — soit
le mode de défaillance que ceci existe pour supprimer, arrivant par une porte
laissée ouverte.

Les avis de sécurité sont exclus délibérément. Un avis publié dans la nuit contre
une dépendance ferait sinon virer au rouge chaque pull request sans que rien
n'ait changé dans le dépôt, ce qui entraîne les contributeurs à ignorer une
compilation rouge. L'avis apparaît toujours dans le journal, et y donner suite est
un changement à part entière plutôt qu'un blocage de travaux sans rapport.

## Alternatives envisagées

### Échouer sur les avertissements partout, y compris en local

Envisagé parce que c'est plus simple à expliquer et que cela supprime toute
fenêtre pendant laquelle un avertissement existe.

Rejeté parce que cela rend l'itération hostile : une variable temporairement
inutilisée dans un changement à moitié écrit arrête la compilation, si bien que la
friction retombe sur le moment où l'on réfléchit plutôt que sur celui où l'on
propose.

### Laisser les avertissements en avertissements et s'en remettre à la revue

Envisagé parce que le décompte actuel est de zéro et que l'équipe est petite.

Rejeté parce qu'un relecteur qui lit un diff voit le code, non le journal de
compilation, et parce qu'un avertissement introduit dans un gabarit n'est pas
visible du tout dans le diff — il est visible dans plusieurs centaines de fichiers
que personne n'ouvre.

### Ne promouvoir que l'interrupteur du compilateur

Envisagé parce que c'est celui que tout le monde connaît et qu'il couvre les
diagnostics auxquels on pense.

Rejeté parce qu'il laisse silencieusement hors du cliquet les avertissements du
SDK et de l'empaquetage : la garantie serait donc plus étroite qu'elle ne se lit.

## Conséquences

### Positives

* L'état sans avertissement est verrouillé plutôt que maintenu par la vigilance.
* Un avertissement introduit par le gabarit est attrapé une fois, à la frontière,
  plutôt que multiplié sur tout le catalogue.
* Les consommateurs n'héritent pas d'un avertissement issu d'une déclaration
  livrée.

### Négatives

* Un changement légitime mais qui avertit doit être résolu — supprimé avec un
  motif, ou corrigé — avant de pouvoir être fusionné, même quand l'avertissement
  n'est pas l'objet du changement.
* Les compilations locale et CI se comportent différemment, ce qui surprend qui
  n'a pas lu ceci.

### Risques

* La pression pour faire taire vite un avertissement peut produire une suppression
  générale plutôt qu'un correctif. Rien ici ne l'empêche ; une suppression est un
  changement comme un autre et est relue comme tel.

## Références

* `Directory.Build.props` — là où le cliquet est câblé.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) —
  pourquoi un défaut du gabarit atteint tout le catalogue d'un coup.
