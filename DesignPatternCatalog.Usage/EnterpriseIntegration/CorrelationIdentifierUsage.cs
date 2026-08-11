#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.CorrelationIdentifierSample {

    // The terminal has forty release enquiries open at once. Forty answers come back on one channel, and
    // nothing in an answer says which question it belongs to.
    //
    // CORRELATION IDENTIFIER is the pair of properties that fixes it: the request carries an identifier, and
    // the reply quotes it. That quotation is the whole pattern.

    /// <summary>
    ///     The request, and the identifier a reply will quote.
    /// </summary>
    public sealed class ReleaseEnquiry {

        public ReleaseEnquiry(Guid enquiryId, string containerNumber) {
            EnquiryId       = enquiryId;
            ContainerNumber = containerNumber;
        }

        /// <summary>
        ///     Identifies this request uniquely.
        /// </summary>
        /// <remarks>
        ///     It must stay unique for as long as an answer might arrive, which is longer than the request
        ///     takes.
        /// </remarks>
        [CorrelationIdentifier.Identifier]
        public Guid EnquiryId { get; }

        public string ContainerNumber { get; }

    }

    /// <summary>
    ///     The reply, quoting the request it answers.
    /// </summary>
    public sealed class ReleaseAnswer {

        public ReleaseAnswer(Guid inReplyTo, bool released) {
            InReplyTo = inReplyTo;
            Released  = released;
        }

        /// <summary>
        ///     The identifier of the request this answers.
        /// </summary>
        /// <remarks>
        ///     The assertion the pattern exists for: an answer without it cannot be matched to anything, and a
        ///     requestor holding forty open enquiries has no way to guess.
        /// </remarks>
        [CorrelationIdentifier.Correlation(Identifier = typeof(ReleaseEnquiry))]
        public Guid InReplyTo { get; }

        public bool Released { get; }

    }
}
