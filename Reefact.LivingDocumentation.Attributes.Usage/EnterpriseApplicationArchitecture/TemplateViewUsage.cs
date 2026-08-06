#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.TemplateViewSample {

    // Enrolment portal: the prospectus page, written as the page it produces.
    //
    // Three files, three ways to render — this one, TransformViewUsage.cs and TwoStepViewUsage.cs. They are
    // alternatives, and they are on the same site so the differences are about the rendering rather than
    // about the content.
    //
    // A TEMPLATE VIEW is markup with the dynamic parts marked in it. Its virtue is that the marketing team
    // can read it: the file looks like the page, so someone who is not a developer can move a heading.
    //
    // The pattern comes with its own warning and this sample states it, because the warning is the whole
    // discipline: LOGIC IN A TEMPLATE IS LOGIC NOTHING CAN TEST. A condition in markup cannot be unit
    // tested, cannot be stepped through, and is invisible to a code reviewer reading a diff of prose.
    //
    // So the rule is that a template CALLS rather than DECIDES. Below, whether a course shows a "few places
    // left" badge is a property on the presentation model — one line, testable, named — and the template
    // asks. The version of this that put `credits >= 60 && places < 5` in the markup is how a template view
    // becomes the least maintainable part of a site.

    /// <summary>
    ///     A template that renders the prospectus. It calls; it does not decide.
    /// </summary>
    [TemplateView]
    public interface IProspectusTemplate {

        /// <summary>
        ///     Renders the page from a model that has already answered every question the markup asks.
        /// </summary>
        string Render(ProspectusPageModel model);

    }

    /// <summary>
    ///     Everything the template needs, decided in code where it can be tested.
    /// </summary>
    public sealed record ProspectusPageModel(
        string  CourseTitle,
        string  FormattedFee,
        bool    ShowsFewPlacesBadge,
        bool    ShowsClearingNotice,
        IReadOnlyList<string> ModuleTitles);

}
