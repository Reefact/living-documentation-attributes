# Domain-Driven Design — le guide des patrons

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](README-en.md)

*Domain-Driven Design: Tackling Complexity in the Heart of Software* — Eric Evans, Addison-Wesley, 2003.
Vingt-trois patrons catalogués, et les vingt-trois traités ici : ceux que le livre nomme et auxquels C#
offre quelque chose à quoi s'attacher ([la liste des omissions
délibérées](../../../../catalog/README.md#patterns-deliberately-left-out) dit lesquels manquent et
pourquoi), plus un qu'Evans a nommé plus tard.

Celui-là est [Domain Event](DomainEvent-fr.md), et sa page le dit à l'endroit où sa source a sa place. Il
n'est pas dans le livre de 2003 ; Evans le nomme dans la *Domain-Driven Design Reference* de 2015, et ce
catalogue le détient pour une raison que consigne
l'[ADR-0041](../../for-maintainers/adr/0041-hold-a-pattern-named-in-an-authors-later-reference-edition.fr.md).

Ce guide n'est pas l'index du catalogue.
L'[index](../../../generated/catalog-index.md#domain-driven-design) donne l'annotation à taper, ce à quoi
chaque rôle s'applique et où se trouve l'exemple ; il est généré, complet, et on le consulte. Ces pages
donnent à quoi sert un patron, quand le sortir, quand ne pas le sortir, et ce qu'il coûte. Elles sont
écrites à la main, on les lit plutôt qu'on ne les consulte, et elles arrivent un catalogue à la fois
([ADR-0040](../../for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.fr.md)).

## Les briques de la conception pilotée par le modèle

Ce sont les pièces dont un modèle est fait, et les deux patrons qui décident s'il y a un modèle du tout.

| Patron | À quoi il sert |
|---|---|
| [Layered Architecture](LayeredArchitecture-fr.md) | Isoler le modèle de l'écran, de la coordination et de la plomberie, pour qu'une règle puisse vivre à un endroit que tous les appelants atteignent. |
| [Smart UI](SmartUi-fr.md) | Le contraire, nommé par le livre comme l'anti-patron — et assorti des circonstances où il a malgré tout raison. |
| [Entity](Entity-fr.md) | Un objet que le domaine a besoin de désigner : *celui-là*, quoi qui ait changé de lui depuis. |
| [Value Object](ValueObject-fr.md) | Un objet décrit uniquement par ses valeurs, sans identité et sans rien à suivre. |
| [Service](Service-fr.md) | Une opération qui est vraiment une opération, n'appartenant à aucune entité ni à aucun objet-valeur. |
| [Aggregate](Aggregate-fr.md) | Une frontière avec un objet qui en a la charge, pour qu'un invariant traversant plusieurs objets puisse réellement être imposé. |
| [Factory](Factory-fr.md) | La création comme acte à part entière, pour que ce qui en sort n'ait jamais été à moitié construit. |
| [Repository](Repository-fr.md) | L'illusion d'une collection, pour que le modèle demande des agrégats sans apprendre où ils sont rangés. |
| [Domain Event](DomainEvent-fr.md) | Une affirmation que quelque chose s'est produit — au passé, immuable, et portant le moment où c'est arrivé. |

## Refactorer vers une compréhension plus profonde

Ils traitent de rendre souple un modèle une fois qu'il existe : ce qu'une règle a le droit d'être, ce
qu'une opération promet, et combien un lecteur doit tenir en tête avant de se fier à l'une ou à l'autre.

| Patron | À quoi il sert |
|---|---|
| [Specification](Specification-fr.md) | Une règle métier devenue objet — nommée, combinable, et interrogée par tous au lieu d'être réimplémentée par chacun. |
| [Assertion](Assertion-fr.md) | Le contrat énoncé plutôt que déduit : ce qu'une opération promet, et ce qui est vrai d'un type à chaque instant. |
| [Side-Effect-Free Function](SideEffectFreeFunction-fr.md) | Une opération qui répond et ne change rien, donc qu'on peut essayer, répéter et jeter librement. |
| [Closure of Operation](ClosureOfOperation-fr.md) | Une opération qui prend et rend son propre type, si bien que les résultats se réinjectent sans introduire de dépendance. |
| [Standalone Class](StandaloneClass-fr.md) | Un type qui ne dépend de rien, et se lit donc d'une traite et se teste avec des valeurs seules. |

## Conception stratégique

Ils s'appliquent au-dessus du type : où un modèle s'arrête et où un autre commence, comment deux modèles
se rencontrent, et lequel des deux vaut l'effort. La plupart sont annotés sur une assembly, parce que
c'est la plus petite chose qu'offre C# capable de faire une affirmation sur tout le code qu'elle contient.

**Où un modèle s'arrête, et comment deux modèles se rencontrent** — chapitre 14.

| Patron | À quoi il sert |
|---|---|
| [Bounded Context](BoundedContext-fr.md) | La frontière d'un modèle, à l'intérieur de laquelle un mot a un sens et hors de laquelle il peut en avoir un autre. |
| [Shared Kernel](SharedKernel-fr.md) | L'exception délibérée : un petit sous-ensemble que deux équipes partagent et ne changent que d'un commun accord. |
| [Anticorruption Layer](AnticorruptionLayer-fr.md) | Un mur à trois métiers, pour qu'un modèle amont qu'on ne peut pas changer n'atteigne jamais le vôtre. |
| [Open Host Service](OpenHostService-fr.md) | Un protocole conçu pour tous les venants, au lieu d'une intégration négociée par consommateur. |
| [Published Language](PublishedLanguage-fr.md) | Un vocabulaire documenté pour l'échange — non le modèle interne avec un sérialiseur boulonné dessus. |

**Quelle part vaut l'effort** — chapitres 15 et 16.

| Patron | À quoi il sert |
|---|---|
| [Core Domain](CoreDomain-fr.md) | La part qui rend le produit digne d'être écrit, marquée pour que l'effort soit dirigé plutôt que réparti. |
| [Generic Subdomain](GenericSubdomain-fr.md) | Nécessaire et non distinctive — la part qu'on pourrait acheter, et que les meilleurs devraient laisser tranquille. |
| [Cohesive Mechanism](CohesiveMechanism-fr.md) | Un solveur sorti de derrière les concepts, pour qu'une conduite reste une conduite. |
| [Pluggable Component Framework](PluggableComponentFramework-fr.md) | Un noyau gelé que plusieurs équipes implémentent, pour qu'un composant bâti en 2031 tourne sous une application de 2011. |

## Comment une page est organisée

Chaque page suit le même ordre.

| | |
|---|---|
| **Intention** | une phrase |
| **Problème** | la situation qui rend le patron envisageable, en code |
| **Solution** | ce que le patron y fait |
| **Structure** | un diagramme des rôles — de classes, ou d'assemblies là où les rôles s'appliquent à des assemblies |
| **Les rôles** | une ligne chacun, et l'annotation qui le marque |
| **L'exemple** | l'exemple de `DesignPatternCatalog.Usage`, par morceaux |
| **Possibilités d'application** | ce que l'œuvre elle-même énonce |
| **Quand ne pas l'utiliser** | les cas où le patron coûte plus qu'il ne rapporte |
| **Avantages** et **Inconvénients** | deux listes |
| **Liens avec les autres patrons** | les voisins, et ce qui les sépare |
| **Source** | l'œuvre, et les liens de retour vers l'index et le code |

## Ce que ces pages ne font pas

Elles n'inventent pas. Là où le livre n'énonce rien, la page le dit plutôt que de remplir la section, et
là où une page rapporte un jugement que la profession a formé après 2003, elle dit de qui est ce jugement
— la frontière d'agrégat et la contention, le modèle de domaine anémique, l'entité confondue avec une
table.

Deux conséquences méritent d'être nommées pour ce catalogue en particulier.

**Evans n'écrit pas de section *Applicability*.** Le livre argumente en prose et clôt chaque patron par un
*Therefore*. Ce qui figure ici sous *Possibilités d'application* en est tiré, non d'une liste que le livre
fournirait, et s'en tient à ce que le livre dit effectivement.

**Le livre énonce ses propres limites plus souvent que la plupart.** La page *Smart UI* en est le cas le
plus net : Evans le nomme l'anti-patron puis lui donne une liste d'avantages réels, et la page porte cette
liste comme étant la sienne au lieu de la convertir en avertissement.
