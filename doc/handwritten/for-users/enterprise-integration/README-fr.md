# Enterprise Integration Patterns — le guide des patrons

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](README-en.md)

*Enterprise Integration Patterns: Designing, Building, and Deploying Messaging Solutions* — Gregor
Hohpe et Bobby Woolf, Addison-Wesley, 2003. Soixante-cinq patrons catalogués, et c'est le plus grand
catalogue d'ici ; **dix-neuf sont écrits à ce jour**, et les cinq chapitres qui manquent encore sont
nommés ci-dessous avec ce qui en tient lieu jusqu'à leur existence.

Ce guide n'est pas l'index du catalogue.
L'[index](../../../generated/catalog-index.md#enterprise-integration-patterns) donne l'annotation par
type, ce à quoi chaque rôle s'applique et où est l'exemple ; il est généré, complet et consulté. Ces
pages donnent à quoi sert un patron, quand y recourir, quand ne pas le faire et ce qu'il coûte. Elles
sont écrites à la main, et elles arrivent un volet à la fois
([ADR-0040](../../for-maintainers/adr/0040-write-the-pattern-guide-by-hand-in-both-languages.md)).

Tous les exemples de ce catalogue sont un seul système — un terminal à conteneurs, avec ses
déclarations en douane, ses portiques, ses guérites et ses manifestes EDI — et les pages se renvoient
l'une à l'autre parce que le code le fait.

## Les styles d'intégration

Chapitre 2. Quatre façons de faire travailler deux applications ensemble, présentées par le livre comme
un seul choix plutôt que comme quatre techniques. Les pages se renvoient l'une à l'autre pour cette
raison : chacune se lit mieux face aux trois autres, et la recommandation propre du livre est la
quatrième.

| Style | À quoi il sert |
|---|---|
| [File Transfer](FileTransfer-fr.md) | Une application produit un fichier que l'autre consomme, de sorte qu'aucune ne sache rien de l'autre au-delà d'un format convenu. |
| [Shared Database](SharedDatabase-fr.md) | Les deux lisent et écrivent un seul schéma, de sorte qu'il n'y ait aucune donnée à transférer et rien qui puisse se désynchroniser. |
| [Remote Procedure Invocation](RemoteProcedureInvocation-fr.md) | L'une appelle une procédure que l'autre expose, de sorte que données et comportement voyagent ensemble et que l'appelant apprenne la réponse tout de suite. |
| [Messaging](Messaging-fr.md) | Des paquets de données voyagent sur des canaux, de sorte qu'émetteur et receveur soient découplés dans le temps autant que dans la technologie. |

## Les patrons racines

Chapitre 3. Six patrons dont tout le reste du livre est composé : chaque chapitre ultérieur en spécialise
un. Ce sont les pages les plus courtes du catalogue et celles sur lesquelles les autres s'appuient le
plus.

| Patron | À quoi il sert |
|---|---|
| [Message Channel](MessageChannel-fr.md) | Le chemin logique qu'emprunte un message, de sorte qu'un émetteur adresse un canal plutôt qu'un receveur. |
| [Message](Message-fr.md) | Des données enveloppées dans un paquet que le canal sait porter, de sorte que ce qui est envoyé soit une chose à part entière plutôt que les arguments d'un appel. |
| [Pipes and Filters](PipesAndFilters-fr.md) | Une tâche divisée en étapes indépendantes reliées par des canaux, de sorte qu'une étape puisse être réordonnée, réutilisée ou remplacée. |
| [Message Router](MessageRouter-fr.md) | Où va ensuite un message, décidé à un seul endroit, de sorte que les étapes d'un processus n'aient pas à connaître leurs adresses respectives. |
| [Message Translator](MessageTranslator-fr.md) | Un message converti d'un format vers un autre, de sorte que des applications aux formats différents puissent se parler sans qu'aucune soit changée. |
| [Message Endpoint](MessageEndpoint-fr.md) | La façon dont une application s'attache à un canal, de sorte que son code envoie et reçoive sans détenir l'API du système de messagerie. |

## Les canaux de messagerie

Le chapitre qui transforme [Message Channel](MessageChannel-fr.md) en décisions. Neuf patrons, et ils
répondent à quatre questions différentes au sujet d'un même canal — combien de receveurs, ce qui peut
voyager, ce qu'il advient de ce qui ne peut pas être traité, et si le canal survit à son propre hôte. Les
deux premiers se lisent ensemble, et les deux du milieu aussi ; c'est dans les paires que vivent les
distinctions.

| Patron | À quoi il sert |
|---|---|
| [Point-to-Point Channel](PointToPointChannel-fr.md) | Chaque message à exactement un receveur, de sorte que des consommateurs concurrents se partagent une charge sans rien traiter deux fois. |
| [Publish-Subscribe Channel](PublishSubscribeChannel-fr.md) | Une copie à chaque abonné, de sorte qu'un événement atteigne tous les intéressés et que l'émetteur n'en connaisse aucun. |
| [Datatype Channel](DatatypeChannel-fr.md) | Une sorte de message par canal, de sorte qu'un receveur sache ce qu'il lit sans l'inspecter. |
| [Invalid Message Channel](InvalidMessageChannel-fr.md) | Un endroit où un receveur met ce qu'il a lu et rejeté, de sorte qu'une donnée mauvaise ne bloque pas le canal et ne disparaisse pas. |
| [Dead Letter Channel](DeadLetterChannel-fr.md) | Un endroit où le système de messagerie met ce qu'il n'a pas pu délivrer, de sorte qu'un échec de délivrance soit visible plutôt que silencieux. |
| [Guaranteed Delivery](GuaranteedDelivery-fr.md) | Un canal qui fait persister ce qu'il porte, de sorte qu'une panne entre l'envoi et la réception ne perde rien. |
| [Channel Adapter](ChannelAdapter-fr.md) | Tendre le bras dans une application depuis l'extérieur, de sorte qu'une application qui ne connaît rien à la messagerie puisse tout de même y prendre part. |
| [Messaging Bridge](MessagingBridge-fr.md) | Deux systèmes de messagerie joints, de sorte qu'une migration puisse se faire une application à la fois. |
| [Message Bus](MessageBus-fr.md) | Une infrastructure partagée **et** un jeu de commandes convenu, de sorte qu'une application puisse être ajoutée ou retirée sans que les autres soient touchées. |

## La construction des messages

Pas encore écrits. Command Message, Document Message, Event Message, Request-Reply, Return Address,
Correlation Identifier, Message Sequence, Message Expiration, Format Indicator — tous les neuf sont
catalogués et annotés ; seules leurs pages de guide manquent. En attendant qu'elles existent, les
[entrées d'index](../../../generated/catalog-index.md#enterprise-integration-patterns) et les exemples
sous
[`DesignPatternCatalog.Usage/EnterpriseIntegration`](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration)
sont ce qu'il y a.

## Le routage des messages

Pas encore écrits. Content-Based Router, Message Filter, Dynamic Router, Recipient List, Splitter,
Aggregator, Resequencer, Composed Message Processor, Scatter-Gather, Routing Slip, Process Manager,
Message Broker — douze, et [Message Router](MessageRouter-fr.md) est la racine qu'ils spécialisent.

## La transformation des messages

Pas encore écrits. Envelope Wrapper, Content Enricher, Content Filter, Claim Check, Normalizer,
Canonical Data Model — six, qui spécialisent [Message Translator](MessageTranslator-fr.md) comme les
douze ci-dessus spécialisent le routeur.

## Les points de terminaison

Pas encore écrits. Messaging Gateway, Messaging Mapper, Transactional Client, Polling Consumer,
Event-Driven Consumer, Competing Consumers, Message Dispatcher, Selective Consumer, Durable Subscriber,
Idempotent Receiver, Service Activator — onze, qui spécialisent
[Message Endpoint](MessageEndpoint-fr.md).

## L'exploitation

Pas encore écrits. Control Bus, Detour, Wire Tap, Message History, Message Store, Smart Proxy, Test
Message, Channel Purger — huit, et le seul chapitre d'ici qui porte sur l'exploitation d'une solution de
messagerie plutôt que sur sa construction.

## Comment une page est organisée

Toutes les pages suivent le même ordre.

| | |
|---|---|
| **Intention** | une phrase |
| **Problème** | la situation qui rend le patron digne d'être considéré, en code |
| **Solution** | ce que le patron y fait |
| **Structure** | un diagramme des rôles — un diagramme de classes, ou un diagramme de flux là où le patron est une étape d'un pipeline |
| **Les rôles** | une ligne chacun, et l'annotation qui le marque |
| **L'exemple** | l'exemple de `DesignPatternCatalog.Usage`, par morceaux |
| **Possibilités d'application** | ce que l'œuvre énonce elle-même |
| **Quand ne pas l'utiliser** | les cas où le patron coûte plus qu'il ne rapporte |
| **Avantages** et **Inconvénients** | deux listes |
| **Liens avec les autres patrons** | les voisins, et ce qui les sépare |
| **Source** | l'œuvre, et les liens de retour vers l'index et le code |

## Ce que ces pages ne font pas

Elles n'inventent pas. Là où le livre n'énonce pas quelque chose, la page le dit plutôt que de remplir la
section. Cinq conséquences méritent d'être nommées pour ce catalogue en particulier.

**Les quatre styles d'intégration sont des alternatives, et les pages ne les aplatissent pas en une seule
recommandation.** Le livre préfère la messagerie et le dit, mais il donne aussi à File Transfer et à
Shared Database de véritables avantages, et ces pages les portent comme ceux des auteurs plutôt que de
les convertir en avertissements.

**L'un de ces noms est un anti-patron dans un autre catalogue.** Hohpe et Woolf présentent Shared
Database comme un style à choisir ; Richardson le présente comme ce que *Database per Service* existe
pour fuir. Le catalogue tient les deux entrées — le même schéma, la recommandation inverse — et la page
[Shared Database](SharedDatabase-fr.md) suit la lecture de ce livre-ci et nomme l'autre.

**Les exemples de ce catalogue sont plus laconiques que ceux des autres.** La plupart élident leur corps
par `// ...`, parce que ce qu'un patron affirme ici est d'ordinaire une signature et une absence plutôt
qu'une implémentation — un routeur qui rend un nom de canal n'a nulle part où mettre une charge utile.
Les pages s'appuient donc davantage sur l'explication de ce qui *manque* à une signature que sur la
citation de ce qui y est.

**Un exemple prend un raccourci, et sa page le dit.** Le pipeline de l'exemple de
[Pipes and Filters](PipesAndFilters-fr.md) appelle ses filtres directement plutôt qu'à travers les tuyaux
qu'il déclare. Le livre admet les deux agencements, l'exemple n'est donc pas faux — mais un lecteur qui
compare le diagramme au code le remarquerait, et la page le nomme d'abord.

**Plusieurs de ces patrons sont d'ordinaire de la configuration plutôt que du code, et les pages le disent
au lieu de faire comme si.** Un canal de lettres mortes est normalement un réglage de courtier ; un canal
est souvent un nom de file configuré, sans type à annoter. Là où c'est le cas, il n'y a rien à quoi
l'annotation puisse s'attacher, ce qui est la condition ordinaire de tout rôle plutôt qu'un manque dans
l'entrée — le motif que consigne
l'[ADR-0029](../../for-maintainers/adr/0029-admit-enterprise-integration-patterns-as-a-catalogue.md) pour
avoir admis les canaux. Les pages concernées le nomment dans *Les rôles*, là où regardera un lecteur qui
se demande s'il faut annoter quoi que ce soit.
