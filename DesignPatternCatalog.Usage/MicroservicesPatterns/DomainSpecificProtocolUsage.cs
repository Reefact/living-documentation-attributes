#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.DomainSpecificProtocolSample {

    // Half the meter readings the grid operator receives still arrive by e-mail: an industrial customer's
    // meter operator sends a spreadsheet to a mailbox once a month, because that is what the contract says
    // and the contract runs to 2031. Nothing about that conversation is REST and nothing about it is a
    // broker — it is IMAP.
    //
    // DOMAIN-SPECIFIC PROTOCOL is the third answer to how services talk, and the reason to annotate it is
    // what it rules out: no circuit breaker, no service registry, no channel. A reader who assumes the
    // house conventions apply here will be wrong in a way that only shows up in production.

    /// <summary>
    ///     Collects readings from the meter operator's mailbox.
    /// </summary>
    /// <remarks>
    ///     None of this catalogue's other communication machinery governs this class. The annotation is
    ///     where that is said; otherwise it is learned by reading the implementation, which is exactly the
    ///     kind of knowledge that stays in one person's head.
    /// </remarks>
    [DomainSpecificProtocol]
    public sealed class MeterOperatorMailbox {

        public IReadOnlyList<string> Fetch(string folder) {
            // ... IMAP: SELECT the folder, FETCH unseen messages, pull the attachment off each
            return new List<string>();
        }

    }
}
