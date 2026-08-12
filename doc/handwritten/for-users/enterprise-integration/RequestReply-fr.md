# Request-Reply

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](RequestReply-en.md)

## Intention

Request-Reply apparie une requête et une réponse sur deux canaux, de sorte qu'un message puisse obtenir une réponse
sans qu'aucun des deux côtés se bloque sur la disponibilité de l'autre.

## Problème

Avant le chargement, le terminal demande à l'armateur si un conteneur est libéré.

Écrit en appel distant, le portique attend que la ligne soit debout :

```csharp
bool released = _lineService.IsReleased(containerNumber);
```

Une ligne, et la capacité du terminal à charger un navire dépend désormais du service web d'une autre entreprise.
Quand le système de la ligne est lent, le portique est lent ; quand il est arrêté, le portique est arrêté ; et le
terminal n'y peut rien, parce que l'attente est bâtie dans la forme de l'appel.

## Solution

Le patron est deux messages à sens unique sur deux canaux.

Le terminal envoie une requête et poursuit. La réponse arrive quand elle arrive, sur un canal à elle, et le
terminal est libre entre-temps. Être un **message séparé** est ce qui permet au demandeur d'être arrêté quand la
réponse arrive et de la recevoir tout de même — ce qu'un appel ne peut pas faire, parce que la réponse d'un appel
n'existe que pendant que l'appelant l'attend.

C'est la distinction sur laquelle l'exemple insiste : ce sont deux messages, *non un appel déguisé en message*.

## Structure

```mermaid
flowchart LR
    RQ["IReleaseEnquirer<br/>[RequestReply.Requestor]"]
    A["ReleaseEnquiry<br/>[RequestReply.Request]"]
    C1{{"demandes"}}
    RP["IReleaseAuthority<br/>[RequestReply.Replier]"]
    B["ReleaseAnswer<br/>[RequestReply.Reply]"]
    C2{{"le canal nomme par la requete"}}
    RQ --> A --> C1 --> RP
    RP --> B --> C2 --> RQ
```

Deux canaux, et le second est nommé par la requête plutôt que configuré dans le répondeur.

## Les rôles

| Rôle | Annotation | S'applique à | Ce qu'il porte |
|---|---|---|---|
| Request | `[RequestReply.Request]` | classe, structure | Le message qui demande, en nommant ou en portant le canal sur lequel la réponse revient. |
| Reply | `[RequestReply.Reply]` | classe, structure | Le message qui répond, envoyé sur un canal à lui. |
| Requestor | `[RequestReply.Requestor]` | interface, classe | Le participant qui envoie la requête et consomme la réponse. |
| Replier | `[RequestReply.Replier]` | interface, classe | Le participant qui consomme la requête et envoie la réponse. |

Quatre rôles — le plus grand nombre de ce chapitre — parce que le patron est une conversation plutôt qu'un message.
Deux sont des messages et deux sont des participants, et la paire de messages est liée dans les deux sens : la
`Request` nomme sa `Reply` et la `Reply` nomme sa `Request`.

## L'exemple

Extrait de [`RequestReplyUsage.cs`](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration/RequestReplyUsage.cs).

Les deux messages d'abord, chacun désignant l'autre :

```csharp
[RequestReply.Request(Reply = typeof(ReleaseAnswer))]
public sealed record ReleaseEnquiry(Guid EnquiryId, string ContainerNumber, string ReplyTo);
```

```csharp
[RequestReply.Reply(Request = typeof(ReleaseEnquiry))]
public sealed record ReleaseAnswer(Guid InReplyTo, bool Released, string? Hold);
```

Le `typeof` mutuel est ce qui rend la paire contrôlable. Une requête dont personne n'envoie jamais le type de
réponse déclaré, ou une réponse que personne ne demande, est une conversation à un seul bout — et c'est exactement
la sorte de défaut qu'une règle portant sur les annotations peut trouver, puisque aucun des deux messages ne
paraît faux à lui seul.

Deux propriétés de ces enregistrements sont d'autres patrons qui travaillent ici. `ReplyTo` est une
[adresse de retour](ReturnAddress-fr.md) ; `EnquiryId` et `InReplyTo` sont un
[identifiant de corrélation](CorrelationIdentifier-fr.md) et sa citation. Ils sont annotés dans leurs propres
exemples plutôt que dans celui-ci, ce qui est la décomposition d'une même conversation par les exemples plutôt que
sa répétition.

`string? Hold` est la réponse qui porte le *pourquoi*. Une réponse `false` sans raison envoie quelqu'un au
téléphone.

Puis les deux participants :

```csharp
[RequestReply.Requestor]
public interface IReleaseEnquirer {

    void Ask(ReleaseEnquiry enquiry);

    void OnAnswer(ReleaseAnswer answer);

}
```

`Ask` rend `void`, et c'est tout le patron en une signature. Un demandeur dont `Ask` rendrait une `ReleaseAnswer`
serait de nouveau un appel distant, quel que soit le nombre de canaux en dessous. La réponse arrive à `OnAnswer`,
séparément, peut-être bien plus tard, peut-être après un redémarrage.

```csharp
[RequestReply.Replier]
public interface IReleaseAuthority {

    void Handle(ReleaseEnquiry enquiry);

}
```

Une seule méthode, et elle prend la requête et ne rend rien. Le répondeur ne rend pas une réponse à un appelant ;
il en envoie une, sur le canal que la requête a nommé. L'exemple énonce ce que cela achète : *il apprend où
répondre depuis le message plutôt que depuis la configuration, ce qui permet à un répondeur de servir des
demandeurs dont on ne lui a jamais parlé.*

## Possibilités d'application

**Employez requête-réponse là où une réponse est réellement nécessaire et où l'attente ne l'est pas.** Le cadrage
du livre : un message peut obtenir une réponse sans qu'aucun des deux côtés se bloque sur la disponibilité de
l'autre.

**Employez-le là où le répondeur appartient à quelqu'un d'autre.** Le système d'un partenaire sera lent et sera
arrêté, et c'est la forme qui empêche que cela devienne le problème du terminal.

**Laissez la requête nommer son canal de réponse.** [Return Address](ReturnAddress-fr.md) est ce qui permet à un
répondeur de servir des demandeurs pour lesquels il n'a jamais été configuré.

**Corrélez.** Un demandeur qui a quarante demandes ouvertes ne peut pas apparier les réponses en devinant, ce qui
est pourquoi l'[identifiant de corrélation](CorrelationIdentifier-fr.md) et ce patron se voient toujours ensemble.

## Quand ne pas l'utiliser

**Ne l'employez pas là où l'appelant ne peut réellement pas continuer.** Si rien ne peut se produire avant
l'arrivée de la réponse, l'asynchronie est une fiction et
[Remote Procedure Invocation](RemoteProcedureInvocation-fr.md) est l'agencement honnête — il est au moins clair sur
ce qu'il coûte.

**Ne le cachez pas derrière une enveloppe bloquante.** Un demandeur qui envoie puis attend la réponse a rebâti
l'appel avec un courtier au milieu : le même couplage, plus une file, plus deux canaux à exploiter.

**Ne l'employez pas là où aucune réponse n'est voulue.** Un [événement](EventMessage-fr.md) n'attend aucune
réponse, et lui donner un canal de réponse oblige à décider quelle réponse d'abonné compte.

**Ne l'envoyez pas sans identifiant de corrélation.** La réponse arrive sur un canal partagé parmi trente-neuf
autres, et une réponse qu'on ne peut apparier à aucune question est une réponse à rien.

**Ne laissez pas la requête ouverte indéfiniment.** Un demandeur qui garde un état pour chaque demande sans réponse
l'accumule jusqu'à ce que quelque chose le purge, et le patron qui borne cela est
[Message Expiration](MessageExpiration-fr.md).

**Ne mettez pas une réponse sur un canal de publication-abonnement.** Une réponse a un destinataire légitime — le
demandeur qui a posé la question — et la diffuser dit à trois autres systèmes la réponse à une question qu'ils
n'ont pas posée.

## Avantages

* Aucun des deux côtés ne se bloque sur la disponibilité de l'autre, ce qui est toute la raison de le préférer à un
  appel.
* Le demandeur peut redémarrer entre la question et la réponse, et recevoir la réponse tout de même.
* Un répondeur peut servir des demandeurs pour lesquels il n'a jamais été configuré, parce que la requête dit où
  répondre.
* La conversation est deux types de messages déclarés : une règle peut vérifier que les deux bouts existent.
* La lenteur du répondeur devient de la latence plutôt que de l'indisponibilité.

## Inconvénients

* C'est une conversation à bâtir : deux canaux, un identifiant de corrélation, une adresse de retour et un état
  chez le demandeur.
* Le demandeur détient un état pour chaque requête ouverte, et quelque chose doit le purger.
* Les réponses arrivent dans le désordre et peut-être longtemps après : le code du demandeur devient asynchrone de
  bout en bout.
* Il est facile de l'envelopper dans un appel bloquant et de perdre tout ce que le patron avait acheté, sans que
  cela se voie.
* Une réponse perdue ressemble à une réponse lente, et seul un délai d'attente les distingue.

## Liens avec les autres patrons

**`ReturnAddress`** est ce que la requête porte pour que le répondeur sache où répondre.

**`CorrelationIdentifier`** est ce qui rend la réponse appariable, et l'exemple dit sans détour pourquoi les deux
patrons se voient toujours ensemble.

**`CommandMessage`** est ce qu'une requête est d'ordinaire, et la réponse est ce que le livre dit qu'une commande
obtient d'ordinaire.

**`RemoteProcedureInvocation`** est le style d'intégration que celui-ci remplace, et celui qu'il redevient dès
l'instant où quelqu'un l'enveloppe dans une méthode bloquante.

**`MessageExpiration`** est ce qui borne une requête que personne ne répondra.

**`Messaging`** est le style dont ce patron montre le plus nettement le découplage dans le temps, parce qu'ici une
réponse est réellement voulue et que personne n'attend pour autant.

## Source

*Enterprise Integration Patterns*, Gregor Hohpe et Bobby Woolf, Addison-Wesley, 2003 — le chapitre sur la
construction des messages.

* [Entrée d'index](../../../generated/catalog-index.md#requestreply-enterprise-integration-patterns)
* [Attribut généré](../../../../DesignPatternCatalog.EnterpriseIntegration/RequestReply.cs)
* [Exemple](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration/RequestReplyUsage.cs)
