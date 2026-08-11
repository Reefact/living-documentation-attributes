#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.TransformViewSample {

    // Enrolment portal: the same prospectus, rendered as a transformation instead of as a page.
    //
    // A TRANSFORM VIEW walks the data and turns each element into output. Where the template next door is
    // written as the page with holes in it, this is written as a set of rules — one per kind of thing that
    // can appear — and the page is what falls out.
    //
    // It is harder to picture. Nobody looks at the code below and sees the prospectus, and the marketing
    // team cannot edit it. That is the real cost and it is not small.
    //
    // What it buys is composition and testing. Each rule is a function of one element, so each can be
    // tested alone, and a module rendered inside a course is the same rule as a module rendered on its own
    // — which a template achieves only by growing partials that pass state to each other.
    //
    // And it is the natural choice the moment one model must produce several formats. The portal renders
    // the prospectus as HTML for the site, as XML for the national course-comparison service, and as plain
    // text for the print supplement. Three transformations over one model; three templates would be three
    // copies of the same structure, and the third would drift.

    /// <summary>
    ///     Renders a prospectus element by element, in whatever format the rules produce.
    /// </summary>
    [TransformView]
    public interface IProspectusTransform {

        string Course(CourseElement course);

        string Module(ModuleElement module);

        string Fee(decimal amount, string currency);

    }

    public sealed record CourseElement(string Title, IReadOnlyList<ModuleElement> Modules, decimal Fee);

    public sealed record ModuleElement(string Title, int Credits);

}
