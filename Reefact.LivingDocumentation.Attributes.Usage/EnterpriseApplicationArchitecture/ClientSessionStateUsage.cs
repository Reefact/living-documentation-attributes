#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.ClientSessionStateSample {

    // Enrolment portal: where the half-finished application lives between two requests. Three files, three
    // answers, one question — this one, ServerSessionStateUsage.cs and DatabaseSessionStateUsage.cs.
    //
    // CLIENT SESSION STATE keeps it on the client and gets it back with each request. The server keeps
    // nothing.
    //
    // What that buys is a deployment with no memory: any of the six web nodes can serve any request, a node
    // can be replaced mid-application, and a deploy in the middle of clearing week does not log anyone out.
    // For a portal that doubles its traffic for four days a year, that is the whole argument.
    //
    // The two costs are inseparable from it and both are visible below.
    //
    // Everything held this way travels on EVERY request. The chosen-modules list is fine; the applicant's
    // qualification history is not, and the answer is not to compress it — it is that this pattern is the
    // wrong home for it.
    //
    // And none of it can be trusted. A field the client can edit is a field the client WILL edit — the
    // signature below is not defensive programming, it is the pattern's requirement. Without it,
    // `FeeStatus = "home"` is a form field, and home fees are nine thousand pounds cheaper.

    /// <summary>
    ///     What the portal keeps on the client between requests — small, and never trusted.
    /// </summary>
    /// <remarks>
    ///     Signed, and re-checked server-side on anything that decides money or eligibility. Anything that
    ///     cannot survive being tampered with does not belong here.
    /// </remarks>
    [ClientSessionState]
    public sealed record ApplicationDraftCookie(
        string  ApplicantReference,
        IReadOnlyList<string> ChosenModuleCodes,
        string  LastCompletedScreen,
        string  Signature);

}
