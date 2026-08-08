# ADR-0025 | Laisser une œuvre antérieure reprendre un pattern à un catalogue postérieur

🌍 🇬🇧 [English](0025-let-an-earlier-work-reclaim-a-pattern-from-a-later-catalog.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-08
**Décideurs :** Reefact

## Contexte

L'[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md)
décide lequel de deux catalogues détient la définition quand tous deux nomment le même
pattern : la publication la plus ancienne, l'autre en déclinant ou s'en restreignant.
Le schéma du catalogue documente le champ de référence comme porteur pour cette raison
précise, et non comme une politesse éditoriale.

Les catalogues ont été ouverts dans un ordre qui n'a rien à voir avec les dates de
publication. *Design Patterns* est de 1994, *Patterns of Enterprise Application
Architecture* de 2002, *Domain-Driven Design* de 2003, et *Analysis Patterns* de 1997 —
et ce dernier a été catalogué en quatrième, après que les trois autres avaient été
déclarés complets.

Cataloguer son troisième chapitre a donc exigé de modifier une entrée d'un catalogue
complet. `EnterpriseApplicationArchitecture/Money` est devenue une spécialisation de
`AnalysisPatterns/Quantity`, parce que l'argent est un montant avec une unité et une
arithmétique qui refuse de mélanger les unités — ce que Fowler a nommé une quantité en
1997.

Deux cas de même nature sont prévisibles depuis la table des matières du livre. Le
chapitre 12 nomme Two-Tier Architecture, Three-Tier Architecture et Presentation and
Application Logic, face à `DomainDrivenDesign/LayeredArchitecture` de 2003. Le chapitre
13 nomme Application Facade, face aux façades de 2002.

Un tel changement ne reste pas dans les données du catalogue. La relation est générée
comme un héritage, donc l'identité de l'attribut — le type qu'un consommateur atteint
en remontant
([ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.fr.md)) — devient un
type différent. Un consommateur qui groupe par identité obtient une autre réponse pour
une entrée qu'il a déjà lue.

Rien n'est encore publié : le README indique que la première version reste à venir.
L'[ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.fr.md)
énonce que ce qu'un consommateur lit est versionné, pas seulement ce qu'il compile.

## Décision

Lorsqu'une œuvre catalogée plus tard se révèle avoir nommé un pattern la première, la
relation est consignée plutôt qu'évitée, et l'entrée qui change est celle du catalogue
de l'œuvre postérieure.

## Justification

L'ADR-0006 décide déjà quel côté détient la définition. Ce qui n'était pas décidé est
si une entrée déjà écrite peut être modifiée pour l'honorer, et la réponse doit être
oui, parce que l'alternative fait dépendre la réponse du catalogue de l'ordre dans
lequel les livres ont été catalogués. Cet ordre est un accident de l'histoire de ce
dépôt. Un vocabulaire dans lequel deux entrées sont liées ou non selon l'ordre de
chargement n'est pas un vocabulaire, et un lecteur n'a aucun moyen de savoir dans quel
cas il se trouve.

Consigner la relation dans l'autre sens — l'entrée de 1997 déclinant de celle de 2002 —
honorerait l'ordre de chargement à la place, et ferait de la présentation postérieure la
définition du pattern antérieur. C'est exactement ce que l'ADR-0006 existe pour
empêcher, et l'année de référence figure dans le schéma afin que la question ne soit
jamais tranchée par qui a écrit le premier ici.

Ce qui rend l'opération assez peu coûteuse pour être faite, c'est que **seule la
relation bouge**. L'entrée postérieure garde son nom, ses rôles, ses cibles et son
catalogue : un lecteur de *Patterns of Enterprise Application Architecture* trouve
toujours `Money` sous ce catalogue, orthographié comme ce livre l'orthographie, ce qui
est l'autre moitié de l'ADR-0006. Déplacer l'entrée elle-même casserait cela, et serait
une décision différente et pire.

Le coût est réel et il est du côté du consommateur : une identité qu'il a lue change.
Il est borné ici par le fait que rien n'est publié, donc aucun consommateur ne détient
l'ancienne réponse aujourd'hui. Il ne le sera plus ensuite, et c'est la part qui mérite
d'être écrite — après la première version, ce type de changement est une rupture de ce
qu'un consommateur lit, et l'ADR-0021 dit déjà qu'une telle rupture est versionnée
plutôt que glissée.

Geler un catalogue une fois déclaré complet éviterait tout cela et signifierait que le
catalogue est sciemment faux. La complétude est un énoncé sur le contenu d'un livre —
que chacun de ses patterns a été tranché — et non l'affirmation que rien de ces entrées
ne peut être appris d'un autre livre.

## Alternatives envisagées

### Laisser le catalogue complet intact et cataloguer le pattern antérieur sans relation

Envisagée parce que c'est le plus petit changement : la nouvelle entrée arrive, rien
d'existant ne bouge, aucun consommateur n'est touché.

Rejetée parce qu'elle laisse deux entrées sans relation pour un seul pattern, ce que
l'[ADR-0005](0005-relate-patterns-by-inheritance-and-read-identity-from-it.fr.md) et
l'ADR-0019 existent pour empêcher. Un consommateur qui groupe par identité compte le
pattern deux fois, et une règle écrite pour les quantités n'atteint pas l'argent — les
deux choses à quoi sert la relation.

### Consigner la relation sur l'entrée antérieure, pointant vers la postérieure

Envisagée parce qu'elle produit aussi une seule identité, et ne touche que l'entrée
ajoutée plutôt qu'une entrée déjà écrite.

Rejetée parce qu'elle inverse l'ADR-0006. Le sens de la relation n'est pas une affaire
de commodité : il énonce quelle œuvre définit le pattern, et faire pointer l'entrée de
1997 vers celle de 2002 affirme que *Patterns of Enterprise Application Architecture*
définit un pattern qu'*Analysis Patterns* a nommé cinq ans plus tôt.

### Déplacer l'entrée dans le catalogue de l'œuvre antérieure

Envisagée parce qu'elle semble suivre l'ADR-0006 jusqu'à sa conclusion — si l'œuvre
antérieure détient la définition, peut-être l'entrée doit-elle y être.

Rejetée parce que l'ADR-0006 dit le contraire dans son autre moitié : un pattern est
catalogué là où l'œuvre qui l'a nommé l'a mis, et *Patterns of Enterprise Application
Architecture* nomme bien Money. Un lecteur de ce livre doit la trouver sous ce
catalogue, orthographiée comme ce livre l'orthographie. Les deux moitiés tiennent en
même temps, ce qu'une relation exprime précisément et qu'un déplacement ne fait pas.

### Déclarer un catalogue intouchable une fois complet

Envisagée parce que cela donnerait un sens fort à « complet » et protégerait les
consommateurs exactement du changement décrit ici.

Rejetée parce qu'elle achète cette protection au prix d'un catalogue faux. Elle rendrait
aussi la protection arbitraire : qu'une entrée soit intouchable dépendrait de savoir si
son livre a été terminé avant ou après celui qui lui reprend son pattern.

## Conséquences

### Positives

* La réponse du catalogue cesse de dépendre de l'ordre dans lequel les livres ont été
  catalogués. Deux entrées sont liées à cause de ce qu'elles affirment et de leur date
  de publication, seul récit qu'un lecteur puisse vérifier.
* Une règle écrite pour le pattern le plus large atteint le plus étroit, ce qui est tout
  l'objet d'une relation plutôt que de deux entrées.
* La portée du changement est énoncée : la relation, et rien d'autre. C'est ce qu'un
  relecteur a besoin de savoir pour relire l'un de ces cas rapidement.

### Négatives

* Un catalogue déclaré complet n'est jamais définitif à cet égard. Tout catalogue déjà
  écrit est exposé à n'importe quelle œuvre antérieure catalogée ensuite.
* Relire un chapitre d'une œuvre antérieure implique désormais de le confronter aux
  autres catalogues et non seulement au livre. C'est une relecture plus lourde, et elle
  incombe au relecteur autant qu'à l'auteur — les collisions se trouvent en connaissant
  les deux livres.
* Après la première version, ce type de changement est une rupture de ce qu'un
  consommateur lit, et doit être versionné comme telle. Le changement est peu coûteux
  maintenant et ne le restera pas.

### Risques

* La tentation de déclarer une identité pour ranger un recouvrement qui n'en est pas
  un. L'ADR-0007 est le garde-fou — les assertions décident, non les noms — et il doit
  s'appliquer face au texte de l'œuvre antérieure plutôt qu'à un résumé.
* Combien de collisions restent est inconnu jusqu'à la lecture de chaque chapitre. Deux
  sont prévisibles depuis une table des matières ; une troisième visible seulement dans
  le corps d'un chapitre serait trouvée tard, après que le catalogue concerné aurait été
  déclaré complet une seconde fois.

## Actions de suivi

* Trancher les chapitres 12 et 13 d'*Analysis Patterns* face à
  `DomainDrivenDesign/LayeredArchitecture` et aux façades du catalogue d'entreprise,
  sous l'ADR-0007, avant de cataloguer l'un ou l'autre.
* Tenir dans `catalog/README.md` le registre de ce qui a bougé — il porte déjà l'entrée
  `Money` et ce qui est attendu ensuite — afin qu'une absence se distingue d'un oubli.

## Références

* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md) — quel
  catalogue tient un pattern, et quelle publication tient sa définition. Ce record ne
  décide que de ce qui se passe quand les deux sont appris dans le mauvais ordre.
* [ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md) — ce qui
  décide que deux entrées ne font qu'un pattern, et le garde-fou contre le rangement.
* [ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.fr.md) — comment une
  relation devient une identité, et donc pourquoi ceci atteint un consommateur.
* [ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.fr.md) —
  pourquoi ce sera une rupture après la première version.
* `catalog/README.md` — le déplacement de `Money`, et les collisions attendues des
  chapitres 12 et 13.
