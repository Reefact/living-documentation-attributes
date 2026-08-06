#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.TwoStepViewSample {

    // Enrolment portal: the third answer, and the one chosen for a reason nothing else could satisfy.
    //
    // The university runs eleven sites off this portal — the main one, four faculties, the business school
    // with its own brand, the international office in three languages. The screens are the same; the look
    // is not. And the accessibility team changes a table's markup once and expects it to change everywhere.
    //
    // With a template per screen per site, that change is a hundred and forty files. With a transform view,
    // eleven transforms that drift.
    //
    // A TWO STEP VIEW renders in two stages: the data becomes a LOGICAL page — a title, a table, a set of
    // fields, a call to action — and a second step turns that into the actual markup for one site.
    //
    // The whole value is the second step being written once per look rather than once per screen. A change
    // to how every table is rendered is one change, and it reaches all eleven sites.
    //
    // The price is exactly as real. The logical layer must express everything every screen needs — the day
    // one screen needs something no logical element describes, the choice is to widen the vocabulary for
    // all of them or to break out of it, and both are expensive. That is why the pattern belongs to sites
    // with many screens and one look, and not to a site with three screens.

    /// <summary>
    ///     Step one: the page as a logical structure, with no markup anywhere in it.
    /// </summary>
    public abstract record LogicalElement;

    public sealed record LogicalTitle(string Text) : LogicalElement;

    public sealed record LogicalTable(IReadOnlyList<string> Headings, IReadOnlyList<IReadOnlyList<string>> Rows) : LogicalElement;

    public sealed record LogicalAction(string Label, string Target) : LogicalElement;

    /// <summary>
    ///     Step two: one implementation per look, shared by every screen of that site.
    /// </summary>
    [TwoStepView]
    public interface ISiteRenderer {

        string SiteName { get; }

        string Render(IReadOnlyList<LogicalElement> page);

    }

}
