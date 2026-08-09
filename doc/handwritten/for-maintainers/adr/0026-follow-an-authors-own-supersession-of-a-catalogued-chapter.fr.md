# ADR-0026 | Suivre la mise hors d'usage qu'un auteur prononce sur un chapitre catalogué

🌍 🇬🇧 [English](0026-follow-an-authors-own-supersession-of-a-catalogued-chapter.md) · 🇫🇷 Français (ce fichier)

**Statut :** Accepté
**Proposé :** 2026-08-09
**Accepté :** 2026-08-09
**Décideurs :** Reefact

## Contexte

L'[ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.fr.md) a admis
*Analysis Patterns* de Fowler (1997) au catalogue. Les chapitres 2, 3, 4 et 5 sont
catalogués. Le chapitre 6, *Inventory and Accounting*, est le plus gros du livre avec
quinze sections de patterns, et c'est pour lui qu'un lecteur du livre vient le plus
souvent.

Le compagnon UML que Fowler publie pour ce chapitre porte une note de sa main : une
discussion plus à jour des patterns comptables existe à
`martinfowler.com/apsupp/accounting.pdf`, et « les patterns qui y figurent remplacent
ceux du livre *Analysis Patterns* ». La note figure sur le compagnon du chapitre 6 et
sur aucun autre.

Ce papier est *Accounting Patterns* : soixante-douze pages, son PDF créé le
8 décembre 2000 — postérieur au livre, antérieur à *Patterns of Enterprise Application
Architecture* (2002) et à *Domain-Driven Design* (2003). Ce n'est pas un livre publié ;
c'est un brouillon que Fowler garde sur son propre site.

Son vocabulaire n'est pas celui du livre. Le papier travaille en Accounting Entry,
Accounting Event, Event Type, Posting Rule, Adjustment et Event Process Log. Le
chapitre 6 nomme Account, Transactions, Summary Account, Memo Account, Posting Rules,
Individual Instance Method, Posting Rule Execution et neuf autres. Seul Posting Rule
s'orthographie pareil dans les deux.

L'[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md)
décide deux choses : un pattern est catalogué là où l'œuvre qui l'a nommé l'a mis, et
lorsque deux œuvres nomment le même pattern, **la publication la plus ancienne détient
la définition**. Par la seconde, lue seule, le livre de 1997 détiendrait la définition
face à un papier de 2000.
L'[ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) décide
que l'identité se tranche par les assertions que deux entrées portent, jamais par
leurs noms.

Rien n'est encore publié
([ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.fr.md)).

## Décision

Lorsque l'auteur d'une œuvre cataloguée énonce qu'une œuvre postérieure de sa main en
remplace une partie, le catalogue suit l'œuvre postérieure, et celle-ci est cataloguée
comme un catalogue à part entière.

## Justification

La règle d'antériorité de l'ADR-0006 existe pour empêcher qu'une présentation
postérieure **par quelqu'un d'autre** ne redéfinisse un pattern qu'une œuvre
antérieure a nommé — c'est la défaillance contre laquelle elle a été écrite, et la
raison pour laquelle l'année de référence est porteuse dans le schéma. Un auteur qui
retire son propre modèle n'est pas une présentation concurrente. C'est la même voix
disant que la première avait tort, et lui appliquer l'antériorité transformerait une
règle qui protège la paternité d'une œuvre en une règle contre elle.

Toute la prétention du catalogue est qu'un pattern est ce que l'œuvre dit qu'il est.
Cataloguer quinze sections dont l'auteur énonce qu'elles sont remplacées publierait,
comme vocabulaire courant, un modèle que cet auteur a remplacé — et comme les
attributs sont générés, rien dans la sortie ne permettrait à un lecteur de savoir
lesquelles. C'est exactement la catégorie de fait que ce dépôt garde dans des records
et non dans du code.

L'œuvre postérieure est cataloguée à part plutôt que fondue dans `AnalysisPatterns`
parce que l'*autre* moitié de l'ADR-0006 tient toujours. Un pattern est catalogué là
où l'œuvre qui l'a nommé l'a mis, et le papier est une autre œuvre sous un autre nom.
Mettre `AccountingEntry` sous `AnalysisPatterns` affirmerait que le livre l'a nommé,
ce qu'il ne fait pas, et laisserait un lecteur du papier chercher ses patterns sous le
titre d'un livre.

La date du papier mérite d'être consignée plutôt que traitée comme accessoire. À 2000
il est antérieur aux catalogues d'entreprise et domain-driven, donc là où il nomme un
pattern que l'un d'eux nomme aussi, c'est le papier qui détient la définition. C'est
l'ADR-0006 appliqué tel qu'écrit, et un reach-back à prévoir sous
l'[ADR-0025](0025-let-an-earlier-work-reclaim-a-pattern-from-a-later-catalog.fr.md),
non une règle nouvelle.

La décision est bornée de trois façons, et ces bornes sont ce qui la rend sûre. Elle ne
prend que l'**auteur** de l'œuvre — pas un commentateur, si bon soit-il. Elle ne prend
qu'un énoncé **explicite** de remplacement dans le matériel de l'œuvre elle-même, non
une inférence tirée d'un livre postérieur traitant d'un terrain voisin. Et elle
n'atteint que la **partie** que l'auteur nomme : rien ici ne touche aux chapitres 2 à
5, et la note figure sur le seul compagnon du chapitre 6.

## Alternatives envisagées

### Cataloguer quand même le chapitre 6 du livre

Envisagée parce que c'est l'ADR-0006 lu à la lettre : le livre est l'œuvre admise, le
papier ne l'est pas, et le livre est la publication la plus ancienne.

Rejetée parce qu'elle publie comme vocabulaire courant un modèle retiré, et que le
lecteur de la sortie générée ne peut pas le savoir. Elle se trompe aussi sur l'objet
de la règle d'antériorité : celle-ci tranche un conflit entre deux présentations, et il
n'y a pas de conflit quand un auteur remplace la sienne.

### Sauter le chapitre 6 et ne rien cataloguer à sa place

Envisagée parce que c'est le plus petit changement, et qu'elle évite d'admettre une
seconde œuvre.

Rejetée parce que le matériel comptable est la part la plus citée des modèles du livre,
et qu'un trou de quinze sections n'est pas une décision sur ces patterns — c'est
l'absence de décision. Suivre l'auteur coûte un catalogue et répond à la question.

### Mettre les patterns du papier dans `AnalysisPatterns`

Envisagée parce que les deux œuvres sont d'un seul auteur sur un seul sujet, et qu'un
catalogue coûte moins que deux.

Rejetée parce qu'elle affirme qu'*Analysis Patterns* a nommé des patterns dont il n'a
jamais employé les mots. La première moitié de l'ADR-0006 porte sur là où un lecteur
cherche, et un lecteur du papier ne cherche pas sous le livre.

### Ranger les patterns du papier sous `Idioms`

Envisagée parce que le papier est un brouillon et non un livre publié, ce qui est un
titre plus faible que celui des quatre œuvres déjà cataloguées.

Rejetée parce que l'[ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md)
range un pattern qui n'a **aucun corpus propre**. Soixante-douze pages portant un
langage de patterns qui se référence lui-même sont un corpus ; être non publié en fait
une source plus faible, pas un orphelin.

## Conséquences

### Positives

* Le catalogue énonce ce que l'auteur tient aujourd'hui, et non ce qu'il tenait en 1997
  et dit ne plus tenir.
* Un lecteur de l'une ou l'autre œuvre trouve ses patterns sous son propre nom, ce qui
  garde l'ADR-0006 entier au lieu de l'échanger.
* La date de 2000 place le papier devant deux des quatre catalogues existants, donc les
  collisions qu'il produit se résolvent dans un sens déjà décidé.

### Négatives

* Un cinquième catalogue, et l'argument de l'ADR-0024 à refaire pour une source qui est
  un brouillon et non un livre publié.
* Le chapitre 6 du livre reste non catalogué. `catalog/README.md` doit le dire, sans
  quoi un lecteur comparant sections et entrées lit une décision comme un oubli.
* Le papier n'a ni ISBN ni date de publication propre : sa référence repose sur une date
  de création de PDF. C'est une citation plus faible que celle de toute autre entrée.

### Risques

* Le remplacement tient à une phrase sur une page de support. Si elle était retirée ou
  réécrite, cette décision devrait être revisitée — et cette phrase n'est versionnée
  nulle part que ce dépôt contrôle.
* Les patterns d'un brouillon sont moins stabilisés que ceux d'un livre : les entrées
  qui en viennent changeront peut-être plus souvent que celles des quatre livres.
* Trancher lesquelles des quinze sections du chapitre 6 ont un successeur dans le papier,
  c'est l'ADR-0007 appliqué quinze fois, et certaines n'en auront peut-être aucun. Elles
  seraient alors absentes pour une raison qui n'est *pas* le remplacement, et dire
  laquelle est un travail que cette décision crée plutôt qu'elle ne l'évite.

## Actions de suivi

* Ajouter `AccountingPatterns` à l'énumération du schéma et à la table de libellés du
  générateur, sans quoi aucune entrée du nouveau catalogue ne valide.
* Énumérer les patterns du papier depuis le papier lui-même, et consigner dans
  `catalog/README.md` lesquelles des quinze sections du chapitre 6 y ont un successeur
  et lesquelles n'en ont pas, afin qu'une absence se distingue d'un oubli.
* Confronter le papier à `EnterpriseApplicationArchitecture` et à `DomainDrivenDesign`
  sous l'ADR-0025 avant de cataloguer : à 2000 il est la publication la plus ancienne,
  donc toute collision déplace l'entrée postérieure.

## Références

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) — les
  deux moitiés : où un pattern est catalogué, et quelle publication détient sa
  définition. Ce record décide de ce qui se passe quand les deux œuvres sont d'un seul
  auteur et qu'il remplace la première.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) — ce qui
  tranchera, section par section, si un pattern du chapitre 6 a un successeur dans le
  papier.
* [ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.fr.md) — pourquoi
  le papier est un catalogue et non une étagère d'idiomes.
* [ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.fr.md) — l'admission
  d'*Analysis Patterns*, et les termes auxquels une œuvre entre au catalogue.
* [ADR-0025](0025-let-an-earlier-work-reclaim-a-pattern-from-a-later-catalog.fr.md) —
  pourquoi la date de 2000 du papier atteint les catalogues de 2002 et 2003.
* Fowler, *Accounting Patterns*, `martinfowler.com/apsupp/accounting.pdf`, et la note de
  remplacement sur `martinfowler.com/apsupp/apchap6.pdf`.
