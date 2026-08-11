#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.ApplicationControllerSample {

    // Enrolment portal: the international applicant's admission, which is nine screens and a lot of rules
    // about which one comes next.
    //
    // Qualifications, then English language evidence — unless the qualification is from a majority-English
    // country, in which case skip it. Then references, but only two if the applicant is a returner. Then
    // funding, which for a sponsored applicant becomes three screens instead of one. Then a review that
    // sends you back to whichever screen was incomplete.
    //
    // Written across the screens, that flow lives in nine "where do I go next" decisions, each knowing
    // about the ones around it. Nobody can answer "what does an applicant see, in what order" without
    // reading all nine, and a rule change touches four of them.
    //
    // An APPLICATION CONTROLLER holds the flow itself: what may be done now, and what comes next.
    //
    // It is worth its weight exactly here, where the flow IS the difficult part — and it is dead weight on
    // the prospectus, where each page follows a link. That is why annotating it says something: it records
    // that this application has a flow complex enough to have been given its own home.

    /// <summary>
    ///     What an applicant may do now, and what follows — in one place rather than nine.
    /// </summary>
    [ApplicationController]
    public sealed class AdmissionFlow {

        public string NextScreenAfter(string completed, ApplicantProfile profile) {
            return completed switch {
                "qualifications" => profile.QualifiedInEnglish ? "references" : "language-evidence",
                "language-evidence" => "references",
                "references" => profile.IsSponsored ? "sponsor-details" : "funding",
                "sponsor-details" => "sponsor-approval",
                _ => "review"
            };
        }

        public bool MayEnter(string screen, ApplicantProfile profile) {
            return screen != "sponsor-details" || profile.IsSponsored;
        }

    }

    public sealed record ApplicantProfile(bool QualifiedInEnglish, bool IsSponsored, bool IsReturner);

}
