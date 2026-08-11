# ADR-0035 | Indexer le langage de patterns, et admettre sur un exposé plutôt que sur une citation du livre

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0035-index-the-pattern-language-and-require-a-write-up.md)

**Statut :** Accepté
**Proposé :** 2026-08-10
**Accepté :** 2026-08-10
**Décideurs :** Reefact

## Contexte

[L'ADR-0033](0033-admit-microservices-patterns-as-a-catalogue.md) a admis *Microservices Patterns*
et, dans ses *Risques*, énoncé une règle pour garder le champ `reference` honnête :

> L'atténuation est le champ `reference`, qui nomme l'œuvre et non l'URL, et la règle qu'une entrée
> n'est ajoutée que là où le site indique que le livre la traite ou que le pattern lui est
> antérieur.

Neuf tranches plus tard, cette règle a été appliquée à cinquante et une pages, et trois choses sont
désormais connues qui ne l'étaient pas à sa rédaction.

**Le premier volet ne se déclenche presque jamais.** Dix des vingt-quatre pages cataloguées avant ce
document renvoient au livre de 2018 dans leur corps, et sept seulement disent qu'il « décrit ce
pattern ». Toutes les autres entrées reposent sur le second volet, ou sur un troisième motif que
l'ADR-0033 n'énonce nulle part — *c'est le sujet d'un chapitre* — employé dans les
[#57](https://github.com/Reefact/design-pattern-catalog/pull/57) et
[#58](https://github.com/Reefact/design-pattern-catalog/pull/58), et qui est une inférence
tirée d'un titre de chapitre plutôt qu'un énoncé de l'auteur.

**Une citation du livre n'apporte rien de ce que cette bibliothèque consomme.** Ce dont une entrée a
besoin, c'est d'un problème, d'une solution et de participants nommés, parce que ce sont eux qui
deviennent des rôles et des assertions. La ligne sur le livre dit au lecteur où lire davantage.
C'est une courtoisie, et elle faisait office de critère.

**Trois patterns sont bloqués, pour deux raisons différentes.** `SelfContainedService` et
`ServicePerTeam` ont des exposés complets — contexte, problème, forces, solution, contexte résultant,
patterns liés — et sont les deux seules des cinquante-trois puces de l'index que l'auteur marque
`new`, ce qui est son propre signal qu'elles sont postérieures au livre. `ConsumerSideContractTest`
n'a aucun exposé : une ligne de glose dans l'index, sur une puce qui pointe vers la page d'un autre
pattern.

Les deux premiers sont bloqués par une règle sur le livre. Le troisième l'est par tout autre chose,
et la différence a été consignée en prose sur deux tranches sans jamais être tranchée.

**L'arithmétique de l'ADR-0033 est fausse elle aussi, et c'est ici qu'un lecteur l'apprend.** Il
énonce 48 patterns en quatorze groupes, avec un compte par groupe. Le recomptage puce par puce donne
**53 puces sur 51 pages distinctes en 15 groupes** — deux pages sont listées deux fois, et un groupe,
*Architectural style*, avait été manqué et se trouve désormais exclu. L'ADR-0033 est un enregistrement
historique et n'est pas édité ; `catalog/README.md` porte les chiffres justes.

## Décision

**L'œuvre que ce catalogue indexe est le langage de patterns de Richardson tel qu'il le publie**,
dont le livre de 2018 est l'édition principale. Trois règles en découlent, et elles remplacent la
phrase citée plus haut.

1. **Une entrée est admise là où l'auteur présente le pattern** : une page de lui portant au moins un
   problème et une solution. Que le livre de 2018 la traite aussi est consigné, non exigé.
2. **Une glose d'index n'est pas un exposé.** Une puce avec une description d'une ligne et sans page
   propre n'admet pas d'entrée, aussi réel que soit le pattern ailleurs.
3. **`reference` reste `Chris Richardson / Microservices Patterns / 2018` pour chaque entrée**, et
   dans ce catalogue cela désigne le langage de patterns sous le titre et l'année de l'édition qui en
   a fixé l'essentiel. `catalog/README.md` nomme chaque entrée que le livre de 2018 ne traite pas,
   pour que l'exception soit énumérée plutôt que sous-entendue.

En conséquence, `SelfContainedService` et `ServicePerTeam` sont admis, et
`ConsumerSideContractTest` ne l'est pas — jusqu'à ce que l'auteur l'expose.

## Justification

Le critère doit être ce que le catalogue consomme réellement. Une entrée est bâtie sur un problème,
une solution et des participants nommés : ce sont eux qui deviennent le résumé, les rôles et les
assertions auxquelles un relecteur peut tenir une pull request. Une phrase renvoyant à un livre n'en
fournit aucun. Neuf tranches d'application de l'ancienne règle ont produit exactement un signal utile
— *y a-t-il un exposé ?* — et une gêne persistante, qui consistait à inventer des motifs pour des
entrées qui allaient manifestement de soi.

Le marqueur `new` renseigne sur la croissance du langage de patterns, pas sur la réalité d'un
pattern. *Service per team* a un contexte citant la loi de Conway, cinq forces, une solution en cinq
clauses et un contexte résultant avec quatre bénéfices et deux inconvénients. Le refuser parce que
l'auteur l'a ajouté après 2018 privilégie la date de publication d'une édition sur l'énoncé actuel
que l'auteur donne de son propre langage de patterns — soit le contraire de ce que demande
[l'ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md), dont la question est
si *l'œuvre présente le pattern*, et l'œuvre le présente à l'instant même.

La barre de l'exposé est là où l'honnêteté se joue vraiment, et elle n'est pas basse. C'est elle qui
a maintenu les rôles de vingt-quatre entrées traçables jusqu'à des participants que l'auteur nomme ;
deux noms seulement dans tout le catalogue sont une invention de ce catalogue, et les deux sont
signalés. Une entrée frappée à partir d'une glose d'index aurait un résumé et des résumés de rôles
tous écrits ici, c'est-à-dire la discipline de provenance abandonnée plutôt qu'étirée.
`AntiCorruptionLayer` — un problème, une solution, et rien d'autre — est la chose la plus maigre qui
passe cette barre, et elle la passe.

La règle 3 est le compromis, et il faut le dire comme tel. Le schéma porte auteur, œuvre et année, et
un langage de patterns vivant n'a pas d'année de publication. Énoncer dans un ADR ce que la référence
signifie pour ce catalogue, et énumérer les exceptions dans `catalog/README.md`, est moins fictif
qu'inventer une année et moins coûteux que réécrire vingt-quatre références — mais cela signifie que
les données seules ne disent pas à un lecteur que le livre de 2018 n'a pas `ServicePerTeam`. La prose
le dit.

## Alternatives envisagées

### Garder la règle de l'ADR-0033 telle quelle

Le statu quo. Son argument était que la référence ne doit pas affirmer ce qui n'est pas étayé, et cet
argument était juste — c'est pour cela que les deux patterns ont été retenus plutôt qu'ajoutés en
silence.

Rejeté parce que la règle échoue dans les deux sens. Elle exclut deux patterns que l'auteur présente
en entier, et elle a été discrètement complétée par un troisième motif — *le sujet d'un chapitre* —
qu'elle n'énonçait pas et qui est plus faible que les deux volets qu'elle énonçait. Une règle
contournée est pire qu'une règle remplacée.

### Réécrire `reference.work` des vingt-quatre entrées pour nommer le langage de patterns

`work: "The Microservice Architecture Pattern Language"`, pour que la référence se suffise à
elle-même et qu'aucune prose ne soit nécessaire pour la garder vraie.

Rejeté, bien que ce soit l'option la plus honnête en ses propres termes et que le mainteneur puisse
la préférer. Elle coûte la réécriture de toutes les entrées existantes et une modification visible de
chaque ligne de documentation générée du paquet, et elle ne résout pas l'année : 2018 resterait
l'année d'un livre, attachée à une œuvre qui n'en a pas. Elle échange de la prose contre du
remue-ménage sans supprimer la convention.

### Admettre aussi `ConsumerSideContractTest`, sur sa glose d'index

Les trois, au motif qu'une glose écrite par l'auteur reste l'auteur, et qu'un lecteur cherchant
*consumer-side contract test* mérite de le trouver — l'argument même que la posture inclusive de
l'ADR-0033 fait valoir pour les homonymes.

Rejeté. La posture inclusive porte sur *quelle œuvre présente un pattern*, pas sur *jusqu'où un
exposé peut être maigre*. Ici, le résumé de l'entrée, ses rôles et chaque assertion seraient écrits
par ce catalogue, et l'unique ligne de l'auteur pointe vers une page décrivant autre chose. C'est
l'alternative à prendre si la barre de la règle 2 se révèle mauvaise ; la prendre, c'est accepter que
les assertions de certaines entrées soient celles du catalogue et non de l'œuvre, et le dire dans
`catalog/README.md`.

### Ajouter un champ d'édition au schéma

`reference` gagne une note optionnelle disant quelle publication porte le pattern, pour que les
données disent ce que la règle 3 laisse à la prose.

Ajourné sur le terrain de
[l'ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md) — faiblement, puisque
deux entrées l'exerceraient aussitôt. À prendre si un second catalogue rencontre le même problème ;
les exceptions d'un seul catalogue tiennent dans un paragraphe.

### Séparer les patterns propres au site dans un second catalogue

`MicroservicesPatterns` pour le livre, un autre paquet pour les ajouts ultérieurs du langage.

Rejeté sur [l'ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) : un paquet par
œuvre catalguée, et c'est une seule œuvre. Cela mettrait aussi deux patterns qu'un lecteur tient pour
ceux de Richardson dans un paquet qu'il n'aura pas l'idée d'installer.

## Conséquences

### Positives

* Le critère devient ce que le catalogue consomme — un exposé — au lieu d'une ligne de courtoisie que
  dix pages sur vingt-quatre portent par hasard.
* Deux patterns aux bonnes assertions sont admis : *aucun appel synchrone pendant le traitement d'une
  requête*, et *exactement une équipe peut modifier ce service*.
* Le troisième motif employé en pratique cesse d'être tacite. Il y a une règle, et c'est celle qu'on
  applique.

### Négatives

* La `reference` de deux entrées nomme une édition qui ne les porte pas. C'est une convention énoncée
  et non un accident, et `catalog/README.md` l'énumère — mais un consommateur qui ne lit que la
  documentation générée ne le saura pas.
* Le catalogue suit désormais un document vivant. L'auteur peut ajouter un pattern demain, et
  *complet* voudra dire complet à une date plutôt que complet par rapport à un livre.

### Risques

* Un pattern que l'auteur retire ou renomme ensuite laisse derrière lui une entrée que plus aucune
  source n'étaye. Rien ne le détecte ; l'atténuation est l'audit de complétude qui a trouvé les
  erreurs de ce document même, refait plutôt que tenu pour acquis.
* La barre de l'exposé est un jugement sur la quantité de texte qui suffit. `AntiCorruptionLayer`
  place le plancher à un problème et une solution ; la prochaine page limite sera plaidée contre lui,
  et c'est à cela que sert un précédent.

## Actions de suivi

* Cataloguer `SelfContainedService` et `ServicePerTeam`, avec leurs exemples, à l'acceptation.
* Garder `ConsumerSideContractTest` dans la section des retenus de `catalog/README.md`, avec la
  règle 2 pour motif plutôt qu'une hésitation.
* Consigner dans `catalog/README.md` les entrées que le livre de 2018 ne traite pas, selon la
  règle 3.

## Références

* [ADR-0033](0033-admit-microservices-patterns-as-a-catalogue.md) — le document dont ce texte
  remplace la phrase des *Risques* ; sa décision, sa posture inclusive sur les homonymes et ses
  critères d'exclusion demeurent.
* [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — demande si l'œuvre
  présente le pattern, question à laquelle la règle 1 rend possible de répondre.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.md) — inchangé : un pattern présenté qu'aucune
  déclaration ne peut porter reste écarté.
* `catalog/README.md` — la section des retenus, et le relevé de ce que le livre ne traite pas.
