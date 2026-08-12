# File Transfer

🌍 🇫🇷 Français (ce fichier) · 🇬🇧 [English](FileTransfer-en.md)

## Intention

File Transfer intègre des applications en faisant produire par l'une un fichier que l'autre consomme, de sorte
qu'aucune n'ait besoin de rien savoir de l'autre au-delà d'un format convenu.

## Problème

Un terminal à conteneurs et l'administration des douanes. La douane n'ouvrira pas de socket vers un terminal, et
le terminal ne recevra pas de compte chez la douane.

Il n'y a aucune technologie commune sur laquelle bâtir, et aucune perspective d'en avoir une : deux
organisations, deux cycles d'achat, deux politiques de sécurité. Tout ce qui exigerait des deux côtés de faire
tourner le même intergiciel, ou d'être disponibles au même instant, n'est pas envisageable.

Ce qui traverse est un fichier.

## Solution

Le patron partage un format et rien d'autre.

Une application écrit un fichier à un endroit convenu, dans une disposition convenue, à une heure convenue.
L'autre le trouve et le lit. Aucune ne tient de référence vers l'autre, aucune n'a besoin que l'autre tourne, et
aucune n'apprend comment l'autre est faite.

Le coût est la fraîcheur. Rien n'est su avant que quelqu'un écrive un fichier et que quelqu'un d'autre le
remarque — une déclaration déposée une minute après l'export attend un jour.

## Structure

```mermaid
flowchart LR
    T["Terminal<br/>DeclarationFileExport"]
    F["/outbound/customs-YYYYMMDD.edi"]
    C["Douane<br/>lit a son propre rythme"]
    T -->|"ecrit a 04:00"| F
    F -->|"trouve, plus tard"| C
```

Deux boîtes sans flèche entre elles. Le fichier est la seule chose que les deux côtés touchent, et l'écart au
milieu est là où passe la journée.

## Les rôles

| Rôle | Annotation | S'applique à | Ce qu'il porte |
|---|---|---|---|
| FileTransfer | `[FileTransfer]` | interface, classe, assembly | Le participant qui produit ou consomme le fichier partagé. |

Un seul rôle, et il couvre les deux bouts : l'exportateur et l'importateur sont la même revendication vue de
deux côtés. L'assembly figure parmi les cibles parce qu'un projet d'intégration entier est parfois la portée
honnête.

## L'exemple

Extrait de [`FileTransferUsage.cs`](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration/FileTransferUsage.cs).

```csharp
[FileTransfer]
public sealed class DeclarationFileExport {

    public string WriteFor(DateOnly day, IReadOnlyList<string> declarations) {
        string path = $"/outbound/customs-{day:yyyyMMdd}.edi";
        // ... writes one line per declaration, in the agreed layout
        return path;
    }

}
```

Toute l'intégration est une méthode qui rend un chemin. Ce n'est pas l'exemple qui abrège — c'est le patron : il
n'y a pas de client, pas de connexion, pas de protocole, et rien à simuler dans un test.

`{day:yyyyMMdd}` dans le nom est l'autre moitié du contrat. Le *nom* du fichier est autant un accord partagé que
son contenu, parce que c'est ainsi que le receveur sait quel jour il détient et qu'il n'a pas déjà lu celui-là.

La remarque de l'exemple énonce le marché en une ligne : *les deux systèmes ne partagent aucune technologie, ce
qui est tout le bénéfice. Le coût est la fraîcheur : une déclaration déposée à 04h01 attend un jour.*

## Possibilités d'application

Le livre compare les quatre styles d'intégration sur la même poignée de critères, et le profil de File Transfer
est la raison de le choisir :

**Employez File Transfer là où les deux applications ne peuvent partager aucune technologie.** Il exige le moins
des deux côtés — un système de fichiers et une disposition convenue — et c'est pourquoi il traverse des
organisations qui ne s'accorderont sur rien d'autre.

**Employez-le là où la donnée n'a pas besoin d'être fraîche.** La conséquence propre du style est un délai d'un
intervalle de transfert, et le choisir c'est l'accepter.

**Employez-le là où ce qui traverse est de la donnée plutôt que du comportement.** Un fichier porte de
l'information ; il ne peut rien demander à l'autre côté de faire.

## Quand ne pas l'utiliser

**Ne l'employez pas là où la réponse est nécessaire maintenant.** Le portique qui attend un contrôle de mainlevée
ne peut pas attendre le fichier de demain. C'est le cas de
[Remote Procedure Invocation](RemoteProcedureInvocation-fr.md).

**Ne l'employez pas là où les deux copies doivent s'accorder à chaque instant.** Entre deux transferts, la vue du
receveur est périmée par construction, et aucun soin dans l'export n'y change rien. Là où la péremption est
inacceptable, la réponse du livre est [Shared Database](SharedDatabase-fr.md).

**Ne l'employez pas là où le format changera souvent.** La disposition est le contrat, et c'est un contrat sans
négociation de version et sans compilateur : une colonne ajoutée d'un côté est une lecture silencieusement
fausse de l'autre.

**Ne l'employez pas pour déplacer de très gros volumes fréquemment.** La granularité du style est le fichier
entier : changer un enregistrement suppose de tout écrire et tout relire — c'est pourquoi l'intervalle de
transfert tend à grandir plutôt qu'à rétrécir.

## Avantages

* Il exige le moins des deux côtés : ni intergiciel commun, ni runtime commun, ni disponibilité simultanée.
* Aucune application n'a besoin de savoir que l'autre existe, au-delà d'un chemin et d'une disposition.
* Tout est inspectable : l'état entier de l'intégration est un fichier que quelqu'un peut ouvrir.
* Il survit aux pannes de chaque côté sans perte — le fichier attend.

## Inconvénients

* La donnée est périmée entre deux transferts, d'exactement un intervalle de transfert.
* Le format est un contrat que rien ne vérifie, et une divergence est une lecture fausse plutôt qu'une erreur.
* Quelqu'un doit décider quand un fichier est complet, quand il a été lu, et ce qui se passe s'il est lu deux
  fois.
* Seule la donnée traverse ; rien ne peut être demandé à l'autre côté.

## Liens avec les autres patrons

**`SharedDatabase`**, **`RemoteProcedureInvocation`** et **`Messaging`** sont les trois autres styles, et les
quatre se lisent comme un seul choix.

**`Messaging`** est ce que celui-ci devient quand l'intervalle rétrécit et que la granularité tombe à un
événement — c'est-à-dire tout le reste de ce catalogue.

**`MessageTranslator`** est ce dont la disposition convenue a d'ordinaire besoin à l'arrivée, dès que le côté
receveur a un modèle propre.

## Source

*Enterprise Integration Patterns*, Gregor Hohpe et Bobby Woolf, Addison-Wesley, 2003 — chapitre 2, les styles
d'intégration.

* [Entrée d'index](../../../generated/catalog-index.md#filetransfer-enterprise-integration-patterns)
* [Attribut généré](../../../../DesignPatternCatalog.EnterpriseIntegration/FileTransfer.cs)
* [Exemple](../../../../DesignPatternCatalog.Usage/EnterpriseIntegration/FileTransferUsage.cs)
