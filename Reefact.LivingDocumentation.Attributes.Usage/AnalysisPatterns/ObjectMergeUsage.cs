#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.ObjectMergeSample {

    // A veterinary group practice. "J. McAllister, Ardrishaig" and "Jean McAllister, Ardrishaig" are one client,
    // registered twice — once at the branch surgery in 2019, once at the main practice when she moved her cat's
    // care across. Two records, one person, and between them: eleven invoices, a vaccination history, a repeat
    // prescription and a reminder letter already in the post.
    //
    // Deleting the duplicate is the obvious move and it breaks all of that. Every invoice referring to it becomes
    // an invoice for nobody, and the practice cannot explain its own ledger.
    //
    // OBJECT MERGE keeps the record and points it forward. Figure 5.5 makes the distinction «dynamic», which is
    // exactly right: no record is created superseded, it simply has not been merged away yet.
    //
    // What the annotation licenses is the resolution rule, and it is the one that gets forgotten. A reference to a
    // superseded record must resolve forward — every time, including through a chain, because the record you
    // merged into can itself be merged later. A query that reads a superseded record directly is reading
    // something the practice no longer believes, and it will not fail while doing it.
    //
    // Figure 5.6 gives the other shape the section offers, and it is here for contrast: where nothing makes one
    // record primary, both stay appearances of one essence instead of one absorbing the other. A model picks one
    // shape or the other.

    /// <summary>
    ///     A client record.
    /// </summary>
    [Party]
    public class ClientRecord {

        public ClientRecord(string reference, string name) {
            Reference = reference;
            Name      = name;
        }

        /// <summary>The practice's reference for this record.</summary>
        public string Reference { get; }

        /// <summary>How the record spells the client.</summary>
        public string Name { get; }

    }

    /// <summary>
    ///     A client record still in use.
    /// </summary>
    /// <remarks>
    ///     «dynamic» in figure 5.5: this is not a kind of record, it is a record that has not been merged away.
    /// </remarks>
    [ObjectMerge.ActiveObject]
    public sealed class ActiveClient : ClientRecord {

        private readonly List<SupersededClient> _absorbed = new();

        public ActiveClient(string reference, string name) : base(reference, name) { }

        /// <summary>The records merged into this one.</summary>
        public IReadOnlyList<SupersededClient> Absorbed => _absorbed;

        internal void Absorb(SupersededClient superseded) {
            _absorbed.Add(superseded);
        }

    }

    /// <summary>
    ///     A client record found to be the same client as another, kept rather than deleted.
    /// </summary>
    /// <remarks>
    ///     Kept because eleven invoices refer to it. A reference to it must resolve forward.
    /// </remarks>
    [ObjectMerge.SupersededObject(ActiveObject = typeof(ActiveClient))]
    public sealed class SupersededClient : ClientRecord {

        public SupersededClient(string reference, string name, ClientRecord mergedInto, DateOnly on)
            : base(reference, name) {
            MergedInto = mergedInto;
            On         = on;
        }

        /// <summary>
        ///     The record now in use — which may itself be superseded, because a merge can be merged.
        /// </summary>
        public ClientRecord MergedInto { get; }

        /// <summary>When the merge was made.</summary>
        public DateOnly On { get; }

    }

    /// <summary>
    ///     The client list, and the resolution the pattern exists for.
    /// </summary>
    public sealed class ClientList {

        private readonly Dictionary<string, ClientRecord> _byReference = new();

        /// <summary>Adds a record of either kind.</summary>
        public void Add(ClientRecord record) {
            _byReference[record.Reference] = record;
        }

        /// <summary>
        ///     Merges one record into another, keeping the first.
        /// </summary>
        public SupersededClient Merge(ClientRecord duplicate, ActiveClient into, DateOnly on) {
            if (ReferenceEquals(duplicate, into)) {
                throw new InvalidOperationException("a record cannot be merged into itself");
            }

            SupersededClient superseded = new(duplicate.Reference, duplicate.Name, into, on);
            into.Absorb(superseded);
            _byReference[superseded.Reference] = superseded;

            return superseded;
        }

        /// <summary>
        ///     The record in use for a reference, following the chain to its end. Every read goes through here;
        ///     that is the rule the annotation states, and the chain is why it cannot be one hop.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the chain does not terminate.</exception>
        public ClientRecord? Resolve(string reference) {
            if (!_byReference.TryGetValue(reference, out ClientRecord? record)) {
                return null;
            }

            int guard = 0;
            while (record is SupersededClient superseded) {
                record = superseded.MergedInto;
                if (++guard > 100) {
                    throw new InvalidOperationException($"the merge chain from {reference} does not terminate");
                }
            }

            return record;
        }

    }

    /// <summary>
    ///     The other shape figure 5.6 gives: the client themselves, holding the records that appear to be them.
    /// </summary>
    /// <remarks>
    ///     For a practice that never decided which record was primary. Present here for contrast — a model uses
    ///     this or the pair above, never both.
    /// </remarks>
    [ObjectMerge.ObjectEssence]
    public sealed class Client {

        private readonly List<ClientRecord> _appearances = new();

        public Client(string canonicalName) {
            CanonicalName = canonicalName;
        }

        /// <summary>What the practice has settled on calling them.</summary>
        public string CanonicalName { get; }

        /// <summary>Every record that turned out to be them.</summary>
        public IReadOnlyList<ClientRecord> Appearances => _appearances;

        /// <summary>Records that a record is an appearance of this client.</summary>
        public void AlsoAppearsAs(ClientRecord record) {
            _appearances.Add(record);
        }

    }

}
