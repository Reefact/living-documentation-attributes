#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.MicroservicesPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.MicroservicesPatterns.AccessTokenSample {

    // The customer web site authenticates a customer once, at the gateway. Billing, metering and outages
    // each need to know which customer is asking, and none of them is going to ask again.
    //
    // ACCESS TOKEN carries the answer inward. Every service downstream trusts it, so its validation is the
    // security boundary of the whole system — and reading a claim without checking the signature is a hole
    // that compiles, passes and looks like everything else.

    /// <summary>
    ///     Who is asking, carried across services.
    /// </summary>
    /// <remarks>
    ///     Everything downstream trusts this, which makes validating it the whole security boundary. A
    ///     service that reads a claim without verifying the signature is a hole nothing else in the
    ///     codebase can see, and this annotation is where a reviewer knows to look.
    /// </remarks>
    [AccessToken]
    public sealed class GridAccessToken {

        public GridAccessToken(string subject, IReadOnlyList<string> scopes, string signature) {
            Subject   = subject;
            Scopes    = scopes;
            Signature = signature;
        }

        public string Subject { get; }

        public IReadOnlyList<string> Scopes { get; }

        public string Signature { get; }

    }
}
