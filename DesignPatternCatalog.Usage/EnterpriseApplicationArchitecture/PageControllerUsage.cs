#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.PageControllerSample {

    // Enrolment portal: one controller per page — and the file next door is the alternative.
    //
    // A PAGE CONTROLLER handles the requests of one page. The prospectus page has one, the enrolment form
    // has one, the timetable has one. Each is independent, and adding a page adds a class rather than a
    // branch in something shared.
    //
    // It suits the portal's public side, where the pages genuinely differ more than they share: a
    // prospectus is cached and anonymous, a timetable is personal and never cached, and the two have almost
    // no request handling in common.
    //
    // The cost is visible below and is the honest reason to read FrontControllerUsage.cs before choosing:
    // both controllers begin by resolving the locale. So does every other page. That duplication is
    // tolerable at four pages, it is a bug farm at forty — one page will be added without it — and the
    // usual remedy, a base class every controller inherits, is a front controller with more steps.

    /// <summary>
    ///     The course prospectus page.
    /// </summary>
    [PageController]
    public sealed class ProspectusController {

        public string Show(string requestedLocale) {
            string locale = Locale.Resolve(requestedLocale);

            return $"prospectus:{locale}";
        }

    }

    /// <summary>
    ///     The timetable page — independent, and repeating the first two lines.
    /// </summary>
    [PageController]
    public sealed class TimetableController {

        public string Show(string requestedLocale, string studentNumber) {
            string locale = Locale.Resolve(requestedLocale);

            return $"timetable:{locale}:{studentNumber}";
        }

    }

    internal static class Locale {

        #region Statics members declarations

        public static string Resolve(string requested) {
            return string.IsNullOrWhiteSpace(requested) ? "en-GB" : requested;
        }

        #endregion

    }

}
