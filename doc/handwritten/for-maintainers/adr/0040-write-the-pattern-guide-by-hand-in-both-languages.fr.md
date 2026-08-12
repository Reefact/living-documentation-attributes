# ADR-0040 | Écrire le guide des patterns à la main, dans les deux langues

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0040-write-the-pattern-guide-by-hand-in-both-languages.md)

**Statut :** Proposé
**Proposé :** 2026-08-12
**Décideurs :** Reefact

## Contexte

Le dépôt publie un seul document pour un lecteur du catalogue :
[`doc/generated/catalog-index.md`](../../../generated/catalog-index.md), 6091 lignes, généré depuis
`catalog/<Catalogue>/<Pattern>.json`. Par pattern, il donne le résumé, le tableau des rôles avec
l'annotation à taper, ce à quoi chaque rôle s'applique, s'il se répète et ce qu'il lie, une phrase par
rôle, la référence, et les liens vers la source générée et vers l'exemple.

**Il répond à *quoi taper*. Il ne répond pas à *pourquoi*, *quand*, ni *quand pas*.** Rien dans le
dépôt ne dit à un lecteur qui ne connaît pas déjà Abstract Factory quel problème il résout, ce qu'il
coûte, ni quelles situations en font le mauvais choix. Cette connaissance est dans les œuvres, et un
lecteur qui n'a pas lu l'œuvre n'a nulle part où aller.

Il existe 348 fichiers d'exemple sous `DesignPatternCatalog.Usage`, un par pattern
([ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md)), chacun en domaine métier et
chacun portant les annotations. Ils sont compilés et exécutés — l'exemple imprime le catalogue entier
relu par le seul attribut de base — donc ils ne peuvent pas pourrir. Ce qu'ils portent en prose est un
commentaire ou deux.

[ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) garde le catalogue
comme donnée et en génère les attributs et l'index. Le schéma porte un `summary` pour le pattern et un
par rôle, et aucune autre prose. L'applicabilité, les conséquences et les patterns qu'un lecteur
confond avec celui-ci **ne s'en dérivent pas** : ils viennent de l'œuvre.

**La définition d'un pattern ne bouge pas.** Les œuvres sont publiées et fixées ; ce qui bouge, c'est le
rendu qu'en fait ce dépôt — un rôle renommé, un exemple réécrit, une entrée admise ou exclue. L'index
généré porte déjà tout ce qui bouge.

Le dépôt s'écrit en anglais. La base d'ADR est bilingue avec l'anglais canonique et la traduction à
côté sous `NNNN-title.fr.md`, le suffixe marquant lequel des deux textes est le record.

## Décision

Un guide d'une page par pattern, écrit à la main en anglais et en français, est tenu sous
`doc/handwritten/for-users/`, n'est pas généré depuis le catalogue, et n'énonce rien que son œuvre ne
dise : une section que l'œuvre n'étaye pas est marquée vide plutôt que remplie.

## Justification

**Générer le guide mettrait dans le catalogue de la prose qu'aucun attribut ne porte.** Le schéma
gagnerait quatre ou cinq champs par pattern — quand l'utiliser, quand ne pas l'utiliser, conséquences,
confusions — dont le seul consommateur serait un rendu markdown. Le catalogue est la donnée dont les
attributs sont faits, et [ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md)
refuse déjà la machinerie qui ne sert rien de ce qui est émis. De la prose qu'aucun attribut ne porte
n'a pas sa place dans le fichier dont les attributs sont générés.

**L'écriture à la main est abordable précisément parce que le contenu ne bouge pas avec le code.** À
quoi sert un pattern a été tranché par son œuvre et ne changera pas ; ce qui change est le rendu du
catalogue, et l'index généré le porte. Une page vieillit donc face à un sujet fixe et non mouvant, et
c'est ce qui fait de 343 pages écrites à la main un travail fini plutôt qu'un tapis roulant.

**Une référence et un tutoriel se lisent différemment, et les fondre ne sert ni l'un ni l'autre.**
L'index se consulte — un lecteur arrive en connaissant le pattern et repart avec l'annotation. Le guide
se lit — un lecteur arrive sans savoir et repart capable de choisir. Mettre le second dans le premier
ferait de 6091 lignes une chose que personne ne parcourt ; les deux se pointent l'un l'autre à la
place.

**Aucune des deux langues n'est une traduction de référence, et les noms de fichiers le disent
symétriquement.** La base d'ADR marque un texte canonique parce qu'un record est une décision et
qu'une décision a besoin d'une formulation qui fait foi. Un guide n'a aucune autorité à protéger : un
lecteur francophone et un lecteur anglophone ont droit à la même page, pas à une page et son ombre.
`Xxxx-en.md` à côté de `Xxxx-fr.md` énonce cela ; `Xxxx.md` à côté de `Xxxx.fr.md` ne l'énoncerait pas.

**Une section vide vaut mieux qu'une section plausible, et c'est la moitié de la décision qui sera
éprouvée le plus souvent.** *Quand ne pas l'utiliser* est la section dont un lecteur a le plus besoin
et celle qu'une œuvre énonce le moins souvent en clair : le Gang of Four énumère des bénéfices et aucun
inconvénient, et plusieurs œuvres catalographiées ne sont pas à portée de main du tout. Une page écrite
pour avoir l'air complète remplirait cette section d'une prose qui sonne comme l'œuvre sans y être — et
comme tout le guide se lit d'une seule voix, un lecteur n'aurait aucun moyen de distinguer la phrase
sourcée de la phrase inventée. Donc une section que l'œuvre n'étaye pas est marquée vide et dit
pourquoi, et une section qui rapporte un jugement formé après l'œuvre dit de qui est ce jugement. Le
guide a le droit d'être incomplet ; il n'a pas le droit d'être plausible.

**Le risque que cela crée est la dérive, et la réponse est une consigne plutôt qu'une vérification.**
Rien dans le build ne compare une page au catalogue : un rôle renommé laisse donc une page fausse en
silence. `CLAUDE.md` gagne une règle permanente disant qu'un changement dans les rôles d'un pattern,
son exemple ou son statut oblige sa page. C'est plus faible qu'un test, et le dire ici est le point :
un lecteur de ce record doit savoir que le garde-fou est une habitude, pas une barrière.

## Alternatives envisagées

### Générer le guide depuis de nouveaux champs de schéma

Envisagée parce que c'est ce que le dépôt fait de tout le reste, et parce que des pages générées ne
peuvent pas dériver de la donnée dont elles sont faites.

Rejetée parce que la donnée serait de la prose qu'aucun attribut n'émet, portée dans le fichier dont
les attributs sont générés, et parce qu'elle n'achèterait pas ce que la génération achète d'ordinaire.
La dérive qu'un générateur empêche est celle entre la donnée et son rendu ; ici, la dérive qui mérite
d'être empêchée est celle entre une page et une œuvre publiée il y a trente ans, qu'aucun générateur
ne peut vérifier.

### Étendre l'index généré au lieu d'ajouter un guide

Envisagée parce que cela garde un document au lieu de deux.

Rejetée parce que l'index fait déjà 6091 lignes pour 343 patterns, et qu'une page pédagogique est plus
longue que toute une entrée d'index. Cela transformerait le seul document qu'un lecteur consulte en un
document que personne ne parcourt.

### L'anglais seul

Envisagée parce que le dépôt s'écrit en anglais et que la base d'ADR fait de l'anglais le canonique.

Rejetée par décision : le guide s'adresse aux utilisateurs et non aux mainteneurs, et le public du
mainteneur lit le français.

### Suivre la convention `NNNN-title.fr.md` de la base d'ADR

Envisagée par cohérence avec le seul matériau bilingue que le dépôt possède déjà.

Rejetée parce que cette convention existe pour marquer un texte comme le record, ce qui est juste pour
une décision et faux pour un guide. Deux fichiers pairs veulent deux noms symétriques.

### Une page par catalogue plutôt qu'une par pattern

Envisagée parce que cela fait moins de fichiers.

Rejetée sur le plus gros catalogue : 65 patterns d'intégration d'entreprise dans un document, chacun
avec un problème, un diagramme, un exemple et deux listes, n'est pas une page que quiconque lit.

## Conséquences

### Positives

* Un lecteur qui ne connaît pas un pattern a quelque part où aller, ce qui est la seule chose que le
  dépôt ne pouvait pas offrir.
* Chaque page peut être aussi longue que son pattern le mérite, sans allonger le document que tous les
  autres consultent.
* Rien n'est ajouté au générateur, au build ni à la suite de tests.

### Négatives

* **Rien ne confronte une page au catalogue.** Un rôle renommé, un exemple réécrit ou un pattern
  nouvellement exclu laisse sa page fausse et muette. Le garde-fou est une consigne permanente dans
  `CLAUDE.md`, c'est-à-dire une habitude et non une barrière.
* 343 patterns en deux langues font 686 pages une fois terminé, livrées catalogue par catalogue. Entre
  temps, le guide est partiel d'une façon dont l'index généré ne l'est jamais, et un lecteur rencontrera
  des patterns sans page.
* Certaines œuvres ne sont pas à portée de main. Là où une page ne peut pas énoncer l'applicabilité
  depuis l'œuvre elle-même, elle le dit plutôt que de remplir la section, et un lecteur rencontre un
  manque assumé. Sur les catalogues dont les œuvres sont les plus difficiles à atteindre, *Quand ne pas
  l'utiliser* pourra rester vide longtemps — ce qui est le résultat voulu, non un défaut à réparer en
  écrivant quelque chose.

### Risques

* Une page qui se lit avec une autorité qu'elle n'a pas. Un guide écrit de mémoire d'une œuvre plutôt
  que depuis l'œuvre énonce des choses que l'œuvre ne dit pas, sur un ton qui laisse croire le
  contraire. Deux atténuations, toutes deux visibles sur la page : une section que l'œuvre n'étaye pas
  est **marquée vide**, et un jugement formé après l'œuvre est **attribué** — la critique de Singleton
  est le cas le plus net, et sa page dit en toutes lettres que tout ce qui dépasse les deux conditions
  du livre est le point de vue de la profession et non celui des auteurs.

## Actions de suivi

* Ajouter deux règles permanentes à `CLAUDE.md` : un changement dans les rôles d'un pattern, son
  exemple ou son statut oblige les pages correspondantes ; et une section qu'aucune œuvre n'étaye reste
  marquée vide plutôt qu'écrite.
* Livrer catalogue par catalogue, et catégorie par catégorie là où un catalogue est assez gros pour le
  justifier.
* Décider, une fois plusieurs catalogues couverts, si un test doit affirmer que chaque pattern
  catalogué a une page dans les deux langues. C'est peu coûteux et cela transformerait la consigne
  permanente en barrière ; c'est différé plutôt que refusé.

## Références

* [ADR-0002](0002-keep-the-pattern-catalog-as-data-and-generate-the-attributes.md) — ce qui est généré
  depuis le catalogue, et donc ce que ce guide n'est pas.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.md) — les exemples dont le guide
  part.
* [ADR-0031](0031-carry-no-generator-machinery-for-an-unused-capability.md) — le refus que ce record
  étend aux champs de catalogue qu'aucun attribut ne porte.
* [`doc/generated/catalog-index.md`](../../../generated/catalog-index.md) — la référence vers laquelle
  le guide pointe au lieu de la répéter.
