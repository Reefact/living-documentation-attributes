# ADR-0006 | Cataloguer un pattern là où l'ouvrage qui l'a nommé l'a placé

🌍 🇬🇧 [English](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-05
**Accepté :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Les patterns viennent de livres et d'articles publiés sur trois décennies, et le
catalogue est organisé le long de ces corpus : un espace de noms par catalogue,
qui est aussi ce que parcourt un lecteur.

Un pattern n'est pas toujours là où un lecteur l'attend. Fowler nomme *Special
Case* ce qu'une bonne part de l'industrie appelle *Null Object*. Le même pattern
est parfois catalogué par deux ouvrages, parfois sous deux noms.

Placer un pattern là où il est le mieux connu a été envisagé et aurait signifié le
nommer comme le fait l'industrie plutôt que comme le fait sa source — mettre
`NullObject` sous Patterns of Enterprise Application Architecture, là où Fowler a
écrit `SpecialCase`.

Que deux patterns soient le même se tranche séparément, par les assertions qu'ils
portent (ADR-0007). Lorsqu'ils sont le même et sont catalogués deux fois, l'un des
deux doit détenir la définition et l'autre en dériver (ADR-0005), et rien dans
leur sens ne les ordonne : ils disent la même chose.

Le catalogue est appelé à couvrir bien d'autres ouvrages : la question du
placement n'est donc pas tranchée une fois, elle se repose pour chaque pattern
ajouté.

## Décision

Un pattern est catalogué dans le corpus qui l'a nommé, sous le nom que cet ouvrage
lui a donné, et lorsque deux ouvrages nomment le même pattern, c'est la
publication la plus ancienne qui détient la définition.

## Justification

La provenance est un fait, la notoriété n'en est pas un. Quel ouvrage a introduit
un pattern sous quel nom est vérifiable et stable ; quel nom est le mieux connu
varie selon les communautés et les décennies, et un catalogue organisé là-dessus
demanderait à être réarrangé à mesure que les usages se déplacent. Placer par
provenance fait en outre répondre au catalogue la question qu'a réellement le
lecteur d'un livre — *le pattern que je viens de lire est-il ici* — plutôt qu'une
question sur les habitudes de l'industrie.

Cela ne coûte rien en découvrabilité, parce que c'est à cela que sert une
déclinaison. Un lecteur qui connaît le pattern sous un autre nom le trouve sous ce
nom, dans le catalogue qu'il lit, sous la forme d'un attribut dérivant de la
définition. Placement par provenance et découvrabilité par déclinaison sont deux
mécanismes séparés, et aucun n'a à être sacrifié à l'autre.

L'antériorité ordonne ce que le sens ne peut pas ordonner. Quand deux ouvrages
disent la même chose, aucun argument tiré du contenu ne peut en préférer un, et
tout autre départage — le plus cité, celui que préfère le mainteneur — est un
jugement qui serait rouvert. L'ordre de publication est un fait, consigné et
vérifiable : le schéma exige l'année, et une déclinaison dont la définition aurait
été publiée plus tard est rejetée.

La référence cesse dès lors d'être éditoriale. C'est elle qui fixe le sens d'un
héritage, et donc la forme de l'API publique — d'où le fait qu'elle soit exigée
plutôt qu'appréciable, et qu'elle doive être exacte plutôt qu'approximative.

Lorsque deux ouvrages sont contemporains, le départage est le nom le plus employé,
et il est consigné dans le catalogue comme une décision plutôt que calculé depuis
une date. Le générateur applique ce qui est écrit ; il n'arbitre pas.

## Alternatives envisagées

### Cataloguer un pattern sous le nom sous lequel il est le mieux connu

Envisagé parce que c'est ce qu'un développeur tape, et qu'un éditeur cherche des
noms de types.

Rejeté parce que cela fait affirmer au catalogue quelque chose de faux sur la
provenance — Fowler n'a pas écrit `NullObject` — et parce qu'il faudrait y revenir
à mesure que l'usage bouge. La découvrabilité ainsi achetée l'est autrement par
les déclinaisons, qui ne coûtent rien en vérité.

### Donner la définition au plus connu des deux ouvrages

Envisagé parce que cela place l'identité canonique là où la plupart des
consommateurs s'attendraient à la trouver.

Rejeté parce que c'est un jugement et non un fait : cela invite à rouvrir le débat
pattern par pattern, et cela fait dépendre l'identité d'un pattern de quelque
chose qui change.

### Laisser le générateur déduire le sens depuis les années consignées

Envisagé parce que les années sont dans le catalogue et que la règle est
mécanique.

Rejeté parce qu'une date contestée ou égale ferait choisir le générateur en
silence, et parce que la décision que deux patterns sont le même est de toute
façon humaine — en consigner l'issue la garde relisible, là où la déduire la cache
dans une arithmétique.

## Conséquences

### Positives

* Le lecteur d'un livre trouve ses patterns sous son nom, écrits comme il les a
  écrits.
* L'organisation du catalogue repose sur des faits plutôt que sur une lecture de
  l'industrie.
* Le sens de chaque déclinaison est justifié par quelque chose de consigné et de
  vérifiable.
* Un lecteur qui connaît un autre nom trouve tout de même le pattern là où il
  regarde.

### Négatives

* L'identité canonique d'un pattern n'est parfois pas le catalogue auquel on
  l'associe en pratique, ce qui surprendra qui lira un rapport groupé.
* Chaque entrée doit porter une référence exacte, année comprise, ce qui relève de
  la recherche plutôt que de la transcription.

### Risques

* Une référence mal datée inverse silencieusement une déclinaison. Le schéma
  vérifie l'ordre mais ne peut pas vérifier les dates elles-mêmes.
* Les dates de publication sont parfois contestées ou difficiles à rattacher à une
  année. La règle de départage couvre l'égalité, non le désaccord, et un cas
  litigieux doit être argumenté dans l'entrée du catalogue.

## Actions de suivi

* Maintenir en place le rejet, par le schéma, d'une déclinaison antidatée à mesure
  que des catalogues sont ajoutés.

## Références

* [ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.md) —
  la relation que ceci ordonne.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.md) — ce qui
  doit être tranché avant que cette règle ne s'applique.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md) — où va
  un pattern qu'aucun ouvrage ne revendique.
