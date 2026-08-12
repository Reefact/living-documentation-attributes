# Gang of Four — le guide des patrons

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](README-en.md)

*Design Patterns: Elements of Reusable Object-Oriented Software* — Erich Gamma, Richard Helm, Ralph
Johnson et John Vlissides, Addison-Wesley, 1994. Vingt-trois patrons, les vingt-trois catalogués.

Ce guide n'est pas l'index du catalogue. L'[index](../../../generated/catalog-index.md#gang-of-four)
donne l'annotation à taper, ce à quoi chaque rôle s'applique et où se trouve l'exemple ; il est généré,
complet, et on le consulte. Ces pages donnent à quoi sert un patron, quand le sortir, quand ne pas le
sortir, et ce qu'il coûte. Elles sont écrites à la main, on les lit plutôt qu'on ne les consulte, et
elles arrivent un catalogue à la fois
([ADR-0040](../../for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.fr.md)).

## Patrons de création

Ils concernent la venue au monde des objets : qui décide de la classe, qui appelle le constructeur, et
combien le code appelant doit en savoir.

| Patron | À quoi il sert |
|---|---|
| [Abstract Factory](AbstractFactory-fr.md) | Créer des familles entières de parties qui doivent aller ensemble, la famille étant choisie une fois plutôt qu'à chaque `new`. |
| [Builder](Builder-fr.md) | Une séquence de construction, plusieurs représentations — les mêmes étapes produisant du texte, du HTML ou un fichier. |
| [Factory Method](FactoryMethod-fr.md) | Une classe sait quand créer ; une sous-classe décide quoi. |
| [Prototype](Prototype-fr.md) | De nouveaux objets par copie d'une instance configurée, de sorte que les sortes créables deviennent des données plutôt que des types. |
| [Singleton](Singleton-fr.md) | Une instance unique et un point d'accès global — et la page expose pourquoi seule la première moitié est d'ordinaire voulue. |

## Patrons structurels

Ils concernent l'assemblage des objets : ce qui enveloppe quoi, ce qui détient quoi, et ce qu'un appelant a
le droit de voir de la disposition.

| Patron | À quoi il sert |
|---|---|
| [Adapter](Adapter-fr.md) | Rendre un type utilisable à travers l'interface qu'un client attend, en traduisant entre les deux. |
| [Bridge](Bridge-fr.md) | Deux axes qui varient séparément — ce que dit une notification, comment elle est livrée — tenus à part au lieu d'être multipliés. |
| [Composite](Composite-fr.md) | Un arbre dont le tout et les parties répondent aux mêmes questions, de sorte que la récursion vit dans la structure et non chez chaque appelant. |
| [Decorator](Decorator-fr.md) | Ajouter une responsabilité autour d'un objet à l'exécution, sans y toucher et sans une classe par combinaison. |
| [Facade](Facade-fr.md) | Un point d'entrée devant plusieurs sous-systèmes, portant l'ordre des opérations que chaque appelant devrait sinon mémoriser. |
| [Flyweight](Flyweight-fr.md) | Quarante mille objets servis par onze, en séparant le partageable de ce qui ne l'est pas. |
| [Proxy](Proxy-fr.md) | Un substitut de même interface, qui contrôle l'accès à l'objet réel — en différant, en vérifiant, ou en traversant un réseau. |

## Patrons comportementaux

Pas encore écrits. Chain of Responsibility, Command, Interpreter, Iterator, Mediator, Memento, Observer,
State, Strategy, Template Method et Visitor — même remarque.

## Comment une page est organisée

Chaque page suit le même ordre.

| | |
|---|---|
| **Intention** | une phrase |
| **Problème** | la situation qui rend le patron envisageable, en code |
| **Solution** | ce que le patron y fait |
| **Structure** | un diagramme de classes des rôles |
| **Les rôles** | une ligne chacun, et l'annotation qui le marque |
| **L'exemple** | l'exemple de `DesignPatternCatalog.Usage`, par morceaux |
| **Possibilités d'application** | ce que l'œuvre elle-même énonce |
| **Quand ne pas l'utiliser** | les cas où le patron coûte plus qu'il ne rapporte |
| **Avantages** et **Inconvénients** | deux listes |
| **Liens avec les autres patrons** | les voisins, et ce qui les sépare |
| **Source** | l'œuvre, et les liens de retour vers l'index et le code |

## Ce que ces pages ne font pas

Elles n'inventent pas. Là où une œuvre n'énonce rien, la page le dit plutôt que de remplir la section —
le plus souvent dans *Quand ne pas l'utiliser*, que beaucoup d'œuvres laissent au lecteur. Là où une page
rapporte un jugement que la profession a formé après la publication de l'œuvre, elle dit de qui est ce
jugement. La page [Singleton](Singleton-fr.md) est le cas le plus net : le livre énumère des avantages
pour ce patron et aucun inconvénient, et la page marque la différence.
