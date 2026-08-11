# ADR-0034 | Laisser une spécialisation nommer le rôle qu'elle restreint

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0034-let-a-specialisation-name-the-role-it-narrows.md)

**Statut :** Accepté
**Proposé :** 2026-08-10
**Accepté :** 2026-08-10
**Décideurs :** Reefact

## Contexte

`specialisationOf` nomme un pattern, et l'attribut généré dérive de l'une de deux choses selon
la cible ([ADR-0003](0003-give-each-role-its-own-attribute-nested-in-its-pattern.md),
[ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md)) :

* une cible à **un** rôle est plate, donc la restriction dérive d'attribut à attribut —
  `TestStubAttribute : TestDoubleAttribute`. Précision maximale ;
* une cible à **plusieurs** rôles est un conteneur, donc la restriction dérive de la base
  abstraite `Role` — `WireTapAttribute : RecipientList.Role`. Elle répond comme *un participant
  d'*une recipient list plutôt que comme la recipient list elle-même.

Vingt-quatre relations sont livrées aujourd'hui : quinze précises et neuf grossières. Les neuf
sont `SecondaryPostingRule`, les quatre restrictions d'accountability, les trois intentions de
message et `WireTap`.

[L'ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.md) a accepté la forme
grossière plutôt que de la contourner, et a rangé le correctif dans ses *Alternatives
envisagées* avec le schéma exact qu'il demanderait —
`{"catalog": …, "name": "Message", "role": "Message"}`. Il l'a ajourné sur un seul motif :
*l'imprécision qu'il corrige n'a encore rien coûté à personne*.
[L'ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md) a ensuite retiré le
crochet inutilisé du générateur, en disant expressément qu'une alternative ajournée est
réimplémentée si elle est un jour prise.

**Elle vient de coûter quelque chose.** Le catalogage du groupe *Transactional messaging* de
*Microservices Patterns* a produit une relation que l'œuvre énonce noir sur blanc et que les
données ne savent pas porter du tout :

> There are two patterns for implementing the Message relay: the Transaction log tailing
> pattern, the Polling publisher pattern.

`MessageRelay` est l'un des quatre rôles de `TransactionalOutbox`, à côté de `Sender`,
`Database` et `MessageOutbox`. Les deux ne sont donc pas des restrictions du pattern — un
polling publisher n'est pas une sorte de transactional outbox — ce sont deux façons d'être
**l'un de ses participants**.

C'est un échec d'une autre nature que celui des neuf. Là où l'œuvre dit *le pattern A est une
sorte du pattern B*, dériver de `B.Role` est vrai et seulement grossier : un command message
est réellement un message. Là où l'œuvre dit *le pattern A est une façon d'être le rôle R du
pattern B*, il n'existe aucun énoncé vrai au niveau du pattern à enregistrer, et en enregistrer
un surinterpréterait l'œuvre — ce que l'ADR-0030 existe précisément pour empêcher. Rien n'a
donc été enregistré, et la relation a d'abord été écrite en prose dans `catalog/README.md` —
ce qui est à l'origine de ce document.

L'alternative ajournée a donc désormais deux usages démontrés au lieu d'aucun : elle
**affinerait** neuf relations déjà émises, et elle **rendrait possibles** deux relations qui ne
peuvent pas l'être du tout.

## Décision

`specialisationOf` peut nommer un **rôle** de la cible en plus du pattern :

```json
"specialisationOf": { "catalog": "MicroservicesPatterns", "name": "TransactionalOutbox", "role": "MessageRelay" }
```

Quand `role` est donné, l'attribut du pattern restreignant dérive de **l'attribut de ce rôle**,
qui est émis non scellé :

```csharp
public sealed class PollingPublisherAttribute : TransactionalOutbox.MessageRelayAttribute { }
```

Quand `role` est omis, rien ne change : les deux formes existantes demeurent, et aucune relation
déjà livrée n'est réécrite par cette décision.

## Justification

Le mécanisme de relation existe pour porter un énoncé d'auteur, et il y a deux sortes d'énoncés
de restriction dans les œuvres cataloguées ici. L'un est *ce pattern est une sorte de celui-là*,
que la forme actuelle porte. L'autre est *ce pattern est l'un des participants de celui-là*,
qu'elle ne porte pas du tout. Ajouter un champ est un changement plus petit que laisser toute
une catégorie d'énoncés d'auteur inexprimable.

La précision n'est pas décorative. Ce qu'une relation achète, c'est qu'une règle écrite pour le
plus large atteigne le plus étroit sans le nommer. Aujourd'hui un consommateur qui demande tous
les message relays — *est-ce que quelque chose vide cet outbox ?* — ne trouve rien, parce que
les deux réponses sont des types sans lien. C'est exactement la question dont ce groupe parle.

Le coût est borné et connu. `sealed` sur un attribut de rôle ne fait pas partie de la baseline
d'API publique, donc aucune baseline n'est réécrite ; la remontée d'identité est inchangée,
parce qu'un attribut de rôle n'est ni abstrait ni déclaré dans le pattern restreignant, si bien
que la remontée s'arrête sur l'attribut le plus étroit exactement comme aujourd'hui (la règle de
[l'ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.md), telle que reformulée sur
`DesignPatternAttribute`) ; et le test de convention
`A_role_is_sealed_unless_something_derives_from_it` a été écrit pour permettre précisément ce
cas — il exige *sealed* **sauf** si quelque chose dérive, ce qui explique qu'il survive sans
retouche.

Les neuf relations grossières ne sont **pas** rétro-adaptées par cette décision. L'ADR-0030
continue de gouverner ce qui peut être enregistré : un rôle est nommé là où l'œuvre en nomme un,
et non parce que le mécanisme le permet désormais. Chacune des neuf est revue sur ses propres
preuves, ou pas du tout.

## Alternatives envisagées

### Laisser la relation en prose

Le statu quo, et ce que la branche qui a trouvé le cas a fait d'abord plutôt que de trancher
unilatéralement : la relation écrite en paragraphe dans `catalog/README.md`, où un lecteur de ce
fichier la trouve et où rien d'autre ne la trouve.

Rejeté. La prose est le bon endroit pour ce que les données **ne peuvent pas** dire ; c'est un
mauvais endroit pour ce qu'elles sont à un champ de pouvoir dire. Et c'est l'asymétrie qui rend
la chose fausse ici : le catalogue porterait *command message est un message* en héritage et
*polling publisher est un message relay* en paragraphe, alors que l'auteur énonce les deux sur
le même ton.

### Relier au pattern et accepter la sous-spécification

`PollingPublisherAttribute : TransactionalOutbox.Role`, ce que le mécanisme actuel émettrait.

Rejeté, et c'est l'alternative sur laquelle il faut être précis, parce que c'est exactement ce
que l'ADR-0030 a **accepté** pour les neuf autres. Cela marche là-bas et échoue ici.
`CommandMessage` dérive de `Message.Role` et répond comme *un participant d'un message*, ce qui
est vrai et seulement moins que toute la vérité. `PollingPublisher` dérivant de
`TransactionalOutbox.Role` ferait écrire au générateur *« A narrower case of TransactionalOutbox:
every participant annotated here is one of those too »* — or un polling publisher n'est pas un
cas particulier d'un transactional outbox. L'enregistrer serait la surinterprétation que
l'ADR-0030 interdit.

### Ne changer que la phrase générée

Garder `<Target>.Role` et réécrire la documentation émise, pour qu'elle dise *un participant de
X, sans dire lequel de ses rôles* plutôt que *un cas particulier de X*.

Rejeté, bien que ce soit l'option la moins chère et qu'elle corrige effectivement la prose. Elle
laisse la revendication au niveau des types intacte, donc un consommateur qui demande tous les
message relays ne trouve toujours rien — et une règle qu'on peut écrire vaut ici davantage
qu'une phrase honnête. À faire tout de même, séparément, si ce document est refusé.

### Dériver du rôle homonyme par défaut, sans changer le schéma

Dans chacune des neuf relations grossières, le rôle visé est celui qui porte le nom du pattern —
`Message.Message`, `RecipientList.RecipientList`, `Accountability.Accountability`,
`PostingRule.PostingRule`. Le générateur pourrait donc simplement préférer ce rôle à `Role`
quand la cible en a un, et aucun champ de schéma ne serait nécessaire.

Rejeté sur deux points. Cela change en silence le sens de neuf relations déjà livrées, ce qui
est la seule chose dont l'ADR-0030 prévient qu'elle est irréversible une fois qu'un consommateur
a écrit une règle dessus. Et cela ne résout pas le cas à l'origine de ce document :
`TransactionalOutbox` n'a pas de rôle appelé `TransactionalOutbox`, donc le défaut ne se
déclencherait pas et la relation de l'outbox resterait inécrivable. C'est une commodité pour les
neuf faciles qui rate les deux difficiles.

### Faire de Message relay un pattern à part entière

Promouvoir le rôle en entrée, pour que la spécialisation de pattern à pattern fonctionne sans
changement.

Rejeté. L'œuvre le présente comme l'un des quatre participants d'un pattern numéroté, pas comme
un pattern ; inventer une entrée pour que le mécanisme tombe juste, c'est le catalogue qui dit
au livre ce qu'il a dit. Un pattern est tenu sous le nom et sous la forme que son œuvre lui a
donnés ([ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md)).

## Conséquences

### Positives

* Un énoncé d'auteur qui porte sur un pattern et un **rôle** devient exprimable, là où seuls les
  énoncés portant sur deux patterns l'étaient.
* Une règle peut demander toutes les implémentations d'un participant — *est-ce que quelque
  chose vide cet outbox ?* — et obtenir une réponse du système de types plutôt que d'un
  paragraphe.
* Neuf relations existantes deviennent affinables, une par une, chacune sur les preuves que
  l'ADR-0030 exige.

### Négatives

* Une deuxième forme d'émission pour un seul mécanisme de relation, ce qui est ce que l'ADR-0030
  avait mis dans la balance contre elle. Trois formes dépendent maintenant de la cible :
  attribut plat, base `Role`, rôle nommé.
* Le générateur récupère une machinerie que l'ADR-0031 avait retirée. C'est l'issue que
  l'ADR-0031 décrivait plutôt qu'une contradiction, mais cela signifie qu'un crochet est
  réécrit quinze jours après avoir été supprimé, et la suppression était juste à l'époque.
* Un attribut de rôle non scellé est une surface publique plus large qu'un attribut scellé :
  n'importe quoi peut désormais dériver de `MessageRelayAttribute`, y compris du code extérieur
  à ce dépôt.

### Risques

* **Propriétés de lien héritées.** Un attribut de rôle qui porte des liens —
  `MessageRelayAttribute` a `MessageOutbox` — les transmet à ce qui en dérive, si bien que
  `[PollingPublisher]` accepte désormais un argument que sa propre entrée ne déclare pas.
  Vérifié plutôt que supposé avant l'acceptation de ce document : l'analyseur veut des symboles
  déclarés, un membre hérité n'en est pas un, et la baseline est inchangée par la relation. La
  surface a tout de même grandi, et elle a grandi là où le catalogue ne la montre pas.
* **Cibles incohérentes.** Rien n'empêcherait une restriction qui vise `Method` de nommer un
  rôle qui ne vise que `Class`. Le code généré compile et l'assertion n'a pas de sens.
* **Une relation est une promesse.** Une fois que `[PollingPublisher]` répond comme un message
  relay, un consommateur peut écrire une règle dessus, et retirer la relation ensuite le casse.
  Même avertissement que l'ADR-0030, et il porte davantage ici parce que la relation est plus
  fine.

## Actions de suivi

Toutes sont réalisées dans la pull request qui enregistre cette décision, plutôt que remises à
plus tard : le cas qui a provoqué ce document est dans la même branche, et une décision acceptée
dont l'implémentation est différée est la façon dont un catalogue et son générateur se
désynchronisent.

* Ajouter `role` à `patternRef` dans `catalog/pattern.schema.json`, optionnel.
* Apprendre deux règles au validateur : le rôle nommé doit exister sur la cible, et les cibles
  d'un rôle du pattern restreignant ne doivent pas dépasser celles du rôle qu'il restreint.
* Rétablir le crochet de descellement du générateur, cette fois piloté par le catalogue plutôt
  que par un ensemble vide — un rôle est émis non scellé exactement quand une entrée le nomme.
* Vérifier la baseline d'API publique d'un paquet dont une entrée restreint un rôle porteur de
  liens, avant que la première entrée de ce genre soit committée.
* Enregistrer les deux relations à l'origine de ce document — `PollingPublisher` et
  `TransactionLogTailing` sur `TransactionalOutbox.MessageRelay` — et retirer le paragraphe de
  `catalog/README.md` qui en tenait lieu.

## Références

* [ADR-0030](0030-relate-only-the-narrowings-a-work-states-outright.md) — a ajourné exactement
  cette alternative, avec le schéma qu'elle demanderait, et continue de gouverner *ce qui* peut
  être enregistré.
* [ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md) — a retiré la
  machinerie, en disant qu'elle est réimplémentée si l'alternative est prise.
* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) — la relation est
  émise sous forme d'héritage, ce qui est la raison pour laquelle la forme de la cible compte.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — le test qui sépare
  *est une sorte de* et *est un participant de*.
* `catalog/README.md` — où le groupe est décrit, et où se trouvait le paragraphe que ce document
  remplace.
