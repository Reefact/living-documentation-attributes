# ADR-0020 | Couvrir une forme générée par des fixtures, non par une entrée de catalogue

🌍 🇬🇧 [English](0020-cover-a-generated-shape-with-fixtures-not-a-catalog-entry.md) · 🇫🇷 Français (ce fichier)

**Statut :** Proposé
**Proposé :** 2026-08-05
**Décideurs :** Reefact

## Contexte

Le générateur émet quatre formes : un attribut plat, un conteneur de rôles, une
déclinaison, une spécialisation. Le catalogue en emploie deux. Une entrée —
l'objet-valeur d'Evans restreignant celui de Fowler — emploie la troisième, dans
sa seule forme plate. Aucune entrée ne décline quoi que ce soit, et aucune ne
relie un pattern multi-rôles
([ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.fr.md)).

Une forme que rien n'emploie est une forme que rien ne vérifie. La règle qui la
lit, la branche qui l'émet et l'exemple qui l'exercerait sont absents de toute
exécution, et le défaut que l'ADR-0019 a corrigé — une spécialisation absorbée
dans le pattern qu'elle restreint — était exactement de cette nature : il
compilait, il produisait un décompte plausible, et il manquait un pattern.

Ce qu'un pattern doit satisfaire pour entrer au catalogue est tranché et ne
concerne pas l'outillage. Il porte des assertions vérifiables
([ADR-0007](0007-decide-sameness-by-the-assertions-a-pattern-carries.fr.md)), il
peut être attaché à quelque chose
([ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md)), et il vient d'un
corpus qui l'a nommé
([ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.fr.md)).
L'ADR-0011 a refusé un type marqueur dont l'unique raison d'exister était d'être
annoté, au motif que le code documenté ne doit gagner aucun artefact du système de
documentation.

Les exemples ne sont pas disponibles non plus. Ils sont un artefact pédagogique :
un exemple métier réaliste par pattern, dans un domaine choisi pour convenir
([ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.fr.md)), et leur
inventaire est une mesure — le décompte ne signifie quelque chose que parce que
toutes les bases de code annotent de la même façon
([ADR-0010](0010-annotate-the-declaration-that-introduces-a-role.fr.md)).

Les tests de convention, différés trois fois, sont désormais écrits, et c'est ce
qui pose la question. Il leur faut quelque chose à lire.

## Décision

Une forme que le générateur peut émettre est couverte par des fixtures déclarées
dans le projet de tests, et jamais par une entrée de catalogue écrite pour être
testée.

## Justification

Le catalogue répond à une question sur les design patterns, et la couverture n'en
fait pas partie. Une entrée ajoutée parce que le générateur a une branche non
testée serait l'affirmation qu'un pattern existe, faite pour satisfaire un outil —
soit le type marqueur rejeté par l'ADR-0011 sous un autre habit — et elle serait
livrée. Des fixtures rendent les mêmes formes lisibles sans rien affirmer d'aucun
pattern, et rien d'elles ne quitte l'assembly de tests.

Les tenir hors des exemples protège une mesure. L'inventaire est ce qui prouve que
le catalogue se relit, et ses nombres ont un sens parce qu'ils comptent de vraies
annotations sur du code réaliste ; un pattern de fixture les gonflerait, et la
suite d'exemples enseignerait un pattern qui n'existe pas.

Les déclarer à la main est ce qui les rend indépendantes de ce qu'elles vérifient.
Une fixture écrite d'après le gabarit est un second énoncé de la forme : un test
qui la lit échoue donc dès que les règles de lecture cessent de s'accorder avec
elle — soit la défaillance dont on se garde. Cette indépendance est aussi la
limite, et elle doit être dite : ces fixtures prouvent que les règles tiennent sur
les formes, non que le générateur les émet encore. L'aller-retour prouve que les
sources sont ce que le catalogue produit ; aucun des deux contrôles n'absorbe
l'autre, et c'est la paire qui couvre une forme employée.

Pour une forme que rien n'emploie, la paire est incomplète, et cela est assumé
plutôt que masqué. Le gabarit pourrait changer sa façon d'émettre une déclinaison
sans que les fixtures cessent de passer, puisqu'aucune déclinaison générée
n'existe pour les contredire. L'alternative qui referme cet écart est réelle et
figure ci-dessous ; elle coûte une machinerie proportionnée aux deux formes
qu'elle couvrirait, et ce marché vaudra d'être revu quand le catalogue cessera de
le rendre hypothétique.

## Alternatives envisagées

### Ajouter une entrée de catalogue qui exerce la forme

Envisagé parce que c'est le chemin le plus court, et parce que l'entrée serait
générée, exemplifiée et relue exactement comme toutes les autres — la couverture
serait complète plutôt que partielle.

Rejeté parce que cela corrompt la réponse que donne le catalogue. L'entrée serait
un pattern que la littérature ne nomme pas, présent parce qu'une branche n'était
pas testée, et elle serait livrée aux consommateurs comme partie du vocabulaire.
L'ADR-0011 a refusé un type marqueur pour la même raison : rien n'existe dans
l'artefact documenté pour la commodité du système de documentation.

### Générer les fixtures depuis un catalogue de fixtures

Envisagé parce que cela supprime la limite ci-dessus, et c'est la plus forte des
alternatives. Un second catalogue, réservé aux tests, passé par le même générateur
vers le projet de tests prouverait que ce que le gabarit émet — et pas seulement
ce que les règles lisent — est juste pour chaque forme, y compris celles qu'aucun
pattern n'emploie.

Rejeté comme disproportionné aujourd'hui, non comme faux. Cela demande au
générateur un chemin de sortie et un chemin d'entrée dont il n'a aucun autre
usage, ajoute un second arbre généré que l'aller-retour devra couvrir sous peine
de le voir dériver, et fait tout cela pour deux formes dont aucune entrée n'a
encore besoin. Le marché devient favorable dès que le nombre de formes inemployées
croît, ou que la première relation réelle tarde — d'où un report plutôt qu'un
rejet.

### Placer les fixtures dans le projet d'exemples

Envisagé parce que le lecteur y tourne déjà, et que l'inventaire les prendrait sans
aucun projet nouveau.

Rejeté parce que les exemples enseignent et que l'inventaire mesure. Un pattern de
fixture apparaîtrait dans un inventaire destiné à compter de vraies annotations, et
un lecteur ouvrant le répertoire d'exemples y trouverait un pattern
n'appartenant à aucun corpus, dans un projet dont tous les autres fichiers sont des
exemples métier réalistes.

### Attendre la première entrée réelle qui emploie la forme

Envisagé parce que la couverture viendrait alors gratuitement, par la machinerie
ordinaire, sans rien inventer.

Rejeté parce que cela laisse l'intervalle sans garde, et que c'est dans
l'intervalle que vit le défaut. L'ADR-0019 a livré un changement de règle dont rien
n'exerçait les deux formes nouvelles ; attendre signifie que le prochain
changement de ce genre sera vérifié une fois, à la main, par celui qui le fait —
ce qui est exactement la façon dont la spécialisation absorbée a survécu.

## Conséquences

### Positives

* Une capacité peut être couverte avant que le catalogue n'en ait besoin : une
  forme n'est donc jamais émise sans que rien ne la lise.
* Le catalogue continue de ne répondre que sur des patterns, et l'inventaire des
  exemples de ne compter que de vraies annotations.
* Les fixtures énoncent les formes de façon compacte, dans un seul fichier, où un
  lecteur peut comparer les quatre.

### Négatives

* Les fixtures sont une copie à la main du gabarit : ajouter une forme suppose de
  l'écrire deux fois — une fois dans le générateur, une fois ici.
* Une forme qu'aucune entrée n'emploie n'est couverte que du côté de la lecture.

### Risques

* Les fixtures peuvent dériver du gabarit en silence : pour une forme employée,
  les tests de convention sur le catalogue livré l'attrapent ; pour une forme
  inemployée, rien ne l'attrape.
* Les fixtures sont bon marché : elles invitent donc à couvrir des formes
  hypothétiques que le générateur ne sait pas émettre. Seule la discipline d'en
  ajouter une en même temps qu'un vrai changement du générateur y résiste.

## Actions de suivi

* Générer les fixtures depuis un catalogue de fixtures si les formes inemployées
  se multiplient, ou si aucune relation multi-rôles réelle n'est cataloguée — c'est
  l'alternative qui referme l'écart restant.
* Supprimer une fixture dès qu'une entrée de catalogue couvre sa forme par la
  machinerie ordinaire, afin que l'ensemble des fixtures reste l'ensemble de ce que
  rien d'autre n'atteint.

## Références

* [ADR-0019](0019-stop-the-identity-climb-at-the-pattern-boundary.fr.md) — les
  formes que ceci couvre, et le défaut qui a montré pourquoi il fallait les
  couvrir.
* [ADR-0011](0011-leave-out-what-cannot-be-annotated.fr.md) — le même refus,
  appliqué à ce qui peut entrer au catalogue.
* [ADR-0012](0012-show-every-pattern-at-work-in-a-business-example.fr.md) —
  pourquoi les exemples ne peuvent pas servir de fixtures.
* [ADR-0016](0016-prove-the-sources-are-what-the-catalog-generates.fr.md) —
  l'aller-retour, soit la moitié de la paire que ces fixtures ne fournissent pas.
