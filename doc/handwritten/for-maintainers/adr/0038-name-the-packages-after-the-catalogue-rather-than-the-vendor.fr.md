# ADR-0038 | Nommer les paquets d'après le catalogue plutôt que d'après l'éditeur

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](0038-name-the-packages-after-the-catalogue-rather-than-the-vendor.md)

**Statut :** Proposé
**Proposé :** 2026-08-11
**Décideurs :** Reefact

## Contexte

Dix œuvres sont cataloguées, plus `Idioms` : **343 patterns, 619 noms de rôles**, huit des dix
complètes, et trente-sept records derrière. **Rien n'est publié.** La version est un espace réservé de
développement, il n'existe aucun workflow de publication, et il n'y a de consommateur nulle part.

Les paquets s'appellent `Reefact.LivingDocumentation.Attributes.<Catalogue>`. L'identifiant le plus
long qu'un consommateur tape fait **soixante-douze caractères** ; la solution, les vingt-six projets et
le dépôt GitHub portent tous la même racine. Le segment `Attributes` nomme ce que les types sont déjà :
tout type public de tout paquet est un attribut, et un consommateur le voit à la première ligne
d'usage.

**Aucun record n'a jamais décidé de l'identité des paquets.** Trente-sept records couvrent le format du
catalogue, le générateur, la politique de versionnement, la baseline de surface publique et onze
admissions, et aucun ne nomme un paquet. Deux records citent le namespace actuel en prose
([ADR-0033](0033-admit-microservices-patterns-as-a-catalogue.md) et sa traduction) ; rien d'autre dans
la base n'en dépend.

Le nom apparaît **56 984 fois dans 3 582 fichiers**. La quasi-totalité est du code généré et du code
d'exemple, qui suivent mécaniquement. Les douze baselines `PublicAPI.Shipped.txt` font exception :
chaque ligne commence par le namespace, et
[ADR-0018](0018-hold-the-public-surface-to-a-committed-baseline.md) interdit de les dériver avec le
générateur.

**Quatre faits de nuget.org pèsent sur le nom.**

* `Reefact.` est un **préfixe réservé et vérifié**. Quatre paquets y sont publiés, détenus par
  `Reefact` et `SylvainAurat`, et chacun porte la marque de vérification.
* `DesignPatternCatalog` ne renvoie **aucun résultat**. L'identifiant est libre.
* NuGet ne connaît pas de propriété d'un nom hors la réservation de préfixe, et celle-ci s'obtient sur
  un préfixe dont le demandeur peut prouver la propriété. Un préfixe générique n'est ordinairement pas
  réservé.
* **L'éditeur publie déjà une famille de catalogues sans préfixe.** `DiagnosticCatalog` et ses sept
  compagnons — `.NetAnalyzers`, `.Sonar`, `.StyleCop`, `.CodeStyle`, `.Trimming`, `.AspNetCore`,
  `.Self` — appartiennent à `Reefact`, ne portent aucun préfixe d'éditeur, ne sont pas vérifiés, et
  prennent la forme d'un méta-paquet au nom nu plus un paquet par sous-catalogue. Un catalogue de
  règles d'analyseurs écrites par d'autres est publié sous le nom du catalogue et non sous celui de
  l'éditeur.

**Ce que le catalogue tient est plus large qu'une seule sorte de pattern.** Il porte 62 patterns de
conception de test ([ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md)), 48 entrées qui
sont des modèles du métier ([ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.md)), des
anti-patterns ([ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md)), trois
lifestyles ([ADR-0037](0037-admit-the-dependency-injection-catalogue.md)) et des idiomes
([ADR-0013](0013-shelve-a-pattern-without-a-body-of-work-under-idioms.md)). Ces records partagent un
raisonnement, énoncé dans le troisième : les critères d'admission *« ne demandent pas quelle sorte de
chose est un pattern »*. Le même record trace une distinction avec laquelle le nom devra vivre — des
patterns d'analyse de Fowler, il dit en toutes lettres : **« None of them is a shape the code takes;
each is a claim about the domain being modelled. »**

Les quatre règles de lecture publiées sur l'attribut de base définissent **Catalog** comme *le premier
segment de namespace sous la racine*. Sous une racine qui nomme le tout, `DesignPatternCatalog.GangOfFour`
se résout toujours comme la règle le dit : la racine est `DesignPatternCatalog`, le catalogue est
`GangOfFour`.

Le renommage d'un dépôt GitHub laisse une redirection permanente. `PackageProjectUrl` et
`RepositoryUrl` nomment le dépôt, donc chaque paquet en porte le nom.

## Décision

Tous les paquets, namespaces, projets, la solution et le dépôt sont renommés de
`Reefact.LivingDocumentation.Attributes` en `DesignPatternCatalog`, nommant ce que les paquets portent
plutôt que qui les publie.

## Justification

**Les patterns ne sont pas ceux de l'éditeur, et un préfixe d'éditeur affirme qu'ils le sont.** Cette
bibliothèque existe pour attribuer chaque pattern à l'œuvre qui l'a nommé —
[ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) place un pattern là où son
œuvre l'a mis, et [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) le tient
dans l'orthographe de cette œuvre pour qu'un lecteur d'un livre y retrouve ses patterns tels qu'il les
a écrits. Un identifiant de paquet qui commence par le nom de l'éditeur contredit cette discipline dans
la seule chaîne qu'un consommateur lit avant tout le reste : il place le nom de l'éditeur devant celui
de Gamma, de Fowler, d'Evans, de Schmidt. La pratique de l'éditeur y répond déjà — les huit paquets
`DiagnosticCatalog` ne portent aucun préfixe, pour la même raison, sur un catalogue de règles qui ne
sont pas davantage les siennes.

**La convention retenue est celle de l'éditeur, non une exception à celle-ci.** Reefact en a deux :
`Reefact.*` pour ses propres bibliothèques, et `<Domaine>Catalog.*` pour les catalogues de matériaux
écrits par d'autres. La décision applique la seconde famille à un second catalogue sans rien inventer,
et la forme la suit exactement — un méta-paquet nu pour qui veut tout, un paquet par sous-catalogue
pour qui veut une œuvre.

**Le coût est réel et il est déjà assumé.** Le préfixe réservé `Reefact.` ne s'étend pas aux nouveaux
identifiants, donc les paquets seront publiés sans la marque de vérification. Ce n'est pas un coût que
cette décision introduit : c'est celui que l'éditeur a accepté en publiant les huit paquets
`DiagnosticCatalog`, et l'accepter une seconde fois conserve une règle au lieu d'en avoir deux.

**« Design pattern » est employé ici au sens courant** — une solution nommée et récurrente de
conception logicielle — sous lequel un pattern de conception de test, un pattern de modélisation
métier et un idiome sont tous des design patterns. Ce sens est délibérément plus large que
l'opposition qu'ADR-0024 trace entre un modèle du métier et une forme du code : ce record distingue des
sortes *à l'intérieur* du catalogue, celui-ci nomme le catalogue *de l'extérieur*, et un nom qui doit
couvrir dix œuvres ne peut pas porter la distinction que fait le record plus fin. Nommer le domaine est
tout ce dont dispose un lecteur qui cherche sur nuget.org, et c'est ce que le mot nu `Patterns` ne
donne pas.

**Rien n'est publié, et c'est toute la raison pour laquelle cela se décide maintenant.** L'identité
d'un paquet est la seule chose que le versionnement d'[ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md)
ne peut pas porter : un nouvel identifiant est un nouveau paquet, jamais une nouvelle version de
l'ancien. Renommer aujourd'hui coûte une passe mécanique sur des fichiers générés et douze baselines
réécrites à la main. Renommer après la première release coûte un cycle de dépréciation sur douze
paquets publiés et coupe en deux l'historique de chaque consommateur. La fenêtre se ferme à la première
release et ne se rouvre pas.

**Les règles de lecture restent inchangées**, ce qui rend le mot `Catalog` utilisable à la racine bien
qu'il nomme déjà une œuvre ailleurs dans le dépôt. La règle lit un catalogue comme le premier segment
sous la racine ; la nouvelle racine tient en un segment, donc le catalogue reste l'œuvre, et
`DesignPatternCatalog.GangOfFour` énonce que l'assembly est le catalogue des design patterns du Gang of
Four.

**Le dépôt est renommé avec les paquets** parce qu'il est nommé à l'intérieur d'eux.
`PackageProjectUrl` et `RepositoryUrl` sont des métadonnées expédiées : un dépôt qui garderait
l'ancien nom laisserait l'ancienne identité dans chaque paquet portant la nouvelle, et la redirection
permanente de GitHub rend le changement gratuit pour qui détient un ancien lien.

## Alternatives envisagées

### Garder le préfixe éditeur et raccourcir le milieu — `Reefact.Patterns.<Catalogue>`

Envisagée parce que le préfixe réservé `Reefact.` est un acquis déjà gagné, parce que la marque de
vérification est un vrai signal pour un consommateur qui hésite entre deux paquets de même nom, et
parce que `Company.Product.Feature` est la forme .NET ordinaire. C'est aussi la plus courte des
candidates — cinquante caractères au pire cas contre cinquante-quatre.

Rejetée parce qu'elle maintient le nom de l'éditeur devant des œuvres qui ne sont pas les siennes, ce
qui est l'objet même de cette décision, et parce que la famille de catalogues de l'éditeur répond déjà
à la question dans l'autre sens. La retenir signifierait publier `DiagnosticCatalog` sans préfixe et
`Reefact.Patterns` avec, sur le même argument, dans le même compte.

### `Patterns` plutôt que `DesignPatterns`

Envisagée, et c'est l'alternative qui a derrière elle le fait le plus solide : ADR-0024 dit des
patterns d'analyse qu'**aucun d'eux n'est une forme que prend le code**, et le catalogue en tient 48,
plus 62 patterns de conception de test. Sous cette lecture, le mot qui réunit est `Pattern`, et
`DesignPattern` revendique une catégorie plus étroite que ce que la base admet.

Rejetée parce qu'un `Patterns` nu ne nomme aucun domaine — il dit que le paquet contient des patterns
et non de quoi — et parce que le sens courant de « design pattern » est la formule qu'un lecteur
cherche et celle sous laquelle les dix œuvres sont rangées. La tension avec la formulation d'ADR-0024
est réelle et se trouve enregistrée plus bas comme conséquence plutôt qu'écartée par l'argument.

### N'enlever que `.Attributes` — `Reefact.LivingDocumentation.<Catalogue>`

Envisagée parce que c'est le plus petit changement possible, qu'elle supprime le segment qui ne dit
véritablement rien, et qu'elle conserve la *documentation vivante*, qui est l'argument de la section
d'ouverture du README et la raison d'être de la bibliothèque.

Rejetée parce qu'elle garde le préfixe éditeur et court encore à soixante et un caractères, et parce
que « living documentation » nomme la finalité plutôt que le contenu. Un consommateur qui cherche sur
nuget.org cherche ce qu'il y a dans le paquet.

### Ne rien renommer

Envisagée parce que le changement touche 3 582 fichiers et que les noms actuels fonctionnent.

Rejetée parce que le coût ne baisse jamais et devient irréversible : à la première release, le même
renommage se transforme en cycle de dépréciation sur douze paquets publiés. Différer, c'est choisir la
version chère de la même décision.

### Renommer les paquets mais pas le dépôt

Envisagée parce qu'un renommage de dépôt touche tous les liens externes.

Rejetée parce que `PackageProjectUrl` et `RepositoryUrl` sont expédiés dans les paquets : l'ancienne
identité survivrait dans les métadonnées de chaque paquet publié sous la nouvelle.

## Conséquences

### Positives

* L'identifiant le plus long passe de soixante-douze à cinquante-quatre caractères, et le segment qui
  ne disait rien disparaît.
* Aucun identifiant de paquet ne revendique la paternité d'une œuvre qu'il catalogue.
* Une seule règle de nommage sur les catalogues de l'éditeur au lieu de deux.
* Les quatre règles de lecture sont inchangées : rien de ce qu'un consommateur a appris sur la
  relecture des annotations n'est à réapprendre.
* Le changement est gratuit aujourd'hui et ne pourra plus jamais l'être.

### Négatives

* **Les paquets seront publiés sans la marque de vérification.** Le préfixe réservé `Reefact.`
  n'atteint pas les nouveaux identifiants, et un préfixe générique n'est ordinairement pas réservé.
* `DesignPatternCatalog` est un identifiant générique et désirable, sans réservation derrière lui.
  Rien n'empêche un tiers de publier `DesignPatternCatalog.Quelquechose` qui paraîtra appartenir à
  cette famille.
* **Le nom est plus large que les mots mêmes d'ADR-0024.** Un lecteur qui rencontre *« None of them is
  a shape the code takes »* dans un record accepté et `DesignPatternCatalog.AnalysisPatterns` sur
  nuget.org trouve deux records acceptés employant « design pattern » en deux sens. Ce record est le
  seul endroit qui dise que le sens large est délibéré.
* Les douze baselines de surface publique sont réécrites à la main, ADR-0018 interdisant de les
  générer.
* ADR-0033 et sa traduction citent l'ancien namespace et ne sont pas modifiés, un record accepté ne se
  réécrivant pas. Un lecteur de la base y rencontre l'ancien nom et est renvoyé ici.
* Tout lien externe vers le dépôt se résout par une redirection plutôt que directement.

### Risques

* Un renommage couvrant 3 582 fichiers peut manquer une chaîne qui n'appartient pas à un namespace —
  un lien de documentation, un chemin de workflow, un renvoi dans un commentaire XML. Les garde-fous
  sont qu'une régénération à catalogue inchangé doit laisser l'arbre propre, et que l'exemple doit
  toujours imprimer le catalogue entier relu par le seul attribut de base.
* Le catalogue pourra admettre plus tard une œuvre dont le contenu tende le nom plus loin encore que ne
  le font déjà les patterns d'analyse. Le nom sera alors faux au seul endroit qui ne se corrige pas à
  bon compte.

## Actions de suivi

* Renommer en une passe, régénérer, et vérifier qu'un catalogue inchangé laisse l'arbre de travail
  propre.
* Réécrire à la main les douze baselines `PublicAPI.Shipped.txt`.
* Renommer le dépôt GitHub en `design-pattern-catalog` et mettre à jour `PackageProjectUrl` et
  `RepositoryUrl`.
* Demander une réservation de préfixe d'identifiant sur `DesignPatternCatalog.`, et consigner la
  réponse ici — un préfixe générique peut fort bien être refusé, et un refus est le fait qui rend
  permanent le risque de squat.
* Mettre à jour les quatre documents racine et `catalog/README.md`, qui nomment les paquets de bout en
  bout.
* Laisser ADR-0033 et sa traduction intacts.

## Références

* [ADR-0001](0001-check-every-pull-request-against-the-adr-base.md) — pourquoi un changement de cette
  taille ne peut pas atterrir sans record, et pourquoi celui-ci existe.
* [ADR-0006](0006-catalogue-a-pattern-where-the-work-that-named-it-put-it.md) et
  [ADR-0028](0028-hold-a-pattern-in-every-catalogue-whose-work-presents-it.md) — la discipline
  d'attribution que le préfixe éditeur contredisait.
* [ADR-0018](0018-hold-the-public-surface-to-a-committed-baseline.md) — pourquoi les douze baselines
  sont réécrites à la main plutôt que régénérées.
* [ADR-0021](0021-version-what-a-consumer-reads-and-not-only-what-it-compiles.md) — le versionnement ne
  peut pas porter un changement d'identifiant, ce qui fait de la première release l'échéance.
* [ADR-0022](0022-admit-a-pattern-of-test-design-to-the-catalog.md),
  [ADR-0023](0023-admit-an-anti-pattern-on-the-same-terms-as-any-pattern.md) et
  [ADR-0024](0024-admit-a-model-of-the-business-to-the-catalog.md) — les trois records qui décident que
  le catalogue ne filtre pas par catégorie, et la source de la tension consignée plus haut.
* [ADR-0027](0027-ship-one-independent-package-per-catalogued-work.md) — un paquet par œuvre, forme que
  la nouvelle famille conserve.
* La famille de catalogues sans préfixe déjà publiée par l'éditeur sur nuget.org : `DiagnosticCatalog`
  et ses sept compagnons.
