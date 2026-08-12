# Dependency Injection — le guide des patrons

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](README-en.md)

*Dependency Injection Principles, Practices, and Patterns* — Steven van Deursen et Mark Seemann, Manning,
2019. Onze entrées cataloguées, et les onze traitées ici.

Le livre en nomme quatorze, en trois sections de catalogue. Les trois **code smells** du chapitre 6 ne sont
délibérément pas catalogués, pour une raison que consigne
l'[ADR-0037](../../for-maintainers/adr/0037-admit-the-dependency-injection-catalogue.fr.md) : le mot de degré.
La *sur*-injection est un jugement sur la quantité, et ce catalogue détient des formes plutôt que des
quantités.

Ce guide n'est pas l'index du catalogue.
L'[index](../../../generated/catalog-index.md#dependency-injection-principles-practices-and-patterns) donne
l'annotation à taper, ce à quoi chaque rôle s'applique et où se trouve l'exemple ; il est généré, complet, et on
le consulte. Ces pages donnent à quoi sert un patron, quand le sortir, quand ne pas le sortir, et ce qu'il
coûte. Elles sont écrites à la main
([ADR-0040](../../for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.fr.md)).

Tous les exemples de ce catalogue forment un seul système — la diffusion d'une radio associative — et les pages
se renvoient les unes aux autres parce que le code le fait. Les dix-neuf appels de résolution de l'histoire de
la racine de composition sont les mêmes dix-neuf dont la page du service locator fait le décompte.

## Les patrons

Chapitre 4. Comment une classe reçoit ce dont elle a besoin, et où le don a lieu.

| Patron | À quoi il sert |
|---|---|
| [Composition Root](CompositionRoot-fr.md) | Un seul endroit où le graphe d'objets est assemblé, pour que tout le reste soit composé au lieu de composer. |
| [Constructor Injection](ConstructorInjection-fr.md) | Le choix par défaut : ce qu'une classe exige, déclaré là où une instance ne peut pas exister sans. |
| [Method Injection](MethodInjection-fr.md) | Pour une dépendance qui appartient à l'appel plutôt qu'à l'instance. |
| [Property Injection](PropertyInjection-fr.md) | Pour une dépendance vraiment optionnelle, derrière un défaut qui fonctionne vraiment. |

## Les anti-patrons

Chapitre 5. Quatre façons pour une classe de cesser de déclarer ce dont elle dépend. Le livre présente les
quatre comme des défauts, et ces pages ne l'adoucissent pas — mais elles consignent pourquoi chacun apparaît
dans du code dont personne n'a été négligent.

| Anti-patron | Ce que c'est |
|---|---|
| [Control Freak](ControlFreak-fr.md) | Une classe qui construit ses propres dépendances : rien d'extérieur — pas même un test — ne peut les remplacer. |
| [Service Locator](ServiceLocator-fr.md) | Une classe qui résout ce dont elle a besoin : son contrat n'énonce aucune de ses préconditions. |
| [Ambient Context](AmbientContext-fr.md) | Un point d'accès statique : la dépendance n'est déclarée par personne et atteignable par tous. |
| [Constrained Construction](ConstrainedConstruction-fr.md) | Une signature de constructeur imposée de l'extérieur : sa vacuité ne prouve rien. |

## Les durées de vie

Chapitre 8. Une question — combien de temps une instance peut-elle vivre, et que peut-elle donc détenir —
répondue de trois façons. À lire ensemble : l'essentiel de ce que dit chaque page est un contraste avec les
deux autres.

| Durée de vie | Ce que cela veut dire |
|---|---|
| [Singleton Lifestyle](SingletonLifestyle-fr.md) | Une instance pour le processus. Doit être sûre en concurrence, et ne peut dépendre de rien de plus court. |
| [Scoped Lifestyle](ScopedLifestyle-fr.md) | Une par requête ou unité de travail. Sûre dans une portée, et perdue si on l'atteint depuis l'extérieur. |
| [Transient Lifestyle](TransientLifestyle-fr.md) | Une par consommateur. Peut détenir de l'état librement, et si elle est libérable, elle n'est libérée par personne en particulier. |

## Comment une page est organisée

Chaque page suit le même ordre.

| | |
|---|---|
| **Intention** | une phrase |
| **Problème** | la situation qui rend le patron envisageable, en code |
| **Solution** | ce que le patron y fait — ou, pour un anti-patron, ce que fait l'annotation |
| **Structure** | un diagramme des rôles |
| **Les rôles** | une ligne chacun, et l'annotation qui le marque |
| **L'exemple** | l'exemple de `DesignPatternCatalog.Usage`, par morceaux |
| **Possibilités d'application** | ce que l'œuvre elle-même énonce |
| **Quand ne pas l'utiliser** | les cas où le patron coûte plus qu'il ne rapporte |
| **Avantages** et **Inconvénients** | deux listes |
| **Liens avec les autres patrons** | les voisins, et ce qui les sépare |
| **Source** | l'œuvre, et les liens de retour vers l'index et le code |

## Ce que ces pages ne font pas

Elles n'inventent pas, et ce catalogue met cette règle à l'épreuve plus durement que les deux autres.

**Quatre entrées sont des anti-patrons, et le livre ne leur donne aucun avantage.** *Domain-Driven Design* en
donne huit à Smart UI, et le guide les porte comme étant ceux d'Evans. Seemann et van Deursen n'en donnent
aucun à ces quatre — la section *Avantages* de chacune des quatre dit donc que le livre n'en énumère aucun,
énonce le ou les deux faits circonstanciels honnêtement vrais, et s'arrête. Les remplir avec les arguments de
la profession mettrait des mots dans la bouche des auteurs.

**Deux entrées portent un désaccord plutôt qu'un verdict.** Fowler a nommé le service locator comme un patron
et Seemann l'appelle un anti-patron ; le même auteur appelait le contexte ambiant un patron en 2011 et un
anti-patron en 2019. Les pages nomment les deux lectures et suivent l'édition cataloguée, ce qui est pourquoi
l'ADR-0037 nomme une édition et non une œuvre.

**Une entrée partage son nom avec un patron d'un autre catalogue et n'est pas lui.** Le Singleton du Gang of
Four et le Singleton Lifestyle de ce catalogue sont deux choses différentes — l'un est une classe qui impose sa
propre unicité, l'autre une décision d'enregistrement prise hors de la classe. La page de la durée de vie le
dit, parce qu'un lecteur qui les confond écrit celui qui a les inconvénients.
