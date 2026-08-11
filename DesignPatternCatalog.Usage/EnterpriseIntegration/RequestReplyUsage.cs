#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.RequestReplySample {

    // Before loading, the terminal asks the line whether a container is released. Earlier that question was a
    // remote call and the crane waited on the line being up. As two messages it does not: the answer arrives
    // when it arrives, and the terminal is free in between.
    //
    // REQUEST-REPLY is that exchange — two one-way messages on two channels, not a call wearing a message's
    // clothes.

    /// <summary>
    ///     The message that asks.
    /// </summary>
    /// <remarks>
    ///     It names the channel the answer belongs on, which is what makes this two messages rather than a
    ///     call.
    /// </remarks>
    [RequestReply.Request(Reply = typeof(ReleaseAnswer))]
    public sealed record ReleaseEnquiry(Guid EnquiryId, string ContainerNumber, string ReplyTo);

    /// <summary>
    ///     The message that answers, on a channel of its own.
    /// </summary>
    /// <remarks>
    ///     Being a separate message is what lets the requestor be down when it arrives and still receive it.
    /// </remarks>
    [RequestReply.Reply(Request = typeof(ReleaseEnquiry))]
    public sealed record ReleaseAnswer(Guid InReplyTo, bool Released, string? Hold);

    /// <summary>
    ///     Sends the request and consumes the reply.
    /// </summary>
    /// <remarks>
    ///     It must match one to the other, which is what a correlation identifier is for — and why the two
    ///     patterns are always seen together.
    /// </remarks>
    [RequestReply.Requestor]
    public interface IReleaseEnquirer {

        void Ask(ReleaseEnquiry enquiry);

        void OnAnswer(ReleaseAnswer answer);

    }

    /// <summary>
    ///     Consumes the request and sends the reply.
    /// </summary>
    /// <remarks>
    ///     It learns where to answer from the message rather than from configuration, which is what lets one
    ///     replier serve requestors it was never told about.
    /// </remarks>
    [RequestReply.Replier]
    public interface IReleaseAuthority {

        void Handle(ReleaseEnquiry enquiry);

    }
}
