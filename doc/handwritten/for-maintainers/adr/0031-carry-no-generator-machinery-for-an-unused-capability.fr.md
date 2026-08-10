# ADR-0031 | Ne porter dans le générateur aucune mécanique pour une capacité inutilisée

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0031-carry-no-generator-machinery-for-an-unused-capability.md)

**Statut :** Accepté
**Proposé :** 2026-08-10
**Accepté :** 2026-08-10
**Décideurs :** Reefact

## Contexte

Le générateur scellait chaque attribut de rôle conditionnellement : il tenait un ensemble de
rôles à émettre non scellés, le consultait au moment de choisir le modificateur de chaque
rôle, et n'y mettait jamais rien. Tous les rôles ont donc toujours été émis `sealed`, par une
branche dont l'autre côté était inatteignable.

Cet ensemble existait pour une capacité que le catalogue n'a pas. Un `specialisationOf` nomme
un pattern, jamais un rôle : rien ne peut dériver d'un rôle isolé et rien n'a besoin qu'il
soit non scellé.
[ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.md) a envisagé qu'une
relation nomme un rôle — `CommandMessageAttribute : Message.MessageAttribute` plutôt que
`: Message.Role` —, l'a reporté, et a laissé cette question derrière : cet ensemble est-il du
code mort, ou la couture dont cette alternative aurait besoin ?

Le retirer ne change aucun fichier généré. C'est le fait qui tranche : la branche n'avait
qu'un côté atteignable, donc la supprimer et écrire `sealed` directement produit une sortie
identique octet pour octet sur les 212 patterns.

La difficulté de ce dépôt est énoncée dans son propre guide : les attributs sont générés,
donc un lecteur de la sortie ne peut pas distinguer un trait décidé d'un trait incident. Une
condition toujours fausse est le même problème à l'intérieur du générateur — elle se lit
comme une décision prise à propos des rôles, et ce n'en est pas une.

## Décision

Le générateur ne porte aucune mécanique pour une capacité qu'aucune entrée du catalogue
n'exerce ; une alternative reportée est réimplémentée si elle est un jour retenue.

## Justification

Une couture gardée au chaud pour une alternative reportée est un pari : que l'alternative
arrive, et que ce soit cette forme-là dont elle ait besoin. Les deux moitiés sont douteuses
ici : les relations visant un rôle peuvent ne jamais être retenues, et si elles le sont, ce
qu'elles demandent est un changement du schéma, du validateur et de l'émission de la base —
dont le scellement du rôle visé est la plus petite part et la plus facile à ajouter. La
mécanique n'économisait rien qui vaille la confusion qu'elle créait.

Cette confusion est le vrai coût. Rien dans le générateur n'est testé contre un cas qui ne
peut pas survenir, donc la branche fausse n'était pas seulement inutilisée mais
invérifiable : si elle avait été fausse, rien ne l'aurait dit. Un contributeur qui reprendrait
l'alternative reportée aurait dû la lire, décider s'il lui fait confiance, et la tester quand
même — c'est-à-dire tout le travail qu'elle semblait épargner.

La suppression s'affirme au lieu de s'espérer, parce que la sortie générée est versionnée :
regénérer après retrait laisse l'arbre propre, ce qui est le contrôle permanent du dépôt sur
la cohérence entre catalogue et sources, appliqué ici comme preuve que la branche était
morte.

## Alternatives envisagées

### Garder l'ensemble et ajouter un test qui l'exerce

Donner au générateur un test qui dé-scelle un rôle et vérifie l'émission, pour que la branche
cesse d'être invérifiable tout en gardant la couture.

Écartée : cela teste une capacité que le catalogue ne peut pas demander, donc le test affirme
un comportement du générateur sur une entrée qu'aucun fichier de catalogue ne peut produire.
C'est de la mécanique qui garde de la mécanique, et cela fait passer l'alternative reportée
pour décidée.

### La garder en signalant par un commentaire qu'elle est inutilisée

Le moins cher, et cela adresse directement la confusion.

Écartée : un commentaire expliquant pourquoi du code inatteignable est là est une odeur contre
laquelle ce dépôt a tranché ailleurs — la base ADR existe pour que le raisonnement vive dans
des enregistrements plutôt qu'en aparté, et une branche inatteignable commentée reste une
branche inatteignable.

## Conséquences

### Positives

* La règle de scellement du générateur est désormais énoncée plutôt que calculée : le
  modificateur d'un pattern est conditionnel parce qu'un pattern peut être restreint, celui
  d'un rôle ne l'est pas parce qu'un rôle ne peut pas être nommé.
* Aucun fichier généré ne change, donc le retrait est vérifiable par le contrôle d'aller-retour
  que le dépôt exécute déjà.

### Négatives

* Si les relations visant un rôle sont un jour retenues, ce petit morceau est réécrit. Le coût
  est de quelques lignes, payé par celui qui change déjà le schéma et le validateur pour la
  même fonctionnalité.

### Risques

* Aucun identifié. La branche retirée était inatteignable depuis toute entrée du catalogue, et
  la sortie générée est versionnée : une erreur se serait vue en arbre sale.

## Actions de suivi

* Aucune. La question laissée ouverte par ADR-0030 sur ce crochet est réglée par cet
  enregistrement.

## Références

* [ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.md) — a reporté les
  relations visant un rôle et laissé cette question derrière.
* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) — le
  générateur existe pour que la forme d'un attribut soit écrite une seule fois.
