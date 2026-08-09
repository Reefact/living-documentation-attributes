# ADR-0027 | Livrer un package indépendant par œuvre cataloguée

🌍 🇬🇧 [English](0027-ship-one-independent-package-per-catalogued-work.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-09
**Accepté :** 2026-08-09
**Décideurs :** Reefact

## Contexte

La bibliothèque livre une seule assembly contenant tous les catalogues. Les namespaces la
partitionnent — `GangOfFour`, `DomainDrivenDesign`, `EnterpriseApplicationArchitecture`,
`AnalysisPatterns`, `AccountingPatterns`, `Idioms` — et un consommateur qui installe le
package les reçoit tous, qu'il en veuille un ou aucun.

Elle porte 140 patterns et 286 rôles, et l'ambition affichée est de croître d'un ordre de
grandeur. Les catalogues ne croissent pas ensemble : `AnalysisPatterns` a pris trente-deux
entrées en deux jours, tandis que `GangOfFour` est à vingt-trois depuis qu'il a été écrit
et n'en bougera pas.

Quatorze entrées portent une relation vers une autre. **Dix restent dans un catalogue.**
**Quatre en traversent un :**

```
DomainDrivenDesign/Repository   → EnterpriseApplicationArchitecture/Repository
DomainDrivenDesign/ValueObject  → EnterpriseApplicationArchitecture/ValueObject
EnterpriseApplicationArchitecture/Money → AnalysisPatterns/Quantity
Idioms/NullObject               → EnterpriseApplicationArchitecture/SpecialCase
```

Une relation est émise en héritage, et l'identité d'un pattern se lit en la remontant.
L'héritage entre assemblies exige une référence d'assembly : une relation qui traverse un
catalogue et une frontière de package qui sépare les catalogues ne peuvent donc pas
coexister.

Les attributs ne portent aucun comportement ni aucune dépendance. Toute la bibliothèque
fait quelques dizaines de kilo-octets d'IL : ce qu'un consommateur transporte aujourd'hui
est négligeable en taille et total en portée.

Rien n'est publié.

## Décision

Chaque œuvre cataloguée est livrée comme son propre package, indépendant des autres, et
aucun pattern n'est relié à un pattern d'une autre œuvre.

## Justification

Les dépendances d'un projet devraient dire ce qu'il utilise. Une base de code qui déclare
du domain-driven design n'a rien à faire du Gang of Four, et la frontière de namespace ne
le procure pas : elle filtre après coup, à l'intérieur d'un fichier, là où un package est
ce qu'un développeur choisit avant d'écrire une ligne.

Les catalogues n'ont en commun que leur forme. Chacun est le vocabulaire d'une œuvre,
appris d'un livre, cohérent parce que ce livre est cohérent. Un développeur adopte une
œuvre, pas une bibliothèque, et l'unité de distribution devrait être l'unité d'adoption.

**C'est l'indépendance qui rend ce choix réel.** Un package par œuvre avec des références
suivant l'antériorité de publication conserverait toutes les relations et serait même
acyclique par construction — une œuvre postérieure restreint une antérieure, jamais
l'inverse, donc le graphe de références serait la chronologie des publications, et le temps
ne boucle pas. C'est rejeté parce que la feuille qu'un développeur choisit est l'œuvre la
*plus récente*, laquelle traînerait les trois antérieures derrière elle. La découpe serait
cosmétique.

Le coût est précis et petit : une règle écrite pour le pattern d'une œuvre cesse
d'atteindre le pattern plus étroit d'une autre. Cela concerne quatre relations sur cent
quarante, et le consommateur qui veut les deux nomme les deux types d'attributs — deux
lignes dans un test écrit contre des attributs dont c'est précisément la raison d'être.
La bibliothèque refuse déjà de livrer un lecteur au motif qu'un consommateur doit écrire
ses règles ; c'est le même raisonnement un cran plus haut.

La taille n'est pas l'argument et ne doit pas être avancée comme tel. Ce sont des attributs
inertes sans dépendance ; la découpe n'économise presque rien en octets. Elle achète
l'intention, et elle achète une cadence de publication par œuvre — aujourd'hui, un
consommateur du `GangOfFour` subit une montée de version pour chaque journée de travail sur
un livre qu'il ne lit pas.

L'indépendance est aussi une **simplification**, et c'est ce qui tranche. Le mécanisme de
déclinaison perd son seul usage — le même pattern nommé par deux œuvres. La règle
d'antériorité perd son objet, puisqu'elle n'existe que pour arbitrer entre deux œuvres qui
nomment un même pattern, et que des œuvres qui ne se rencontrent jamais n'ont rien à
arbitrer. Le reach-back disparaît avec elle : cataloguer un livre de 1997 en quatrième
cesse de fouiller trois catalogues déjà déclarés complets. Les règles de lecture passent de
quatre à trois.

Un package doit malgré tout détenir le marqueur de base. Le lecteur d'un consommateur a
besoin d'un type unique pour trouver toutes les annotations, et un type de base par package
l'obligerait à les connaître tous.

## Alternatives envisagées

### Conserver une seule assembly

Envisagée parce que c'est l'existant, que cela ne coûte rien, et que les namespaces
séparent déjà les catalogues.

Rejetée parce qu'un seul numéro de version fait subir à chaque consommateur le bruit de
tous les catalogues, et parce qu'un namespace n'est pas un choix : un développeur ne peut
pas décliner les patterns d'un livre qu'il n'a pas lu. À dix fois la taille, une assembly
de quatorze cents patterns est un problème de découvrabilité qu'aucune directive `using`
ne règle.

### Un package par œuvre, référençant les œuvres qu'il restreint

Envisagée parce qu'elle préserve toutes les relations, garde l'identité lisible par
remontée, et produit un graphe acyclique par construction, une relation pointant toujours
d'une œuvre postérieure vers une antérieure.

Rejetée parce qu'elle ruine l'objet de la découpe. Quatre relations sur cent quarante
feraient dépendre le catalogue le plus récent, transitivement, de tous les plus anciens :
le développeur qui voulait un vocabulaire en recevrait quatre.

### Garder les relations comme donnée du catalogue, émise dans l'index et non dans l'IL

Envisagée parce qu'elle préserve le travail de comparaison sans le couplage : le JSON porte
déjà la relation, et un index ou un fichier de relations pourrait la publier pour qui veut
dédupliquer.

Rejetée parce qu'une relation que rien ne compile et que rien ne vérifie est exactement ce
que ce dépôt tient pour sans valeur — la prémisse de tout le projet est qu'une information
tenue hors du code dérive. L'outil générique qui consommerait un tel fichier n'a pas été
demandé, et l'inventer est la généralité spéculative que la bibliothèque refuse ailleurs.

### Un package « pont » par paire d'œuvres, rendant le couplage optionnel

Envisagée parce qu'elle laisse la relation à qui la veut et laisse les autres tranquilles.

Rejetée parce que l'identité d'une annotation dépendrait alors des packages installés. Le
même `[ValueObject]` répondrait une chose dans un projet ayant installé le pont et une
autre dans un projet ne l'ayant pas fait, ce qui est pire que l'un ou l'autre bout du
choix.

## Conséquences

### Positives

* Les dépendances d'un projet énoncent les vocabulaires qu'il emploie, et un développeur
  peut décliner une œuvre au lieu de la filtrer.
* Chaque œuvre publie à son rythme, donc un catalogue stable cesse d'hériter du bruit de
  version d'un catalogue actif.
* La règle d'antériorité et le reach-back perdent leur objet. Cataloguer une œuvre
  antérieure après coup n'atteint plus des catalogues déjà terminés — ce qui retire
  l'obligation la plus lourde encore en attente dans ce dépôt.
* Les règles de lecture perdent une clause, et le marqueur `[Declension]` perd sa raison
  d'être.
* « Une relation ne traverse pas un catalogue » devient une règle que le validateur
  vérifie, là où l'équivalent était de la prose.

### Négatives

* Une règle écrite pour le pattern d'une œuvre n'atteint plus le pattern plus étroit d'une
  autre. Le consommateur nomme les deux types.
* N packages, c'est N jeux de notes de version et une question de compatibilité qui
  n'existait pas.
* La même idée sera décrite dans plusieurs catalogues, avec les mots de chaque œuvre, sans
  aucun mécanisme pour remarquer que les descriptions ont divergé.
* Le travail de comparaison déjà fait entre catalogues est abandonné plutôt que réencodé.

### Risques

* Le nombre de packages croît avec l'ambition. Un ordre de grandeur de patterns en plus
  signifie beaucoup plus de packages, et leur nommage comme leur découverte deviennent un
  problème en soi.
* Un consommateur qui installe deux catalogues reçoit deux attributs pour une idée sans que
  rien ne le dise. C'est assumé, non résolu.
* Versionner les packages indépendamment est ce qui achète le bénéfice sur le bruit, et cela
  coûte une matrice de compatibilité. La première version devrait les verrouiller ensemble ;
  relâcher ensuite est facile, resserrer ensuite ne l'est pas.

## Actions de suivi

* Découper les projets : un par œuvre cataloguée, plus un détenant le marqueur de base que
  tous référencent, plus un méta-package pour qui veut l'ensemble.
* Supprimer les quatre relations inter-catalogues, et réécrire les résumés qui citent une
  autre œuvre — `EnterpriseApplicationArchitecture/Money`, et les deux rôles d'horodatage
  d'`AccountingPatterns/Event`.
* Retirer `declensionOf` du schéma et le marqueur de déclinaison des attributs ; ni l'un ni
  l'autre n'a d'usage restant.
* Apprendre à `tools/catalog/validate.py` à refuser une relation dont la cible nomme un
  autre catalogue.
* Réécrire la section du README racine sur les relations autour d'un exemple
  intra-catalogue, et retirer la clause de déclinaison de la quatrième règle de lecture.
* Purger `catalog/README.md` de ses comparaisons inter-catalogues.
* Donner à chaque package sa propre baseline d'API publique, la surface étant désormais
  découpée.

## Références

* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.fr.md) — le
  catalogue est une donnée et les assemblies en sont générées, ce qui fait de ceci un
  changement du générateur et non de cent quarante fichiers à la main.
* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) — sa moitié
  d'antériorité perd son objet ici ; ce que chaque catalogue détient est décidé à neuf dans
  l'[ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.fr.md).
* [ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.fr.md) — la remontée
  d'identité, dont la moitié était la remontée à travers une déclinaison.
* [ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.fr.md) — ce
  qui est versionné, désormais sur N packages et non un.
* [ADR-0025](0025-let-an-earlier-work-reclaim-a-pattern-from-a-later-catalog.fr.md) — le
  reach-back, que cette décision retire.
